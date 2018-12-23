/* --------------------------------------------------------------------
		Laboratory for Computer Vision
	      Image Restoration SHUFFLE Utility

		Rearrange an image, shuffled by an FFT,
		into its original form.

J. Parker 95-12-08

   -------------------------------------------------------------------- */
#define MAX 1
#include "lib.h"

int NR, NC;
char filename[128];

int main (int argc, char *argv[])
{
	char typ[64], cmd[128], cg[64];
	FILE *f;
	int colour = 0, n=0, k=0;
	IMAGE im1, p;
	int i,j;

	printf ("Image Restoration System module N - Shuffle\n");

/* Try to open standard image files. Is it colour (3 images) or grey? */
	if (argc < 2)   /* No argument passed */
	{
	  printf ("SHUFFLE - No image argment.\n");
	  exit (1);
	}

	f = fopen (argv[1], "r");
	if (f == NULL)          /* File name passed as argument */
	{
		printf ("Cannot open image file '%s'. \n");
		exit (1);
	}
	fclose (f);
	strcpy (filename, argv[1]);
	im1 = Input_PBM(filename);
	if (im1 == 0)
	{
		printf ("No such input image.\n");
		exit(0);
	}

/* Determine the size of the new image */
	NR = im1->info->nr; NC = im1->info->nc;
	if (NR != NC)
	{
	  printf ("Processed input image is not square!\n");
	  exit (1);
	}

	n = NR; k = n/2;
	printf ("Size is  %dx%d\n", n,n);

/* Allocate a new image */
	p = newimage (n, n);

/* Copy the old image into the new frame */
	for (i=0; i<k; i++)
	  for (j=0; j<k; j++)
	  {
	    p->data[i][j]     = im1->data[i+k][j+k];
	    p->data[i+k][j+k] = im1->data[i][j];
	    p->data[i][j+k]   = im1->data[i+k][j];
	    p->data[i+k][j]   = im1->data[i][j+k];
	  }

/* Write the new array into an image file */
	if (argc > 1)
	{
	    Output_PBM (p, "shuff.pgm");
	    printf ("Result is 'shuff.pgm'\n");
	    exit (0);
	}
}

