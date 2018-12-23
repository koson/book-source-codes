#include "ErrorCorrelator.h"
#include <iostream>
using namespace std;

int main(int argc, char** argv)
{
  ErrorCorrelator ec;

  ec.addError(Error(3, "Unable to read file"));
  ec.addError(Error(1, "Incorrect entry from user"));
  ec.addError(Error(10, "Unable to allocate memory!"));

  while (true) {
    try {
      Error e = ec.getError();
      cout << e << endl;
    } catch (out_of_range& ) {
      cout << "Finished processing errors\n";
      break;
    }
  }

  return (0);
}
