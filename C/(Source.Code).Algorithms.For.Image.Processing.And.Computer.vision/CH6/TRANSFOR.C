/* Compute and display the Fourier transform */
#define MAX
#include "fftlib.h"

int
main (int argc, char *argv[])
{
	int nr, nc, again, radius;
	IMAGE im1, p;
	COMPLEX_IMAGE fft1, fft2;
	int i,j, hc, vc, width,dist;

	printf ("Image Restoration System module 12 - FFT\n");

/* Try to open standard image files.  */
	if (argc < 3)
	{
		printf ("transform input output\n\n");
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

	normalize_set();
	image_fftoc (im1, &fft1);
	normalize_clear();
	filt_toint (fft1, im1, 0);
	Output_PBM (im1, argv[2]);
}

