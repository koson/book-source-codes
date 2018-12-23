#include "PointerBool.h"
#include "SpreadsheetCell.h"

#include <iostream>
using namespace std;

int main(int argc, char** argv)
{
  Pointer<SpreadsheetCell> smartCell(new SpreadsheetCell);
  smartCell->set(5);
  if (smartCell != NULL) {
    cout << "not NULL!\n";
  }
  if (smartCell) {
    cout << "not NULL!\n";
  }
  if (!smartCell) {
    cout << "NULL\n";
  }

  return (0);
} 
