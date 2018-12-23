#include <new>
#include <cstdlib>
#include <iostream>

using namespace std;

void myNewHandler()
{
  cerr << "Unable to allocate memory. Terminating program!\n";
  abort();
}

int main(int argc, char** argv)
{
  // code omitted

  // Set the new new_handler and save the old.
  new_handler oldHandler = set_new_handler(myNewHandler);
  // code that calls new

  // reset the old new_handler
  set_new_handler(oldHandler);
  // code omitted
  return (0);
}
