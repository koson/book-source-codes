/*      Pavlidis - contour trace        */

#define MAX
#include "lib.h"

struct qrec {
	int r, c, d;
	struct qrec *next, *prev;
};
typedef struct qrec * QUEUE;

struct qitem {
	QUEUE q;
	QUEUE first;
	int n;
};

#define TRUE 1
#define FALSE 0
#define NORTH 1
#define SOUTH 3

void xstart (IMAGE im, int *r, int *c);
void mark (IMAGE im, QUEUE p, int k);
int neighbor (IMAGE im, int r, int c, int dir);
void godir (int *r, int *c, int dir);
int tracer (IMAGE im);
int paireq (int r, int c, QUEUE p1, QUEUE P2);
void outqs ();
void gen (int r, int c, int dir);
void scan_contour (IMAGE im);
void free_contour ();
void cthin (IMAGE im);
void trace_all (IMAGE im);
void thin2 (IMAGE im);
void Delete (IMAGE im, IMAGE tmp);
void check (int v1, int v2, int v3);
int edge (IMAGE im, int r, int c);
void stair (IMAGE im, IMAGE tmp, int dir);
int nays8 (IMAGE im, int r, int c);
int Yokoi (IMAGE im, int r, int c);
void pre_smooth (IMAGE im);

QUEUE  queue = 0, first=0;
struct qitem  queues[20];
int nq = 0, nqentries=0;

int di[8] = {0, -1, -1, -1, 0, 1, 1, 1};
int dj[8] = {1, 1, 0, -1, -1, -1, 0, 1};
int DEBUG = 0;
int t00, t01, t11, t01s;

void main (int argc, char *argv[])
{
	IMAGE data, im;
	int i,j;

	if (argc < 3)
	{
	  printf ("Usage: contour <input file> <output file> \n");
	  exit (0);
	}

	data = Input_PBM (argv[1]);
	if (data == NULL)
	{
		printf ("Bad input file '%s'\n", argv[1]);
		exit(1);
	}

/* Pre-process */
	pre_smooth (data);

/* Embed input into a slightly larger image */
	im = newimage (data->info->nr+2, data->info->nc+2);
	for (i=0; i<data->info->nr; i++)
	  for (j=0; j<data->info->nc; j++)
	    if (data->data[i][j]) im->data[i+1][j+1] = 0;
	     else im->data[i+1][j+1] = 1;

	for (i=0; i<im->info->nr; i++) 
	{
	  im->data[i][0] = 0;
	  im->data[i][im->info->nc-1] = 0;
	}
	for (j=0; j<im->info->nc; j++)
	{
	  im->data[0][j] = 0;
	  im->data[im->info->nr-1][j] = 0;
	}

	cthin (im);

	for (i=0; i<data->info->nr; i++)
	  for (j=0; j<data->info->nc; j++)
	    if (im->data[i+1][j+1] >0) data->data[i][j] = 0;
	    else data->data[i][j] = 255;

	Output_PBM (data, argv[2]);
}

void cthin (IMAGE im)
{
	int i, j, again;
	IMAGE tmp;

	do
	{
	  trace_all (im);
	  scan_contour (im);

	  again = 0;
	  for (i=1; i<im->info->nr-1; i++)
	    for (j=1; j<im->info->nc-1; j++)
	      if (im->data[i][j] == 2)
	      {
		im->data[i][j] = 0;
		again = 1;
	      }
	    else if (im->data[i][j] > 0) im->data[i][j] = 1;

	  free_contour ();
	} while (again);

/* Deal with 2 pixel thick regions */
	thin2 (im);

/* Staircase removal */
	tmp = newimage (im->info->nr, im->info->nc);
	for (i=0; i<im->info->nr; i++)
	 for (j=0; j<im->info->nc; j++)
	    tmp->data[i][j] = 0;

	stair (im, tmp, NORTH);
	Delete (im, tmp);
	stair (im, tmp, SOUTH);
	Delete (im, tmp);
	freeimage (tmp);
}

