/* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

		Handprinted Digit Recognition

	This program locates convex deficiencies in the character
	image and classifies based on their size and position.

   - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

#include <stdio.h>
#include <malloc.h>
#define MAX 1
#include "lib.h"

#define VERBOSE 0
#define DEBUG VERBOSE
#define BACKGROUND 255
int DO_DRAW = 1;
int DRAW_VAL = 1;

/*      An insignificant region, in terms of % of black */
#define INSIG 6.000001

/*      A small region  */
#define SMALL 30.00001

/*      A medium sized region */
#define MIDDLE 60.00001

/*      A large region  */
#define LARGE 60.000


struct crec {                          /* A 'convexity zone' record. One  */
	float cmi, cmj;                /* of these is created for each    */
	float x1, x2, m02, m20,m11;             /* Convexity measures of this region  */
	float r1, r2, circ;
	int size;                      /* convex background zone enclosed */
	int zone;                      /* by glyph pixels.                */  
	struct crec *next;
};

struct crec *conv_zones[5];
int votes[10];
unsigned char buff[1000];
unsigned char data[76][72];
unsigned char **digit[10];
struct image *im=0;
int NR, NC;

/* Stats for a glyph - used in training for identifying useful features */
int set_pixels = 0;                     /*  Number of black pixels in image */
int uli,ulj;                            /*  Upper left corner coordinates   */
int lri, lrj;                           /*  Lower right corner coordinates  */
float Ci, Cj;                           /*  Centre of mass coordinates      */
float cx1,cx2;                          /* Convexity measures for glyph */
float rec1, rec2, cir1, cir2;
float m20, m02,m11;
float M02, M20, M11;

int czones (struct image *x);
void dump_zones(struct image *z);
void store (int zon, int size, float cmi, float cmj, float c1, float c2);
void xmark8 (struct image *x, int value, int iseed, int jseed);
int above (struct crec *p1, struct crec *p2);
int below (struct crec *p1, struct crec *p2);
int left_of (struct crec *p1, struct crec *p2);
int right_of (struct crec *p1, struct crec *p2);
int upper (struct crec *p);
int lower (struct crec *p);
int left (struct crec *p);
int right (struct crec *p);
int upper_left (struct crec *p);
int upper_right (struct crec *p);
int lower_left (struct crec *p);
int lower_right (struct crec *p);
int middle (struct image *z, struct crec *p);
void swap (struct crec **p1, struct crec **p2);
int recnum(struct image * x);
void ext_glyph (struct image *x, struct image **y);
void dump (struct image *x);
int classify (struct image *z);    
void osmooth (struct image *x);
float C1 (struct image *x, int val);
float R1 (struct image *x, int val);
float R2 (struct image *x, int val);

void freeimage (struct image  *z);
struct image  *newimage (int nr, int nc);
int area (struct image *x, int val);
void bound4 (struct image *x, int val);
void box(struct image *x, int val, float *x1, float *y1);
void center_of_mass (struct image *x, int val, float *ii, float *jj);
float central_moments (struct image *x, int i, int j,  int val);
int convex_hull (int *rows, int *columns, int n);
void convexity (struct image *im, int val, 
		float *x1, float *x2);
int crossing_index(struct image *x, int ii, int jj);
void dilate (struct image *x, int val);
void dilaten (struct image *x, int val, int n);
int distance_4 (int i, int j, int n, int m);
int distance_8 (int i, int j, int n, int m);
float distance_e (int i, int j, int n, int m);
void mer (struct image *x, int val, float *x1, float *y1);
void filled_polygon (struct image *y, int *r, int *c, int n, int val);
void fill (struct image *y, int i, int j, int val);
void principal_axis(struct image *x, int val, float *i1, float *j1, 
                      float *i2, float *j2);
int line_intersect (float a1, float b1, float c1, float a2,
                      float b2, float c2, float *x, float *y);
float fpow (float x, int j);
void hswap (int *rows, int *columns, int i, int j);
int line2pt (float x1, float y1, float x2, float y2, 
             float *a, float *b, float *c);
float minmax_dist (struct image *x, float i1, float j1, float i2,
               float j2, int val, int *i3, int *j3, int *i4, int *j4);
void perp (float a, float b, float c, float *a1, float *b1, 
           float *c1, float x, float y);
float all_dist (struct image *x, float i1, float j1,
                 float i2, float j2, int val);

#if defined (PC)
void del_reg (struct image *x, int value);
#else
void delete (struct image *x, int value);
#endif

void extract (struct image *x, struct image **y, int val, 
	      int *rm, int *cm);
void fill_holes (struct image *x, int v);
void hole_metrics(struct image *x, int v, int *hn, 
	     float *hp, float *ha);
int is_background (int i);
int is_zero (float x);
void line (struct image *im, int x1, int y1, int x2, int y2);
void mark4 (struct image *x, int value, int iseed, int jseed);
void mark8 (struct image *x, int value, int iseed, int jseed);
int nay4 (struct image *x, int i, int j, int val);
int nay8 (struct image *x, int i, int j, int val);
float perimeter (struct image *x, int val);
int plot(struct image *im, int x, int y);
int range (struct image *x, int n, int m);
void region_4 (struct image *x, int value);
int region_8 (struct image *x, int value);
void remark (struct image *x, int v1, int v2);
void set_background (int v);
void set_draw_val (int a);
void thinzs (struct image *x, int val);
void background (struct image *x, int t);
void thresh_cor (struct image *x, int *t);
void thresh_is (struct image *x, int *t);
void threshold (struct image *x, int t);
float distance_e_n (float *fv1, float *fv2, int n);


int recnum(struct image *x)
{
	struct image *y=0;
	int i,j,ii,jj,k,n, c[256], na, ma;
	char fn[128];
	float i1,i2,j1,j2, x1,x2,x3;
	int oldnr, oldnc;

	if (VERBOSE) printf ("Extracting a minimal sized image\n");
	extract (x, &y, 0, &ii, &jj);
	if (VERBOSE) printf ("Calling CZONES\n");

	set_pixels = 0;
	for (i=0; i<y->info->nr; i++)
	  for (j=0; j<y->info->nc; j++)
	    if (y->data[i][j] == 0) set_pixels++;
	center_of_mass (y, 0, &Ci, &Cj);
	convexity (y, 0, &cx1, &cx2);
	M02 = central_moments (y, 0,2, 0);
	M20 = central_moments (y, 2,0, 0);
	M11 = central_moments (y, 1,1, 0);

/* Test the new feature - convexity zones */
	jj = czones (y);
	printf ("THE NUMBER WAS %d \n\n\n", jj);
	if(jj>9) return -1;
	freeimage (y);
	return jj;
}

int rec, actual, guess;

