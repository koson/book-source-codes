/* Thresholding by using the iterative selection */

#define MAX
#include "lib.h"

void thr_is (IMAGE im);
void thr_is_fast (IMAGE im);

void main (int argc, char *argv[])
{
	IMAGE data;

	if (argc < 3)
	{
	  printf ("Usage: thris <input file> <output file>\n");
	  exit (0);
	}

	data = Input_PBM (argv[1]);
	if (data == NULL)
	{
		printf ("Bad input file '%s'\n", argv[1]);
		exit(1);
	}

	thr_is_fast (data);

	Output_PBM (data, argv[2]);
}

void thr_is (IMAGE image)
{
	float tt, tb, to, t2;
	int   i, j, t;
	long  N, no, nb;

	N = (long)image->info->nc * (long)image->info->nr;
	tb = 0.0;       to = 0.0;       no = 0;
	for (i=0; i<image->info->nr; i++) 
	  for (j=0; j<image->info->nc; j++)
	    to = to+ (image->data[i][j]);
	tt = (to/(float)N);

	while (N) 
	{
	  no = 0; nb = 0; tb=0.0; to = 0.0;
	  for (i=0; i<image->info->nr; i++) 
	    for (j=0; j<image->info->nc; j++)
	    {
	      if ( (float)(image->data[i][j]) >= tt ) 
	      {
		to = to + (float)(image->data[i][j]);
		no++;
	      } else {
		tb = tb + (float)(image->data[i][j]);
		nb++;
	      }
	    }

	  if (no == 0) no = 1;
	  if (nb == 0) nb = 1;
	  t2 = (tb/(float)nb + to/(float)no )/2.0;
	  if (t2 == tt) N=0;
	  tt = t2;
	}
	t = (int) tt;
	printf("Threshold found is %d\n", t);

/* Threshold */
	for (i=0; i<image->info->nr; i++)
	  for (j=0; j<image->info->nc; j++)
	   if (image->data[i][j] < t)
	     image->data[i][j] = 0;
	   else
	     image->data[i][j] = 255;
}

void thr_is_fast (IMAGE im)
{
	long i, j, told, tt, a, b, c, d;
	long N, *hist;

	hist = (long *) malloc(sizeof(long)*257);
	for (i=0; i<256; i++) hist[i] = 0;

/* Compute the mean and the histogram */
	N = (long)im->info->nc * (long)im->info->nr;
	tt = 0;
	for (i=0; i<im->info->nr; i++)
	  for (j=0; j<im->info->nc; j++)
	  {
	    hist[im->data[i][j]] += 1;
	    tt = tt + (im->data[i][j]);
	  }
	tt = (tt/(float)N);

	do
	{
	  told = tt;
	  a = 0; b = 0;
	  for (i=0; i<=told; i++)
	  {
	    a += i*hist[i];
	    b += hist[i];
	  }
	  b += b;

	  c = 0; d = 0;
	  for (i=told+1; i<256; i++)
	  {
	    c += i*hist[i];
	    d += hist[i];
	  }
	  d += d;

	  if (b==0) b = 1;
	  if (d==0) d = 1;
	  tt = a/b + c/d;
	} while (tt != told);
	printf ("Fast threshold is %d\n", tt);
	free (hist);

/* Threshold */
	 for (i=0; i<im->info->nr; i++)
	   for (j=0; j<im->info->nc; j++)
	     if (im->data[i][j] < tt)
	       im->data[i][j] = 0;
	     else
	       im->data[i][j] = 255;
}
