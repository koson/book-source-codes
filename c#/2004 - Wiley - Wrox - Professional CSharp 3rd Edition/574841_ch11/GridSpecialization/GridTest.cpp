#include <iostream>
using namespace std;

#include "GridString.h"

int main(int argc, char** argv)
{
  Grid<int> myIntGrid; // Uses original Grid template
  Grid<char*> stringGrid1(2, 2); // Uses char* specialization

  char* dummy = new char[10];

  strcpy(dummy, "dummy");
  stringGrid1.setElementAt(0, 0, "hello");
  stringGrid1.setElementAt(0, 1, dummy);
  stringGrid1.setElementAt(1, 0, dummy);
  stringGrid1.setElementAt(1, 1, "there");

  delete [] dummy;

  Grid<char*> stringGrid2(stringGrid1);

  return (0);
}
