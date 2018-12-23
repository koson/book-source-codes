/* Signature computation for simple regions */


#define MAX
#include "lib.h"

#define OBJECT 1
#define BACK 0
#define CONV 3.1415926535/180.0

float cmc, cmr;

void bound (IMAGE im);
float med (float a, float b, float c);
int nay4 (IMAGE im, int I, int J);
float cm (IMAGE im, float *cmr, float *cmc);
float angle_2pt (int r1, int c1, int r2, int c2);
void signature (IMAGE im, float *sig);

void main (int argc, char *argv[])
{
	IMAGE im, im2;
	char text[128];
	int i, j, k, ii, jj;
	FILE *f;
	float sig[361];

	if (argc < 2)
	{
	  printf ("Usage: sig <image> \n");
	  exit (1);
	}

	im = Input_PBM (argv[1]);
	if (im == 0)
	{
	  printf ("The image file '%s' does not exist, or is unreadable.\n");
	  exit (2);
	}

/* Center of mass */
	cm (im, &cmr, &cmc);

/* Identify the boundary */
	bound (im);
	for (i=0; i<360; i++) sig[i] = 0.0;

	signature (im, sig);
	for (i=0; i<im->info->nr; i++)
	  for (j=0; j<im->info->nc; j++)
	    im->data[i][j] = 255;
	im->data[(int)cmr][(int)cmc] = 0;
	im->data[(int)cmr+1][(int)cmc] = 0;
	im->data[(int)cmr-1][(int)cmc] = 0;
	im->data[(int)cmr][(int)cmc+1] = 0;
	im->data[(int)cmr][(int)cmc-1] = 0;

	for (i=0; i<360; i++)
	{
	  if (sig[i] > 0.0)
	  {
	    printf ("%d %f\n", i, sig[i]);
	    ii = (int)(cmr + sig[i]*sin((double)(CONV*i)));
	    jj = (int)(cmc + sig[i]*cos((double)(CONV*i)));
	    im->data[ii][jj] = 0;
	  }
	}
	Display (im);
}


/* Delete pixels that are NOT on an object outline (boundary) */
void bound (IMAGE im)
{
	int i,j;
	IMAGE x;

	x = newimage (im->info->nr, im->info->nc);
	for (i=0; i<im->info->nr; i++)
	  for (j=0; j<im->info->nc; j++)
	    x->data[i][j] = im->data[i][j];

	for (i=0; i<x->info->nr; i++)
	  for (j=0; j<x->info->nc; j++)
	    if (x->data[i][j] == OBJECT && nay4(x, i, j) == 4)
		im->data[i][j] = BACK;

	freeimage (x);
}

int nay4 (IMAGE im, int I, int J)
{
	int n=0;

	if ( (I-1 >= 0) && im->data[I-1][J]==OBJECT) n++;
	if ( (J-1 >= 0) && im->data[I][J-1]==OBJECT) n++;
	if ( (I+1 < im->info->nr) && im->data[I+1][J]==OBJECT) n++;
	if ( (J+1 < im->info->nc) && im->data[I][J+1]==OBJECT) n++;
	return n;
}

float cm (IMAGE im, float *cmr, float *cmc)
{
	int i,j, r, c, n;

	n = r = c = 0;
	for (i=0; i<im->info->nr; i++)
	  for (j=0; j<im->info->nc; j++)
	    if (im->data[i][j] == OBJECT)
	    {
	      r += i; c += j; n++;
	    }
	*cmr = (float)r/(float)n;
	*cmc = (float)c/(float)n;
}

void signature (IMAGE im, float *sig)
{
	int i,j, k;
	float x, d, s[361];

	for (i=0; i<im->info->nr; i++)
	  for (j=0; j<im->info->nc; j++)
	    if (im->data[i][j] == OBJECT)
	    {
		x = angle_2pt (i, j, (int)(cmr), (int)(cmc));
		d = sqrt((double)((cmr-i)*(cmr-i)+(cmc-j)*(cmc-j)));
	        k = (int)(x + 0.5);
	        if (k>360) k = k-360;
		if (sig[k]< d) sig[k] = d;
	    }

	for (i=0; i<360; i++)
	  s[i] = sig[i];

	for (i=1; i<359; i++)
	  sig[i] = med(s[i-1], s[i], s[i+1]);
}


float med (float a, float b, float c)
{
	if (a >= b && a <= c) return a;
	if (a >= c && a <= b) return a;
	if (b >= a && b <= c) return b;
	if (b >= c && b <= a) return b;
	if (c >= a && c <= b) return c;
	if (c >= b && c <= a) return c;
	printf ("$$$$$$$$$$$$$$$$\n");
}

float angle_2pt (int r1, int c1, int r2, int c2)
{
	double atan(), fabs();
	float x, dr, dc, conv;

	conv = 180.0/3.1415926535;
	dr = (double)(r2-r1); dc = (double)(c2-c1);

/*	Compute the raw angle based of Drow, Dcolumn		*/
	if (dr==0 && dc == 0) x = 0.0;
	else if (dc == 0) x = 90.0;
	else {
		x = fabs(atan (dr/dc));
		x = x * conv;
	}

/*	Adjust the angle according to the quadrant		*/
	if (dr <= 0) {			/* upper 2 quadrants */
	  if (dc < 0) x = 180.0 - x;	/* Left quadrant */
	} else if (dr > 0) {		/* Lower 2 quadrants */
	  if (dc < 0) x = x + 180.0;	/* Left quadrant */
	  else x = 360.0-x;		/* Right quadrant */
	}

	return x;
}
