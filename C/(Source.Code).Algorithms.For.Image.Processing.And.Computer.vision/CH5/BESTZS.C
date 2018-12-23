/* Zhang & Suen's thinnning algorithm */
/* Stentiford's pre-processing
   Holt's post-processing 	*/

#define MAX
#include "lib.h"
#include <math.h>

#define TRUE 1
#define FALSE 0
#define NORTH 1
#define SOUTH 3
#define REMOVE_STAIR 1

void thnz (IMAGE im);
int nays8 (IMAGE im, int r, int c);
int Connectivity (IMAGE im, int r, int c);
void Delete (IMAGE im, IMAGE tmp);
void check (int v1, int v2, int v3);
int edge (IMAGE im, int r, int c);
void stair (IMAGE im, IMAGE tmp, int dir);
int Yokoi (IMAGE im, int r, int c);
void pre_smooth (IMAGE im);
void match_du (IMAGE im, int r, int c, int k);
void aae (IMAGE image);
int snays (IMAGE im, int r, int c);

int t00, t01, t11, t01s;

void main (int argc, char *argv[])
{
	IMAGE data, im;
	int i,j;

	if (argc < 3)
	{
	  printf ("Usage: thnbest <input file> <output file> \n");
	  exit (0);
	}

	data = Input_PBM (argv[1]);
	if (data == NULL)
	{
		printf ("Bad input file '%s'\n", argv[1]);
		exit(1);
	}

/* Embed input into a slightly larger image */
	im = newimage (data->info->nr+2, data->info->nc+2);
	for (i=0; i<data->info->nr; i++)
	  for (j=0; j<data->info->nc; j++)
	    if (data->data[i][j]) im->data[i+1][j+1] = 1;
	     else im->data[i+1][j+1] = 0;

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

/* Pre_process */
	pre_smooth (im);
	aae (im);

/* Thin */
	thnz (im);

	for (i=0; i<data->info->nr; i++)
           for (j=0; j<data->info->nc; j++)
	      data->data[i][j] = im->data[i+1][j+1];

	Output_PBM (data, argv[2]);
}

/*	Zhang-Suen with Holt's staircase removal */
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

/* Post_process */
        stair (im, tmp, NORTH);
        Delete (im, tmp);
        stair (im, tmp, SOUTH);
        Delete (im, tmp);

/* Restore levels */
	for (i=1; i<im->info->nr-1; i++)
	  for (j=1; j<im->info->nc-1; j++)
	  if (im->data[i][j] > 0) im->data[i][j] = 0;
	    else im->data[i][j] = 255;

	freeimage (tmp);
}

/*	Delete any pixel in IM corresponding to a 1 in TMP	*/
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

/*	Number of neighboring 1 pixels	*/
int nays8 (IMAGE im, int r, int c)
{
	int i,j,k=0;

	for (i=r-1; i<=r+1; i++)
	  for (j=c-1; j<=c+1; j++)
	    if (i!=r || c!=j)
	      if (im->data[i][j] >= 1) k++;
	return k;
}

/*	Number of neighboring 0 pixels	*/
int snays (IMAGE im, int r, int c)
{
        int i,j,k=0;

	for (i=r-1; i<=r+1; i++)
	  for (j=c-1; j<=c+1; j++)
	    if (i!=r || c!=j)
	      if (im->data[i][j] == 0) k++;
	return k;
}

/*	Connectivity by counting black-white transitions on the boundary	*/
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

/*	Stentiford's boundary smoothing method		*/
void pre_smooth (IMAGE im)
{
	int i,j;

	for (i=0; i<im->info->nr; i++)
	  for (j=0; j<im->info->nc; j++)
	    if (im->data[i][j] == 0)
		if (snays(im, i, j) <= 2 && Yokoi (im, i, j)<2)
		  im->data[i][j] = 2;

	for (i=0; i<im->info->nr; i++)
	  for (j=0; j<im->info->nc; j++)
	    if (im->data[i][j] == 2) im->data[i][j] = 1;
}

