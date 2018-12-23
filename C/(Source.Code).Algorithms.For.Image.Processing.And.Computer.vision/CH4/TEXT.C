/* Code for applying a texture operator to an image in a series of
   2D windows, each centered on a pixel. The value of the center
   pixel in the output will be the median value of the texture
   operator over the entire window. A sequence of these windows,
   of gradually increasing sizes, will be created for segmentation */

#define MAX
#include "lib.h"
#include <math.h>

#define SD_SCALE 0.2
#define KURT_SCALE 100
#define SKEW_SCALE 10000.0
#define SOBATHRESH 40
#define SOBMTHRESH 20

struct wstruct {
	int top, bottom, left, right;
};
typedef struct wstruct WINDOW;

float **Mat0=0, **Mat45=0, **Mat90=0, **Mat135=0;
float param[6] = {0.0, 0.0, 0.0, 1.0, 0.0, 0.0};
int dir = 0;

void measure_window (IMAGE im, int wn, int which, float **res);
void Output_FPBM (float **x, char *filename, int nr, int nc);
void rescale (float ** res, int nr, int nc);
float **f2d (int nr, int nc);
void clri (float ** res, int nr, int nc);
void dump_algs ();
float ** which_matrix (int which);
float apply_op (IMAGE im, WINDOW *w, int size, int op);

/* Simple statistical measures on grey level */
float average_grey_level (IMAGE im, WINDOW *w, int size);
float stddev_grey_level (IMAGE im, WINDOW *w, int size);
float kurtosis_grey_level (IMAGE im, WINDOW *w, int size);
float skew_grey_level (IMAGE im, WINDOW *w, int size);

/* Grey level co-occurrence matrices */
void glcm (IMAGE im, int d, WINDOW *w);
float p_max (float **x);
float moments (float **x, int k);
float contrast (float **x);
float homo (float **x);
float entropy (float **x);

/* Vector dispersion */
float vd_window (IMAGE im, WINDOW *w, int size);
void lsbfp (IMAGE im, int row, int col, float *a, float *b, float *c);

/* Edge measures */
float txtsobel (IMAGE x, WINDOW *w, int which);
float imsobel (IMAGE x, int which, IMAGE out);
double norm (double x, double y);

/* Fractal measures */
void fit (float *x, float *y, int ndata, float *a, float *m, float *r);
float Hurst (IMAGE im, int r, int c, float *err);
int MinMax4 (int a, int b, int c, int d, int *dmax, int *dmin);
int MinMax8 (int a, int b, int c, int d, int e, int f, int g,
	 int h, int *dmax, int *dmin);
void fractal_a (IMAGE im);

/* Surface curvature */
float local_mean (IMAGE im, int r, int c);
float sc_pixel (IMAGE im, int which, int i, int j);
float sc_window (IMAGE im, WINDOW *w, int size, int which);

/* Texture energy */
int filterarg (char *s);
void make_filter (int filt[5][5], int f1, int f2);
float te_window (IMAGE im, WINDOW *w, int size);
void vvm (int m[5][5], int v1[5], int v2[5]);
float te_pixel (int mask[5][5], IMAGE im, int r, int c);

IMAGE sim;
int TE_MASK[5][5];
int TE_NORM = 1.0;

/* Segment an image using text. Use the specified method */

