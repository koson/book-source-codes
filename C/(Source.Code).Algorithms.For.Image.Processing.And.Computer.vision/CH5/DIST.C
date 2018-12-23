/* Distance transform */

#define MAX
#include "lib.h"
#include <math.h>

void dt (IMAGE im);

void main (int argc, char *argv[])
{
	IMAGE data;

	if (argc < 3)
	{
	  printf ("Usage: dist <input file> <output file> \n");
	  exit (0);
	}

	data = Input_PBM (argv[1]);
	if (data == NULL)
	{
		printf ("Bad input file '%s'\n", argv[1]);
		exit(1);
	}

	dt (data);

	Output_PBM (data, argv[2]);
}

void dt (IMAGE im)
{
	int i,j,k=1, more=0, ii, jj;
	IMAGE d;

	d = newimage (im->info->nr, im->info->nc);
	for (i=0; i<im->info->nr; i++)
	  for (j=0; j<im->info->nc; j++)
	    d->data[i][j] = 255;

	do {

	more = 0;
	for (i=0; i<im->info->nr; i++)
	  for (j=0; j<im->info->nc; j++)
	  {
	    if (im->data[i][j] == 0)
	    {
	      k = 255;
	      for (ii=i-1; ii<=i+1; ii++)
		for (jj=j-1; jj<=j+1; jj++)
		  if (ii!= i || jj!= j)
		    if (im->data[ii][jj] < k && im->data[ii][jj]>0)
			k = im->data[ii][jj];
	      if (k == 255) { d->data[i][j] = 0; more++; }
		else { d->data[i][j] = k+1; more++; }
	    }
	  }

	for (i=0; i<im->info->nr; i++)
	  for (j=0; j<im->info->nc; j++)
	    im->data[i][j] = d->data[i][j];

	} while (more);

	freeimage (d);
}
