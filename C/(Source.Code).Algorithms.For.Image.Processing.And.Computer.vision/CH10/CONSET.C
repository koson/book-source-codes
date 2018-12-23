/*      User-defined functions for the contour problem */
/* For use with fpcgen */

#define EX 0

#include "lib.h"
#include <math.h>
#include <stdio.h>

extern int nargs;
IMAGE im, dist, edge;
int N=0;

double getarg (int n, unsigned char *this);
void edge_sobel (IMAGE x, IMAGE z);
long line_acc (int x0, int y0, int x1, int y1);
void distance_map (IMAGE ed, IMAGE dm);
int putpix (int x, int y);
double angle_2pt (int r1, int c1, int r2, int c2);

void user_init ()
{
	int i,j, k;
	FILE *f;
	char fn[128];

	printf ("This GA fits a contour to an input image.\n");

	printf ("The name of the image file is: ");
	scanf ("%s", fn);
	im = Input_PBM (fn);
	if (im == 0) exit (1);

	edge = newimage (im->info->nr, im->info->nc);
	edge_sobel (im, edge);

	dist = newimage (im->info->nr+256, im->info->nc+256);
	distance_map (edge, dist);
}

/* Evaluate chomosome N and store result in EVALS array */
double feval (unsigned char *bs, int n)
{
	static double CONV = 180.0/3.1415926535;
	double a, b, polyx[7], polyy[7], a1, a2;
	double r1,r2,r3, t1,t2,t3,x1,y1;
	int i,j,k, nn=0;

/* Extract the arguments */
	r1 = getarg(0, bs);
	r2 = getarg(1, bs);
	r3 = getarg(2, bs);
	t1 = getarg(3, bs);
	t2 = getarg(4, bs);
	t3 = getarg(5, bs);
	x1 = getarg(6, bs);
	y1 = getarg(7, bs);

/* Locate the polygon vertices (of the contour) */
	a = r1 * sin(t1);       b = r1 * cos(t1);
	polyx[0] = x1 + b;      polyx[3] = x1 - b;
	polyy[0] = y1 + a;      polyy[3] = y1 - a;
	a = r2 * sin(t2);       b = r2 * cos(t2);
	polyx[1] = x1 + b;      polyx[4] = x1 - b;
	polyy[1] = y1 + a;      polyy[4] = y1 - a;
	a = r3 * sin(t3);       b = r3 * cos(t3);
	polyx[2] = x1 + b;      polyx[5] = x1 - b;
	polyy[2] = y1 + a;      polyy[5] = y1 - a;
	polyx[6] = polyx[0];    polyy[6] = polyy[0];

/* Compute constraint penalties */
	b = 0.0;
	for (i=1; i<7; i++)
	{
	  a1 = angle_2pt (x1, y1, polyx[i-1], polyy[i-1]);
	  a2 = angle_2pt (x1, y1, polyx[i], polyy[i]);
	  if (a2 - a1 < 0.0) b+= 1000.0;
	  if (fabs(a2-a1)*CONV < 5.0) b += 500.0;
	}

/* Compute the distance for this contour */
	for (i=0; i<6; i++)
	{
	  a = line_acc (polyx[i], polyy[i], polyx[i+1], polyy[i+1]);
	  nn += N;
	  b += a;
	}
	if (nn>0)
		return b/nn;
	return 10000.0;
}

double angle_2pt (int r1, int c1, int r2, int c2)
{
/*      Compute the angle between two points. (r1,c1) is the origin
	specified as row, column, and (r2,c2) is the second point.
	Result is between 0-360 degrees, where 0 is horizontal right. */

	double atan(), fabs();
	double x, dr, dc, conv;

	conv = 180.0/3.1415926535;
	dr = (double)(r2-r1); dc = (double)(c2-c1);

/*      Compute the raw angle based of Drow, Dcolumn        */
	if (dr==0 && dc == 0) x = 0.0;
	else if (dc == 0) x = 90.0;
	else {
		x = fabs(atan (dr/dc));
		x = x * conv;
	}

/*      Adjust the angle according to the quadrant            */
	if (dr <= 0) {                  /* upper 2 quadrants */
	  if (dc < 0) x = 180.0 - x;    /* Left quadrant */
	} else if (dr > 0) {            /* Lower 2 quadrants */
	  if (dc < 0) x = x + 180.0;    /* Left quadrant */
	  else x = 360.0-x;             /* Right quadrant */
	}

	return x;
}

/* Sobel edge detector - no templates */

void thresh (IMAGE z)
{
	int histo[256];
	int i,j,t;
	
/* Compute a grey level histogram */
	for (i=0; i<256; i++) histo[i] = 0;
	for (i=1; i<z->info->nr-1; i++)
	  for (j=1; j<z->info->nc-1; j++)
	  {
	    histo[z->data[i][j]]++;
	  }
	
/* Threshold at the middle of the occupied levels */
	i = 255; 
	while (histo[i] == 0) i--;
	j = 0;
	while (histo[j] == 0) j++;
	t = (i+j)/2;

/* Apply the threshold */
	for (i=1; i<z->info->nr-1; i++)
	  for (j=1; j<z->info->nc-1; j++)
	    if (z->data[i][j] >= t) z->data[i][j] = 0;
	    else z->data[i][j] = 255;
}

