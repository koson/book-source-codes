#include <iostream>
using namespace std;

#include "SpreadsheetCell.h"

template <typename T>
int Find(T& value, T* arr, int size)
{
  for (int i = 0; i < size; i++) {
    if (arr[i] == value) {
      // found it; return the index
      return (i);
    }
  }
  // failed to find it; return -1
  return (-1);
}

int main(int argc, char** argv)
{
  int x = 3, intArr[4] = {1, 2, 3, 4};
  double d1 = 5.6, dArr[4] = {1.2, 3.4, 5.7, 7.5};

  int res;
  res = Find(x, intArr, 4); // calls Find<int> by deduction
  cout << res << endl;

  res = Find<int>(x, intArr, 4); // call Find<int> explicitly

  res = Find(d1, dArr, 4); // call Find<double> by deduction
  res = Find<double>(d1, dArr, 4); // calls Find<double> explicitly
  cout << res << endl;

  //  res = Find(x, dArr, 4);  // DOES NOT COMPILE! Arguments are different types.

  SpreadsheetCell c1(10), c2[2] = {SpreadsheetCell(4), SpreadsheetCell(10)};

  res = Find(c1, c2, 2); // calls Find<SpreadsheetCell> by deduction
  res = Find<SpreadsheetCell>(c1, c2, 2); // calls Find<SpreadsheetCell> explicitly

  return (0);
}
