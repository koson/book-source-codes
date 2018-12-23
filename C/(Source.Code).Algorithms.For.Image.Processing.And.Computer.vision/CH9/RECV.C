/* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

		    Handprinted digit recognition
	
	This program uses scalable vector templates. An incoming digit
	image is matched against a template, which has been scaled to
	the same size and line width. The matching is done by a pixel
	to pixel minimum distance method, similar to maximizing the
	forces between pixels.

   - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

#define DEBUG 0

#include <stdio.h>
#define MAX 1
#include "lib.h"
#include <math.h>
#define BACKGROUND 255
 int DO_DRAW = 1;
 int DRAW_VAL = 1;

unsigned char buff[1000];
struct image *data;
struct image *digit;
int NR, NC;
int dwidth = 0;

struct template_record {
	int x1[24], y1[24], x2[24], y2[24];
	int n;
};
struct template_record *templates[10][10];
int num_templ[10] = {0,0,0,0,0,0,0,0,0,0};
int uli,ulj, lri,lrj, tarea, darea;
int escape = 0, actual = 0, rec = 0;

void init();
void load_vt (int dig, int tmp, char *fn);
void read_image(struct image **im);
  void read_ctype (struct image *im, int g); 
  void dump (struct image *im);                                            
int analyze(struct image *im);
  int width1 (struct image *im, struct image *x, struct image *gx); 
void scale_template (int idig, int itmp, struct image **im);
float match (struct image *x, struct image *im, int xarea);
  int find_min_dist (int i, int j, int *ii, int *jj, struct image *x);
  int dist8 (int i, int j, int ii, int jj);                                  
void findmax (struct image *x, int *ii, int *jj);         
void dumpr (struct image *x, int ii, int jj, int k);

float central_moments (struct image *x, int i, int j, int val);
int moments (struct image *x, int i, int j, int val);
void box(struct image *x, int val, float *x1, float *y1);
float fpow (float x, int j);
void extract (struct image *x, struct image **y, int val,  int *rm, int *cm);
void line (struct image *im, int x1, int y1, int x2, int y2);
void set_draw_val (int a);

int N,M;

int main(int argc, char *argv[])
{
	int i,j, dmin, js;
	float d[10],dd, means[10];
	FILE *ff;

/* Initialize the vector templates */
	init ();

/* Read a digit image */

        if (argc > 1)
        {
          data = Input_PBM (argv[1]);
          if (data == 0) 
          {
            printf ("Can't access the input image.\n");
            exit (1);
          }
        } else
        {
          printf ("recv <image>\n");
          printf (" Digit recognition using convex deficiencies.\n");
          exit (1);
        }

/* Reverse the levels, if needed */
         for (i=0; i<data->info->nr; i++)
           for (j=0; j<data->info->nc; j++)
             if (data->data[i][j] == 0) data->data[i][j] = 1;
             else data->data[i][j] = 0;
	darea = data->info->nr*data->info->nc;

/* Determine size and properties */
	dwidth = analyze(data);

/* Try to match against each template */
	dmin = 0; d[dmin] =  10000;
	js = -1;

	for (i=0; i<10; i++)  {
	  means[i] = 0.0;
	  for (j=0; j<num_templ[i]; j++) {
	    if (DEBUG) printf ("Scale and match digit %d template %d\n", i,j);
	    scale_template (i,j, &digit);  
	    if (escape) {
		dmin = 1;
		break;
	    }
	    dd = match(digit, data, darea);

/* Experiment - match both ways, use sum */
	    dd += match (data, digit, darea);

	    means[i] += dd;
	    if (dd < d[dmin]) {
	      dmin = i;
	      d[i] = dd;
	      js = j;
	    }
	  }
	  means[i] = means[i]/num_templ[i];
	}

	printf ("Min distance metric:\n");
	if (tarea>darea)
	  printf ("THE DIGIT WAS '%d' template %d > \n", dmin, js);
	else
	  printf ("THE DIGIT WAS '%d' template %d < \n", dmin, js);

}

void dump (struct image  *im)
{
	int i,j;

	if (DEBUG) {
	  for (i=0; i<76; i++){
	    for (j=0; j<72; j++)
		if (im->data[i][j]==0) printf ("#");
		 else printf ("%1d", im->data[i][j]%10);
	    printf ("\n");
	  }
	}
}

int analyze(struct image *im)
{
	int i,j,k,ii,jj,err=0;
	struct image *y=0;

/* Compute the width of the stroke */        
	if(DEBUG) printf ("Analyzing ...\n");
	k = width2 (im);
	if(DEBUG) printf ("Estmated width is %d pixels\n", k);

/* Compute the upper left and lower right coordinates */
	extract (im, &y, 0, &ii, &jj);

/* Copy from Y to im, then delete Y */
	for (i=0; i<im->info->nr; i++)
	  for (j=0; j<im->info->nc; j++)
	    im->data[i][j] = 1;
	for (i=0; i<y->info->nr; i++)
	  for (j=0; j<y->info->nc; j++) {
	    im->data[i][j] = y->data[i][j];
	    if (y->data[i][j] == 0) {
	      if (i > lri) lri = i;
	      if (j > lrj) lrj = j;
	    }
	  }
	freeimage (y);
	uli = 1; ulj = 1;
	if(DEBUG) printf ("Upper left (%d,%d)  Lower right (%d,%d)\n",uli,ulj,lri,lrj);
	
	dump(im);
	return k;
}

/*      Estimate the stroke width using horizontal/vertical projections */
int width2 (struct image *x)
{       
	int i,j,k, hist[100];

	for (i=0; i<100; i++) hist[i] = 0;

	if(DEBUG) printf ("WIDTH2: Histogram initialized - scanning the rows...\n");
	for (i=0; i<x->info->nr; i++) {
	  j = 0;
	  do {
	    while (j<x->info->nc && x->data[i][j] != 0) j++;
	    if (j >= x->info->nc) break;
	    k = j;
	    while (j<x->info->nc && x->data[i][j]==0) j++;
	    if (j>=x->info->nc) break;
	    k = j-k;
	    hist[k] += 1;
	  } while (j < x->info->nc);
	}

	if(DEBUG) printf ("Scanning the columns ...\n");
	for (j=0; j<x->info->nc; j++) {
	  i = 0;
	  do {
	    while (i<x->info->nr && x->data[i][j] != 0) i++;
	    if (i >= x->info->nr) break;
	    k = i;
	    while (i<x->info->nr && x->data[i][j]==0) i++;
	    if (i>=x->info->nr) break;
	    k = i-k;
	    hist[k] += 1;
	  } while (i < x->info->nr);
	}

	k=0;
	for (i=0; i<100; i++)
	  if (hist[i] > hist[k]) k=i;

	return k;
}


