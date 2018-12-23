
/* Two dimensional wavelet transform */

#include <malloc.h>
#include <math.h>
#define MAX
#include "lib.h"

#define FORWARD 1
#define INVERSE -1

float *wksp;

void fwt_1d (float *data,  int N, int direction);
void wavelet (float *data, int N, int direction);
void Haar_2 (float *data,  int N, int direction);
void Daub_4 (float *a, int n, int isign);
void fwt_2d (IMAGE im, float **result);
void fwti_2d (float **result, IMAGE im);
float ** f2d (int i, int j);

void NEXT (int k)
{
	static int i = 0;
	static char xc[5] = {'|', '/', '-', '\\', ' '};

	fprintf (stdout, "%c%c%c%3d", 010, 010, 010, k);
	fflush (stdout);
	if (i>3) i = 0;
}

void main(int argc, char *argv[])
{
	IMAGE im;
	float **result, xmax=0.0, xmin=0.0, xd=0.0, t=0.0;
	long maxr=0, i=0, j=0;
	long k=0;
	FILE *f;

	if (argc<3)
	{
	  printf ("2d wavelet transform - <input> <output>\n");
	  printf ("File of raw wavelet coefficients saved in 'wtout'\n");
	  exit (1);
	}

	printf ("Computing a 2d wavelet transform on a Daubechies basis:\n");
	im = Input_PBM (argv[1]);
	if (im == 0)
	{
		printf ("Cannot create input image!\n");
		exit (1);
	}
	result = f2d(im->info->nr, im->info->nc);
	wksp = (float *)malloc(sizeof(float)*1025);
	if (wksp == 0)
	{
	  printf ("Out of storage!\n");
	  return;
	}

	printf ("Forward transform ---\n");
	fwt_2d (im, result);

	printf ("Enter threshold:\n");
	scanf ("%f", &t);

/* Remove coefficients less than T */
	k = 0;
	printf ("Thresholding: row -   ");            
	for (i=0; i<im->info->nr; i++)
	{  
	  NEXT(i);
	  for (j=0; j<im->info->nc; j++)
	   if (result[i][j] < t)
	   {
	     result[i][j] = 0.0;
	     k++;
	   }
	}

	printf ("Deleted %d coefficients.\n", k);

/* back-transform */
	fwti_2d (result, im);

	Output_PBM (im, argv[2]);
	printf ("Output file is %s\n", argv[2]);
	return;
}

void fwt_1d (float *data, int N, int direction)
{
	int thisn=0;

	if (N > 4)
	{
	  if (direction >= 0)
	  {
	    thisn = N;
	    while (thisn >= 4)
	    {
	      wavelet (data, thisn, direction);
	      thisn /= 2;
	    }
	  } else if (direction == INVERSE) 
	  {
	    thisn = 4;
	    while (thisn <= N)
	    {
	      wavelet (data, thisn, direction);
	      thisn *= 2;
	    }
	  }
	}
}

/* Image is preferably square; size of each dimension is a power of two */
void fwt_2d (IMAGE im, float **result)
{
	int i,j,k=0;
	float *tmp;

/* Copy to the floating point array */
	for (i=0; i<im->info->nr; i++)
	  for (j=0; j<im->info->nc; j++)
	    result[i][j] = (float)(im->data[i][j]);

/* Transform the rows */
	tmp = (float *)malloc (sizeof(float)*(int)(im->info->nr));   
	if (tmp == 0)
	{
	  printf ("Out of storage in FWT.\n");
	  exit (1);
	}
	printf ("\nRows: ");
	for (i=0; i<im->info->nr; i++)
	{
	  fwt_1d (result[i], (int)(im->info->nc), FORWARD);
	  NEXT(i);
	}

/* Transform the columns */
	printf ("\nColumns: ");
	for (j=0; j<im->info->nc; j++)
	{
	  for (i=0; i<im->info->nr; i++)
	    tmp[i] = result[i][j];
	  fwt_1d (tmp, (int)(im->info->nr), FORWARD);
	  for (i=0; i<im->info->nr; i++) 
	    result[i][j] = tmp[i];
	  NEXT(j);
	}
	printf ("\n");
	free(tmp);
}

