#include <iostream>
using namespace std;

//#define SQUARE(x) ((x) * (x)) // No semicolon after the macro definition!
#define SQUARE(x) (x * x)

int main(int argc, char** argv)
{
  cout << SQUARE(4) << endl;
  cout << SQUARE(2 + 2) << endl;

  return (0);
}
