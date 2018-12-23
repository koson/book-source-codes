/* ================================================================
	 Backpropagation network for character recognition  
		       Classify Using A Net
   The set of  data  applied  to the inputs  will  be classified 
   according to the weights read in from the file 'weights.dat'.
   
   ================================================================ */

/* TRIAL : Test data is 6x8 element feature vectors from recognizer */

#include <stdio.h>
#include <math.h>
#include <malloc.h>
#include <stdlib.h>
#include <sys/types.h>
#define MAX
#include "lib.h"

int NO_OF_INPUTS = 48;
int NO_OF_HIDDEN = 0;
int NO_OF_OUTPUTS = 0;

float *inputs;                           /* Input values in the first layer */

float **hweights;               /* Weights for hidden layer. HWEIGHTS[i][j] */
				/* is the weight for hidden node i from     */
				/* input node j.                            */
float *hidden;                  /* Outputs from the hidden layer            */
float *vhidden;
float *err_hidden;                                /* Errors in hidden nodes */

float **oweights;               /* Weights for the output layer, as before  */
float *outputs;                                            /* Final outputs */
float *voutputs;
float *err_out;                                   /* Errors in output nodes */

float *should;                  /* Correct output vector for training datum */
float learning_rate = 0.9;

FILE *training_data=0;                           /* File with training data */
FILE *test_data=0;                               /* File with unknown data  */
FILE *infile;                                 /* One of the two files above */
/* int actual;                           Actual digit - tells us the output */

time_t start, end;
long etime;

void get_weights();
void compute_hidden (int node);
void compute_output (int node);
float output_function (float x);
void compute_all_hidden ();
void compute_all_outputs ();
float weight_init (void);
void initialize_all_weights ();
float compute_output_error ();
float compute_hidden_node_error (int node);
void compute_hidden_error ();
void update_output ();
void update_hidden (void);
float * fvector (int n);
float **fmatrix (int n, int m);
void setup (void);
void get_params (int *ni, int *nh, int *no);
int get_inputs (float *x);

int dset = 1;

void get_weights ()
{
	FILE *f;
	int i,j,k;

	f=fopen ("weights.dat", "r");
	if (f==NULL)
	{
		printf ("NO WEIGHTS FILE: weights.dat\n");
		exit(1);
	}

	fscanf (f, "%d", &NO_OF_INPUTS);
	fscanf (f, "%d", &NO_OF_HIDDEN);
	fscanf (f, "%d", &NO_OF_OUTPUTS);
	setup();

	for (i=0; i<NO_OF_INPUTS; i++)
	  for (j=0; j<NO_OF_HIDDEN; j++)
	    fscanf (f, "%f", &(hweights[j][i]));

	for (i=0; i<NO_OF_HIDDEN; i++)
	  for (j=0; j<NO_OF_OUTPUTS; j++)
	       fscanf (f, "%f", &(oweights[j][i]));
}

/*      Compute the output from hidden node NODE        */
void compute_hidden (int node)
{
	int i = 0;
	float x = 0;

	for (i=0; i<NO_OF_INPUTS; i++)
	  x += inputs[i]*hweights[node][i];
	hidden[node] = output_function (x);
	vhidden[node] = x;
}

/*      Compute the output from output node NODE        */
void compute_output (int node)
{
	int i = 0;
	float x = 0;

	for (i=0; i<NO_OF_HIDDEN; i++)
	  x += hidden[i]*oweights[node][i];
	outputs[node] = output_function (x);
	voutputs[node] = x;
}

/*      Output function for hidden node - linear or sigmoid     */
float output_function (float x)
{
	return 1.0/(1.0 + exp((double)(-x)));

/*        return x;                Linear */
}

/*      Derivative of the output function       */
float of_derivative (float x)
{
	float a = 0.0;

	a = output_function(x);
	
	return 1.0;                                               /* Linear */
/*      return a*(1.0-a);               */
}


/*      Compute all hidden nodes        */
void compute_all_hidden ()
{
	int i = 0;

	for (i=0; i<NO_OF_HIDDEN; i++)
	  compute_hidden (i);
}

/*      Compute all hidden nodes        */
void compute_all_outputs ()
{
	int i = 0;

	for (i=0; i<NO_OF_OUTPUTS; i++)
	  compute_output (i);
}

/*      Initialize a weight     */
float weight_init (void)
{
	return (float)(drand32() - 0.5);
}

