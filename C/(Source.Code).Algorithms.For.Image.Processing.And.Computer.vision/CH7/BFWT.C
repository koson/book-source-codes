/* 1D Wavelet transform */

/* ****************************************************************
   WARNING - This transform takes a VERY long time, and is awkward
   to use. It is included for expository purposes only 
  *****************************************************************/

#include <math.h>
#include <stdio.h>
#include <malloc.h>

float g(float x);
float mother (float a, float b, float t);
void dwt_1d (float *data, float **wt, int n);
void dwti_1d (float *data, float **wt, int n);
void NEXT ();

float A0, B0;

void NEXT ()
{
	static int i = 0;
	static char xc[5] = {'|', '/', '-', '\\', ' '};

	fprintf (stdout, "%c%c", 010, xc[i++]);
	fflush (stdout);
	if (i>3) i = 0;
}

/* Brute force 1D forward wavelet transform */
void dwt_1d (float *data, float **wt, int n)
{
	int i,j,k;
	float scale, x, trans, sum;
	float a0, b0, sqa;

	printf ("Forward: ");
	wt[0][0] = 0.0;
	a0 = A0;
	b0 = B0;
	scale = a0;
	for (i=0; i<n; i++)
	{
	  sqa = (float)sqrt((double)scale);
	  trans = scale*.99;
	  for (j=0; j<n; j++)
	  {
	    sum = 0.0;
	    for (k=0; k<n; k++)
	      sum += data[k]*mother(scale, trans, (float)k);
	    wt[i][j] = sum/sqa;
/*            printf ("%d %d %f\n", i,j,wt[i][j]);    */
	    trans *= 1.03;
	    NEXT();
	  }
	  scale *= 1.01;
	}
	printf ("\n");
/* scale 1.01 trans 1.03, call with 8.0 0.0 to get impulse figure */
}

/* Brute force 1D inverse wavelet transform */
void dwti_1d (float *data, float **wt, int n)
{
	int i,j,k;
	float  sqa, sum, scale, trans;
	float a0, b0;

	printf ("Inverse: ");
	a0 = A0;
	b0 = B0;
	for (k=0; k<n; k++)
	{
	  sum = 0.0;
	  scale = a0;
	  for (i=0; i<n; i++)
	  {
	    sqa = (float)sqrt((double)scale);
	    trans = scale*0.99;
	    for (j=0; j<n; j++)
	    {
	      sum += mother(scale, trans, (float)k) * wt[i][j]/sqa;
	      trans *= 1.03;
	    }
	    scale *= 1.01;
	    NEXT();
	  }
	  data[k] = sum;
	}
	printf ("\n");
}

void main(int argc, char *argv[])
{
	int i,j,k;
	float x, y, z, a, b;
	FILE *f;
	float *data, **wt, *rev;

	data = (float *)malloc (sizeof(float)*1024);
	rev  = (float *)malloc (sizeof(float)*1024);
	wt = (float **)malloc (sizeof(float *)*256);
	for (i=0; i<256; i++)
	  wt[i] = (float *)malloc(sizeof(float)*256);

	if (argc < 3)
	{
		printf ("bfwt a0 b0\n");
		exit (1);
	}
	sscanf (argv[1], "%f", &A0);
	sscanf (argv[2], "%f", &B0);

	for (i=0; i<256; i++) data[i] = 0.0;
	data[64] = 1.0;
	dwt_1d (data, wt, 128);
	dwti_1d (rev, wt, 128);
	for (i=0; i<128; i++)
	  printf ("%d %f (%f)\n", i, rev[i], fabs(rev[i]-data[i]));
}

/* Mother - A Gaussian damped sine function */
float g(float x)
{
	return (float)(sin((double)(4*x))*exp((double)(-(x*x))));
}

float mother (float a, float b, float t)
{
	float y, z;

	z = (float)sqrt((double)a);
	if (z != 0.0)
	  z = 1.0/z;
	else z = 0.0;

	return z*g((t-b)/a);
}