main(int argc, char *argv[])
{
	int i,j,k, n,m;
	FILE *f, *g;
	struct image *x=0;

/*	_stklen = 16000; */

	rec = 0;
	if (argc > 1)
	{
/* Read in an image */

	  x = Input_PBM (argv[1]);
	  if (x == 0) 
	  {
	    printf ("Can't access the input image.\n");
	    exit (1);
	  }
	} else
	{
	  printf ("recc <image>\n");
	  printf (" Digit recognition using convex deficiencies.\n");
	  exit (1);
	}

 /* Reverse the levels, if needed */
         for (i=0; i<x->info->nr; i++)
           for (j=0; j<x->info->nc; j++)
             if (x->data[i][j] == 0) x->data[i][j] = 1;
             else x->data[i][j] = 0;
  
/* Smooth the outline */
	osmooth (x);

	if (VERBOSE) {
	  printf ("Input binary image:\n");
	  dump(x);
	}

/* Now try to classify the glyph as a digit */
	if (VERBOSE)
	 printf ("Attempting to classify using convexity regions.\n");

	guess = recnum(x);
}

/* Outline smoothing using templates */

/*

Outline smoothing for Convexity digit recognition:

The idea is to smooth the character outline without changing
the overall large-scale shape. Let's try template matching 3x3
regions using coding:

	1   2   4
        8   *  16
        32 64 128

where the * is black, and black is to be matched. Delete * when any
of the templates below match:

	* * *	     	   *    *   
	  *	 *	 * *	* * 
	       * * *	   *	*
	  7	224	148	 41


     - - -     - * *     * - -    - - - 
     - * *     - * -     * * -    - * -
     - - *     - - -     - - -    * * -

	144	6	9	96

     - - *     * * -     - - -    - - - 
     - * *     - * -     * * -    - * -
     - - -     - - -     * - -    - * *
	20	3	40	192


*/

void osmooth (struct image *x)
{
	int i,j,k,d;
	int ii,jj, again;

	again = 1;
	while (again) 
	{
	 again = 0;
         for (i=1; i<x->info->nr-1; i++)
           for (j=1; j<x->info->nc-1; j++) 
	      if (x->data[i][j]==0) 
	      {
		k = 0; d = 1;
	        for (ii= -1; ii<= 1; ii++)
		{
		  for (jj= -1; jj<= 1; jj++)
		  {
		    if ( (ii==0) && (jj==0) ) continue;
		    if (x->data[i+ii][j+jj] == 0)
		      k += d;
		    d = d*2;
		  }
		}
		if (k==7 || k==224 || k==148 || k == 41 ||
			k==144 || k==6 || k==9 || k == 96 ||
			k==20 || k==3 || k==40 || k==192)
		{
		  x->data[i][j] = 1;
		  again = 1;
		}
	      }
	}
}

/* Use CONVEXITY ZONES to classify a glyph X */

int czones (struct image *x)
{
	int i,j,k,n,m,left,right,up,down;
	int again, size;
	struct image *z;
	float cmi, cmj, c1, c2;

	z = 0;
	if (VERBOSE) printf ("Running CZONES\n");
	for (i=0; i<5; i++) conv_zones[i] = (struct crec *)0;

	copy (&z, x);
	if (VERBOSE) printf ("Locating and marking convex regions\n");

	for (i=0; i<z->info->nr; i++)
	  for (j=0; j<z->info->nc; j++) {
		if (x->data[i][j] == 0) z->data[i][j] = 255;
		else {
		  up = 0; down = 0; left = 0; right = 0;

/* Check upwards */
		  for (k=i-1; k>=0; k--)
			if (x->data[k][j] == 0) {
			  up = 1; break;
			}
/* Check down */
		  for (k=i+1; k<x->info->nr; k++)
			if (x->data[k][j] == 0) {
			  down = 1; break;
			}
/* Check left */
		for (k=j; k>=0; k--) 
			if (x->data[i][k] == 0) {
			  left = 1; break;
			}
/* Check right */
		 for (k=j+1; k<x->info->nc; k++) 
			 if (x->data[i][k] == 0) {
			   right = 1; break;
			 }

/* Consolidate these directions */
		  if (left && up && right && down) z->data[i][j] = 0;
		  else if (left && up && right) z->data[i][j] = 1; /* Open down */
		  else if (up && right && down) z->data[i][j] = 2; /* Open left */
		  else if (right && down && left) z->data[i][j] = 3; /* Up */
		  else if (down && left && up)  z->data[i][j] = 4;
		  else z->data[i][j] = 255;
		}
	  }

/* Mark zones and create records */
	if (VERBOSE) printf ("Marking zones and creating records.\n");
	for (n=0; n<=4; n++) {  /* For each possible zone type */
	  if (VERBOSE) printf ("Creating zone %d records.\n", n);
	  do {
	    again = 0;
	    for (i=0; i<z->info->nr; i++)
	      for (j=0; j<z->info->nc; j++) {
		if (z->data[i][j] == n) {       /* Found a zone */
		  if (n==0) {           /* Look for shadow holes */
		    if (VERBOSE) printf ("Shadow hole?\n");
		    if (shadow(z, i, j)) continue;
		    if (VERBOSE) printf ("Nope! Real.\n");
		  }
		  again = 1;
		  if (VERBOSE) printf ("Marking zone starting at (%d,%d)\n",
						i,j);
		  xmark8 (z, 5, i, j);          /* Mark this zone */
		  size = area (z, 5);   /* How big? */
		  if (VERBOSE) printf ("Marked. Area is %d\n", size);
		  center_of_mass (z, 5, &cmi, &cmj);
		  if (VERBOSE) printf ("Centre of mass at (%f, %f)\n", cmi,cmj);
		  convexity (z, 5, &c1, &c2);
		  if (VERBOSE) printf ("Convexity is %f %f\n", c1, c2);
		rec1 = R1 (z, 5);
		if (VERBOSE) printf("R1 is %f\n", rec1);
		rec2 = R2 (z, 5);
		if (VERBOSE) printf ("Rectangularity is %f %f\n", rec1, rec2);
		cir1 = C1 (z, 5);
		if (VERBOSE) printf ("Circularity is %f\n", cir1);
		m02 = central_moments (z, 0,2, 5);
		if (VERBOSE) printf ("M02 is %f\n", m02);
		m20 = central_moments (z, 2,0, 5);
	       if (VERBOSE) printf ("M20 is %f\n", m20);
		m11 = central_moments(z, 1,1, 5);
	       if (VERBOSE) printf ("M11 is %f\n", m11);
		  store (n, size, cmi, cmj, c1, c2);
		  if(VERBOSE) printf ("Region stored. Deleting...\n");
#if defined (PC)
		  del_reg (z, 5, &k);
#else
		  delete (z, 5);
#endif
		  if (VERBOSE) printf ("Done.\n");
		}
	      }
	  } while (again);
	}
	dump_zones (z);
	return classify (z);
}

/* Maintain a structure (list) of convexity zones in this glyph.
   For each zone (5 of them) there is a list, sorted on size,
   of actual zones found in the image. These will be matched against
   templates for the test glyphs.                               */

