#include <iostream>

using namespace std;

int main(int argc, char** argv)
{
  int* ptr;
  int numInts = 10;

  try {
    ptr = new int[numInts];
  } catch (bad_alloc& e) {
    cerr << "Unable to allocate memory!\n";
    // Handle memory allocation failure
    return (0);
  }
  // Proceed with function that assumes memory has been allocated

  return (0);
}
