/* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

	Handprinted digit recognition using contour features

   - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*/

#include <stdio.h>
#include <malloc.h>
#define MAX 1
#include "lib.h"

/* Set DEBUG to 1 for massive amounts of output */
#define DEBUG 0
#define BACKGROUND 255

int votes[10];
int nholes = 0;
int database[1000][50];
int dbv[1000];
int DBSIZE = 0;

float lrpeak (float *data, int lmin, int lmax);
int lrmax (float *data, int lmin, int lmax);
int lrmin (float *data, int lmin, int lmax);
void outdata (float *ldata, int n);
void outprof (float *ldata, float *rdata, int n);
void rescale (float *data, int n);
int recnum(struct image * x);
void ext_glyph (struct image *x, struct image **y);
int search_db(int *bits);
int bdiff (int *a, int *b);
void get_database ();

/* ALPHA Library routines */

void freeimage (struct image  *z);
void an_error (int ecode);
int area_loc (struct image *x, int val);
void box(struct image *x, int val, float *x1, float *y1);
void center_of_mass (struct image *x, int val, float *ii, 
                float *jj);
int distance_8 (int i, int j, int n, int m);
float distance_e (int i, int j, int n, int m);
void delete (struct image *x, int value);
void extract (struct image *x, struct image **y, int val, 
              int *rm, int *cm);
void fill (struct image *y, int i, int j, int val);
void hole_metrics(struct image *x, int v, int *hn, 
             float *hp, float *ha);
void hswap (int *rows, int *columns, int i, int j);
void mark4 (struct image *x, int value, int iseed, int jseed);
void mark8 (struct image *x, int value, int iseed, int jseed);
int nay4 (struct image *x, int i, int j, int val);
int nay8 (struct image *x, int i, int j, int val);
float perimeter (struct image *x, int val);
void principal_axis(struct image *x, int val, float *i1, float *j1, 
                    float *i2, float *j2);
int range (struct image *x, int n, int m);
void region_4 (struct image *x, int value);
int region_8 (struct image *x, int value);
void remark (struct image *x, int v1, int v2);
void background (struct image *x, int t);
void thresh_is (struct image *x, int *t);
void threshold (struct image *x, int t);


int recnum(struct image * x)
{
	struct image *y, *z;
	int i,j,ii,jj,k,n, c[256], na, ma;
	char fn[128];
	float i1,i2,j1,j2, x1,x2,x3;
	float at2(), xx1[12], yy1[12];
	double d1[2][4], d2[2][4];
	int oldnr, oldnc;


/* Extract the glyph, pre-process into Y */
	ext_glyph (x, &y);

/* Count the significant holes */
	hole_metrics(y, 0, &nholes, &i1,&i2);
	if (DEBUG)
	 printf (" Holes: %d of them, area %f perimeter %f\n", nholes, i2, i1);
	if (nholes>0)
		if (i2/nholes < 6) nholes = 0;

/* Remove blank rows and columns */
	extract (y, &x, 0, &ii, &jj);
	z = 0;
	copy (&z, x);

/* The EXTRACT has a blank row and column on each side - remove this */
	for (i=0; i<x->info->nr-1; i++)
	  for (j=0; j<x->info->nc-1; j++) 
		x->data[i][j] = x->data[i+1][j+1];
	x->info->nc -= 2;	x->info->nr -= 2;

/* Now try to recognize the digit image */
	numeral (x, &ii);
	printf ("THE NUMBER WAS %d  (method 1) \n\n\n", ii);
	return ii;
}


/*	Attempt to recognize handprinted numerals. This approach is based
	on the use of left and right profiles, and calculates 48 different
	feature values, which are Boolean. It then classifies the image
	based on a collection of Boolean expressions using the features	  */

