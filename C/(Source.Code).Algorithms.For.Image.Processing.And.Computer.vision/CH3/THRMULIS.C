/* Thresholding by using the iterative selection over regions */

#define MAX
#include "lib.h"

void thr_is (IMAGE im);
int local (IMAGE im, int rmin, int rmax, int cmin, int cmax);

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

	thr_is (data);

	Output_PBM (data, argv[2]);
}

void thr_is (IMAGE im)
{
	IMAGE thr;
	int i, j;

	thr = newimage (im->info->nr, im->info->nc);
	for (i=0; i<im->info->nr; i++) 
	  for (j=0; j<im->info->nc; j++)
	    thr->data[i][j] = 0;

	for (i=10; i<im->info->nr-10; i++)
	  for (j=10; j<im->info->nc-10; j++)
	    thr->data[i][j] = local (im, i-10, i+10, j-10, j+10);
	Output_PBM (thr, "thrim.pgm");

/* Threshold */
	for (i=0; i<im->info->nr; i++)
	  for (j=0; j<im->info->nc; j++)
	    if (im->data[i][j] < thr->data[i][j])
	      im->data[i][j] = 0;
	    else
	      im->data[i][j] = 255;
	freeimage (thr);
}

int local (IMAGE im, int rmin, int rmax, int cmin, int cmax)
{
	long hist[256];
	int i, j, n=0, told, tt=0;
	long a, b, c, d;

	for (i=0; i<256; i++) hist[i] = 0;

/* Compute the mean and the histogram */
	for (i=rmin; i<rmax; i++)
	  for (j=cmin; j<cmax; j++)
	  {
	    hist[im->data[i][j]] += 1;
	    tt = tt + (im->data[i][j]);
	    n++;
	  }
	tt = (tt/(float)n);

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
	  if (b == 0)
	  {
	    tt++; continue;
	  }

	  c = 0; d = 0;
	  for (i=told+1; i<256; i++)
	  {
	    c += i*hist[i];
	    d += hist[i];
	  }
	  d += d;
	  if (d==0)
	  {
	    tt--; continue;
	  }

	  tt = a/b + c/d;
	} while (tt != told);
	return tt;
}
