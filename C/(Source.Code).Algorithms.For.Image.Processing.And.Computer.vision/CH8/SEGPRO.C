/* Segment into isolated characters using projections only */

#define MAX
#include "lib.h"
#define OBJECT 1
#define BACK 0
#define DEBUG 0
#define DB2 1
#define MARK 2
#define HOLE 3
#define SPLIT_THRESH 0.15
int PRINTMATCH = 0;

struct lrec {
	int rs, re, cs, ce;
} LINE[400];
int Widths[128];

int mask[128];
int Nlines = -1;
int Space = 1;
int Meanwidth = 0;
int SHOW = 0;
int MaxWidth = 2;

typedef unsigned char ** GIMAGE;

struct grec {           /* Information concerning a glyph */
	char value;
	short int nr, nc;
	GIMAGE ptr;
	struct grec * next;
};
typedef struct grec * GPTR;
GPTR database[129];

void lines   (IMAGE im);
void dumpglyph (IMAGE im, int rs, int cs, int re, int ce, char gly);
GIMAGE newglyph (int nr, int nc);
void initialize();
void clean (IMAGE im);
void remove_region (IMAGE im, int rs, int cs, int re, int ce);
void nextblackpixel (IMAGE im, int rs, int re, int this, int *row, int *col);
void nextblackcol (IMAGE im, int *this, int rs, int re);
void nextwhitecol (IMAGE im, int *this, int rs, int re);
GIMAGE gcopy (GIMAGE x, int nr, int nc);
void segment (IMAGE x, int line);

void main (int argc, char *argv[])
{
	IMAGE im;
	char text[128];
	FILE *f;
	int i;

	if (argc < 2)
	{
	  printf ("Usage: segpro <image>\n");
	  printf ("Segmentation using projections \n");
	  exit (1);
	}

	im = Input_PBM (argv[1]);
	if (im == 0)
	{
	  printf ("The image file '%s' does not exist, or is unreadable.\n");
	  exit (2);
	}

/* Find the lines of text and remember where they were */
	lines (im);

	for (i=0; i<Nlines; i++)
	  segment (im, i);
	fclose (f);
}

/* Locate each line of text, and remember the start and end columns
   and rows. These are saved in the global array LINES.             */
void lines (IMAGE im)
{
	int *hpro, i,j,k,n,m, N;
	int lstart, lend;

	N = im->info->nr;

/* Construct a horizontal projection and look for minima. */
	hpro = (int *) malloc (N * sizeof (int));
	for (i=0; i<N; i++)
	{
	  m = 0;
	  for (j=0; j<im->info->nc; j++)
	    if (im->data[i][j] == OBJECT) m++;
	  hpro[i] = m;
	  if (DEBUG)
	    printf ("Row %d:  %d\n", i, m);
	}

/* Zeros in HPRO mean that the row was all white. */
	i = 0;   j= 0;
	while (i<N)
	{

/* Find the start of a line */
	  while (i<N && hpro[i] == 0) i++;
	  lstart = i;
	  while (i<N && hpro[i]  > 0) i++;
	  lend = i-1;
	  LINE[j].rs = lstart; LINE[j].re = lend;

/* Look for the start and end columns */
	  LINE[j].cs = -1;
	  for (m=0; m<im->info->nc; m++)
	  {
	    for (n=lstart; n<=lend; n++)
	      if (im->data[n][m] == OBJECT)
	      {
		LINE[j].cs = m;
		break;
	      }
	    if (LINE[j].cs >= 0) break;
	  }

	  LINE[j].ce = -1;
	  for (m=im->info->nc-1; m>=0; m--)
	  {
	    for (n=lstart; n<=lend; n++)
	      if (im->data[n][m] == OBJECT)
	      {
		LINE[j].ce = m;
		break;
	      }
	    if (LINE[j].ce > 0) break;
	  }
	  j = j + 1;
	}
	j--;

	free (hpro);
	if (DEBUG)
	{
	  printf ("%d lines seen. Summary:\n", j-1);
	  for (m=0; m<j; m++)
	  {
	    printf ("Line %3d: (%d,%d)		(%d,%d)\n", m,LINE[m].rs,
		LINE[m].cs, LINE[m].rs, LINE[m].ce);
	    printf ("          (%d,%d)		(%d,%d)\n", LINE[m].re,
		LINE[m].cs, LINE[m].re, LINE[m].ce);
	  }
	  printf ("\n");
	}
	Nlines = j;

	if (DEBUG)
	  printf ("A Space is %d pixels.\n", Space);
}

