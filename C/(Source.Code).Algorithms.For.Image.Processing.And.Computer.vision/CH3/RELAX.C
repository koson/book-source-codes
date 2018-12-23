/* Relaxation method 1 - Rosenfeld & Kak  */

#define MAX
#include "lib.h"

void thr_relax (IMAGE im);
void minmax (IMAGE im, float *gmin, float *gmax);
float update (IMAGE im, float **p, float **q);
float R(int l1, int l2);
void assign_class (IMAGE im, float **, float **q);
float mean (IMAGE im);
void meanminmax (IMAGE im,    int r,       int c, 
		 float *mean, float *xmin, float *xmax);
void thresh (IMAGE im, float **p, float **q);
float Q(float **p, float **q, int r, int c, int class);

float **pp, **qq;

void main (int argc, char *argv[])
{
	IMAGE data;
	float percent;

	if (argc < 3)
	{
	  printf ("Usage: relax <input file> <output file> \n");
	  exit (0);
	}

	data = Input_PBM (argv[1]);
	if (data == NULL)
	{
		printf ("Bad input file '%s'\n", argv[1]);
		exit(1);
	}

	thr_relax (data);

	Output_PBM (data, argv[2]);
}

void thr_relax (IMAGE im)
{
	float res = 0.0, minres = 10000.0;
	float **p, **q;
	int iter = 0, i, j, count = 0;

/* Space allocation */
	p = f2d (im->info->nr, im->info->nc);
	q = f2d (im->info->nr, im->info->nc);
	pp = f2d (im->info->nr, im->info->nc);
	qq = f2d (im->info->nr, im->info->nc);

/* Initial assignment of pixel classes */
	assign_class (im, p, q);
	thresh (im, p, q);

/* Relaxation */
	do
	{
	  res = update (im, p, q);
	  iter += 1;
	  printf ("Iteration %d residual is %f\n", iter, res);
	  if (res < minres)
	  {
		minres = res;
		count = 1;
	  } else if (fabs(res-minres) < 0.0001)
	  {
		if (count > 2) break;
		else count++;
	  }
	  thresh (im, p, q);
	} while (iter < 100 && res > 1.0);

	thresh (im, p, q);
}

/* Threshold */
void thresh (IMAGE im, float **p, float **q)
{
	int i,j;

	for (i=0; i<im->info->nr; i++)
	  for (j=0; j<im->info->nc; j++)
	    if (p[i][j] > q[i][j]) im->data[i][j] = 0;
	     else im->data[i][j] = 255;
}

/* Mean grey level */
float mean (IMAGE im)
{
	int i, j;
	long N, sum=0;

	for (i=0; i<im->info->nr; i++)
	  for (j=0; j<im->info->nc; j++)
	    sum += im->data[i][j];
	N = (long)(im->info->nr) * (long)(im->info->nc);
	return (float)sum/(float)(N);
}

/* Min and max levels */
void minmax (IMAGE im, float *gmin, float *gmax)
{
	int i,j;

	*gmin = *gmax = im->data[0][0];
	for (i=0; i<im->info->nr; i++)
	  for (j=0; j<im->info->nc; j++)
	    if (*gmin > im->data[i][j]) *gmin = im->data[i][j];
	     else if (*gmax < im->data[i][j]) *gmax = im->data[i][j];
}

void assign_class (IMAGE im, float **p, float **q)
{
	int i,j,k,n,m;
	float ud2, y, z, u, lm2;

/* Rosenfeld & Smith class assignment */
	u = mean (im);
	minmax (im, &z, &y);
	ud2 = 2.0*(u-z);
	lm2 = 2.0*(y-u);
	printf ("Mean %f min %f max %f ud2 %f lm2 %f\n",u,z,y,ud2,lm2); 

/* P is the probability that a pixel is black */
	for (i=0; i<im->info->nr; i++)
	  for (j=0; j<im->info->nc; j++)
	    if (im->data[i][j] <= u)
	    {
		p[i][j] = 0.5 + (u-im->data[i][j])/ud2;
		q[i][j] = 1.0-p[i][j];
	    }

/* q is the probability that a pixel is white */
	    else
	    {
		q[i][j] = (0.5 + (im->data[i][j]-u)/lm2);
		p[i][j] = 1.0-q[i][j];
	    }
	return;

PARKER:
/* Parker class assignment */
	for (i=0; i<im->info->nr; i++)  /* Initially, all are 50% */
	  for (j=0; j<im->info->nc; j++)
	    p[i][j] = q[i][j] = 0.5;

	for (i=0; i<im->info->nr; i++)  /* Mean of local area */
	  for (j=0; j<im->info->nc; j++)
	  {
		meanminmax (im, i, j, &u, &z, &y);
		ud2 = 2.0*(u-z);
		lm2 = 2.0*(y-u);
		if (im->data[i][j] <= u)
		{
		  p[i][j] = 0.5 + (u-im->data[i][j])/ud2;
		  q[i][j] = 1.0-p[i][j];
		} else {
		  q[i][j] = (0.5 + (im->data[i][j]-u)/lm2);
		  p[i][j] = 1.0-q[i][j];
		}
	  }
}

void meanminmax (IMAGE im, int r, int c, float *mean, float *xmin, float *xmax)
{
	int i,j;
	long sum = 0, k=0;

	*xmin = *xmax = im->data[r][c];
	for (i=r-10; i<=r+10; i++)
	  for (j=c-10; j<=c+10; j++)
	  {
	    if (range(im, i, j) != 1) continue;
	    if (*xmin > im->data[i][j]) *xmin = im->data[i][j];
	    else if (*xmax < im->data[i][j]) *xmax = im->data[i][j];
	    sum += im->data[i][j];
	    k++;
	  }
	*mean = (float)sum/(float)(k);
}

float Q(float **p, float **q, int r, int c, int class)
{
	int i,j;
	float sum = 0.0;

	for (i=r-1; i<=r+1; i++)
	  for (j=c-1; j<=c+1; j++)
	    if (i!=r || j!=c)
	      sum += R(class, 0)*p[i][j] + R(class, 1)*q[i][j];
	return sum/8.0;
}


float update (IMAGE im, float **p, float **q)
{
	float z, num, qw, pk, qb;
	int i,j;

	for (i=1; i<im->info->nr-1; i++)
	  for (j=1; j<im->info->nc-1; j++)
	  {
	    qb = (1.0 + Q(p, q, i, j, 0));
	    qw = (1.0 + Q(p, q, i, j, 1));
	    pk = p[i][j]*qb + q[i][j]*qw;

	    if (pk == 0.0) 
	      continue;

	    pp[i][j] = p[i][j]*qb/pk;
	    qq[i][j] = q[i][j]*qw/pk;
	  }

	z = 0.0;
	for (i=1; i<im->info->nr-1; i++)
	  for (j=1; j<im->info->nc-1; j++)
	  {
		z += fabs(p[i][j]-pp[i][j]) + fabs(q[i][j]-qq[i][j]);
		p[i][j] = pp[i][j];
		q[i][j] = qq[i][j];
		qq[i][j] = pp[i][j] = 0.0;
	  }
	return z;
}

float R(int l1, int l2)
{
	if (l1 == l2) return 0.9;
	return -0.9;
}

