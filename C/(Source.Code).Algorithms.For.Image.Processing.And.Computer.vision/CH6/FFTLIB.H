/* Frequency domain library - FFT routines and ancilliary */

#include "lib.h"

typedef float ** COMPLEX_IMAGE;
struct  Complex  {  /* Complex variable type */
    float real;
    float imag;
};

#if defined(MAX)
double  *sintabpt = (double *)0;      /* Points to sine table */
int  *bittabpt = (int *)0;         /* Points to bit table */
int  *powers = (int *)0;           /* Points to table of powers of 2 */
int  numpts;            /* Size of FFT */
int  nn;                /* 2^nn = numpts */
float direction;        /* Direction of FFT */
double  scalef = 0.0;    /* Scale factor, will = 1/sqrt(numpts) */
int FFTN=0;
int NORMALIZE = 0;
#else
extern double  *sintabpt;
extern int  *bittabpt;
extern int  *powers;
extern int  numpts;            /* Size of FFT */
extern int  nn;                /* 2^nn = numpts */
extern float direction;        /* Direction of FFT */
extern double  scalef;
extern int FFTN;
extern int NORMALIZE;
#endif

/*  Header File for Fast Fourier Transform library routine */
#define     FORWARD     -1.0    /* Perform forward FFT */
#define     REVERSE      1.0    /* Perform reverse FFT */

COMPLEX_IMAGE dupcomplex (COMPLEX_IMAGE im);
void freecomplex (COMPLEX_IMAGE x);
COMPLEX_IMAGE newcomplex (IMAGE im);
void image_fft (IMAGE image, COMPLEX_IMAGE *output);
void image_fftoc (IMAGE image, COMPLEX_IMAGE *output);
void fftx (IMAGE image, int oc, COMPLEX_IMAGE *output);
void image_fftinv (COMPLEX_IMAGE image, COMPLEX_IMAGE * output);
void normalize_set ();
 void normalize_clear ();
void image_fftinvoc (COMPLEX_IMAGE image, COMPLEX_IMAGE * output);
void fftinvx (COMPLEX_IMAGE image, int oc, COMPLEX_IMAGE *output);
void cprod (float c1r, float c1i, float *c2r, float *c2i);
float pix_cnorm (float cr, float ci);
void cdiv (float c1r, float c1i, float *c2r, float *c2i);
void fft2d ( COMPLEX_IMAGE image, float direction );
void filt_orig( COMPLEX_IMAGE array );
void filt_toint ( COMPLEX_IMAGE image , IMAGE output, float *h);
int vlog2 (int x);
void fft( float data[], float dir );
static int _fft( float tseries[], int level, int chunk );
void fftinit( int nopts );
static int bitrev( int bits );
void realtoint (COMPLEX_IMAGE fim, long *H);
void pairsort (float *arr, unsigned int *iarr, int n);
void fqsort (float *arr, unsigned int *iarr, int l, int r);

