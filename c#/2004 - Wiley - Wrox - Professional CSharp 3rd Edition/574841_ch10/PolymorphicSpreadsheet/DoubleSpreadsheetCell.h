#include "SpreadsheetCell.h"

class DoubleSpreadsheetCell : public SpreadsheetCell
{
 public:
  DoubleSpreadsheetCell ();
  virtual void set(double inDouble);
  virtual void set(std::string inString);
    
  virtual std::string getString() const;

 protected:
  static std::string doubleToString(double inValue); 
  static double stringToDouble(std::string inValue);

  double mValue;
};
