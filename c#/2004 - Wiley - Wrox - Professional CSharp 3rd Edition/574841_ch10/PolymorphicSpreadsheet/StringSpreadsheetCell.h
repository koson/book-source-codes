#include "SpreadsheetCell.h"

class StringSpreadsheetCell : public SpreadsheetCell
{
 public:
  StringSpreadsheetCell();

  virtual void set(std::string inString);
    
  virtual std::string getString() const;

 protected:
  std::string mValue;
};
