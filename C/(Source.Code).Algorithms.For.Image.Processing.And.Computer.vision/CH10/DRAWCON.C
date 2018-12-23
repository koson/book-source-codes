/*	Draw a contour		*/

#define MAX
#define EX 0

#include "lib.h"
#include <math.h>
#include <stdio.h>

IMAGE im;

void line (int x0, int y0, int x1, int y1);
void putpix (int x, int y);

void main(int argc, char *argv[])
{
	int i,j,k;
	double r1, r2, r3, t1, t2, t3, x1, y1;
	float polyx[9], polyy[9];
	double a, b;

	if (argc < 2)
	{
	  printf ("drawcon <image>\n");
	  printf ("Overlay a six-point contour on an image.\n");
	  exit (1);
	}

	im = Input_PBM (argv[1]);
	if (im == 0)
	{
	  printf ("Can't read the image file '%s'.\n", argv[1]);
	  exit (1);
	}


	printf ("Enter the 6pt parameters (r1 r2 r3 t1 t2 t3 x1 y1):\n");
	scanf ("%lf", &r1);
	scanf ("%lf", &r2);
	scanf ("%lf", &r3);
	scanf ("%lf", &t1);
	scanf ("%lf", &t2);
	scanf ("%lf", &t3);
	scanf ("%lf", &x1);
	scanf ("%lf", &y1);

	a = r1 * sin(t1);	b = r1 * cos(t1);
	polyx[0] = x1 + b;	polyx[3] = x1 - b;
	polyy[0] = y1 + a;	polyy[3] = y1 - a;
	a = r2 * sin(t2);	b = r2 * cos(t2);
	polyx[1] = x1 + b;	polyx[4] = x1 - b;
	polyy[1] = y1 + a;	polyy[4] = y1 - a;
	a = r3 * sin(t3);	b = r3 * cos(t3);
	polyx[2] = x1 + b;	polyx[5] = x1 - b;
	polyy[2] = y1 + a;	polyy[5] = y1 - a;
	polyx[6] = polyx[0];	polyy[6] = polyy[0];

	for (i=0; i<6; i++)
	  line (polyx[i], polyy[i], polyx[i+1], polyy[i+1]);

	Display (im);
}

/* Midpoint line scan conversion */
void line (int x0, int y0, int x1, int y1)
{
	int dx, dy, incre, incrne, d, x, y, xx0, m;

	if (x0 > x1)
	{
		xx0 = x0; x0 = x1; x1 = xx0;
		xx0 = y0; y0 = y1; y1 = xx0;
	}

	m = 1;
	dx = x1-x0;	dy = y1-y0;
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
	  dx = x1-x0;	dy = y1-y0;
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
	  putpix (x, y);
	  if (EX) printf ("Plot pixel at (%d,%d)\n", x, y);
	}
	else
	  putpix (y, x);

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
		  putpix (x, y);
		  if(EX) printf ("Plotting pixel at (%d,%d)\n", x, y);
	  }
	  else if (m == -1)
		putpix (x, y0-(y-y0));
	  else if (m > 1)
		putpix (y, x);
	  else if (m < -1)
		putpix (y0-(y-y0), x);
	}
}

void putpix (int x, int y)
{
	if (x <0 || x > im->info->nc) return;
	if (y < 0 || y > im->info->nr) return;
	im->data[y][x] = 255;
}

