/* Two dimensional wavelet transform (Haar) */
/* Threshold the image before reconstruction */

#define MAX
#include "lib.h"

#define FORWARD 1
#define INVERSE -1

void fwt_1d (float *data,  int N, int direction);
void wavelet (float *data, int N, int direction);
void Haar_2 (float *data,  int N, int direction);
void fwt_2d (IMAGE im, float **result);
void fwti_2d (float **result, IMAGE im);
float ** f2d (int i, int j);

void main(int argc, char *argv[])
{
	IMAGE im, tmp;
	float **result, xmax, xmin, xd;
	int t, i, j, k;

	if (argc<3)
	{
	  printf ("Thresholded wavelet transform - <input> <output>\n");
	  exit (1);
	}

	printf ("Computing a 2d wavelet transform on a Haar basis:\n");
	im = Input_PBM (argv[1]);
	result = f2d(im->info->nr, im->info->nc);

	fwt_2d (im, result);

	printf ("Reconstructing the data from the wavelets:\n");
	printf ("Enter threshold for reconstruction:\n");
	scanf ("%d", &t);

/* Remove coefficients less than T */
	k = 0;
	for (i=0; i<im->info->nr; i++)
	  for (j=0; j<im->info->nc; j++)
	   if (result[i][j] < t)
	   {
	     result[i][j] = 0.0;
	     k++;
	   }
	printf ("Deleted %d coefficients.\n", k);

/* back-transform */
	fwti_2d (result, im);
	Output_PBM (im, argv[2]);
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
	  fwt_1d (result[i], im->info->nc, FORWARD);

/* Transform the columns */
	tmp = (float *)malloc (sizeof(float)*im->info->nr);
	for (j=0; j<im->info->nc; j++)
	{
	  for (i=0; i<im->info->nr; i++)
	    tmp[i] = result[i][j];
	  fwt_1d (tmp, im->info->nr, FORWARD);
	  for (i=0; i<im->info->nr; i++) 
	    result[i][j] = tmp[i];
	}
	free(tmp);
}

void fwti_2d (float **result, IMAGE im)
{
	int i,j,k;
	float *tmp;

/* Transform the rows */
	for (i=0; i<im->info->nr; i++)
	  fwt_1d (result[i], im->info->nc, INVERSE);

/* Transform the columns */
        tmp = (float *)malloc (sizeof(float)*im->info->nr);
	for (j=0; j<im->info->nc; j++)
	{
	  for (i=0; i<im->info->nr; i++)
	    tmp[i] = result[i][j];
	  fwt_1d (tmp, im->info->nr, INVERSE);
	  for (i=0; i<im->info->nr; i++) 
	    result[i][j] = tmp[i];
	}
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

/*	h0 = 1.0/sqrt(2.0); h1 = 1.0/sqrt(2.0);	*/
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

