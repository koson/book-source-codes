/* Minimum error thresholding */

#define MAX
#include "lib.h"

void thr_me (IMAGE im);
void histogram (IMAGE im, long *hist);
float s2 (int t);
float s1 (int t);
float u2 (int t);
float u1 (int t);
float P2 (int t);
float P1 (int t);
float J(int t);
float flog (float x);

long h[256];

void main (int argc, char *argv[])
{
	IMAGE data;

	if (argc < 3)
	{
	  printf ("Usage: thrme <input file> <output file>\n");
	  exit (0);
	}

	data = Input_PBM (argv[1]);
	if (data == NULL)
	{
		printf ("Bad input file '%s'\n", argv[1]);
		exit(1);
	}

	thr_me (data);

	Output_PBM (data, argv[2]);
}

void thr_me (IMAGE im)
{
	int i,j,t,tbest;
	float F[256];

/* Histogram */
	histogram (im, h);

/* Compute the factors */
	for (i=1; i<256; i++)
	{
	  F[i] = J(i);
	  if (F[i] < F[tbest]) tbest = i;
	}

	fprintf (stderr, "Threshold is %d\n", tbest);

/* Threshold */
	for (i=0; i<im->info->nr; i++)
	  for (j=0; j<im->info->nc; j++)
	    if (im->data[i][j] < tbest)
	      im->data[i][j] = 0;
	    else
	      im->data[i][j] = 255;
}

float J(int t)
{
	float a, b, c, d, x1;

	a = P1(t); b = s1(t);
	c = P2(t); d = s2(t);

	x1 = 1.0 + 2.0*(a*flog(b) + c*flog(d)) -
		2.0*(a*flog(a) + c*flog(c));
	return x1;
}

void histogram (IMAGE im, long *hist)
{
	int i, j;

	for (i=0; i<256; i++) hist[i] = 0;

	for (i=0; i<im->info->nr; i++)
	  for (j=0; j<im->info->nc; j++)
	    hist[im->data[i][j]] += 1;
}

float flog (float x)
{
	if (x > 0.0) return (float)log((double)x);
	 else return 0.0;
}

float P1 (int t)
{
	long i, sum = 0;

	for (i=0; i<=t; i++)
	  sum += h[i];
	return (float)sum;
}

float P2 (int t)
{
	long i, sum = 0;

	for (i=t+1; i<=255; i++)
	  sum += h[i];
	return (float)sum;
}

float u1 (int t)
{
	long i;
	float sum=0.0, p;

	p = P1 (t);
	if (p <= 0.0) return 0.0;

	for (i=0; i<=t; i++)
	  sum += h[i]*i;
	return sum/p;
}

float u2 (int t)
{
	long i;
	float sum=0.0, p;
 
	p = P2 (t);
	if (p <= 0.0) return 0.0;
 
	for (i=t+1; i<=255; i++)
	  sum += h[i]*i;
	return sum/p;
}

float s1 (int t)
{
	int i;
	float sum=0.0, p, u, x;

	p = P1(t);
	if (p<=0.0) return 0.0;
	u = u1(t);
	for (i=0; i<=t; i++)
	{
	  x = (i-u)*(i-u);
	  sum += x*h[i];
	}
	return sum/p;
}

float s2 (int t)
{
	int i;
	float sum=0.0, p, u, x;
 
	p = P2(t);
	if (p<=0.0) return 0.0;
	u = u2(t);
	for (i=t+1; i<=255; i++)
	{
	  x = (i-u)*(i-u);
	  sum += x*h[i];
	}
	return sum/p;
}

