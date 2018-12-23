#include <math.h>

/*
float data[16] = {1,0,-3,2, 1,0,1,2};
float data[16] = {0,0,.5,.5,.5,.5,1.0, 2.0, 1.0, 0,0,0,0, 0, 0};
*/
float data[16] = {0,1,2,2,4,4,2,2,0,0,2,2,0,0,0,0};

float c[16][16];
float a[16][16];
float rec[16] = {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};

float phi (int j, int k, float x);
float ptwo (int p);
float haar (float x);
float recur (int level, float *data, int k);
/* Haar wavelets */

float haar (float x)
{
	float y;

	if ( (x>0) && (x<=0.5) ) return 1.0;
	if ( (x>=0.5) && (x<1.0) ) return -1.0;
	return 0.0;
}


float ptwo (int p)
{
	if (p == 0) return 1.0;
	if (p>0) return (float)(1<<p);
	if (p<0) return (1.0/(float)(1<<(-p)));
}

float phi (int j, int k, float x)
{
	return haar( ptwo(j)*(x+0.0001) - (float)k );
}

float recur (int level, float *data, int k)
{
	if (level==0)
	  return data[k] ;
	else
	  return (recur(level+1, data, 2*k)+recur(level+1, data, 2*k+1))/2.0;
}

float coef (int level, float *data, int k)
{
	return (recur(level+1, data, 2*k) - recur(level+1, data, 2*k+1))/2.0;
}

main()
{
	int i,j,k;
	float sum=0.0;

	for (j=1; j<16; j++)
	  for (k=1; k<16; k++)
	    a[j][k] = c[j][k] = 0.0;

	printf ("Computing a wavelet transform on a Haar basis:\n");
	for (i=0; i<16; i++)
	 a[0][i] = data[i];

	for (j=1; j<6; j++)
	{
	  printf ("J = -%d\n", j);
	  printf ("Cjk computed iteratively:\n");
	  for (k=0; k<16/(1<<j); k++)
	  {
	    c[j][k] = .5 * (a[j-1][2*k] - a[j-1][2*k+1]);
	    printf ("%5.3f ", c[j][k]);
	  }
	  printf ("\n");
	  printf ("Cjk computed recursively:\n");
	  for (k=0; k<16/(1<<j); k++)
	    printf ("%5.3f ", coef(-j, data, k));
	  printf ("\n");

	  printf ("Ajk computed iteratively:\n");
	  for (k=0; k<16/(1<<j); k++)
	  {
	    a[j][k] = .5 * (a[j-1][2*k] + a[j-1][2*k+1]);
	    printf ("%5.3f ", a[j][k]);
	  }
	  printf ("\n");

	  printf ("Computed recursively:\n");
	  for (k=0; k<16/(1<<j); k++)
	    printf ("%5.3f ", recur (-j, data, k));
	  printf ("\n");
	}

	printf ("\n\nC-5,0 is %f\n", coef(-5, data, 0));

	printf ("Reconstructing the data from the wavelets:\n");

	for (i=0; i<16; i++)
	{
	  printf ("Element %d\n", i);
	  sum = 0.0;
	  for (j=1; j<=5; j++)
	  {
	    for (k=0; k<16/(1<<j); k++)
	    {
	      sum += phi(-j,k,(float)i)*c[j][k];
printf ("PHI(%d,%d,%d) = %6.3f c[%d][%d] = %7.3f product %8.3f sum %8.3f\n",
		-j, k, i, phi(-j,k,(float)i), j, k, c[j][k],
		phi(-j,k,(float)i)*c[j][k], sum);
	    }
	  }
	  rec[i] = sum + a[4][0];
	  printf ("%d: %7.4f\n", i, rec[i]);
	}

	for (j=1; j<5; j++)
	{
	  for (k=0; k<16/(1<<j); k++)
	  {
	    printf ("Haar basis %d %d\n", -j, k);
	    for (i=0; i<16; i++)
	      printf ("%6.4f ", phi(-j, k, (float)i));
	    printf("\n");
	  }
	}

	for (i=0; i<16; i++)
	  printf ("%d: %7.4f\n", i, rec[i]);
}
