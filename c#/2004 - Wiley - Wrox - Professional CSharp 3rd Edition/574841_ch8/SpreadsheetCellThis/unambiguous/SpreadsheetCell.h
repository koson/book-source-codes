#include <string>
using std::string;

class SpreadsheetCell
{
 public:
  void setValue(double mValue);
  double getValue();
  void setString(string inString);
  string getString();

 protected:
  string doubleToString(double inValue);
  double stringToDouble(string inString);

  double mValue;
  string mString;
};
