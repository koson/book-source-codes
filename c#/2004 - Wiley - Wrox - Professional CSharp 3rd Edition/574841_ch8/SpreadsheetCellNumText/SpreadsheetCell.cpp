#include "SpreadsheetCell.h"

#include <iostream>
#include <sstream>
using namespace std;

void SpreadsheetCell::setValue(double inValue)
{
  mValue = inValue;
  mString = doubleToString(mValue);
}

double SpreadsheetCell::getValue()
{
  return (mValue);
}

void SpreadsheetCell::setString(string inString)
{
  mString = inString;
  mValue = stringToDouble(mString);
}

string SpreadsheetCell::getString()
{
  return (mString);
}

string SpreadsheetCell::doubleToString(double inValue)
{
  ostringstream ostr;

  ostr << inValue;
  return (ostr.str());
}

double SpreadsheetCell::stringToDouble(string inString)
{
  double temp;

  istringstream istr(inString);

  istr >> temp;
  if (istr.fail() || !istr.eof()) {
    return (0);
  }
  return (temp);
}