void main(int argc, char **argv)
{
	int i,j,n,m, ws;
	IMAGE im;
	float **res;
	int alg = 0;
	char filename[64];
   
/* Command line args - file name */
	if (argc < 2)
	{
	  printf ("USAGE: texture <imagefile> <method>\n");
	  exit (1);
	}
	im = Input_PBM (argv[1]);
	if (im == 0)
	{
	  printf ("Can't read input image from '%s'.\n", argv[1]);
	  exit (2);
	}

	if (argc < 3)
	{
	  printf ("Using average grey level as a texture measure.\n");
	  alg = 0;
	} else {
	  if (strcmp(argv[2], "grey_level") == 0) alg = 0;
	  else if (strcmp(argv[2], "sd_level") == 0) alg = 1;
	  else if (strcmp(argv[2], "kurtosis") == 0) alg = 2;
	  else if (strcmp(argv[2], "skewness")==0) alg = 3;
	  else if (strcmp(argv[2], "glcm")==0) alg = 4;
	  else if (strcmp(argv[2], "pmax")==0) alg = 5;
	  else if (strcmp(argv[2], "moments")==0) alg = 6;
	  else if (strcmp(argv[2], "contrast")==0) alg = 7;
	  else if (strcmp(argv[2], "homo")==0) alg = 8;
	  else if (strcmp(argv[2], "entropy")==0) alg = 9;
	  else if (strcmp(argv[2], "vd") == 0) alg = 10;
	  else if (strcmp(argv[2], "dx") == 0) alg = 11;
	  else if (strcmp(argv[2], "dy") == 0) alg = 12;
	  else if (strcmp(argv[2], "nx") == 0) alg = 13;
	  else if (strcmp(argv[2], "ny") == 0) alg = 14;
	  else if (strcmp(argv[2], "sang") == 0) alg = 15;
	  else if (strcmp(argv[2], "sobel") == 0) alg = 16;
	  else if (strcmp(argv[2], "fractal")==0) alg = 17;
	  else if (strcmp(argv[2], "k1")==0) alg = 18;
	  else if (strcmp(argv[2], "k2")==0) alg = 19;
	  else if (strcmp(argv[2], "k3")==0) alg = 20;
	  else if (strcmp(argv[2], "k4")==0) alg = 21;
	  else if (strcmp(argv[2], "k5")==0) alg = 22;
	  else if (strcmp(argv[2], "k6")==0) alg = 23;
	  else if (strcmp(argv[2], "elliptic")==0) alg = 24;
	  else if (strcmp(argv[2], "parabolic")==0) alg = 25;
	  else if (strcmp(argv[2], "saddle")==0) alg = 26;
	  else if (filterarg(argv[2])) alg = 27;
	  else 
	  {
		printf ("No such algorithm as: '%s'.\n", argv[2]);
		dump_algs();
		exit (1);
	  }
	}

/* Read the remaining parameters from the command line */
	for (i=3; i<argc; i++)
	  sscanf (argv[i], "%f", &(param[i]));

/* Any other images needed? */
	if (alg == 15 || alg == 16)
	  sim = newimage (im->info->nr, im->info->nc);
	else sim = 0;

/* Apply the metric */
	res = f2d (im->info->nr, im->info->nc);
	ws = 6;
	for (ws=6; ws <=24; ws += 4)
	{
	  clri (res, im->info->nr, im->info->nc);
	  measure_window (im, ws, alg, res);
	  rescale (res, im->info->nr, im->info->nc);
	  sprintf (filename, "txt%d.pgm", ws);
	  Output_FPBM (res, filename, im->info->nr, im->info->nc);
	  printf ("Output file is '%s'\n", filename);
	}
}

void rescale (float ** res, int nr, int nc)
{
	int i,j;
	float xmin, xmax, xd;

	xmin = xmax = res[0][0];
	for (i=0; i<nr; i++)
	  for (j=0; j<nc; j++)
	  {
	    if (res[i][j] > xmax) xmax = res[i][j];
	    if (res[i][j] < xmin) xmin = res[i][j];
	  }

	xd = xmax - xmin;

	for (i=0; i<nr; i++)
	  for (j=0; j<nc; j++)
	    res[i][j] = ( (res[i][j]-xmin)/xd ) * 255;
}

void clri (float ** res, int nr, int nc)
{
	int i,j;
	float xmin, xmax, xd;

	for (i=0; i<nr; i++)
	  for (j=0; j<nc; j++)
	    res[i][j] = 0;
}

void measure_window (IMAGE im, int wn, int which, float **res)
{
	int top, bottom, left, right;
	int i, j, size;
	WINDOW w;
	float z;

	top = wn;
	bottom = im->info->nr-wn-1;
	left = wn;
	right = im->info->nc-wn-1;

	size = 2*wn + 1;
	printf ("Operator %d at size %dx%d ...\n", which, size, size);

	for (i=top; i<=bottom; i++)
	{
	  for (j=left; j<=right; j++)
	  {
	    w.top  = i-wn; w.bottom = i+wn;
	    w.left = j-wn; w.right  = j+wn;
	    z = apply_op (im, &w, size, which);
	    res[i][j] =  z;
	  }
	}
}

void dump_algs ()
{
	printf ("Possible algorithms include:\n");
	printf ("grey_level		- mean grey level over a window.\n");
	printf ("sd_level		- std dev  grey level over a window.\n");
	printf ("skewness		- 3rd moment\n");
	printf ("kurtosis		- 4th moment\n");
	printf ("glcm			- co-occurrence matrices\n");
	printf ("pmax			- max probability\n");
	printf ("moments		- kth moment\n");
	printf ("contrast		- expected value of delta\n");
	printf ("homo			- homogeneity measure\n");
	printf ("entropy		- information content\n");
	printf ("vd			- vector dispersion\n");
	printf ("dx			- Edge direction (X)\n");
	printf ("dy			- Edge direction (Y)\n");
	printf ("nx			- Edge direction (X)\n");
	printf ("ny			- Edge direction (Y)\n");
	printf ("sang			- Edge direction glcm\n");
	printf ("sobel			- Edge glcm\n");
	printf ("fractal		- Fractal dimension \n");
	printf ("k1			- Curvature measure\n");
	printf ("k2			- Curvature measure\n");
	printf ("k3			- Curvature measure\n");
	printf ("k4			- Curvature measure\n");
	printf ("k5			- Curvature measure\n");
	printf ("elliptic		- Curvature measure\n");
	printf ("parabolic		- Curvature measure\n");
	printf ("saddle			- Curvature measure\n");
}

float ** which_matrix (int which)
{
	switch (which)
	{
case 0:		return Mat0;
case 1:		return Mat45;
case 2:		return Mat90;
case 3:		return Mat135;
default:	printf ("Bad matrix specifed in WHICH: %d\n");
		return Mat0;
	}
}

