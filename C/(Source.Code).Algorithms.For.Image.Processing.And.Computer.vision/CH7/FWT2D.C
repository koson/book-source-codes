/* Two dimensional wavelet transform */

#define MAX
#include "lib.h"
#include <math.h>
#include <malloc.h>
#include <stdio.h>

#define FORWARD 1
#define INVERSE -1

float rec[16] = {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};

void fwt_1d (float *data,  int N, int direction);
void wavelet (float *data, int N, int direction);
void Haar_2 (float *data,  int N, int direction);
void fwt_2d (IMAGE im, float **result);
void fwti_2d (float **result, IMAGE im);
float ** f2d (int i, int j);
void NEXT();

void NEXT ()
{
	static int i = 0;
	static char xc[5] = {'|', '/', '-', '\\', ' '};

	fprintf (stdout, "%c%c", 010, xc[i++]);
	fflush (stdout);
	if (i>3) i = 0;
}

int main(int argc, char *argv[])
{
	IMAGE im, tmp;
	float **result, xmax, xmin, xd;
	int maxr, i, j, k;
	FILE *f;

	if (argc<3)
	{
	  printf ("2d wavelet transform - <input> <output>\n");
	  printf ("File of raw wavelet coefficients saved in 'wtout'\n");
	  exit (1);
	}

	printf ("Computing a 2d wavelet transform on a Haar basis:\n");
	im = Input_PBM (argv[1]);
	if (im == 0)
	{
		printf ("Can't open file '%s'.\n", argv[1]);
		return 0;
	}

	result = f2d(im->info->nr, im->info->nc);
	printf ("Forward: ");

	fwt_2d (im, result);

	tmp = newimage (im->info->nr, im->info->nc);
	xmax = -1.0e12; xmin = -xmax;
	for (i=0; i<im->info->nr; i++)
	  for (j=0; j<im->info->nc; j++)
	  {
	    if (xmax < result[i][j]) xmax = result[i][j];
	    if (xmin > result[i][j]) xmin = result[i][j];
	  }
	xd = xmax-xmin;
	for (i=0; i<im->info->nr; i++)
	  for (j=0; j<im->info->nc; j++)
	    tmp->data[i][j] = (unsigned char) ((result[i][j]-xmin)/xd * 255);
	Output_PBM (tmp, "wtout");
	printf ("\nWavelet transform saved in file 'wtout'.\n");
	freeimage (tmp);

	printf ("Reconstructing the data from the wavelets:\n");
	printf ("Enter max row/column for reconstruction:\n");
	scanf ("%d", &maxr);

/* Remove all coefficients except those in rows/cols 1-maxr */
	if (maxr > 0)
	for (i=0; i<im->info->nr; i++)
	  for (j=0; j<im->info->nc; j++)
	    if (i>maxr || j > maxr) result[i][j] = 0.0;

/* back-transform */
	printf ("Inverse transform: ");
	fwti_2d (result, im);
	Output_PBM (im, argv[2]);
	printf ("Restored image saved in '%s'.\n", argv[2]);
	return 0;
}

void fwt_1d (float *data, int N, int direction)
{
	int thisn;

	if (N > 2)
	{
	  if (direction >= 0)
	  {
	    thisn = N;
	    while (thisn >= 2)
	    {
	      wavelet (data, thisn, direction);
	      thisn /= 2;
	    }
	  } else if (direction == INVERSE) 
	  {
	    thisn = 2;
	    while (thisn <= N)
	    {
	      wavelet (data, thisn, direction);
	      thisn *= 2;
	    }
	  }
	}
}

/* Image is preferably square; size of each dimension is a power of two */
void fwt_2d (IMAGE im, float **result)
{
	int i,j,k;
	float *tmp;

/* Copy to the floating point array */
	for (i=0; i<im->info->nr; i++)
	  for (j=0; j<im->info->nc; j++)
	    result[i][j] = (float)(im->data[i][j]);

/* Transform the rows */
	for (i=0; i<im->info->nr; i++)
	{
	  fwt_1d (result[i], im->info->nc, FORWARD);
	  NEXT();
	}

/* Transform the columns */
	tmp = (float *)malloc (sizeof(float)*im->info->nr);
	for (j=0; j<im->info->nc; j++)
	{
	  for (i=0; i<im->info->nr; i++)
	    tmp[i] = result[i][j];
	  fwt_1d (tmp, im->info->nr, FORWARD);
	  for (i=0; i<im->info->nr; i++) 
	    result[i][j] = tmp[i];
	  NEXT();
	}
	free(tmp);
}

void fwti_2d (float **result, IMAGE im)
{
	int i,j,k;
	float *tmp;

/* Transform the rows */
	printf ("\nRows: ");
	for (i=0; i<im->info->nr; i++)
	{  
	  fwt_1d (result[i], im->info->nc, INVERSE);
	  NEXT(); 
	}

	printf ("\n Columns: ");
/* Transform the columns */
	tmp = (float *)malloc (sizeof(float)*im->info->nr);
	for (j=0; j<im->info->nc; j++)
	{
	  for (i=0; i<im->info->nr; i++)
	    tmp[i] = result[i][j];
	  fwt_1d (tmp, im->info->nr, INVERSE);
	  for (i=0; i<im->info->nr; i++) 
	    result[i][j] = tmp[i];
	  NEXT();
	}
	printf ("\n");
	free (tmp);

/* Copy into the image */
	for (i=0; i<im->info->nr; i++)
	  for (j=0; j<im->info->nc; j++)
	    if (result[i][j] > 255.0) im->data[i][j] = 255;
	    else if (result[i][j] < 0) im->data[i][j] = 0;
	    else im->data[i][j] = (unsigned char)result[i][j];
}

void wavelet (float *data, int N, int direction)
{
	Haar_2 (data, N, direction);
}

void Haar_2 (float *data, int N, int direction)
{
	int i,j,nover2;
	float h0, h1, *tmp;

/*      h0 = 1.0/sqrt(2.0); h1 = 1.0/sqrt(2.0); */
	h0 = h1 = 0.5;
	tmp = (float *)malloc (sizeof(float)*N);

	nover2 = N/2;
	if (direction == FORWARD) 
	{
	  i=0;
	  for (j=0; j<N-1; j+=2)
	  {
	    tmp[i]        = h0*data[j] + h1*data[j+1];
	    tmp[i+nover2] = h0*data[j] - h1*data[j+1];
	    i++;
	  }
	} else if (direction == INVERSE) 
	{

/* Comment the next line out for symmetric scaling */
	  h1 = h0 = 1.0;
	  i = j = 0;
	  do 
	  {
	    tmp[j]   = h0*data[i] + h1*data[i+nover2];
	    tmp[j+1] = h0*data[i] - h1*data[i+nover2];
	    j += 2; i++;
	  } while (i <= nover2);
	}

	for (i=0; i<N; i++)
	  data[i] = tmp[i];
	free(tmp);
}