void fwti_2d (float **result, IMAGE im)
{
	int i,j,k=0;
	float *tmp, imax=0.0, imin=0.0;

/* Transform the rows */
	printf ("\nInverse ---\nRows: ");
	for (i=0; i<im->info->nr; i++)
	{  
	  fwt_1d (result[i], (int)(im->info->nc), INVERSE);
	  NEXT(i);
	}

/* Transform the columns */
	tmp = (float *)malloc (sizeof(float)*(int)(im->info->nr));
	printf ("\nColumns: ");
	for (j=0; j<im->info->nc; j++)
	{
	  for (i=0; i<im->info->nr; i++)
	    tmp[i] = result[i][j];
	  fwt_1d (tmp, (int)(im->info->nr), INVERSE);
	  for (i=0; i<im->info->nr; i++) 
	    result[i][j] = tmp[i];
	  NEXT (j);
	}
	free (tmp);
	printf ("\n");

/* Copy into the image */
	imax = imin = im->data[3][3];
	for (i=0; i<im->info->nr; i++)
	  for (j=0; j<im->info->nc; j++)
	  {
	    if (im->data[i][j] > imax) imax = im->data[i][j];
	    if (im->data[i][j] < imin) imin = im->data[i][j];
	  }
	if (imax == imin) { printf ("Phase error ...\n"); exit (0); }
	printf ("Minimum is %f maximum is %f\n", imin, imax);

	printf ("Converting into pixels again.\n");
	for (i=0; i<im->info->nr; i++)
	{  
	  NEXT(i);
	  for (j=0; j<im->info->nc; j++)
/*            im->data[i][j] = (unsigned char)
	       (((result[i][j]-imin)/(imax-imin))*255.0);
	}
	printf ("\n");       */

	    if (result[i][j] > 255.0) im->data[i][j] = (unsigned char)255;
	    else if (result[i][j] < 0.0) im->data[i][j] = (unsigned char)0;
	    else im->data[i][j] = (unsigned char)(result[i][j]);    
	    NEXT(i);      
	}    

}

void wavelet (float *data, int N, int direction)
{
	Daub_4 (data, N, direction);
}

void Daub_4 (float *a, int n, int isign)
{
	int i,j,k,nh,nh1;
	double c0,c1,c2,c3;

	c0 = 0.4829629131445341; c1 = 0.8365163037378079;
	c2 = 0.2241438680420134; c3 = -0.1294095225512604;

	nh=n/2;
	nh1=nh+1;
	if (isign>=0) 
	{
	  i=1;
	  for (j=1; j<=n-3; j+=2)
	  {
	    wksp[i]    = (float)(c0*a[j]+c1*a[j+1]+c2*a[j+2]+c3*a[j+3]);
	    wksp[i+nh] = (float)(c3*a[j]-c2*a[j+1]+c1*a[j+2]-c0*a[j+3]);
	    i++;
	  }
	  wksp[i]    = (float)(c0*a[n-1]+c1*a[n]+c2*a[1]+c3*a[2]);
	  wksp[i+nh] = (float)(c3*a[n-1]-c2*a[n]+c1*a[1]-c0*a[2]);
	} else {
	  wksp[1] = (float)(c2*a[nh]+c1*a[n]+c0*a[1]+c3*a[nh1]);
	  wksp[2] = (float)(c3*a[nh]-c0*a[n]+c1*a[1]-c2*a[nh1]);
	  j=3;
	  for (i=1; i<=nh-1; i++)
	  {
	    wksp[j]  = (float)(c2*a[i]+c1*a[i+nh]+c0*a[i+1]+c3*a[i+nh1]);
	    wksp[j+1]= (float)(c3*a[i]-c0*a[i+nh]+c1*a[i+1]-c2*a[i+nh1]);
	    j=j+2;
	  }
	}

	for (i=0; i<n; i++) a[i] = wksp[i];
}

void Haar_2 (float *data, int N, int direction)
{
	int i,j,nover2;
	float h0, h1, *tmp;

/*      h0 = 1.0/sqrt(2.0); h1 = 1.0/sqrt(2.0); */
	h0 = h1 = 0.5;
	tmp = (float *)malloc (sizeof(float)*N);

	nover2 = N/2;
	if (direction == FORWARD) 
	{
	  i=0;
	  for (j=0; j<N-1; j+=2)
	  {
	    tmp[i]        = h0*data[j] + h1*data[j+1];
	    tmp[i+nover2] = h0*data[j] - h1*data[j+1];
	    i++;
	  }
	} else if (direction == INVERSE) 
	{

/* Comment the next line out for symmetric scaling */
	  h1 = h0 = 1.0;
	  i = j = 0;
	  do 
	  {
	    tmp[j]   = h0*data[i] + h1*data[i+nover2];
	    tmp[j+1] = h0*data[i] - h1*data[i+nover2];
	    j += 2; i++;
	  } while (i <= nover2);
	}

	for (i=0; i<N; i++)
	  data[i] = tmp[i];
	free(tmp);
}