void scale_template(int idig, int itmp, struct image **im)
{
	int i,j,k, err, nr, nc, ii, jj, w, a1, a2;
	double si, sj, dw, m02, m20;
	struct template_record *t;
	struct image *distim;

	printf ("Digit %d template %d  ", idig, itmp);
/* Create a new image, if necessary */
	if (*im == 0) *im = newimage(data->info->nr, data->info->nc );
	else err = 0;

/* Clear the image */
	for (i=0; i<data->info->nr; i++)
	  for (j=0; j<data->info->nc; j++)
	    (*im)->data[i][j] = 1;

/* Now scale the vectors in template (idig,itmp) */
	nr = lri-uli; nc = lrj-ulj;
	w = dwidth/2;
	if(DEBUG) printf ("Height is %d  width is %d  Line width is %d\n", nr, nc, w);
	si = (double)(nr-dwidth-1)/9.0;
	sj = (double)(nc-dwidth-1)/9.0;
	printf ("si/sj=%f  width=%d nr=%d nc=%d\n", si/sj, dwidth, nr, nc);
	if (si/sj >= 3.5) {
		escape = 1;
		return;
	}
	if (nc <= dwidth*3+2) {
                 escape = 1;
                 return;
	}
	if (si/sj >= 2.0) {
		k = 0;
		m02 = (double)central_moments (data, 0,2, 0);
		m20 = (double)central_moments (data, 2,0, 0);
		if (m20/m02 > 10.0) {
		  escape = 1;
		  return;
		}
	}
	if(DEBUG) printf ("Template scaling: %lf by %lf \n", si, sj);

/* Draw the vectors into the image 'im' */
	set_draw_val (0);
	dw = dwidth/2.0 + 1.0;
	t = templates[idig][itmp];
	for (k=0; k<t->n; k++) 
#if defined (PC)
	  draw_line ((*im), (int)((double)(t->x1[k])*si+dw), 
			    (int)((double)(t->y1[k])*sj+dw),
			    (int)((double)(t->x2[k])*si+dw), 
			    (int)((double)(t->y2[k])*sj+dw) );
#else          
	  line ((*im), (int)((double)(t->x1[k])*si+dw), 
		       (int)((double)(t->y1[k])*sj+dw),
		       (int)((double)(t->x2[k])*si+dw), 
		       (int)((double)(t->y2[k])*sj+dw) );
#endif
	if(DEBUG) printf ("Template [%d/%d]: \n", idig, itmp);


/* Grow each pixel to the appropriate width */
	for (i=0; i<lri; i++)
	  for (j=0; j<lrj; j++) 
	    if ((*im)->data[i][j] == 0) {
	      for (ii=i-w; ii<=i+w; ii++)
		for (jj=j-w; jj<=j+w; jj++)
		  if (ii>=0 && jj>= 0) 
		    (*im)->data[ii][jj] = 2;
	    }

	for (i=0; i<(*im)->info->nr; i++)
	  for (j=0; j<(*im)->info->nc; j++) 
	    if ((*im)->data[i][j] == 2) (*im)->data[i][j] = 0;
	 dump (*im);        
}

/*      Return the coordinates of the largest entry in X        */
void findmax (struct image *x, int *ii, int *jj)
{
	int i,j;

	*ii = 0; *jj = 0;
	for (i=0; i<x->info->nr; i++)
	  for (j=0; j<x->info->nc; j++) 
	    if (x->data[i][j] > 0) {
	      if (*ii == 0) {
		*ii = i; *jj = j;
	      } else if (x->data[*ii][*jj] < x->data[i][j]) {
		*ii = i; *jj = j;
	      }
	    }
}

/*      Find the pixel in X closest to (i,j) in digit. Return ii,jj     */

int find_min_dist (int x, int y, int *ii, int *jj, struct image *im)
{
	int i,j,k, maxk;

/* Search a growing region around (i,j) for a 0 pixel */
	*ii = 0; *jj = 0; maxk = im->info->nr/2;

	for (k=0; k<maxk; k++) {
	  for (i=x-k; i<=x+k; i++) {
	    if(range(im,i,y-k)) 
	      if (im->data[i][y-k] == 0) {
		*ii = i; *jj = y-k; 
		return k;
	      }
	    if (range(im, i, y+k))
	      if (im->data[i][y+k] == 0) {
		*ii = i; *jj = y+k;
		return k;
	      }
	  }

	  for (j=y-k; j<=y+k; j++) {
	    if (range(im, x-k, j))
	      if (im->data[x-k][j] == 0) {
		*ii = x-k; *jj = j;
		return k;
	      }
	    if (range(im,x+k, j))
	      if (im->data[x+k][j] == 0) {
		*ii = x+k; *jj = j;
		return k;
	      }
	  }
	}
}

/*      Print a small region of an image        */
void dumpr (struct image *x, int ii, int jj, int k)
{
	int i,j;

	if (DEBUG == 0) return;
	printf ("    ");
	for (i=jj-k; i<jj+k; i++) if(i>=0) printf ("%2d ", i);
	printf ("\n    ----------------------------------------------\n");
	for (i=ii-k; i<ii+k; i++) {
	  if (range(x,i,1)) {
	   printf ("%2d: ", i);
	   for (j=jj-k; j<jj+k; j++)
		if (range(x,i,j)) printf ("%2d ", x->data[i][j]%100);
	   printf ("\n");
	  }
	}
}

/*      Compute the 8-distance between (i,j) and (ii,jj)        */

int dist8 (int i, int j, int ii, int jj)
{
	int a, b;
	a = abs (jj-j);
	b = abs (ii-i);
	if (a<b) return b;
	return a;
}

float match (struct image *x, struct image *im, int xarea)
{
	struct image *mapi, *mapj;
	struct image *dist;
	int i,j,k,ii,jj, d, nd1, nd2, NR, NC;

	NR = lri; NC = lrj;
	mapi = 0;
	copy (&mapi,x);
	mapj = 0;
	copy (&mapj,x);
	dist = 0;
	copy (&dist,x);

/* Locate the obvious overlap pixels */
	for (i=0; i<NR; i++) 
	  for (j=0; j<NC; j++)
	    if (im->data[i][j] == 0 && x->data[i][j]==0) {
	      mapi->data[i][j] = i;
	      mapj->data[i][j] = j;
	      dist->data[i][j] = 0;
	    } else if (im->data[i][j] == 0) dist->data[i][j] = 255;
	    else dist->data[i][j] = 0;
/* Now, pixels where DIST(i,j) == 0 are 0 in both template and data images,
   and have a mapij entry. All others have DIST=255, and no map entry.      */

/* First guess at the minimum distance map */
	d = 0;
	for (i=0; i<NR; i++)
	  for (j=0; j<NC; j++)
	    if (dist->data[i][j] == 255) {
	      dist->data[i][j] = find_min_dist (i, j, &ii, &jj, x);
	      mapi->data[i][j] = ii;
	      mapj->data[i][j] = jj;
	      d += dist->data[i][j];
	    }
	if(DEBUG) printf ("First guess total distance is %d\n", d);

/*      dump(dist);     */
/* Now, MAPi and MAPj are a pixel mapping from im to x; that is, if (i,j)
/*   is a pixel =0 in IM, then (mapi[i,j], mapj[i,j]) is the nearest pixel
/*   to it in X. DIST[i,j] is the distance between (i,j) and (ii,jj).   */
/*              */
/* Now try to improve (minimize) D by swapping pixels having non-zero   */
/*   distance measures. If the swap improves total distance, do it       */
/*      for (i=0; i<NR; i++)    */
/*        for (j=0; j<NC; j++) {        */
/*          if (dist->data[i][j] == 0) continue;        */
/*          for (ii=i; ii<NR; ii++)     */
/*            for (jj=0; jj<NC; jj++) { */
/*              if (dist->data[ii][jj] == 0) continue;  */
/*              if (ii==i && jj==j) continue;   */
/*      */
/* Compute the new distance if (i,j) and (ii,jj) are swapped */
/*              nd1 = dist8(i,j, mapi->data[ii][jj], mapj->data[ii][jj]);       */
/*              nd2 = dist8(ii,jj,mapi->data[i][j], mapj->data[i][j]);  */
/*              if(DEBUG) printf ("Swap attempt (%d,%d)%d (%d,%d)%d gives %d %d\n",     */
/*                      i,j,dist->data[i][j], ii,jj,dist->data[ii][jj], */
/*                      nd1, nd2);      */
/*      */
/*              if (dist->data[i][j]+dist->data[ii][jj] > nd1+nd2) {    */
/*                if(DEBUG) printf ("SWAP\n");  */
/*                d = d - dist->data[ii][jj] - dist->data[i][j];        */
/*                d = d + nd1 + nd2;    */
/*                dist->data[ii][jj] = nd2;     */
/*                dist->data[i][j] = nd1;       */
/*                k = mapi->data[ii][jj]; mapi->data[ii][jj] = mapi->data[i][j]; mapi->data[i][j]=k;    */
/*                k = mapj->data[ii][jj]; mapj->data[ii][jj] = mapj->data[i][j]; mapj->data[i][j]=k;    */
/*              }       */
/*            } */
/*        }     */

	printf ("Best distance is %d (%f per pixel):\n", d, (float)d/(float)xarea);
	dump (dist);

	freeimage (mapi);
	freeimage (mapj);
	freeimage (dist);
	return (float)d/(float)xarea;
}

