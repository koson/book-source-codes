/* Identify the skew angle by connecting the bottom center points
   on the bounding boxes of the connected components.             */

#define MAX
#include "lib.h"

#define OBJECT 1
#define BACK 0
#define SIZE 20

typedef unsigned char ** GIMAGE;

void unmark (IMAGE im, int is, int js, int ie, int je, int val);
void markcc (IMAGE im, IMAGE im2);
void mark (IMAGE im, int row, int col, int MARK);
void bbox (IMAGE x,int I,int J, int val,int *ulr,
	int *ulc,int *lrr,int *lrc);

void main (int argc, char *argv[])
{
	IMAGE im, im2;
	char text[128];
	int i, j;
	FILE *f;

	if (argc < 2)
	{
	  printf ("Usage: baird <image> \n");
	  exit (1);
	}

	im = Input_PBM (argv[1]);
	if (im == 0)
	{
	  printf ("The image file '%s' does not exist, or is unreadable.\n");
	  exit (2);
	}

	im2 = newimage (im->info->nr, im->info->nc);
	for (i=0; i<im->info->nr; i++)
	  for (j=0; j<im->info->nc; j++)
	    im2->data[i][j] = 0;

	markcc (im, im2);

	for (i=0; i<im->info->nr; i++)
	  for (j=0; j<im->info->nc; j++)
	    if (im2->data[i][j] >= 1)
	      im2->data[i][j] = 255;
	printf ("Saving the output in 'baird.pgm'.\n");
	Output_PBM (im2, "baird.pgm");
	Display (im2);
	printf ("Now find the skew angle using 'hskew baird.pgm'\n\n");
}

void markcc (IMAGE im, IMAGE im2)
{
	int i,j, ulr, ulc, lrr, lrc;

	for (i=0; i<im->info->nr; i++)
	{
	  for (j=0; j<im->info->nc; j++)
	    if (im->data[i][j] == OBJECT)
	    {
	      mark (im, i, j, 2);
	      bbox (im, i, j, 2, &ulr, &ulc, &lrr, &lrc);
	      im2->data[lrr][(lrc+ulc)/2] = 1;
	      unmark (im, ulr, ulc, lrr, lrc, 2);
	    }
	}
}

void mark (IMAGE im, int row, int col, int MARK)
{
	im->data[row][col] = MARK;
	if (row-1>=0 && col-1>=0 && im->data[row-1][col-1] == OBJECT) 
		mark (im, row-1,col-1, MARK);
	if (row-1>=0 && im->data[row-1][col] == OBJECT) 
		mark (im, row-1,col, MARK);
	if (row-1>=0 && col+1<im->info->nc && im->data[row-1][col+1] == OBJECT) 
		mark (im, row-1,col+1, MARK);
	if (col-1>=0 && im->data[row][col-1] == OBJECT) 
		mark (im, row,col-1, MARK);
	if (col+1<im->info->nc && im->data[row][col+1] == OBJECT) 
		mark (im, row,col+1, MARK);
	if (row+1<im->info->nr && col-1>=0 && im->data[row+1][col-1] == OBJECT) 
		mark (im, row+1,col-1, MARK);
	if (row+1<im->info->nr && im->data[row+1][col] == OBJECT) 
		mark (im, row+1,col, MARK);
	if (row+1<im->info->nr && col+1<im->info->nc && im->data[row+1][col+1] == OBJECT) 
		mark (im, row+1,col+1, MARK);
}

void unmark (IMAGE im, int is, int js, int ie, int je, int val)
{
	int i,j;

	for (i=is; i<=ie; i++)
	  for (j=js; j<=je; j++)
	    if (im->data[i][j] == val)
		im->data[i][j] = BACK;
}

void bbox (IMAGE x,int I,int J, int val,int *ulr,
	int *ulc,int *lrr,int *lrc)
{
	int i,j,ip1,jp1,ip2,jp2;
	int is, ie, js, je;
	 
	ip1 = 10000;    jp1 = 10000;
	ip2 = -1;       jp2 = -1;
 
	if (I-SIZE < 0) is = 0;
	  else is = I-SIZE;
	if (I+SIZE>=x->info->nr) ie = x->info->nr-1;
	  else ie = I+SIZE;
	if (J-SIZE < 0) js = 0;
	  else js = J-SIZE;
	if (J+SIZE >= x->info->nc) je = x->info->nc-1;
	  else je = J+SIZE;

 /* Find the min and max coordinates, both row and column */
	 for (i=is; i<=ie; i++)
	   for(j=js; j<=je; j++)
	     if (x->data[i][j] == val) 
	     {
	       if (i < ip1) ip1 = i;
	       if (i > ip2) ip2 = i;
	       if (j < jp1) jp1 = j;
	       if (j > jp2) jp2 = j;
	     }

	 if (jp2 < 0) 
		 return;
 
	*ulr = ip1; *ulc = jp1;
	*lrr = ip2; *lrc = jp2;
 }

