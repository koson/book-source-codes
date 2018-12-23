/* Stentiford's thinning algorithm */

#define MAX
#include "lib.h"
#include <math.h>

#define DEBUG 0

void thnstent (IMAGE im);
int nays8 (IMAGE im, int r, int c);
int Yokoi (IMAGE im, int r, int c);
void pre_smooth (IMAGE im);

void main (int argc, char *argv[])
{
	IMAGE data, im;
	int i,j;

	if (argc < 3)
	{
	  printf ("Usage: thnstent <input file> <output file> \n");
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

	thnstent (im);

	for (i=0; i<data->info->nr; i++)
	   for (j=0; j<data->info->nc; j++)
	      data->data[i][j] = im->data[i+1][j+1];

	Output_PBM (data, argv[2]);
}

void thnstent (IMAGE im)
{
	int i,j,k, again=1;

/* BLACK = 0, WHITE = 1. */
	for (i=0; i<im->info->nr; i++)
	  for (j=0; j<im->info->nc; j++)
	    if (im->data[i][j] > 0) im->data[i][j] = 1;

/* Mark and delete */
	while (again)
	{
	  again = 0;

/* Matching template M1 - scan top-bottom, left - right */
	  for (i=1; i<im->info->nr-1; i++)
	    for (j=1; j<im->info->nc-1; j++)
	      if (im->data[i][j] == 0)
		if (im->data[i-1][j] == 1 && im->data[i+1][j] != 1)
		  if (nays8(im, i, j) != 1  && Yokoi (im, i, j) == 1)
		    im->data[i][j] = 2;

/* Template M2 bottom-top, left-right */
	  for (j=1; j<im->info->nc-1; j++)
	    for (i=im->info->nr-2; i>=1; i--)
	      if (im->data[i][j] == 0)
		if (im->data[i][j-1] == 1 && im->data[i][j+1] != 1)
		  if (nays8(im, i, j) != 1  && Yokoi (im, i, j) == 1)
		    im->data[i][j] = 2;

/* Template M3 right-left, bottom-top */
	  for (i=im->info->nr-2; i>=1; i--)
	    for (j=im->info->nc-2; j>=1; j--)
	      if (im->data[i][j] == 0)
		if (im->data[i-1][j] != 1 && im->data[i+1][j] == 1)
		  if (nays8(im, i, j) != 1  && Yokoi (im, i, j) == 1)
		    im->data[i][j] = 2;

/* Template M4 */
	  for (j=im->info->nc-2; j>=1; j--)
	    for (i=1; i<im->info->nr-1; i++)
	      if (im->data[i][j] == 0)
		if (im->data[i][j-1] != 1 && im->data[i][j+1] == 1)
		  if (nays8(im, i, j) != 1  && Yokoi (im, i, j) == 1)
		    im->data[i][j] = 2;

/* Delete pixels that are marked (== 2) */
	  for (i=1; i<im->info->nr-1; i++)
	    for (j=1; j<im->info->nc-1; j++)
	      if (im->data[i][j] == 2)
	      {
		im->data[i][j] = 1;
		again = 1;
	      }

	  /*Display (im); */
	}

	for (i=1; i<im->info->nr-1; i++)
	  for (j=1; j<im->info->nc-1; j++)
	    if (im->data[i][j] > 0) im->data[i][j] = 255;

	/* Display (im); */
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
	Display (im);

	for (i=0; i<im->info->nr; i++)
	  for (j=0; j<im->info->nc; j++)
	    if (im->data[i][j] == 2) im->data[i][j] = 1;
	Display (im);
}