numeral (struct image * x, int *num)
{
	float *lprof, *rprof, *dl, *dr, *width;
	int i,j,nr, wi;
	int lmax,lmin,rmax,rmin;
	int a1,a2,a3,a4,a5,a6,a7,a8,a9,a10,a11,a12,a13,a14,a15,a16,a17;
	int b1,b2,b3,b4,b5,b6,b7,b8,b9,b10,b11,c1,c2,c3,d1,d2;
	int e1,e2,e3,e4,e5,e6,f1,f2,f3,g1,g2,g3,g4,h,ii;
	float ratio, wmax, lrpeak(), w1, w2, w3,k;
	int bits[50];

	*num = -1;
	wmax = -1;
	if (x->info->nr < 52) nr = 52; else nr = x->info->nr;
	lprof = (float *)malloc ((nr+1)*sizeof(float));
	rprof = (float *)malloc ((nr+1)*sizeof(float));
	dl = (float *)malloc ((nr+1)*sizeof(float));
	dr = (float *)malloc ((nr+1)*sizeof(float));
	width = (float *)malloc ((x->info->nr+1)*sizeof(float));

	nr = x->info->nr;
	for (i=0; i<x->info->nr; i++) {

/* Compute the left and right profiles */
	   j = 0;
	   while (j<x->info->nc && x->data[i][j]>0) j++;
	   lprof[i] = j;
	   j = x->info->nc-1;
	   while (j>0 && x->data[i][j] > 0) j--;
	   rprof[i] = j;
	   if (DEBUG) printf (" %d: L%f R%f\n", i, lprof[i], rprof[i]);

/* May as well compute width, too */
	   width[i] = rprof[i]-lprof[i];
	   if (width[i] > wmax) { wmax = width[i]; wi = i; }

/* First differences of the profiles */
	   if (i==0)
	      dl[i] = 0;
	   else
	      dl[i] = lprof[i]-lprof[i-1];
	   if (i==0)
	      dr[i] = 0;
	   else
	      dr[i] = rprof[i]-rprof[i-1];
	}

/* Scale: features are based on a standard size of 50 pixels */
	rescale (lprof, nr);
	rescale (rprof, nr);
	rescale (dl, nr);
	rescale (dr, nr);


	for (i=50; i>0; i--) {
	   lprof[i] = lprof[i-1];
	   rprof[i] = rprof[i-1];
	   dl[i] = dl[i-1];
	   dr[i] = dr[i-1];
	   width[i] = width[i-1];
	}

	if (DEBUG) {
		printf ("Scaled profiles: \n");	
		outprof (lprof, rprof, 50);
		printf ("Scaled profiles: dLeft\n");	
		outdata (dl, 50);
		printf ("Scaled profiles: dRight\n");	
		outdata (dr, 50);	
	}

/* Find the width everywhere */
	j = 1;
	for (i=1; i<=50; i++) {
	   width[i] = rprof[i]-lprof[i];
	   if (width[i] > width[j]) j = i;
	}
	wmax = width[j]; wi = j;
	ratio = (float)(nr)/(float)wmax;

/* The numeral 1 is often easy */
	if (ratio > 2.5) {
	   *num = 1;
	   if(DEBUG)  printf ("Probably a '1'\n");
	}


/*	At this point we have:
	LPROF - The left profile (distance from left hand margin, in pixels)
		scaled to be from 0..49 (50 rows).
	RPROF - The right profile (distance from left hand margin, in pixels)
		scaled to be from 0..49 (50 rows).
	DL    - First difference in the left profile. DL[k]=LPROF[k]-LPROF[k-1].
	DR    - First difference in the right profile. DR[k]=RPROF[k]-RPROF[k-1].
*/

/* The A and B classes of features are based on the value
   of the peaks in the first difference arrays.		  */

	if ((k=lrpeak(dl,  2, 50)) < 10) a1  = 1; else a1 = 0; bits[0] = a1;
	if (DEBUG) printf ("%3f ", k);
	if ((k=lrpeak(dl,  2, 10)) <  5) a2  = 1; else a2 = 0; bits[1] = a2;
	if (DEBUG) printf ("%3f ", k);
	if ((k=lrpeak(dl,  2, 15)) >  5) a3  = 1; else a3 = 0; bits[2] = a3;
	if (DEBUG) printf ("%3f ", k);
	if ((k=lrpeak(dl,  2, 15)) > 10) a4  = 1; else a4 = 0; bits[3] = a4;
	if (DEBUG) printf ("%3f ", k);
	if ((k=lrpeak(dl,  2, 20)) > 10) a5  = 1; else a5 = 0; bits[4] = a5;
	if (DEBUG) printf ("%3f ", k);
	if ((k=lrpeak(dl,  2, 25)) >  5) a6  = 1; else a6 = 0; bits[5] = a6;
	if (DEBUG) printf ("%3f ", k);
	if ((k=lrpeak(dl,  5, 15)) >  5) a7  = 1; else a7 = 0; bits[6] = a7;
	if (DEBUG) printf ("%3f ", k);
	if ((k=lrpeak(dl,  5, 35)) >  5) a8  = 1; else a8 = 0; bits[7] = a8;
	if (DEBUG) printf ("%3f\n", k);
	if ((k=lrpeak(dl,  5, 40)) > 10) a9  = 1; else a9 = 0; bits[8] = a9;
	if (DEBUG) printf ("%3f ", k);
	if ((k=lrpeak(dl, 10, 30)) > 10) a10 = 1; else a10 = 0; bits[9] = a10;
	if (DEBUG) printf ("%3f ", k);
	if ((k=lrpeak(dl, 15, 40)) > 10) a11 = 1; else a11 = 0; bits[10] = a11;
	if (DEBUG) printf ("%3f ", k);
	if ((k=lrpeak(dl, 25, 50)) <  5) a12 = 1; else a12 = 0; bits[11] = a12;
	if (DEBUG) printf ("%3f ", k);
	if ((k=lrpeak(dl, 30, 50)) > 10) a13 = 1; else a13 = 0; bits[12] = a13;
	if (DEBUG) printf ("%3f ", k);
	if ((k=lrpeak(dl, 30, 50)) <  5) a14 = 1; else a14 = 0; bits[13] = a14;
	if (DEBUG) printf ("%3f ", k);
	if ((k=lrpeak(dl, 35, 50)) <  5) a15 = 1; else a15 = 0; bits[14] = a15;
	if (DEBUG) printf ("%3f ", k);
	if ((k=lrpeak(dl, 35, 50)) > 10) a16 = 1; else a16 = 0; bits[15] = a16;
	if (DEBUG) printf ("%3f ", k);
	if ((k=lrpeak(dl, 40, 50)) >  5) a17 = 1; else a17 = 0; bits[16] = a17;
	if (DEBUG) printf ("%3f\n ", k);

	if ((k=lrpeak(dr,  2, 50)) > 10) b1  = 1; else b1  = 0; bits[17] = b1;
	if (DEBUG) printf ("%3f ", k);
	if ((k=lrpeak(dr,  2, 15)) > 10) b2  = 1; else b2  = 0; bits[18] = b2;
	if (DEBUG) printf ("%3f ", k);
	if ((k=lrpeak(dr,  2, 30)) < 10) b3  = 1; else b3  = 0; bits[19] = b3;
	if (DEBUG) printf ("%3f ", k);
	if ((k=lrpeak(dr,  2, 45)) <  5) b4  = 1; else b4  = 0; bits[20] = b4;
	if (DEBUG) printf ("%3f ", k);
	if ((k=lrpeak(dr, 25, 45)) < 10) b5  = 1; else b5  = 0; bits[21] = b5;
	if (DEBUG) printf ("%3f ", k);
	if ((k=lrpeak(dr, 25, 50)) > 10) b6  = 1; else b6  = 0; bits[22] = b6;
	if (DEBUG) printf ("%3f ", k);
	if ((k=lrpeak(dr, 25, 50)) <  5) b7  = 1; else b7  = 0; bits[23] = b7;
	if (DEBUG) printf ("%3f ", k);
	if ((k=lrpeak(dr, 30, 50)) > 10) b8  = 1; else b8  = 0; bits[24] = b8;
	if (DEBUG) printf ("%3f\n", k);
	if ((k=lrpeak(dr, 35, 50)) >  5) b9  = 1; else b9  = 0; bits[25] = b9;
	if (DEBUG) printf ("%3f ", k);
	if ((k=lrpeak(dr, 35, 50)) > 10) b10 = 1; else b10 = 0; bits[26] = b10;
	if (DEBUG) printf ("%3f ", k);
	if ((k=lrpeak(dr, 40, 50)) >  5) b11 = 1; else b11 = 0; bits[27] = b11;
	if (DEBUG) printf ("%3f\n ", k);

/* The C features require that minima be found on each side
   of the minimum of a profile in a given range.            */
	lmin = lrmin (lprof,  1, 50);
	lmax = lrmax (lprof,  1, 50);
	rmin = lrmin (rprof,  1, 50);
	rmax = lrmax (rprof,  1, 50);

	if ( (lrmin(rprof, 1, 30) < lrmax(rprof, rmin, 30)) &&
	     (lrmin(rprof, 1, 30) > lrmax(rprof, 1, rmin) ) )
		c1 = 1; else c1 = 0;
	if ( (lrmin(rprof, 10, 40) < lrmax(rprof, rmin, 40)) &&
	     (lrmin(rprof, 10, 40) > lrmax(rprof,  1, rmin)) )
		c2 = 1; else c2 = 0;
	if ( (lrmin(rprof, 10, 45) < lrmax(rprof, rmin, 45)) &&
	     (lrmin(rprof, 10, 45) > lrmax(rprof,  1, rmin)) )
		c3 = 1; else c3 = 0;
	bits[28] = c1; bits[29] = c2; bits[30] = c3;

/* The D features use right profile properties */
	if (rprof[lrmin(rprof, 5,25)] == rprof[lrmax(rprof,1,rmin)]) 
		d1 = 1; else d1 = 0;
	if (rprof[lrmin(rprof, 5,25)] == rprof[lrmax(rprof,rmin,40)])
		d2 = 1; else d2 = 0;
	bits[31] = d1; bits[32] = d2;

/* The E features use simple left profile properties  */
	if (lrmax(lprof, 1, 30) < lrmin(lprof, 1, lmax)) e1 = 1; else e1 = 0;
	if (lrmax(lprof, 10, 30) < lrmin(lprof, 10, lmax)) e2 = 1; else e2 = 0;
	if (lrmax(lprof, 10, 30) < lrmin(lprof, 10, lmax)) e3 = 1; else e3 = 0;
	if (lrmax(lprof, 15, 45) < lrmin(lprof, 15, 45)) e4 = 1; else e4 = 0;
	if (lrmax(lprof, 20, 50) < lrmin(lprof, 20, 50)) e5 = 1; else e5 = 0;
	if (lrmax(lprof, 40, 50) < lrmin(lprof, 40, 50)) e6 = 1; else e6 = 0;
	bits[33] = e1; bits[34] = e2; bits[35] = e3; 
	bits[36] = e4; bits[37] = e5; bits[38] = e6;

/* The F features use simple right profile properties */
	if (lrmin(rprof, 1, 30) < lrmax(rprof, 1, rmin)) f1 = 1; else f1 = 0;
	if (lrmin(rprof, 20, 35) < lrmax(rprof, 20, 35)) f2 = 1; else f2 = 0;
	if (lrmin(rprof, 35, 50) < lrmax(rprof, 35, 50)) f3 = 1; else f3 = 0;
	bits[39] = f1; bits[40] = f2; bits[41] = f3;

/* The G features use the width */
	if (width[20] >= width[40]) g1 = 1; else g1 = 0;
	if (width[25] >= width[10]) g2 = 1; else g2 = 0;
	if (width[25] >= width[40]) g3 = 1; else g3 = 0;
	if (width[25] >= width[45]) g4 = 1; else g4 = 0;
	bits[42] = g1; bits[43] = g2; bits[44] = g3; bits[45] = g4;

/* The H property is the ratio between height and width */
	if (ratio > 2.5) h = 1; else h = 0;
	bits[46] = h;

/* The I property is a complicated combination of width properties */
	j = lrmin(width, 11, 39);
	w1 = width[j];
	w2 = width[ lrmax(width, 2, j-1) ];
	w3 = width[ lrmax(width, j+1, 49)];
	if (w1<w2 && w1<w3) ii = 1; else ii = 0;
	bits[47] = ii;
	free(lprof); free(rprof); free(dl); free(dr); free(width);

	*num = search_db (bits);

	printf ("%1d%1d%1d%1d%1d%1d%1d%1d%1d%1d",a1,a2,a3,a4,a5,a6,a7,a8,a9,a10);
	printf ("%1d%1d%1d%1d%1d%1d%1d ", a11,a12,a13,a14,a15,a16,a17);
	printf ("%1d%1d%1d%1d%1d%1d%1d%1d%1d%1d%1d ", 
			b1,b2,b3,b4,b5,b6,b7,b8,b9,b10,b11);
	printf ("%1d%1d%1d ", c1,c2,c3);
	printf ("%1d%1d ", d1,d2);
	printf ("%1d%1d%1d%1d%1d%1d ", e1, e2, e3, e4, e5, e6);
	printf ("%1d%1d%1d ", f1,f2,f3);
	printf ("%1d%1d%1d%1d %1d %1d\n",g1,g2,g3,g4, h, ii);
	printf ("Found a '%d'\n", *num);

	return ;

}

