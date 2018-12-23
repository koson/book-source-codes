/* Test of Fourier transform */
#include <math.h>
#include <stdio.h>
#include <malloc.h>

#define twopi 3.1415926535 * 2.0
struct cpx {
	float real;
	float imag;
};
typedef struct cpx COMPLEX;

void slowft (float *x, COMPLEX *y, int n);
void cexp (COMPLEX z1, COMPLEX *res);
void csum (COMPLEX z1, COMPLEX z2, COMPLEX *res);
void cmult (COMPLEX z1, COMPLEX z2, COMPLEX *res);
void cmplx (float rp, float ip, COMPLEX *z);
float cnorm (COMPLEX z);
void NEXT();

static float data[2050], data2[2050];
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

	n = 1024;               /* Size of sampled signal */
	t = 1.0/128.0;          /* Period of signal */
	t2 = 1.0/300.0;
 
	for (i=0; i<n; i++)
	{
	  data2[i] = 
	   data[i] = 2.0* sin(twopi * t * i)
		 /* + 3*sin (twopi * t2 * i) */ ;
	  data2[i+n] = 
	   data[i+n] = 0.0;
	}
 
	printf ("Raw sine-wave data\" \n");
	for (i=0; i<n; i++)
	  printf ("%d %f\n", i, data[i]);

	printf ("Slow Fourier Transform:\n");
	slowft (data, Data, 1024);

	printf ("Slow Fourier Transform data\" \n");
	for (i=0; i<n; i++)
	  printf ("%d %f\n", i, cnorm (Data[i]));
	return 0;

/* Further test ... */

	data[0] = -6.0; data[1] = -2.0; data[2] = 0.0; 
	data[3] = 2.0;  data[4] = 4.0;  data[5] = 6.0;
	data[6] = 3.0;  data[7] = -1.0;
	slowft (data, Data, 8);
	printf ("  M      Real		imag\n");
	for (i=0; i<=4; i++)
	  printf (" %2d    %8.4f	  %8.4f\n", i, Data[i].real,
		Data[i].imag);
	return 0;
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
	COMPLEX tmp, z1, z2, z3, z4;
	int m, k;

/* Constant factor -2 pi */
	cmplx (0.0, (float)(atan (1.0)/n * -8.0), &tmp);

	printf (" ");
	for (m = 0; m<=n; m++)
	{
	  NEXT();
	  cmplx (x[0], 0.0, &(y[m]));
	  for (k=1; k<=n-1; k++)
	  {
/* Exp (tmp*k*m) */
	    cmplx ((float)k, 0.0, &z2);
	    cmult (tmp, z2, &z3);
	    cmplx ((float)m, 0.0, &z2);
	    cmult (z2, z3, &z4);
	    cexp (z4, &z2);
/* *x[k] */
	    cmplx (x[k], 0.0, &z3);
	    cmult (z2, z3, &z4);
/* + y[m] */
	    csum (y[m], z4, &z2);
	    y[m].real = z2.real; y[m].imag = z2.imag;
	  }
	}
}

