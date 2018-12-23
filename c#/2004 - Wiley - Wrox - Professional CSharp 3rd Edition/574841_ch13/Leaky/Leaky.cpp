#include <iostream>

using namespace std;

class Simple 
{
public:
  Simple() { mIntPtr = new int(); }
  ~Simple() { delete mIntPtr; }

  void setIntPtr(int inInt) { *mIntPtr = inInt; }

protected:
  int* mIntPtr;
};

void doSomething(Simple*& outSimplePtr)
{
  outSimplePtr = new Simple(); // BUG! Doesn.t delete the original
}

int main(int argc, char** argv)
{
  Simple* simplePtr = new Simple(); // allocate a Simple object

  doSomething(simplePtr);

  delete simplePtr; // only cleans up the second object
}