/*      Initialize - Construct all template records.            */

void init ()
{
	int i,j,k;

	for (i=0; i<10; i++) {
	  num_templ[i] = 0;
	  for (j=0; j<10; j++) 
	    templates[i][j] = (struct template_record *)0;
	}

/* Templates for zero 0 0 0 0 */
	templates[0][0] = 
	  (struct template_record *)malloc (sizeof(struct template_record));
	templates[0][0]->x1[0] = 0; templates[0][0]->y1[0] = 2;
	templates[0][0]->x2[0] = 0; templates[0][0]->y2[0] = 7;
	templates[0][0]->x1[1] = 0; templates[0][0]->y1[1] = 7;
	templates[0][0]->x2[1] = 2; templates[0][0]->y2[1] = 9;
	templates[0][0]->x1[2] = 2; templates[0][0]->y1[2] = 9;
	templates[0][0]->x2[2] = 7; templates[0][0]->y2[2] = 9;
	templates[0][0]->x1[3] = 7; templates[0][0]->y1[3] = 9;
	templates[0][0]->x2[3] = 9; templates[0][0]->y2[3] = 7;
	templates[0][0]->x1[4] = 9; templates[0][0]->y1[4] = 7;
	templates[0][0]->x2[4] = 9; templates[0][0]->y2[4] = 2;
	templates[0][0]->x1[5] = 9; templates[0][0]->y1[5] = 2;
	templates[0][0]->x2[5] = 7; templates[0][0]->y2[5] = 0;
	templates[0][0]->x1[6] = 7; templates[0][0]->y1[6] = 0;
	templates[0][0]->x2[6] = 2; templates[0][0]->y2[6] = 0;
	templates[0][0]->x1[7] = 2; templates[0][0]->y1[7] = 0;
	templates[0][0]->x2[7] = 0; templates[0][0]->y2[7] = 2;
	templates[0][0]->n = 8;
	num_templ[0] = 1;

/* Templates for one 1 1 1 1 */
/*
	templates[1][1] = 
	  (struct template_record *)malloc (sizeof(struct template_record));
	templates[1][1]->x1[0] = 0; templates[1][1]->y1[0] = 1;
	templates[1][1]->x2[0] = 9; templates[1][1]->y2[0] = 1;
	templates[1][1]->n = 1;
*/


	templates[1][0] = 
	  (struct template_record *)malloc (sizeof(struct template_record));
	templates[1][0]->x1[0] = 0; templates[1][0]->y1[0] = 9;
	templates[1][0]->x2[0] = 9; templates[1][0]->y2[0] = 9;
	templates[1][0]->x1[1] = 0; templates[1][0]->y1[1] = 9;
	templates[1][0]->x2[1] = 1; templates[1][0]->y2[1] = 0;
	templates[1][0]->n = 2;

/*
	templates[1][2] = 
	  (struct template_record *)malloc (sizeof(struct template_record));
	templates[1][2]->x1[0] = 0; templates[1][2]->y1[0] = 5;
	templates[1][2]->x2[0] = 9; templates[1][2]->y2[0] = 5;
	templates[1][2]->x1[1] = 1; templates[1][2]->y1[1] = 3;
	templates[1][2]->x2[1] = 0; templates[1][2]->y2[1] = 5;
	templates[1][2]->x1[2] = 9; templates[1][2]->y1[2] = 0;
	templates[1][2]->x2[2] = 9; templates[1][2]->y2[2] = 9;
	templates[1][2]->n = 3;
*/

	num_templ[1] = 1;

/* Templates for two 2 2 2 2 */
	templates[2][0] = 
	  (struct template_record *)malloc (sizeof(struct template_record));
	templates[2][0]->x1[0] = 3; templates[2][0]->y1[0] = 0;
	templates[2][0]->x2[0] = 0; templates[2][0]->y2[0] = 3;
	templates[2][0]->x1[1] = 0; templates[2][0]->y1[1] = 3;
	templates[2][0]->x2[1] = 0; templates[2][0]->y2[1] = 6;
	templates[2][0]->x1[2] = 0; templates[2][0]->y1[2] = 6;
	templates[2][0]->x2[2] = 3; templates[2][0]->y2[2] = 9;
	templates[2][0]->x1[3] = 3; templates[2][0]->y1[3] = 9;
	templates[2][0]->x2[3] = 9; templates[2][0]->y2[3] = 0;
	templates[2][0]->x1[4] = 9; templates[2][0]->y1[4] = 0;
	templates[2][0]->x2[4] = 9; templates[2][0]->y2[4] = 9;
	templates[2][0]->n = 5;
 
	templates[2][1] = 
	  (struct template_record *)malloc (sizeof(struct template_record));
	templates[2][1]->x1[0] = 2; templates[2][1]->y1[0] = 0;
	templates[2][1]->x2[0] = 0; templates[2][1]->y2[0] = 2;
	templates[2][1]->x1[1] = 0; templates[2][1]->y1[1] = 2;
	templates[2][1]->x2[1] = 0; templates[2][1]->y2[1] = 6;
	templates[2][1]->x1[2] = 0; templates[2][1]->y1[2] = 6;
	templates[2][1]->x2[2] = 2; templates[2][1]->y2[2] = 9;
	templates[2][1]->x1[3] = 2; templates[2][1]->y1[3] = 9;
	templates[2][1]->x2[3] = 7; templates[2][1]->y2[3] = 1;
	templates[2][1]->x1[4] = 7; templates[2][1]->y1[4] = 1;
	templates[2][1]->x2[4] = 9; templates[2][1]->y2[4] = 1;
	templates[2][1]->x1[5] = 9; templates[2][1]->y1[5] = 1;
	templates[2][1]->x2[5] = 9; templates[2][1]->y2[5] = 9;
	templates[2][1]->n = 6;
 
	templates[2][2] = 
	  (struct template_record *)malloc (sizeof(struct template_record));
	templates[2][2]->x1[0] = 2; templates[2][2]->y1[0] = 3;
	templates[2][2]->x2[0] = 1; templates[2][2]->y2[0] = 3;
	templates[2][2]->x1[1] = 1; templates[2][2]->y1[1] = 3;
	templates[2][2]->x2[1] = 0; templates[2][2]->y2[1] = 4;
	templates[2][2]->x1[2] = 0; templates[2][2]->y1[2] = 4;
	templates[2][2]->x2[2] = 0; templates[2][2]->y2[2] = 7;
	templates[2][2]->x1[3] = 0; templates[2][2]->y1[3] = 7;
	templates[2][2]->x2[3] = 2; templates[2][2]->y2[3] = 9;
	templates[2][2]->x1[4] = 2; templates[2][2]->y1[4] = 9;
	templates[2][2]->x2[4] = 8; templates[2][2]->y2[4] = 3;
	templates[2][2]->x1[5] = 9; templates[2][2]->y1[5] = 0;
	templates[2][2]->x2[5] = 6; templates[2][2]->y2[5] = 9;
	templates[2][2]->n = 6;
	num_templ[2] = 3;

/* Templates for three 3 3 3 3 */

	templates[3][0] = 
	  (struct template_record *)malloc (sizeof(struct template_record));
	templates[3][0]->x1[0] = 0; templates[3][0]->y1[0] = 0;
	templates[3][0]->x2[0] = 0; templates[3][0]->y2[0] = 9;
	templates[3][0]->x1[1] = 0; templates[3][0]->y1[1] = 9;
	templates[3][0]->x2[1] = 9; templates[3][0]->y2[1] = 9;
	templates[3][0]->x1[2] = 9; templates[3][0]->y1[2] = 9;
	templates[3][0]->x2[2] = 9; templates[3][0]->y2[2] = 0;
	templates[3][0]->x1[3] = 4; templates[3][0]->y1[3] = 5;
	templates[3][0]->x2[3] = 4; templates[3][0]->y2[3] = 9;
	templates[3][0]->n = 4;
	num_templ[3] = 1;

/* Templates for four  4 4 4 4 4 */
        templates[4][0] = 
          (struct template_record *)malloc (sizeof(struct template_record));
         templates[4][0]->x1[0] = 0;  templates[4][0]->y1[0] = 4;
         templates[4][0]->x2[0] = 1;  templates[4][0]->y2[0] = 4;
         templates[4][0]->x1[1] = 6;  templates[4][0]->y1[1] = 3;
         templates[4][0]->x2[1] = 1;  templates[4][0]->y2[1] = 4;
         templates[4][0]->x1[2] = 1;  templates[4][0]->y1[2] = 4;
         templates[4][0]->x2[2] = 6;  templates[4][0]->y2[2] = 0;
         templates[4][0]->x1[3] = 6;  templates[4][0]->y1[3] = 0;
         templates[4][0]->x2[3] = 6;  templates[4][0]->y2[3] = 0;
         templates[4][0]->x1[4] = 6;  templates[4][0]->y1[4] = 0;
         templates[4][0]->x2[4] = 6;  templates[4][0]->y2[4] = 3;
         templates[4][0]->x1[5] = 6;  templates[4][0]->y1[5] = 3;
         templates[4][0]->x2[5] = 6;  templates[4][0]->y2[5] = 3;
         templates[4][0]->x1[6] = 6;  templates[4][0]->y1[6] = 4;
         templates[4][0]->x2[6] = 6;  templates[4][0]->y2[6] = 9;
         templates[4][0]->x1[7] = 6;  templates[4][0]->y1[7] = 3;
         templates[4][0]->x2[7] = 6;  templates[4][0]->y2[7] = 3;
         templates[4][0]->x1[8] = 6;  templates[4][0]->y1[8] = 3;
         templates[4][0]->x2[8] = 9;  templates[4][0]->y2[8] = 3;
         templates[4][0]->n = 9;

/* 445 */
        templates[4][1] = 
          (struct template_record *)malloc (sizeof(struct template_record));
         templates[4][1]->x1[0] = 0;  templates[4][1]->y1[0] = 3;
         templates[4][1]->x2[0] = 6;  templates[4][1]->y2[0] = 0;
         templates[4][1]->x1[1] = 6;  templates[4][1]->y1[1] = 0;
         templates[4][1]->x2[1] = 7;  templates[4][1]->y2[1] = 5;
         templates[4][1]->x1[2] = 0;  templates[4][1]->y1[2] = 7;
         templates[4][1]->x2[2] = 6;  templates[4][1]->y2[2] = 5;
         templates[4][1]->x1[3] = 6;  templates[4][1]->y1[3] = 9;
         templates[4][1]->x2[3] = 6;  templates[4][1]->y2[3] = 6;
         templates[4][1]->x1[4] = 6;  templates[4][1]->y1[4] = 5;
         templates[4][1]->x2[4] = 6;  templates[4][1]->y2[4] = 5;
         templates[4][1]->x1[5] = 7;  templates[4][1]->y1[5] = 5;
         templates[4][1]->x2[5] = 9;  templates[4][1]->y2[5] = 5;
         templates[4][1]->n = 6;

	templates[4][2] = 
	  (struct template_record *)malloc (sizeof(struct template_record));
	templates[4][2]->x1[0] = 1; templates[4][2]->y1[0] = 0;
	templates[4][2]->x2[0] = 5; templates[4][2]->y2[0] = 0;
	templates[4][2]->x1[1] = 5; templates[4][2]->y1[1] = 0;
	templates[4][2]->x2[1] = 5; templates[4][2]->y2[1] = 9;
	templates[4][2]->x1[2] = 0; templates[4][2]->y1[2] = 6;
	templates[4][2]->x2[2] = 9; templates[4][2]->y2[2] = 6;
	templates[4][2]->n = 3;

	templates[4][3] = 
	  (struct template_record *)malloc (sizeof(struct template_record));
	templates[4][3]->x1[0] = 0; templates[4][3]->y1[0] = 4;
	templates[4][3]->x2[0] = 5; templates[4][3]->y2[0] = 0;
	templates[4][3]->x1[1] = 5; templates[4][3]->y1[1] = 0;
	templates[4][3]->x2[1] = 5; templates[4][3]->y2[1] = 9;
	templates[4][3]->x1[2] = 0; templates[4][3]->y1[2] = 8;
	templates[4][3]->x2[2] = 9; templates[4][3]->y2[2] = 8;
	templates[4][3]->n = 3;

/* A GENVT template */
	templates[4][4] = 
	  (struct template_record *)malloc (sizeof(struct template_record));
	 templates[4][4]->x1[0] = 0;  templates[4][4]->y1[0] = 1;
	 templates[4][4]->x2[0] = 5;  templates[4][4]->y2[0] = 0;
	 templates[4][4]->x1[1] = 5;  templates[4][4]->y1[1] = 0;
	 templates[4][4]->x2[1] = 6;  templates[4][4]->y2[1] = 4;
	 templates[4][4]->x1[2] = 0;  templates[4][4]->y1[2] = 4;
	 templates[4][4]->x2[2] = 6;  templates[4][4]->y2[2] = 4;
	 templates[4][4]->x1[3] = 6;  templates[4][4]->y1[3] = 4;
	 templates[4][4]->x2[3] = 6;  templates[4][4]->y2[3] = 4;
	 templates[4][4]->x1[4] = 6;  templates[4][4]->y1[4] = 4;
	 templates[4][4]->x2[4] = 6;  templates[4][4]->y2[4] = 9;
	 templates[4][4]->x1[5] = 9;  templates[4][4]->y1[5] = 4;
	 templates[4][4]->x2[5] = 6;  templates[4][4]->y2[5] = 4;
	 templates[4][4]->x1[6] = 6;  templates[4][4]->y1[6] = 4;
	 templates[4][4]->x2[6] = 6;  templates[4][4]->y2[6] = 4;
	 templates[4][4]->n = 7;

	num_templ[4] = 5;

/* Templates for five 5 5 5 5 */
	templates[5][0] = 
	  (struct template_record *)malloc (sizeof(struct template_record));
	templates[5][0]->x1[0] = 0; templates[5][0]->y1[0] = 1;
	templates[5][0]->x2[0] = 4; templates[5][0]->y2[0] = 1;
	templates[5][0]->x1[1] = 1; templates[5][0]->y1[1] = 1;
	templates[5][0]->x2[1] = 1; templates[5][0]->y2[1] = 9;
	templates[5][0]->x1[2] = 4; templates[5][0]->y1[2] = 1;
	templates[5][0]->x2[2] = 4; templates[5][0]->y2[2] = 6;
	templates[5][0]->x1[3] = 4; templates[5][0]->y1[3] = 6;
	templates[5][0]->x2[3] = 6; templates[5][0]->y2[3] = 9;
	templates[5][0]->x1[4] = 6; templates[5][0]->y1[4] = 9;
	templates[5][0]->x2[4] = 9; templates[5][0]->y2[4] = 6;
	templates[5][0]->x1[5] = 9; templates[5][0]->y1[5] = 6;
	templates[5][0]->x2[5] = 9; templates[5][0]->y2[5] = 2;
	templates[5][0]->x1[6] = 9; templates[5][0]->y1[6] = 2;
	templates[5][0]->x2[6] = 7; templates[5][0]->y2[6] = 0;
	templates[5][0]->n = 7;

/* 573 */
        templates[5][1] = 
          (struct template_record *)malloc (sizeof(struct template_record));
         templates[5][1]->x1[0] = 0;  templates[5][1]->y1[0] = 1;
         templates[5][1]->x2[0] = 1;  templates[5][1]->y2[0] = 2;
         templates[5][1]->x1[1] = 1;  templates[5][1]->y1[1] = 2;
         templates[5][1]->x2[1] = 2;  templates[5][1]->y2[1] = 9;
         templates[5][1]->x1[2] = 2;  templates[5][1]->y1[2] = 2;
         templates[5][1]->x2[2] = 2;  templates[5][1]->y2[2] = 1;
         templates[5][1]->x1[3] = 2;  templates[5][1]->y1[3] = 1;
         templates[5][1]->x2[3] = 4;  templates[5][1]->y2[3] = 1;
         templates[5][1]->x1[4] = 4;  templates[5][1]->y1[4] = 1;
         templates[5][1]->x2[4] = 5;  templates[5][1]->y2[4] = 2;
         templates[5][1]->x1[5] = 5;  templates[5][1]->y1[5] = 2;
         templates[5][1]->x2[5] = 5;  templates[5][1]->y2[5] = 4;
         templates[5][1]->x1[6] = 5;  templates[5][1]->y1[6] = 4;
         templates[5][1]->x2[6] = 6;  templates[5][1]->y2[6] = 6;
         templates[5][1]->x1[7] = 9;  templates[5][1]->y1[7] = 0;
         templates[5][1]->x2[7] = 8;  templates[5][1]->y2[7] = 0;
         templates[5][1]->x1[8] = 6;  templates[5][1]->y1[8] = 6;
         templates[5][1]->x2[8] = 6;  templates[5][1]->y2[8] = 6;
         templates[5][1]->x1[9] = 9;  templates[5][1]->y1[9] = 4;
         templates[5][1]->x2[9] = 9;  templates[5][1]->y2[9] = 0;
         templates[5][1]->x1[10] = 6;  templates[5][1]->y1[10] = 6;
         templates[5][1]->x2[10] = 6;  templates[5][1]->y2[10] = 6;
         templates[5][1]->x1[11] = 7;  templates[5][1]->y1[11] = 6;
         templates[5][1]->x2[11] = 9;  templates[5][1]->y2[11] = 4;
         templates[5][1]->n = 12;

/* 558 */
        templates[5][2] = 
          (struct template_record *)malloc (sizeof(struct template_record));
         templates[5][2]->x1[0] = 9;  templates[5][2]->y1[0] = 5;
         templates[5][2]->x2[0] = 8;  templates[5][2]->y2[0] = 0;
         templates[5][2]->x1[1] = 0;  templates[5][2]->y1[1] = 9;
         templates[5][2]->x2[1] = 0;  templates[5][2]->y2[1] = 3;
         templates[5][2]->x1[2] = 0;  templates[5][2]->y1[2] = 2;
         templates[5][2]->x2[2] = 4;  templates[5][2]->y2[2] = 2;
         templates[5][2]->x1[3] = 4;  templates[5][2]->y1[3] = 2;
         templates[5][2]->x2[3] = 4;  templates[5][2]->y2[3] = 7;
         templates[5][2]->x1[4] = 4;  templates[5][2]->y1[4] = 7;
         templates[5][2]->x2[4] = 7;  templates[5][2]->y2[4] = 8;
         templates[5][2]->x1[5] = 7;  templates[5][2]->y1[5] = 8;
         templates[5][2]->x2[5] = 9;  templates[5][2]->y2[5] = 5;
         templates[5][2]->n = 6;
	num_templ[5] = 3;

/* Templates for siz 6 6 6 6 */

/* A GENVT template (699) */
	templates[6][1] = 
	  (struct template_record *)malloc (sizeof(struct template_record));
	 templates[6][1]->x1[0] = 0;  templates[6][1]->y1[0] = 6;
	 templates[6][1]->x2[0] = 2;  templates[6][1]->y2[0] = 1;
	 templates[6][1]->x1[1] = 2;  templates[6][1]->y1[1] = 1;
	 templates[6][1]->x2[1] = 3;  templates[6][1]->y2[1] = 1;
	 templates[6][1]->x1[2] = 3;  templates[6][1]->y1[2] = 1;
	 templates[6][1]->x2[2] = 4;  templates[6][1]->y2[2] = 0;
	 templates[6][1]->x1[3] = 4;  templates[6][1]->y1[3] = 0;
	 templates[6][1]->x2[3] = 8;  templates[6][1]->y2[3] = 0;
	 templates[6][1]->x1[4] = 8;  templates[6][1]->y1[4] = 0;
	 templates[6][1]->x2[4] = 9;  templates[6][1]->y2[4] = 2;
	 templates[6][1]->x1[5] = 9;  templates[6][1]->y1[5] = 2;
	 templates[6][1]->x2[5] = 8;  templates[6][1]->y2[5] = 6;
	 templates[6][1]->x1[6] = 8;  templates[6][1]->y1[6] = 6;
	 templates[6][1]->x2[6] = 6;  templates[6][1]->y2[6] = 9;
	 templates[6][1]->x1[7] = 6;  templates[6][1]->y1[7] = 9;
	 templates[6][1]->x2[7] = 4;  templates[6][1]->y2[7] = 8;
	 templates[6][1]->x1[8] = 4;  templates[6][1]->y1[8] = 8;
	 templates[6][1]->x2[8] = 3;  templates[6][1]->y2[8] = 6;
	 templates[6][1]->x1[9] = 3;  templates[6][1]->y1[9] = 6;
	 templates[6][1]->x2[9] = 3;  templates[6][1]->y2[9] = 2;
	 templates[6][1]->n = 10;

/* A GENVT template */
	templates[6][0] = 
	  (struct template_record *)malloc (sizeof(struct template_record));
	templates[6][0]->x1[0] = 0;  templates[6][0]->y1[0] = 6;
	templates[6][0]->x2[0] = 2;  templates[6][0]->y2[0] = 2;
	templates[6][0]->x1[1] = 2;  templates[6][0]->y1[1] = 2;
	templates[6][0]->x2[1] = 4;  templates[6][0]->y2[1] = 1;
	templates[6][0]->x1[2] = 5;  templates[6][0]->y1[2] = 1;
	templates[6][0]->x2[2] = 7;  templates[6][0]->y2[2] = 0;
	templates[6][0]->x1[3] = 7;  templates[6][0]->y1[3] = 0;
	templates[6][0]->x2[3] = 9;  templates[6][0]->y2[3] = 2;
	templates[6][0]->x1[4] = 9;  templates[6][0]->y1[4] = 2;
	templates[6][0]->x2[4] = 8;  templates[6][0]->y2[4] = 7;
	templates[6][0]->x1[5] = 8;  templates[6][0]->y1[5] = 7;
	templates[6][0]->x2[5] = 6;  templates[6][0]->y2[5] = 9;
	templates[6][0]->x1[6] = 6;  templates[6][0]->y1[6] = 9;
	templates[6][0]->x2[6] = 5;  templates[6][0]->y2[6] = 5;
	templates[6][0]->x1[7] = 5;  templates[6][0]->y1[7] = 5;
	templates[6][0]->x2[7] = 5;  templates[6][0]->y2[7] = 1;
	templates[6][0]->n = 8;

	num_templ[6] = 2;

/* Templates for seven 7 7 7 7 */
	templates[7][0] = 
	  (struct template_record *)malloc (sizeof(struct template_record));
	templates[7][0]->x1[0] = 0; templates[7][0]->y1[0] = 0;
	templates[7][0]->x2[0] = 0; templates[7][0]->y2[0] = 9;
	templates[7][0]->x1[1] = 0; templates[7][0]->y1[1] = 9;
	templates[7][0]->x2[1] = 9; templates[7][0]->y2[1] = 4;
	templates[7][0]->n = 2;
	
	templates[7][1] = 
	  (struct template_record *)malloc (sizeof(struct template_record));
	templates[7][1]->x1[0] = 0; templates[7][1]->y1[0] = 0;
	templates[7][1]->x2[0] = 0; templates[7][1]->y2[0] = 9;
	templates[7][1]->x1[1] = 0; templates[7][1]->y1[1] = 9;
	templates[7][1]->x2[1] = 9; templates[7][1]->y2[1] = 4;
	templates[7][1]->x1[2] = 0; templates[7][1]->y1[2] = 0;
	templates[7][1]->x2[2] = 1; templates[7][1]->y2[2] = 0;
	templates[7][1]->n = 3;

	templates[7][2] = 
	  (struct template_record *)malloc (sizeof(struct template_record));
	templates[7][2]->x1[0] = 0; templates[7][2]->y1[0] = 0;
	templates[7][2]->x2[0] = 0; templates[7][2]->y2[0] = 9;
	templates[7][2]->x1[1] = 0; templates[7][2]->y1[1] = 9;
	templates[7][2]->x2[1] = 9; templates[7][2]->y2[1] = 4;
	templates[7][2]->x1[2] = 0; templates[7][2]->y1[2] = 0;
	templates[7][2]->x2[2] = 3; templates[7][2]->y2[2] = 2;
	templates[7][2]->n = 3;

/* 740 */
        templates[7][3] = 
          (struct template_record *)malloc (sizeof(struct template_record));
         templates[7][3]->x1[0] = 0;  templates[7][3]->y1[0] = 9;
         templates[7][3]->x2[0] = 9;  templates[7][3]->y2[0] = 7;
         templates[7][3]->x1[1] = 3;  templates[7][3]->y1[1] = 0;
         templates[7][3]->x2[1] = 0;  templates[7][3]->y2[1] = 0;
         templates[7][3]->x1[2] = 0;  templates[7][3]->y1[2] = 0;
         templates[7][3]->x2[2] = 0;  templates[7][3]->y2[2] = 9;
         templates[7][3]->n = 3;
/* 747 */
/*
        templates[7][3] = 
          (struct template_record *)malloc (sizeof(struct template_record));
         templates[7][3]->x1[0] = 0;  templates[7][3]->y1[0] = 9;
         templates[7][3]->x2[0] = 9;  templates[7][3]->y2[0] = 7;
         templates[7][3]->x1[1] = 2;  templates[7][3]->y1[1] = 0;
         templates[7][3]->x2[1] = 0;  templates[7][3]->y2[1] = 1;
         templates[7][3]->x1[2] = 0;  templates[7][3]->y1[2] = 1;
         templates[7][3]->x2[2] = 0;  templates[7][3]->y2[2] = 7;
         templates[7][3]->x1[3] = 0;  templates[7][3]->y1[3] = 7;
         templates[7][3]->x2[3] = 0;  templates[7][3]->y2[3] = 9;
         templates[7][3]->n = 4;
*/
	num_templ[7] = 4;


/* Templates for eight 8 8 8 8 */

/* 826 */
        templates[8][0] = 
          (struct template_record *)malloc (sizeof(struct template_record));
         templates[8][0]->x1[0] = 0;  templates[8][0]->y1[0] = 6;
         templates[8][0]->x2[0] = 0;  templates[8][0]->y2[0] = 2;
         templates[8][0]->x1[1] = 0;  templates[8][0]->y1[1] = 2;
         templates[8][0]->x2[1] = 1;  templates[8][0]->y2[1] = 0;
         templates[8][0]->x1[2] = 1;  templates[8][0]->y1[2] = 0;
         templates[8][0]->x2[2] = 3;  templates[8][0]->y2[2] = 0;
         templates[8][0]->x1[3] = 3;  templates[8][0]->y1[3] = 0;
         templates[8][0]->x2[3] = 4;  templates[8][0]->y2[3] = 5;
         templates[8][0]->x1[4] = 4;  templates[8][0]->y1[4] = 5;
         templates[8][0]->x2[4] = 7;  templates[8][0]->y2[4] = 0;
         templates[8][0]->x1[5] = 7;  templates[8][0]->y1[5] = 0;
         templates[8][0]->x2[5] = 8;  templates[8][0]->y2[5] = 0;
         templates[8][0]->x1[6] = 8;  templates[8][0]->y1[6] = 0;
         templates[8][0]->x2[6] = 9;  templates[8][0]->y2[6] = 2;
         templates[8][0]->x1[7] = 9;  templates[8][0]->y1[7] = 2;
         templates[8][0]->x2[7] = 9;  templates[8][0]->y2[7] = 6;
         templates[8][0]->x1[8] = 9;  templates[8][0]->y1[8] = 6;
         templates[8][0]->x2[8] = 7;  templates[8][0]->y2[8] = 9;
         templates[8][0]->x1[9] = 7;  templates[8][0]->y1[9] = 9;
         templates[8][0]->x2[9] = 5;  templates[8][0]->y2[9] = 8;
         templates[8][0]->x1[10] = 5;  templates[8][0]->y1[10] = 8;
         templates[8][0]->x2[10] = 4;  templates[8][0]->y2[10] = 6;
         templates[8][0]->x1[11] = 0;  templates[8][0]->y1[11] = 7;
         templates[8][0]->x2[11] = 0;  templates[8][0]->y2[11] = 9;
         templates[8][0]->x1[12] = 0;  templates[8][0]->y1[12] = 9;
         templates[8][0]->x2[12] = 2;  templates[8][0]->y2[12] = 9;
         templates[8][0]->x1[13] = 2;  templates[8][0]->y1[13] = 9;
         templates[8][0]->x2[13] = 3;  templates[8][0]->y2[13] = 6;
         templates[8][0]->x1[14] = 4;  templates[8][0]->y1[14] = 5;
         templates[8][0]->x2[14] = 4;  templates[8][0]->y2[14] = 5;
         templates[8][0]->n = 15;

/* 847 */
        templates[8][1] = 
          (struct template_record *)malloc (sizeof(struct template_record));
         templates[8][1]->x1[0] = 4;  templates[8][1]->y1[0] = 4;
         templates[8][1]->x2[0] = 1;  templates[8][1]->y2[0] = 0;
         templates[8][1]->x1[1] = 1;  templates[8][1]->y1[1] = 0;
         templates[8][1]->x2[1] = 0;  templates[8][1]->y2[1] = 2;
         templates[8][1]->x1[2] = 0;  templates[8][1]->y1[2] = 2;
         templates[8][1]->x2[2] = 0;  templates[8][1]->y2[2] = 6;
         templates[8][1]->x1[3] = 0;  templates[8][1]->y1[3] = 6;
         templates[8][1]->x2[3] = 0;  templates[8][1]->y2[3] = 8;
         templates[8][1]->x1[4] = 0;  templates[8][1]->y1[4] = 8;
         templates[8][1]->x2[4] = 3;  templates[8][1]->y2[4] = 7;
         templates[8][1]->x1[5] = 3;  templates[8][1]->y1[5] = 7;
         templates[8][1]->x2[5] = 4;  templates[8][1]->y2[5] = 5;
         templates[8][1]->x1[6] = 4;  templates[8][1]->y1[6] = 4;
         templates[8][1]->x2[6] = 7;  templates[8][1]->y2[6] = 1;
         templates[8][1]->x1[7] = 7;  templates[8][1]->y1[7] = 1;
         templates[8][1]->x2[7] = 8;  templates[8][1]->y2[7] = 1;
         templates[8][1]->x1[8] = 8;  templates[8][1]->y1[8] = 1;
         templates[8][1]->x2[8] = 9;  templates[8][1]->y2[8] = 3;
         templates[8][1]->x1[9] = 9;  templates[8][1]->y1[9] = 3;
         templates[8][1]->x2[9] = 9;  templates[8][1]->y2[9] = 7;
         templates[8][1]->x1[10] = 9;  templates[8][1]->y1[10] = 7;
         templates[8][1]->x2[10] = 8;  templates[8][1]->y2[10] = 9;
         templates[8][1]->x1[11] = 8;  templates[8][1]->y1[11] = 9;
         templates[8][1]->x2[11] = 7;  templates[8][1]->y2[11] = 9;
         templates[8][1]->x1[12] = 7;  templates[8][1]->y1[12] = 9;
         templates[8][1]->x2[12] = 4;  templates[8][1]->y2[12] = 5;
         templates[8][1]->x1[13] = 4;  templates[8][1]->y1[13] = 4;
         templates[8][1]->x2[13] = 4;  templates[8][1]->y2[13] = 4;
         templates[8][1]->n = 14;

/* 816 */
        templates[8][2] = 
          (struct template_record *)malloc (sizeof(struct template_record));
         templates[8][2]->x1[0] = 5;  templates[8][2]->y1[0] = 3;
         templates[8][2]->x2[0] = 3;  templates[8][2]->y2[0] = 0;
         templates[8][2]->x1[1] = 3;  templates[8][2]->y1[1] = 0;
         templates[8][2]->x2[1] = 2;  templates[8][2]->y2[1] = 0;
         templates[8][2]->x1[2] = 2;  templates[8][2]->y1[2] = 0;
         templates[8][2]->x2[2] = 0;  templates[8][2]->y2[2] = 3;
         templates[8][2]->x1[3] = 0;  templates[8][2]->y1[3] = 3;
         templates[8][2]->x2[3] = 0;  templates[8][2]->y2[3] = 6;
         templates[8][2]->x1[4] = 0;  templates[8][2]->y1[4] = 6;
         templates[8][2]->x2[4] = 1;  templates[8][2]->y2[4] = 9;
         templates[8][2]->x1[5] = 1;  templates[8][2]->y1[5] = 9;
         templates[8][2]->x2[5] = 2;  templates[8][2]->y2[5] = 9;
         templates[8][2]->x1[6] = 2;  templates[8][2]->y1[6] = 9;
         templates[8][2]->x2[6] = 5;  templates[8][2]->y2[6] = 5;
         templates[8][2]->x1[7] = 5;  templates[8][2]->y1[7] = 4;
         templates[8][2]->x2[7] = 5;  templates[8][2]->y2[7] = 5;
         templates[8][2]->x1[8] = 5;  templates[8][2]->y1[8] = 5;
         templates[8][2]->x2[8] = 6;  templates[8][2]->y2[8] = 8;
         templates[8][2]->x1[9] = 6;  templates[8][2]->y1[9] = 8;
         templates[8][2]->x2[9] = 8;  templates[8][2]->y2[9] = 8;
         templates[8][2]->x1[10] = 8;  templates[8][2]->y1[10] = 8;
         templates[8][2]->x2[10] = 9;  templates[8][2]->y2[10] = 6;
         templates[8][2]->x1[11] = 9;  templates[8][2]->y1[11] = 6;
         templates[8][2]->x2[11] = 9;  templates[8][2]->y2[11] = 3;
         templates[8][2]->x1[12] = 9;  templates[8][2]->y1[12] = 3;
         templates[8][2]->x2[12] = 8;  templates[8][2]->y2[12] = 1;
         templates[8][2]->x1[13] = 8;  templates[8][2]->y1[13] = 1;
         templates[8][2]->x2[13] = 5;  templates[8][2]->y2[13] = 3;
         templates[8][2]->n = 14;

/* 850 */
        templates[8][3] = 
          (struct template_record *)malloc (sizeof(struct template_record));
         templates[8][3]->x1[0] = 4;  templates[8][3]->y1[0] = 6;
         templates[8][3]->x2[0] = 1;  templates[8][3]->y2[0] = 9;
         templates[8][3]->x1[1] = 1;  templates[8][3]->y1[1] = 9;
         templates[8][3]->x2[1] = 0;  templates[8][3]->y2[1] = 6;
         templates[8][3]->x1[2] = 0;  templates[8][3]->y1[2] = 6;
         templates[8][3]->x2[2] = 0;  templates[8][3]->y2[2] = 4;
         templates[8][3]->x1[3] = 0;  templates[8][3]->y1[3] = 4;
         templates[8][3]->x2[3] = 0;  templates[8][3]->y2[3] = 2;
         templates[8][3]->x1[4] = 0;  templates[8][3]->y1[4] = 2;
         templates[8][3]->x2[4] = 2;  templates[8][3]->y2[4] = 0;
         templates[8][3]->x1[5] = 2;  templates[8][3]->y1[5] = 0;
         templates[8][3]->x2[5] = 2;  templates[8][3]->y2[5] = 0;
         templates[8][3]->x1[6] = 2;  templates[8][3]->y1[6] = 0;
         templates[8][3]->x2[6] = 4;  templates[8][3]->y2[6] = 4;
         templates[8][3]->x1[7] = 4;  templates[8][3]->y1[7] = 5;
         templates[8][3]->x2[7] = 4;  templates[8][3]->y2[7] = 5;
         templates[8][3]->x1[8] = 4;  templates[8][3]->y1[8] = 5;
         templates[8][3]->x2[8] = 4;  templates[8][3]->y2[8] = 5;
         templates[8][3]->x1[9] = 4;  templates[8][3]->y1[9] = 6;
         templates[8][3]->x2[9] = 5;  templates[8][3]->y2[9] = 8;
         templates[8][3]->x1[10] = 5;  templates[8][3]->y1[10] = 8;
         templates[8][3]->x2[10] = 7;  templates[8][3]->y2[10] = 9;
         templates[8][3]->x1[11] = 7;  templates[8][3]->y1[11] = 9;
         templates[8][3]->x2[11] = 9;  templates[8][3]->y2[11] = 5;
         templates[8][3]->x1[12] = 9;  templates[8][3]->y1[12] = 5;
         templates[8][3]->x2[12] = 9;  templates[8][3]->y2[12] = 2;
         templates[8][3]->x1[13] = 9;  templates[8][3]->y1[13] = 2;
         templates[8][3]->x2[13] = 8;  templates[8][3]->y2[13] = 0;
         templates[8][3]->x1[14] = 8;  templates[8][3]->y1[14] = 0;
         templates[8][3]->x2[14] = 6;  templates[8][3]->y2[14] = 1;
         templates[8][3]->x1[15] = 6;  templates[8][3]->y1[15] = 1;
         templates[8][3]->x2[15] = 4;  templates[8][3]->y2[15] = 4;
         templates[8][3]->n = 16;

	num_templ[8] = 4;

/* Templates for nine 9 9 9 9 */
/* A GENVT Template */

	templates[9][0] = 
	  (struct template_record *)malloc (sizeof(struct template_record));
	templates[9][0]->x1[0] = 4;  templates[9][0]->y1[0] = 6;
	templates[9][0]->x2[0] = 2;  templates[9][0]->y2[0] = 9;
	templates[9][0]->x1[1] = 2;  templates[9][0]->y1[1] = 9;
	templates[9][0]->x2[1] = 0;  templates[9][0]->y2[1] = 8;
	templates[9][0]->x1[2] = 0;  templates[9][0]->y1[2] = 8;
	templates[9][0]->x2[2] = 0;  templates[9][0]->y2[2] = 6;
	templates[9][0]->x1[3] = 0;  templates[9][0]->y1[3] = 6;
	templates[9][0]->x2[3] = 0;  templates[9][0]->y2[3] = 2;
	templates[9][0]->x1[4] = 0;  templates[9][0]->y1[4] = 2;
	templates[9][0]->x2[4] = 1;  templates[9][0]->y2[4] = 0;
	templates[9][0]->x1[5] = 1;  templates[9][0]->y1[5] = 0;
	templates[9][0]->x2[5] = 4;  templates[9][0]->y2[5] = 0;
	templates[9][0]->x1[6] = 4;  templates[9][0]->y1[6] = 0;
	templates[9][0]->x2[6] = 4;  templates[9][0]->y2[6] = 2;
	templates[9][0]->x1[7] = 4;  templates[9][0]->y1[7] = 2;
	templates[9][0]->x2[7] = 4;  templates[9][0]->y2[7] = 5;
	templates[9][0]->x1[8] = 4;  templates[9][0]->y1[8] = 6;
	templates[9][0]->x2[8] = 5;  templates[9][0]->y2[8] = 6;
	templates[9][0]->x1[9] = 5;  templates[9][0]->y1[9] = 6;
	templates[9][0]->x2[9] = 9;  templates[9][0]->y2[9] = 4;
	templates[9][0]->n = 10;

        templates[9][1] = 
          (struct template_record *)malloc (sizeof(struct template_record));
        templates[9][1]->x1[0] = 5;  templates[9][1]->y1[0] = 6;
        templates[9][1]->x2[0] = 2;  templates[9][1]->y2[0] = 9;
        templates[9][1]->x1[1] = 2;  templates[9][1]->y1[1] = 9;
        templates[9][1]->x2[1] = 0;  templates[9][1]->y2[1] = 8;
        templates[9][1]->x1[2] = 0;  templates[9][1]->y1[2] = 8;
        templates[9][1]->x2[2] = 0;  templates[9][1]->y2[2] = 6;
        templates[9][1]->x1[3] = 0;  templates[9][1]->y1[3] = 6;
        templates[9][1]->x2[3] = 1;  templates[9][1]->y2[3] = 1;
        templates[9][1]->x1[4] = 1;  templates[9][1]->y1[4] = 1;
        templates[9][1]->x2[4] = 3;  templates[9][1]->y2[4] = 0;
        templates[9][1]->x1[5] = 3;  templates[9][1]->y1[5] = 0;
        templates[9][1]->x2[5] = 5;  templates[9][1]->y2[5] = 2;
        templates[9][1]->x1[6] = 5;  templates[9][1]->y1[6] = 2;
        templates[9][1]->x2[6] = 5;  templates[9][1]->y2[6] = 5;
        templates[9][1]->x1[7] = 5;  templates[9][1]->y1[7] = 6;
        templates[9][1]->x2[7] = 9;  templates[9][1]->y2[7] = 6;
        templates[9][1]->n = 8;

/* 937 */
        templates[9][2] = 
          (struct template_record *)malloc (sizeof(struct template_record));
         templates[9][2]->x1[0] = 5;  templates[9][2]->y1[0] = 7;
         templates[9][2]->x2[0] = 3;  templates[9][2]->y2[0] = 9;
         templates[9][2]->x1[1] = 3;  templates[9][2]->y1[1] = 9;
         templates[9][2]->x2[1] = 1;  templates[9][2]->y2[1] = 9;
         templates[9][2]->x1[2] = 1;  templates[9][2]->y1[2] = 9;
         templates[9][2]->x2[2] = 0;  templates[9][2]->y2[2] = 7;
         templates[9][2]->x1[3] = 0;  templates[9][2]->y1[3] = 7;
         templates[9][2]->x2[3] = 0;  templates[9][2]->y2[3] = 3;
         templates[9][2]->x1[4] = 0;  templates[9][2]->y1[4] = 3;
         templates[9][2]->x2[4] = 3;  templates[9][2]->y2[4] = 0;
         templates[9][2]->x1[5] = 3;  templates[9][2]->y1[5] = 0;
         templates[9][2]->x2[5] = 5;  templates[9][2]->y2[5] = 2;
         templates[9][2]->x1[6] = 5;  templates[9][2]->y1[6] = 2;
         templates[9][2]->x2[6] = 5;  templates[9][2]->y2[6] = 6;
         templates[9][2]->x1[7] = 5;  templates[9][2]->y1[7] = 7;
         templates[9][2]->x2[7] = 9;  templates[9][2]->y2[7] = 7;
         templates[9][2]->n = 8;

/* 958 */
        templates[9][3] = 
          (struct template_record *)malloc (sizeof(struct template_record));
         templates[9][3]->x1[0] = 3;  templates[9][3]->y1[0] = 7;
         templates[9][3]->x2[0] = 2;  templates[9][3]->y2[0] = 9;
         templates[9][3]->x1[1] = 2;  templates[9][3]->y1[1] = 9;
         templates[9][3]->x2[1] = 0;  templates[9][3]->y2[1] = 9;
         templates[9][3]->x1[2] = 0;  templates[9][3]->y1[2] = 9;
         templates[9][3]->x2[2] = 0;  templates[9][3]->y2[2] = 7;
         templates[9][3]->x1[3] = 0;  templates[9][3]->y1[3] = 7;
         templates[9][3]->x2[3] = 0;  templates[9][3]->y2[3] = 2;
         templates[9][3]->x1[4] = 0;  templates[9][3]->y1[4] = 2;
         templates[9][3]->x2[4] = 1;  templates[9][3]->y2[4] = 0;
         templates[9][3]->x1[5] = 1;  templates[9][3]->y1[5] = 0;
         templates[9][3]->x2[5] = 2;  templates[9][3]->y2[5] = 0;
         templates[9][3]->x1[6] = 2;  templates[9][3]->y1[6] = 0;
         templates[9][3]->x2[6] = 3;  templates[9][3]->y2[6] = 1;
         templates[9][3]->x1[7] = 3;  templates[9][3]->y1[7] = 1;
         templates[9][3]->x2[7] = 3;  templates[9][3]->y2[7] = 6;
         templates[9][3]->x1[8] = 3;  templates[9][3]->y1[8] = 7;
         templates[9][3]->x2[8] = 9;  templates[9][3]->y2[8] = 7;
         templates[9][3]->n = 9;
	num_templ[9] = 4;

}


/*	Load a temporary vector template from a file. For testing
	the usefulness of new templates					*/

void load_vt (int dig, int tmp, char *fn)
{
	int i,j,k;
	FILE *xx;

	xx = fopen (fn, "r");
	if (xx == NULL) {
		printf ("NO SUCH TEMPLATE FILE: '%s'\n\n", fn);
		exit (1);
	}

        templates[dig][tmp] = 
          (struct template_record *)malloc (sizeof(struct template_record));
	fscanf (xx, "%d", &k);			/* Number of vectors */
	for (i=0; i<k; i++) {
	   fscanf (xx, "%d", &(templates[dig][tmp]->x1[i]));
	   fscanf (xx, "%d", &(templates[dig][tmp]->y1[i]));
	   fscanf (xx, "%d", &(templates[dig][tmp]->x2[i]));
	   fscanf (xx, "%d", &(templates[dig][tmp]->y2[i]));
	}
        templates[dig][tmp]->n = k;
	fclose (xx);
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
 
