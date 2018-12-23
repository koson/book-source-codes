// SpreadsheetCell.h

#ifndef __SPREADSHEET_H__
#define __SPREADSHEET_H__

#include <string>

class SpreadsheetCell
{
 public:
  SpreadsheetCell();
  virtual ~SpreadsheetCell();

  virtual void set(std::string inString) = 0;

  virtual std::string getString() const = 0;
};

#endif
