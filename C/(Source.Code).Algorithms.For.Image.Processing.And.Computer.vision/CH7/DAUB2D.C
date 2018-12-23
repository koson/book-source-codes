/* 2D wavelet transform, using Daubechies basis (4 coefficients) */
/* Allows filtering by asking how many of the coefficients to
   use in the reconstruction (like Haar_2d.c)                   */

#include <malloc.h>
#include <math.h>
#define MAX
#include "lib.h"

#define FORWARD 1
#define INVERSE -1

float h0,h1,h2,h3;

void fwt_1d (float *data,  int N, int direction);
void wavelet (float *data, int N, int direction);
void Daub_4 (float *data, int N, int direction);
void fwt_2d (IMAGE im, float **result);
void fwti_2d (float **result, IMAGE im);
float ** f2d (int i, int j);
void toint ( float ** image , IMAGE output, float *h);

void main(int argc, char *argv[])
{
	IMAGE im;
	float **result, xmax, xmin, xd;
	int maxr, i, j, k;
	FILE *f;

	if (argc<3)
	{
	  printf ("2d wavelet transform - <input> <output>\n");
	  printf ("File of raw wavelet coefficients saved in 'wtout'\n");
	  printf ("Daubechies basis\n");
	  exit (1);
	}

	printf ("Computing a 2d wavelet transform on a Daubechies basis:\n");
	im = Input_PBM (argv[1]);
	if (im == 0)
	{
		printf ("Cannot create input image!\n");
		exit (1);
	}
	result = f2d(im->info->nr, im->info->nc);

	fwt_2d (im, result);

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
	    im->data[i][j] = (unsigned char) ((result[i][j]-xmin)/xd * 255);
	Output_PBM (im, "wtout");

	printf ("Reconstructing the data from the wavelets:\n");
	printf ("Enter max row/column for reconstruction:\n");
	scanf ("%d", &maxr);

/* Remove all coefficients except those in rows/cols 1-maxr */
	if (maxr > 0)
	for (i=0; i<im->info->nr; i++)
	  for (j=0; j<im->info->nc; j++)
	    if (i>maxr || j > maxr) result[i][j] = 0.0;

/* back-transform */
	fwti_2d (result, im);

	Output_PBM (im, argv[2]);
}

void fwt_1d (float *data, int N, int direction)
{
	int thisn;

	if (N > 4)
	{
	  if (direction >= 0)
	  {
	    thisn = N;
	    while (thisn >= 4)
	    {
	      wavelet (data, thisn, direction);
	      thisn /= 2;
	    }
	  } else if (direction == INVERSE) 
	  {
	    thisn = 4;
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
	toint (result, im, 0);
	return ;
 
	for (i=0; i<im->info->nr; i++)
	  for (j=0; j<im->info->nc; j++)
	    if (result[i][j] > 255.0) im->data[i][j] = 255;
	    else if (result[i][j] < 0) im->data[i][j] = 0;
	    else im->data[i][j] = (unsigned char)result[i][j];
}

void wavelet (float *data, int N, int direction)
{
	Daub_4 (data, N, direction);
}

void Daub_4 (float *data, int N, int direction)
{
	int i,j,nover2;
	float *tmp;
	static int init = 0;

	if (init == 0)
	{
	  double rt3, rt2;

/* The coefficients (Equation 7.30) */
	  rt3 = sqrt(3.0);
	  h0 = (1.0 + rt3)/4.0;         h1 = (3.0 + rt3)/4.0;
	  h2 = (3.0 - rt3)/4.0;         h3 = (1.0 - rt3)/4.0;
	  rt2 = sqrt(2.0);

/* Symmetrical scaling */
	  h0 /= rt2;    h1 /= rt2;      h2 /= rt2;      h3 /= rt2;
	  init = 1;
	}

	tmp = (float *)malloc (sizeof(float)*N);
	nover2 = N/2;
	if (direction>=0) 
	{
	  i=0;
	  for (j=0; j<N-3; j+=2)
	  {
	    tmp[i] = h0*data[j] + h1*data[j+1] + h2*data[j+2] + h3*data[j+3];
	    tmp[i+nover2] = 
		-(h0*data[j+3] + h2*data[j+1]) + (h1*data[j+2] + h3*data[j]);
	    i++;
	  }
/* End around overlap */
	    tmp[i] = h0*data[N-2] + h1*data[N-1] + h2*data[0] + h3*data[1];
	    tmp[i+nover2] =
	      -(h0*data[1] + h2*data[N-1]) + (h1*data[0] + h3*data[N-2]);
	} else {
	  i = 0; j = 2;
	  do
	  {
	    tmp[j]=h2*data[i]+h1*data[i+nover2]+
		   h0*data[(i+1)%nover2]+h3*data[(i+1)%nover2 + nover2];
	    tmp[j+1]=h3*data[i]-h0*data[i+nover2]+
		   h1*data[(i+1)%nover2]-h2*data[(i+1)%nover2 + nover2];
	    j=(j+2)%N; i++;
	  } while (i < nover2);
	}

	for (i=0; i<N; i++) data[i] = tmp[i];
/*        bcopy (tmp, data, N*sizeof(float));          */
	free (tmp);
}

/* Convert a REAL/COMPLEX image into an integer one for display */
void toint ( float ** image , IMAGE output, float *h)
{
	float xmax, xmin, x, dx,y, xdif;
	int i,j,n, ii, jj, kk,k,nn, H[256];
	IMAGE cim;

/* OUTPUT must be an allocated byte image, at least as big as IMAGE in
   each dimension. IMAGE will be converted into real from complex.      */

	n = output->info->nr;
	cim = output;
	xmax = -1.0e20; xmin = -xmax; 
	for (i=0; i<n; i++) 
	  for (j=0; j<n; j++) 
	  {
	    if (xmax < image[i][j]) xmax = image[i][j];
	    if (xmin > image[i][j]) xmin = image[i][j];
	  }

	y = xmin;
	xmax = -1.0e20; xmin = -xmax; 
	for (i=0; i<n; i++) 
	  for (j=0; j<n; j++) 
	  {
	    x = image[i][j] - y;
	    if (x > 0.0)
	      x = (float) log(sqrt ( (double) x ));     
	    else x = 0.0;
	    if (xmax < x) xmax = x;
	    if (xmin > x) xmin = x;
	    image[i][j] = x;
	  }

	if ( (xmin <= 0.00001) && (xmin >= -0.00001) ) xmin = 0.000000;
	printf ("Xmax is %12.5f   Xmin is %12.6f\n", xmax, xmin);

/* Compute the histogram H or accept it as a parameter */
	if (h==0)
	  for (i=0; i<256; i++) H[i] = (n*n)/256;
	else 

/* If h is passed, it contains % of total number of pixels. */
	  for (i=0; i<256; i++) 
		H[i] = (int) ( (h[i]/100.0)*(n*n) );

	xdif = xmax-xmin;
	if (xdif <= 0.0) printf ("******* ZERO RANGE!!\n");
	for (i=0; i<n; i++)
	  for (j=0; j<n; j++) 
	  {
	    x = image[i][j];
	    x = (x-xmin)/xdif * 255.0;
	    if (x<0.0) x = 0.0;
	     else if (x>255.0) x=255.0;
	    cim->data[i][j] = (unsigned char) x;
	  }
}
