/*      FPcGen: Simple Genetic Algorithms Package              */
/*              This version deals with double parameters.     */

#include <stdio.h>
#include <math.h>
double drand32();
#define MAX
#include "lib.h"

#define MAXARGS         20      /* Maximum args to the objective function */
#define MAXPOP          2000    /* Maximum population to be examined */
#define BESTPCT         1
#define ROULETTE        2
#define LINEAR          3
#define LN_INIT 1000
#define LN_DEC 20
#define GMAX 30

typedef  unsigned char * bits;

char *bit2str (bits, int);
unsigned long bextract (bits, int, int);
void binsert (bits, int, int, unsigned long);
bits bnew (int);
void ceval (int);
double decode (bits, int, int, double, double);
double drange (double, double);
void encode (bits, int, int, double, double, double);
double getarg (int n, bits thisc);
int getbit (bits, int);
double irange (int, int);
void outbit (bits, int);
void putbit (bits, int, int);
void reprod ();
int rbit();
void select (int);
bits str2bit (char *, int);
double feval (unsigned char *bs, int n);
void user_init();

/* The parameter list - list of packed bit-string parameters to the
   function to be optimized.                                            */

struct plentry {
	int n, sb;
	double dmin, dmax;
};
typedef struct plentry myarg;
typedef struct plentry * argptr;

argptr  allargs[MAXARGS];
static int nargs = 0;

/* A population is a large collection of chromosomes, each consisting of a
   set of genes (arguments to objective function). A chromosome is a long
   bit string; a gene is a shorter bit string.                          */

bits *population;       /* All chromosomes in this population */
int chromlen = 0;               /* Number of bits in a chromosome */
static int npop;                /* Number in this population now */
double evals[MAXPOP];           /* Evaluation for each chromosome */
double sbm_rate = 0.01;         /* Single bit mutation rate */
double sc_rate = 0.65;          /* Single crossover rate */
int bestpct = 10;               /* Percentage of chromosomes that reproduce */
int bestind = 200;              /* Number of reproducing chromosomes */
int niter = 100;                /* Number of iterations (generations) */
int select_method = LINEAR;     /* Parent selection method */


/*            B I T    S T R I N G    F U N C T I O N S                 */
/*
	A bit string is represented as a sequence of bytes, each
	byte having 8 bits. Bit 0 is the high bit (MSB):

	1  0  1  1  0  0  1  0    1  1  1  1  0  1  0  0  ...
bit #   0  1  2  3  4  5  6  7    8  9 10 11 12 13 14 15
byte #  0  0  0  0  0  0  0  0    1  1  1  1  1  1  1  1  

	GETBIT will return 1 or 0, the value of the specified bit in the string.
	BIT2STR will convert a bit string into a character string ('1' and '0').
	OUTBIT  will print a bit string to standard output.

*/

/* Extract the value of the Nth bit of the bit string given */

int getbit (bits given, int n)
{
	int i,j,k;

	i = n % 8;                      /* Which bit? */
	j = n / 8;                      /* Which byte */
	k = given[j];                   /* Get the byte */
	k = k>>(7-i);                   /* Shift the bit to low position */
	return (k&1);                   /* Return the bit */
}

/* Place a bit into a bit string */

void putbit (bits bstring, int n, int val)
{
	 int i,j,k;
 
	 i = n % 8;                      /* Which bit? */
	 j = n / 8;                      /* Which byte */
	 k = 1<<(7-i);                       /* Set the bit */
	if (val==1) bstring[j] |= k;
	 else if (val == 0) bstring[j] &= (~k);
	 else printf ("::PutBit  Non binary value = %d\n", val);
 }

/* Convert a bit string into a character string */

char * bit2str (bits bstring, int n)
{
	char *s1;
	int i;

	s1 = (char *)malloc (n+1);      /* Allocate a char per bit */
	for (i=0; i<n; i++)             /* Convert each bit to a '0' or '1' */
	   s1[i] = (char)(getbit(bstring, i)+(int)'0');

	s1[n] = '\0';                   /* Terminate the string */
	return s1;                      /* ... and return it */
}

/* Extract a bit sequence from a bit string */

unsigned long bextract (bits b, int start, int n)
{
	int i,j;
	long int val;

	val = 0; j = n-1;
	if (n>GMAX) 
	{
	  printf ("MAXIMUM gene size is %d bits: exceeded (%d)\n",GMAX, n);
	  exit(2);
	}
	for (i=start; i<start+n; i++)
	  val += getbit(b, i) << (j--);
	return val;
}

