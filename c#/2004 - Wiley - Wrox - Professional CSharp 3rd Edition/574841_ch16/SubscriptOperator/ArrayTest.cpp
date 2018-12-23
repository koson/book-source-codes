#include "Array.h"

#include <iostream>
using namespace std;

void printArray(const Array& arr, int size);

int main(int argc, char** argv)
{
  Array arr;
  int i;

  for (i = 0; i < 10; i++) {
    arr[i] = 100; // Calls the non-const operator[] because
    // arr is a non-const object.
  }
  printArray(arr, 10);
  return (0);
}

void printArray(const Array& arr, int size)
{
  for (int i = 0; i < size; i++) {
    cout << arr[i] << " "; // calls the const operator[] because arr is a const
    // object.
  }
  cout << endl;
}
