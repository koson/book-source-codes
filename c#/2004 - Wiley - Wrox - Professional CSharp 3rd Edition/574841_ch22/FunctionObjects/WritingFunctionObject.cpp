#include <functional>
#include <algorithm>
#include <cctype>
#include <string>
#include <iostream>
using namespace std;

class myIsDigit : public unary_function<char, bool>
{
public:
  bool operator() (char c) const { return (::isdigit(c)); }
};

bool isNumber(const string& str)
{
  string::const_iterator it = find_if(str.begin(), str.end(),
				      not1(myIsDigit()));
  return (it == str.end());
}

int main(int argc, char** argv)
{

  cout << isNumber("12345") << endl;
  cout << isNumber("hello") << endl;
  cout << isNumber("1234a") << endl;

  return (0);
}
