#include "SpreadsheetCell.h"
#include <iostream>
using namespace std;

int main(int argc, char** argv)
{
  SpreadsheetCell myCell;
  myCell.setValue(6);

  cout << "cell 1: " << myCell.getValue() << endl;

  // Note the function-call syntax
  SpreadsheetCell* myCellp = new SpreadsheetCell();
      
  return (0);
}
