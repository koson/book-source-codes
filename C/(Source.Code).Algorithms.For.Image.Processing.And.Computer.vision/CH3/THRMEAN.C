/* Thresholding by using the mean       */

#define MAX
#include "lib.h"
void thr_mean (IMAGE im);

void main (int argc, char *argv[])
{
	IMAGE data;
	int i;

	if (argc < 3)
	{
	  printf ("Usage: thrmean <input file> <output file>\n");
	  exit (0);
	}
	
	data = Input_PBM (argv[1]);
	if (data == NULL)
	{
		printf ("Bad input file '%s'\n", argv[1]);
		exit(1);
	}
	thr_mean (data);
	Output_PBM (data, argv[2]);
}

void thr_mean (IMAGE im)
{
	long N, i, j;
	float mean = 0;

/* Find the mean */
	N = (long)im->info->nc*(long)im->info->nr;
	for (i=0; i<im->info->nr; i++)
	  for (j=0; j<im->info->nc; j++)
	  mean += (float)(im->data[i][j]);
	mean /= (float)N;

	fprintf (stderr, "Threshold is %d\n", (int)mean);

/* Threshold */
	for (i=0; i<im->info->nr; i++)
	  for (j=0; j<im->info->nc; j++)
	    if (im->data[i][j] < mean) im->data[i][j] = 0;
	      else im->data[i][j] = 255;
}
