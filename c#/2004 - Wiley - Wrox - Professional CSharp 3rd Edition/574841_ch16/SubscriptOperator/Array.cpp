#include "Array.h"

#include <stdexcept>
using namespace std;

// Microsoft Visual Studio requires you to omit the following line.
// However, some compilers require it.
//const int Array::kAllocSize;

Array::Array()
{
  mSize = kAllocSize;
  mElems = new int[mSize];
}

Array::~Array()
{
  delete [] mElems;
}

void Array::resize(int newSize)
{
  int* newElems = new int[newSize]; // Allocate the new array of the new size

  // The new size is always bigger than the old size
  for (int i = 0; i < newSize; i++) {
    // Copy the elements from the old array to the new one
    newElems[i] = mElems[i];
  }
  mSize = newSize; // store the new size
  delete [] mElems; // free the memory for the old array
  mElems = newElems; // Store the pointer to the new array
}

int& Array::operator[](int x)
{
  if (x < 0) {
    throw out_of_range("");
  }
  if (x >= mSize) {
    // Allocate kAllocSize past the element the client wants
    resize (x + kAllocSize);
  }
  return (mElems[x]);
}

const int& Array::operator[](int x) const
{
  if (x < 0 || x >=mSize) {
    throw out_of_range("");
  }
  return (mElems[x]);
}
