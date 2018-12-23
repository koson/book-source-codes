/* Thresholding by using the two histogram peaks */

#define MAX
#include "lib.h"
void peaks (IMAGE im, int *hist, float **lap, int lval, int *t);
void thr_lap (IMAGE im);
void fhist (float **data, int *hist, int nr, int nc);
void hi_pct (int *hist, int NH, long N, float pct, int *val);
void Laplacian (IMAGE input, float **output);
float pix_lap (IMAGE im, int r, int c);

float PCT = 85.0;
static int hist[2048];

void main (int argc, char *argv[])
{
	IMAGE data;
	float percent;

	if (argc < 4)
	{
	  printf ("Usage: thrlap <input file> <output file> <percentage>\n");
	  exit (0);
	}

	data = Input_PBM (argv[1]);
	if (data == NULL)
	{
		printf ("Bad input file '%s'\n", argv[1]);
		exit(1);
	}

	sscanf (argv[3], "%f", &PCT);
	thr_lap (data);

	Output_PBM (data, argv[2]);
}

void thr_lap (IMAGE im)
{
	float ** Lap;
	int i, j, t, v;
	unsigned char *p;

/* Compute the Laplacian of 'im' */
	Lap = f2d (im->info->nr, im->info->nc);
	Laplacian (im, Lap);

/* Find the high 85% of the Laplacian values */
	fhist (Lap, hist, im->info->nr, im->info->nc);
	hi_pct (hist, 2048, (long)im->info->nr*(long)im->info->nc, PCT, &v);

/* Construct histogram of the grey levels of hi Laplacian pixels */
	peaks (im, hist, Lap, v, &t);

	fprintf (stderr, "Threshold is %d\n", t);

/* Threshold */
	for (i=0; i<im->info->nr; i++)
	  for (j=0; j<im->info->nc; j++)
	  if (im->data[i][j] < t)
	    im->data[i][j] = 0;
	  else
	    im->data[i][j] = 255;
}

/*      Return the level marking the high 85% of pixels */
void hi_pct (int *hist, int NH, long N, float pct, int *val)
{
	int i,j=0, m;

	*val = -1;
	m = (pct/100.0) * N;
	for (i=0; i<NH; i++)
	{
	  j += hist[i];
	  if (j>=m)
	  {
	    *val = i;
	    break;
	  }
	}
	if (*val < 0) printf ("BAD HISTOGRAM in 'hi_pct'.\n");
}

/*      Construct a histogram of a float matrix         */
void fhist (float **data, int *hist, int nr, int nc)
{
	int i,j;

	for (i=0; i<2048; i++) hist[i] = 0;
	for (i=0; i<nr; i++)
	  for (j=0; j<nc; j++)
	    hist[(int)(data[i][j])] += 1;
}

void Laplacian (IMAGE input, float **output)
{
	int i,j;

	for (i=1; i<input->info->nr-1; i++)
	  for (j=1; j<input->info->nc-1; j++)
	    output[i][j] = pix_lap (input, i, j);
}

float pix_lap (IMAGE im, int r, int c)
{
	int k=0, i,j;

/*
	k = (int)im->data[r-1][c]+(int)im->data[r+1][c] +
	    (int)im->data[r][c-1]+(int)im->data[r][c-1];
	k = k-(int)im->data[r][c]*4;
*/
	for (i= -1; i<=1; i++)
	  for (j= -1; j<=1; j++)
	    if (i!=0 || j!=0)
		k += im->data[i+r][j+c];
	k = k - 8*(int)im->data[r][c];
	if (k<=0) return 0.0;
	return (float)k/8.0;
}

void peaks (IMAGE im, int *hist, float **lap, int lval, int *t)
{
	int N, i,j,k;

	for (i=0; i<256; i++) hist[i] = 0;
	*t = -1;

/* Find the histogram */
	N = im->info->nc*im->info->nr;

	for (i=0; i<im->info->nr; i++)
	  for (j=0; j<im->info->nc; j++)
	    if (lap[i][j] >= lval)
	      hist[im->data[i][j]] += 1;

/* Find the first peak */
	j = 0;
	for (i=0; i<256; i++)
	  if (hist[i] > hist[j]) j = i;

/* Find the second peak */
	k = 0;
	for (i=0; i<256; i++)
	  if (i>0 && hist[i-1]<=hist[i] && i<255 && hist[i+1]<=hist[i])
	    if ((k-j)*(k-j)*hist[k] < (i-j)*(i-j)*hist[i]) k = i;

	*t = j;
	if (j<k)
	{
	  for (i=j; i<k; i++)
	    if (hist[i] < hist[*t]) *t = i;
	} else {
	  for (i=k; i<j; i++)
	    if (hist[i] < hist[*t]) *t = i;
	}
}

