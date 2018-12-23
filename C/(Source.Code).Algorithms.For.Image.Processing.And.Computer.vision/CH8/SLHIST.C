/* Slope histogram VIA vectorization */


#define MAX
#include "lib.h"
#include <math.h>

#define OBJECT 1
#define BACK 0
#define MAXPL 12
#define MAXVL 12
#define VDT 1

struct pixlist {
	int N, n;
	int **list;
};
typedef struct pixlist * PIXLIST;
PIXLIST allpix[MAXPL], plist;

struct vle {
	int r1,c1,r2,c2;
	struct vle *next;
};
typedef struct vle * VECLIST;
VECLIST allvecs[MAXVL], vlist;
int NEXTV = 1;
IMAGE mim;

void bound (IMAGE im);
void extract (IMAGE im, PIXLIST *p);
void vectorize (PIXLIST p, VECLIST *v);
PIXLIST newp (int N);
void chain (IMAGE x, int I, int J, PIXLIST c, int pmax, int *nn);
int nay4 (IMAGE im, int I, int J);
VECLIST newv ();
void outlist (VECLIST p);
float checkd (PIXLIST p, int start, int end);
int ptldist (int ax, int ay, int bx, int by, int px, int py);
void vmark (PIXLIST p, int st, int en);
void slh (float angle, float length, float *hist);
double angle_2pt (int r1, int c1, int r2, int c2);

void main (int argc, char *argv[])
{
	IMAGE im, im2;
	char text[128];
	int i, j, k;
	FILE *f;
	float hist[9], l, a, vleng;

	if (argc < 2)
	{
	  printf ("Usage: %s <image> \n", argv[0]);
	  exit (1);
	}

	im = Input_PBM (argv[1]);
	if (im == 0)
	{
	  printf ("The image file '%s' does not exist, or is unreadable.\n");
	  exit (2);
	}
	mim = newimage (im->info->nr, im->info->nc);
	for (i=0; i<im->info->nr; i++)
	  for (j=0; j<im->info->nc; j++)
	    mim->data[i][j] = 0;

/* Identify the boundary */
	bound (im);

	k = 0;

/* Extract the boundary */
	  extract (im, &plist);
	  if (plist->n <= 0) exit(1);

/*
	  printf ("Boundary:\n");
	  for (i=0; plist->list[0][i]>=0; i++)
	    printf ("[%3d, %3d]\n", plist->list[0][i], plist->list[1][i]);
*/

/* Vectorize */
/*        printf ("Vector segments:\n"); */
	  vectorize (plist, &vlist);
/*        outlist (vlist); */

	  allvecs[k++] = vlist;

/* Compute the slope histogram from the vectors */
	for (i=0; i<8; i++)
	  hist[i] = 0.0;
	
	vleng = 0.0;
	for (i=1; plist->list[0][i]>=0; i++)
	{
	  a = angle_2pt(plist->list[0][i-1],plist->list[1][i-1], 
		plist->list[0][i],plist->list[1][i]);
	  l = (float)sqrt((double)((plist->list[0][i-1]-plist->list[0][i])*
		(plist->list[0][i-1]-plist->list[0][i])) +
		((plist->list[1][i-1]-plist->list[1][i])*
		 (plist->list[1][i-1]-plist->list[1][i]) ));
	  slh (a, l, hist);
	  vleng += l;
	}
	if (vleng > 0.0)
	  for (i=0; i<8; i++)
	    printf ("%d :: %10.4f\n", i, hist[i]/vleng);
}

void slh (float angle, float length, float *hist)
{
	int i,j, a;

/* Quantize all possible angles into only eight. Each bin
   holds 22.5 degrees, 11.25 degree on each side of the central angle.
   Angles should be the same MOD 180; that is 0 degrees = 180 degrees,
   also = 360 degrees.                                          */

	if (angle > 180) angle = angle - 180.0;
	if (angle < 22.5) a = 0;
	else if (angle < 45.0) a = 1;
	else if (angle < 67.5) a = 2;
	else if (angle < 90.0) a = 3;
	else if (angle < 112.5) a= 4;
	else if (angle < 135.0) a = 5;
	else if (angle < 157.5) a = 6;
	else if (angle < 180.0) a = 7;
	hist[a] += length;
}

