/* Read the sample page and build the database. This is version 2, which
   uses scanned input instead of 'perfect' input. Image must be bi-level */

#define MAX
#include "lib.h"
#define OBJECT 1
#define BACK 0
#define DEBUG 1
#define MARK 2
#define HOLE 3

struct lrec {
	int rs, re, cs, ce;
} LINE[400];
float WT[128] = {0.0000,0.0000,0.0000,0.0000,0.0000,0.0000,0.0000,0.0000,
     0.0000,0.0000,0.0000,0.0000,0.0000,0.0000,0.0000,0.0000,0.0000,0.0000,
     0.0000,0.0000,0.0000,0.0000,0.0000,0.0000,0.0000,0.0000,0.0000,0.0000,
     0.0000,0.0000,0.0000,0.0000,0.0000,0.0000,0.0000,0.0000,0.0000,0.0000,
     0.0000,0.0000,0.0000,0.0000,0.0000,0.0000,0.0000,0.0000,0.0000,0.0000,
     0.6522,0.3913,0.6522,0.5652,0.6522,0.6087,0.6087,0.6087,0.6087,0.6522,
     0.0000,0.0000,0.0000,0.0000,0.0000,0.0000,0.0000,1.0000,0.7826,0.8261,
     0.9565,0.7826,0.6957,0.9130,0.8696,0.3478,0.4783,0.9130,0.7826,1.1304,
     0.9565,0.9130,0.6957,0.9130,0.8696,0.6522,0.7826,0.9130,0.9130,1.2174,
     0.9130,0.9130,0.8261,0.0000,0.0000,0.0000,0.0000,0.0000,0.0000,0.6087,
     0.6522,0.5652,0.6522,0.5652,0.4783,0.6087,0.6522,0.3043,0.4348,0.6522,
     0.3043,1.0000,0.5652,0.6522,0.6087,0.6522,0.4348,0.4348,0.3043,0.6087,
     0.6087,0.9130,0.6087,0.6087,0.5217,0.0000,0.0000,0.0000,0.0000,0.0000};
int Widths[128];

int Nlines = -1;
int Space = 0;
int Meanwidth = 0;
/* The text that appears on the training file */
char *data[] = {
        "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
        "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
        "abcdefghijklmnopqrstuvwxyz",
        "abcdefghijklmnopqrstuvwxyz",
        "012345678901234567890123456789",
        "!@#$%^&*()_+-=|\\{",
        "}[]:\"~<>?;',./",
        "!@#$%^&*()_+-=|\\{",
        "}[]:\"~<>?;',./"
};

typedef unsigned char ** GIMAGE;

struct grec {		/* Information concerning a glyph */
	char value;
	short int nr, nc;
	GIMAGE ptr;
	struct grec * next;
};
typedef struct grec * GPTR;
GPTR database[128];

void lines   (IMAGE im);
void extract (IMAGE im);
void dumpglyph (IMAGE im, int rs, int cs, int re, int ce, char gly);
void saveglyph (IMAGE im, int rs, int cs, int re, int ce, char gly, GPTR *x);
GIMAGE newglyph (int nr, int nc);
GPTR insert_to_database (GIMAGE x, int rs, int re, int cs, int ce, char gc);
int glyeq (GIMAGE x, int nr, int nc, char gc, GPTR p);
void dump_db (void);
void initialize(void);
GIMAGE nextglyph (IMAGE im, struct lrec *thisline, int *thiscol, int *space,
		int *nr, int *nc);
void savedatabase (char *filename);
void clean (IMAGE im);
void remove_gly(IMAGE im, int rs, int cs, int re, int ce);
void mark (IMAGE im, int row, int col);
void mark_region (IMAGE im, int *rs, int *cs, int *re, int *ce,
	 int row, int col);
void nextblackpixel (IMAGE im, int rs, int re, int this, int *row, int *col);
void nextblackcol (IMAGE im, int *this, int rs, int re);
void nextwhitecol (IMAGE im, int *this, int rs, int re);
void build_widths (int cs, int ce);
void find_A (IMAGE im, int *rrs, int *rcs, int *rre, int *rce);
void bkmark (GIMAGE x, int r, int c, int nr, int nc, int val);
void holes (GIMAGE x, int nr, int nc);
void gprint (GIMAGE x, int nr, int nc);
void loaddatabase ();

