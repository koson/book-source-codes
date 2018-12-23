/* Minimizing fuzziness */

#define MAX
#include "lib.h"

#define ENTROPY 1
#define YAGER 2
void thr_fuz (IMAGE im);
void histogram (IMAGE im, double *hist);
double Ux (int g, int u0, int u1, int t);
double Shannon (double x);
double fuzzy (double *hist, int u0, int u11, int t);
double Yager (int u0, int u1, int t);

int method = YAGER;

void main (int argc, char *argv[])
{
	IMAGE data;

	if (argc < 4)
	{
	  printf ("Usage: thrfuz <input file> <output file> <method>\n");
	  printf (" where <method> is either ENTROPY or YAGER.\n");
	  exit (0);
	}
	if (strcmp(argv[3], "ENTROPY")==0) method = ENTROPY;
	else if (strcmp(argv[3], "YAGER")==0) method = YAGER;
	else {
	  fprintf (stderr, "Bad method requested: '%s'\n", argv[3]);
	  exit(1);
	}

	data = Input_PBM (argv[1]);
	if (data == NULL)
	{
		printf ("Bad input file '%s'\n", argv[1]);
		exit(1);
	}

	thr_fuz (data);

	Output_PBM (data, argv[2]);
}

void thr_fuz (IMAGE im)
{
	double *S, *Sbar, *W, *Wbar;
	double *hist, *F, MN, maxv=0.0, delta;
	int i,j,k, t, kk, tbest= -1, u0, u1, sum, minsum;
	int start, end;

	S = (double *)malloc(sizeof(double)*256);
	Sbar = (double *)malloc(sizeof(double)*256);
	W = (double *)malloc(sizeof(double)*256);
	Wbar = (double *)malloc(sizeof(double)*256);
	hist = (double *)malloc(sizeof(double)*256);
	F = (double *)malloc(sizeof(double)*256);

/* Find the histogram */
	histogram (im, hist);
	MN = (double)(im->info->nr)*(double)(im->info->nc);

/* Find cumulative histogram */
	S[0] = hist[0]; W[0] = 0;
	for (i=1; i<256; i++)
	{
	  S[i] = S[i-1] + hist[i];
	  W[i] = i*hist[i] + W[i-1];
	}

/* Cumulative reverse histogram */
	Sbar[255] = 0; Wbar[255] = 0;
	for (i=254; i>= 0; i--)
	{
	  Sbar[i] = Sbar[i+1] + hist[i+1];
	  Wbar[i] = Wbar[i+1] + (i+1)*hist[i+1];
	}

	for (t=1; t<255; t++)
	{
	  if (hist[t] == 0.0) continue;
	  if (S[t] == 0.0) continue;
	  if (Sbar[t] == 0.0) continue;

/* Means */
	  u0 = (int)(W[t]/S[t] + 0.5);
	  u1 = (int)(Wbar[t]/Sbar[t] + 0.5);

/* Fuzziness measure */
	  F[t] = fuzzy (hist, u0, u1, t)/MN;

/* Keep the minimum fuzziness */
	  if (F[t] > maxv) maxv = F[t];
	  if (tbest < 0) tbest = t;
	  else if (F[t] < F[tbest]) tbest = t;
	}

/* Find best out of a range of thresholds */
	delta = F[tbest] + (maxv-F[tbest])*0.05;        /* 5% */
	start = (int)(tbest - delta);
	if (start <= 0) start = 1;
	end   = (int)(tbest + delta);
	if (end>=255) end = 254;
	minsum = 1000000;

	for (i=start; i<=end; i++)
	{
	  sum = hist[i-1] + hist[i] + hist[i+1];
	  if (sum < minsum)
	  {
		t = i;
		minsum = sum;
	  }
	}
	fprintf (stderr,"Threshold is %d\n", t);

/* Threshold */
	for (i=0; i<im->info->nr; i++)
	  for (j=0; j<im->info->nc; j++)
	    if (im->data[i][j] < t)
	      im->data[i][j] = 0;
	    else
	      im->data[i][j] = 255;
	
	free(S); free(Sbar);
	free(W); free(Wbar);
	free(hist); free(F);
}

void histogram (IMAGE im, double *hist)
{
	int j, i;

	for (i=0; i<256; i++) hist[i] = 0.0;
	for (i=0; i<im->info->nr; i++)
	  for (j=0; j<im->info->nc; j++)
	    hist[(im->data[i][j])] += 1.0;
}

double Ux (int g, int u0, int u1, int t)
{
	double ux, x;

	if (g <= t)
	{
	  x = 1.0 + ((double)abs(g - u0))/255.0;
	  ux = 1.0/x;
	} else {
	  x = 1.0 + ((double)abs(g - u1))/255.0;
	  ux = 1.0/x;
	}
	if (ux< 0.5 || ux>1.0)
	 printf ("Ux = %f\n", ux);
	return ux;
}

double fuzzy (double *hist, int u0, int u1, int t)
{
	int i;
	double E=0;

	if (method == ENTROPY)
	{
	  for (i=0; i<255; i++)
	  {
	    E += Shannon (Ux(i,u0,u1, t))*hist[i];
	  }
	  return E;
	} else {
	  return Yager (u0, u1, t);
	}
}

double Shannon (double x)
{
	if (x > 0.0 && x < 1.0)
	  return (double)(-x*log((double)x) - (1.0-x)*log((double)(1.0-x)));
	else return 0.0;
}

double Yager (int u0, int u1, int t)
{
	int i;
	double x, sum=0.0;

	for (i=0; i<256; i++)
	{
	  x = Ux(i, u0, u1, t);
	  x = x*(1.0-x);
	  sum += x*x;
	}
	x = (double)sqrt((double)sum);
	return x;
}