void store (int zon, int size, float cmi, float cmj, float c1, float c2)
{
	int i,j,k;
	struct crec *p1, *p2;
	float fsize;

	if (VERBOSE) printf ("Storing a zone %d record size %d\n", zon, size);

/* Don't bother with INSIG sized regions */
	if (set_pixels > 0)
	  fsize = (float)(size/(float)set_pixels) * 100.0;
	if (fsize < INSIG) return;

/* Change SIZE from #pixels to % of black, rounded. */
	size = (int)(fsize + 0.5);

/* Create a new record and insert it */
	p1 = (struct crec *)malloc (sizeof(struct crec));
	if (p1) {
	  p1->cmi = cmi;  p1->cmj = cmj;
	  p1->size = size;        p1->next = (struct crec *)0;
	  p1->x1 = c1; p1->x2 = c2;
	  p1->r1 = rec1; p1->r2 = rec2; p1->circ = cir1;
	  p1->m02 = m02; p1->m20 = m20; p1->m11 = m11;

	  if (conv_zones[zon] == 0) {
	    conv_zones[zon] = p1;
	    return;
	  }

	  if (size > conv_zones[zon]->size) {     /* New max */
	    p1->next = conv_zones[zon];
	    conv_zones[zon] = p1;
	    return;
	  }
	  p2 = conv_zones[zon];
	  do {
		  if (p2->next == 0) {
		    p2->next = p1;
		    return;
		  }

		  if (p2->next->size > size)
		    p2 = p2->next;
		  else {
		    p1->next = p2->next;
		    p2->next = p1;
		    return;
		  }
	  } while (p2);
	} else printf ("*** No memory in STORE! ***\n");
}

/* Is the 0 zone (hole) at Z[i,j] real, or is it a 'shadow' hole?
   That is, does this hole border on any other zones? If so, fill.      */

int shadow (struct image *z, int a, int b)
{
	int i,j,k,n,m;
	int zns[10];

	for (i=0; i<10; i++) zns[i] = 0;

/* Mark the hole */
	xmark8 (z, 9, a,b);

/* Locate any 9-pixel that has a neighbor between 1 - 4. */
	for (i=0; i<z->info->nr; i++) {
	  for (j=0; j<z->info->nc; j++) {
	    if (z->data[i][j] == 9) {
	      for (n = i-1; n<=i+1; n++) {
		if (n<0 || n>=z->info->nr) continue;
		for (m = j-1; m<=j+1; m++)  {
		  if (m==j || n == i) 
		  {
		    if (m<0 || m>=z->info->nc) continue;
		    if (z->data[n][m] >= 1 && z->data[n][m] <=4) {
		      k = z->data[n][m];
		      xmark8 (z, k+4, n,m);
		    }
		  }
		}
	      }
	    }
	  }
	}

/* Find areas of neighboring regions */
	for (i=1; i<=4; i++)
	   zns[i] += area (z, i+4);

/* Find the largest such neighboring region, if any. Fill the hole
   with that value, and fix the values if so (return 1). Otherwise,
   fix the hole values (return 0).                                      */
	n = 0;
	for (i=1; i<=4; i++) 
	   if (zns[n] <zns[i]) n = i;

/* No shadow hole */
	if (zns[n] == 0) 
	{
		remark (z, 9, 0);
		return 0;
	}

/* Shadow hole */
	remark (z, 9, n);           /* Fill the hole */
	for (i=1; i<=4; i++) {
	  m = 0;
	  if (zns[i]>0) remark (z, i+4, n);
	}
	return n;
}