/* Delete pixels that are NOT on an object outline (boundary) */
void bound (IMAGE im)
{
	int i,j;
	IMAGE x;

	x = newimage (im->info->nr, im->info->nc);
	for (i=0; i<im->info->nr; i++)
	  for (j=0; j<im->info->nc; j++)
	    x->data[i][j] = im->data[i][j];

	for (i=0; i<x->info->nr; i++)
	  for (j=0; j<x->info->nc; j++)
	    if (x->data[i][j] == OBJECT && nay4(x, i, j) == 4)
		im->data[i][j] = BACK;

	freeimage (x);
}

int nay4 (IMAGE im, int I, int J)
{
	int n=0;

	if ( (I-1 >= 0) && im->data[I-1][J]==OBJECT) n++;
	if ( (J-1 >= 0) && im->data[I][J-1]==OBJECT) n++;
	if ( (I+1 < im->info->nr) && im->data[I+1][J]==OBJECT) n++;
	if ( (J+1 < im->info->nc) && im->data[I][J+1]==OBJECT) n++;
	return n;
}

/* Convert the boundary image into a list of pixels */
void extract (IMAGE im, PIXLIST *p)
{
	int i, j, k, nn;
	PIXLIST q;

	k = im->info->nr * im->info->nc;
	q = newp (k);

	for (i=0; i<im->info->nr; i++)
	  for (j=0; j<im->info->nc; j++)
	    if (im->data[i][j] == OBJECT)
	    {
		chain (im, i, j, q, k, &nn);
		*p = q;
		q->n = nn;
		return;
	    }
}

/* Convert the list of pixels into a set of line segments */
void vectorize (PIXLIST p,  VECLIST *v)
{
	int i,j, si;
	VECLIST w, lw;

	lw = w = newv ();
	
	i = 0;
	while (i<p->n)
	{
	  si = i++;
	  if (p->list[0][i] < 0) break;
	  do
	  {
	    i++;
	    if (p->list[0][i]<0 || checkd (p, si, i) > VDT)
	    {
	      i--;
	      w->r1 = p->list[0][si]; w->c1 = p->list[1][si];
	      w->r2 = p->list[0][i];  w->c2 = p->list[1][i];
	      vmark (p, si, i);

/*      printf ("Vector: [%3d,%3d]->[%3d,%3d].\n",w->r1,w->c1,w->r2,w->c2); */

	      w->next = newv();
	      w = w->next;
	      w->next = 0; w->r1=w->r2=w->c1=w->c2 = -1;
	      break;
	    }
	  } while (i<p->n);
	}
	*v = lw;
}

void vmark (PIXLIST p, int st, int en)
{
	int i,j;

	for (i=st; i<=en; i++)
	  mim->data[p->list[0][i]][p->list[1][i]] = NEXTV;
	NEXTV++;
}

float checkd (PIXLIST p, int start, int end)
{
	int i;
	float d, dmax=0.0;

/*      printf ("Checking points %d thru %d. Line [%d,%d]->[%d,%d]\n",
		start, end, p->list[0][start], p->list[1][start],
		p->list[0][end], p->list[1][end]); */

	for (i=start+1; i<end; i++)
	{
	  d = (float)ptldist (p->list[0][start], p->list[1][start], 
		      p->list[0][end], p->list[1][end],
		      p->list[0][i], p->list[1][i]);
	  if (d > dmax) dmax = d;
	}
/*      printf ("Max distance seen was %f\n", dmax); */

	return dmax;
}

/* Perpendicular distance of the point (px,py) from the line between the
   two points (ax,ay) and (bx, by).                                     */

int ptldist (int ax, int ay, int bx, int by, int px, int py)
{
	int vp[2], vl[2],t,d,vb[2],s,v;
	int vr[2];

	vp[0] = px-ax;  vp[1] = py-ay;
	vl[0] = bx-ax;  vl[1] = by-ay;
	t = vp[0]*vl[0] + vp[1]*vl[1];
	if (t<0) d = vp[0]*vp[0] + vp[1]*vp[1];
	else {
	  s = vl[0]*vl[0] + vl[1]*vl[1];
	  if (t>s || s<= 0) {
	    vb[0] = px-bx;      vb[1] = py-by;
	    d = vb[0]*vb[0] + vb[1]*vb[1];
	  } else {
	    vr[0] = vp[0] - t*vl[0]/s;  
	    vr[1] = vp[1] - t*vl[1]/s;
	    vr[0] = vp[0]*vp[0] -(2*vp[0]*t*vl[0])/s + 
		(t*t*vl[0]*vl[0])/(s*s);
	    vr[1] = vp[1]*vp[1] -(2*vp[1]*t*vl[1])/s + 
		(t*t*vl[1]*vl[1])/(s*s);
	    return vr[0]+vr[1];
	  }
	}
	return d;
}

