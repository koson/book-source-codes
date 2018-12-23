// SpreadsheetCell.cpp
#include "SpreadsheetCell.h"

void SpreadsheetCell::setValue(double inValue)
{
  mValue = inValue;
}

double SpreadsheetCell::getValue()
{
  return (mValue);
}
