#include <string>
using std::string;

class SpreadsheetCell
{
 public:
  SpreadsheetCell();
  SpreadsheetCell(double initialValue);
  SpreadsheetCell(const string& initialValue);
  SpreadsheetCell(const SpreadsheetCell &src);
  void setValue(double inValue);
  double getValue();
  void setString(const string& inString);
  string getString();

 protected:
  string doubleToString(double inValue);
  double stringToDouble(const string& inString);

  double mValue;
  string mString;
};