int search_db(int *bits)
{
	int i,j,k,vmin,imin, clear = 0;
	int hist[10];

	vmin = 50; imin = -1;
	hist[0] = hist[1] = hist[2] = hist[3] = hist[4] = 0;
	hist[5] = hist[6] = hist[7] = hist[8] = hist[9] = 0;
	for (i=0; i<DBSIZE; i++)
	{
	  j = bdiff (bits, database[i]);
	  if (j < vmin)
	  {
	    vmin = j;
	    imin = i;
	    hist[0] = hist[1] = hist[2] = hist[3] = hist[4] = 0;
	    hist[5] = hist[6] = hist[7] = hist[8] = hist[9] = 0;
	    hist[dbv[imin]] += 1;
	    clear = 1;
	  } else if (j == vmin)
	  {
		hist[dbv[imin]] += 1;
		clear = 0;
	  }
	}
	if (clear) 
	 return dbv[imin];

	printf ("Tie ... ");
	j = 0;
	for (i = 0; i<10; i++)
	  if (hist[i]>hist[j]) j = i;
	k = 0;
	for (i=0; i<10; i++)
	{
	  if (hist[i] == hist[j]) k++;
	  printf ("%d ", hist[i]);
	}
	printf ("\n");
	if (k>1) return -1;
	return j;
}