float apply_op (IMAGE im, WINDOW *w, int size, int op)
{
	int i,j,k,n,m;
	float ** matrix;
	IMAGE save;

	if (op >= 4 && op <=9)
	{
	  glcm (im, (int)param[3], w);
	  matrix = which_matrix ((int)param[4]);
	}
	else if (op == 15)
	{
	  imsobel (im, 1, sim);
	  op = 5; param[4] = 0;
	  glcm (sim, 1, w);
	  matrix = which_matrix ((int)0);
	  save = im; im = sim;
	} else if (op == 16)
	{
	  imsobel (im, 2, sim);
	  op = 5; param[4] = 0;
	  glcm (sim, 1, w);
	  matrix = which_matrix ((int)0);
	  save = im; im = sim;
	}

	switch (op)
	{
case 0:		return average_grey_level (im, w, size);

case 1:		return stddev_grey_level (im, w, size)*SD_SCALE;

case 2:		return kurtosis_grey_level (im, w, size)*KURT_SCALE + 128;

case 3:		return skew_grey_level (im, w, size)*SKEW_SCALE+128;

case 4:		printf ("Binary Co-occurrence matrices:\n");
		printf ("     Horizontal (0)		Vertical (90)\n");
		printf ("   %6.4f      %6.4f	     %6.4f      %6.4f\n",
			Mat0[0][0], Mat0[0][1], Mat90[0][0], Mat90[0][1]);
		printf ("   %6.4f      %6.4f	     %6.4f      %6.4f\n",
			Mat0[1][0], Mat0[1][1], Mat90[1][0], Mat90[1][1]);
		printf ("\n\n	45 diagonal             135 diagonal \n");
		printf ("   %6.4f      %6.4f	     %6.4f      %6.4f\n",
			Mat45[0][0], Mat45[0][1], Mat135[0][0], Mat135[0][1]);
		printf ("   %6.4f      %6.4f	     %6.4f      %6.4f\n",
			Mat45[1][0], Mat45[1][1], Mat135[1][0], Mat135[1][1]);
		exit (0);

case 5:		return p_max (matrix);

case 6:		return moments (matrix, (int)param[5]);

case 7:		return contrast (matrix);

case 8:		return homo (matrix);

case 9:		return entropy (matrix);

case 10:	return vd_window (im, w, size);

case 11:	return txtsobel (im, w, 1);
case 12:	return txtsobel (im, w, 2);
case 13:	return txtsobel (im, w, 3);
case 14:	return txtsobel (im, w, 4);
case 17:	fractal_a (im);

case 18:
case 19:
case 20:
case 21:
case 22:
case 23:
case 24:
case 25:
case 26:
		return sc_window (im, w, size, op-17);

case 27:	return te_window (im, w, size);

default:	printf ("No such operator implemented: %d\n", op);
		exit(1);
	}
}

/* Compute 4 grey level spatial dependence matrices for 4 major directions */

void glcm (IMAGE im, int d, WINDOW *w)
{
	int ngl, p1, p2, i, j;
	int levels[256];
	int SC0, SC45, SC90, SC135, x, y;

/* Assume that there are 256 grey levels */
	ngl = 256;

/* Allocate the matrix */
	if (Mat0 == 0 && param[4] == 0.0)
	{
		Mat0   = f2d (ngl, ngl);
		bzero (Mat0[0], sizeof (float)*256*256);
	}
	else if (Mat45 == 0 && param[4] == 1.0)
	{
		Mat45  = f2d (ngl, ngl);
		bzero (Mat45[0], sizeof (float)*256*256);
		dir = 1;
	}
	else if (Mat90 == 0 && param[4] == 2.0)
	{
		Mat90  = f2d (ngl, ngl);
		bzero (Mat90[0], sizeof (float)*256*256);
		dir = 2;
	}
	else if (Mat135 == 0 && param[4] == 3.0)
	{
		Mat135 = f2d (ngl, ngl);
		bzero (Mat135[0], sizeof (float)*256*256);
		dir = 3;
	}

/* Initialize */
	SC0 = SC45 = SC90 = SC135 = 0;

/* Compute the GLCM for any of 4 directions */
	for (i = w->top; i < w->bottom; i++)
	  for (j = w->left; j < w->right; j++)
	  {
	    p1 = (int)im->data[i][j];

	    if (j+d < w->right && dir == 0)		/* Horizontal */
	    {
		p2 = (int)im->data[i][j + d];
		Mat0[p1][p2]++;
		Mat0[p2][p1]++;
		SC0++;
	    }

	    if (i+d < w->bottom && dir == 2)		/* Vertical */
	    {
		p2 = (int)im->data[i+d][j];
		Mat90[p1][p2]++;
		Mat90[p2][p1]++;
		SC90++;
	    }

	    if (i+d < w->bottom && j-d >= w->top && dir == 1)	/* 45 degree diagonal */
	    {
		p2 = (int)im->data[i+d][j-d];
		Mat45[p1][p2]++;
		Mat45[p2][p1]++;
		SC45++;
	    }

	    if (i+d < w->bottom && 
		j+d < w->right && dir == 3)		/*135 degree diagonal */
	    {
		p2 = (int)im->data[i+d][j+d];
		Mat135[p1][p2]++;
		Mat135[p2][p1]++;
		SC135++;
	    }
	  }

/* Normalize */
	for (i=0; i<ngl; i++)
	  for (j=0; j<ngl; j++)
	  {
	    if (SC0 != 0 && dir == 0)   Mat0[i][j] /= SC0+SC0;
	    if (SC45 != 0 && dir == 1)  Mat45[i][j] /= SC45+SC45;
	    if (SC90 != 0 && dir == 2)  Mat90[i][j] /= SC90+SC90;
	    if (SC135 != 0 && dir == 3) Mat135[i][j] /= SC135+SC135;
	  }
}

