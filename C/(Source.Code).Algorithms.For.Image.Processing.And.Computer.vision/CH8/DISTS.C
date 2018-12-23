/* Compute distances between a set of feature vectors */
#include <math.h>

float v[8][8] = {0.3815, 0.0, 0.1439, 0.0, 0.3307, 0.0, 0.1439, 0.0,
		 0.2733, 0.0, 0.26,   0.0, 0.2050, 0.0, 0.2609, 0.0, 
		 0.3409, 0.0, 0.1875, 0.0, 0.2841, 0.0, 0.1875, 0.0,
		 0.3118, 0.0, 0.2004, 0.0, 0.2740, 0.0, 0.2138, 0.0,
		 0.3714, 0.0, 0.1751, 0.0, 0.2785, 0.0, 0.1751, 0.0};
char *labels[5] = {"B", "C", "D1", "D2", "D3"};

float dist (float *a, float *b)
{
	int i, j, k;
	float sum = 0.0;

	for (i=0; i<8; i++)
	  sum += (a[i]-b[i])*(a[i]-b[i]);
	return (float)sqrt((double)sum);
}

main ()
{
	float dm[5][5], v1[8], v2[8], v3[8], v4[8], v5[8];
	int i,j,k;

	for (i=0; i<5; i++)
	  for (j=0; j<5; j++)
	    dm[i][j] = dm[j][i] = dist(v[i], v[j]);

	printf ("         B          C         D1           D2          D3\n");
	for (i=0; i<5; i++)
	{
	  printf ("%2s: ", labels[i]);
	  for (j=0; j<5; j++)
	    printf ("%10.5f ", dm[i][j]);
	  printf ("\n");
	}
}
