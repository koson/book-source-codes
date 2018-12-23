
/*              Convert a glyph to 8x6 size for use by NNCLASS          */
#define MAX
#include "lib.h"

void scale_down (IMAGE z, int newr, int newc, IMAGE *newim);
double bigger(double x);
void bbox (IMAGE *x);                   
void normalize (IMAGE x);  

int main (int argc, char *argv[])
{
	int i,j;
	FILE *f;
	IMAGE im, newim;

	if (argc < 3)
	{
	  printf ("Usage: %s <input image> <output file>\n", argv[0]);
	  printf ("  Convert a glyph image into a neural net input file.\n");
	  printf ("  This program converts the input image to one that\n");
	  printf ("  has the size 8 rows by 6 columns.\n\n");
	  exit (1);
	}

	im = Input_PBM (argv[1]);    /* Open and read the input image file */
	if (im == 0)
	{
	  printf ("Can't acquire the image file '%s'.\n", argv[1]);
	  exit (1);
	}

	f = fopen (argv[2], "w");         /* Open the required output file */
	if (f == NULL)
	{
	  printf ("Can't open the file '%s' for output.\n", argv[2]);
	  exit (1);
	}

	normalize(im);                  /* Convert grey levels to 0 - 255 */
	bbox (&im);                           /* Reduce input to min size */
	scale_down (im, 8, 6, &newim);

	for (i=0; i<8; i++)
	{
	  for (j=0; j<6; j++)
	    fprintf (f, "%f ", (float)(newim->data[i][j])/255.0);
	  fprintf (f, "\n");
	}

	return 0;
}

void scale_down (IMAGE z, int newr, int newc, IMAGE *newim)
{
	IMAGE y, x;
	int i,j,ii,jj, k,colk,rowk, rs, cs;
	double rfactor, cfactor, accum, rw[25], cw[25], area, xf;

/* Create a new, smaller, image */
	y = newimage (newr, newc);  
	x = z;

/* Compute scale factor for rows and columns */
	rfactor = (double)newr/(double)(x->info->nr);
	cfactor = (double)newc/(double)(x->info->nc);
	rfactor = 1.0/rfactor;
	cfactor = 1.0/cfactor;
	area = cfactor*rfactor;

/* For each pixel in the new image compute a grey level 
   based on interpolation from the original image       */
	for (i=0; i<newr; i++)
	   for (j=0; j<newc; j++) 
	   {
/* Set up the row re-scale */
		rw[0] = bigger(i*rfactor) - i*rfactor;
		rs = (int) (floor(i*rfactor)+0.001);  k=1;
		xf = rfactor - rw[0];

		while (xf >= 1.0) 
		{
			rw[k++] = 1.0;
			xf = xf - 1.0;
		}
		if (xf < 0.0001) k--; 
		 else rw[k] = xf;
		rowk = k;

/* Set up the column re-scale */
		cw[0] = bigger(j*cfactor) - j*cfactor;
		cs = (int) (floor(j*cfactor)+0.001);  k=1;
		xf = cfactor - cw[0];

		while (xf >= 1.0) 
		{
			cw[k++] = 1.0;
			xf = xf - 1.0;
		}
		if (xf < 0.0001) k--; 
		 else cw[k] = xf;
		colk = k;

/* Collect and weight pixels from the original into the new pixel */
		accum = 0.0;
		for (ii=0; ii <= rowk; ii++) 
		{
		   if (ii+rs >= x->info->nr) continue;
		   for (jj=0; jj<= colk; jj++) 
		   {
		      if (jj+cs >= x->info->nc) continue;
		      accum += rw[ii]*cw[jj]*(x->data[rs+ii][cs+jj]);
		   }
		}
		accum = accum/area;
		if (accum > 255.0) printf ("%lf at (%d,%d)\n",accum,i,j);
		y->data[i][j] = (int)(accum + 0.5);
	}
	*newim = y;
}

/*      Return the next bigger integer to the number X  */
double bigger(double x)
{
	double y;

	y = ceil(x);
	if (y == x) y = y + 1.0;
	return y;
}

void bbox (IMAGE *x)
{
	int i, j, sr=10000, lr=0, sc=10000, lc=0, n, m;
	IMAGE im, y;

	im = *x;
	for (i=0; i<im->info->nr; i++)
	{
	  for (j=0; j<im->info->nc; j++)
	  {
	     if (im->data[i][j] == 0)
	     {
	       lr = i;
	       if (lc < j) lc = j;
	       if (sr > i) sr = i;
	       if (sc > j) sc = j;
	     }
	  }
	}
	
	n = lr-sr+1;    m = lc-sc+1;
	y = newimage (n, m);

	for (i=sr; i<=lr; i++)
	  for (j=sc; j<=lc; j++)
	    y->data[i-sr][j-sc] = im->data[i][j];
	*x = y;
}

void normalize (IMAGE x)
{
	int i,j,vmin=256, vmax=0, k;
	float scale = 1.0;

	for (i=0; i<x->info->nr; i++)
	  for (j=0; j<x->info->nc; j++)
	  {
	    if (x->data[i][j] < vmin) vmin = x->data[i][j];
	    if (x->data[i][j] > vmax) vmax = x->data[i][j];
	  }

	k = vmax-vmin;
	scale = (float)k;

	for (i=0; i<x->info->nr; i++)
	  for (j=0; j<x->info->nc; j++)
	    x->data[i][j] = 
		   (unsigned char)(((x->data[i][j]-vmin)/scale)*255.0);
}