/*      Apply a Sobel edge mask to the image X  */

void edge_sobel (IMAGE x, IMAGE z)
{
	int i,j,n,m,k;

	for (i=0; i<x->info->nr; i++)
	  for (j=0; j<x->info->nc; j++) 
	    z->data[i][j] = 255;

/* Now compute the convolution, scaling */
	for (i=1; i<x->info->nr-1; i++)
	  for (j=1; j<x->info->nc-1; j++) 
	  {
	    n = (x->data[i-1][j+1]+2*x->data[i][j+1]+x->data[i+1][j+1]) -
		(x->data[i-1][j-1]+2*x->data[i][j-1]+x->data[i+1][j-1]);
	    m = (x->data[i+1][j-1]+2*x->data[i+1][j]+x->data[i+1][j+1])-
		(x->data[i-1][j-1]+2*x->data[i-1][j]+x->data[i-1][j+1]);
	    k = (int)( sqrt( (double)(n*n + m*m) )/4.0 );
	    z->data[i][j] = k;
	  }

/* Threshold the edges */
	thresh (z);
}

void distance_map (IMAGE ed, IMAGE dm)
{
	int i,j,k, nays[2][8];
	int n1, n2, f;
	long val;

	for (i=0; i<dm->info->nr; i++)
	  for (j=0; j<dm->info->nc; j++)
	    dm->data[i][j] = 255;

	for (i=0; i<ed->info->nr; i++)
	  for (j=0; j<ed->info->nc; j++)
	    dm->data[i+128][j+128] = ed->data[i][j];

	for (k=0; k<256; k++)
	{
	  f = 0;
	  for (i=0; i<ed->info->nr; i++)
	    for (j=0; j<ed->info->nc; j++)
	      if (dm->data[i+128][j+128] == k)
	      {
		for (n1= i-1; n1 <= i+1; n1++)
		  for (n2 = j-1; n2 <= j+1; n2++)
		  {
		    if(n1<0 || n2<0 || n1>=dm->info->nr || n2>=dm->info->nc)
			continue;
		    if (dm->data[n1+128][n2+128]>k+1)
		    { 
			dm->data[n1+128][n2+128] = k+1;
			f = 1;
		    }
		  }
	      }
	  if (f==0) break;
	}
}

/* Midpoint line scan conversion */
long line_acc (int x0, int y0, int x1, int y1)
{
	int dx, dy, incre, incrne, d, x, y, xx0, m;
	long sum = 0;

	N = 0;
	if (x0 > x1)
	{
		xx0 = x0; x0 = x1; x1 = xx0;
		xx0 = y0; y0 = y1; y1 = xx0;
	}

	m = 1;
	dx = x1-x0;     dy = y1-y0;
	if (EX) printf ("Dx = %d, Dy = %d\n", dx, dy);

	if (abs(dy)>abs(dx)) {
	  m = 2;
	  xx0 = x0; x0 = y0; y0 = xx0;
	  xx0 = x1; x1 = y1; y1 = xx0;
	  if (x0 > x1)
	  {
		xx0 = x0; x0 = x1; x1 = xx0;
		xx0 = y0; y0 = y1; y1 = xx0;
	  }
	  dx = x1-x0;   dy = y1-y0;
	}

	if (dy <= dx && (dy*dx<0)) { m = -m; dy = -dy; }

	d = 2*dy-dx;
	incre = 2*dy;
	incrne = 2*(dy-dx);
	x = x0; y = y0;
	if(EX) printf ("D=%d  incrne = %d  incre = %d\n (x,y)= (%d,%d)\n",
		d, incrne, incre, x, y);

	if (m*m==1)
	{
	  sum += putpix (x, y);
	  if (EX) printf ("Plot pixel at (%d,%d)\n", x, y);
	}
	else
	  sum += putpix (y, x);

	while (x < x1)
	{
	  if (d <= 0)
	  {
	    d += incre;
	    x++;
	    if(EX) printf ("d<0 so go east, x is now %d d now %d\n", x,d);
	  } else
	  {
	    d += incrne;
	    x++; y++;
	    if(EX) printf ("d>=0 so go northeast, (x,y) is now (%d,%d) d now %d\n", 
			x, y, d);
	  }
	  if (m==1)
	  {
		  sum += putpix (x, y);
		  if(EX) printf ("Plotting pixel at (%d,%d)\n", x, y);
	  }
	  else if (m == -1)
		sum += putpix (x, y0-(y-y0));
	  else if (m > 1)
		sum += putpix (y, x);
	  else if (m < -1)
		sum += putpix (y0-(y-y0), x);
	}
	return sum;
}

int putpix (int x, int y)
{
/*
	if (x < 0 || x > dist->info->nc) return 257;
	if (y < 0 || y > dist->info->nr) return 257;
*/
	N++;
	return dist->data[y+128][x+128];
}