float average_grey_level (IMAGE im, WINDOW *w, int size)
{
	float x;
	int i,j,k,n;

	x = 0.0; n = 0;
	for (i=w->top; i<=w->bottom; i++)
	  for (j=w->left; j<=w->right; j++)
	  {
	    x += (float)(im->data[i][j]);
	    n++;
	  }

	if (n>0)
	  return x/(float)n;
	else return 0.0;
}

float stddev_grey_level (IMAGE im, WINDOW *w, int size)
{
	float x, xsq, y;
	int i,j,k,n;

	x = 0.0; n = 0; y = 0.0; xsq = 0.0;
	for (i=w->top; i<=w->bottom; i++)
	  for (j=w->left; j<=w->right; j++)
	  {
	    y = (float)(im->data[i][j]);
	    x += y;
	    xsq += (y*y);
	    n++;
	  }

	if (n>0)
	{
	  y = (xsq - x*x/(float)n) / (float)(n-1);
	  x =  (float)sqrt((double)y);
	  return x;
	}
	else return 0.0;
}

float kurtosis_grey_level (IMAGE im, WINDOW *w, int size)
{
	float x, y, mean, sigma;
	int i,j,k,n;

	mean = average_grey_level (im, w, size);
	sigma = stddev_grey_level (im, w, size);
	if (sigma < 0.01) return 0.0;

	x = 0.0; n = 0; y = 0.0; 
	for (i=w->top; i<=w->bottom; i++)
	  for (j=w->left; j<=w->right; j++)
	  {
	    y = ((float)(im->data[i][j]) - mean)/sigma;
	    x += y*y*y;
	    n++;
	  }

	if (n>0)
	{
	  y = x/(float)n;
	  return y;
	}
	else return 0.0;
}

float skew_grey_level (IMAGE im, WINDOW *w, int size)
{
	float x, y, mean, sigma;
	int i,j,k,n;

	x = 0.0; n = 0; y = 0.0; 
	mean = average_grey_level (im, w, size);
	sigma = stddev_grey_level (im, w, size);
	if (sigma < 0.01) return 0.0;

	for (i=w->top; i<=w->bottom; i++)
	  for (j=w->left; j<=w->right; j++)
	  {
	    y = ((float)(im->data[i][j]) - mean)/sigma;
	    x += y*y*y*y;
	    n++;
	  }

	if (n>0)
	{
	  y = y/(float)n;
	  return y;
	}
	else return 0.0;
}

float p_max (float **x)
{
	int i,j;
	float y;

	y = x[0][0];
	for (i=0; i<256; i++)
	  for (j=i+1; j<256; j++)
	    if (x[i][j] > y) y = x[i][j];
	return y;
}

float moments (float **x, int k)
{
	int i,j;
	float y=0.0, z;

	for (i=0; i<256; i++)
	  for (j=i+1; j<256; j++)
	  {
	    if (k>0)
	    {
	      z = (float)pow ((double)(i-j), (double)k);
	    } else {
		if (i == j) continue;
		z = (float)pow ((double)(i-j), -k);
		z = x[i][j]/z;
	    }
	    y += z * x[i][j];
	  }
	return y;
}

float contrast (float **x)
{
	int i,j,k;
	float y=0.0;

	for (i=0; i<256; i++)
	  for (j=i+1; j<256; j++)
	    y += abs(i-j) * x[i][j];
	return y;
}

float homo (float **x)
{
	int i,j,k;
	float y = 0.0;

	for (i=0; i<256; i++)
	  for (j=i+1; j<256; j++)
	    y += x[i][j]/(1.0+abs(i-j));
	return y;
}

float entropy (float **x)
{
	int i,j;
	float y;

	y = 0.0;
	for (i=0; i<256; i++)
	  for (j=i+1; j<256; j++)
	    if (x[i][j] > 0.0)
	      y += x[i][j] * log(x[i][j]);
	return -y;
}