/*      Return the index of the next column with a black pixel in it    */
void nextblackpixel (IMAGE im, int rs, int re, int this, int *row, int *col)
{
	int i,j;

	for (j= this; j<im->info->nc; j++)
	  for (i=rs; i<=re; i++)
	    if (im->data[i][j] == OBJECT)
	    {
		*row = i; *col = j;
		return;
	    }
	*row = -1;
	return ;
}

/* Print a glyph */
void dumpglyph (IMAGE im, int rs, int cs, int re, int ce, char gly)
{
	int i,j;

	if (!DEBUG) return;
	printf ("========== Next character is '%c' ==========\n", gly);
	for (i=rs; i<=re; i++)
	{
	  printf ("====    ");
	  for (j=cs; j<=ce; j++)
	    if (im->data[i][j] == MARK) printf ("#");
	     else printf (" ");
	  printf ("	====\n");
	}
	printf ("===================================================\n\n");
}

/* Create a new glyph record and return a pointer */
GIMAGE newglyph (int nr, int nc)
{
	unsigned char **a, *b;
	int i,j;

	a = (unsigned char **) malloc (nr * sizeof (unsigned char *));
	b = (unsigned char *)  malloc (nr*nc);
	a[0] = b;
	for (i=1; i<nr; i++)
	  a[i] = a[i-1] + nc;

	for (i=0; i<nr; i++)
	  for (j=0; j<nc; j++)
	    a[i][j] = (unsigned char)0;

	return a;
}

/* Remove single pixel black regions */
void clean (IMAGE im)
{
	int i,j, ii, jj;

	for (i=0; i<im->info->nr; i++)
	  for (j=0; j<im->info->nc; j++)
	    if (im->data[i][j] == OBJECT)
	    {
		if (im->data[i][j-1] == BACK && im->data[i][j+1] == BACK &&
		    im->data[i-1][j] == BACK && im->data[i+1][j] == BACK &&
		    im->data[i-1][j-1] == BACK && im->data[i-1][j+1] == BACK &&
		    im->data[i+1][j-1] == BACK && im->data[i+1][j+1] == BACK)
		im->data[i][j] = BACK;
	    }
}

/* Clear the marked pixels ifrom the image in the given region */
void remove_region (IMAGE im, int rs, int cs, int re, int ce)
{
	int i,j;

	for (i=rs; i<=re; i++)
	  for (j=cs; j<=ce; j++)
	    if (im->data[i][j] == MARK) im->data[i][j] = BACK;
}

/*      Return the index of the next column with a black pixel in it    */
void nextblackcol (IMAGE im, int *this, int rs, int re)
{
	int i,j;

	for (j= *this; j<im->info->nc; j++)
	  for (i=rs; i<=re; i++)
	    if (im->data[i][j] == OBJECT)
	    {
		*this = j;
		return;
	    }
	*this = -1;
	return ;
}

/*      Return the index of the next column with a black pixel in it    */
void nextwhitecol (IMAGE im, int *this, int rs, int re)
{
	int i,j, flag;

	for (j=*this; j<im->info->nc; j++)
	{
	  flag = 1;
	  for (i=rs; i<=re; i++)
	    if (im->data[i][j] == OBJECT)
	    {
	      flag = 0;
	      break;
	    }
	  if (flag)
	  {
	    *this = j;
	    return;
	  }
	}
	*this = -1;
	return ;
}

GIMAGE gcopy (GIMAGE x, int nr, int nc)
{
	int i,j;
	GIMAGE y;

	y = newglyph (nr, nc);
	for (i=0; i<nr; i++)
	  for (j=0; j<nc; j++)
		y[i][j] = x[i][j];
	return y;
}

void bkmark (GIMAGE x, int r, int c, int nr, int nc, int val)
{
	x[r][c] = val;
	if (r-1>=0 && x[r-1][c]==0) bkmark (x, r-1, c, nr, nc, val);
	if (r+1<nr && x[r+1][c]==0) bkmark (x, r+1, c, nr, nc, val);
	if (c-1>=0 && x[r][c-1]==0) bkmark (x, r, c-1, nr, nc, val);
	if (c+1<nc && x[r][c+1]==0) bkmark (x, r, c+1, nr, nc, val);
}

void segment (IMAGE x, int line)
{
	int i,j,rs,re,k, *vproj;

	vproj = (int *)malloc (sizeof(int)*(x->info->nc));
	rs = LINE[0].rs;
	re = LINE[0].re;
	for (j=0; j<x->info->nc; j++)
	{
	  k = 0;
	  for (i=rs; i<re; i++)
	    if (x->data[i][j] > 0) k++;
	  vproj[j] = k;
	  printf ("%3d  %3d\n", j, vproj[j]);
	}
}