int classify (struct image *z)
{
	int i,j,k;
	int v1,v2,v3;
	struct crec *p1, *p2, *p3, *p4, *px;
	struct crec * et[12];
	int votes[10];

/* Find biggest feature */
	if (VERBOSE) printf ("Attempting to classify.\n");
	v2 = -1;        v1 = -1;
	for (i=0; i<5; i++)
	   if (conv_zones[i])
	    if (conv_zones[i]->size > v2)  {
	      v1 = i; v2 = conv_zones[v1]->size;
	    }
	if (v2 <= 0) p1 = 0;
	else {
	  p1 = conv_zones[v1]; p1->zone = v1;
	  conv_zones[v1] = p1->next;
	}

/* Next biggest */
	 v2 = -1;        v1 = -1;
	 for (i=0; i<5; i++)
	    if (conv_zones[i])
	     if (conv_zones[i]->size > v2)  {
	       v1 = i; v2 = conv_zones[v1]->size;
	     }
	 if (v2 <= 0) p2 = 0;
	 else {
	   p2 = conv_zones[v1]; p2->zone = v1;
	   conv_zones[v1] = p2->next;
	 }
 
 
 /* Next biggest */
	  v2 = -1;        v1 = -1;
	  for (i=0; i<5; i++)
	     if (conv_zones[i])
	      if (conv_zones[i]->size > v2)  {
		v1 = i; v2 = conv_zones[v1]->size;
	      }
	  if (v2 <= 0) p3 = 0;
	  else {
	    p3 = conv_zones[v1]; p3->zone = v1;
	    conv_zones[v1] = p3->next;
	  }
 
 /* Next biggest */
	  v2 = -1;        v1 = -1;
	  for (i=0; i<5; i++)
	     if (conv_zones[i])
	      if (conv_zones[i]->size > v2)  {
		v1 = i; v2 = conv_zones[v1]->size;
	      }
	  if (v2 <= 0) p4 = 0;
	  else {
	    p4 = conv_zones[v1]; p4->zone = v1;
	    conv_zones[v1] = p4->next;
	  }

/*
	if (p2)
	  if (p1->size/p2->size > 20) {
		p2 = 0;
		p3 = 0; 
		p4 = 0;
	  }
	if (p3)
	  if (p1->size/p3->size > 20) {
		 p3 = 0; 
		 p4 = 0;
	   }
	 if (p4)
	   if (p1->size/p4->size > 20) 
		  p4 = 0;
*/
	for (i=0; i<10; i++) votes[i] = 0;

/* 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 */
/* The numeral 1 */
	if (p1 == 0) {
		printf ("No convex zones - a one.\n");
		votes[1] = 1;
		return 1;                  /* No zones at all */
	}

	if ((p1 && p1->zone == 0) || (p2 && p2->zone==0)) k = 0;
	else k = 1;
	if (k==1)
	{
	  if (cx1< 1.2 && cx1 >1.0 && cx2<.76 && cx2 > .3
		 && M20/M02>5.0 && p1->size<60)
			return 1;
	  if (M20/M02 > 10.0) return 1;

/* One small '1' zone, upper left */
	  if (p2==0) {
	    if (p1->zone == 1 && upper_left (p1) && p1->size < 36) {
		printf ("One small '1' zone, upper left - a one.\n");
		votes[1] += 1;
	    } else if (p1->zone==2 && lower_left(p1) && p1->size < MIDDLE) {
		printf ("One small '2' zone, lower left - a one.\n");
		votes[1] += 1;
	    }
	  }

	}

/* 2 2 2 2 2 2 2 2 2 2 2 2 2 2 2 2 2 2 2 2 2 2 2 2 2 2 2 2 2 2 2 2 2 2 2 2 */
/* The numeral 2 */
	if (p2) {
	  if (p1->zone == 2 && p2->zone == 4 && upper_left (p1)
	    && lower_right(p2) && (float)(p1->size)/(float)(p2->size)<8.0)
		 votes[2] += 1;
	  if (p1->zone == 4 && p2->zone == 2 && upper_left(p2) 
	    && lower_right(p1) && (float)(p1->size)/(float)(p2->size)<8.0)
		 votes[2] += 1;
	}

/* 3 3 3 3 3 3 3 3 3 3 3 3 3 3 3 3 3 3 3 3 3 3 3 3 3 3 3 3 3 3 3 3 3 3 3 3 */
/* The numeral 3 */
	if (p2) {
	  if (p1->zone == 2 && p2 && p2->zone == 4 && left_of (p1,p2) &&
		p1->size>80 && (float)(p1->size)/(float)(p2->size)>=8.0)
			 votes[3] += 1;
	  if (p1->zone == 2 && p2 && p2->zone == 2 && p3 && p3->zone==4 &&
	    left_of(p1,p3) && left_of(p2,p3) && (p1->size+p2->size)>90 )
		votes[3] += 1;
	  if (p1->zone == 2 && p2 && p2->zone == 2 && p3==0 && 
		(upper(p1)&&lower(p2) || upper(p2)&&lower(p1))  
		&& (p1->size+p2->size)>90 )
		votes[3] += 1;
	} else if (p1->zone == 2 && p1->size >60 && 
		p1->x1 > 1.1) votes[3] += 1;

/* 4 4 4 4 4 4 4 4 4 4 4 4 4 4 4 4 4 4 4 4 4 4 4 4 4 4 4 4 4 4 4 4 4 4 4 4 */
/* Numeral 4 */
	if (p1->zone == 3 && upper (p1)) votes[4] += 1;

/* 5 5 5 5 5 5 5 5 5 5 5 5 5 5 5 5 5 5 5 5 5 5 5 5 5 5 5 5 5 5 5 5 5 5 5 5 */
/* The numeral 5 */
	if (p2) {
	  if (p1->zone == 4 && p2->zone == 2 && upper_right(p1) &&
		lower_left(p2) && p1->size/p2->size<9 &&
		p2->size/p1->size < 9 ) votes[5] += 1;
	  if (p1->zone == 2 && p2->zone == 4 && upper_right(p2) &&
		lower_left(p1) && p1->size/p2->size<9 &&
		p2->size/p1->size < 9) votes[5] += 1;
	  if (p1->zone == 4 && p2->zone == 2 && upper_right(p1) &&
		lower_right(p2) && p1->size/p2->size<9 &&
		p2->size/p1->size < 9 ) votes[5] += 1;
	  if (p1->zone == 2 && p2->zone == 4 && upper_right(p2) &&
		lower_right(p1) && p1->size/p2->size<9 &&
		p2->size/p1->size < 9) votes[5] += 1;

	}

/* 8 8 8 8 8 8 8 8 8 8 8 8 8 8 8 8 8 8 8 8 8 8 8 8 8 8 8 8 8 8 8 8 8 8 8 8 */
/* The digit 8 */
/* Arrange the zones for checking .... */
	if (p1 && p2 && p3==0) {
	  if (p1->zone==0 && p2->zone==0 && ((upper(p1) && lower(p2))||
		(upper(p2)&&lower(p1)))) votes[8] += 1;
	}

	if (p3 && p4==0) {
	   et[0] = p1; et[1] = p2; et[2] = p3; 
	   for (i=1; i<3; i++)
	     if (et[i]->zone == 0) {
		 px = et[0]; et[0] = et[i]; et[i] = px;
	     }
	   for (i=2; i<3; i++)
	     if (et[i]->zone == 0) {
		 px = et[1]; et[1] = et[i]; et[i] = px;
	     }
	   if (above(et[1], et[0])) {
		 px = et[1]; et[1] = et[0]; et[0] = px;
	   }
	   if (et[0]->zone==0 && et[1]->zone==0 && (et[2]->zone==2||
		et[2]->zone==4) && above (et[0], et[2]) &&
		below(et[1], et[2])) votes[8] += 1;
	    i = 0;
	    if (et[0] && et[0]->zone == 0) i++;
	    if (et[1] && et[1]->zone == 0) i++;
	    if (et[2] && et[2]->zone == 0) i++;
	    if (i>=2) votes[8] += 1;
	}

	if (p1 && p2 && p3 && p4) {
	  et[0] = p1; et[1] = p2; et[2] = p3; et[3] = p4;
	  for (i=1; i<4; i++)
	    if (et[i]->zone == 0) {
		px = et[0]; et[0] = et[i]; et[i] = px;
	    }
	  for (i=2; i<4; i++)
	    if (et[i]->zone == 0) {
		px = et[1]; et[1] = et[i]; et[i] = px;
	    }
	  if (above(et[1], et[0])) {
		px = et[1]; et[1] = et[0]; et[0] = px;
	  }
	  if (et[2]->zone == 4 && et[3]->zone == 2) {
		px = et[3]; et[3] = et[2]; et[2] = px;
	  }

/* The test: */
	  if (et[0]->zone == 0 && et[1]->zone == 0 && 
	      et[2]->zone == 2 && et[3]->zone == 4) {
	    if (above (et[0], et[1]) && above(et[0], et[2]) && above(et[0], et[3])){
	      if (above(et[2],et[1]) && above(et[3], et[1]) && left_of(et[2], et[3]))
		votes[8] += 1;
	    }
	  }
	    i = 0;
	    if (et[0] && et[0]->zone == 0) i++;
	    if (et[1] && et[1]->zone == 0) i++;
	    if (et[2] && et[2]->zone == 0) i++;
	    if (et[3] && et[3]->zone == 0) i++;
	    if (i>=2) votes[8] += 1;
	}

/* 6 6 6 6 6 6 6 6 6 6 6 6 6 6 6 6 6 6 6 6 6 6 6 6 6 6 6 6 6 6 6 6 6 6 6 6 */
/* The numeral 6 */
	if (p2) {
	  if (p1->zone == 4 && p2->zone == 0 && above (p1, p2)) votes[6] += 1;
	  if (p1->zone == 0 && p2->zone == 4 && above (p2, p1)) votes[6] += 1;
	}

/* 7 7 7 7 7 7 7 7 7 7 7 7 7 7 7 7 7 7 7 7 7 7 7 7 7 7 7 7 7 7 7 7 7 7 7 7 */
/* The digit 7 */
	if (p1->zone == 2 && p2==0 && p1->size>50 && left(p1) &&
		votes[3]==0)
		votes[7] += 1;
	if ((p2==0||(p2->zone==4&&p2->size<30)) && p1->zone==1 && 
		p1->size>MIDDLE && left(p1) &&
		votes[1]==0)
			 votes[7] += 1;
	if ((p2==0||(p1->size/p2->size>=5)) 
		&& p1->zone==2 && p1->size<=50 && upper_left(p1)
		&& p1->x1<1.1) votes[7] += 1;



/* 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 */
/* The digit 0 */
	if (p1->zone == 0 && (p2==0) && middle(z,p1)) votes[0] += 1;
	if (p2)
	if (p1->zone == 0 && p2->size < SMALL && middle(z,p1)) votes[0] += 1;

/* 9 9 9 9 9 9 9 9 9 9 9 9 9 9 9 9 9 9 9 9 9 9 9 9 9 9 9 9 9 9 9 9 9 9 9 9 */
/* The digit 9 */
	if (p3) {
		if (p2->zone == 4) swap (&p2, &p3);
		if (p1->zone == 0 && (p2->zone==2 || p2->zone == 1) &&
		    p3->zone == 4 && above (p1,p2) && left_of (p1,p3) &&
			middle(z,p1)==0)
			votes[9] += 1;
	}
	if (p2) {
	  if (p1->zone == 0 && p2->zone == 2 && above (p1, p2)&& !middle(z,p1)) votes[9] += 1;
	  if (p1->zone == 2 && p2->zone == 0 && above (p2, p1)&& !middle(z,p2)) votes[9] += 1;
	  if (p1->zone == 0 && p2->zone == 1 && above (p1, p2)&& !middle(z,p1)) votes[9] += 1;
	  if (p1->zone == 1 && p2->zone == 0 && above (p2, p1)&& !middle(z,p2)) votes[9] += 1;
	} else {
		if (p1->zone == 0 && middle(z, p1)==0) votes[9] += 1;
	}

/* Count the votes */
	k = 0; j = 0;
	for (i=0; i<10; i++) {
	  printf ("%d ", votes[i]);
	  if (votes[k] < votes[i])  k = i; 
	  if (votes[i] > 0) j+=votes[i];
	}
	printf ("\n Result is %d\n", k);
	if ((float)j/2.0 >= votes[k]) return -1;
	if (votes[k] > 0) return k;
	return  -1;
}

