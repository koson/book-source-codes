/*      User-defined functions for the geometric problem */

/*      IPcGen : Simple Genetic Algorithms Package              */
/*               This version deals with integer parameters.    */

#define MAX
#include <stdio.h>
#include <math.h>
#include "lib.h"
double drand32();

#define MAXARGS         20      /* Maximum args to the objective function */
#define MAXPOP          2000    /* Maximum population to be examined */
#define BESTPCT         1
#define GMAX 30
#define MAXCREG 100
#define MAXCPIX 10000

IMAGE im, dist, edge, fb;
int N=0;
int plistx[MAXCPIX], plisty[MAXCPIX];
int PLOT = 0;

struct rr {
	int start, end;
} rlist[MAXCREG];
int creg;


typedef  unsigned char * bits;

char *bit2str (bits, int);
unsigned long bextract (bits, int, int);
void binsert (bits, int, int, unsigned long);
bits bnew (int);
void ceval (int);
int decode (bits, int, int);
double drange (double, double);
void encode (bits, int, int, int);
int getarg (int n, bits thisc);
int getbit (bits, int);
double irange (int, int);
void outbit (bits, int);
void putbit (bits, int, int);
void reprod ();
int rbit();
void select (int);
bits str2bit (char *, int);
double rw_setup (double *rw, double *ev);

void edge_sobel (IMAGE x, IMAGE z);
float putpix (float x, float y);
float circle (float h, float k, float rad, IMAGE im);
float circlepts (float x, float y, float xc, float yc);
double det3 (float a11, float a12, float a13, float a21, float a22, float a23,
		float a31, float a32, float a33);
void circle_3pt (float x1, float y1, float x2, float y2, float x3, float y3,
			double *A, double *D, double *E, double *F);
void locate_regions (IMAGE im);
void sample ();
void pix_lookup (int r, float *x, float *y);
void save_region (IMAGE im, int val);
void mark_region (IMAGE im, int row, int col, int val);

/* User functions */
void user_init ();
double ieval (bits chrom, int n);


/* The parameter list - list of packed bit-string parameters to the
   function to be optimized.                                            */

struct plentry {
	int n, sb;
	double dmin, dmax;
};
typedef struct plentry * argptr;

argptr  allargs[MAXARGS];
int nargs = 0;
int cpix = 0;

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
int select_method = BESTPCT;     /* Parent selection method */


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

/* Decode a data element from a bit string. Data element is coded as an 
   integer  into a bit string of length N starting at position
   SB in the bit string DATA. Value is returned as a signed int.        */

int decode (bits data, int sb, int n)
{
	int k;
	double z;
	bits b1;
	unsigned long t;

	t = bextract(data, sb, n);      /* Get the relevant bit string */
	return (int)t;
}

void encode (bits data, int sb, int n, int val)
{
	int i,k;
	unsigned long t;
	double z;

	k = 1<<n;                       /* How many numbers are possible? */
	if (val >= k)
	{
	  printf ("Value %d is bigger than the space permits (%d)\n", val,k);
	  exit (1);
	}
	t = val;
	binsert (data, sb, n, t);
}

int getarg (int n, bits thisc)
{
	int x;

	if (n >= nargs) 
	{
	   printf ("::GETARG - Asked for arg %d, there are only %d.\n", n, nargs);
	   return 0.0;
	}
	x = decode (thisc, allargs[n]->sb, allargs[n]->n);
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

	user_init();

	nextbit = 0;
	for (i=0; i<nargs; i++) {
	   tmp = (argptr)malloc(sizeof(arg));
	   printf ("Argument %d: number of bits? ", i);
	   scanf ("%d", &(tmp->n));
	   tmp->dmin = 0; tmp->dmax = cpix;

	   allargs[i] = tmp;
	   tmp->sb = nextbit;
	   nextbit += tmp->n;
	}

	printf ("What percentage of the chromosomes can reproduce? (1-100): ");
	chromlen = nextbit;

	for (i=0; i<npop; i++)  
		population[i] = rbits(chromlen);
	sample();
	for (i=0; i<npop; i++)  
		ceval (i);

	scanf ("%d", &bestpct);
	if (bestpct > 100 || bestpct < 1) 
	{
		printf ("::Init - must be between 1 and 100.\n\n");
		exit(0);
	}
	bestind = ((float)bestpct/100.0) * npop;

	printf ("How many generations? (integer): ");
	scanf ("%d", &niter);
}

