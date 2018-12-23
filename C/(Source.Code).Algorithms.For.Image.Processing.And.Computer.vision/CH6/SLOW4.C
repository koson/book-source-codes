/* Fourth iteration : Steps to a Fast Fourier Transform */
/* Assumes: power of 2 for N, and thus a recursive FFT call */

#include <math.h>
#include <stdio.h>
#include <malloc.h>

#define twopi 3.1415926535 * 2.0
struct cpx {
	float real;
	float imag;
};
typedef struct cpx COMPLEX;

void cexp (COMPLEX z1, COMPLEX *res);
void csum (COMPLEX z1, COMPLEX z2, COMPLEX *res);
void cmult (COMPLEX z1, COMPLEX z2, COMPLEX *res);
void cmplx (float rp, float ip, COMPLEX *z);
void cdif (COMPLEX z1, COMPLEX z2, COMPLEX *res);
void cdiv (COMPLEX z1, COMPLEX z2, COMPLEX *res);
float cnorm (COMPLEX z);
void slowft (float *x, COMPLEX *y, int n);
void evenodd (COMPLEX *x, COMPLEX *y, int n);

static float data[2050];
static COMPLEX Data[2050];

void NEXT ()
{
	static int i = 0;
	static char xc[5] = {'|', '/', '-', '\\', ' '};

	fprintf (stdout, "%c%c", 010, xc[i++]);
	fflush (stdout);
	if (i>3) i = 0;
}

int main()
{
	int i,j,k,n;
	double x, y, z, t, t2;
	FILE *f;

	n = 1024;               /* Size of sampled signal */
	t = 1.0/128.0;          /* Period of signal */
	t2 = 1.0/300.0;
 
	for (i=0; i<n; i++)
	{
	   data[i] = 2.0* sin(twopi * t * i)
		 /* + 3*sin (twopi * t2 * i) */ ;
	   data[i+n] = 0.0;
	}
 
	printf ("Fourier Transform of sine wave: vers. 4\n");
	slowft (data, Data, 1024);

	f = fopen ("slow4.out", "w");
	if (f == NULL)
	{
		printf ("Can't open the output file 'slow4.out'\n");
		exit (1);
	}

	for (i=0; i<n; i++)
	  fprintf (f, "%d %f\n", i, cnorm (Data[i]));
	fclose (f);
	return (0);

	data[0] = -6.0; data[1] = -2.0; data[2] = 0.0; 
	data[3] = 2.0;  data[4] = 4.0;  data[5] = 6.0;
	data[6] = 3.0;  data[7] = -1.0;
	slowft (data, Data, 8);
	printf ("  M      Real		imag\n");
	for (i=0; i<=4; i++)
	  printf (" %2d    %8.4f	  %8.4f\n", i, Data[i].real,
		Data[i].imag);
	exit(0);

}

void cexp (COMPLEX z1, COMPLEX *res)
{
	COMPLEX x, y;

	x.real = exp((double)z1.real);
	x.imag = 0.0;
	y.real = (float)cos((double)z1.imag);
	y.imag = (float)sin((double)z1.imag);
	cmult (x, y, res);
}

void cmult (COMPLEX z1, COMPLEX z2, COMPLEX *res)
{
	res->real = z1.real*z2.real - z1.imag*z2.imag;
	res->imag = z1.real*z2.imag + z1.imag*z2.real;
}

void csum (COMPLEX z1, COMPLEX z2, COMPLEX *res)
{
	res->real = z1.real + z2.real;
	res->imag = z1.imag + z2.imag;
}

void cdif (COMPLEX z1, COMPLEX z2, COMPLEX *res)
{
	res->real = z1.real - z2.real;
	res->imag = z1.imag - z2.imag;
}

void cdiv (COMPLEX z1, COMPLEX z2, COMPLEX *res)
{
	 float z,real, imag;

	z = cnorm (z2);
	if (z != 0.0)
	{
	  res->real = (z1.real*z2.real + z1.imag*z2.imag)/z;
	  res->imag = (z2.real*z1.imag - z1.real*z2.imag)/z;
	  return;
	}
	res->real = res->imag = 0.0;
	printf ("ZERO DIVIDE!\n");
}

void cmplx (float rp, float ip, COMPLEX *z)
{
	z->real = rp;
	z->imag = ip;
}

float cnorm (COMPLEX z)
{
	return z.real*z.real + z.imag*z.imag;
}

void slowft (float *x, COMPLEX *y, int n)
{
	COMPLEX *xx;
	int i;

	xx = (COMPLEX *)malloc(sizeof(struct cpx)*1024);
	for (i=0; i<n; i++)
	  cmplx (x[i], 0.0, &(xx[i]));
	evenodd (xx, y, n);
	free (xx);
}

void evenodd (COMPLEX *x, COMPLEX *y, int n)
{
	int m, i;
	COMPLEX *Sum, *Diff;
	COMPLEX *Even, *Odd;
	COMPLEX z1, z2, z3, z4,  tmp, two;
	float xs, ys;

	if (n < 1)
	  printf ("What???? \n");

/* The simple case, where N=1 */
	cmplx (2.0, 0.0, &two);
	if (n == 1)
	{
	  cmplx (x[0].real, x[0].imag, &(y[0]));
	  return;
	}

/* Otherwise, N is even */
	m = n/2;
	cmplx (0.0, atan (1.0)/n * -8.0, &z1);
	cexp (z1, &tmp);
	cmplx (1.0, 0.0, &z1);

/* Allocate temporary space  */
	Sum  = (COMPLEX *)malloc (sizeof (struct cpx) * m);
	Diff = (COMPLEX *)malloc (sizeof (struct cpx) * m);
	Even = (COMPLEX *)malloc (sizeof (struct cpx) * m);
	Odd  = (COMPLEX *)malloc (sizeof (struct cpx) * m);
	if (Sum==0 || Diff==0 || Even==0 || Odd==0)
	{
	  printf ("Panic - not enough memory.\n");
	  exit(1);
	}

	for (i=0; i<m; i++)
	{
	  NEXT();
	  csum (x[i], x[i+m], &z3);
	  cdiv (z3, two, &(Sum[i]));

	  cdif (x[i], x[i+m], &z3);
	  cmult (z3, z1, &z4);
	  cdiv (z4, two, &(Diff[i]));

	  cmult (z1, tmp, &z2);
	  cmplx (z2.real, z2.imag, &z1);
	}

	evenodd (Sum, Even, m);
	evenodd (Diff, Odd,  m);
	
	for (i=0; i<m; i++)
	{
	  y[i*2].real = Even[i].real;
	  y[i*2].imag = Even[i].imag;
	  y[i*2 + 1].real = Odd[i].real;
	  y[i*2 + 1].imag = Odd[i].imag;
	}

	free(Sum); free(Diff);
	free(Even); free(Odd);
}

