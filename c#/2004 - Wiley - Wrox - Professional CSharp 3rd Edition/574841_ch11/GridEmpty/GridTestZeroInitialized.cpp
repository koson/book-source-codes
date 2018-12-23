#include "GridZeroInitialized.h"

#include <iostream>
using namespace std;

template <typename T>
void printGrid(const Grid<T>& g)
{
  for (int i = 0; i < g.getWidth(); i++) {
    for (int j = 0; j < g.getHeight(); j++) {
      cout << g.getElementAt(i, j) << " ";
    }
    cout << endl;
  }
  cout << endl;
}

int main(int argc, char** argv)
{
  Grid<int> myIntGrid;
  Grid<int> myIntGrid2;
  Grid<int> myIntGrid3(myIntGrid);

  printGrid(myIntGrid);
  printGrid(myIntGrid2);

  myIntGrid = myIntGrid3;

  myIntGrid.setElementAt(3, 4, 5);
  cout << myIntGrid.getElementAt(3, 4) << endl;

  //  SpreadsheetCell emptyCell;
  //  Grid<SpreadsheetCell, emptyCell> mySpreadsheet; // WILL NOT COMPILE

  return (0);
}
