/* Inverse 2D Walsh transform */

#define MAX
#include "lib.h"
#include <stdio.h>
#include <malloc.h>

double res[128][128];

int logn (int n);
void walsh1d (double *data, int n);
void swap (double *a, double *b);
void walsh2d (int N);

void NEXT ()
{
	static int i = 0;
	static char xc[5] = {'|', '/', '-', '\\', ' '};

	fprintf (stdout, "%c%c", 010, xc[i++]);
	fflush (stdout);
	if (i>3) i = 0;
}


void main(int argc, char *argv[])
{
	int i,j, n, rs;
	FILE *f;
	IMAGE x;
	double xmin, xmax, zz;

	printf ("Inverse WALSH transform.\n");
	if (argc <= 1)
	{
		printf ("Missing file name: ");
		printf ("Assuming 'walsht.dat'\n");      
		f = fopen ("walsht.dat", "r");
	} else f = fopen (argv[1], "r"); 
	
	if (f == NULL)
	{
		printf ("Can't get the input file.\n");
		exit (0);
	}

/* READ raw data file */
	fscanf (f, "%d", &n);
	fscanf (f, "%d", &rs);
	printf ("Size: %3d  Coeffs: %3d\n", n, rs);
	for (i=0; i<n; i++)
	  for (j=0; j<n; j++)
	    res[i][j] = 0.0;

	for (i=0; i<rs; i++)
	  for (j=0; j<rs; j++)
	    fscanf (f, "%lf", &(res[i][j]));
	fclose (f);

/* Transform */
	walsh2d(n);

	xmin = xmax = res[0][0];
	for (i=0; i<n; i++)
	{
	  for (j=0; j<n; j++)
	  {
	    if (xmax < res[i][j]) xmax = res[i][j];
	    if (xmin > res[i][j]) xmin = res[i][j];
	  }
	}

	zz = xmax - xmin;
	x = newimage (n, n);
	for (i=0; i<n; i++)
	  for (j=0; j<n; j++)
	    x->data[i][j] = (unsigned char)( (res[i][j]-xmin)/zz * 255 );
	Output_PBM (x, "unwalsh.pgm");
	printf ("Result is 'unwalsh.pgm'\n");
}

int logn (int n)
{
	int i,j;

	j = 1; i = 0;
	while (j<n)
	{
	  j = j << 1;
	  i++;
	}
	if (j!=n)
	{
		printf ("NOT A POWER OF 2!\n");
		exit (1);
	}
	return i;
}

void swap (double *a, double *b)
{
	double tmp;

	tmp = *a; *a = *b; *b = tmp;
}

void walsh1d (double *f, int n)
{
	int i, j=0, k, k1, k2, ptwo;
	double tmp;

	for (i=0; i<n-1; i++)
	{
	  if (i < j) 
	    swap (&(f[i]), &(f[j]));

	  k = n/2;
	  while (k<j+1)
	  {
	    j -= k;
	    k /= 2;
	  }
	  j += k;
	}

	ptwo = logn(n);
	for (k=1; k<=ptwo; k++)
	{
	  k2 = 1<<k;
	  k1 = 1<<(k-1);
	  for (j=0; j<k1; j++)
	  {
	    for (i=j; i<n; i+= k2)
	    {
	      tmp = f[i+k1];
	      f[i+k1] = f[i] - tmp;
	      f[i] = f[i] + tmp;
	    }
	  }
	}

	for (i=0; i<n; i++)
	  f[i] = f[i]/(float)n;
}

void walsh2d (int N)
{
	double *data;
	int i,j;

	printf ("Transform ...\n");
	data = (double *)malloc (sizeof(double)*N);
	printf ("Rows:  ");
	for (i=0; i<N; i++)
	{
	  for (j=0; j<N; j++)
	    data[j] = res[i][j];
	  walsh1d (data, N);
	  for (j=0; j<N; j++)
	    res[i][j] = data[j]*N;
	  NEXT ();
	}

	printf ("\nCols:  ");
	for (j=0; j<N; j++)
	{
	  for (i=0; i<N; i++)
	    data[i] = res[i][j];
	  walsh1d(data, N);
	  for (i=0; i<N; i++)
	    res[i][j] = data[i]*N;
	  NEXT();
	}
	printf ("\n");
	free(data);
}