int bdiff (int *a, int *b)
{
	int i,j=0;

	for (i=0; i<48; i++)
	  if (a[i] != b[i]) j++;
	return j;
}

/*	Print a profile to the screen	*/

void outprof (float *ldata, float *rdata, int n)
{
	int i,j;

	for (i=1; i<=n; i++) {
	   printf ("%2d : ", i);
	   for (j=0; j<ldata[i]; j++) printf (".");
	   for (j=ldata[i]; j<=rdata[i]; j++) printf ("#");
	   printf ("\t\t\t%f %f\n", ldata[i], rdata[i]);
	}
}

void outdata (float *ldata, int n)
{
	int i,j;

	for (i=1; i<=n; i++) {
	   printf ("%2d : %f\n", i, ldata[i]);
	}
}

/*	The array DATA has N elements in it. Stretch it so that
	it has 50 elements; use linear interpolation.			*/

void rescale (float *data, int n)
{
	float newd[50];
	float w1, w2, dj, xinc, xi, w[20],kp;
	int i,j,j1,j2,nx, ix,k1,k2;

	if (n < 50) {
	  dj = (float)n/50.0;
	  for (i=0; i<49; i++) {
	   kp = (float)(n*i)/50.0;
	   k1 = (int)kp;
	   k2 = k1 + 1;
	   newd[i] = (data[k2]-data[k1])*(kp-k1) + data[k1];
	  }
	  newd[49] = data[n-1];

/*		j1 = (int)(i*dj);
		j2 = j1+1;
		w1 = j2 - (i*dj);
		w2 = 1.0-w1;
		if (w2 < 0) {w1 = 1.0; w2 = 0.0; }
		newd[i] = w1*data[j1] + w2*data[j2];
	  }
	  newd[49] = data[n-1];
*/
	} else {
	  xinc = (float)n/50.0;
	  if (xinc-(int)xinc == 0) nx = (int)(xinc + 1.0);
	    else nx = (int)(xinc+2.0);
	  for (i=0; i<50; i++) {
		xi = (float)i*xinc;
		ix = (int)xi;
		w[0] = (int)(xi+1.0) - xi;	/* First weight */
		w[nx-1] = 1.0-w[0];
		for (j=1; j<nx-1; j++) w[j] = 1.0;
		w1 = 0.0; w2 = 0.0;
		for (j=0; j<nx; j++) {
		   w1 = w1 + w[j]*data[i+j];
		   w2 += w[j];
		}
		newd[i] = w1/w2;
	  }
	}

	for (i=0; i<50; i++)
	   data[i] = newd[i];
}

