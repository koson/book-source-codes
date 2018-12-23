/* Third iteration : Steps to a Fast Fourier Transform */
/* Even and Odd FT components are computed separately here */

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
void evenodd (float *x, COMPLEX *y, int n);
void NEXT();

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

main()
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
 
	printf ("Fourier Transform of sine wave: vers. 3\n");
	evenodd (data, Data, 1024);

	f = fopen ("slow3.out", "w");
	if (f == NULL)
	{
		printf ("Can't open the output file 'slow3.out'\n");
		exit (1);
	}

	for (i=0; i<n; i++)
	  fprintf (f, "%d %f\n", i, cnorm (Data[i]));
	return (0);

	data[0] = -6.0; data[1] = -2.0; data[2] = 0.0; 
	data[3] = 2.0;  data[4] = 4.0;  data[5] = 6.0;
	data[6] = 3.0;  data[7] = -1.0;
	evenodd (data, Data, 8);
	printf ("  M      Real		imag\n");
	for (i=0; i<=4; i++)
	  printf (" %2d    %8.4f	  %8.4f\n", i, Data[i].real,
		Data[i].imag);

	fclose (f);
	return (0);

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
	COMPLEX tmp, z1, z2, z3, z4, pre[1024];
	int m, k, i, p;

/* Constant factor -2 pi */
	cmplx (0.0, (float)(atan (1.0)/n * -8.0), &z1);
	cexp (z1, &tmp);

/* Pre-compute most of the exponential */
	cmplx (1.0, 0.0, &z1);          /* Z1 = 1.0; */
	for (i=0; i<n; i++)
	{
	  NEXT ();
	  cmplx (z1.real, z1.imag, &(pre[i]));
	  cmult (z1, tmp, &z3);
	  cmplx (z3.real, z3.imag, &z1);
	}

/* Double loop to compute all Y entries */
	for (m = 0; m<=n; m++)
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
	    y[m].real = z2.real; y[m].imag = z2.imag;
	  }
	}
}

void evenodd (float *x, COMPLEX *y, int n)
{
	int m, i;
	COMPLEX Sum[1024], Diff[1024], z1, z2;
	COMPLEX Even[512], Odd[512], tmp;
	float xs, ys;

	m = n/2;
	cmplx (0.0, atan (1.0)/n * -8.0, &z1);
	cexp (z1, &tmp);
	cmplx (1.0, 0.0, &z1);

	for (i=0; i<m; i++)
	{
	  xs = (x[i] + x[i+m])/2.0;
	  ys = (x[i] - x[i+m])/2.0;
	  cmplx (xs, 0.0, &(Sum[i]));
	  cmplx (ys, 0.0, &z2);
	  cmult (z1, z2, &(Diff[i]));
	  cmult (z1, tmp, &z2);
	  cmplx (z2.real, z2.imag, &z1);
	}

	slowft (x, Even, m);
	slowft (x, Odd,  m);
	
	for (i=0; i<m; i++)
	{
	  y[i<<1] = Even[i];
	  y[i<<1 + 1] = Odd[i];
	}
}
