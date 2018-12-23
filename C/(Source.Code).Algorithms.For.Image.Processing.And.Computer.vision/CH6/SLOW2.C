/* Test of Fourier transform */
#include <math.h>
#include <stdio.h>
#include <malloc.h>

#define twopi 3.1415926535 * 2.0
struct cpx {
	double real;
	double imag;
};
typedef struct cpx COMPLEX;

void slowft (double *x, COMPLEX *y, int n);
void cexp (COMPLEX z1, COMPLEX *res);
void csum (COMPLEX z1, COMPLEX z2, COMPLEX *res);
void cmult (COMPLEX z1, COMPLEX z2, COMPLEX *res);
void cmplx (double rp, double ip, COMPLEX *z);
double cnorm (COMPLEX z);
void NEXT ();

static double *data;
static COMPLEX *Data;

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

	n = 1024;               /* Size of sampled signal */
	t = 1.0/128.0;          /* Period of signal */
	
	Data = (COMPLEX *)malloc(sizeof(struct cpx)*2050);
	data = (double *) malloc(sizeof(double)*2050);

	for (i=0; i<n; i++)
	{
	   data[i] = 2.0* sin(twopi * t * i)
		 /* + 3*sin (twopi * t2 * i) */ ;
	   data[i+n] = 0.0;
	}
 
	printf ("Slow Fourier Transform:\n");
	slowft (data, Data, 1024);
	return 0;
}

void cexp (COMPLEX z1, COMPLEX *res)
{
	COMPLEX x, y;

	x.real = exp((double)z1.real);
	x.imag = 0.0;
	y.real = cos(z1.imag);
	y.imag = sin(z1.imag);
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

void cmplx (double rp, double ip, COMPLEX *z)
{
	z->real = rp;
	z->imag = ip;
}

double cnorm (COMPLEX z)
{
	return z.real*z.real + z.imag*z.imag;
}

void slowft (double *x, COMPLEX *y, int n)
{
	COMPLEX tmp, z1, z2, z3, z4, *pre;
	long  m, k, i, p;

	pre = (COMPLEX *)malloc(sizeof(struct cpx)*1024);

/* Constant factor -2 pi */
	cmplx (0.0, atan (1.0)/n * -8.0, &z1);  
	cexp (z1, &tmp);

/* Pre-compute most of the exponential */
	cmplx (1.0, 0.0, &z1);          /* Z1 = 1.0; */
	printf (" ");
	for (i=0; i<n; i++)
	{
	  cmplx (z1.real, z1.imag, &(pre[i]));
	  cmult (z1, tmp, &z3);
	  cmplx (z3.real, z3.imag, &z1);
	  NEXT();
	}

/* Double loop to compute all Y entries */
	for (m = 0; m<n; m++)
	{
	  NEXT();
	  cmplx (x[0], 0.0, &(y[m]));
	  for (k=1; k<=n-1; k++)
	  {
/* Exp (tmp*k*m) */
	    p = (k*m % n);

/* *x[k] */
	    cmplx (x[k], 0.0, &z3);
	    cmult (z3, pre[p], &z4);
/* + y[m] */
	    csum (y[m], z4, &z2);
	    y[m].real = z2.real; 
	    y[m].imag = z2.imag;
	  }
	}
}

