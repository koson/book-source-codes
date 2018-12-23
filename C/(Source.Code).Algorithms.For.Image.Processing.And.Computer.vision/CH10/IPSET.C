/*	User-defined functions  for the Integer Programming Problem */

#include <math.h>
extern int nargs;

void user_init ()
{
	int i;

	printf ("This GA solves the Integer Programming Problem :\n");
	printf ("   1600x1 + 3200x2 + 2300x3 = MIN\n");

}

/* Evaluate chomosome N and store result in EVALS array */
double ieval (unsigned char *b, int n)
{
	float total_distance;
	int i,j,k, M, l;
	int x1, x2, x3;

/* Extract the parameters */
	x1 = getarg(0, b);
	x3 = getarg(1, b);
	x2 = 2000 - 6*x3 -3*x1;

	if (x2 < 0) l = abs(x2) * 10000;
	 else l = 0;
	M = 1600*x1 + 3200*x2 + 2300*x3;

/* Compute penalties for violating the constraints */
/* Constraint 2: */
	k = 2*x1 + 5*x2 + x3;
	if (k < 1000) k = (1000 - k) * 3000;
	else k = 0;

/* Constraint 3: */
	j = x1 + 2*x2 + 4*x3;
	if (j > 3000) j = (j-3000) * 3000;
	else j = 0;

	return (double)( M + k + j + l );
}