float lrpeak (float *data, int lmin, int lmax)
{
	int i,j, imax, imin;

	imin = 100; imax = -imin;
	for (i=lmin; i<=lmax; i++) {
	   if (data[i] < imin) imin = data[i];
	   if (data[i] > imax) imax = data[i];
	}
	return imax - imin + 1;
}

int lrmax (float *data, int lmin, int lmax)
{
	int i,j;
	float x;

	x = data[lmin]; j = lmin;
	for (i=lmin; i<=lmax; i++) {
	   if (data[i] > x) {
	      j = i;
	      x = data[i];
	   }
	}
	return j;
}

int lrmin (float *data, int lmin, int lmax)
{
	int i,j;
	float x;

	x = data[lmin]; j = lmin;
	for (i=lmin; i<lmax; i++) {
	   if (data[i] < x) {
	      j = i;
	      x = data[i];
	   }
	}
	return j;
}


int rec, actual, guess;

main(int argc, char *argv[])
{
	int i,j,k, ii, jj, page, n,m, magic;
	FILE *f, *outf, *g, *ff;
	char b1[2953], astr[65];
	long *b3;
	int kk,tmp, err;
	struct image *x;

/* Read in an image */
         if (argc < 2)
         {
           printf ("Usage: recp <image> \n");
           printf ("This program examines a scanned digit image\n");
           printf ("and attempts to recognize the digit. \n");
	   printf ("Uses profiles.\n");
           exit (1);
         }

        x = Input_PBM (argv[1]);
        if (x == 0)
        {
         printf ("The image file '%s' does not exist, or is unreadable.\n", argv[1]);
         exit (2);
        }

/* Reverse the levels, if needed */
	for (i=0; i<x->info->nr; i++)
	  for (j=0; j<x->info->nc; j++)
	    if (x->data[i][j] == 0) x->data[i][j] = 1;
	    else x->data[i][j] = 0;
 
/* Get the database */
	get_database ();

/* Now try to classify the glyph as a digit */
	 guess = recnum(x);
	 rec++;
}