void Output_FPBM (float **x, char *filename, int nr, int nc)
{
	int i,j,k;
	FILE *f;

	f = fopen (filename, "w");
	if (f)
	{
	  k = 0;
	  fprintf (f, "P2\n%d %d\n255\n", nc, nr);
	  for (i=0; i<nr; i++)
	  {
	    for (j=0; j<nc; j++)
	    {
	      fprintf (f, "%3d ", (int)(x[i][j]));
	      k++;
	      if (k>15)
	      {
		k = 0;
		fprintf (f, "\n");
	      }
	    }
	  }
	}
	fprintf (f, "\n");
	fclose (f);
}

/* Vector dispersion - fit planar surface to each region, use normals	*/

float vd_window (IMAGE im, WINDOW *w, int size)
{
	int i,j, n=0;
	float x=0, a=0, b=0, c=0, sa=0, sb=0, sc=0;
	float r2=0, r;

/* Compute the surface normal for each 3x3 region in the window */
	for (i=w->top+1; i<w->bottom; i += 3)
	  for (j=w->left+1; j<w->right; j+=3)
	  {
	    lsbfp (im, i, j, &a, &b, &c);

/* Normalize and average */
	    x = (float)sqrt((double)(a*a + b*b + 1));
	    sa += a/x;
	    sb += b/x;
	    sc += (-1.0)/x;
	    n++;
	  }

	r2 = sa*sa + sb*sb + sc*sc;
	r = (float)sqrt ((double)r2);

/* Compute the descriptor Kappa */
	x = (float)(n - 1)/(float)(n-r);
	return x;
}

/* Least squares best fit plane to a small sub-image */
 
void lsbfp (IMAGE im, int row, int col, float *a, float *b, float *c)
{
        int i,j, I, J, k;
        int g = 0, alp=0, bet=0, r2=0, c2=0;
 
        for (i=row-1; i<=row+1; i++)
          for (j=col-1; j<=col+1; j++)
           {
            k = (int)im->data[i][j];
            I = i-row; J = j-col;
            g += k;
            alp += I*k;
            r2 += I*I;
            bet += J*k;
            c2 += J*J;
          }
        *c = (float)g/9.0;
        *b = (float)bet/(float)c2;
        *a = (float)alp/(float)r2;
}

/*	Surface curvature - use quadratic forms rather than planes	*/

float local_mean (IMAGE im, int r, int c)
{
	int i,j,k=0;
	float sum=0.0;

	for (i=r-1; i<=r+1; i++)
	  for (j=c-1; j<=c+1; j++)
	    if (range(im,i,j))
	    {
	      sum += im->data[i][j];
	      k++;
	    }
	return sum/(float)k;
}


float sc_pixel (IMAGE im, int which, int i, int j)
{
	float z1,z2,z3,z4,z5,z6,z7,z8,z9;	/* Pixels in 3x3 region */
	float a00,a01,a10,a20,a02,a11;		/* Parameters to the surface */
	float A1,A2,A3,A4,A5,A6;		/* Least squares varaiables  */
	float k1,k2,k3,k4,k5,k6;		/* Curvature metrics */
	float E, F, G, DISC, SQRTDISC, e, f, g, disc;
	int elliptic, parabolic, saddle;

/* Compute the parameters to the equation of the surface 
	z1 = im->data[i-1][j-1];	z2 = im->data[i-1][j];
	z3 = im->data[i-1][j+1];	z4 = im->data[i][j-1];
	z5 = im->data[i][j];		z6 = im->data[i][j+1];
	z7 = im->data[i+1][j-1];	z8 = im->data[i+1][j];
	z9 = im->data[i+1][j+1];
*/
	z1 = local_mean (im, i-2, j-2);
	z2 = local_mean (im, i-2, j);
	z3 = local_mean (im, i-2, j+2);
	z4 = local_mean (im, i, j-2);
	z5 = local_mean (im, i, j);
	z6 = local_mean (im, i, j+2);
	z7 = local_mean (im, i+2, j-2);
	z8 = local_mean (im, i+2, j);
	z9 = local_mean (im, i+2, j+2);

	A1 = z1+z2+z3+z4+z5+z6+z7+z8+z9;
	A2 = -z1+z3-z4+z6-z7+z9;
	A3 = z1+z2+z3 - z7-z8-z9;
	A4 = z1+z3+z4+z6+z7+z9;
	A5 = z1+z2+z3+z7+z8+z9;
	A6 = -z1+z3+z7-z9;

	a00 = 5.0*A1/9.0 - A4/3.0 -A5/3.0;
	a10 = A2/6.0;
	a01 = A3/6.0;
	a20 = A4/2.0 - A1/3.0;
	a02 = A5/2.0 - A1/3.0;
	a11 = A6/4.0;

/* Derivative values from the equation of the surface */
	E = 1 + a10*a10;
	F = a10*a01;
	G = 1 + a01*a01;
	DISC = E*G - F*F;
	SQRTDISC = (float)sqrt((double)DISC);
	e = 2.0*a20/SQRTDISC;
	f = a11/SQRTDISC;
	g = 2.0*a02/SQRTDISC;
	disc = e*g - f*f;

/* Compute the curvature values at this point */

/* K1 is minimum curvature */
	k1 = (g*E+G*e-2.0*F*f)*(g*E+G*e-2.0*F*f) - 4.0*disc*DISC;
	k1 = (float)sqrt((double)k1);
	k1 = (g*E - 2.0*F*f + G*e - k1)/(2.0*DISC);

/* K2 is Maximum curvature */
	k2 = (g*E+G*e-2.0*f*F)*(g*E+G*e-2.0*f*F) - 4.0*disc*DISC;
	k2 = (float)sqrt((double)k2);
	k2 = (g*E-2.0*F*f+G*e+k2)/(2.0*DISC);

	switch (which)
	{

case 1:
/* K1 is minimum curvature */
	return k1;

case 2:
/* K2 is Maximum curvature */
	return k2;

case 3:
/* Total curvature */
	k3 = k1*k2;
	return k3;

case 4:
/* Mean curvature */
	k4 = (k1+k2)/2.0;
	return k4;

case 7:
/* Elliptic point */
	elliptic = disc > 0.0;
	return elliptic;

case 8:
/* Parabolic point */
	parabolic = fabs(disc) < 0.01;
	return parabolic;

case 9:
/* Saddle point */
	saddle = disc < 0;
	return (float)saddle;

case 5:
/* New metric #1 */
	k5 = (k2-k1)/2.0;
	return k5;

case 6:
/* New metric #2 */
	k6 = fabs(k1);
	if (fabs(k2) > k6) k6 = fabs(k2);
	return k6;

default:
		printf ("No such curvature metric as #%d\n", which);
		exit(2);
	}
}

