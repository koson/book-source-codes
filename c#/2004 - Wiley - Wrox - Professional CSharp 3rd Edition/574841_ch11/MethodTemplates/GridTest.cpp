#include "Grid.h"

int main(int argc, char** argv)
{
  Grid<int> myIntGrid;
  Grid<double> myDoubleGrid;

  myDoubleGrid = myIntGrid; // DOES NOT COMPILE
  Grid<double> newDoubleGrid(myIntGrid); // DOES NOT COMPILE

  return (0);
}
