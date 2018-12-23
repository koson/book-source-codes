#include <cstdlib>

const double PI = 3.14159;

class BigClass;
void doSomething(const BigClass& arg)
{
  // implementation here
}

void constIntOne()
{
  const int* ip;
  ip = new int[10];
  //  ip[4] = 5; // DOES NOT COMPILE!
}

void constIntTwo()
{
  int const * ip;
  ip = new int[10];
  //  ip[4] = 5; // DOES NOT COMPILE!
}

void constPtrOne()
{
  int* const ip = NULL;
  //  ip = new int[10]; // DOES NOT COMPILE!
  ip[4] = 5;
}

void constIntPtrOne()
{
  int const * const ip = NULL;
}

void constIntPtrTwo()
{
  const int * const ip = NULL;
}

void manyLevelConst()
{
  const int * const * const * const ip = NULL;
}

int main(int argc, char** argv)
{
  int* ip;
  ip = new int[10];
  ip[4] = 5;

  int z;
  const int& zRef = z;
  //  zRef = 4; // DOES NOT COMPILE

  return (0);
}
