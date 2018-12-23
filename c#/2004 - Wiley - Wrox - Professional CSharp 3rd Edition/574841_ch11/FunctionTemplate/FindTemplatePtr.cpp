#include <iostream>
#include <cstring>
using namespace std;

#include "SpreadsheetCell.h"

template <typename T>
int Find(T& value, T* arr, int size)
{
  cout << "original\n";
  for (int i = 0; i < size; i++) {
    if (arr[i] == value) {
      // found it; return the index
      return (i);
    }
  }
  // failed to Find it; return -1
  return (-1);
}

template <typename T>
int Find(T*& value, T** arr, int size)
{
  cout << "ptr special\n";
  for (int i = 0; i < size; i++) {
    if (*arr[i] == *value) {
      // found it; return the index
      return (i);
    }
  }
  // failed to Find it; return -1
  return (-1);
}

/*
template <typename T>
int Find<T*>(T*& value, T** arr, int size)
{
  cout << "ptr special\n";
  for (int i = 0; i < size; i++) {
    if (*arr[i] == *value) {
      // found it; return the index
      return (i);
    }
  }
  // failed to Find it; return -1
  return (-1);
}
*/

template<>
int Find<char*>(char*& value, char** arr, int size)
{
  cout << "Specialization\n";
  for (int i = 0; i < size; i++) {
    if (strcmp(arr[i], value) == 0) {
      // found it; return the index
      return (i);
    }
  }
  // failed to find it; return -1
  return (-1);
}

int Find(char*& value, char** arr, int size)
{
  cout << "overload\n";
  for (int i = 0; i < size; i++) {
    if (strcmp(arr[i], value) == 0) {
      // found it; return the index
      return (i);
    }
  }
  // failed to find it; return -1
  return (-1);
}

int main(int argc, char** argv)
{
  char* word = "two";
  char* arr[4] = {"one", "two", "three", "four"};
  int res;

  int x = 3, intArr[4] = {1, 2, 3, 4};
  double d1 = 5.6, dArr[4] = {1.2, 3.4, 5.7, 7.5};

  res = Find(x, intArr, 4); // calls find<int> by deduction
  res = Find<int>(x, intArr, 4); // call find<int> explicitly

  res = Find(d1, dArr, 4); // call find<double> by deduction
  res = Find<double>(d1, dArr, 4); // calls find<double> explicitly

  res = Find<char *>(word, arr, 4); // Calls the template specialization for char *s
  res = Find(word, arr, 4); // Calls the overloaded Find for char *s

  int *px = &x, *pArr[2] = {&x, &x};
  res = Find(px, pArr, 2); // Calls the overloaded Find for pointers

  SpreadsheetCell c1(10), c2[2] = {SpreadsheetCell(4), SpreadsheetCell(10)};

  res = Find(c1, c2, 2); // calls find<SpreadsheetCell> by deduction
  res = Find<SpreadsheetCell>(c1, c2, 2); // calls find<SpreadsheetCell> explicitly

  SpreadsheetCell *pc1 = &c1;
  SpreadsheetCell *psa[2] = {&c1, &c1};

  res = Find(pc1, psa, 2); // Calls the overloaded Find for pointers

  return (0);
}
