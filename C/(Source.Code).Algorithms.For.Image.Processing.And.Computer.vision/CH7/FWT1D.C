#include <math.h>
#include <stdio.h>
#include <malloc.h>

#define FORWARD 1
#define INVERSE -1

static float rec[16] = {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};
static float data[16] = { 0,1,2,2,4,4,2,2,0,0,2,2,0,0,0,0};  

void fwt_1d  (float *data, int N, int direction);
void wavelet (float *data, int N, int direction);
void Haar_2  (float *data, int N, int direction);

int main()
{
	int i;

	printf ("Computing a wavelet transform on a Haar basis:\n");
	fwt_1d (data, 16, FORWARD);

	printf ("Reconstructing the data from the wavelets:\n");
	for (i=0; i<16; i++)
	  rec[i] = data[i];
	fwt_1d (rec, 16, INVERSE);

	printf ("I    WT      Data\n");
	for (i=0; i<16; i++)
	  printf ("%d: %7.4f  %7.4f\n", i, data[i], rec[i]);
	return 0;
}

void fwt_1d (float *data, int N, int direction)
{
	long thisn;

	if (N > 2)
	{
	  if (direction >= 0)
	  {
	    thisn = N;
	    while (thisn >= 2)
	    {
	      wavelet (data, thisn, direction);
	      thisn /= 2;
	    }
	  } else if (direction == INVERSE) 
	  {
	    thisn = 2;
	    while (thisn <= N)
	    {
	      wavelet (data, thisn, direction);
	      thisn *= 2;
	    }
	  }
	}
}

void wavelet (float *data, int N, int direction)
{
	Haar_2 (data, N, direction);
}

void Haar_2 (float *data, int N, int direction)
{
	int i,j,nover2;
	float h0, h1, *tmp;

/*      h0 = 1.0/sqrt(2.0); h1 = 1.0/sqrt(2.0); */
	h0 = h1 = 0.5;
	tmp = (float *)malloc (sizeof(float)*N);

	nover2 = N/2;
	if (direction == FORWARD) 
	{
	  i=0;
	  for (j=0; j<N-1; j+=2)
	  {
	    tmp[i]        = h0*data[j] + h1*data[j+1];
	    tmp[i+nover2] = h0*data[j] - h1*data[j+1];
	    i++;
	  }
	} else if (direction == INVERSE) 
	{

/* Comment the next line out for symmetric scaling */
	  h1 = h0 = 1.0;
	  i = j = 0;
	  do 
	  {
	    tmp[j]   = h0*data[i] + h1*data[i+nover2];
	    tmp[j+1] = h0*data[i] - h1*data[i+nover2];
	    j += 2; i++;
	  } while (i <= nover2);
	}

	for (i=0; i<N; i++)
	  data[i] = tmp[i];
	free(tmp);
}