void swap (struct crec **p1, struct crec **p2)
{
	struct crec *p;

	p = *p1; *p1 = *p2; *p2 = p;
}

int middle (struct image *z, struct crec *p)
{
	int i,j, dn, dm;

	dn = z->info->nr/16+1; dm = z->info->nc/16+1;
	if (p->cmi < Ci-dn || p->cmi > Ci+dn) return 0;
	if (p->cmj < Cj-dm || p->cmj > Cj+dm) return 0;
	return 1;
}

/*      Is zone P1 above zone P2?       */

int above (struct crec *p1, struct crec *p2)
{
	if (p1->cmi < p2->cmi) return 1;
	return 0;
}

/*      Is zone P1 below zone P2?       */

int below (struct crec *p1, struct crec *p2)
{
	if (p1->cmi > p2->cmi) return 1;
	return 0;
}

int left_of (struct crec *p1, struct crec *p2)
{
	if (p1->cmj < p2->cmj) return 1;
	return 0;
}

int right_of (struct crec *p1, struct crec *p2)
{
	if (p1->cmj > p2->cmj) return 1;
	return 0;
}
 
/*      Is zone P in the lower part of the image?       */

int lower (struct crec *p)
{
	if (p->cmi >= Ci) return 1;
	return 0;
}

/*      Is zone P in the upper part of the image?       */

int upper (struct crec *p)
{
	if (p->cmi <= Ci) return 1;
	return 0;
}

/*      Is zone P in the left part of the image?       */

int left (struct crec *p)
{
	if (p->cmj <= Cj) return 1;
	return 0;
}

/*      Is zone P in the right part of the image?       */

int right (struct crec *p)
{
	if (p->cmj >= Cj) return 1;
	return 0;
}

int upper_left (struct crec *p)
{
	if ((p->cmj <= Cj) && (p->cmi <= Ci)) return 1;
	return 0;
}

int upper_right (struct crec *p)
{
	if ((p->cmj >= Cj) && (p->cmi <= Ci)) return 1;
	return 0;
}

int lower_left (struct crec *p)
{
	if ((p->cmj <= Cj) && (p->cmi >= Ci)) return 1;
	return 0;
}

int lower_right (struct crec *p)
{
	if ((p->cmj >= Cj) && (p->cmi >= Ci)) return 1;
	return 0;
}

void dump_zones (struct image * z)
{
	int i,j,k;
	struct crec *p1, *p2;
	FILE *ff;

	ff = fopen ("stats", "a");
	printf ("Zones found for this glyph (%d black pixels):\n", set_pixels);
	printf ("Glyph CM at (%f,%f) X1=%f X2 = %f\n", Ci, Cj, cx1,cx2);
	printf ("moments: (2,0)=%6.2f  (0,2) = %6.2f  M11=%6.2f\n", M20,M02,M11);
	printf ("Zone Size     Pct  Cmi    Cmj   Convexity   Position\n");

	if (ff != NULL)
	 fprintf (ff,"%f %f %f %f %f ", cx1,cx2,M20,M02,M11);

	for (i=0; i<5; i++) {
	  if (conv_zones[i] == 0) continue;
	  p1 = conv_zones[i];
	  while (p1) {
	    printf (" %1d  %3d  %6.2f %5.2f %5.2f ", i, 
		p1->size, (float)p1->size, 
		p1->cmi, p1->cmj);
	    printf ("%5.3f,%5.3f ", p1->x1, p1->x2);
	    if (upper_left(p1)) printf ("Upper Left ");
	    if (lower_left(p1)) printf ("Lower Left ");
	    if (upper_right(p1)) printf ("Upper Right ");
	    if (lower_right(p1)) printf ("Lower Right ");
	    if (middle(z, p1)) printf ("[Centre] ");
	    printf ("\n");
    printf ("R1=%6.2f  R2=%6.2f  Circ = %6.2f M02 = %6.2f  M20 = %6.2f M11 = %6.2f\n",
		p1->r1,
		p1->r2, p1->circ, p1->m02, p1->m20, p1->m11);

	    if (ff != NULL)
	      fprintf (ff, "\n%d %f %f %f %f %f %f %f %f",
		i, (float)p1->size/(float)set_pixels*100.0, p1->x1,p1->x2, p1->r1,p1->r2, p1->circ,
		p1->m02, p1->m20);
	    k = 0;
            if (upper_left(p1)) k = 0;
            if (lower_left(p1)) k = 1;
            if (upper_right(p1)) k = 2;
            if (lower_right(p1)) k = 3;
	    fprintf (ff, " %d ", k);
            if (middle(z, p1)) fprintf (ff, " 1 ");
		  else     fprintf (ff, " 0 ");

	    p1 = p1->next;
	  }
	}
	fprintf (ff, "\n-1\n");
	fclose (ff);
}

/*      Mark an 8-connected region, beginning at (iseed, jseed), with VALUE   */

void xmark8 (struct image *x, int value, int iseed, int jseed)
{
	int i,j,n,m, v, again;

	v = x->data[iseed][jseed];
	x->data[iseed][jseed] = value;  

	do {
	  again = 0;
	  for (i=0; i<x->info->nr; i++)
	    for (j=0; j<x->info->nc; j++) 
	      if (x->data[i][j] == value)
		for (n=i-1; n<=i+1; n++)
		  for (m=j-1; m<=j+1; m++) {
		    if (range(x,n,m) == 0) continue;
		    if (x->data[n][m] == v) {
		      x->data[n][m] = value;
		      again = 1;
		    }
		  }
	  
	  for (i=x->info->nr-1; i>=0; i--)
	    for (j=x->info->nc-1; j>=0; j--)
	      if (x->data[i][j] == value)
		for (n=i-1; n<=i+1; n++)
		  for (m=j-1; m<=j+1; m++) {
		    if (range(x,n,m) == 0) continue;
		    if (x->data[n][m] == v) {
		      x->data[n][m] = value;
		      again = 1;
		    }
		  }
	} while (again);

/*        for (i= -1; i<=1; i++)          
	  for (j= -1; j<=1; j++) {      
	    n = i+iseed;  m = j+jseed;
	    if (range(x, n,m) == 0)     
		continue;
	    if (x->data[n][m] == v)     
	      xmark8 (x, value, n, m);  
	  }        */
}

