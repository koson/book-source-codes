/* Homomorphic filter */

/* The high emphasis filter cross-section */
float d[] = {
	0.5,  0.51,
	0.53, 0.55, 0.6, 0.8, 1.0, 1.3, 1.5, 1.6, 1.7, 
	1.8, 1.85, 1.9, 1.94, 1.97, 1.99, 2.0, 2.0};
int Nd = 18;
int FMAX = 17;
int FMIN = 1;

#define MAX
#include "fftlib.h"

void quicksort (float *arr, int l, int r);
void scale (COMPLEX_IMAGE x, IMAGE im, float pct);

int
main (int argc, char *argv[])
{
	int nr, nc, again, radius;
	IMAGE im1, p;
	COMPLEX_IMAGE fft1, fft2, res;
	int i,j, hc, vc, width, ia, ib;
	float val, a, b, dist, xmax, xmin;

	printf ("Image Restoration System module 16 - Homomorphic filter\n");

/* Try to open standard image files.  */
	if (argc < 3)
	{
		printf ("homo input output\n\n");
		exit (0);
	}

	im1 = Input_PBM (argv[1]);
	if (im1 == 0)
	{
	  printf ("No such file as '%s'\n", argv[1]);
	  exit(1);
	}

	if (im1->info->nr != im1->info->nc)
	{
		printf ("Input image is not square. Use ALIGN.\n");
		exit(0);
	}

	nr = im1->info->nr; nc = im1->info->nc;

	FFTN = nc;
	fftinit (FFTN);
	fft1 = newcomplex (im1);

/* Take the log of the image  */
	for (i=0; i<nr; i++)
	  for (j=0; j<nc; j++)
	  {
	    fft1[i][j] = (float)log( (double)(im1->data[i][j]+1) );
	    fft1[i][j+nc] = 0.0;
	  }

	filt_orig (fft1);
	fft2d (fft1, FORWARD);

/* Apply filter */
	vc = nc/2; hc = nr/2;
	for (i=0; i<nr; i++)
	  for (j=0; j<nc; j++)
	  {
	    dist = (float)sqrt((double)((i-vc)*(i-vc) + (j-hc)*(j-hc)));
	    if (dist <= FMIN) cprod (0.4, 0.0, &fft1[i][j], &fft1[i][j+nc]);
	    else if (dist >= FMAX)
		cprod (2.0, 0.0, &fft1[i][j], &fft1[i][j+nc]);
	    else {
	      ia = (int)dist;  ib = ia+1;
	      a  = d[ia];      b  = d[ib];
	      val = (float)(dist-ia)*a + (float)(ib-dist)*b;
	      cprod (val, 0.0, &fft1[i][j], &fft1[i][j+nc]);
	    }
	  }

	image_fftinvoc (fft1, &res);
	freecomplex (fft1);

/* Exponentiate  */
	xmin = 1.0e12; xmax = -xmin;
	for (i=0; i<nr; i++)
	  for (j=0; j<nc; j++)
	  {
	    a = pix_cnorm (res[i][j], res[i][j+nc]);
	    a = sqrt(a);
	    res[i][j] = (float)exp( (double)a) -1.0;
	    if (xmax < res[i][j]) xmax = res[i][j];
	    if (xmin > res[i][j]) xmin = res[i][j];
	  }
	printf ("MAX %f MIN %f\n", xmax, xmin);

	scale (res, im1, 3.0);
	Output_PBM (im1, argv[2]);
}

void scale (COMPLEX_IMAGE x, IMAGE im, float pct)
{
	int i,j,k,n,m;
	float z;
	float *hist, *p;

	n = im->info->nr*im->info->nc;
	hist = (float *)malloc (sizeof(float)*n );
	p = hist;
	for (i=0; i<im->info->nr; i++)
	  for (j=0; j<im->info->nc; j++)
	   *p++ = x[i][j];
	quicksort (hist, 0, n);
	k = n*(pct/100.0);
	z = hist[k];

	for (i=0; i<im->info->nr; i++)
	  for (j=0; j<im->info->nc; j++)
	    if (x[i][j] > z) im->data[i][j] = 255;
	    else im->data[i][j] = (x[i][j]/z)*254;
	free(hist);
}

void quicksort (float *arr, int l, int r)
{
	int i,j;
	float x, w;
 
	i = l; j = r;
	x = arr[(l+r)/2];
	do
	{
	  while (arr[i] > x) i++;
	  while (x > arr[j]) j--;
	  if (i <= j)
	  {
	    w = arr[i]; arr[i] = arr[j]; arr[j] = w;
	    i++; j--;
	  }
	} while (i <= j);
	if (l<j) quicksort (arr, l, j);
	if (i<r) quicksort (arr, i, r);
 }
