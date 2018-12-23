/* Zhang & Suen's thinnning algorithm */
/* Holt's variation */

#define MAX
#include "lib.h"
#include <math.h>

#define DEBUG 1
#define TRUE 1
#define FALSE 0
#define NORTH 1
#define SOUTH 3
#define REMOVE_STAIR 1

void thnz (IMAGE im);
void Delete (IMAGE im, IMAGE tmp);
void check (int v1, int v2, int v3);
int edge (IMAGE im, int r, int c);
void stair (IMAGE im, IMAGE tmp, int dir);

int t00, t01, t11, t01s;

void main (int argc, char *argv[])
{
	IMAGE data, im;
	int i,j;

	if (argc < 3)
	{
	  printf ("Usage: thnz <input file> <output file> \n");
	  exit (0);
	}

	data = Input_PBM (argv[1]);
	if (data == NULL)
	{
		printf ("Bad input file '%s'\n", argv[1]);
		exit(1);
	}

	im = newimage (data->info->nr+2, data->info->nc+2);
	for (i=0; i<data->info->nr; i++)
	  for (j=0; j<data->info->nc; j++)
	    im->data[i+1][j+1] = data->data[i][j];
	for (i=0; i<im->info->nr; i++) 
	{
	  im->data[i][0] = 1;
	  im->data[i][im->info->nc-1] = 1;
	}
	for (j=0; j<im->info->nc; j++)
	{
	  im->data[0][j] = 1;
	  im->data[im->info->nr-1][j] = 1;
	}

	thnz (im);

	for (i=0; i<data->info->nr; i++)
           for (j=0; j<data->info->nc; j++)
	      data->data[i][j] = im->data[i+1][j+1];

	Output_PBM (data, argv[2]);
}

void thnz (IMAGE im)
{
	int i,j,k, again=1;
	IMAGE tmp;

	tmp = newimage (im->info->nr, im->info->nc);

/* BLACK = 1, WHITE = 0. */
	for (i=0; i<im->info->nr; i++)
	  for (j=0; j<im->info->nc; j++)
	  {
	    if (im->data[i][j] > 0) im->data[i][j] = 0;
	      else im->data[i][j] = 1;
	    tmp->data[i][j] = 0;
	  }

/* Mark and delete */
	while (again)
	{
	  again = 0;

	  for (i=1; i<im->info->nr-1; i++)
	    for (j=1; j<im->info->nc-1; j++)
	    {
	      if (im->data[i][j] == 1) 
	      {
	        if (!edge(im,i,j) || 
		    (edge(im,i,j+1) && im->data[i-1][j] && im->data[i+1][j]) ||
		    (edge(im,i+1,j) && im->data[i][j-1] && im->data[i][j+1]) ||
		    (edge(im,i,j+1) && edge(im, i+1,j+1) && edge(im, i+1, j)) )
		  tmp->data[i][j] = 0;
	        else 
	        {
		  tmp->data[i][j] = 1;
		  again = 1; 
	        }
	      } else tmp->data[i][j] = 1;
	    }

	  Delete (im, tmp);
	}

/* Staircase removal */
	if (REMOVE_STAIR)
	{
	  stair (im, tmp, NORTH);
	  Delete (im, tmp);
	  stair (im, tmp, SOUTH);
	  Delete (im, tmp);
	}

/* Restore levels */
	for (i=1; i<im->info->nr-1; i++)
	  for (j=1; j<im->info->nc-1; j++)
	  if (im->data[i][j] > 0) im->data[i][j] = 0;
	    else im->data[i][j] = 255;

	freeimage (tmp);
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
	       tmp->data[i][j] = 0;		/* Survives */
	     else
	       tmp->data[i][j] = 1;
	   } else if (dir == SOUTH)
	   {
	     if (C && !(S && 
		      ((E && !SE && !NW && (!W || !N)) || 
		       (W && !SW && !NE && (!E || !N)) )) )
	       tmp->data[i][j] = 0;		/* Survives */
	     else
	       tmp->data[i][j] = 1;
	   }
	  }
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