/* Evaluate chomosome N and store result in EVALS array */
void ceval (int n)
{
	evals[n] = ieval (population[n], chromlen);
}

void iterate ()
{
	int i,j;

	for (i=0; i<niter; i++) {
	   printf ("."); fflush (stdout);
	   select (1);
/*         select(i == niter-1);        /* Sort population based on fitness */
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
	int x1,y1;

	for (i=0; i<npop; i++) {
	   for (j=i+1; j<npop; j++) {
	      if (evals[j] > evals[i]) {        /* Swap */
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
	     {
		k = getarg(j, population[i]);
		x1 = plistx[k]; y1 = plisty[k];
		printf ("%d(%d,%d)\t", k, x1, y1);
	     }
	     printf ("\n");
	  }
	}
}

void reprod ()
{
	int i,j;

/*    Replace the least fit 90% of the population 
      with mutated versions of the best 10%.                           */

	for (i=bestind+1; i<npop; i++) 
	{  
	   j = irange(0, bestind-1);
	   mbcopy (population[j], population[i], chromlen);
	   b1mut (population[i], chromlen);
	}
}

int main ()
{
	int i,j,k,n,sb;
	char *x1, x2[20];
	int dat[3], h[12];
	double d, d2, dmin, dmax;
	bits b1;
	double A, D, E, F;
	float cx, cy, radius, a;
	int p1, p2, p3;

	sbm_rate = .01; sc_rate = 0.6;
	printf (" Mutation rate %lf, NO CROSSOVERS\n", sbm_rate);
	init();
	iterate();

	PLOT = 1;
	for (i=0; i<im->info->nr; i++)
	  for (j=0; j<im->info->nc; j++)
	      im->data[i][j] = 255;

	for (i=0; i<=npop; i++)
	{
	  if ((evals[0]-evals[i]) > (evals[0]*0.1)) break;
	  p1 = getarg(0, population[i]);
	  p2 = getarg(1, population[i]);
	  p3 = getarg(2, population[i]);
	  printf (" (%d,%d) (%d,%d) (%d,%d)\n", plistx[p1],plisty[p1],
	      plistx[p2],plisty[p2],plistx[p3],plisty[p3]);
	  circle_3pt (plistx[p1],plisty[p1],plistx[p2],plisty[p2],
			plistx[p3],plisty[p3], &A, &D, &E, &F);
	  if (A >= -0.001 && A <= 0.001) {  continue; }
	  D = D/(2*A);
	  E = E/(2*A);
	  F = F/(A);
	  cx = -D; cy = -E;
	  radius = sqrt (D*D + E*E - F);
	  printf (" (%f,%f) radius %f gives ", cx, cy, radius);
	  a = circle (cx, cy, radius, edge);
	  printf (" %f\n", a);
	}
	Display (im);
	return 0;
}

void user_init ()
{
	int i,j, k;
	FILE *f;
	char fn[128];

	printf ("This GA locates geometric shapes in an input image.\n");

	printf ("The name of the image file is: ");
	scanf ("%s", fn);
	im = Input_PBM (fn);
	if (im == 0) exit (1);
	fb = im;

/* Edge enhance */
	edge = newimage (im->info->nr, im->info->nc);
	printf ("Edge detection ...\n");
	edge_sobel (im, edge);
	Display (edge);

/* Find connected regions */
	locate_regions (edge);

}

/* Evaluate chomosome N and store result in EVALS array */
double ieval (unsigned char *bs, int n)
{
	int i,j,k;
	int r1, r2, r3;
	double A, D, E, F, a=0;
	float x1, x2, x3, y1, y2, y3, penalty = 0.0;
	float cx, cy, radius;

/* Extract the arguments */
	r1 = getarg(0, bs);
	r2 = getarg(1, bs);
	r3 = getarg(2, bs);

	if (r1==r2 || r2==r3 || r1==r3)
		return -1000.0;

/* Arguments are pixel numbers - find the pixel coordinates */
	pix_lookup (r1, &x1, &y1);
	pix_lookup (r2, &x2, &y2);
	pix_lookup (r3, &x3, &y3);

/* Find the equation of the circle */
	if (x1 < 0.0 || x2 < 0.0 || x3 < 0.0) 
		return -2002.0;
	circle_3pt (x1, y1, x2, y2, x3, y3, &A, &D, &E, &F);

/* Compute constraint penalties */
	if (A < 0.001 && A>= -0.001)
	{
	  penalty = -10000.0;
	  return penalty;
	}
	else {
	  D = D/(2*A); E = E/(2*A); F = F/(A);
	  cx = -D; cy = -E;
	  if (D*D + E*E - F >= 0.0)
	    radius = sqrt (D*D + E*E - F);
	  else radius = 0.0;
	  if (radius < 4.0 || radius > 126.0) penalty = -2008.0;
	}

/* Compute the distance for this circle */
	a = circle (cx, cy, radius, edge);
	return (a + penalty);
}

float circle (float h, float k, float rad, IMAGE edge)
{
	float x, y, dp, res=0;
	int N=0;

	x = 0;  y = rad;
	dp = 1-rad;
	res = circlepts (x, y, h, k);
	N += 1;

	while (y > x)
	{
	  if (dp<0)
	  {
	    dp += 2*x + 3;
	    x += 1;
	  }
	  else 
	  {
	    dp += 2*(x-y)+5;
	    x += 1; y -= 1;
	  }
	  res += circlepts (x, y, h, k);
	  N += 1;
	}
	return (float)(res);
}

float circlepts (float x, float y, float xc, float yc)
{
	int res = 0;

	res = putpix (xc+x, yc+y);
	res += putpix (xc+y, yc+x);

	res += putpix (xc+y, yc-x);
	res += putpix (xc+x, yc-y);

	res += putpix (xc-x, yc-y);
	res += putpix (xc-y, yc-x);

	res += putpix (xc-y, yc+x);
	res += putpix (xc-x, yc+y);
	return (float)res;
}


/* Compute the equation of a circle given three points on that circle */
void circle_3pt (float x1, float y1, float x2, float y2, float x3, float y3,
			double *A, double *D, double *E, double *F)
{
	float z1, z2, z3;

	*A = det3 (x1, y1, 1.0, x2, y2, 1.0, x3, y3, 1.0);
	z1 = x1*x1+y1*y1;
	z2 = x2*x2+y2*y2;
	z3 = x3*x3+y3*y3;
	*D = -det3 (z1, y1, 1.0, z2, y2, 1.0, z3, y3, 1.0);
	*E = det3 (z1, x1, 1.0, z2, x2, 1.0, z3, x3, 1.0);
	*F = -det3 (z1, x1, y1, z2, x2, y2, z3, x3, y3);
}

double det3 (float a11, float a12, float a13, float a21, float a22, float a23,
		float a31, float a32, float a33)
{
	double tmp;

	tmp = -a31*a22*a13 - a32*a23*a11 - a33*a21*a12 
	      +a11*a22*a33 + a12*a23*a31 + a13*a21*a32;
	return tmp;
}

/* Sobel edge detector - no templates */

void thresh (IMAGE z)
{
	int histo[256];
	int i,j,t;
	
/* Compute a grey level histogram */
	for (i=0; i<256; i++) histo[i] = 0;
	for (i=1; i<z->info->nr-1; i++)
	  for (j=1; j<z->info->nc-1; j++)
	  {
	    histo[z->data[i][j]]++;
	  }
	
/* Threshold at the middle of the occupied levels */
	i = 255; 
	while (histo[i] == 0) i--;
	j = 0;
	while (histo[j] == 0) j++;
	t = (i+j)/2;

/* Apply the threshold */
	for (i=1; i<z->info->nr-1; i++)
	  for (j=1; j<z->info->nc-1; j++)
	    if (z->data[i][j] >= t) z->data[i][j] = 0;
	    else z->data[i][j] = 255;
}

/*      Apply a Sobel edge mask to the image X  */

void edge_sobel (IMAGE x, IMAGE z)
{
	int i,j,n,m,k;

	for (i=0; i<x->info->nr; i++)
	  for (j=0; j<x->info->nc; j++) 
	    z->data[i][j] = 255;

/* Now compute the convolution, scaling */
	for (i=1; i<x->info->nr-1; i++)
	  for (j=1; j<x->info->nc-1; j++) 
	  {
	    n = (x->data[i-1][j+1]+2*x->data[i][j+1]+x->data[i+1][j+1]) -
		(x->data[i-1][j-1]+2*x->data[i][j-1]+x->data[i+1][j-1]);
	    m = (x->data[i+1][j-1]+2*x->data[i+1][j]+x->data[i+1][j+1])-
		(x->data[i-1][j-1]+2*x->data[i-1][j]+x->data[i-1][j+1]);
	    k = (int)( sqrt( (double)(n*n + m*m) )/4.0 );
	    z->data[i][j] = k;
	  }

/* Threshold the edges */
	thresh (z);
}

float putpix (float xx, float yy)
{
	int res = 0, x, y, i, j;

	x = (int)(xx+0.5); y = (int)(yy+0.5);
	if (x < 0 || x >= edge->info->nc) return -4;
	if (y < 0 || y >= edge->info->nr) return -4;
/*
	for (i=y-1; i<=y+1; i++)
	  for (j=x-1; j<=x+1; j++)
	    if(range(edge, i,j) && edge->data[i][j]<creg) res += 1;
	if (edge->data[y][x] < creg) res += 2;
*/

	if (edge->data[y][x] < creg) res = 1;
	else res = -1;

	if (PLOT)
	  fb->data[y][x] = 0;
	return (float)res;
}

void locate_regions (IMAGE im)
{
	int i,j,mark=1, k=0;

	for (i=0; i<im->info->nr; i++)
	  for (j=0; j<im->info->nc; j++)
	    if (im->data[i][j] == 0)
	    {
	      if (creg >= MAXCREG)
	      {
		printf ("Too many regions (max is %d)\n", MAXCREG);
		exit (1);
	      }

	      mark_region (im, i, j, mark);
	      rlist[creg].start = cpix;
	      save_region (im, mark);
	      rlist[creg].end   = cpix-1;
	      mark++; creg++;
	    }
	printf ("Found %d regions\n", mark-1);

/*        for (i=0; i<creg; i++)
	{
	  k = 0;
	  printf ("======= Region %d ========\n", i);           
	  for (j=rlist[i].start; j<=rlist[i].end; j++)
	  {
	    printf ("(%d,%d) ", plistx[j], plisty[j]);         
	    if (k++ > 10)
	    {
		printf ("\n");
		k = 0;
	    }
	  }
	  if (k) printf ("\n");
	}                                        */
}

/* Recursive flood fill */
void mark_region (IMAGE im, int row, int col, int val)
{
	im->data[row][col] = val;
	if (row>0)
	{
	  if (col-1>=0 &&im->data[row-1][col-1] == 0)
		 mark_region (im, row-1,col-1, val);
	  if (im->data[row-1][col] == 0)
		 mark_region (im, row-1,col, val);
	  if (col+1<im->info->nc &&im->data[row-1][col+1] == 0)
		 mark_region (im, row-1,col+1, val);
	}

	if (im->data[row][col-1] == 0) mark_region (im, row,col-1, val);
	if (im->data[row][col+1] == 0) mark_region (im, row,col+1, val);

	if (row+1 < im->info->nr)
	{
	  if (col-1>=0 &&im->data[row+1][col-1] == 0)
		 mark_region (im, row+1,col-1, val);
	  if (im->data[row+1][col] == 0)
		 mark_region (im, row+1,col, val);
	  if (col+1 < im->info->nc &&im->data[row+1][col+1] == 0)
		 mark_region (im, row+1,col+1, val);
	}
}

void save_region (IMAGE im, int val)
{
	int i,j;

	for (i=0; i<im->info->nr; i++)
	  for (j=0; j<im->info->nc; j++)
	    if (im->data[i][j] == val)
	    {
		if (cpix >= MAXCPIX)
		{
		  printf ("Too many pixels (max is %d)\n", MAXCPIX);
		  exit (1);
		}
		plistx[cpix] = j;
		plisty[cpix] = i;
		cpix++;
	    }
}

void sample ()
{
	int i, j, k, p1,p2,p3, is, ie;

	for (i=0; i<npop; i++)
	{
	  k = irange (0, creg-1);
	  is = rlist[k].start; ie = rlist[k].end;
	  p1 = irange (is, ie);

	  p2 = p1;
	  while (p2 == p1)
	    p2 = irange(is, ie);

	  p3 = p2;
	  while (p3 == p1 || p3 == p2)
	    p3 = irange(is, ie);

	  encode (population[i], allargs[0]->sb, allargs[0]->n, p1);
	  encode (population[i], allargs[1]->sb, allargs[1]->n, p2);
	  encode (population[i], allargs[2]->sb, allargs[2]->n, p3);
	
	}
}

void pix_lookup (int r, float *x, float *y)
{
	if (r < cpix)
	{
	  *x = plistx[r]; *y = plisty[r];
	} else
	{
	  *x = -1; *y = -1;
	}
}
