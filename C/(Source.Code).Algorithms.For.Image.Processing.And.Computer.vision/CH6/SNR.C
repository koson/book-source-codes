/* Structured noise removal */
#define MAX
#include "fftlib.h"

int
main (int argc, char *argv[])
{
	int nr, nc, again;
	IMAGE im1, p;
	COMPLEX_IMAGE fft1, fft2;
	int i,j, ulr, ulc, lrr, lrc, ik, jk;

	printf ("Image Restoration System module 8 - Structured noise\n");

/* Try to open standard image files.  */
	if (argc < 3)
	{
		printf ("snr input  output\n\n");
		exit (0);
	}

/* A grey image is 1 conversion. A colour image needs 3, but all the same */
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
	p = newimage (nr, nc);

	normalize_set();
	image_fftoc (im1, &fft1);
	normalize_clear();
	fft2 = dupcomplex (fft1);
	filt_toint (fft1, p, 0);
	Display (p);

	do {
	  printf ("Select a region in the UPPER part of the image:\n");
	  printf ("Enter UL corner of area to be cleared:\n");
	  scanf ("%d", &ulr);
	  scanf ("%d", &ulc);
	  printf ("Enter LR corner of area to be cleared:\n");
	  scanf ("%d", &lrr);
	  scanf ("%d", &lrc);
	  printf ("Setting to 0 elements in that region.\n");
	  for (i=ulr; i<=lrr; i++)
	    for (j=ulc; j<=lrc; j++)
	    {
	      fft1[i][j] = 0.0;
	      fft1[i][j+nc] = 0.0;
		fft2[i][j] = 0.0;
		fft2[i][j+nc] = 0.0;
	    }
/*
          filt_toint (fft1, p, 0);
          Display (p);
*/
	
	  printf ("Another region? (1 for yes, 0 for no): ");
	  scanf ("%d", &again);
	} while (again);

	freecomplex (fft1); fft1 = (COMPLEX_IMAGE)0;
	image_fftinv (fft2, &fft1);

	realtoint (fft1, 0);
	for (i=1; i<nr-1; i++)
	  for (j=1; j<nc-1; j++)
	  {
	    if ( (i%2 == 0 && j%2==1) ||
		 (i%2 == 1 && j%2==0) )
	      im1->data[i][j] = (unsigned char)
	       ((fft1[i-1][j]+fft1[i+1][j]+fft1[i][j+1]+fft1[i][j-1])/4);
	    else
	      im1->data[i][j] = (int)fft1[i][j];
	  }
/*
	filt_toint (fft1, im1, 0);
	freecomplex (fft2); fft2 = (COMPLEX_IMAGE)0;
*/
	Output_PBM (im1, argv[2]);

}

