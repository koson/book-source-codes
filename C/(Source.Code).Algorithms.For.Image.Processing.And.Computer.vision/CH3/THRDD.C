/* Digital Desk - adaptive thresholding */

#define MAX
#include "lib.h"
void thrdd (IMAGE im);

float pct = 15.0;       /* Make smaller to darken the image */
float Navg = 8.0;       /* Fraction of a row in the average (ie 1/8) */

void main (int argc, char *argv[])
{
	IMAGE data;

	if (argc < 3)
	{
	  printf ("Usage: thrdd <input file> <output file>\n");
	  exit (0);
	}

	data = Input_PBM (argv[1]);
	if (data == NULL)
	{
		printf ("Bad input file '%s'\n", argv[1]);
		exit(1);
	}

	thrdd (data);
	Output_PBM (data, argv[2]);
}

void thrdd (IMAGE im)
{
	int NC, row, col, inc;
	float mean, s, sum;
	unsigned char *p;
	long N, i;

	N = (long)im->info->nc * (long)im->info->nr;
	NC = im->info->nc;
	s = (int)(float)(NC/Navg);
	sum = 127*s;

	row = col = 0;
	p = &(im->data[0][0]);
	inc = 1;

	for (i=0; i<N-1; i++)
	{
	  if (col >= NC)
	  {
	    col = NC-1; row++;
	    p = &(im->data[row][col]);
	    inc = -1;
	  } else if (col < 0)
	  {
	    col = 0;
	    row++;
	    p = &(im->data[row][col]);
	    inc = 1;
	  }

/* Estimate the mean of the last NC/8 pixels. */
	  sum = sum - sum/s + *p;
	  mean = sum/s;
	  if (*p < mean*(100-pct)/100.0) *p = 0;
		else *p = 255;
	  p += inc;
	  col += inc;
	}
}
