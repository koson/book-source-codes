#include <iostream>
#include <cstring>
using namespace std;

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

int main(int argc, char** argv)
{
  char* word = "two";
  char* arr[4] = {"one", "two", "three", "four"};
  int res;

  res = Find<char*>(word, arr, 4); // Calls the char * specialization
  res = Find(word, arr, 4); // Calls the char * specialization
  cout << res << endl;

  return (0);
}
