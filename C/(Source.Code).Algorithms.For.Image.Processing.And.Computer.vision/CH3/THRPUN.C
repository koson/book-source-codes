/* Pun method for using entropy */

#define MAX
#include "lib.h"

void thr_pun (IMAGE im);
void histogram (IMAGE im, float *hist);
float flog (float x);
float entropy (float *h, int a);
float maxfromt (float *h, int i);
float maxtot (float *h, int i);

void main (int argc, char *argv[])
{
	IMAGE data;

	if (argc < 3)
	{
	  printf ("Usage: thrpun <input file> <output file>\n");
	  exit (0);
	}

	data = Input_PBM (argv[1]);
	if (data == NULL)
	{
		printf ("Bad input file '%s'\n", argv[1]);
		exit(1);
	}

	thr_pun (data);

	Output_PBM (data, argv[2]);
}

void thr_pun (IMAGE im)
{
	int i, j, t;
	float *Ht, HT, *Pt, x, *F, y, z;
	unsigned char *p;
	float *hist, to, from;

/* Histogram */
	Ht = (float *)malloc (sizeof(float)*256);
	Pt = (float *)malloc (sizeof(float)*256);        
	F  = (float *)malloc (sizeof(float)*256);        
	hist = (float *)malloc (sizeof(float)*256);        
	histogram (im, hist);

/* Compute the factors */
	HT = Ht[0] = entropy (hist, 0);
	Pt[0] = hist[0];
	for (i=1; i<256; i++)
	{
	  Pt[i] = Pt[i-1] + hist[i];
	  x = entropy(hist, i);
	  Ht[i] = Ht[i-1] + x;
	  HT += x;
	}

/* Calculate the function to be maximized at all levels */
	t = 0;
	for (i=0; i<256; i++)
	{
	  to = (maxtot(hist,i));
	  from = maxfromt(hist, i);
	  if (to > 0.0 && from > 0.0)
	  {
	    x = (Ht[i]/HT)* flog(Pt[i])/flog(to);
	    y = 1.0 - (Ht[i]/HT);
	    z = flog(1 - Pt[i])/flog(from);
	  }
	  else x = y = z = 0.0;
	  F[i] = x + y*z;
	  if (i>0 && F[i] > F[t]) t = i;
	}
	fprintf (stderr, "Threshold is %d\n", t);
	free(Ht); free(Pt); free(F);  free(hist); 

/* Threshold */
	for (i=0; i<im->info->nr; i++)
	  for (j=0; j<im->info->nc; j++)
	    if (im->data[i][j] < t)
	      im->data[i][j] = 0;
	    else
	      im->data[i][j] = 255;
}

void histogram (IMAGE im, float *hist)
{
	int i, j;
	long N;

	for (i=0; i<256; i++) hist[i] = 0.0;
	N = (long)im->info->nc * (long)im->info->nr;

	for (i=0; i<im->info->nr; i++)
	  for (j=0; j<im->info->nc; j++)
	    hist[(im->data[i][j])] += 1.0;

	for (i=0; i<256; i++)
	  hist[i] /= (float)N;
}

float entropy (float *h, int a)
{
	if (h[a] > 0.0)
	  return -(h[a] * (float)log((double)h[a]));
	return 0.0;
}

float flog (float x)
{
	if (x <= 0.0) return 0.0;
	return (float)log((double)x);
}

float maxtot (float *h, int i)
{
	float x;
	int j;

	x = h[0];
	for (j=1; j<=i; j++)
	  if (x < h[j]) x = h[j];
	return x;
}

float maxfromt (float *h, int i)
{
	int j;
	float x;

	x = h[i+1];
	for (j=i+2; j<=255; j++)
	  if (x < h[j]) x = h[j];
	return x;
}
