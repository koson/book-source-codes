#include "SpreadsheetCell.h"
#include <iostream>
using namespace std;

int main(int argc, char** argv)
{
  SpreadsheetCell myCell;
  string name = "heading one";

  myCell.setString(name); // copies name

  SpreadsheetCell myCell2(4);
  SpreadsheetCell anotherCell(myCell2);
  // anotherCell now has the values of myCell2

  return (0);
}
