#include "SpreadsheetCell.h"
#include <iostream>
using namespace std;

int main(int argc, char** argv)
{
  SpreadsheetCell* myCellp = new SpreadsheetCell();

  (*myCellp).setValue(3.7);
  cout << "cell 1: " << (*myCellp).getValue() <<
    " " << (*myCellp).getString() << endl;
  delete myCellp;

  return (0);
}
