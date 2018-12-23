#include <string>
#include <iostream>

using namespace std;

int main(int argc, char** argv)
{
  string myString = "hello";

  myString += ", there";

  string myOtherString = myString;

  if (myString == myOtherString) {
    myOtherString[0] = 'H';
  }

  cout << myString << endl;
  cout << myOtherString << endl;
}
