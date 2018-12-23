/* Implement a single bit mutation */

void fmut (float *b, int n, float xmax, float xmin, int size)
{
	int i,j;
	double m, v, d3;

	v = (double)(xmax-xmin)/(double)(1<<size);
	m = (double)(1<<(size-n-1));
	if ( (int)((*b-xmin)/(v*m)) % 2 )
		 d3 = *b - v*m;
	else 
		 d3 = *b + v*m;
	*b = d3;
}

/* Perform a 1 point crossover */
void fcross1 (float *s1, float *s2, int n, float xmin, float xmax, int size)
{
	int i,k,c1;
	double xl,xh,yl,yh;
	double v,j;

	v = (double)(xmax-xmin)/(double)(1<<size);
	j = (double)(1 << (size-n));
	xh = (int)((*s1-xmin)/(j*v)) * (j*v);
	xl = (*s1) - xh;
	yh = (int)((*s2-xmin)/(j*v)) * (j*v);
	yl = (*s2) - yh;
	xh = xh + yl ;
	yh = yh + xl ;
	*s1 = (float)xh;
	*s2 = (float)yh;
}

