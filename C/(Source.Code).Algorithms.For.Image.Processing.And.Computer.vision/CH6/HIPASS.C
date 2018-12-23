/* A high pass filter, in the frequency domain */
#define MAX
#include "fftlib.h"

int
main (int argc, char *argv[])
{
	int nr, nc, again, radius;
	IMAGE im1, p;
	COMPLEX_IMAGE fft1, fft2;
	int i,j, hc, vc, width,dist;

	printf ("Image Restoration System module 13 - High pass filter\n");

/* Try to open standard image files.  */
	if (argc < 4)
	{
		printf ("hipass input radius output\n\n");
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
	sscanf (argv[2], "%d", &radius);

	normalize_set();
	image_fftoc (im1, &fft1);
	normalize_clear();

/* Clear pixels outside the specified radius of the center */
	hc = nc/2; vc = nr/2;
	for (i=0; i<nr; i++)
	  for (j=0; j<nc; j++)
	  {
	    dist = (float)sqrt((double)((i-vc)*(i-vc) + (j-hc)*(j-hc)));
	    if (dist <= radius)
	    {
		fft1[i][j] = 0.0;
		fft1[i][j+nc] = 0.0;
	    }
	  }

	image_fftinv (fft1, &fft2);
	realtoint (fft2, 0);
	for (i=0; i<nr; i++)
	  for (j=0; j<nc; j++)
	      im1->data[i][j] = (int)fft2[i][j];
	Output_PBM (im1, argv[3]);
}

