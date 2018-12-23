/* --------------------------------------------------------------------
		Create a point spread function , Gaussian
			128x128
			256x256
			512x512

J. Parker 95-08-01
   -------------------------------------------------------------------- */

#define MAX
#include "lib.h"

int NR, NC;
char filename[128];

float gauss (int I, int J, float NR, float sd, float v)
{
	float arg, ex, r;

	r = (float)sqrt ((double)((I-NR)*(I-NR) + (J-NR)*(J-NR)));
	arg = (r)/sd;
	ex = (float)exp((double)(-arg*arg));
	return v*ex;
}

int main (int argc, char *argv[])
{
	FILE *f;
	int colour = 0, n=0, shrink=0;
	int i,j;
	IMAGE im1, p;
	float xc, a, b, c, xx, yy;

	printf ("Image Restoration System : Create PSF \n");
	if (argc < 2)
	{
	  printf ("%s <size>\n", argv[0]);
	  exit (1);
	}

	sscanf (argv[1], "%d", &NR);
	NC = NR;
	printf("Size is to be %dx%d\n", NR, NC);

/* Allocate a new image */
	p = newimage (NR, NC);
	xc = (float)NR/2.0;

/* Args? */
	printf ("SD  : "); scanf ("%f", &b);
	printf ("I   : "); scanf ("%f", &c);

	xx =  gauss(xc, xc, xc, b, c);
	for (i=0; i<NR; i++)
	  for (j=0; j<NC; j++)
	    p->data[i][j] = (unsigned char)((gauss(i,j,xc, b, c)));

/* Write the new array into an image file */
	Output_PBM (p, "psf.pgm");
	printf ("Output file is 'psf.pgm'.\n");

	return 0;
}