void get_database ()
{
	int i,j,k, v;
	FILE *f;

	f = fopen ("prof.db", "r");
	if (f == NULL) 
	{
	  printf ("ERROR: No database!\n");
	  exit (2);
	}

	for (i=0; i<1000; i++)
	{
	  k = fscanf (f, "%d", &v);
	  if (k < 1)  { DBSIZE = i; break; };
	  dbv[i] = v;
	  for (j=0; j<48; j++)
	  {
	    k = fscanf (f, "%1d", &v);
	    if (k<1)  { DBSIZE = i; break; };
	    database[i][j] = (v==1);
	  }
	  if (k<1) break;
	}
	DBSIZE = 1000;
	fclose (f);
}

/*	Extract a glyph in standard size (50x50) and orientation	*/

void ext_glyph (struct image *x, struct image **y)
{
	struct image *z, *w;
	int i,j,k,n,m;
	int na, ma, code;
	float i1,i2,j1,j2,theta;

/* Make a copy */
	z = 0;
	copy (&z, x);

/* Find the biggest region */
        na = 0; ma = 0;
        do {
                code = region_8 (z, 9);
                na = area_loc (z, 9);
                if (na <= 0) break;
                if (na > ma) 
		{
                        if (ma > 0) delete (z, 10);
                        remark (z, 9, 10);
                        ma = na;
                } else delete (z, 9);
        } while (code == 0);
        remark (z, 10, 0);

	*y = z;
	return;
}

/*	Count and return the number of pixels having value VAL	*/

int area_loc (struct image *x, int val)
{
	int i,j,k;

	k = 0;
	for (i=0; i<x->info->nr; i++)
	  for (j=0; j<x->info->nc; j++) 
		if (x->data[i][j] == val) k++;
	return k;
}

/*      Locate a black (0) region and mark it with value VALUE. 8-conneceted  */
int region_8 (struct image *x, int value)
{
        int i,j,ii,jj;

        ii= -1; jj = -1;
        for (i=0; i<x->info->nr; i++) {
           for (j=0; j<x->info->nc; j++)
                if (x->data[i][j] == 0) {
                        ii=i; jj=j;
                        break;
                }
           if (ii >= 0) break;
        }

        if (ii < 0) 
                return 1;

        mark8 (x, value, ii,jj);
	return 0;
}

/*      Mark an 8-connected region, beginning at (iseed, jseed), with VALUE   */
void mark8 (struct image *x, int value, int iseed, int jseed)
{
        int i,j,n,m;
        int range();

        if (x->data[iseed][jseed] != 0) 
                return;
        x->data[iseed][jseed] = value;  

        for (i= -1; i<=1; i++)          
          for (j= -1; j<=1; j++) {      
            n = i+iseed;  m = j+jseed;
            if (range(x, n,m) == 0)     
                continue;
            if (x->data[n][m] == 0)     
              mark8 (x, value, n, m);   
          }
}

/*      Change all pixels with value VALUE to value BACKGROUND  */
void delete (struct image *x, int value)
{
        int i,j;

        for (i=0; i<x->info->nr; i++)
           for (j=0; j<x->info->nc; j++)
                if (x->data[i][j] == value)
                        x->data[i][j] = (unsigned char)BACKGROUND;
}

/*      Change all pixels with value V1 to value V2.    */

void remark (struct image *x, int v1, int v2)
{
        int i,j;

        for (i=0; i<x->info->nr; i++)
           for (j=0; j<x->info->nc; j++) 
                if (x->data[i][j] == v1) x->data[i][j] = v2;
}


