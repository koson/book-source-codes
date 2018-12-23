/* (Integer program) Compute x2 given x1 and x3 */
main()
{
	int i,j,k,a,b,c,x1,x2,x3;

	printf ("Enter x1: "); scanf ("%d", &x1);
	printf ("Enter x3: "); scanf ("%d", &x3);

	x2 = 2000 - 3*x1 - 6*x3;
	printf ("X1 = %d   X2 = %d   X3 = %d\n", x1, x2, x3);

	printf ("2x1 + 5x2 + x3 = %d >= 1000\n", 2*x1+5*x2+x3);
	printf ("x1 + 2x2 + 4x3 = %d <= 3000\n", x1+2*x2+4*x3);

	printf ("Objective value: %d\n", 1600*x1 + 3200*x2 + 2300*x3);
}
