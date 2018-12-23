/* OCR on simple perfect images */

#define MAX
#include "lib.h"

#define OBJECT 1
#define BACK 0

struct lrec {
	int rs, re, cs, ce;
} LINE[400];
int Nlines = -1;
int Space = 1;
int Meanwidth = 0;

typedef unsigned char ** GIMAGE;

struct grec {		/* Information concerning a glyph */
	char value;
	short int nr, nc;
	GIMAGE ptr;
	struct grec * next;
};
typedef struct grec * GPTR;
GPTR database[256];

void orient  (IMAGE im);
void lines   (IMAGE im);
void nextblackcol (IMAGE im, int *this, int rs, int re);
void nextwhitecol (IMAGE im, int *this, int rs, int re);
void dumpglyph (IMAGE im, int rs, int cs, int re, int ce, char gly);
void saveglyph (IMAGE im, int rs, int cs, int re, int ce, char gly);
GIMAGE newglyph (int nr, int nc);
void insert_to_database (GIMAGE x, int rs, int re, int cs, int ce, char gc);
int glyeq (GIMAGE x, int nr, int nc, char gc, GPTR p);
void dump_db ();
void initialize();
void txtread (IMAGE im, int lineno, char *result);
int match (GIMAGE a, int nra, int nca, GIMAGE b, int nrb, int ncb);
GIMAGE nextglyph (IMAGE im, struct lrec *thisline, int *thiscol, int *space,
		int *nr, int *nc);
void loaddatabase ();

void main (int argc, char *argv[])
{
	IMAGE im;
	char text[128];
	int i;
	FILE *f;

	if (argc < 2)
	{
	  printf ("Usage: ocr1 <image> <text file>\n");
	  printf ("This program examines a simple perfect text image\n");
	  printf ("and attempts to recognize the characters. A text\n");
	  printf ("file will be created with the ASCII version of the\n");
	  printf ("image.\n");
	  exit (1);
	}

	im = Input_PBM (argv[1]);
	if (im == 0)
	{
	  printf ("The image file '%s' does not exist, or is unreadable.\n");
	  exit (2);
	}

/* Get the database */
	loaddatabase ();

/* Determine the orientation - this image is supposed to be perfect */
	orient (im);

/* Find the lines of text and remember where they were */
	lines (im);

/* Read ... */
	f = fopen (argv[2], "w");
	if (f == NULL)
	{
		printf ("Can't open output file '%s'\n", argv[2]);
		exit(1);
	}

	for (i=0; i<Nlines; i++)
	{
	  txtread (im, i, text);
	  fprintf (f, "%s\n", text);
	}
}

void initialize()
{
	int i;
	
	for (i=0; i<256; i++)
	  database[i] = (GPTR)0;
}

void orient (IMAGE im)
{
}

/* Locate each line of text, and remember the start and end columns
   and rows. These are saved in the global array LINES.		    */
void lines (IMAGE im)
{
	int *hpro, i,j,k,n,m, N;
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
/*
	  printf ("Row %d:  %d\n", i, m);
*/
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

/*
	printf ("%d lines seen. Summary:\n", j-1);
	for (m=0; m<j; m++)
	{
	  printf ("Line %3d: (%d,%d)		(%d,%d)\n", m,LINE[m].rs,
		LINE[m].cs, LINE[m].rs, LINE[m].ce);
	  printf ("          (%d,%d)		(%d,%d)\n", m, LINE[m].re,
		LINE[m].cs, LINE[m].re, LINE[m].ce);
	}
	printf ("\n");
*/
	Nlines = j;
	free (hpro);
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

void dumpglyph (IMAGE im, int rs, int cs, int re, int ce, char gly)
{
	int i,j;

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

void saveglyph (IMAGE im, int rs, int cs, int re, int ce, char gly)
{
	int i,j,k;
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

/* Fill the remainder of the glyph record */
	insert_to_database (x, rs, re, cs, ce, gly);
}

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

void insert_to_database (GIMAGE x, int rs, int re, int cs, int ce, char gc)
{
	int i,j,k;
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
		return;
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
}

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

void txtread (IMAGE im, int lineno, char *result)
{
	int i,j,k, cs,ce, nr,nc, space, csold;
	GIMAGE x;

	cs = LINE[lineno].cs; ce = LINE[lineno].ce;
	csold = cs;
	k = 0;
	do
	{
	  x = nextglyph (im, &(LINE[lineno]), &cs, &space, &nr, &nc);
	  if (x == 0) break;

	  j = bestmatch (x, nr, nc);

/* A space or two? */
	  space = cs - csold - Meanwidth+1;
	  while (space >= Space)
	  {
	    result[k++] = ' ';
	    space -= Space;
	  }
	  csold = cs;

	  result[k++] = (char)j;
	  printf ("Character seen was '%c'\n", j);
	  free(x);
	} while (cs < ce);

	result[k++] = '\0';
	printf ("String was \"%s\"\n", result);
}

int bestmatch (GIMAGE x, int nr, int nc)
{
	int i,j,k, bestk, count;
	GPTR p;
	char result;

	bestk = -1; result = '\0';
	for (i=0; i<256; i++)
	{
	  if (database[i] == 0) continue;
	  p = database[i];
	  while (p)
	  {
	    k = match (x, nr, nc, p->ptr, p->nr, p->nc);
	    if (k > bestk)
	    {
		printf ("Previous best was %d now best is %d at '%c'\n", 
			bestk, k, i);
		bestk = k;
		result = i;
		count = 1;
	    } else if (k == bestk)
	    {
		count++;
		printf ("Tie with '%c'\n", i);
	    }
	    p = p->next;
	  }
	}
	printf ("Overall best is '%c' at %d.\n", result, bestk);
	return result;
}

int match (GIMAGE a, int nra, int nca, GIMAGE b, int nrb, int ncb)
{
	int i,j,dr, dc, tot, best, ii, jj, nr, nc;
	GIMAGE x, y;

	if (nra <= nrb && nca <= ncb)
	{
	  x = a; dr = nrb-nra; dc = ncb-nca;
	  y = b; nr = nra; nc = nca;
	} else if (nrb <= nra && ncb <= nca)
	{
	  x = b; dr = nra-nrb; dc = nca-ncb;
	  y = a; nr = nrb; nc = ncb;
	} else return 0;

	best = 0;
	for (i=0; i<=dr; i++)
	  for (j=0; j<=dc; j++)
	  {
	    tot = 0;
	    for (ii=0; ii<nr; ii++)
	    {
	      for (jj=0; jj<nc; jj++)
		if (y[i+ii][j+jj] == OBJECT && x[ii][jj]==OBJECT)
		{
		  tot++;
	        }
		else if(y[i+ii][j+jj] == OBJECT || x[ii][jj]==OBJECT)
		{ 
		  tot--; 
		}
	    }
	    if (best < tot)
	      best = tot;
	  }
	return best;
}

GIMAGE nextglyph (IMAGE im, struct lrec *thisline, int *thiscol, int *space,
		int *nr, int *nc)
{
	int i,j,k, rs, re, cs, ce, first, last;
	int ecol, scol;
	static int oldscol = -1;
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

void loaddatabase ()
{
	FILE *f;
	int i,j,k, n,m, nr, nc, ival, items, v;
	GPTR p, q;
	GIMAGE x;
	char cval;

	f = fopen ("helv.db", "r");
	if (f == NULL)
	{
	  printf ("Cannot read database from file '%s'\n", "helvetica.db");
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
}
