/* Notch filter for grid removal */
#define MAX
#include "fftlib.h"

int
main (int argc, char *argv[])
{
	int nr, nc, again;
	IMAGE im1, p;
	COMPLEX_IMAGE fft1, fft2;
	int i,j, hc, vc, width,dist;

	printf ("Image Restoration System module 9 - notch filter\n");

/* Try to open standard image files.  */
	if (argc < 3)
	{
		printf ("notch input  output\n\n");
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
	p = im1;

	normalize_set();
	image_fftoc (im1, &fft1);
	normalize_clear();
	fft2 = dupcomplex (fft1);

	printf ("Select notch: Enter the horizontal center: ");
	scanf ("%d", &hc);
	printf ("Enter the vertical center: ");
	scanf ("%d", &vc);
	printf ("Enter width/2 (in pixels). i.e. Pixels to left of center: ");
	scanf ("%d", &width);
	printf ("Enter the distance of the notch from the center: ");
	scanf ("%d", &dist);
	printf ("Setting to 0 elements in that region.\n");

/* Vertical notch */
	for (i=0; i<nr; i++)
	{
	  if (abs(nr/2-i) < dist) continue;
	  for (j=hc-width; j<=hc+width; j++)
	  {
		fft2[i][j] = 0.0;
		fft2[i][j+nc] = 0.0;
	  }
	}

/* Horizontal notch */
	for (i=vc-width; i<=vc+width; i++)
	  for (j=0; j<nc; j++)
	  {
	    if (abs(nc/2-j) < dist)continue;
	    fft2[i][j] = 0.0;
	    fft2[i][j+nc] = 0.0;
	  }

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
	Output_PBM (im1, argv[2]);
}

