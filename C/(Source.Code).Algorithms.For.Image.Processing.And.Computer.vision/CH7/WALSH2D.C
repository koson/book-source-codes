/* 2D Walsh transform */
#define MAX
#include "lib.h"
#include <stdio.h>
#include <malloc.h>

double *res[256];

void NEXT ()
{
	static int i = 0;
	static char xc[5] = {'|', '/', '-', '\\', ' '};

	fprintf (stdout, "%c%c", 010, xc[i++]);
	fflush (stdout);
	if (i>3) i = 0;
}

void walsh1d (double *f, int ln)
{
	double t;
	long n, nv2, le1, le, k, nm1;
	long i,j, l, ip;

	n = 1<<ln;
	nv2 = n/2;
	nm1 = n-1;
	j = 1;
	for (i=1; i<=nm1; i++)
	{
	  if (i < j) 
	  {
	    t = f[j];
	    f[j] = f[i];
	    f[i] = t;
	  }
	  k = nv2;
	  while (k<j)
	  {
	    j = j - k;
	    k = k/2;
	  }
	  j = j+k;
	}

	for (l=1; l<=ln; l++)
	{
	  le = 1<<l;
	  le1 = le/2;
	  for (j=1; j<=le1; j++)
	  {
	    for (i=j; i<=n; i+= le)
	    {
	      ip = i + le1;
	      t = f[ip];
	      f[ip] = f[i] - t;
	      f[i] = f[i] + t;
	    }
	  }
	}
	for (i=1; i<=n; i++)
	  f[i] = f[i]/(float)n;
}

void walsh2d (IMAGE x)
{
	double *data;
	long i,j, n;

	data = (double *)malloc (sizeof(double)*x->info->nc);
	n = 2; i = 1;
	while (n<=x->info->nc) { n = n+n; i++; }
	i = i-1;
	printf ("Rows: ");
	n = i;
	for (i=0; i<x->info->nr; i++)
	{ 
	  for (j=1; j<=x->info->nc; j++)
	    data[j] = (double)x->data[i][j-1];
	  walsh1d (data, n);
	  NEXT();
	  for (j=1; j<=x->info->nc; j++)
	     res[i][j-1] = data[j];
	
	}

	free(data);
	data = (double *)malloc (sizeof(double) * x->info->nr);
	n = 2; i = 1;
	while (n<=x->info->nr) { n = n+n; i++; }
	i = i - 1;
	printf ("\nColumns: ", n, i);
	n = i;
	for (j=0; j<x->info->nc; j++)
	{
	  for (i=1; i<=x->info->nc; i++)
	    data[i] = res[i-1][j];
	  walsh1d(data, n);
	  NEXT();
	  for (i=1; i<=x->info->nc; i++)
	    res[i-1][j] = data[i];
	}
	printf ("\n");
	free(data);
}

int main(int argc, char *argv[])
{
	int i,j, rs;
	FILE *f;
	IMAGE x;
	double xmin, xmax, zz;

	if (argc < 3)
	{
	  printf ("Usage: walsh2d image resultsize\n");
	  printf ("Resultsize is the number of transform entries to store.\n");
	  printf ("E.G. Image is 128x128, resultsize = 64 saves 1/4 of the\n");
	  printf ("transform entries, achieving a factor of 4 compression.\n");
	  exit (1);
	}
	x = Input_PBM (argv[1]);
	if (x == 0)
	{
	  printf ("No such file as '%s'\n", argv[1]);
	  exit(1);
	}

	if (x->info->nr != x->info->nc)
	{
	  printf ("Image MUST be square, and a power of two in size.\n");
	  exit (1);
	}

	sscanf (argv[2], "%d", &rs);
	if (rs>x->info->nc)
	{
	  printf ("Resultsize should be smaller than the image size.\n");
	  rs = x->info->nc;
	}
	
	for (i=0; i<x->info->nr; i++)
	  res[i] = (double *)malloc(sizeof(double)*x->info->nc);
	
	walsh2d(x);

	f = fopen ("walsht.dat", "w");
	fprintf (f, "%d %d\n", x->info->nc, rs);
	
	xmin = xmax = res[0][0];
	for (i=0; i<rs; i++)
	{
	  for (j=0; j<rs; j++)
	  {
	    fprintf (f, "%f ", res[i][j]);
	    if (xmax < res[i][j]) xmax = res[i][j];
	    if (xmin > res[i][j]) xmin = res[i][j];
	  }
	  fprintf (f, "\n");
	}
	fclose (f);
	printf ("Output walsh transform is in file 'walsht.dat'\n");

/* Create a display-able image */
	zz = xmax - xmin;
	for (i=0; i<x->info->nr; i++)
	  for (j=0; j<x->info->nc; j++)
	  {
	    x->data[i][j] = (unsigned char)( (res[i][j]-xmin)/zz * 255 );
	  }
	Output_PBM (x, "walsh.pgm");
	printf ("A display-able image is in file 'walsh.pgm'\n");
	return 0;
}
