/* Johannsen method for using entropy */

#define MAX
#include "lib.h"

void thr_joh (IMAGE im);
float entropy (float h);
void histogram (IMAGE im, float *hist);
float flog (float x);

void main (int argc, char *argv[])
{
	IMAGE data;

	if (argc < 3)
	{
	  printf ("Usage: thrjoh <input file> <output file>\n");
	  exit (0);
	}

	data = Input_PBM (argv[1]);
	if (data == NULL)
	{
		printf ("Bad input file '%s'\n", argv[1]);
		exit(1);
	}

	thr_joh (data);

	Output_PBM (data, argv[2]);
}

void thr_joh (IMAGE im)
{
	int i, j, t, start, end;
	float Sb, Sw, *Pt, *hist, *F, *Pq;

/* Histogram */
	Pt = (float *)malloc(sizeof(float)*256);
	hist = (float *)malloc(sizeof(float)*256);
	F = (float *)malloc(sizeof(float)*256);
	Pq = (float *)malloc(sizeof(float)*256);

	histogram (im, hist);

/* Compute the factors */
	Pt[0] = hist[0];
	Pq[0] = 1.0 - Pt[0];
	for (i=1; i<256; i++)
	{
	  Pt[i] = Pt[i-1] + hist[i];
	  Pq[i] = 1.0 - Pt[i-1];
	}

	start = 0;
	while (hist[start++] <= 0.0) ;
	end = 255;
	while (hist[end--] <= 0.0) ;

/* Calculate the function to be minimized at all levels */
	t = -1;
	for (i=start; i<=end; i++)
	{
	  if (hist[i] <= 0.0) continue;
	  Sb = flog(Pt[i]) + (1.0/Pt[i])*
		(entropy(hist[i])+entropy(Pt[i-1]));
	  Sw = flog (Pq[i]) + (1.0/Pq[i])*
		(entropy(hist[i]) + entropy(Pq[i+1]));
	  F[i] = Sb+Sw;
	  if (t<0) t = i;
	  else if (F[i] < F[t]) t = i;
	}
	fprintf (stderr, "Threshold is %d\n", t);
	free(hist); free (Pt); free (F); free (Pq);

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
	int  i, j;
	long N;

	for (i=0; i<256; i++) hist[i] = 0.0;
	N = (long)im->info->nc * (long)im->info->nr;

	for (i=0; i<im->info->nr; i++)
	  for (j=0; j<im->info->nc; j++)
	    hist[(im->data[i][j])] += 1.0;

	for (i=0; i<256; i++)
	  hist[i] /= (float)N;
}

float entropy (float h)
{
	if (h > 0.0)
	  return (-h * (float)log((double)(h)));
	else return 0.0;
}

float flog (float x)
{
	if (x > 0.0) return (float)log((double)x);
	 else return 0.0;
}