void main (int argc, char *argv[])
{
	IMAGE im;
	char text1[80], text2[80], text3[80];

	if (argc < 2)
	{
	  printf ("Usage: learn <image> <template file>\n");
	  printf ("This program examines a simple perfect test image\n");
	  printf ("and attempts to recognize the characters. A data\n");
	  printf ("file will be created with a database of templates\n");
	  printf ("for recognizing the characters.\n");
	  exit (1);
	}

	im = Input_PBM (argv[1]);
	if (im == 0)
	{
	  printf ("The image file '%s' does not exist, or is unreadable.\n");
	  exit (2);
	}

	initialize();
	clean (im);

	loaddatabase();

/* Find the lines of text and remember where they were */
	lines (im);

/* Now extract and recognize the glyphs */
	extract (im);

/* Save */
	savedatabase (argv[2]);
}

void initialize()
{
	int i;
	
	for (i=0; i<128; i++)
	{
	  database[i] = (GPTR)0;
	  Widths[i] = 0;
	}
}

/* Locate each line of text, and remember the start and end columns
   and rows. These are saved in the global array LINES.		    */
void lines (IMAGE im)
{
	int *hpro, i,j,n,m, N;
	int lstart, lend, sp[4];

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
	free (hpro);

	if (Space == 0)
	{
	  sp[0] = (LINE[1].cs - LINE[0].cs)/2;
	  sp[1] = (LINE[3].cs - LINE[2].cs)/2;
	  sp[2] = (LINE[6].cs - LINE[5].cs);
	  sp[3] = (LINE[8].cs - LINE[7].cs);
	  Space = (sp[0]+sp[1]+sp[2]+sp[3])/4;
	  if (DEBUG)
	  {
	    printf ("Spacing: %d %d %d %d\n", sp[0],sp[1], sp[2],sp[3]);
	    printf ("A Space is %d pixels.\n", Space);
	  }
	}
}

/* Extract the templates from the training image */
void extract (IMAGE im)
{
	int i,j, scol, qflag = 0, tspace=0, spacing;
	int thisline, thischar, rs,re,cs,ce, oldscol, nspace = 0;
	int rre, rrs, rce,rcs, row, col, actual, width, wid2;
	float mspace = 0;
	GPTR thisptr;

/* Use the first 10 lines (all characters) */
	for (thisline = 0; thisline <= 8; thisline++)
	{

/* How wide and tall is this line? */
	  rs = LINE[thisline].rs; re = LINE[thisline].re;
	  cs = LINE[thisline].cs; ce = LINE[thisline].ce;
	  thischar = 0;

/* Extract individual characters */
	  col = cs;
	  oldscol = -1;
	  do
	  {

/* Find the next black pixel */
	    scol = col;
	    nextblackpixel (im, rs, re, scol, &row, &col);
	    if (row < 0) break;

/* Mark the connected region */
	    rrs = rs; rre = re; rcs = col; rce = ce;
/*
	    mark_region (im, &rrs, &rcs, &rre, &rce, row, col);
	    if (rrs < 0 || rcs < 0) continue;
*/

/* No overlap - spaces between each character */
	    rcs = scol;
	    nextblackcol (im, &rcs, rs, re);
	    if (rcs < 0) continue;
	    rce = rcs;
	    nextwhitecol (im, &rce, rs, re);
	    if (rce < 0) continue;
	    rre = re; rrs = rs;
	    for (i=rrs; i<=rre; i++)
	      for (j=rcs; j<=rce; j++)
		if (im->data[i][j] == OBJECT)
			im->data[i][j] = MARK;

/* Is this the first letter? If so, extract carefully and measure widths */
	    if (thisline == 0 && thischar == 0)
	    {
	      if ( (float)(rre-rrs)/(float)(rce-rcs) > 1.5 ||
		   (float)(rce-rcs)/(float)(rre-rrs) > 1.5)
		find_A (im, &rrs, &rcs, &rre, &rce);
	      build_widths (rcs, rce);
	    }

/* Use predicted character widths to determine multiple glyphs */
	    actual = (int)(data[thisline][thischar]);
	    width  = rce-rcs+1;
	    printf ("Character '%c' (%o) Actual width %d predicted %d delta %d\n",
		actual, actual, width, Widths[actual], width-Widths[actual]);
	    if (Widths[actual] == 0)
		Widths[actual] = rce-rcs+1;

/* We now have a glyph - dump it */
/* EXCEPTION: A double quote has two vertically separated sets of black. */
	    if (DEBUG)
	      if (rrs>=0 && rcs>=0)
	       dumpglyph (im, rrs, rcs, rre, rce, data[thisline][thischar]);

/* Save the glyph as a template for its character class */
	   if (rrs>=0 && rcs>=0)
	   {
	    saveglyph (im, rrs, rcs, rre, rce,
		 data[thisline][thischar], &thisptr);
	    remove_gly (im, rrs, rcs, rre, rce);
	   }

	    if (data[thisline][thischar] == '"' && qflag == 0)
	    {
	      oldscol = -1;
	      qflag = 1;	/* The first of two black columns for a " */
	    }
	    else
	    {
	      thischar++;
	      if (qflag)
	      {
		qflag = 0;
		oldscol = -1;
	      }
	    }
	    if (oldscol < 0) oldscol = rcs;
	    else
	    {
		spacing = rcs - oldscol;

/* Special characters all have a space between them */
		spacing -= Space;
		tspace += spacing;
		nspace++;
		oldscol = rcs;
	    }
	    col = rce++;
	  } while (rcs >= 0 && rce >= 0);

	}
	mspace = (float)tspace/(float)nspace;
	if (DEBUG)
		printf ("Average character width: %f\n", mspace);
	if (Meanwidth == 0)
	  Meanwidth = (int)(mspace+0.5);
}