/*      Remove 2 pixel wide lines, replace with 1 pixel wide lines      */
void thin2 (IMAGE im)
{
	int i,j;

/* Corners, upper left:  0 0 x
			 0 1 1
			 x 1 1                                  */

	for (i=1; i<im->info->nr-1; i++)
	  for (j=1; j<im->info->nc-1; j++)
	    if (im->data[i][j] != 0)
	    {
		if (im->data[i-1][j-1] == 0 && im->data[i-1][j] == 0 &&
		    im->data[i][j-1] == 0 &&
		    im->data[i][j+1] != 0   && im->data[i+1][j] != 0 &&
		    im->data[i+1][j+1] != 0) im->data[i][j] = 0;
	    }

/* Corners, upper right:  x 0 0
			  1 1 0
			  1 1 x                                 */

	for (i=1; i<im->info->nr-1; i++)
	  for (j=im->info->nc-1; j>0; j--)
	    if (im->data[i][j] != 0)
	    {
	      if (im->data[i-1][j] == 0 && im->data[i-1][j+1] == 0 &&
		  im->data[i][j+1] == 0 && im->data[i][j-1] != 0  &&
		  im->data[i+1][j-1] != 0 && im->data[i+1][j] != 0)
			im->data[i][j] = 0;
	    }

/* Corners, lower left:  x 1 1
			 0 1 1
			 0 0 x                                  */

	for (i=im->info->nr-1; i>0; i--)
	  for (j=1; j<im->info->nc-1; j++)
	    if (im->data[i][j] != 0)
	    {
		if (im->data[i+1][j-1] == 0 && im->data[i+1][j] == 0 &&
			 im->data[i][j-1] == 0 && im->data[i][j+1] != 0   &&
			 im->data[i-1][j] != 0 && im->data[i-1][j+1] != 0)
			im->data[i][j] = 0;
	    }

/* Corners, lower right: 1 1 x
			 1 1 0
			 x 0 0                                  */

	for (i=im->info->nr-1; i>0; i--)
	  for (j=im->info->nc-1; j>0; j--)
	    if (im->data[i][j] != 0)
	    {
	      if (im->data[i+1][j] == 0 && im->data[i+1][j+1] == 0 &&
		  im->data[i][j+1] == 0 && im->data[i][j-1] != 0   &&
		  im->data[i-1][j] !=0 && im->data[i-1][j-1] != 0)
			im->data[i][j] = 0;
	    }
}

/*      Find a starting (boundary) pixel        */
void xstart (IMAGE im, int *r, int *c)
{
	int i,j;

	*r = *c = -1;
	for (i=1; i<im->info->nr-1; i++)
	  for (j=1; j<im->info->nc-1; j++)
	    if (im->data[i][j] == 1)
	      if (im->data[i-1][j]==0 || im->data[i+1][j]==0 ||
		  im->data[i][j-1]==0 || im->data[i][j+1]==0)
	      {
		*r = i; *c = j;
		return;
	      }
}

/*      Trace all contours */
void trace_all (IMAGE im)
{
	int i,j,k;

	do
	{

/* Find a start pixel; otherwise we are done */
	  xstart (im, &i, &j);
	  if (i<0) break;

/* Clear the global queue */
	  queue = (QUEUE)0; first = (QUEUE)0; nqentries = 0;

/* Trace the contour starting at the START pixel */
	  tracer (im);

/* Save the pixel list in the next queue */
	  queues[nq].q = queue;
	  queues[nq].first = first;
	  queues[nq].n = nqentries;

/* Mark the contour pixels */
	  mark (im, queue, nq+2);

/* Next queue */
	  nq++;
	} while (nq<20);

/* Reset the image pixels */
	for (i=1; i<im->info->nr-1; i++)
	  for (j=1; j<im->info->nc-1; j++)
	    if (im->data[i][j] > 0) im->data[i][j] = 1;

/* Mark the contours by incrementing the pixels. Don't mark
   contours <5 pixels in size.                                  */
	for (k=0; k<nq; k++)
	  if (queues[k].n > 5)
	    mark (im, queues[k].q, -1);
}

int tracer (IMAGE im)
{
	int ci, cj, ai, aj, s, first, found, k;

/* Locate a starting pixel */
	xstart (im, &ai, &aj);
	if (ai < 0) return 0;

	ci = ai; cj = aj;
	s = 6; 
	first = 1;

	gen (ci, cj, 8);
	while (first || (ci!=ai || cj!=aj))
	{
	  found = 0;
	  for (k=1; k<=3 && !found; k++)
	  {
	    if (neighbor (im, ci, cj, s-1) == 1)
	    {
	      im->data[ci][cj] += 1;
	      godir (&ci, &cj, s-1);
	      if ((ci!=ai || cj!=aj))
		gen (ci, cj, s-1);
	      s = (s+6)%8;
	      found = 1;
	    } else if (neighbor (im, ci, cj, s) == 1)
	    {
	      im->data[ci][cj] += 1;
	      godir (&ci, &cj, s);
	      if ((ci!=ai || cj!=aj))
		gen (ci, cj, s);
	      found = 1;
	    } else if (neighbor (im, ci, cj, s+1) == 1)
	    {
	      im->data[ci][cj] += 1;
	      godir (&ci, &cj, s+1);
	      if ((ci!=ai || cj!=aj))
		gen (ci, cj, s+1);
	      found = 1;
	    } else s = (s+2)%8;
	  }
	  first = 0;
	}
	return 1;
}

