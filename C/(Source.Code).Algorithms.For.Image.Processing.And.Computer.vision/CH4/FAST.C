/* FAST code for applying a texture operator to an image in a series of
   2D windows, each centered on a pixel. The value of the center
   pixel in the output will be the descriptor value of the texture
   operator over the entire window. A sequence of these windows,
   of gradually increasing sizes, will be created for segmentation */

#define MAX
#include "lib.h"

#define SD_SCALE 0.2
#define KURT_SCALE 100
#define SKEW_SCALE 10000.0

struct wstruct {
	int top, bottom, left, right;
};
typedef struct wstruct WINDOW;

float **Mat0=0, **Mat45=0, **Mat90=0, **Mat135=0;
float param[6] = {0.0, 0.0, 0.0, 1.0, 0.0, 0.0};
int dir = 0;
float *Sum, *Diff, Mean;

void measure_window (IMAGE im, int wn, int which, float **res);
void Output_FPBM (float **x, char *filename, int nr, int nc);
void rescale (float ** res, int nr, int nc);
float apply_op (IMAGE im, WINDOW *w, int size, int op);
void glcm (IMAGE im, int d, WINDOW *w);
float average ();
float stddev ();
float **f2d (int nr, int nc);
void dump_algs ();
float p_max ();
float energy ();
float contrast ();
float homo ();
float entropy ();

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
	if (strcmp(argv[2], "average") == 0) alg = 0;
	  else if (strcmp(argv[2], "stddev")==0) alg = 1;
	  else if (strcmp(argv[2], "pmax")==0) alg = 5;
	  else if (strcmp(argv[2], "energy")==0) alg = 6;
	  else if (strcmp(argv[2], "contrast")==0) alg = 7;
	  else if (strcmp(argv[2], "homo")==0) alg = 8;
	  else if (strcmp(argv[2], "entropy")==0) alg = 9;
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

	res = f2d (im->info->nr, im->info->nc);
	ws = 6;

/* Apply the metric */
	measure_window (im, ws, alg, res);

	rescale (res, im->info->nr, im->info->nc);
	sprintf (filename, "txt.pgm", ws);
	Output_FPBM (res, filename, im->info->nr, im->info->nc);
	printf ("Output file is '%s'\n", filename);
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
	  printf ("Finished row %d\n", i);
	}
}

void dump_algs ()
{
	printf ("Possible algorithms include:\n");
	printf ("average		- mean\n");
	printf ("stddev			- standard deviation\n");
	printf ("pmax			- max probability\n");
	printf ("energy			- energy\n");
	printf ("contrast		- expected value of delta\n");
	printf ("homo			- homogeneity measure\n");
	printf ("entropy		- information content\n");
}

float apply_op (IMAGE im, WINDOW *w, int size, int op)
{
	int i,j,k,n,m;

	glcm (im, (int)param[3], w);
	switch (op)
	{

case 0:		return average ();
case 1:		return stddev ();
case 5:		return p_max ();
case 6:		return energy ();
case 7:		return contrast ();
case 8:		return homo ();
case 9:		return entropy ();

default:	printf ("No such operator implemented: %d\n", op);
		exit(1);
	}
}

/* Compute the sum and difference histograms, and the mean */

void glcm (IMAGE im, int d, WINDOW *w)
{
	int ngl, p1, p2, i, j, k;
	static float *Ps, *Pd;
	float sum;

/* Assume that there are 256 grey levels */
	ngl = 256;

/* Allocate the matrix */
	if (Ps == 0)
	{
	  Ps = (float *)calloc (ngl*2, sizeof(float));
	  Pd = (float *)calloc (ngl*2, sizeof(float));
	  Sum = Ps;
	  Diff = Pd;
	}

	dir = (int)param[4];

/* Compute the histograms for any of 4 directions */
	k = 0;
	for (i = w->top; i < w->bottom; i++)
	  for (j = w->left; j < w->right; j++)
	  {
	    p1 = im->data[i][j];

	    if (j+d < w->right && dir == 0)		/* Horizontal */
		p2 = im->data[i][j + d];
	    else if (i+d < w->bottom && dir == 2)		/* Vertical */
		p2 = im->data[i+d][j];
	    else
	      if (i+d < w->bottom && j-d >= w->left && dir == 1)	/* 45 degree diagonal */
		p2 = im->data[i+d][j-d];
	    else if (i+d < w->bottom && 
		j+d < w->right && dir == 3)		/*135 degree diagonal */
		p2 = im->data[i+d][j+d];
	    else continue;
	    k++;
	    Ps[p1+p2]++;
	    Pd[p1-p2+ngl]++;
	  }

/* Normalize */
	sum = 0.0;
	for (i=0; i<ngl+ngl; i++)
	{
	  Ps[i] /= k;
	  sum += Ps[i]*i;
	  Pd[i] /= k;
	}
	Mean = sum/2.0;
}

float average ()
{
	return Mean;
}

float stddev ()
{
	float s1, s2, meanmean, var;
	int i,j;

	s1 = s2 = 0.0;
	meanmean = Mean+Mean;

	for (i=0; i<512; i++)
	{
	  s1 += (i-meanmean)*(i-meanmean)*Sum[i];
	  j = i-255;
	  s2 += j*j*Diff[i];
	}
	var = (s1 + s2)/2.0;
	return (float)sqrt ((double)var);
}

float p_max ()
{
	int i;
	float y;

	y = 0.0;
	for (i=0; i<512; i++)
	    if (Sum[i] > y) y = Sum[i];
	return y;
}

float energy ()
{
	int i;
	float y=0.0, z = 0.0;

	for (i=0; i<512; i++)
	{
	  y += Sum[i]*Sum[i];
	  z += Diff[i]*Diff[i];
	}
	return y*z;
}

float contrast ()
{
	int i,j,k;
	float y=0.0;

	for (i= -255; i<= 255; i++)
	  y += i*i*Diff[i+255];
	return y;
}

float homo ()
{
	int i,j,k;
	float y = 0.0;

	for (i= -255; i<255; i++)
	{
	  y += 1.0/(1.0 + i*i) * Diff[i+255];
	}
	return y;
}

float entropy ()
{
	int i,j;
	float y, h1=0.0, h2=0.0;

	y = 0.0;
	for (i=0; i<512; i++)
	  if (Sum[i] > 0)
	    h1 += Sum[i]*log((double)Sum[i]);
	for (i=-255; i<= 255; i++)
	  if (Diff[i+255] > 0)
	    h2 += Diff[i+255]*log((double)Diff[i+255]);
	y = -h1 - h2;

	return y;
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
