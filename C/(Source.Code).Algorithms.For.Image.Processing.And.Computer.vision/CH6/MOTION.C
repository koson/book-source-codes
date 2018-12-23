/*
			    Motion Blur Removal
*/
#define MAX
#include "lib.h"

int main (int argc, char *argv[])
{
	int n=0, nr, nc;
	IMAGE im1;
	float **p, xmax, xmin, xd;
	float xi, xspeed, sum1, sum2, mean_val;
	int i,j,k, x, m, y, jj, jind, K, a;

	printf ("Image Restoration System module 11 - Motion Blur\n");

/* Try to open standard image files.  */
	if (argc < 4)
	{
		printf ("MOTION input Xspeed output\n\n");
		exit (0);
	}

	sscanf (argv[2], "%f", &xspeed);
	im1 = Input_PBM (argv[1]);
	if (im1 == 0)
	{
	  printf ("No such file as '%s'\n", argv[1]);
	  exit(1);
	}

/*
	if (im1->info->nr != im1->info->nc)
	{
		printf ("Input image is not square. Use ALIGN.\n");
		exit(0);
	}
*/
	nr = im1->info->nr; nc = im1->info->nc;

	p = f2d (nr, nc);
	mean_val = 0.0;
	for (i=0; i<nr; i++)
	  for (j=0; j<nc; j++)
	  {
	    p[i][j] = 0;
	    mean_val += (float)(im1->data[i][j]);
	  }
	mean_val = mean_val/(float)(nc*nr);

/* Restore */
	a = (int)(xspeed+0.1);
	K = nc/a;
	xmax = -1000.0; xmin = -xmax;
	for (y=0; y<nr; y++)
	  for (x=0; x<nc; x++)
	  {
	    m = (int)(x/a);
	    if (m > K-1) continue;

	    sum1 = 0.0;
	    for (k=0; k<=K-1; k++)
	      for (j=0; j<=k; j++)
	      {
		jind = x-m*a + (k-j)*a;
		if (jind < nc && jind > 0)
		  sum1=sum1+(float)(im1->data[y][jind]-im1->data[y][jind-1]);  
	      }
	      sum1 = sum1/(float)K;

	      sum2 = 0.0;
	      for (j = 0; j<=m; j++)
	      {
		jind = x-j*a;
		if (jind < nc && jind > 0)
		  sum2 += (float)(im1->data[y][jind]-im1->data[y][jind-1]);  
	      }

	    sum1  = mean_val - sum1 + sum2; 
	    p[y][x] = sum1; 
	    if (xmax < sum1) xmax = sum1;
	    if (xmin > sum1) xmin = sum1;
	  }
	xd = xmax - xmin;

	for (y=0; y<nr; y++)
	   for (x=0; x<nc; x++)
	    im1->data[y][x] = (unsigned char)( ((p[y][x]-xmin)/xd) * 255 );
	Output_PBM (im1, argv[3]);
}

