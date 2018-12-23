/* Thresholding by using the black percentage   */

#define MAX
#include "lib.h"
void thr_percent (IMAGE im, float pct);

void main (int argc, char *argv[])
{
	IMAGE data;
	float percent;

	if (argc < 4)
	{
	  printf ("Usage: thrpct <input file> <output file> <percentage>\n");
	  exit (0);
	}

	data = Input_PBM (argv[1]);
	if (data == NULL)
	{
		printf ("Bad input file '%s'\n", argv[1]);
		exit(1);
	}

	sscanf (argv[3], "%f",  &percent);
	thr_percent (data, percent);
	Output_PBM (data, argv[2]);
}

void thr_percent (IMAGE im, float pct)
{
	int i, j, hist[256], count= 0, t= -1;
	long N, M;

	for (i=0; i<256; i++) hist[i] = 0;

/* Find the histogram */
	N = (long)im->info->nc*(long)im->info->nr;
	M = (long)((pct/100.0)*N);

	for (i=0; i<im->info->nr; i++)
	  for (j=0; j<im->info->nc; j++)
	    hist[im->data[i][j]] += 1;

/* Find the correct bin */
	for (i=0; i<256; i++)
	{
	  count += hist[i];
	  if (count >= M) 
	  {
	    t = i;
	    break;
	  }
	}

	if (t < 0)
	{
	  fprintf (stderr, "THRESHOLD (pct): No threshold.\n");
	  exit (1);
	}
	fprintf (stderr, "Threshold is %d\n", t);

/* Threshold */
	for (i=0; i<im->info->nr; i++)
	  for (j=0; j<im->info->nc; j++)
	    if (im->data[i][j] < t)
	     im->data[i][j] = 0;
	    else
	     im->data[i][j] = 255;
}