void binsert (bits b, int start, int n, unsigned long val)
{
	long int bval;
	int i,j;

	bval = val & ((1<<n)-1);
	if (bval != val)
	{
	  printf ("BINSERT: Value %ld does not fit into %d bits.\n", val, n);
	  exit (2);
	}

	j = n-1;
	for (i=start; i<start+n; i++)
	  putbit (b, i, ((1<<(j--))&bval)!=0);
}

/* Copy a bit string from one location to another */
void mbcopy (bits source, bits dest, int n)
{
	int i,j;

	for (i=0; i<n; i++) {
	   j = getbit (source, i);
	   putbit (dest, i, j);
	}
}

bits bnew (int size)
{
	return (bits)calloc (size/8+1, 1);
}

/* Convert a character string into a bit string */

bits str2bit (char *cstring, int n)
{
	int i;
	bits b1;

	b1 = (bits)malloc (n/8+1);              /* Allocate the bit string */
	for (i=0; i<n; i++)                     /* Copy each bit */
	   putbit (b1, i, (int)(cstring[i]-(int)'0'));
	return b1;
}

/* Print a bit string to the output file */

void outbit (bits bstring, int n)
{
	char *s;

	s = bit2str (bstring, n);       /* Convert from bit string to ASCII 1/0 */
	printf ("%s", s);               /* Print the ASCII string */
	free(s);                        /* Free the ASCII string */
}

/* Decode a data element from a bit string. Data element is coded as an integer
   in the range dmin..dmax into a bit string of length N starting at position
   SB in the bit string DATA. Value is returned as a signed double.             */

double decode (bits data, int sb, int n, double dmin, double dmax)
{
	int k;
	double z;
	bits b1;
	unsigned long t;

	t = bextract(data, sb, n);      /* Get the relevant bit string */
	k = 1<<n;                       /* How many numbers possible? */
	z = (dmax-dmin)/(double)k;      /* Value of each bit         */
	return dmin + t*z;
}

void encode (bits data, int sb, int n, double dmin, double dmax, double val)
{
	int i,k;
	unsigned long t;
	double z;

	if ((val<dmin) || (val>=dmax)) 
	{
		printf ("::Encode - data value %lf is out of range.\n", val);
		return;
	}
	k = 1<<n;                       /* How many numbers are possible? */
	z = (dmax-dmin)/(double)k;      /* Value of each bit */
	t = (long)((val-dmin)/z + 0.5); /* Encoded fixed point value */
	if ((dmin+t*z)>=dmax) t = t-1;
	binsert (data, sb, n, t);
}

double getarg (int n, bits thisc)
{
	double x;

	if (n >= nargs) 
	{
	   printf ("::GETARG - Asked for arg %d, there are only %d.\n", n, nargs);
	   return 0.0;
	}
	x = decode (thisc, allargs[n]->sb, allargs[n]->n, allargs[n]->dmin,
			allargs[n]->dmax);
	return x;
}

/* ****************************************************************************

		Random Number routines

   **************************************************************************** */

/* return a random double in the range x1...x2  */

double drange (double x1, double x2)
{
	return drand32()*(x2-x1) + x1;
}

/* Return a random integer in the range i1 ... i2       */

double irange (int i1, int i2)
{
	return (int)((i2-i1+1)*drand32())+i1;
}

/* Return a random single bit   */

int rbit()
{
	if(drand32() < 0.5) return 0;
	return 1;
}

/* Return a random bit string N bits in length  */

bits rbits (int n)
{
	int i;
	bits b;

	b = (bits)malloc(n/8+1);
	for (i=0; i<n; i++)
	   putbit(b, i, rbit());
	return b;
}

/* Implement a single bit mutation */

void b1mut (bits b, int n)
{
	int i,j;

	for (i=0; i<n; i++) 
	   if (drand32() < sbm_rate) {
		j = rbit();             /* A random bit */
		if (getbit(b, i) != j)
		   putbit (b, i, j);
	   }
}


/*              Wrapper - Genetic Methods Stuff and initialization */