/* Modify (r,c) to move in the direction indicated by DIR */
void godir (int *r, int *c, int dir)
{
	int d;

	d = (dir+8)%8;
	*r += di[d];
	*c += dj[d];
}

/* Return the value of the pixel in the DIR direction from (r, c) */
int neighbor (IMAGE im, int r, int c, int dir)
{
	int d;

	d = (dir+8)%8;
	return im->data[r+di[d]][c+dj[d]]!=0;
}

void gen (int r, int c, int dir)
{
	int d;
	QUEUE p;

	d = (dir+8)%8;
	p = (QUEUE)malloc (sizeof(struct qrec));
	p->r = r; p->c = c;
	p->d = d;
	p->next = queue; 
	nqentries++;
	if (queue) 
	  queue->prev = p;
	queue = p;
	if (first == 0) first = p;
}

/* Scan the contour for multiple pixels and mark them */
void scan_contour (IMAGE im)
{
	int i,j,k, flag, mult, ii, jj, N;
	QUEUE p, prev, next;

	for (N=0; N<nq; N++)
	{

	 if (queues[N].q == 0) continue;
	 p = queues[N].q;  prev = queues[N].first;
	 next = p->next;

	 while (p)
	 {
	   i = p->r; j = p->c;
	   if (im->data[i][j] > 2)      /* Already a multiple pixel */
	   {
	     prev = p;
	     p = p->next;
	     if (p &&p->next) next = p->next;
	     else next = queue;
	     continue;
	   }

	   flag = 1; mult = 0;
	   for (ii=i-1; ii<=i+1; ii++)          /* Look for interior neighbor */
	     for (jj=j-1; jj<=j+1; jj++)
	       if (ii!=i || jj!=j)
	       {
		if (im->data[ii][jj] == 1)
		  flag = 0;

		if (im->data[ii][jj] > 1)       /* A D-neighbor on the contour */
		  if (paireq (ii,jj,prev, next) == 0) mult = 1;
	       }

	   if (flag) 
	     im->data[i][j] = 80;
	   else if (mult)
	     im->data[i][j] = 90;

	   prev = p;
	   p = p->next;
	   if (p && p->next) next = p->next;
	    else next = queues[N].q;
	 }
	}
}

/*      Is (R, C) one of the pixels P1 or P2?   */
int paireq (int r, int c, QUEUE p1, QUEUE p2)
{
	if ( (p1->r==r) && (p1->c == c) ) return 1;
	if ( (p2->r==r) && (p2->c == c) ) return 1;
	return 0;
}

/*      Free all nodes in the queue     */
void free_contour ()
{
	QUEUE p, q;
	int i;

	for (i=0; i<nq; i++)
	{
	  p = queues[i].q; q = 0;
	  while (p)
	  {
	    q = p;
	    p = p->next;
	    free(q);
	  }
	  queues[i].q = (QUEUE)0;
	}

	queue = first = (QUEUE)0;
	nq = 0;
}

void mark (IMAGE im, QUEUE p, int k)
{
	while (p)
	{
	  if (k>=0)
	    im->data[p->r][p->c] = k;
	  else im->data[p->r][p->c]++;
	  p = p->next;
	}
}

void outqs ()
{
	int i,j;
	QUEUE p;

	for (i=0; i<nq; i++)
	{
	  p = queues[i].q;
	  if (p)
	    printf ("Queue #%d - %d entries:\n", i, queues[i].n);
	  j = 0;
	  while (p)
	  {
	    printf ("(%d,%d) -> %d   ", p->r, p->c, p->d);
	    p = p->next;
	    j++;
	    if (j>6) { j = 0; printf ("\n"); }
	  }
	  printf ("\n");
	}
	printf ("\n");
}

