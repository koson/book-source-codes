
/* Two dimensional wavelet transform */
/* Filter an image by frequency and orientation */

#define MAX
#include "lib.h"

float data[16] = { 0,1,2,2,4,4,2,2,0,0,2,2,0,0,0,0};

#define FORWARD 1
#define INVERSE -1

float rec[16] = {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};

void fwt_1d (float *data,  int N, int direction);
void wavelet (float *data, int N, int direction);
void Haar_2 (float *data,  int N, int direction);
void Daub_4 (float *a, int n, int isign);
void fwt_2d (IMAGE im, float **result);
void fwti_2d (float **result, IMAGE im);
float ** f2d (int i, int j);
void filt_toint ( float ** image , IMAGE output, float *h);
void setquad (float **im, int level, int q, int val);

void main(int argc, char *argv[])
{
	IMAGE im;
	float **result, xmax, xmin, xd;
	int maxr, i, j, k, pt, q;
	FILE *f;

	if (argc<2)
	{
	  printf ("2d wavelet transform - <input> \n");
	  exit (1);
	}

	printf ("Computing a 2d wavelet transform on a Daubechies basis:\n");
	im = Input_PBM (argv[1]);
	result = f2d(im->info->nr, im->info->nc);

	fwt_2d (im, result);
	
	do
	{
	  printf ("Delete which quads? ");
	  scanf ("%d", &q);
	  if (q<0 || q > 3) break;
	  printf ("Start filter at level: ");
	  scanf ("%d", &i);
	  printf ("End filter at level: \n");
	  scanf ("%d", &k);
	  for (j=i; j>=k; j--)
	      setquad (result, j, q, 0);
	} while (q>=0 && q<=3);

	fwti_2d (result, im);

	Output_PBM (im, "wtout");
	Display (im);
}

void setquad (float **im, int level, int q, int val)
{
	int rs,re,cs,ce,i,j,k;

	if (level >=7)
	{
	  rs = cs = 0; re = ce = 127;
	  k = 128;
	} else {
	  k = 1<<level;
	  if (q<2)
	  {
	    rs = 0; re = k;
	  } else {
	    rs = k; re = (1<<(level+1));
	  }
	  if (q%2)
	  {
	    cs = k; ce = (1<<(level+1));
	  } else {
	    cs = 0; ce = k;
	  }
	}
	printf ("Sub-image is (%d,%d) (%d,%d)\n", rs, re, cs, ce);

	for (i=rs; i<re; i++)
	  for (j=cs; j<ce; j++)
	    im[i][j] = (float)val;

}

void fwt_1d (float *data, int N, int direction)
{
	int thisn;

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
	int i,j,k;
	float *tmp;

/* Copy to the floating point array */
	for (i=0; i<im->info->nr; i++)
	  for (j=0; j<im->info->nc; j++)
	    result[i][j] = (float)(im->data[i][j]);

/* Transform the rows */
	for (i=0; i<im->info->nr; i++)
	  fwt_1d (result[i], im->info->nc, FORWARD);

/* Transform the columns */
	tmp = (float *)malloc (sizeof(float)*im->info->nr);
	for (j=0; j<im->info->nc; j++)
	{
	  for (i=0; i<im->info->nr; i++)
	    tmp[i] = result[i][j];
	  fwt_1d (tmp, im->info->nr, FORWARD);
	  for (i=0; i<im->info->nr; i++) 
	    result[i][j] = tmp[i];
	}
	free(tmp);
}

void fwti_2d (float **result, IMAGE im)
{
	int i,j,k;
	float *tmp;

/* Transform the rows */
	for (i=0; i<im->info->nr; i++)
	  fwt_1d (result[i], im->info->nc, INVERSE);

/* Transform the columns */
	tmp = (float *)malloc (sizeof(float)*im->info->nr);
	for (j=0; j<im->info->nc; j++)
	{
	  for (i=0; i<im->info->nr; i++)
	    tmp[i] = result[i][j];
	  fwt_1d (tmp, im->info->nr, INVERSE);
	  for (i=0; i<im->info->nr; i++) 
	    result[i][j] = tmp[i];
	}
	free (tmp);

/* Copy into the image */
	filt_toint (result, im, 0);
	return ;

	for (i=0; i<im->info->nr; i++)
	  for (j=0; j<im->info->nc; j++)
	    if (result[i][j] > 255.0) im->data[i][j] = 255;
	    else if (result[i][j] < 0) im->data[i][j] = 0;
	    else im->data[i][j] = (unsigned char)result[i][j];
}