void dump (struct image *x)
{
	int i,j;

	for (i=0; i<NR; i++){
	  for (j=0; j<NC; j++)
		if (x->data[i][j]==0) printf ("#");
		 else printf (".");
	  printf ("\n");
	}
}

/*      Calculate the coordinates of the center of mass of the region(s)
         marked with the value VAL. Return as (II,JJ).                   */
 
void center_of_mass (struct image *x, int val, float *ii, 
                float *jj)
{
        int i,j,kk;
        float y,z;

        z = 0.0;        kk = 0;
        *ii = 0.0;      *jj = 0.0;
        for (i=0; i<x->info->nr; i++) {
           for (j=0; j<x->info->nc; j++) {
                if (x->data[i][j] == val) {
                        *ii += (float)i;        *jj += (float)j;
                        kk += 1;
                }
           }
        }

        if (kk==0) 
                return;

        y = *ii/(float)kk;              z = *jj/(float)kk;
        *ii = *ii/(float)kk;            *jj = *jj/(float)kk;
}

/*      Calculate the circularity measure C1, ratio or area to perimeter    */

float C1 (struct image *x, int val)
{
        float p,a,c;

        p = perimeter(x, val);
        a = (float)area (x, val);
        if (a <= 0.0) 
           return 9.0e9;
        printf ("Area=%f perimeter=%f\n", a, p);
        c = p*p/(3.1414926535*4.0*a);
        return c;
}

/*      Return rectangularity measure R1, the ration of bounding box
        area to actual measured area of the region marked with VAL.     */

float R1 (struct image *x, int val)
{
/*      Compute image-frame rectangularity measure      */

        float x1[5], y1[5], a, b;

        box (x, val, x1, y1);

        a = (float) ((fabs((double)(x1[1]-x1[0]))+1.0) *
                     (fabs((double)(y1[2]-y1[0]))+1.0) );
        b = (float)area (x, val);

        if (is_zero(a)) 
                return 0.0;
        
        return b/a;
}

/*      Compute the rectangularity measure R2, the REGION oriented 
        bounding box area to measured area ratio.                       */

