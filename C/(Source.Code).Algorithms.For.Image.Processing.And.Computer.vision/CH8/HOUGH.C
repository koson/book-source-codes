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

struct grec {		/* Information concerning a glyph */
	char value;
	short int nr, nc;
	GIMAGE ptr;
	struct grec * next;
};
typedef struct grec * GPTR;
GPTR database[256];

void FDisplay (float ** x, int nr, int nc);
void hough (IMAGE x, float *theta);

void main (int argc, char *argv[])
{
	IMAGE im;
	char text[128];
	int i, j;
	FILE *f;
	float theta;

	im = newimage (64, 64);
	for (i=0; i<64; i++)
	  for (j=0; j<64; j++)
		im->data[i][j] = BACK;
	im->data[2][2] = OBJECT;
	im->data[22][22] = OBJECT;
	im->data[55][55] = OBJECT;
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
	center_x = x->info->nc/2;	center_y = x->info->nr/2;
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
/*			if (r == 0) continue;  */
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
	FDisplay (z, 180, 2*rmax+1);
	free (z[0]); free (z);
}

void FDisplay (float ** x, int nr, int nc)
{
	float xmax, xmin, z, rng;
	int count=0, k=0;
	int i,j;
	FILE *f;

	xmax = xmin = x[0][0];
	for (i=0; i<nr; i++)
	  for (j=0; j<nc; j++)
	  {
	    if (xmax < x[i][j]) xmax = x[i][j];
	    if (xmin > x[i][j]) xmin = x[i][j];
	  }
	fprintf (stderr, "DISPLAY: xmax = %f xmin = %f\n", xmax, xmin);
	rng = xmax - xmin;

	f = fopen ("/tmp/z", "w");
	fprintf (f, "P2\n%d %d\n255\n", nc, nr);
	for (i=0; i<nr; i++)
	  for (j=0; j<nc; j++)
	  {
	    z = x[i][j];
	    k = (unsigned char) ((z-xmin)/rng * 255);
	    fprintf (f, "%3d ", k);
	    count++;
	    if (count > 30)
	    {
		count = 0;
		fprintf (f, "\n");
	    }
	  }
	fclose (f);
	system ("xv /tmp/z");
}