/* Calculate surface curvature metrics over a window. Will simply be
   the 3x3 region at the center for k1-k5, but will be the total number
   of specified points for the remainder.				*/

float sc_window (IMAGE im, WINDOW *w, int size, int which)
{
	int i,j,n;

	if (which <=6)		/* Curvature metric */
	{
	  i = (w->top+w->bottom)/2; j = (w->left+w->right)/2;
	  return sc_pixel (im, which, i, j);
	}

	n = 0;
	for (i=w->top+1; i<w->bottom; i += 3)
	  for (j=w->left+1; j<w->right; j+=3)
	    if (sc_pixel (im, which, i, j) != 0.0) n++;
	return (float)n;
}


/*		Edge related procedures. Basically use Sobel edges	*/

float txtsobel (IMAGE x, WINDOW *w, int which)
{
	int i=0,j=0,n=0,m=0,k=0, ix, iy;
	float dx=0, dy=0;

	for (i=w->top+1; i<w->bottom; i++)
	  for (j=w->left+1; j<w->right; j++) 
	  {
	    ix = (x->data[i-1][j+1]+2*x->data[i][j+1]+x->data[i+1][j+1]) -
	        (x->data[i-1][j-1]+2*x->data[i][j-1]+x->data[i+1][j-1]);
	    iy = (x->data[i+1][j-1]+2*x->data[i+1][j]+x->data[i+1][j+1])-
	        (x->data[i-1][j-1]+2*x->data[i-1][j]+x->data[i-1][j+1]);
	    dx += ix; 
	    dy += iy;
	    if (ix > iy) n++;
	    else if (iy > ix) m++;
	    k++;
	  }

	if (which == 1) return dx/k;
	else if (which == 2) return dy/k;
	else if (which == 3) return (float)n/(float)k;
	else if (which == 4) return (float)m/(float)k;
}

float imsobel (IMAGE x, int which, IMAGE out)
{
	int i=0,j=0,n=0,m=0,k=0;
	float dx=0, dy=0;
	double ix, iy, ang;

	for (i=1; i<x->info->nr-1; i++)
	  for (j=1; j<x->info->nc-1; j++) 
	  {
	    ix = (x->data[i-1][j+1]+2*x->data[i][j+1]+x->data[i+1][j+1]) -
	        (x->data[i-1][j-1]+2*x->data[i][j-1]+x->data[i+1][j-1]);
	    iy = (x->data[i+1][j-1]+2*x->data[i+1][j]+x->data[i+1][j+1])-
	        (x->data[i-1][j-1]+2*x->data[i-1][j]+x->data[i-1][j+1]);
	    if (which == 1)
	    {
	      if (norm(ix, iy) < SOBATHRESH) out->data[i][j] = 0;
	      else {
	        if (ix == 0 && iy == 0) ang = 0;
	        else ang = atan2 (iy, ix) * 40;
	        out->data[i][j] = (unsigned char)ang;
	      }
	    } else if (which == 2)
	    {
		ang = norm(ix, iy);
	        if (ang > 255) printf ("Overflow\n");
	        out->data[i][j] = (unsigned char)ang;
	    }
	  }
	Output_PBM (out, "sobtxt.pgm");
	exit(1);
}

double norm (double x, double y)
{
        return  sqrt ( (x*x + y*y) );
}


/*	Procedures for estimating the fractal dimension		*/

