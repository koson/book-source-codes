/* Zhang & Suen's thinnning algorithm */

#define MAX
#include "lib.h"
#include <math.h>

#define DEBUG 1

void thnz (IMAGE im);
int nays8 (IMAGE im, int r, int c);
int Connectivity (IMAGE im, int r, int c);
void Delete (IMAGE im, IMAGE tmp);

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

/* Second sub-iteration */
	for (i=1; i<im->info->nr-1; i++)
	  for (j=1; j<im->info->nc-1; j++)
	  {
	    if (im->data[i][j] != 1) continue;
	    k = nays8(im, i, j);
	    if ((k >= 2 && k <= 6) && Connectivity(im, i,j)==1)
	    {
	      if (im->data[i][j+1]*im->data[i-1][j]*im->data[i][j-1] == 0 &&
		  im->data[i-1][j]*im->data[i+1][j]*im->data[i][j-1] == 0)
		{
		  tmp->data[i][j] = 1;
		  again = 1;
		}
	    }
	  }

	  Delete (im, tmp);
	  if (again == 0) break;

/* First sub-iteration */
	for (i=1; i<im->info->nr-1; i++)
	  for (j=1; j<im->info->nc-1; j++)
	  {
	    if (im->data[i][j] != 1) continue;
	    k = nays8(im, i, j);
	    if ((k >= 2 && k <= 6) && Connectivity(im, i,j)==1)
	    {
	      if (im->data[i-1][j]*im->data[i][j+1]*im->data[i+1][j] == 0 &&
		  im->data[i][j+1]*im->data[i+1][j]*im->data[i][j-1] == 0)
		{
		  tmp->data[i][j] = 1;
		  again = 1;
		}
	    }
	  }

	  Delete (im, tmp);
	}

/* Restore levels */
	for (i=1; i<im->info->nr-1; i++)
	  for (j=1; j<im->info->nc-1; j++)
	  if (im->data[i][j] > 0) im->data[i][j] = 0;
	    else im->data[i][j] = 255;

	freeimage (tmp);
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
	      if (im->data[i][j] >= 1) k++;
	return k;
}

int Connectivity (IMAGE im, int r, int c)
{
	int i, N=0;

	if (im->data[r][c+1]   >= 1 && im->data[r-1][c+1] == 0) N++;
	if (im->data[r-1][c+1] >= 1 && im->data[r-1][c]   == 0) N++;
	if (im->data[r-1][c]   >= 1 && im->data[r-1][c-1] == 0) N++;
	if (im->data[r-1][c-1] >= 1 && im->data[r][c-1]   == 0) N++;
	if (im->data[r][c-1]   >= 1 && im->data[r+1][c-1] == 0) N++;
	if (im->data[r+1][c-1] >= 1 && im->data[r+1][c]   == 0) N++;
	if (im->data[r+1][c]   >= 1 && im->data[r+1][c+1] == 0) N++;
	if (im->data[r+1][c+1] >= 1 && im->data[r][c+1]   == 0) N++;

	return N;
}

