/*	User-defined functions  for the Gaussian fit problem */

#include <math.h>
#include <stdio.h>

extern int nargs;
float datax[256], datay[256];
int NPTS = 0;

double getarg (int n, unsigned char *this);

void user_init ()
{
	int i,j, k;
	FILE *f;
	char fn[128];

	printf ("This GA fits a Gaussian to a set of data values.\n");

	printf ("The name of the data file is: ");
	scanf ("%s", fn);
	f = fopen (fn, "r");
	if (f == NULL)
	{
	  printf ("Can't open the file '%s' for input\n", fn);
	  exit (1);
	}

	for (i=0; i<256; i++)
	{
	   printf ("Coordinates for point #%d: ", i);
	   k = fscanf (f, "%f", &(datax[i])); printf ("%f ", datax[i]);
	   if (k<1) break;
	   k = fscanf (f, "%f", &(datay[i])); printf ("%f\n", datay[i]);
	   if (k<1) break;
	   j = i;
	}
	fclose (f);
	NPTS = j;
	printf ("Number of points: %d\n\n", NPTS);
}

double gauss (double a, double b, double c, double d)
{
	double x;

	x = -(d-a)*(d-a)/(b*b);
	return c*exp(x);
}
 
/* Evaluate chomosome N and store result in EVALS array */
double feval (unsigned char *bs, int n)
{
	double a, b, x[5];
	int i,j,k;

	x[0] = getarg(0, bs);
	x[1] = getarg(1, bs);
	x[2] = getarg(2, bs);

/* Compute the distance for this route */
	b = 0.0;
	for (i=0; i<NPTS; i++)
	{
	  a = gauss (x[0], x[1], x[2], datax[i]);
	  b += (a-datay[i])*(a-datay[i]);
	}
	return sqrt(b);
}