void extract (struct image *x, struct image **y, int val, 
              int *rm, int *cm)
{
        int i,j, rmin, rmax, cmin, cmax;
        float xx[4], yy[4];
        struct image *z, *newimage();

        box (x, val, xx, yy);
        rmin = xx[0];   cmin = yy[0];   rmax = xx[2];   cmax = yy[2];
        *rm = rmin;     *cm = cmin;

/* Create and initialize the new array */
        z = newimage (rmax-rmin+3, cmax-cmin+3);
        if (x == 0) {
                printf ("EXTRACT: Can't create %d by %d image.\n",
                        rmax-rmin+3, cmax-cmin+3);
                printf ("RMAX=%d Rmin=%d Cmax = %d Cmin=%d\n",rmax,rmin,
                        cmax, cmin);
                return;
        }
        for (i=0; i<z->info->nr; i++)
           for (j=0; j<z->info->nc; j++)
                z->data[i][j] = BACKGROUND;

/* Copy VAL pixels into Z */
        for (i=1; i<z->info->nr-1; i++)
           for (j=1; j<z->info->nc-1; j++)
                if (range(x,i+rmin-1, j+cmin-1)) {
                  if (x->data[i+rmin-1][j+cmin-1] == val)
                        z->data[i][j] = val;
                  else z->data[i][j] = BACKGROUND;
                } else z->data[i][j] = BACKGROUND;
        *y = z;
}

/*      Measure the area and perimeter of the holes in the region
        marked with value V in the image X. Also count the holes.       */

void hole_metrics (struct image *x, int v, int *hn, 
                   float *hp, float *ha)
{
        int i, ii, jj;
        struct image *y;

/* Extract the object into its own local image. */
        extract (x, &y, v, &ii, &jj);

/* Mark the background with a new value. */
        mark4(y, 254, 0, 0);

/* Make sure that the object is NOT=0. */
        if (v == 0) {
           remark (y, 0, 1);
           v = 1;
        }

/* Now pixels having value 255 are holes. Remark region with 0 */
        remark (y, 255, 0);

/* Now background is 254, holes are 0, and object are v. Locate
   holes by locating 0 regions. Count them, compute area&perimeter */

        *hn = 0;        *ha = 0.0;      *hp = 0.0;
        for (i=1; i<254; i++) {
            if (i == v) continue;
            region_4 (y, i);
            *hn += 1;
            *ha += (float)area(y, i);
            *hp += perimeter (y, i);
        }
        freeimage (y);
}

/*      Count and return the number of pixels having value VAL  */

int area (struct image *x, int val)
{
        int i,j,k;

        k = 0;
        for (i=0; i<x->info->nr; i++)
          for (j=0; j<x->info->nc; j++) 
                if (x->data[i][j] == val) k++;
        return k;
}

/*	Compute the perimeter of the region(s) marked with VAL	*/
float perimeter (struct image *x, int val)
{
	int i,j,k, ii,jj,t;
	float p;
	struct image *y;

	p = 0.0; y = 0;
	copy (&y, x);

/* Remove all pixels except those having value VAL */
	for (i=0; i<y->info->nr; i++) {
	   for (j=0; j<y->info->nc; j++) {
		if (x->data[i][j] != val) {
			y->data[i][j] = BACKGROUND;
			continue;
		}
		k = nay4(x, i, j, val);	/* How many neighbors are VAL */
		if (k < 4) 		/* If not all, this is on perim */
			y->data[i][j] = 0;
		else y->data[i][j] = BACKGROUND;
	}  }

	for (i=0; i<y->info->nr; i++) {
	   for (j=0; j<y->info->nc; j++) {
		if (y->data[i][j] != 0) continue;

		if (i==0 || j==0 || i==y->info->nr-1 ||
		    j == y->info->nc-1) {
		    continue;
		}

/*	Match one of the templates	*/

		k = 1;	t = 0;
		for (ii= -1; ii<=1; ii++) {
		   for (jj = -1; jj<=1; jj++) {
			if (ii==0 && jj==0) continue;
			if (y->data[i+ii][j+jj] == 0)
				t = t + k;
			k = k << 1;
		   }
		}

		if (t==0210 || t == 014 || t == 042 ||
		    t==0202 || t ==0101 || t ==0104 ||
		    t== 060 || t == 021) {
			p += 1.207;
			continue;
		}

		if (t == 0201 || t == 044 || t == 041 ||
		    t == 0204 || t ==0240 || t == 005) {
			p += 1.414;
			continue;
		}

		if (t == 030 || t == 0102 || t == 80 ||
		    t == 10 || t == 18) {
			p += 1.0;
			continue;
		}

		p += 1.207;
	}   }
	freeimage (y);
	return p;
}

