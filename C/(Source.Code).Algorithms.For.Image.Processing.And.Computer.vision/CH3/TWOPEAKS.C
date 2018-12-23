/* Thresholding by using the two histogram peaks */

#define MAX
#include "lib.h"

void thr_peaks (IMAGE im);

void main (int argc, char *argv[])
{
	IMAGE data;
	float percent;

	if (argc < 3)
	{
	  printf ("Usage: twopeaks <input file> <output file> \n");
	  exit (0);
	}

	data = Input_PBM (argv[1]);
	if (data == NULL)
	{
		printf ("Bad input file '%s'\n", argv[1]);
		exit(1);
	}

	thr_peaks (data);

	Output_PBM (data, argv[2]);
}

void thr_peaks (IMAGE im)
{
	int i,j,k, hist[256], t= -1;

	for (i=0; i<256; i++) hist[i] = 0;

	for (i=0; i<im->info->nr; i++)
	  for (j=0; j<im->info->nc; j++)
	    hist[(im->data[i][j])] += 1;

/* Find the first peak */
	j = 0;
	for (i=0; i<256; i++)
	  if (hist[i] > hist[j]) j = i;

/* Find the second peak */
	k = 0;
	for (i=0; i<256; i++)
	  if (i>0 && hist[i-1]<=hist[i] && i<255 && hist[i+1]<=hist[i])
	    if ((k-j)*(k-j)*hist[k] < (i-j)*(i-j)*hist[i]) k = i;

	t = j;
	if (j<k)
	{
	  for (i=j; i<k; i++)
	    if (hist[i] < hist[t]) t = i;
	} else {
	  for (i=k; i<j; i++)
	    if (hist[i] < hist[t]) t = i;
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

