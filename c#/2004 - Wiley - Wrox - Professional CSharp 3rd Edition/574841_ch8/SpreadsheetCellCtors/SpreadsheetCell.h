#include <string>
using std::string;

class SpreadsheetCell
{
 public:
  SpreadsheetCell(double initialValue);
  SpreadsheetCell(string initialValue);
  void setValue(double inValue);
  double getValue();
  void setString(string inString);
  string getString();

 protected:
  string doubleToString(double inValue);
  double stringToDouble(string inString);

  double mValue;
  string mString;
};
