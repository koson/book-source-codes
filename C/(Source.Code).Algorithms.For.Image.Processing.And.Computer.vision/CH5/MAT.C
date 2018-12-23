/* Calculation of Blum's medial axis from a bi-level image */
/* THIS IS ONLY APPROXIMATE FOR A SAMPLED IMAGE !! */
/* The MAT is defined for continuous objects only  */

#define MAX
#include "lib.h"
#include <math.h>

#define DISTANCE_4 1
#define DISTANCE_8 2
#define DISTANCE_E 3

float distance_4 (int r1, int c1, int r2, int c2);
float distance_8 (int r1, int c1, int r2, int c2);
float distance_E (int r1, int c1, int r2, int c2);
void thnma (IMAGE im);
float map (IMAGE im, int r, int c);
void bentry (int r, int c);
int ncount (IMAGE data, int I, int J, int v);
void skel (IMAGE data);
int localmax (IMAGE im, int I, int J);

struct bs {
	int r, c;
	struct bs *next;
};

typedef struct bs * bptr;
bptr blist = 0;

int DIST = DISTANCE_E;
IMAGE skeleton=0;
IMAGE count=0;
int bound[2][1000];

void main (int argc, char *argv[])
{
	IMAGE data;
	int i,j;

	if (argc < 4)
	{
	  printf ("Usage: mat <input file> <output file> <distance measure>\n");
	  exit (0);
	}

	if (argv[3][0] == '4') DIST = DISTANCE_4;
	 else if (argv[3][0] == '8') DIST = DISTANCE_8;
	 else if (argv[3][0] == 'E') DIST = DISTANCE_E;
	 else if (argv[3][0] == 'e') DIST = DISTANCE_E;
	 else {
		printf ("Bad distance measure '%s' specified.\n", argv[3]);
		exit (1);
	}

	data = Input_PBM (argv[1]);
	if (data == NULL)
	{
		printf ("Bad input file '%s'\n", argv[1]);
		exit(1);
	}

	count = newimage (data->info->nr, data->info->nc);
	skeleton = newimage (data->info->nr, data->info->nc);
        for (i=0; i<count->info->nr; i++)
          for (j=0; j<count->info->nc; j++)
	  {
		count->data[i][j] = 255;
		skeleton->data[i][j] = 255;
	  }
	thnma (data);
	printf ("Saving distance map in file '%s'\n", argv[2]);
	Output_PBM (data, argv[2]);
	skel(data);
}

float distance_4 (int r1, int c1, int r2, int c2)
{
	return (float)(abs(r2-r1) + abs(c2-c1));
}

float distance_8 (int r1, int c1, int r2, int c2)
{
	int a, b;
	
	a = abs(r2-r1);
	b = abs(c2-c1);
	if (a < b) return (float)b;
	else return (float)a;
}

float distance_E (int r1, int c1, int r2, int c2)
{  
	float x;

	x = (float)( (r2-r1)*(r2-r1) + (c2-c1)*(c2-c1) );
	return (float) sqrt ((double)x);
}

void thnma (IMAGE im)
{
	int i,j,k;

/* BLACK = 0, WHITE = 255. Mark the boundary. */
	for (i=0; i<im->info->nr; i++)
	  for (j=0; j<im->info->nc; j++)
	    if (im->data[i][j] > 0) im->data[i][j] = 255;

	for (i=1; i<im->info->nr-1; i++)
	  for (j=1; j<im->info->nc-1; j++)
	    if (im->data[i][j] == 0 && (im->data[i-1][j]==255 ||
		im->data[i+1][j]==255 || im->data[i][j-1]==255 ||
		im->data[i][j+1]==255 || im->data[i+1][j+1]==255 ||
		im->data[i+1][j-1]==255 || im->data[i-1][j+1]==255 ||
		im->data[i-1][j-1]==255))
		bentry (i, j);

/* Now look at all non boundary pixels, computing the distance to
	the nearest boundary pixel. Keep track of equal distances. */
	for (i=1; i<im->info->nr-1; i++)
	{
	  for (j=1; j<im->info->nc-1; j++)
	    if (im->data[i][j] == 0)
	      im->data[i][j] = (int)(map(im, i, j)*10.0);
	}
	printf ("Result.\n");
	Display (im);
}

float map (IMAGE im, int r, int c)
{
	int i,j, nmind = 0;
	float d, mind;
	bptr a,b;

	mind = im->info->nr*im->info->nc;
	a = blist;
	while (a)
	{
	  i = a->r; j = a->c;
	  if (DIST == DISTANCE_4)
	    d = distance_4 (r, c, i, j);
	  else if (DIST == DISTANCE_8)
	    d = distance_8 (r, c, i, j);
	  else if (DIST == DISTANCE_E)
	    d = distance_E (r, c, i, j);

	  if (fabs(d-mind) < 0.01) nmind++;
	  else if (d < mind)			/* Better distance */
	  {
		mind = d;
		nmind = 1;
	  }  
	  a = a->next;
	}

	if (DIST == DISTANCE_8)
	  nmind = nmind/8;

	if (nmind > 7)
	{
	  printf ("Distance %f has %d instances. (%d,%d)\n", mind, nmind, r, c);
	  b = blist;
	  while (b)
	  {
	    i = b->r; j = b->c;
	    d = distance_8 (r, c, i, j);
	    if (d == mind) printf ("(%d,%d)\n", i,j);
	    b = b->next;
	  }
	}

	count->data[r][c] = nmind;
	return mind;
}

void bentry (int r, int c)
{
	bptr a;

	a = (bptr)malloc (sizeof(struct bs));
	a->r = r; a->c = c;
	a->next = blist;
	blist = a;
}

int localmax (IMAGE im, int I, int J)
{
	int i,j,k, res=1;

	k = im->data[I][J];
	for (i=I-1; i<=I+1; i++)
	  for (j=J-1; j<=J+1; j++)
	    if (im->data[i][j] > k) res = 0;
	return res;
}

void skel (IMAGE data)
{
	int i,j,k;

	for (i=1; i<data->info->nr-1; i++)
	  for (j=1; j<data->info->nc-1; j++)
	  {
	    if (data->data[i][j] == 255) continue;
	    if (localmax(data, i,j))
		skeleton->data[i][j] = 0;
	    if (count->data[i][j] > 1) skeleton->data[i][j] = 0;
	  }
	Output_PBM (skeleton, "mat.pbm");
	printf ("Displaying 'skeleton' in file 'mat.pbm'\n");
}