/*      Compute the chain code of the object beginning at pixel (i,j).
	Return the code as NN integers in the array C.                  */

void chain (IMAGE x, int I, int J, PIXLIST c, int pmax, int *nn)
{
	int val,n,m,q,r, di[9],dj[9],ii, d, dii;
	int lastdir, jj, i;

/*      Table given index offset for each of the 8 directions.          */
	di[0] = 0;      di[1] = -1;     di[2] = -1;     di[3] = -1;
	dj[0] = 1;      dj[1] = 1;      dj[2] = 0;      dj[3] = -1;
	di[4] = 0;      di[5] = 1;      di[6] = 1;      di[7] = 1;
	dj[4] = -1;     dj[5] = -1;     dj[6] = 0;      dj[7] = 1;

	for (ii=0; ii<pmax; ii++)
	{
	  c->list[0][ii] = -1;
	  c->list[1][ii] = -1;
	}
	val = x->data[I][J];
	q = I;  r = J;  lastdir = 4;

	c->list[0][0] = q;
	c->list[1][0] = r;
	n = 1;

	do
	{
	  m = 0;
	  dii = -1;     d = 100;
	  for (ii=lastdir+1; ii<lastdir+8; ii++) 
	  {     /* Look for next */
	    jj = ii%8;
	    if (range(x,di[jj]+q, dj[jj]+r))
	      if ( x->data[di[jj]+q][dj[jj]+r] == val) 
	      {
		   dii = jj;    m = 1;
		   break;
		} 
	  }

	  if (m)        /* Found a next pixel ... */
	  {
	    q += di[dii];       r += dj[dii];
	    if (n<pmax)
	    {
		c->list[0][n] = q;
		c->list[1][n] = r;
		n++;
	    }
	    lastdir = (dii+5)%8;
	  } else break; /* NO next pixel */
	  if (n>pmax) break;
	} while ( (q!=I) || (r!=J) );   /* Stop when next to start pixel */

	for (i=0; i<n; i++)
	  x->data[c->list[0][i]][c->list[1][i]] = BACK;
	*nn = n;
}

PIXLIST newp (int N)
{
	PIXLIST x;

	x = (PIXLIST)malloc (sizeof (struct pixlist));
	x->N = N;
	x->n = 0;
	x->list = (int **)malloc (sizeof(int *) * 2);
	x->list[0] = (int *)malloc (sizeof(int)*N);
	x->list[1] = (int *)malloc (sizeof(int)*N);
	return x;
}

VECLIST newv ()
{
	VECLIST x;

	x = (VECLIST) malloc (sizeof(struct vle));
	x->next = 0; x->r1 = x->r2 = x->c1 = x->c2 = -1;
	return x;
}

void outlist (VECLIST p)
{
	VECLIST q;

	q = p;
	while (q->next)
	{
	  printf ("[%3d,%3d] -> [%3d, %3d]\n", q->r1,q->c1,q->r2,q->c2);
	  q = q->next;
	}
}

double angle_2pt (int r1, int c1, int r2, int c2)
{
/*      Compute the angle between two points. (r1,c1) is the origin
	specified as row, column, and (r2,c2) is the second point.
	Result is between 0-360 degrees, where 0 is horizontal right. */

	double x, dr, dc, conv;

	conv = 180.0/3.1415926535;
	dr = (double)(r2-r1); dc = (double)(c2-c1);

/*      Compute the raw angle based of Drow, Dcolumn            */
	if (dr==0 && dc == 0) x = 0.0;
	else if (dc == 0) x = 90.0;
	else {
		x = fabs(atan (dr/dc));
		x = x * conv;
	}

/*      Adjust the angle according to the quadrant              */
	if (dr <= 0) {                  /* upper 2 quadrants */
	  if (dc < 0) x = 180.0 - x;    /* Left quadrant */
	} else if (dr > 0) {            /* Lower 2 quadrants */
	  if (dc < 0) x = x + 180.0;    /* Left quadrant */
	  else x = 360.0-x;             /* Right quadrant */
	}

	return x;
}