init()
{
	int i,j,k, nextbit;
	argptr tmp;

	printf ("\t\tLaboratory for Computer Vision\n");
	printf ("\tGenetic Algorithms Workbench\n\n");
	printf ("Enter population size (<%d): ", MAXPOP);
	scanf ("%d", &npop);
	if (npop%2) {
		printf ("::INIT - Population increased by 1 to %d\n", npop+1);
		npop++;
	}
	if (npop > MAXPOP) {
		printf ("::INIT: Population too large - using %d\n", MAXPOP);
		npop = MAXPOP;
	}

	population = (bits *)malloc (npop*sizeof(bits *));
	printf ("Enter number of arguments to the objective function(<20): ");
	scanf ("%d", &nargs);

	user_init ();

	nextbit = 0;
	for (i=0; i<nargs; i++) {
	   tmp = (argptr)malloc(sizeof(myarg));
	   printf ("Argument %d: number of bits? ", i);
	   scanf ("%d", &(tmp->n));
	   printf ("Argument %d: Minimum value?  ", i);
	   scanf ("%lf", &(tmp->dmin));
	   printf ("Argument %d: Maximum value?  ", i);
	   scanf ("%lf", &(tmp->dmax));
	   allargs[i] = tmp;
	   tmp->sb = nextbit;
	   nextbit += tmp->n;
	}

	printf ("What percentage of the chromosomes can reproduce? (1-100): ");
	chromlen = nextbit;
	for (i=0; i<npop; i++)  {
		population[i] = rbits(chromlen);
		ceval (i);
	}
	scanf ("%d", &bestpct);
	if (bestpct > 100 || bestpct < 1) {
		printf ("::Init - must be between 1 and 100.\n\n");
		exit(0);
	}
	bestind = ((float)bestpct/100.0) * npop;

	printf ("How many generations? (integer): ");
	scanf ("%d", &niter);
	printf ("\n%d iterations confirmed.\n\n", niter);
}

void reinit ()
{
	int i;

	for (i=0; i<npop; i++) {
	   population[i] = rbits(chromlen);
	   ceval (i);
	}

}

/* Evaluate chomosome N and store result in EVALS array */

void ceval (int n)
{
	evals[n] = feval (population[n], chromlen);
}

void iterate ()
{
	int i,j;

	for (i=0; i<niter; i++) {
	   printf ("."); fflush (stdout);
	   select(i == niter-1);        /* Sort population based on fitness */
	   reprod();                    /* Reproduce */
	   for (j=0; j<npop; j++)
		ceval(j);
	}
	printf ("\n");
}

void select (int f)
{
	bits tb;
	double td;
	int i,j,k;

	for (i=0; i<npop; i++) {
	   for (j=i+1; j<npop; j++) {
	      if (evals[j] < evals[i]) {        /* Swap */
		tb = population[i]; population[i] = population[j]; population[j] = tb;
		td = evals[i]; evals[i] = evals[j]; evals[j] = td;
	      }
	   }
	}

	if (f) {
	  printf ("Best evaluations this generation:\n");
	  printf ("Chromosome Eval                Args\n");
	  for (i=0; i<bestind; i++) {
	     printf ("%d %lf    ", i, evals[i]);
	     for (j=0; j<nargs; j++) 
		printf ("%lf\t", getarg(j, population[i]));
	     printf ("\n");
	  }
	}
}

/* Perform a 1 point crossover */
void cross1 (bits s1, bits s2, int n)
{
	int i,k,c1;
	bits btemp;

	c1 = irange (1, chromlen-1);

	btemp = bnew (chromlen);
	mbcopy (s1, btemp, chromlen);

	for (i=c1; i<chromlen; i++) {
	   k = getbit (s2, i);
	   putbit (s1, i, k);

	   k = getbit(btemp, i);
	   putbit (s2, i, k);
	}
	free (btemp);
}

double rw_setup (double *rw, double *ev)
{
	int i,j,k;
	int v1, vmin;
	double tot;

	v1 = LN_INIT;
	i = 0;
	while (i<npop)
	{
	  rw[i++] = v1;
	  if (i>0 && (evals[i]==evals[i-1])) continue;
	  v1 -= LN_DEC;
	}

	vmin = rw[npop-1]; tot = 0.0;
	for (i=0; i<npop; i++)
	{
	  if (i>0 && evals[i]==evals[i-1])
	    rw[i] = 0;
	  else 
	  {
	    rw[i] -= vmin;
	    tot += rw[i];
	    if (rw[i]==0) rw[i] = 1;
	  }
	}
	return tot;
}