void fractal_a (IMAGE im)
{
	int i,j,k,n,m;
	float x;
	IMAGE res;

	float maxx= 10, err;
	int mi, mj;

	res = newimage (im->info->nr, im->info->nc);
	for (i=3; i<im->info->nr-3; i++)
	  for (j=3; j<im->info->nc-3; j++)
	  {
	    x= Hurst (im, i, j, &err);

	    if (err >= 100 ) {maxx = err; mi = i; mj = j;  goto L1; }

	    res->data[i][j] = (unsigned char) (x*64) ;
	    if (x < 0) printf ("NEGATIVE!!\n");
	    if (x*64 > 255) printf ("Overflow %f\n", x);
	  }
	Output_PBM (res, "fractal.pgm");

L1:
	printf ("MIN Error is %f at (%d, %d)\n", maxx, mi, mj);
	for (i=mi-3; i<=mi+3; i++)
	{
	  for (j= mj-3; j<=mj+3; j++)
	    printf ("%3d ", im->data[i][j]);
	  printf ("\n");
	}

	exit(0);
}

/* Fractal measures */

float Hurst (IMAGE im, int r, int c, float *herr)
{
	float data[10] = {0,0,0,0,0,0,0,0,0,0};
	static float y[8] = {0.0, 0.34657359, 0.69314718, 0.80471896,
			1.0397208, 1.0986123, 1.1512925, 0.0};
	float cept, slope, err;
	int a, b, i, j;

	a = b = (int)im->data[r][c+1];
	MinMax4 ((int)im->data[r][c+1], (int)im->data[r][c-1], 
		 (int)im->data[r-1][c], (int)im->data[r+1][c], &a, &b);
	if (a-b) data[0] = (float)log((double)(a - b));

	MinMax4 ((int)im->data[r-1][c-1], (int)im->data[r-1][c+1],
		 (int)im->data[r+1][c-1], (int)im->data[r+1][c+1], &a, &b);
	if (a-b) data[1] = (float)log((double)(a - b));

	MinMax4 ((int)im->data[r-2][c], (int)im->data[r+2][c],
		 (int)im->data[r][c-2], (int)im->data[r][c+2], &a, &b);
	if (a-b) data[2] = (float)log((double)(a - b));

	MinMax8 ((int)im->data[r-1][c+2], (int)im->data[r-1][c-2],
		 (int)im->data[r+1][c+2], (int)im->data[r+1][c-2],
		 (int)im->data[r-2][c+1], (int)im->data[r-2][c-1],
		 (int)im->data[r+2][c+1], (int)im->data[r+2][c-1], &a, &b);
	if (a-b) data[3] = (float)log((double)(a - b));

	MinMax4 ((int)im->data[r+1][c-2], (int)im->data[r+2][c+2],
		 (int)im->data[r-2][c-2], (int)im->data[r-2][c+2], &a, &b);
	if (a-b) data[4] = (float)log((double)(a - b));

	MinMax4 ((int)im->data[r+3][c], (int)im->data[r-3][c],
		 (int)im->data[r][c-3], (int)im->data[r][c+3], &a, &b);
	if (a-b) data[5] = (float)log((double)(a - b));

	MinMax8 ((int)im->data[r+3][c-1], (int)im->data[r+3][c+1],
		 (int)im->data[r-3][c+1], (int)im->data[r-3][c-1],
		 (int)im->data[r-1][c+3], (int)im->data[r-1][c-3],
		 (int)im->data[r+1][c+3], (int)im->data[r+1][c-3], &a, &b);
	if (a-b) data[6] = (float)log((double)(a - b));

/* Now find the LSBF line to log(dG) and log(distance)	*/

	fit (y, data, 7, &cept, &slope, &err);

	err = 0;
	for (i=1; i<7; i++)
	  if (data[i]>data[i-1]) err+=1;
	if (err >=6) {
	for (i=0; i<7; i++)
	  printf ("%d\n", data[i]);
		*herr = 100;
	}

	return slope;

}

int MinMax4 (int a, int b, int c, int d, int *dmax, int *dmin)
{
	int x[4], i;

	x[0] = a; x[1] = b; x[2] = c; x[3] = d;
	for (i=0; i<=3; i++)
	{
	  if (x[i] > *dmax) *dmax = x[i];
	  if (x[i] < *dmin) *dmin = x[i];
	}
}

int MinMax8 (int a, int b, int c, int d, int e, int f, int g, int h, int *dmax, int *dmin)
{
	int x[8], i;

	x[0] = a; x[1] = b; x[2] = c; x[3] = d;
	x[4] = e; x[5] = f; x[6] = g; x[7] = h;

	for (i=0; i<=7; i++)
	{
	  if (x[i] > *dmax) *dmax = x[i];
	  if (x[i] < *dmin) *dmin = x[i];
	}
}

