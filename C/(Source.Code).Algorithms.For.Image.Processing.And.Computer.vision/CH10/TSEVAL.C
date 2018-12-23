/* Evaluation function for the Travelling Salesman Problem */

typedef  unsigned char * bits;
extern double *evals;
extern bits *population;
extern int nargs;

/* Distance between two cities */
float distance (int a, int b)
{
	float x1, y1, x2, y2;

	x1 = cityx[a]; y1 = cityy[a];
	x2 = cityx[b]; y2 = cityy[b];
	return (float)sqrt( (double)( (x1-x2)*(x1-x2) + (y1-y2)*(y1-y2) ) );
}

/* Evaluate chomosome N and store result in EVALS array */
void ceval (int n)
{
	int *route;
	float total_distance;
	int i,j,k, *visited;

/* Initialize */
	route = (int *)malloc(sizeof(int)*nargs);
	visited = (int *)malloc (sizeof(int)*nargs);

/* Extract the route from this chromosome */
	for (i=0; i<nargs; i++)
	{
	  route[i] = getarg (i, population[n]);
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

	free(route) free (visited);
	evals[n] = total_distance;
}