/*      Calculate the error in the output nodes 
float compute_output_error ()
{
	int i = 0;
	int x = 0;

	for (i=0; i<NO_OF_OUTPUTS; i++)
	{
	  err_out[i] = (should[i]-outputs[i]) * of_derivative(voutputs[i]);
	  x += err_out[i];
	}
	return x;
}

/*      Compute the error term for the given hidden node       
float compute_hidden_node_error (int node)
{
	int i = 0;
	float x = 0.0;

	for (i=0; i<NO_OF_OUTPUTS; i++)
	  x += err_out[i]*oweights[i][node];
	return of_derivative(vhidden[node]) * x;
}

/*      Compute all hidden node error terms     
void compute_hidden_error ()
{
	int i = 0;

	for (i=0; i<NO_OF_HIDDEN; i++)
	  err_hidden[i] = compute_hidden_node_error(i);
}

/*      Update the output layer weights 
void update_output ()
{
	int i=0, j=0;

	for (i=0; i<NO_OF_OUTPUTS; i++)
	  for (j=0; j<NO_OF_HIDDEN; j++)
	    oweights[i][j] += learning_rate*err_out[i]*hidden[j];
}

/*      Update the hidden layer weights         
void update_hidden (void)
{
	int i=0, j=0;

	for (i=0; i<NO_OF_HIDDEN; i++)
	  for (j=0; j<NO_OF_INPUTS; j++)
	    hweights[i][j] += learning_rate*err_hidden[i]*inputs[j];
}

float compute_error_term ()
{
	int i = 0;
	float x = 0.0;

	for (i=0; i<NO_OF_OUTPUTS; i++)
	  x += (err_out[i]*err_out[i]);
	return x/2.0;
}                                         */

float * fvector (int n)
{
	return (float *)malloc(sizeof(float)*n);
}

float **fmatrix (int n, int m)
{
	int i = 0;
	float *x, **y;

/* Allocate rows */
	y = (float **)malloc (sizeof(float)*n);

/* Allocate NxM array of floats */
	x = (float *)malloc (sizeof(float)*n*m);

/* Set pointers in y to each row in x */
	for (i=0; i<n; i++)
	  y[i] = &(x[i*m]);

	return y;
}

/*      Allocate all arrays and matrices        */
void setup (void)
{
	
	inputs     = fvector (NO_OF_INPUTS);

	hweights   = fmatrix (NO_OF_HIDDEN, NO_OF_INPUTS);
	hidden     = fvector (NO_OF_HIDDEN);
	vhidden    = fvector (NO_OF_HIDDEN);
	err_hidden = fvector (NO_OF_HIDDEN);

	oweights   = fmatrix (NO_OF_OUTPUTS, NO_OF_HIDDEN);
	outputs    = fvector (NO_OF_OUTPUTS);
	voutputs   = fvector (NO_OF_OUTPUTS);
	err_out    = fvector (NO_OF_OUTPUTS);

	should     = fvector (NO_OF_OUTPUTS);
}

void get_params (int *ni, int *nh, int *no)
{
	printf ("How many input nodes: ");
	scanf ("%d", ni);
	printf ("How many hidden nodes: ");
	scanf ("%d", nh);
	printf ("How many output nodes: ");
	scanf ("%d", no);
}

int get_inputs (float *x)
{
	float z;
	int i, k;

	for (i=0; i<NO_OF_INPUTS; i++)
	{
	  k = fscanf (infile, "%f", &(x[i]));
	  if (k<1) return 0;
	  x[i]/= 10.0;
	}
/*        fscanf (infile, "%d", &actual);       */

	return 1;
}

void print_outputs ()
{
	int i, j;

	j = 0;
	for (i=0; i<NO_OF_OUTPUTS; i++)
	{
	  printf ("%6.3f ", outputs[i]);
	  if (outputs[i] > outputs[j]) j = i;
	}
	printf ("%d %d \n", dset-1, j);
	printf ("  NN classified the input digit as %d\n", j);
}

int main(int argc, char *argv[])
{
	int k = 0;
	float x = 0.0;

/* Look for data files */
	if (argc < 2)
	{
		printf ("Neural net system: Classification\n");
		printf ("bpn <data set>\n");
		exit(1);
	}

/* Get the size of the net */
	get_weights(); 

/* Train */
	test_data = fopen (argv[1], "r");
	if (test_data == NULL)
	{
	  printf ("Can't open data '%s'\n", argv[1]);
	  exit(3);
	}
	infile = test_data;

/* Now apply the NN to the inputs */
	dset = 1;
	k = get_inputs (inputs);
	while (k)
	{
	  compute_all_hidden();
	  compute_all_outputs();
	  print_outputs();
	  k = get_inputs (inputs);
	  dset++;
	}
	fclose (test_data);
	return 0;
}
