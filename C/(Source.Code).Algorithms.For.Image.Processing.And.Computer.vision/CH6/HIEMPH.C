/* High emphasis filter for Homomorphic processing */


/* The high emphasis filter cross-section */
float d[] = {
	0.5, 0.55, 0.6, 0.8, 1.0, 1.3, 1.5, 1.6, 1.7, 
	1.8, 1.85, 1.9, 1.94, 1.97, 1.99, 2.0, 2.0};
int Nd = 16;
int FMAX = 15;

#define MAX
#include "fftlib.h"

int
main (int argc, char *argv[])
{
	int nr, nc, again, radius;
	IMAGE im1, p;
	COMPLEX_IMAGE fft1, fft2;
	int i,j, hc, vc, width,dist, ia, ib;
	float val, a, b;

	printf ("Image Restoration System module 15 - High emphasis filter\n");

/* Try to open standard image files.  */
	if (argc < 3)
	{
		printf ("hiemph input output\n\n");
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
	p = newimage (nr, nc);

	normalize_set();
	image_fftoc (im1, &fft1);
	normalize_clear();
	fft2 = dupcomplex (fft1);

/* Construct the filter image */
	hc = nc/2; vc = nr/2;
	for (i=0; i<nr; i++)
	  for (j=0; j<nc; j++)
	  {
	    dist = (float)sqrt((double)((i-vc)*(i-vc) + (j-hc)*(j-hc)));
	    if (dist > FMAX) fft2[i][j] = 2.0;
	    else if (dist > 0.0)
	    {
	        ia = (int)dist;  ib = ia+1;
		a  = d[ia];      b  = d[ib];
		val = (float)(dist-ia)*a + (float)(ib-dist)*b;
		fft2[i][j] = val;
	    } else fft2[i][j] = d[0];
	    fft2[i][j+nc] = 0.0;
	    p->data[i][j] = fft2[i][j]*127;
	  }

	Output_PBM (p, "he.pgm");

/* Clear pixels in the specified radius of the center */
	for (i=0; i<nr; i++)
	  for (j=0; j<nc; j++)
	    cprod (fft2[i][j], fft2[i][j+nc], &fft1[i][j], &fft1[i][j+nc]);

	image_fftinv (fft1, &fft2);
	realtoint (fft2, 0);
	for (i=0; i<nr; i++)
	  for (j=0; j<nc; j++)
	      im1->data[i][j] = (int)fft2[i][j];
	Output_PBM (im1, argv[2]);
}