/*	Stentiford's Acute Angle Emphasis	*/
void aae (IMAGE im)
{
	int i,j, again = 0, k;

	again = 0;
	for (k=5; k>= 1; k-=2)
	{
	  for (i=2; i<im->info->nr-2; i++)
	    for (j=2; j<im->info->nc-2; j++)
	      if (im->data[i][j] == 0)
		match_du (im, i, j, k);

	  for (i=2; i<im->info->nr-2; i++)
	    for (j=2; j<im->info->nc-2; j++)
	    if (im->data[i][j] == 2)
	    {
		again = 1;
		im->data[i][j] = 1;
	    }

	  if (again == 0) break;
	} 
}

/*	Template matches for acute angle emphasis	*/
void match_du (IMAGE im, int r, int c, int k)
{

/* D1 */
	if (im->data[r-2][c-2] == 0 && im->data[r-2][c-1] == 0 &&
	    im->data[r-2][c]   == 1 && im->data[r-2][c+1] == 0 &&
	    im->data[r-2][c+2] == 0 &&
	    im->data[r-1][c-2] == 0 && im->data[r-1][c-1] == 0 &&
	    im->data[r-1][c]   == 1 && im->data[r-1][c+1] == 0 &&
	    im->data[r-1][c+2] == 0 &&
	    im->data[r][c-2] == 0 && im->data[r][c-1] == 0 &&
	    im->data[r][c]   == 0 && im->data[r][c+1] == 0 &&
	    im->data[r][c+2] == 0 &&
	    im->data[r+1][c-2] == 0 && im->data[r+1][c-1] == 0 &&
	    im->data[r+1][c]   == 0 && im->data[r+1][c+1] == 0 &&
	    im->data[r+1][c+2] == 0 &&
	    im->data[r+2][c-1] == 0 &&
	    im->data[r+2][c]   == 0 && im->data[r+2][c+1] == 0 )
	{
		im->data[r][c] = 2;
		return;
	}

/* D2 */
	if (k >= 2)
	if (im->data[r-2][c-2] == 0 && im->data[r-2][c-1] == 1 &&
	    im->data[r-2][c]   == 1 && im->data[r-2][c+1] == 0 &&
	    im->data[r-2][c+2] == 0 &&
	    im->data[r-1][c-2] == 0 && im->data[r-1][c-1] == 0 &&
	    im->data[r-1][c]   == 1 && im->data[r-1][c+1] == 0 &&
	    im->data[r-1][c+2] == 0 &&
	    im->data[r][c-2] == 0 && im->data[r][c-1] == 0 &&
	    im->data[r][c]   == 0 && im->data[r][c+1] == 0 &&
	    im->data[r][c+2] == 0 &&
	    im->data[r+1][c-2] == 0 && im->data[r+1][c-1] == 0 &&
	    im->data[r+1][c]   == 0 && im->data[r+1][c+1] == 0 &&
	    im->data[r+1][c+2] == 0 &&
	    im->data[r+2][c-1] == 0 &&
	    im->data[r+2][c]   == 0 && im->data[r+2][c+1] == 0 )
	{
		im->data[r][c] = 2;
		return;
	}

/* D3 */
	if (k>=3)
	if (im->data[r-2][c-2] == 0 && im->data[r-2][c-1] == 0 &&
	    im->data[r-2][c]   == 1 && im->data[r-2][c+1] == 1 &&
	    im->data[r-2][c+2] == 0 &&
	    im->data[r-1][c-2] == 0 && im->data[r-1][c-1] == 0 &&
	    im->data[r-1][c]   == 1 && im->data[r-1][c+1] == 0 &&
	    im->data[r-1][c+2] == 0 &&
	    im->data[r][c-2] == 0 && im->data[r][c-1] == 0 &&
	    im->data[r][c]   == 0 && im->data[r][c+1] == 0 &&
	    im->data[r][c+2] == 0 &&
	    im->data[r+1][c-2] == 0 && im->data[r+1][c-1] == 0 &&
	    im->data[r+1][c]   == 0 && im->data[r+1][c+1] == 0 &&
	    im->data[r+1][c+2] == 0 &&
	    im->data[r+2][c-1] == 0 &&
	    im->data[r+2][c]   == 0 && im->data[r+2][c+1] == 0 )
	{
		im->data[r][c] = 2;
		return;
	}

/* D4 */
	if (k>=4)
	if (im->data[r-2][c-2] == 0 && im->data[r-2][c-1] == 1 &&
	    im->data[r-2][c]   == 1 && im->data[r-2][c+1] == 0 &&
	    im->data[r-2][c+2] == 0 &&
	    im->data[r-1][c-2] == 0 && im->data[r-1][c-1] == 1 &&
	    im->data[r-1][c]   == 1 && im->data[r-1][c+1] == 0 &&
	    im->data[r-1][c+2] == 0 &&
	    im->data[r][c-2] == 0 && im->data[r][c-1] == 0 &&
	    im->data[r][c]   == 0 && im->data[r][c+1] == 0 &&
	    im->data[r][c+2] == 0 &&
	    im->data[r+1][c-2] == 0 && im->data[r+1][c-1] == 0 &&
	    im->data[r+1][c]   == 0 && im->data[r+1][c+1] == 0 &&
	    im->data[r+1][c+2] == 0 &&
	    im->data[r+2][c-1] == 0 &&
	    im->data[r+2][c]   == 0 && im->data[r+2][c+1] == 0 )
	{
		im->data[r][c] = 2;
		return;
	}

/* D5 */
	if (k>=5)
	if (im->data[r-2][c-2] == 0 && im->data[r-2][c-1] == 0 &&
	    im->data[r-2][c]   == 1 && im->data[r-2][c+1] == 1 &&
	    im->data[r-2][c+2] == 0 &&
	    im->data[r-1][c-2] == 0 && im->data[r-1][c-1] == 0 &&
	    im->data[r-1][c]   == 1 && im->data[r-1][c+1] == 1 &&
	    im->data[r-1][c+2] == 0 &&
	    im->data[r][c-2] == 0 && im->data[r][c-1] == 0 &&
	    im->data[r][c]   == 0 && im->data[r][c+1] == 0 &&
	    im->data[r][c+2] == 0 &&
	    im->data[r+1][c-2] == 0 && im->data[r+1][c-1] == 0 &&
	    im->data[r+1][c]   == 0 && im->data[r+1][c+1] == 0 &&
	    im->data[r+1][c+2] == 0 &&
	    im->data[r+2][c-1] == 0 &&
	    im->data[r+2][c]   == 0 && im->data[r+2][c+1] == 0 )
	{
		im->data[r][c] = 2;
		return;
	}

/* U1 */
	if (im->data[r+2][c-2] == 0 && im->data[r+2][c-1] == 0 &&
	    im->data[r+2][c]   == 1 && im->data[r+2][c+1] == 0 &&
	    im->data[r+2][c+2] == 0 &&
	    im->data[r+1][c-2] == 0 && im->data[r+1][c-1] == 0 &&
	    im->data[r+1][c]   == 1 && im->data[r+1][c+1] == 0 &&
	    im->data[r+1][c+2] == 0 &&
	    im->data[r][c-2] == 0 && im->data[r][c-1] == 0 &&
	    im->data[r][c]   == 0 && im->data[r][c+1] == 0 &&
	    im->data[r][c+2] == 0 &&
	    im->data[r-1][c-2] == 0 && im->data[r-1][c-1] == 0 &&
	    im->data[r-1][c]   == 0 && im->data[r-1][c+1] == 0 &&
	    im->data[r-1][c+2] == 0 &&
	    im->data[r-1][c-1] == 0 &&
	    im->data[r-1][c]   == 0 && im->data[r-1][c+1] == 0 )
	{
		im->data[r][c] = 2;
		return;
	}

/* U2 */
	if (k>=2)
	if (im->data[r+2][c-2] == 0 && im->data[r+2][c-1] == 1 &&
	    im->data[r+2][c]   == 1 && im->data[r+2][c+1] == 0 &&
	    im->data[r+2][c+2] == 0 &&
	    im->data[r+1][c-2] == 0 && im->data[r+1][c-1] == 0 &&
	    im->data[r+1][c]   == 1 && im->data[r+1][c+1] == 0 &&
	    im->data[r+1][c+2] == 0 &&
	    im->data[r][c-2] == 0 && im->data[r][c-1] == 0 &&
	    im->data[r][c]   == 0 && im->data[r][c+1] == 0 &&
	    im->data[r][c+2] == 0 &&
	    im->data[r-1][c-2] == 0 && im->data[r-1][c-1] == 0 &&
	    im->data[r-1][c]   == 0 && im->data[r-1][c+1] == 0 &&
	    im->data[r-1][c+2] == 0 &&
	    im->data[r-2][c-1] == 0 &&
	    im->data[r-2][c]   == 0 && im->data[r-2][c+1] == 0 )
	{
		im->data[r][c] = 2;
		return;
	}

/* U3 */
	if (k>=3)
	if (im->data[r+2][c-2] == 0 && im->data[r+2][c-1] == 0 &&
	    im->data[r+2][c]   == 1 && im->data[r+2][c+1] == 1 &&
	    im->data[r+2][c+2] == 0 &&
	    im->data[r+1][c-2] == 0 && im->data[r+1][c-1] == 0 &&
	    im->data[r+1][c]   == 1 && im->data[r+1][c+1] == 0 &&
	    im->data[r+1][c+2] == 0 &&
	    im->data[r][c-2] == 0 && im->data[r][c-1] == 0 &&
	    im->data[r][c]   == 0 && im->data[r][c+1] == 0 &&
	    im->data[r][c+2] == 0 &&
	    im->data[r-1][c-2] == 0 && im->data[r-1][c-1] == 0 &&
	    im->data[r-1][c]   == 0 && im->data[r-1][c+1] == 0 &&
	    im->data[r-1][c+2] == 0 &&
	    im->data[r-2][c-1] == 0 &&
	    im->data[r-2][c]   == 0 && im->data[r-2][c+1] == 0 )
	{
		im->data[r][c] = 2;
		return;
	}

/* U4 */
	if (k>=4)
	if (im->data[r+2][c-2] == 0 && im->data[r+2][c-1] == 1 &&
	    im->data[r+2][c]   == 1 && im->data[r+2][c+1] == 0 &&
	    im->data[r+2][c+2] == 0 &&
	    im->data[r+1][c-2] == 0 && im->data[r+1][c-1] == 1 &&
	    im->data[r+1][c]   == 1 && im->data[r+1][c+1] == 0 &&
	    im->data[r+1][c+2] == 0 &&
	    im->data[r][c-2] == 0 && im->data[r][c-1] == 0 &&
	    im->data[r][c]   == 0 && im->data[r][c+1] == 0 &&
	    im->data[r][c+2] == 0 &&
	    im->data[r-1][c-2] == 0 && im->data[r-1][c-1] == 0 &&
	    im->data[r-1][c]   == 0 && im->data[r-1][c+1] == 0 &&
	    im->data[r-1][c+2] == 0 &&
	    im->data[r-2][c-1] == 0 &&
	    im->data[r-2][c]   == 0 && im->data[r-2][c+1] == 0 )
	{
		im->data[r][c] = 2;
		return;
	}

/* U5 */
	if (k>=5)
	if (im->data[r+2][c-2] == 0 && im->data[r+2][c-1] == 0 &&
	    im->data[r+2][c]   == 1 && im->data[r+2][c+1] == 1 &&
	    im->data[r+2][c+2] == 0 &&
	    im->data[r+1][c-2] == 0 && im->data[r+1][c-1] == 0 &&
	    im->data[r+1][c]   == 1 && im->data[r+1][c+1] == 1 &&
	    im->data[r+1][c+2] == 0 &&
	    im->data[r][c-2] == 0 && im->data[r][c-1] == 0 &&
	    im->data[r][c]   == 0 && im->data[r][c+1] == 0 &&
	    im->data[r][c+2] == 0 &&
	    im->data[r-1][c-2] == 0 && im->data[r-1][c-1] == 0 &&
	    im->data[r-1][c]   == 0 && im->data[r-1][c+1] == 0 &&
	    im->data[r-1][c+2] == 0 &&
	    im->data[r-2][c-1] == 0 &&
	    im->data[r-2][c]   == 0 && im->data[r-2][c+1] == 0 )
	{
		im->data[r][c] = 2;
		return;
	}
}

/*	Yokoi's connectivity measure	*/
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

	return k;
}

/*	Holt's staircase removal stuff	*/
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