void reprod ()
{
	int i,j;
	double rw[MAXPOP], rmax;                /* Roulette wheel */
	bits *new;
	double r1, r2, psum;
	int p1, p2;

	switch (select_method) {

case BESTPCT:
/* Replace the least fit 90% of the population with mutated
   versions of the best 10%.  */
	for (i=bestind+1; i<npop; i++) {  
	   j = irange(0, bestind-1);
	   mbcopy (population[j], population[i], chromlen);
	   b1mut (population[i], chromlen);
	}
	break;

case LINEAR:
	rmax = rw_setup(rw, evals);
	new = (bits *)malloc(npop*sizeof(bits));
	for (i=bestind+1; i<npop; i++) new[i] = (bits)malloc(chromlen/8+1);
 
/* Replace each chromosome with a new one, based on random parents */
	for (i=bestind+1; i<npop; i=i+2) {
	   r1 = drange (0.0, rmax);     /* Roll the wheel, parent 1 */
	   p1 = 0; psum = rw[0];
	   while ((psum<r1) && (p1 < npop)) 
	   {
		p1++;
		psum += rw[p1];
	   }

	   r2 = drange (0.0, rmax);     /* Roll the wheel, parent 2 */
	   p2 = 0; psum = rw[0];
	   while ((psum<r2) && (p2 < npop)) 
	   {
		p2++;
		psum += rw[p2];
	   }

	   mbcopy (population[p1], new[i], chromlen);
	   if (i+1 < npop) {
	    mbcopy (population[p2], new[i+1], chromlen);
	    if (drand32() < sc_rate)            /* Single crossover */
		cross1 (new[i], new[i+1], chromlen);
	   }

	   b1mut (new[i], chromlen);
	   if (i+1 < npop) 
	    b1mut (new[i+1], chromlen);
	}

/* Copy new into old */
	   for (i=bestind+1; i<npop; i++) {
		free(population[i]);
		population[i] = new[i];
	   }
	break;

case ROULETTE:
	rmax = evals[npop-1];   /* Set up roulette wheel for MIN finding */
	rw[0] = rmax -  evals[0];
	new = (bits *)malloc(npop*sizeof(bits));        /* Allocate new population */
	new[0] = (bits)malloc(chromlen/8+1);

	for (i=bestind+1; i<npop; i++) {
		rw[i] = rw[i-1]+(rmax-evals[i]);
		evals[i] = rmax-evals[i];
		new[i] = (bits)malloc(chromlen/8+1);
	}
	rmax = rw[npop-1];

/* Replace each chromosome with a new one, based on random parents */
	for (i=bestind+1; i<npop; i=i+2) {
	   r1 = drange (0.0, rmax);     /* Roll the wheel, parent 1 */
	   p1 = 0; psum = rw[0];
	   while ((psum<r1) && (p1 < npop)) {
		p1++;
		psum += evals[p1];
	   }

	   r2 = drange (0.0, rmax);     /* Roll the wheel, parent 2 */
	   p2 = 0; psum = rw[0];
	   while ((psum<r2) && (p2 < npop)) {
		p2++;
		psum += evals[p2];
	   }

	   mbcopy (population[p1], new[i], chromlen);
	   if (i+1 < npop) {
	    mbcopy (population[p2], new[i+1], chromlen);
	    if (drand32() < sc_rate)            /* Single crossover */
		cross1 (new[i], new[i+1], chromlen);
	   }

	   b1mut (new[i], chromlen);
	   if (i+1 < npop) 
	    b1mut (new[i+1], chromlen);
	}

/* Copy new into old */
	   for (i=bestind+1; i<npop; i++) {
		free(population[i]);
		population[i] = new[i];
	   }
	break;

default:
	printf ("::REPROD - bad parent selection specified.\n");
	exit (0);
	}
}

/* Small system random number generator 

long seed = 132531;

double drand32 ()
{
	static long a=16807L, m=2147483647L,
		    q=127773L, r = 2836L;
	long lo, hi, test;

	hi = seed / q;
	lo = seed % q;
	test = a*lo -r*hi;
	if (test>0) seed = test;
	else seed = test + m;

	return (double)seed/(double)m;
}                                          */

int main ()
{
	int i,j,k,n,sb;
	char *x1, x2[20];
	int dat[3], h[12];
	double d, d2, dmin, dmax;
	bits b1;

	printf (" Mutation rate %lf crossover rate %lf\n",
	   sbm_rate, sc_rate);
	init();
	iterate();
	return 0;
}
