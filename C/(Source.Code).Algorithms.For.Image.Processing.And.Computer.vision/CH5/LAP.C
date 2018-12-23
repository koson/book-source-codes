/* Laplacian - find discontinuities in the derivative of the MAT */

#define MAX
#include "lib.h"
#include <math.h>

void main (int argc, char *argv[])
{
	IMAGE data, im;
	int i,j,k;

	if (argc < 3)
	{
	  printf ("Usage: lap <input file> <output file> \n");
	  exit (0);
	}

	data = Input_PBM (argv[1]);
	if (data == NULL)
	{
		printf ("Bad input file '%s'\n", argv[1]);
		exit(1);
	}

	im = newimage (data->info->nr, data->info->nc);
	for (i=0; i<im->info->nr; i++)
	  for (j=0; j<im->info->nc; j++)
	    im->data[i][j] = 128;

	for (i=1; i<data->info->nr-1; i++)
	  for (j=1; j<data->info->nc-1; j++)
	  {
	    k = 128;
	    k = data->data[i-1][j] + data->data[i+1][j] +
		data->data[i][j-1] + data->data[i][j+1];
	    k = k - 4*data->data[i][j];
	    if (abs(k) > 30) k = 128;
	    else k = 128 + 4*k;
	    im->data[i][j] = k;
	  }

	Output_PBM (im, argv[2]);
}
