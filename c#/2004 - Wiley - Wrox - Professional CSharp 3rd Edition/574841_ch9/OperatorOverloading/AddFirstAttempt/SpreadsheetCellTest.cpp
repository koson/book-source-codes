#include "SpreadsheetCell.h"

#include <iostream>
using namespace std;

int main(int argc, char** argv)
{
  SpreadsheetCell myCell(4), anotherCell(5);
  SpreadsheetCell aThirdCell = myCell.add(anotherCell);

  return (0);
}
