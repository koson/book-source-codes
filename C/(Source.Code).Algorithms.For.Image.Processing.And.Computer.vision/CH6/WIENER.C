/* Wiener filter - restoration using only non-zero PSF values */

#define MAX
#include "fftlib.h"

int
main (int argc, char *argv[])
{
	int n=0, nr, nc;
	IMAGE im1, p;
	COMPLEX_IMAGE fft1, fft2;
	float x, xi, H, K, t;
	int i,j, k;

	printf ("Image Restoration System module 5 - Weiner filter\n");

/* Try to open standard image files.  */
	if (argc < 4)
	{
		printf ("WEINER input psf output\n\n");
		exit (0);
	}

	im1 = Input_PBM (argv[1]);
	if (im1 == 0)
	{
	  printf ("No such file as '%s'\n", argv[1]);
	  exit(1);
	}

	p = Input_PBM (argv[2]);
	if (p == 0)
	{
	  printf ("No such file as '%s'\n", argv[2]);
	  exit(1);
	}


	if (im1->info->nr != im1->info->nc)
	{
		printf ("Input image is not square. Use ALIGN.\n");
		exit(0);
	}

        if (p->info->nr != p->info->nc)
        {
                printf ("PSF image is not square. Use ALIGN.\n");
                exit(0);
        }

	if (im1->info->nr != p->info->nr)
	{
		printf ("Input and PSF images differ in size.\n");
		exit (0);
	}
	nr = im1->info->nr; nc = im1->info->nc;

	image_fftoc (im1, &fft1);
	normalize_set ();
	image_fftoc (p,   &fft2);
	normalize_clear();

	printf ("Enter K: ");
	scanf ("%f", &K);

	for (i=0; i<nr; i++)
	  for (j=0; j<nc; j++)
	  {
	    x = fft2[i][j];	xi = fft2[i][j+nc];
	    H = pix_cnorm (x, xi);
	    if (H == 0.0)
	    {
	      fft1[i][j] = fft1[i][j+n] = 0.0;
	      continue;
	    }
	    t = H/(H+K);
	    cdiv (t, 0.0, &x, &xi);
	    cprod (fft1[i][j], fft1[i][j+nc], &x, &xi);
	    fft1[i][j] = x; fft1[i][j+nc] = xi;
	  }

	freecomplex (fft2); fft2 = (COMPLEX_IMAGE)0;

	image_fftinvoc (fft1, &fft2);
	freecomplex (fft1); fft1 = (COMPLEX_IMAGE)0;

	filt_toint (fft2, p, 0);
	Output_PBM (p, argv[3]);

}

