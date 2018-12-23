/* Otsu's method of 'grey level histograms' */

#define MAX
#include "lib.h"

float nu (float *p, int k, float ut, float vt);
float u (float *p, int k);
void thr_glh (IMAGE im);

void main (int argc, char *argv[])
{
	IMAGE data;

	if (argc < 3)
	{
	  printf ("Usage: thrglh <input file> <output file>\n");
	  exit (0);
	}

	data = Input_PBM (argv[1]);
	if (data == NULL)
	{
		printf ("Bad input file '%s'\n", argv[1]);
		exit(1);
	}

	thr_glh (data);

	Output_PBM (data, argv[2]);
}

void thr_glh (IMAGE x)
{
/*      Threshold selection using grey level histograms. SMC-9 No 1 Jan 1979
		N. Otsu                                                 */

	int i,j,k,m, h[260], t;
	float y, z, p[260];
	float ut, vt;
	long N;

	N = (long)x->info->nr * (long)x->info->nc;
	for (i=0; i<260; i++) {         /* Zero the histograms  */
		h[i] = 0;
		p[i] = 0.0;
	}

	/* Accumulate a histogram */
	for (i=0; i<x->info->nr; i++)
	   for (j=0; j<x->info->nc; j++) {
		k = x->data[i][j];
		h[k+1] += 1;
	   }

	for (i=1; i<=256; i++)          /* Normalize into a distribution */
		p[i] = (float)h[i]/(float)N;

	ut = u(p, 256);         /* Global mean */
	vt = 0.0;               /* Global Variance */
	for (i=1; i<=256; i++)
		vt += (i-ut)*(i-ut)*p[i];

	j = -1; k = -1;
	for (i=1; i<=256; i++) {
		if ((j<0) && (p[i] > 0.0)) j = i;       /* First index */
		if (p[i] > 0.0) k = i;                  /* Last index  */
	}
	z = -1.0;
	m = -1;
	for (i=j; i<=k; i++) {
		y = nu (p, i, ut, vt);          /* Compute NU */
		if (y>=z) {                     /* Is it the biggest? */
			z = y;                  /* Yes. Save value and i */
			m = i;
		}
	}

	t = m;
	printf("Threshold found is %d\n", t);

/* Threshold */
	for (i=0; i<x->info->nr; i++)
	  for (j=0; j<x->info->nc; j++)
	    if (x->data[i][j] < t)
	      x->data[i][j] = 0;
	    else
	      x->data[i][j] = 255;
}

float w (float *p, int k)
{
	int i;
	float x=0.0;

	for (i=1; i<=k; i++) x += p[i];
	return x;
}

float u (float *p, int k)
{
	int i;
	float x=0.0;

	for (i=1; i<=k; i++) x += (float)i*p[i];
	return x;
}

float nu (float *p, int k, float ut, float vt)
{
	float x, y;

	y = w(p,k);
	x = ut*y - u(p,k);
	x = x*x;
	y = y*(1.0-y);
	if (y>0) x = x/y;
	 else x = 0.0;
	return x/vt;
}

