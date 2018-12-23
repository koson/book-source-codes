/* kFill algorithm for noise reduction */

#define MAX
#include "lib.h"
#define OBJECT 1
#define BACK 0
#define DEBUG 1

typedef unsigned char ** GIMAGE;

/* Prototypes - all in this file, alphabetical */
void kFill (IMAGE im, int K);
int kfiter (IMAGE im, int K, int val, IMAGE x);
int kfstep (IMAGE im, int K, int val, int I, int J, IMAGE x);
void mclear (IMAGE im, int val);
void kmark (IMAGE im, int I, int J, int val);
void dump (IMAGE im);
void kdump (IMAGE im, int K, int I, int J);

void main (int argc, char *argv[])
{
	IMAGE im;
	int K;

	if (argc < 3)
	{
	  printf ("Usage: kFill <input> <k> <output>\n");
	  exit (1);
	}

	im = Input_PBM (argv[1]);
	if (im == 0)
	{
	  printf ("The image file '%s' does not exist, or is unreadable.\n");
	  exit (2);
	}
	sscanf (argv[2], "%d", &K);

	kFill (im, K);

	Output_PBM (im, "kfill.pbm");
}


void kFill (IMAGE im, int K)
{
	int i,j,n;
	IMAGE x;

	x = newimage (im->info->nr, im->info->nc);
	for (i=0; i<im->info->nr; i++)
	  for (j=0; j<im->info->nc; j++)
	    x->data[i][j] = im->data[i][j];

	do
	{
	  n = kfiter (im, K, 0, x);
	  printf ("0 pass.\n");
	  dump (x);
	  n += kfiter (im, K, 1, x);
	  printf ("1 pass.\n");
	  dump (x);
	
	  for (i=0; i<im->info->nr; i++)
	    for (j=0; j<im->info->nc; j++)
	      im->data[i][j] = x->data[i][j];
	} while (n);
}

int kfiter (IMAGE im, int K, int val, IMAGE x)
{
	int i,j,k,n,m;

	k = 0;
	for (i=0; i<im->info->nr-K; i++)
	{
	  for (j=0; j<im->info->nc-K; j++)
	  {
	    k += kfstep (im, K, val, i, j, x);
	  }
	}
	return k;
}

int kfstep (IMAGE im, int K, int val, int I, int J, IMAGE x)
{
	int i,j,k,n,m, r, c;

	m = 0;
/* Are all CORE pixels the opposite of VAL? */
	for (i=I+2; i<I+K-2; i++)
	  for (j = J+2; j<J+K-2; j++)
	    if (im->data[i][j] == val) return 0;

/* Compute neighborhood parameters n, r, and c */
/* n is the number of VAL pixels */
	n = 0;
	for (i=I; i<I+K; i++)
	  for (j=J; j<J+K; j++)
	    if (i-I<2 || j-J<2 || i-I>=K-2 || j-J>=K-2)
		if (im->data[i][j] == val) n++;

/* r is the number of VAL corner pixels */
	r = 0;
	if (im->data[I][J] == val) r++;
	if (im->data[I][J+K-1] == val) r++;
	if (im->data[I+K-1][J] == val) r++;
	if (im->data[I+K-1][J+K-1] == val) r++;

/* c is the number of connected regions of value VAL */

	c = 2;
	for (i=I; i<I+K; i++)
	  for (j=J; j<J+K; j++)
	    if (i-I<2 || j-J<2 || i-I>=K-2 || j-J>=K-2)
	      if (im->data[i][j] == val)
	      {
		kmark (im, i, j, c);
		c++;
	      }
	c -= 2;
	mclear (im, val);

	if (c == 1 && ( (n>3*K-K/3) || (n==3*K-K/3 && (r==2)) ))
	{
	  printf ("KFSTEP: Core is OK. (val==%d) at (%d,%d)\n", val, I, J);
	  kdump(im, K, I, J);
	  printf ("Filling core with %d\n", val);
	  for (i=I+2; i<I+K-2; i++)
	    for (j = J+2; j<J+K-2; j++)
		x->data[i][j] = val;
	  m = 1;
	  kdump (x, K, I, J);
	  printf ("n=%d r = %d c = %d.\n", n, r, c);
	}

	return m;
}

void mclear (IMAGE im, int val)
{
	int i,j,f,q;

	q = 0;
	for (i=0; i<im->info->nr; i++)
	{
	  f = 0;
	  for (j=0; j<im->info->nc; j++)
	    if (im->data[i][j] > 1)
	    {
		im->data[i][j] = val;
		q = 1;
		f = 1;
	    }
	  if (f == 0 && q!= 0) return;
	}
}

void kmark (IMAGE im, int I, int J, int val)
{
	int i,j,k;

	k = im->data[I][J];
	im->data[I][J] = val;
	if (I+1<im->info->nr && im->data[I+1][J]==k) kmark (im,I+1,J,val);
	if (I-1>=0 && im->data[I-1][J]==k) kmark(im,I-1,J,val);
	if (J+1<im->info->nc && im->data[I][J+1]==k) kmark (im, I, J+1, val);
	if (J-1>=0 && im->data[I][J-1]==k) kmark (im, I, J-1, val);
}

void dump (IMAGE im)
{
	int i,j;

	for (i=0; i<im->info->nr; i++)
	{
	  for (j=0; j<im->info->nc; j++)
	    if (im->data[i][j]==0) printf (" ");
	    else if (im->data[i][j] == 1) printf ("#");
	    else printf ("%1d", im->data[i][j]);
	  printf ("\n");
	}
}

void kdump (IMAGE im, int K, int I, int J)
{
	int i,j;

	printf ("--------\n");
	for (i=I; i<I+K; i++)
	{
	  printf ("| ");
	  for (j=J; j<J+K; j++)
	    if (im->data[i][j]==0) printf (" ");
	    else if (im->data[i][j] == 1) printf ("#");
	    else printf ("%1d", im->data[i][j]);
	  printf (" |\n");
	}
	printf ("--------\n");
}
