/* Kapur method for using entropy */

#define MAX
#include "lib.h"

void thr_kapur (IMAGE im);
float entropy (float *h, int a, float p);
void histogram (IMAGE im, float *hist);

void main (int argc, char *argv[])
{
	IMAGE data;

	if (argc < 3)
	{
	  printf ("Usage: thrkapur <input file> <output file>\n");
	  exit (0);
	}

	data = Input_PBM (argv[1]);
	if (data == NULL)
	{
		printf ("Bad input file '%s'\n", argv[1]);
		exit(1);
	}

	thr_kapur (data);

	Output_PBM (data, argv[2]);
}

void thr_kapur (IMAGE im)
{
	int i, j, t;
	float Hb, Hw, *Pt, *hist, *F;

/* Histogram */
	Pt = (float *)malloc(sizeof(float)*256);
	hist = (float *)malloc(sizeof(float)*256);  
	F = (float *)malloc(sizeof(float)*256);  
	
	histogram (im, hist);

/* Compute the factors */
	Pt[0] = hist[0];
	for (i=1; i<256; i++)
	  Pt[i] = Pt[i-1] + hist[i];

/* Calculate the function to be maximized at all levels */
	t = 0;
	for (i=0; i<256; i++)
	{
	  Hb = Hw = 0.0;
	  for (j=0; j<256; j++)
	    if (j<=i)
	      Hb += entropy (hist, j, Pt[i]);
	    else 
	      Hw += entropy (hist, j, 1.0-Pt[i]);

	  F[i] = Hb+Hw;
	  if (i>0 && F[i] > F[t]) t = i;
	}
	fprintf (stderr, "Threshold is %d\n", t);
	free(hist); free(Pt); free(F);

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
	    hist[im->data[i][j]] += 1.0;

	for (i=0; i<256; i++)
	  hist[i] /= (float)N;
}

float entropy (float *h, int a, float p)
{
	if (h[a] > 0.0 && p>0.0)
	  return -(h[a]/p * (float)log((double)(h[a])/p));
	return 0.0;
}

