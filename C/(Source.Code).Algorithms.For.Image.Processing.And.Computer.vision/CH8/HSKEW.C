/* Estimate the skew angle: Hough transform of low resolution smoothed image */

#define MAX
#include "lib.h"
#define OBJECT 1
#define BACK 0

struct lrec {
	int rs, re, cs, ce;
} LINE[400];
int Nlines = -1;
int Space = 1;
int Meanwidth = 0;

typedef unsigned char ** GIMAGE;

struct grec {           /* Information concerning a glyph */
	char value;
	short int nr, nc;
	GIMAGE ptr;
	struct grec * next;
};
typedef struct grec * GPTR;
GPTR database[256];

void hough (IMAGE x, float *theta);

void main (int argc, char *argv[])
{
	IMAGE im;
	char text[128];
	int i;
	FILE *f;
	float theta;

	if (argc < 2)
	{
	  printf ("Usage: hskew <image> \n");
	  exit (1);
	}

	im = Input_PBM (argv[1]);
	if (im == 0)
	{
	  printf ("The image file '%s' does not exist, or is unreadable.\n");
	  exit (2);
	}

	hough (im, &theta);
	printf ("Approximate skew angle is %10.5f\n", theta-90);
}

void hough (IMAGE x, float *theta)
{
	float **z;
	int center_x, center_y, r, omega, i, j, rmax, tmax;
	double conv;
	double sin(), cos(), sqrt();
	float tmval;

	conv = 3.1415926535/180.0;
	center_x = x->info->nc/2;       center_y = x->info->nr/2;
	rmax = 
	 (int)(sqrt((double)(x->info->nc*x->info->nc+x->info->nr*x->info->nr))/2.0);

/* Create an image for the Hough space - choose your own sampling */
	z = f2d (180, 2*rmax+1);

	for (r = 0; r < 2 * rmax+1; r++)
	   for (omega = 0; omega < 180; omega++)
		z[omega][r] = 0;

	tmax = 0; tmval = 0;
	for (i = 0; i < x->info->nr; i++)
	  for (j = 0; j < x->info->nc; j++)
		if (x->data[i][j])
		   for (omega = 0; omega < 180; ++omega) 
		   {
			r = (i - center_y) * sin((double)(omega*conv)) 
			   + (j - center_x) * cos((double)(omega*conv));
/*                      if (r == 0) continue;  */
			z[omega][rmax+r] += 1;
		   }

	for (i=0; i<180; i++)
	  for (j=0; j<2*rmax+1; j++)
	    if (z[i][j] > tmval)
	    {
		tmval = z[i][j];
		tmax = i;
	    }
	*theta = tmax;
	free (z[0]); free (z);
}

