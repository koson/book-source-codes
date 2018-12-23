#include <iostream>
#include <sstream>

#include "DoubleSpreadsheetCell.h"

using namespace std;

DoubleSpreadsheetCell::DoubleSpreadsheetCell() : mValue(-1)
{ 
}

void DoubleSpreadsheetCell::set(double inDouble)
{
  mValue = inDouble;
}

void DoubleSpreadsheetCell::set(string inString)
{
  mValue = stringToDouble(inString);
}

string DoubleSpreadsheetCell::getString() const
{
  return doubleToString(mValue);
}

string DoubleSpreadsheetCell::doubleToString(double inValue)
{
  ostringstream ostr;

  ostr << inValue;
  return (ostr.str());
}

double DoubleSpreadsheetCell::stringToDouble(string inString)
{
  double temp;

  istringstream istr(inString);

  istr >> temp;
  if (istr.fail() || !istr.eof()) {
    return (0);
  }
  return (temp);
}
