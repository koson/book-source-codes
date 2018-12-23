#include "SpreadsheetCell.h"

#include <iostream>
using namespace std;

int main(int argc, char** argv)
{
  SpreadsheetCell cell1;
  string s1 = cell1; // Works as expected
  double d2 = cell1;

  //  double d1 = cell1 + 3.3; // DOES NOT COMPILE IF YOU DEFINE operator double()

  return (0);
}