void fit (float *x, float *y, int ndata, float *a, float *m, float *r)
{
	float s1=0.0, s2=0.0, s3=0.0, xbar = 0.0, ybar = 0.0;
	int i;

	for (i=0; i<ndata; i++)
	{
	  xbar += x[i];
	  ybar += y[i];
	}
	xbar /= (float)ndata;
	ybar /= (float)ndata;

	for (i=0; i<ndata; i++)
	{
	  s1 = (x[i] - xbar);
	  s2 += s1*(y[i]-ybar);
	  s3 += s1*s1;
	}
	*m = s2/s3;
	*a = ybar - *m*xbar;

/* Compute error estimate */
	s1 = 0.0;
	for (i=0; i<ndata; i++)
	  s1 += (y[i] - *m * x[i] - *a) * (y[i] - *m * x[i] - *a);
	*r = (float)sqrt((double)s1);

}

/* Texture energy procedures (Laws) */

/* Return TRUE if the arg is a string of the form {ELR}5{ELR}5. EG E5R5. */

int filterarg (char *s)
{
	int f1, f2, i, j;

	if (s[1] != '5' || s[3] != '5') return 0;
	if (s[0] != 'E' && s[0] != 'R' && s[0] != 'L') return 0;
	if (s[2] != 'E' && s[2] != 'R' && s[2] != 'L') return 0;

/* Make the global filter mask for this filter */
	if (s[0] == 'E') f1 = 1;
	 else if (s[0] == 'L') f1 = 2;
	 else f1 = 3;
	if (s[2] == 'E') f2 = 1;
	 else if (s[2] == 'L') f2 = 2;
	 else f2 = 3;

	make_filter (TE_MASK, f1, f2);
	for (i=0; i<5; i++)
	  for (j=0; j<5; j++)
	    TE_NORM += abs(TE_MASK[i][j]);
	TE_NORM -= 1;

	return 1;
}

void make_filter (int filt[5][5], int f1, int f2)
{
	static int E5[5] = {-1, -2,  0,  2,  1};
	static int L5[5] = { 1,  4,  6,  4,  1};
	static int R5[5] = { 1, -4,  6, -4,  1};
	int *F1, *F2;

	if (f1 == 1) F1 = E5;
	 else if (f1 == 2) F1 = L5;
	 else if (f1 == 3) F1 = R5;
	 else {
		printf ("MAKE_FILTER: bad filter f1 = %d\n", f1);
		exit (1);
	 }

	if (f2 == 1) F2 = E5;
	 else if (f2 == 2) F2 = L5;
	 else if (f2 == 3) F2 = R5;
	 else {
		printf ("MAKE_FILTER: bad filter f2 = %d\n", f2);
		exit (1);
	 }

	vvm (filt, F1, F2);
}

/* Multiply two vectors to give a matrix */

void vvm (int m[5][5], int v1[5], int v2[5])
{
	int i,j;

	for (i=0; i<5; i++)
	  for (j=0; j<5; j++)
	    m[i][j] = v1[i]*v2[j];
}

/* Convolve image with filter mask for the specified pixel */

float te_pixel (int mask[5][5], IMAGE im, int r, int c)
{
	int i,j;
	float sum = 0.0;

	for (i=r-2; i<=r+2; i++)
	  for (j=c-2; j<=c+2; j++)
	   if (range(im,i,j))
	    sum += (float)((int)im->data[i][j] * mask[i-r+2][j-c+2]);
	return sum;
}

/* Compute energy for the given window */

float te_window (IMAGE im, WINDOW *w, int size)
{
	int i,j, ii, jj, N[3][3] = {0,0,0,0,0,0,0,0,0};
	float sda[3][3] = {0,0,0,0,0,0,0,0,0};
	float mna[3][3] = {0,0,0,0,0,0,0,0,0}, result, sd;
	int val=0, sum=0;

/* Compute the texture energy for this window */
	for (i=w->top; i<=w->bottom; i++)
	{
	  if (i<(w->top+w->bottom)/2) ii = 0; 
	   else if (i>(w->top+w->bottom)/2) ii = 1;
	   else ii = 2;
	  for (j=w->left; j<=w->right; j++) 
	  {
	    if (j<(w->left+w->right)/2) jj = 0;
	     else if(j>(w->left+w->right)/2) jj = 1;
	     else jj = 2;
	    val = te_pixel (TE_MASK, im, i, j)/TE_NORM;
	    sda[ii][jj] += val*val;
	    mna[ii][jj] += abs(val);
	    N[ii][jj] += 1;
	    sum += abs(val);
	  }
	}
	return sum;

/* Compute stats for each of the 4 regions about the center */
	sd =  1.0e21; result = 0.0;
	for (i=0; i<=1; i++)
	  for (j=0; j<=1; j++)
	  {
	    if (N[i][j] > 0) mna[i][j] /= N[i][j];
	    else mna[i][j] = 1.0e21;
	    if (N[i][j] > 1) sda[i][j] = (sda[i][j]-mna[i][j])/(N[i][j]-1);
	     else sda[i][j] = 1.0e21;
	    if (sda[i][j] < sd)
	    {
		sd = sda[i][j];
		result = mna[i][j];
	    }
	  }

/* Return the mean for the region with smallest standard deviation */
	return result;
}