void stair (IMAGE im, IMAGE tmp, int dir)
{
	int i,j;
	int N, S, E, W, NE, NW, SE, SW, C;

	if (dir == NORTH)
	for (i=1; i<im->info->nr-1; i++)
	  for (j=1; j<im->info->nc-1; j++)
	  {
	   NW = im->data[i-1][j-1]; N = im->data[i-1][j]; NE = im->data[i-1][j+1];
	   W = im->data[i][j-1]; C = im->data[i][j]; E = im->data[i][j+1];
	   SW = im->data[i+1][j-1]; S = im->data[i+1][j]; SE = im->data[i+1][j+1];

	   if (dir == NORTH)
	   {
	     if (C && !(N && 
		      ((E && !NE && !SW && (!W || !S)) || 
		       (W && !NW && !SE && (!E || !S)) )) )
	       tmp->data[i][j] = 0;             /* Survives */
	     else
	       tmp->data[i][j] = 1;
	   } else if (dir == SOUTH)
	   {
	     if (C && !(S && 
		      ((E && !SE && !NW && (!W || !N)) || 
		       (W && !SW && !NE && (!E || !N)) )) )
	       tmp->data[i][j] = 0;             /* Survives */
	     else
	       tmp->data[i][j] = 1;
	   }
	  }
}

void check (int v1, int v2, int v3)
{
	if (!v2 && (!v1 || !v3)) t00 = TRUE;
	if ( v2 && ( v1 ||  v3)) t11 = TRUE;
	if ( (!v1 && v2) || (!v2 && v3) )
	{
		t01s = t01;
		t01  = TRUE;
	}
}

int edge (IMAGE im, int r, int c)
{
	if (im->data[r][c] == 0) return 0;
	t00 = t01 = t01s = t11 = FALSE;

/* CHECK(vNW, vN, vNE) */
	check (im->data[r-1][c-1], im->data[r-1][c], im->data[r-1][c+1]);

/* CHECK (vNE, vE, vSE) */
	check (im->data[r-1][c+1], im->data[r][c+1], im->data[r+1][c+1]);

/* CHECK (vSE, vS, vSW) */
	check (im->data[r+1][c+1], im->data[r+1][c], im->data[r+1][c-1]);

/* CHECK (vSW, vW, vNW) */
	check (im->data[r+1][c-1], im->data[r][c-1], im->data[r-1][c-1]);

	return t00 && t11 && !t01s;
}

void Delete (IMAGE im, IMAGE tmp)
{
	int i,j;

/* Delete pixels that are marked  */
	for (i=1; i<im->info->nr-1; i++)
	  for (j=1; j<im->info->nc-1; j++)
	    if (tmp->data[i][j])
	    {
		im->data[i][j] = 0;
		tmp->data[i][j] = 0;
	    }
}

int nays8 (IMAGE im, int r, int c)
{
	int i,j,k=0;

	for (i=r-1; i<=r+1; i++)
	  for (j=c-1; j<=c+1; j++)
	    if (i!=r || c!=j)
	      if (im->data[i][j] == 0) k++;
	return k;
}

int Yokoi (IMAGE im, int r, int c)
{
	int N[9];
	int i,j,k, i1, i2;

	N[0] = im->data[r][c]      != 0;
	N[1] = im->data[r][c+1]    != 0;
	N[2] = im->data[r-1][c+1]  != 0;
	N[3] = im->data[r-1][c]    != 0;
	N[4] = im->data[r-1][c-1]  != 0;
	N[5] = im->data[r][c-1]    != 0;
	N[6] = im->data[r+1][c-1]  != 0;
	N[7] = im->data[r+1][c]    != 0;
	N[8] = im->data[r+1][c+1]  != 0;

	k = 0;
	for (i=1; i<=7; i+=2)
	{
	  i1 = i+1; if (i1 > 8) i1 -= 8;
	  i2 = i+2; if (i2 > 8) i2 -= 8;
	  k += (N[i] - N[i]*N[i1]*N[i2]);
	}

	if (DEBUG)
	{
	  printf ("Yokoi: (%d,%d)\n",r, c);
	  printf ("%d %d %d\n", im->data[r-1][c-1], im->data[r-1][c],
			im->data[r-1][c+1]);
	  printf ("%d %d %d\n", im->data[r][c-1], im->data[r][c],
			im->data[r][c+1]);
	  printf ("%d %d %d\n", im->data[r+1][c-1], im->data[r+1][c],
			im->data[r+1][c+1]);
	  for (i=0; i<9; i++) printf ("%2d ", N[i]);
	  printf ("\n");
	  printf ("Y = %d\n", k);
	}

	return k;
}

void pre_smooth (IMAGE im)
{
	int i,j;

	for (i=0; i<im->info->nr; i++)
	  for (j=0; j<im->info->nc; j++)
	    if (im->data[i][j] == 0)
		if (nays8(im, i, j) <= 2 && Yokoi (im, i, j)<2)
		  im->data[i][j] = 2;

	for (i=0; i<im->info->nr; i++)
	  for (j=0; j<im->info->nc; j++)
	    if (im->data[i][j] == 2) im->data[i][j] = 1;
}

