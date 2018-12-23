/*	User-defined functions  for the Travelling Salesman Problem	*/

#include <math.h>
#define MAXARGS 100

extern int nargs;
float cityx[MAXARGS], cityy[MAXARGS];

void user_init ()
{
	int i;

	printf ("This GA solves the Travelling Salesman Problem. The\n");
	printf ("of arguments is the number of cities.\n");

	printf ("Now enter the (x,y) coordinates of each city.\n");
	for (i=0; i<nargs; i++)
	{
	   printf ("Coordinates for city #%d: ", i);
	   scanf ("%f", &(cityx[i])); printf ("%f ", cityx[i]);
	   scanf ("%f", &(cityy[i])); printf ("%f\n", cityy[i]);
	}
}

double distance (int a, int b)
{
	float x1, y1, x2, y2;

	x1 = cityx[a]; y1 = cityy[a];
	x2 = cityx[b]; y2 = cityy[b];
	return sqrt( (double)( (x1-x2)*(x1-x2) + (y1-y2)*(y1-y2) ) );
}

/* Evaluate chomosome N and store result in EVALS array */
double ieval (unsigned char *b, int n)
{
	int route[MAXARGS];
	double total_distance;
	int i,j,k, visited[MAXARGS];

/* Extract the route from this chromosome */
	for (i=0; i<nargs; i++)
	{
	  route[i] = getarg (i, b);
	  visited[i] = 0;
	}

/* Compute the distance for this route */
	total_distance = 0.0;
	for (i=0; i<nargs-1; i++)
	{
	  j = route[i]; k = route[i+1];
	  total_distance += distance (j, k);
	  visited[j] = 1; visited[k] = 1;
	}
	total_distance += distance (route[0], route[nargs-1]);
	visited[route[nargs-1]] = 1;

/* Compute penalties for cities not on the route. */
	for (i=0; i<nargs; i++)
	  if (visited[i] == 0) total_distance += 1000.0;

	return  total_distance;
}

