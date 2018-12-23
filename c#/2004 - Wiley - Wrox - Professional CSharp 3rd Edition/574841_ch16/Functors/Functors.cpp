#include <string>
using namespace std;

class FunctionObject
{
public:
  int operator() (int inParam); // function-call operator
  void operator() (string& str) {}
  int aMethod(int inParam); // normal method
};

//Implementation of overloaded function-call operator
int FunctionObject::operator() (int inParam)
{
  return (inParam * inParam);
}

// Implementation of normal method
int FunctionObject::aMethod(int inParam)
{
  return (inParam * inParam);
}

int main(int argc, char** argv)
{
  int x = 3, xSquared, xSquaredAgain;
  FunctionObject square;

  xSquared = square(x); // Call the function-call operator
  xSquaredAgain = square.aMethod(x); // Call the normal method

  return (0);
}

