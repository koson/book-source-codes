/* stringarray.h -- Definitions for string arrays.
 */

#include <stdio.h>

#define LINESIZE 65

typedef char linetype[LINESIZE];

int getline(FILE * fd, char buff[], int nmax);
int getStringArray(char *filename, linetype table[], int nmax);
void writeStringArray(char *filename, linetype table[], int n);
void stringBubble(linetype table[], int n);
int stringBinary(linetype table[], int n, char who[]);
