/* --------------------------------------------------------------------
		Image restoration system module 2:
		Change size to a sqaure power of 2:
			128x128
			256x256
			512x512

J. Parker 95-08-01
   -------------------------------------------------------------------- */

#define MAX
#include "lib.h"

int NR, NC;
char filename[128];

void add_rc (IMAGE a, IMAGE b, int n, int nr, int nc);
void del_rc (IMAGE a, IMAGE b, int n, int nr, int nc);
int yesno (void);

int main (int argc, char *argv[])
{
	FILE *f;
	int colour = 0, n=0, shrink=0;
	IMAGE im1, p;

	printf ("Image Restoration System module 2 - Embed and align\n");
	if (argc < 2)
	{
		printf ("Usage: %s <input image> {size}\n", argv[0]);
		exit(1);
	}

	im1 = Input_PBM (argv[1]);
	if (im1 == 0)
	{
	  printf ("No such image file: '%s'\n", argv[1]);
	  exit(1);
	}
	NR = im1->info->nr;     NC = im1->info->nc;
	strcpy (filename, argv[1]);

/* Determine the size of the new image */
	if (argc < 3) 
	{
	  if (NR > NC) n = NR;
	    else n = NC;
	  if (n>512)
	  {
		n = 512;
		shrink = 1;
	  } else if (n > 256) n = 512;
	    else if (n > 128) n = 256;
	    else n = 128;
	} else
	{
		sscanf (argv[2], "%d", &n);
		printf ("Size is to be %dx%d\n", n,n);
	}

/* Allocate a new image */
	p = newimage (n, n);

/* Copy the old image into the new frame */
	do {
	  if (shrink)
	    del_rc (im1, p, n, NR, NC);
	  else
	    add_rc (im1, p, n, NR, NC);

/* Write the new array into an image file */
	  if (argc > 1)
	  {
	    Output_PBM (p, "align.pgm");
	    printf ("Created 'align.pgm'\n");
	    exit (0);
	  }

	  if (colour == 0)
	  {
	    Output_PBM (p, "input.pgm");
	    printf ("Image has been converted. %s\n", filename);
	    exit(0);
	  }

	  if (filename[5] == '1')
	  {
	    filename[5] = '2';
	    Output_PBM (p, "input1.pgm");
	    im1 = Input_PBM (filename);              /* Read the file */
	  }
	  else if (filename[5] == '2')
	  {
	    Output_PBM (p, "input2.pgm");
	    filename[5] = '3';
	    im1 = Input_PBM (filename);              /* Read the file */
	  }
	  else {
	    Output_PBM (p, "input3.pgm");
	    printf ("COLOUR files have been converted.\n");
	    exit(0);
	  }
	} while (colour);
}

/*      Insert the image 'a' into 'b', centred.         */
void add_rc (IMAGE a, IMAGE b, int n, int nr, int nc)
{
	int i,j,k,mr,mc;

/* Zero the image b */
	for (i=0; i<n; i++)
	  for (j=0; j<n; j++)
	    b->data[i][j] = 0;

/* Compute the offset - b is bigger than a */
	mr = (n-nr)/2; mc = (n-nc)/2;
	
/* Copy a into b */
	for (i=mr; i<mr+nr; i++)
	  for (j=mc; j<mc+nc; j++)
		b->data[i][j] = a->data[i-mr][j-mc];
}

void del_rc (IMAGE a, IMAGE b, int n, int nr, int nc)
{
	int i,j,k;
	static int si = -1, sj = -1;
	char cmd[128];

 /* Zero the image b */
	for (i=0; i<n; i++)
	  for (j=0; j<n; j++)
	    b->data[i][j] = 0;
 
	if (si < 0)
	do 
	{
	  printf ("Must extract a sub-image. Please enter the row and column\n");
	  printf ("index of the upper left pixel of the sub-image.\n");
	  Display (a);
	  scanf ("%d", &si); scanf ("%d", &sj);
	  j = nr - (512+si);
	  k = nc - (512+sj);
	  if (j<0) printf ("Leaves %d rows to be filled. ", -j);
	  if (k<0) printf ("Leaves %d columns to be filled.", -k);
	  printf ("\n");

	  printf ("Is this OK? ");
	  k = yesno();
	} while (k == 0);
	
	for (i=si; i<si+512; i++)
	  for (j=sj; j<sj+512; j++)
	    if (i>=0 && i<nr && j>=0 && j<nc &&
		(i-si)>=0 && (i-si)<512 && (j-sj)>=0 && (j-sj)<512) 
	      b->data[i-si][j-sj] = a->data[i][j];
}

int yesno (void)
{
	char str[32];

	do
	{
		printf (" ('y' or 'n') ");
		scanf ("%s", &str);
	} while (str[0] != 'y' && str[0] != 'n');
	return (str[0] == 'y');
}