float R2 (struct image *x, int val)
{
/*      Compute a rectangularity measure and return the value.          */

        float xx1[5], yy1[5], d1,d2, x1;
        int i;

        x1 = (float)area (x, val);
        if (x1 < 0.0) 
                return 0.0;

/* Find the minimum enclosing rectangle oriented along the principal axis */
        mer(x, val, xx1, yy1);
        d1 = (float)sqrt ( (double)((xx1[0]-xx1[1])*(xx1[0]-xx1[1])) +
                 (double)((yy1[0]-yy1[1])*(yy1[0]-yy1[1])) );
        d2 = (float)sqrt ( (double)((xx1[1]-xx1[2])*(xx1[1]-xx1[2])) +
                 (double)((yy1[2]-yy1[1])*(yy1[2]-yy1[1])) );
        d1 = d1 + 1.0;  d2 = d2+1.0;
        printf ("Area of MER: %f. Ratio A/Ar is %f\n", d1*d2, x1/(d1*d2));
        return x1/(d1*d2);
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

/*      Compute the CENTRAL moments, which use center of mass as origin   */
/*      Return the central moment Mij for the region coded VAL            */

float central_moments (struct image *x, int i, int j, int val)
{
        int xx, yy, ii;
        int m00;
        float cx, cy, res;

/* Normalization requires that moments M00, M10 and M01 be computed */
        m00 = moments(x, 0, 0, val);
        ii = moments (x, 1, 0, val);
        cx = (float)ii/(float)m00;
        ii = moments (x, 0, 1, val);
        cy = (float)ii/(float)m00;

/* Sum of (x-cmx)**i * (y-cm)**j */
        res = 0;
        for (xx = 0; xx<x->info->nr; xx++)
           for (yy = 0; yy<x->info->nc; yy++) 
                if (x->data[xx][yy] == val) 
                        res += fpow(xx-cx,i)*fpow(yy-cy,j);
        return res;
}

/* Compute the specified moments for the region marked with VAL. */

int moments (struct image *x, int i, int j, int val)
{
        int xx, yy, m;
 
        m = 0;
/* Sum of x**i * y**j */
        for (xx = 0; xx<x->info->nr; xx++)
           for (yy = 0; yy<x->info->nc; yy++) 
                if (x->data[xx][yy] == val) 
                        m += ipow(xx,i)*ipow(yy,j);
        return m;
}

 /*      Is a real value close enough to zero?   */
 
 int is_zero (float x)
 {
         if ( (x <= 0.0001) && (x >= -0.0001) ) return 1;
         return 0;
 }
 
/*	Find the minimum enclosing rectangle that is oriented to the
	axis of the object, rather than the image. Do this by constructing
	a box that has sides parallel to the principal axis and minor
	axis and which encloses the object. return the corners of this box. */

void mer (struct image *x, int val, float *x1, float *y1)
{
	float cmi, cmj;		/* Center of mass */
	int i3,i4,i5,i6,j3,j4,j5,j6;
	float e1,e2,e3,e4,f1,f2,f3,f4,g1,g2,g3,g4;
	float e5,e6,f5,f6,g5,g6,xx,yy,ip1,ip2,jp1,jp2;

/* First we locate the principal axis; this defines the direction of
   the 'length' dimension, and is a straight line defined by 2 points */
	principal_axis (x, val, &ip1,&jp1,&ip2,&jp2);
	line2pt (ip1, jp1, ip2, jp2, &e1, &f1, &g1);

/* We now find the two pixels farthest (perpendicular) from the
   PA. One must be positive in distance, the other negative. These
   points will be (i3,j3) =+ve and (i4,j4)=-ve, and will lie on
   opposite sides of the MER.					*/

	minmax_dist (x, ip1,jp1,ip2,jp2, val, &i3,&j3,&i4,&j4);

/* Find the center of mass now. The line perpendicular to the PA
   through the CM will be the minor axis (MA).			*/

	center_of_mass (x, val, &cmi, &cmj);

/* Check for horizontal box. Have already solved that one. */
	if ((ip1-ip2)==0.0 || (jp1-jp2)==0) {
		printf ("MER: Orientation is horizontal.\n");
		box (x, val, x1,y1);
		return;
	}

/* Otherwise, the minor axis is perpendicular to the principal
   axis, and passes through the center of mass.		*/
	perp (e1, f1, g1, &e2, &f2, &g2, cmi, cmj);

/* L1 and L2 are lines forming opposite edges of MER parallel to PA   */
	g3 = -e1*i3-f1*j3; e3 = e1; f3 = f1;
	g4 = -e1*i4-f1*j4; e4 = e1; f4 = f1;

/* Locate point where MA and L1 intersect. */
	line_intersect (e2,f2,g2, e3,f3,g3, &xx, &yy);

/* And find the object pixels farthest from MA on each side */
	minmax_dist (x, cmi,cmj,xx,yy, val, &i5,&j5,&i6,&j6);

/* W1 and W2 are lines parallel to MA forming opposite edges of the MER */
	g5 = -e2*i5-f2*j5;	e5 = e2; f5 = f2;
	g6 = -e2*i6-f2*j6;	e6 = e2; f6 = f2;

/* Intersection of W1 with L1:  */
	line_intersect (e3,f3,g3, e5,f5,g5, &(x1[0]), &(y1[0]));
	printf ("UL corner is (%f,%f)\n", x1[0],y1[0]);
/* Intersection of W2 with L1:  */
	line_intersect (e3,f3,g3, e6,f6,g6, &(x1[1]), &(y1[1]));
	printf ("LL corner is (%f,%f)\n", x1[1],y1[1]);
/* Intersection of W2 with L2:  */
	line_intersect (e4,f4,g4, e6,f6,g6, &(x1[2]), &(y1[2]));
	printf ("LR corner is (%f,%f)\n", x1[2],y1[2]);
/* Intersection of W1 with L2:  */
	line_intersect (e4,f4,g4, e5,f5,g5, &(x1[3]), &(y1[3]));
	printf ("UR corner is (%f,%f)\n", x1[3],y1[3]);
}

/*	Convexity measures - Find the convex region enclosing the region
	marked VAL. Then find the ratios of the original to convex areas
	and perimeters; X1 is perimeter ration, X2 is area ratio.	 */


void convexity (struct image *im, int val, 
                float *x1, float *x2)
{
	int i,j,k;
	float y, z, p, a, b;
	int *rc, *cc;
	struct image *yy;

	z = y = 0.0;
	p = perimeter (im, val);
	a = (float)area (im, val);
	if ((a==0) || (p==0)) 
	   return;

/* Allocate arrays for row and column values of boundary pixels. */
	rc = (int *)malloc(((int)(p)+10)*sizeof(int));
	cc = (int *)malloc(((int)(p)+10)*sizeof(int));

/* Collect the boundary pixels into the arrays RC and CC 	*/
	k = 0;
	for (i=0; i<im->info->nr; i++) 
	   for (j=0; j<im->info->nc; j++) 
		if (im->data[i][j] == val) 
		  if (nay4(im,i,j,val) < 4) {
			rc[k] = i; cc[k++] = j;
		  }

/*	Compute the convex hull of the boundary pixels.		*/
	k = convex_hull (rc, cc, k);

/*	Fill the convex polygon that results				*/
	yy = 0;
	copy (&yy, im);
	filled_polygon (yy,rc, cc, k, val);
	remark(yy, val+1, val);

	b = (float)area(yy, val);
	if (b==0) 
	    return;
	z = perimeter (yy, val);
	freeimage (yy);
	*x2 = a/b;
	*x1 = p/z;
}

/*	Fill a polygon given by row and columns indices in arrays r and c.
	Fill with the value VAL.					  */

void filled_polygon (struct image *y, int *r, int *c, int n, int val)
{
	int i,j;

	DO_DRAW = 1;
	set_draw_val (val+1);
	for (i=0; i<n; i++) {
		line (y, r[i], c[i], r[i+1],c[i+1]);
	}
	for (i=0; i<y->info->nr; i++) 
	   for (j=0; j<y->info->nc; j++)
		if (y->data[i][j] == val) fill (y, i,j,val);
}

/*	Recursive fill of a region with VAL	*/

void fill (struct image *y, int i, int j, int val)
{
	if (range(y,i,j)) {
	  y->data[i][j] = val;
	  if (i+1 < y->info->nr)
	    if(y->data[i+1][j]!=val+1 && y->data[i+1][j]!=val) 
		fill (y, i+1,j,val);
	  if (i-1 >= 0)
	    if(y->data[i-1][j]!=val+1 && y->data[i-1][j]!=val) 
		fill (y, i-1,j,val);
	  if (j+1 < y->info->nc)
	    if(y->data[i][j+1]!=val+1 && y->data[i][j+1]!=val) 
		fill (y, i,j+1,val);
	  if (j-1 >= 0)
	    if(y->data[i][j-1]!=val+1 && y->data[i][j-1]!=val) 
		fill (y, i,j-1,val);
	}
}

double angle_2pt (int r1, int c1, int r2, int c2)
{
/*	Compute the angle between two points. (r1,c1) is the origin
	specified as row, column, and (r2,c2) is the second point.
	Result is between 0-360 degrees, where 0 is horizontal right. */

	double atan(), fabs();
	double x, dr, dc, conv;

	conv = 180.0/3.1415926535;
	dr = (double)(r2-r1); dc = (double)(c2-c1);

/*	Compute the raw angle based of Drow, Dcolumn		*/
	if (dr==0 && dc == 0) x = 0.0;
	else if (dc == 0) x = 90.0;
	else {
		x = fabs(atan (dr/dc));
		x = x * conv;
	}

/*	Adjust the angle according to the quadrant		*/
	if (dr <= 0) {			/* upper 2 quadrants */
	  if (dc < 0) x = 180.0 - x;	/* Left quadrant */
	} else if (dr > 0) {		/* Lower 2 quadrants */
	  if (dc < 0) x = x + 180.0;	/* Left quadrant */
	  else x = 360.0-x;		/* Right quadrant */
	}

	return x;
}

int convex_hull (int *rows, int *columns, int n)
{
/*	Determine the convex hull of the points given in row & column
	coordinates. There are N of them. Return the hull in the argument
	arrays, and the return value will be the number of points	*/

	int i,j,k;
	double angle_2pt(), prev, best, x;

/*	Find the pixel with the largest Row value		*/
	k = 0;
	for (i=1; i<n; i++)
	   if (rows[i] > rows[k]) k = i;
	   else if ((rows[i] == rows[k]) && (columns[i]<columns[k]))
		k = i;			/* Same row, choose leftmost */

/*	Bottom-most point is row[k], column[k]. This will
	be the first point in the convex hull.			*/
	hswap (rows, columns, k, 0);
	rows[n] = rows[0]; columns[n] = columns[0];

/*	The next point in the hull is always the point having the
	smallest angle measured from the previous point. The angles
	must increase as more pixels are added to the hull.		*/
	prev = -1.0;	j = 0;

	do {
	 best = 360.0;  k = -1;
	 for (i=j+1; i<=n; i++) {
	   x = angle_2pt (rows[j],columns[j], rows[i],columns[i]);
	   if ( (x>prev) && (x<best) ) {
	      k = i; best = x;
	   } else if ( (x>prev) && (x == best) ) {
	      if ( (abs(rows[i]-rows[j])+abs(columns[i]-columns[j])) >
		   (abs(rows[k]-rows[j])+abs(columns[k]-columns[j]))) {
		k = i; best = x;
	      }
	   }
	 }

	 if (k > 0) {
		prev = best;
		j = j + 1;
		hswap (rows, columns, k, j);
	 }
	} while (k>0 && (j<n));

	rows[j+1] = rows[0]; columns[j+1] =columns[0];
	return j+1;
}

 void principal_axis(struct image *x, int val, float *i1, float *j1, 
                     float *i2, float *j2)
 {
         int i,j, di,dj,k;
         struct image *y;
         float dmax,dd;
         float cmi, cmj;
 
 /* Locate center of mass */
         center_of_mass (x, val, &cmi, &cmj);
 
 /* Make a local copy of the image so it can be changed */
         y = 0;
	 copy  (&y, x);
 
         cmi = (float)( (int)cmi );      cmj = (float)( (int)cmj );
 
 /*      Mark candidate pixels: perimeter between 0-row CMI and col CMJ-max */
         for (i=0; i<=(int)(cmi+0.5); i++)
            for (j=(int)(cmj); j<x->info->nc; j++)
                 if (x->data[i][j] == val) {
                    if (nay4(x, i,j, val) != 4)
                       y->data[i][j] = val+1;
                 }
 
         dmax = 1.0e20;  di = -1;        dj = -1;
 
 /* The principal axis will pass through the center of mass. Consider
    all candidate pixels, determine the line through it and the COM,
    and sum the distance between the line an all pixels in the region */
         do {
            k = 0;
            for (i=0; i<=(int)(cmi+0.5); i++)
               for (j=(int)(cmj); j<x->info->nc; j++)
               if (y->data[i][j] == val+1) {
                 dd = all_dist(x, cmi,cmj, (float)i,(float)j, val);
                 if (dd < dmax) {
                         dmax = dd;
                         di = i; dj = j;
                         k += 1;
                 }
                 y->data[i][j] = val;
               }
         } while (k);
 
         *i1 = (float)di;        *j1 = (float)dj;
         *i2 = cmi;      *j2 = cmj;
         freeimage (y);
 }

 /*      Find the point where two lines intersect        */
 int line_intersect (float a1, float b1, float c1, float a2,
                     float b2, float c2, float *x, float *y)
 {
         float dt;
 
         dt = a2*b1 - a1*b2;
         if (is_zero(dt)) return 0;
 
         *y = fabs((a2*c1 - a1*c2)/dt);
         *x = (-b1/a1)*(*y) - c1/a1;
         return 1;
 }
 

 /* X to the J power */
 int ipow (int x, int j)
 {
         int i, r;
 
         r = 1;
         for (i=1; i<=j; i++) r = r * x;
         return r;
 }
 
 /* X to the J power, floating point */
 float fpow (float x, int j)
 {
         int i;
         float r;
 
         r = 1.0;
         for (i=1; i<=j; i++) r = r * x;
         return r;
 }

void set_draw_val (int a)
 {
         if (a<256 && a>=0) DRAW_VAL = a;
 }
 
/*      Swap row i with row j and column i with column j        */
 void hswap (int *rows, int *columns, int i, int j)
 {
         int t;
 
         t = rows[i]; rows[i] = rows[j]; rows[j] = t;
         t = columns[i]; columns[i] = columns[j]; columns[j] = t;
 }

 /*      Compute the coefficients of the equation of the line
         between (x1,y1) and (x2,y2): they are a,b and c.        */
 int line2pt (float x1, float y1, float x2, float y2, 
              float *a, float *b, float *c)
 {
         float dx, dy, dsq, dinv;
 
         *a = 0.0; *b = 0.0; *c = 0.0;
         dx = x2-x1;     dy = y2-y1;
         dsq = dx*dx + dy*dy;
         if (dsq < 1.0) return 0;
         dinv = -1.0/sqrt(dsq);
         *a = -dy*dinv;
         *b = dx*dinv;
         *c = (x1*y2 - x2*y1)*dinv;
         if (*c > 0) {
                 *a = -(*a);
                 *b = -(*b);
                 *c = -(*c);
         }
         return 1;
 }

 void line (struct image *im, int x1, int y1, int x2, int y2)
 {
         int  x, y, sigx, sigy;
         int absx, absy, d, dx, dy;
         int True = 1;
 
         dx = x2-x1;
         if (dx < 0) {
            absx = -dx;  sigx = -1;
         } else {
            absx = dx;   sigx = 1;
         }
         absx = absx << 1;
 
         dy = y2-y1;
         if (dy < 0) {
            absy = -dy;     sigy = -1;
         } else {
            absy = dy;      sigy = 1;
         }
         absy = absy << 1;
 
         x = x1; y = y1;
         if (absx > absy) 
	 {
           d = absy-(absx>>1);
           while (True) {
                 plot(im, x, y);
                 if (x==x2) return;
                 if (d>=0) {
                         y += sigy;
                         d -= absx;
                 }
                 x += sigx;
                 d += absy;
            }
         } else {
            d = absx-(absy>>1);
            while (True) {
               plot(im, x, y);
               if (y==y2) return;
               if (d>=0) {
                  x += sigx;
                  d -= absy;
               }
               y += sigy;
               d += absx;
            }
         }
 }

 /*      Set a pixel (x,y) to the plot value DRAW_VAL    */
 int plot(struct image *im, int x, int y)  
 {
         int ret;
 
         if (is_background(im->data[x][y]) == 0) ret = 1;
         else ret = 0;
         if (DO_DRAW == 1)
             im->data[x][y] = DRAW_VAL;
         return ret;
 }
 
 /*      Return TRUE (1) if the value I is the background value  */
 int is_background (int i)
 {
         if (i == BACKGROUND) return 1;
         return 0;
 }
 
/*      Return the coefficients of the line perpendicular to ax+by+c=0  */
 void perp (float a, float b, float c, float *a1, float *b1, 
            float *c1, float x, float y)
 {
         *a1 = b;        
         *b1 = -a;
         *c1 = a*y - b*x;
 }
 
/*      Compute distances between the line given and all pixels
         in the region; return pixels with min and Max distance  */
 float minmax_dist (struct image *x, float i1, float j1, float i2,
                float j2, int val, int *i3, int *j3, int *i4, int *j4)
 {
         int i,j;
         float a, b, c, e, f, dmax,dmin;
 
 /* Equation of the line is a*x + b*y + c = 0 */
         a = j2-j1;
         b = i1-i2;
         c =  -(i1-i2)*j1 + (j1-j2)*i1 ;
         e = a*a + b*b;
         dmax = 0.0;     dmin = 100000.0;
 
 /* Locate the pixels with the maximum and minimum residual */
         for (i=0; i<x->info->nr; i++)
            for (j=0; j<x->info->nc; j++) {
               if (x->data[i][j] != val)continue;
               f = (a*i + b*j + c);
               if (f < dmin) {
                 *i3 = i; *j3 = j;
                 dmin = f;
               }
               if (f > dmax) {
                 *i4 = i;        *j4 = j;
                 dmax = f;
               }
            }
 }

 /* Compute distance between the line given and all pixels in the region */
 /* Line is specified by two points: (i1,j1) and (i2,j2) */
 
 float all_dist (struct image *x, float i1, float j1,
                 float i2, float j2, int val)
 {
         int i,j;
         float a, b, c, e, f, d;
 
 /* Equation of the line is a*x + b*y + c = 0 */
         a = (float)j2-(float)j1;
         b = (float)i1-(float)i2;
         c = (float)( -(i1-i2)*j1 + (j1-j2)*i1 );
         e = a*a + b*b;
         d = 0.0;
 
 /* Sum the residuals, substituting (i,j) for each pixel in place of (x,y) */
         for (i=0; i<x->info->nr; i++)
            for (j=0; j<x->info->nc; j++) {
               if (x->data[i][j] != val)continue;
                 f = (a*i + b*j + c);
                 f = f*f/e;
                 d = d + f;
            }
         return d;
 }
 
