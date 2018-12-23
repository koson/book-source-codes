/*
			    Motion Blur Simulation
*/
#define MAX
#include "lib.h"

int main (int argc, char *argv[])
{
	int n=0, nr, nc;
	IMAGE im1;
	float **p;
	float x, xi, xspeed, mean, xmax, xmin, xd;
	int i,j,k, kk;

	printf ("Image Restoration System module 10 - Motion Blur\n");

/* Try to open standard image files.  */
	if (argc < 4)
	{
		printf ("MBLUR input Xspeed output\n\n");
		exit (0);
	}

	im1 = Input_PBM (argv[1]);
	if (im1 == 0)
	{
	  printf ("No such file as '%s'\n", argv[1]);
	  exit(1);
	}

	sscanf (argv[2], "%f", &xspeed);

	if (im1->info->nr != im1->info->nc)
	{
		printf ("Input image is not square. Use ALIGN.\n");
		exit(0);
	}
	nr = im1->info->nr; nc = im1->info->nc;

	p = f2d (nr, nc);
	mean = 0.0;
	for (i=0; i<nr; i++)
	  for (j=0; j<nc; j++)
	  {
	    p[i][j] = 255.0;
	    mean = mean + im1->data[i][j];
	  }
	mean = mean / (float)(nc*nr);

	xmax = 0; xmin = 35536;
	for (i=0; i<nr; i++)
	  for (j=0; j<nc; j++)
	  {
	    x = 0.0;
	    for (k=j-xspeed+1; k<=j; k++)
	    {
	      if (k<0) kk = k + nc;
		else if (k >= nc) kk = k-nc;
		else kk = k;
	      x = x + (float)im1->data[i][kk];
	    }
	    p[i][j] = (int)(x/(float)xspeed + 0.5);
	    if (xmax < p[i][j]) xmax = p[i][j];
	    if (xmin > p[i][j]) xmin = p[i][j];
	  }
	xd = (xmax-xmin);

	for (i=0; i<nr; i++)
	  for (j=0; j<nc; j++)
	    im1->data[i][j] = (unsigned char)(((p[i][j]-xmin)/xd) *255);
	Output_PBM (im1, argv[3]);
}