/*	Return the index of the next column with a black pixel in it	*/
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

/*	Print the entire database on standard output */
void dump_db ()
{
	int i,j,k;
	GPTR p;

	if (!DEBUG) return;
	for (k=0; k<128; k++)
	{
	  if (database[k] == (GPTR)0) continue;
	  p = database[k];
	  printf ("Character number %d (%o) '%c':\n", k, k, k);
	  while (p)
	  {
	    printf ("==== Rows: %d  Columns: %d   Value: '%c'\n",
		p->nr, p->nc, p->value);
	    for (i=0; i<p->nr; i++)
	    {
	      for (j=0; j<p->nc; j++)
		if (p->ptr[i][j] == BACK) printf (" ");
		 else printf ("#");
	      printf ("\n");
	    }
	    p = p->next;
	  }
	  printf ("****************************************************\n");
	}
}

/* Find bounding box for the extracted glyph and put it into the database */
void saveglyph (IMAGE im, int rs, int cs, int re,
		 int ce, char gly, GPTR *entry)
{
	int i,j;
	int first, last;
	GIMAGE x;

/* Find the first black column */
	first = -1;
	for (j=cs; j<=ce; j++)
	{
	  for (i=rs; i<=re; i++)
	    if (im->data[i][j] == MARK)
	    {
		first = j;
		break;
	    }
	  if (first >= 0) break;
	}
	cs = first;
	if (cs < 0) return;

/* Find the last black column */
	last = -1;
	for (j=ce; j>=cs; j--)
	{
	  for (i=rs; i<=re; i++)
	    if (im->data[i][j] == MARK)
	    {
	      last = j;
	      break;
	    }
	  if (last >= 0) break;
	}
	ce = last;

/* First and last black rows */
	first = -1;
	for (i=rs; i<=re; i++)
	{
	  for (j=cs; j<=ce; j++)
	    if (im->data[i][j] == MARK)
	    {
		first = i;
		break;
	    }
	  if (first >= 0) break;
	}
	rs = first;
	if (rs < 0) return;

	last = -1;
	for (i=re; i>=rs; i--)
	{
	  for (j=cs; j<=ce; j++)
	    if (im->data[i][j] == MARK)
	    {
		last = i;
		break;
	    }
	  if (last >= 0) break;
	}
	re = last;

/* We now have a minimal bounding box. Save the glyph */
	x = newglyph (re-rs+2, ce-cs+2);
	for (i=rs; i<=re; i++)
	  for (j=cs; j<=ce; j++)
	    x[i-rs][j-cs] = im->data[i][j]==2;

/* OPTIONAL: Holes */
	holes (x, re-rs+2,  ce-cs+2);

/* Fill the remainder of the glyph record */
	*entry = insert_to_database (x, rs, re, cs, ce, gly);
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

/* Insert a new template into the database */
GPTR insert_to_database (GIMAGE x, int rs, int re, int cs, int ce, char gc)
{
	int k;
	GPTR gp, p;

	k = (int)gc;			/* ASCII value is database index */
	p = database[k];
	if (database[k] != 0)		/* Add to existing entry */
	{

/* Look for a duplicate */
	  do
	  {
	    if (glyeq(x,re-rs+1,ce-cs+1,gc, database[k]))
	    {
		printf ("EQUAL: no insertion.\n");
		free(x);
		return 0;
	    }
	    if (p->next) p = p->next;
	    else break;
	  } while (p);
	}

/* Create a new entry */
	gp = (GPTR) malloc (sizeof (struct grec));
	gp->nr = re-rs+1;
	gp->nc = ce-cs+1;
	gp->value= gc;
	gp->ptr = x;
	gp->next = (GPTR)0;

/* Add new entry to the end of the list for this character */
	if (p)
	  p->next= gp;
	else
	  database[k] = gp;
	return gp;
}

/* Are the two glyphs the same? */
int glyeq (GIMAGE x, int nr, int nc, char gc, GPTR p)
{
	int i,j;
	
	if (p->nr != nr) return 0;
	if (p->nc != nc) return 0;
	if (p->value != gc) return 0;
	for (i=0; i<nr; i++)
	  for (j=0; j<nc; j++)
	    if (p->ptr[i][j] != x[i][j]) return 0;
	return 1;
}

GIMAGE nextglyph (IMAGE im, struct lrec *thisline, int *thiscol, int *space,
		int *nr, int *nc)
{
	int i,j,rs, re, cs, ce;
	int ecol, scol;
	GIMAGE x;

	*space = -1;
	*nr = *nc = 0;
	rs = thisline->rs; re = thisline->re;
	cs = thisline->cs; ce = thisline->ce;
	scol = *thiscol;
	nextblackcol (im, &scol, rs, re);
	if (scol < 0)
	  return 0;
	if (scol >= ce) 
	  return 0;
	*space = 0;

	ecol = scol;
	nextwhitecol (im, &ecol, rs, re);
	if (ecol < 0) ecol = ce;
	else ecol--;

/* First and last black rows 
	first = -1;
	for (i=rs; i<=re; i++)
	{
	  for (j=scol; j<=ecol; j++)
	    if (im->data[i][j] == OBJECT)
	    {
	        first = i;
	        break;
	    }
	  if (first >= 0) break;
	}
	rs = first;
 
	last = -1;
	for (i=re; i>=rs; i--)
	{
	  for (j=scol; j<=ecol; j++)
	    if (im->data[i][j] == OBJECT)
	    {
	        last = i;
	        break;
	    }
	  if (last >= 0) break;
	}
	re = last;
*/
 
/* We now have a minimal bounding box. Save the glyph */
	*nr = re-rs+1;	*nc =  ecol-scol+1;
	x = newglyph (re-rs+1, ecol-scol+1);
	for (i=rs; i<=re; i++)
	  for (j=scol; j<=ecol; j++)
	    x[i-rs][j-scol] = im->data[i][j];

	if (DEBUG)
         for (i=0; i<*nr; i++)
         {
           for (j=0; j<*nc; j++)
             if (x[i][j] == OBJECT) printf ("#");
               else printf (" ");
             printf ("\n");
         }

	*thiscol = ecol + 1;
	return x;
}

void savedatabase (char *filename)
{
	FILE *f;
	int i,k, n,m;
	GPTR p;

	f = fopen (filename, "w");
	if (f == NULL)
	{
	  printf ("Cannot save database in file '%s'\n", filename);
	  return;
	}

	fprintf (f, "%d %d\n", Space, Meanwidth);
	for (i=0; i<128; i++)
	{
	  if (database[i] == 0) continue;
	  p = database[i];
	  k = 0;
	  while (p)
	  {
	    fprintf (f, "%c %d %d %d %d\n", p->value, k,
			 p->nr, p->nc, Widths[k]);
	    for (n=0; n<p->nr; n++)
	    {
	      for (m=0; m<p->nc; m++)
	        fprintf (f, "%2d", p->ptr[n][m]);
	      fprintf (f, "\n");
	    }
	    p = p->next;
	  }
	}
	fclose (f);
}

void clean (IMAGE im)
{
	int i,j;

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

/* Mark a glyph with '2' valued pixels. */
void mark_region (IMAGE im, int *rs, int *cs, int *re, int *ce, int row, int col)
{
	int i,j,rmax,rmin,cmax,cmin, delta, flag = 0;

	if (DEBUG) printf ("Marking ...\n");
	mark (im, row, col);

	rmax = cmax = -1;	rmin = cmin = 10000;
	delta = (*ce-*cs+1)/4;
	for (j= *cs-delta; j<= *ce+delta; j++)
	{
	  for (i= *rs; i<= *re; i++)
	  {
	    if (range(im,i,j) == 0) continue;
	    if (im->data[i][j] == MARK)
	    {
		if (i<rmin) rmin = i;
		if (j<cmin) cmin = j;
		if (i>rmax) rmax = i;
		if (j>cmax) cmax = j;
	    }
	  }
	}

/* Look for dots above i and j: */
	for (j=(cmax+cmin)/2 - 2; j<=(cmax+cmin)/2+2; j++)
	  for (i= *rs; i<=rmin; i++)
	    if (im->data[i][j] == 1)
	    {
		mark (im, i, j);
		flag = 1;
	    }

	if (flag)
	{
	  rmax = cmax = -1;	rmin = cmin = 10000;
	  for (j= *cs-delta; j<= *ce+delta; j++)
	    for (i= *rs; i<= *re; i++)
	    {
	      if (range(im,i,j) == 0) continue;
	      if (im->data[i][j] == MARK)
	      {
	  	if (i<rmin) rmin = i;
	  	if (j<cmin) cmin = j;
	  	if (i>rmax) rmax = i;
	  	if (j>cmax) cmax = j;
	      }
	    }
	}

	*rs = rmin; *re = rmax;
	*cs = cmin; *ce = cmax;

	if (DEBUG)
	for (i= rmin; i<= rmax; i++)
	{
	  for (j=cmin; j<=cmax; j++)
	    printf ("%1d", im->data[i][j]);
	  printf ("\n");
	}
}

void mark (IMAGE im, int row, int col)
{
	im->data[row][col] = MARK;
	if (im->data[row-1][col-1] == OBJECT) mark (im, row-1,col-1);
	if (im->data[row-1][col] == OBJECT) mark (im, row-1,col);
	if (im->data[row-1][col+1] == OBJECT) mark (im, row-1,col+1);
	if (im->data[row][col-1] == OBJECT) mark (im, row,col-1);
	if (im->data[row][col+1] == OBJECT) mark (im, row,col+1);
	if (im->data[row+1][col-1] == OBJECT) mark (im, row+1,col-1);
	if (im->data[row+1][col] == OBJECT) mark (im, row+1,col);
	if (im->data[row+1][col+1] == OBJECT) mark (im, row+1,col+1);
}

void remove_gly (IMAGE im, int rs, int cs, int re, int ce)
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

/* Build a table of predicted character widths using the width of an 'A' */
void build_widths (int cs, int ce)
{
	int i;
	float x;

	x = (float)(ce - cs + 1);
	for (i=0; i<128; i++)
	  Widths[i] = (int)(WT[i] * x + 0.5);
}

void find_A (IMAGE im, int *rrs, int *rcs, int *rre, int *rce)
{
	int *vproj, i, j, k;

	if (DEBUG) printf ("Finding an 'A' character to calibrate:\n");
	vproj = (int *)malloc (sizeof(int)*(*rce-*rcs+1));
	for (j= *rcs; j<= *rce; j++)
	{
	  k = 0;
	  for (i= *rrs; i<= *rre; i++)
	    if (im->data[i][j] == MARK) k++;
	  vproj[j- *rcs] = k;
	}

	j = (*rre - *rrs + 1);
	k = j/4;
	for (i=j-k; i<j+k; i++)
	  if (vproj[i]<vproj[j]) j = i;
	j += *rcs;

	for (i= *rrs; i<= *rre; i++)
	  for (k = j; k<= *rce; k++)
	    if (im->data[i][k] == MARK) im->data[i][k] = 1;
	*rce = j-1;
	free(vproj);
}

void holes (GIMAGE x, int nr, int nc)
{
	int i,j,ii,jj;

/* Mark any background pixels connected to the bounding box with a 3 */
	for (i=0; i<nr; i++)
	{
	  if (x[i][0] == BACK) bkmark (x, i, 0, nr, nc, 3);
	  if (x[i][nc-1] == BACK) bkmark(x, i, nc-1, nr, nc, 3);
	}

	for (j=0; j<nc; j++)
	{
	  if (x[0][j] == BACK) bkmark (x, 0, j, nr, nc, 3);
	  if (x[nr-1][j] == BACK) bkmark (x, nr-1, j, nr, nc, 3);
	}

/* Any remaining background pixels are holes - mark them */
	for (i=0; i<nr; i++)
	  for (j=0; j<nc; j++)
	    if (x[i][j] == 3) x[i][j] = 0;
	    else if (x[i][j] == 0) x[i][j] = 3;

	gprint (x, nr, nc);
	printf ("Holes:\n");

}

void bkmark (GIMAGE x, int r, int c, int nr, int nc, int val)
{
	x[r][c] = val;
	if (r-1>=0 && x[r-1][c]==0) bkmark (x, r-1, c, nr, nc, val);
	if (r+1<nr && x[r+1][c]==0) bkmark (x, r+1, c, nr, nc, val);
	if (c-1>=0 && x[r][c-1]==0) bkmark (x, r, c-1, nr, nc, val);
	if (c+1<nc && x[r][c+1]==0) bkmark (x, r, c+1, nr, nc, val);
}

void gprint (GIMAGE x, int nr, int nc)
{
	int i,j;

	for (i=0; i<nr; i++)
	{
	  for (j=0; j<nc; j++)
		if (x[i][j] > 0) printf ("%1d", x[i][j]); else printf (" ");
	  printf ("\n");
	}
}

/* Read the database from the file */
void loaddatabase ()
{
	FILE *f;
	int i,j,k, n,m, nr, nc, ival, items, v;
	GPTR p, q;
	GIMAGE x;
	char cval;

	f = fopen ("tr.db", "r");
	if (f == NULL)
	{
	  printf ("Cannot read database from file '%s'\n", "tr.db");
	  return;
	}

	fscanf (f, "%d", &Space);
	fscanf (f, "%d", &Meanwidth);
	do
	{
	  items = fscanf (f, "%c", &cval);
	  if (items < 1) break;

	  while (cval == ' ' || cval == '\n')
	  {
	    items = fscanf (f, "%c", &cval);
	    if (items < 1) break;
	  }

	  ival = (int)cval;
	  items = fscanf (f, "%d", &k);
	  items = fscanf (f, "%d", &nr);
	  items = fscanf (f, "%d", &nc);
	  items = fscanf (f, "%d", &(Widths[ival]));
	  if (items < 1) break;

	  q = (GPTR) malloc (sizeof (struct grec));
	  q->nr = nr; q->nc = nc; q->value = cval; q->next = (GPTR)0;

	  x = newglyph (nr, nc);
	  q->ptr = x;
	  for (n=0; n<q->nr; n++)
	    for (m=0; m<q->nc; m++)
	    {
	      fscanf (f, "%d", &v);
	      x[n][m] = (unsigned char)v;
	    }

	  if (database[ival] == (GPTR)0)
	    database[ival] = q;
	  else {
	    p = database[ival];
	    while (p->next) p = p->next;
	    p->next = q;
	  }
	} while (items > 0);
	fclose (f);

/* Compute maximum character widths and save them */
	for (i=0; i<127; i++)
	{
	  Widths[i] = 0;
	  p = database[i];
	  if (p == 0) continue;
	  printf ("Database: '%c'\n", i);
	  j = k = 0;
	  while (p)
	  {
	    j += p->nc;
	    k++;
	    p = p->next;
	  }
	  Widths[i] = (int)( (float)j/k + 0.5 );
	  if (DEBUG)
	    printf ("Width of %c is %d pixels\n", i, Widths[i]);
	}
}

