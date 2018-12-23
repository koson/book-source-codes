#define MAX
#include "fftlib.h"

int
main (int argc, char *argv[])
{
	int n=0, nr, nc;
	IMAGE im1, p;
	COMPLEX_IMAGE fft1, fft2;
	float x, xi, rel, img;
	int i,j;

	printf ("Image Restoration System module 6 - Structured noise\n");

/* Try to open standard image files.  */
	if (argc < 3)
	{
		printf ("snoise input  output\n\n");
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

/* Fourier transform */
	normalize_set();
	image_fftoc (im1, &fft1);
	normalize_clear();

/* Extract a peak */
	x = 0.0;
	for (i=0; i<nr; i++)
	  for (j=0; j<nc; j++)
	    if ((xi=pix_cnorm(fft1[i][j],fft1[i][j+nc])) > x)
	    {
		x = xi;
		rel = fft1[i][j]; img = fft1[i][j+nc];
	    }

/* Insert new peaks */
	n = nr/4;
	for (i=0; i<nr; i++)
	  for (j=0; j<nc; j++)
	  {
	    fft1[i][j] = 0.0;
	    fft1[i][j+nc] = 0.0;
	  }
	fft1[n][n] = rel/2;
	fft1[3*n][3*n] = rel/2;
	fft1[n][n+nc] = img/2;
	fft1[3*n][3*n+nc] = img/2;

/* Back-transform */
	image_fftinv (fft1, &fft2);
	freecomplex (fft1); fft1 = (COMPLEX_IMAGE)0;

	realtoint (fft2, 0);
	for (i=0; i<nr; i++)
	  for (j=0; j<nc; j++)
	    im1->data[i][j] = ((int)fft2[i][j]+im1->data[i][j])/2;

/*
	filt_toint (fft2, im1, 0);
*/
	Output_PBM (im1, argv[2]);

}

