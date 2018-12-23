#include <iostream>

using namespace std;

class Simple 
{
public:
  Simple() { cout << "Simple constructor called!" << endl; }
};

int main(int argc, char** argv)
{
  Simple** mySimplePtrArray = new Simple*[4];

  //  Allocate an object for each pointer.
  for (int i = 0; i < 4; i++) {
    mySimplePtrArray[i] = new Simple();
  }

  //  Use mySimplePtrArray.

  //  Delete each allocated object.
  for (int i = 0; i < 4; i++) {
    delete mySimplePtrArray[i];
  }

  //  Delete the array itself.
  delete[] mySimplePtrArray;
}