void wavelet (float *data, int N, int direction)
{
	Daub_4 (data, N, direction);
}

void Daub_4 (float *a, int n, int isign)
{
	int i,j,k,nh,nh1;
	float c0,c1,c2,c3, wksp[1024];

	c0 = 0.4829629131445341; c1 = 0.8365163037378079;
	c2 = 0.2241438680420134; c3 = -0.1294095225512604;

	nh=n/2;
	nh1=nh+1;
	if (isign>=0) 
	{
	  i=1;
	  for (j=1; j<=n-3; j+=2)
	  {
	    wksp[i] = c0*a[j]+c1*a[j+1]+c2*a[j+2]+c3*a[j+3];
	    wksp[i+nh] = c3*a[j]-c2*a[j+1]+c1*a[j+2]-c0*a[j+3];
	    i++;
	  }
	  wksp[i] = c0*a[n-1]+c1*a[n]+c2*a[1]+c3*a[2];
	  wksp[i+nh] = c3*a[n-1]-c2*a[n]+c1*a[1]-c0*a[2];
	} else {
	  wksp[1]=c2*a[nh]+c1*a[n]+c0*a[1]+c3*a[nh1];
	  wksp[2]=c3*a[nh]-c0*a[n]+c1*a[1]-c2*a[nh1];
	  j=3;
	  for (i=1; i<=nh-1; i++)
	  {
	    wksp[j]=c2*a[i]+c1*a[i+nh]+c0*a[i+1]+c3*a[i+nh1];
	    wksp[j+1]=c3*a[i]-c0*a[i+nh]+c1*a[i+1]-c2*a[i+nh1];
	    j=j+2;
	  }
	}

	for (i=1; i<=n; i++) a[i] = wksp[i];
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

/* Convert a REAL/COMPLEX image into an integer one for display */
void filt_toint ( float ** image , IMAGE output, float *h)
{
	float xmax, xmin, x, dx,y, xdif;
	int i,j,n, ii, jj, kk,k,nn, H[256];
	IMAGE cim;

/* OUTPUT must be an allocated byte image, at least as big as IMAGE in
   each dimension. IMAGE will be converted into real from complex.      */

	n = output->info->nr;
	cim = output;
	xmax = -1.0e20; xmin = -xmax; 
	for (i=0; i<n; i++) 
	  for (j=0; j<n; j++) 
	  {
	    if (xmax < image[i][j]) xmax = image[i][j];
	    if (xmin > image[i][j]) xmin = image[i][j];
	  }

	y = xmin;
	xmax = -1.0e20; xmin = -xmax; 
	for (i=0; i<n; i++) 
	  for (j=0; j<n; j++) 
	  {
	    x = image[i][j] - y;
	    if (x > 0.0)
	      x = (float) log(sqrt ( (double) x ));     
	    else x = 0.0;
	    if (xmax < x) xmax = x;
	    if (xmin > x) xmin = x;
	    image[i][j] = x;
	  }

	if ( (xmin <= 0.00001) && (xmin >= -0.00001) ) xmin = 0.000000;
	printf ("Xmax is %12.5f   Xmin is %12.6f\n", xmax, xmin);

/* Compute the histogram H or accept it as a parameter */
	if (h==0)
	  for (i=0; i<256; i++) H[i] = (n*n)/256;
	else 

/* If h is passed, it contains % of total number of pixels. */
	  for (i=0; i<256; i++) 
		H[i] = (int) ( (h[i]/100.0)*(n*n) );

	xdif = xmax-xmin;
	if (xdif <= 0.0) printf ("******* ZERO RANGE!!\n");
	for (i=0; i<n; i++)
	  for (j=0; j<n; j++) 
	  {
	    x = image[i][j];
	    x = (x-xmin)/xdif * 255.0;
	    if (x<0.0) x = 0.0;
	     else if (x>255.0) x=255.0;
	    cim->data[i][j] = (unsigned char) x;
	  }
}
