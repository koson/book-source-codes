#include "StringSpreadsheetCell.h"

using namespace std;

StringSpreadsheetCell::StringSpreadsheetCell() : mValue("#NOVALUE")
{
}

void StringSpreadsheetCell::set(string inString)
{
  mValue = inString;
}

string StringSpreadsheetCell::getString() const
{
  return mValue;
}