/*      Determine the image-oriented bounding box for the region in the
         image X marked with value VAL. Return coordinates of the corners
         of the box in the arrays X1 and Y1 - 4 corners, 4 pairs of coords */
 
void box(struct image *x, int val, float *x1, float *y1)
{
        int i,j,ip1,jp1,ip2,jp2;
        
        ip1 = 10000;    jp1 = 10000;
        ip2 = -1;       jp2 = -1;

/* Find the min and max coordinates, both row and column */
        for (i=0; i<x->info->nr; i++)
          for(j=0; j<x->info->nc; j++)
                if (x->data[i][j] == val) {
                      if (i < ip1) ip1 = i;
                      if (i > ip2) ip2 = i;
                      if (j < jp1) jp1 = j;
                      if (j > jp2) jp2 = j;
                }
        if (jp2 < 0) 
                return;

/* Array X has row coordinates, Y has columns. Order is:
        x1[0],y1[0] : Upper left (min,min)
        x1[1],y1[1] : Lower left (max,min)
        x1[2],y1[2] : Lower right (max,max)
        x1[3],y1[3] : Upper right (min,max)                */

        y1[0] = (float) jp1;    x1[0] = (float) ip1;
        y1[1] = (float) jp1;    x1[1] = (float) ip2;
        y1[2] = (float) jp2;    x1[2] = (float) ip2;
        y1[3] = (float) jp2;    x1[3] = (float) ip1;
}

/*      Mark a 4-connected region with VALUE, starting at (iseed,jseed) */

void mark4 (struct image *x, int value, int iseed, int jseed)
{
        int i,j,n,m, k;
        int range();

        if (range(x, iseed, jseed)==0) return;

/* Pixels to be marked will all have the value K */
        k = x->data[iseed][jseed];
        x->data[iseed][jseed] = value;  

/* Recursively mark all neighbors */
        for (i= -1; i<=1; i++)          
          for (j= -1; j<=1; j++) {      
            n = i+iseed;  m = j+jseed;
            if (range(x, n,m) == 0)     
                continue;
            if (i*j) continue;          
            if (x->data[n][m] == k)     
              mark4 (x, value, n, m);   
          }
}

/*      Locate a black (0) region and mark it with VALUE. 4-connected.  */
 
void region_4 (struct image *x, int value)
{
        int i,j,ii,jj;

        ii= -1; jj = -1;
        for (i=0; i<x->info->nr; i++) {
           for (j=0; j<x->info->nc; j++)
                if (x->data[i][j] == 0) {
                        ii=i; jj=j;
                        break;
                }
           if (ii >= 0) break;
        }

        if (ii < 0) 
           return;
        mark4 (x, value, ii,jj);
}

void threshold (struct image *x, int t)
{
        int i,j;

        for (i=0; i<x->info->nr; i++) 
           for (j=0; j<x->info->nc; j++) 
                if (x->data[i][j] < t) x->data[i][j] = (unsigned char)0;
                 else x->data[i][j] = (unsigned char)BACKGROUND;
}

/*      Find a threshold for the image X using Iterative Selection      */
void thresh_is (struct image *x, int *t)
{
        float tt, tb, to, t2;
        int   n, i, j, no, nb;

        tb = 0.0;       to = 0.0;       no = 0;
        n = (x->info->nr) * (x->info->nc);
        for (i=0; i<x->info->nr; i++)
           for (j=0; j<x->info->nc; j++) 
                to += x->data[i][j];
        tt = (to/(float)n);

        while (n) {
                no = 0; nb = 0; tb=0.0; to = 0.0;
                for (i=0; i<x->info->nr; i++) {
                   for (j=0; j<x->info->nc; j++) 
                        if ( (float)(x->data[i][j]) >= tt ) {
                                to = to + (float)(x->data[i][j]);
                                no++;
                        } else {
                                tb = tb + (float)(x->data[i][j]);
                                nb++;
                }       }

                if (no == 0) no = 1;
                if (nb == 0) nb = 1;
                t2 = (tb/(float)nb + to/(float)no )/2.0;
                if (t2 == tt) n=0;
                tt = t2;
        }
        *t = (int) tt;
}

/* Return the number of 4-connected neighbors of (i,j) with value VAL */
 
int nay4 (struct image *x, int i, int j, int val)
{
        int n,m,k,range();

        if (x->data[i][j] != val) return 0;
        k = 0;
        for (n= -1; n<=1; n++) {
           for (m= -1; m<=1; m++) {
                if (n*m) continue;
                if (range(x,i+n, j+m)) 
                  if (x->data[i+n][j+m] == val) k++;
           }
        }
        return k-1;
}
