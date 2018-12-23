#include "SpreadsheetCell.h"

#include <iostream>
using namespace std;

int main(int argc, char** argv)
{
  SpreadsheetCell myCell;
  typedef double (SpreadsheetCell::*PtrToGet) () const;
  PtrToGet methodPtr = &SpreadsheetCell::getValue;
  cout << (myCell.*methodPtr)() << endl;

  return (0);
}
