/*	This program reads the sample text image (training data) and
   attempts to learn the character set. It does this by extracting glyphs
   and storing them in a database as templates. These are written to
   a file, which is used by the simple ocr program OCR1.		*/

#define MAX
#include "lib.h"
#define OBJECT 1
#define BACK 0
#define DEBUG 1

/* The bounding box of a line of text */
struct lrec {
	int rs, re, cs, ce;
} LINE[400];
int Nlines = -1;
int Space = 1;
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
GPTR database[256];

/* Prototypes - all in this file, alphabetical */
int bestmatch (GIMAGE x, int nr, int nc);
void clean (IMAGE im);
void dump_db (void);
void dumpglyph (IMAGE im, int rs, int cs, int re, int ce, char gly);
void extract (IMAGE im);
int glyeq (GIMAGE x, int nr, int nc, char gc, GPTR p);
void initialize(void);
GPTR insert_to_database (GIMAGE x, int rs, int re, int cs, int ce, char gc);
void lines   (IMAGE im);
int match (GIMAGE a, int nra, int nca, GIMAGE b, int nrb, int ncb);
GIMAGE newglyph (int nr, int nc);
void nextblackcol (IMAGE im, int *this, int rs, int re);
GIMAGE nextglyph (IMAGE im, struct lrec *thisline, int *thiscol, int *space,
		int *nr, int *nc);
void nextwhitecol (IMAGE im, int *this, int rs, int re);
void orient  (IMAGE im);
void savedatabase (char *filename);
void saveglyph (IMAGE im, int rs, int cs, int re, int ce, char gly, GPTR *x);
void txtread (IMAGE im, int lineno, char *result);
void mark (GIMAGE x, int r, int c, int nr, int nc, int val);
void holes (GIMAGE x, int nr, int nc);
void gprint (GIMAGE x, int nr, int nc);

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
	
	for (i=0; i<256; i++)
	  database[i] = (GPTR)0;
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

/* Extract the templates from the training image */
void extract (IMAGE im)
{
	int col, scol, ecol, qflag = 0, tspace=0, spacing;
	int thisline, thischar, rs,re,cs,ce, oldscol, nspace = 0;
	float mspace = 0;
	GPTR thisptr;

/* Use the first 10 lines (all characters) */
	for (thisline = 0; thisline <= 10; thisline++)
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

/* Find a column with black pixels in it */
	    scol = col;
	    nextblackcol (im, &scol, rs, re);
	    if (scol < 0) continue;

/* Find a following column that is all white */
	    ecol = scol;
	    nextwhitecol (im, &ecol, rs, re);
	    if (ecol < 0) continue;

/* We now have a glyph - dump it */
/* EXCEPTION: A double quote has two vertically separated sets of black. */
	    if (DEBUG)
	      dumpglyph (im, rs, scol, re, ecol, data[thisline][thischar]);

/* Save the glyph as a template for its character class */
	    saveglyph (im, rs, scol, re, ecol,
		 data[thisline][thischar], &thisptr);

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
	    if (oldscol < 0) oldscol = scol;
	    else
	    {
		spacing = scol - oldscol;
		spacing -= Space;
		tspace += spacing;
		nspace++;
		oldscol = scol;
	    }
	    col = ecol++;
	  } while (scol >= 0 && ecol >= 0);

	}
	mspace = (float)tspace/(float)nspace;
	if (DEBUG)
		printf ("Average character width: %f\n", mspace);
	Meanwidth = (int)(mspace+0.5);
}

/*	Return the index of the next column with a black pixel in it	*/
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

/*	Return the index of the next column with a black pixel in it	*/
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
	    if (im->data[i][j] == OBJECT) printf ("#");
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
	for (k=0; k<256; k++)
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
	    if (im->data[i][j] == OBJECT)
	    {
		first = j;
		break;
	    }
	  if (first >= 0) break;
	}
	cs = first;

/* Find the last black column */
	last = -1;
	for (j=ce; j>=cs; j--)
	{
	  for (i=rs; i<=re; i++)
	    if (im->data[i][j] == OBJECT)
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
	  for (j=cs; j<=ce; j++)
	    if (im->data[i][j] == OBJECT)
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
	    x[i-rs][j-cs] = im->data[i][j];

/* OPTIONAL: Find and mark holes */
	holes (x, re-rs+2, ce-cs+2);

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
	int i,j, rs, re, cs, ce;
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
	for (i=0; i<256; i++)
	{
	  if (database[i] == 0) continue;
	  p = database[i];
	  k = 0;
	  while (p)
	  {
	    fprintf (f, "%c %d %d %d\n", p->value, k, p->nr, p->nc);
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

void holes (GIMAGE x, int nr, int nc)
{
	int i,j,ii,jj;

/* Mark any background pixels connected to the bounding box with a 3 */
	for (i=0; i<nr; i++)
	{
	  if (x[i][0] == BACK) mark (x, i, 0, nr, nc, 3);
	  if (x[i][nc-1] == BACK) mark(x, i, nc-1, nr, nc, 3);
	}

	for (j=0; j<nc; j++)
	{
	  if (x[0][j] == BACK) mark (x, 0, j, nr, nc, 3);
	  if (x[nr-1][j] == BACK) mark (x, nr-1, j, nr, nc, 3);
	}

/* Any remaining background pixels are holes - mark them */
	for (i=0; i<nr; i++)
	  for (j=0; j<nc; j++)
	    if (x[i][j] == 3) x[i][j] = 0;
	    else if (x[i][j] == 0) x[i][j] = 3;

	gprint (x, nr, nc);
	printf ("Holes:\n");

}

void mark (GIMAGE x, int r, int c, int nr, int nc, int val)
{
	x[r][c] = val;
	if (r-1>=0 && x[r-1][c]==0) mark (x, r-1, c, nr, nc, val);
	if (r+1<nr && x[r+1][c]==0) mark (x, r+1, c, nr, nc, val);
	if (c-1>=0 && x[r][c-1]==0) mark (x, r, c-1, nr, nc, val);
	if (c+1<nc && x[r][c+1]==0) mark (x, r, c+1, nr, nc, val);
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

