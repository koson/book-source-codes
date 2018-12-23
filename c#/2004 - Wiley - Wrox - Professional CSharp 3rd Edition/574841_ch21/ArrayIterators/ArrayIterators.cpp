#include <vector>
#include <iostream>

using namespace std;

int main(int argc, char** argv)
{
  int arr[10]; // normal C++ array
  vector<int> vec; // STL vector
  int i;

  //
  // Initialize each element of the array to the value of
  // its index.
  //
  for (i = 0; i < 10; i++) {
    arr[i] = i;
  }

  //
  // Insert the contents of the array into the
  // end of the vector.
  //
  vec.insert(vec.end(), arr, arr + 10);

  // print the contents of the vector
  for (i = 0; i < 10; i++) {
    cout << vec[i] << " ";
  }

  return (0);
}
