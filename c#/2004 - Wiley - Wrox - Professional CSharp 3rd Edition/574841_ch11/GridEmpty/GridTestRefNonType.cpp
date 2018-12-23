#include "GridRefNonType.h"
#include "SpreadsheetCell.h"

extern const int emptyInt = 0;
extern const SpreadsheetCell emptyCell(0);

int main(int argc, char **argv)
{
  // The following line causes an Internal Compiler Error in g++ 3.2.2
  // on Linux.
  Grid<int, emptyInt> myIntGrid;
  Grid<SpreadsheetCell, emptyCell> mySpreadsheet;

  Grid<int, emptyInt> myIntGrid2(myIntGrid);

  return (0);
}
