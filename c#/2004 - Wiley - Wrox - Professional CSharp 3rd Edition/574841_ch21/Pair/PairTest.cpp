#include <utility>
#include <string>
#include <iostream>
using namespace std;

int main(int argc, char** argv)
{
  // two-argument ctor and default ctor
  pair<string, int> myPair("hello", 5), myOtherPair;

  // Can assign directly to first and second
  myOtherPair.first = "hello";
  myOtherPair.second = 6;

  // copy ctor
  pair<string, int> myThirdPair(myOtherPair);

  // operator<
  if (myPair < myOtherPair) {
    cout << "myPair is less than myOtherPair\n";
  } else {
    cout << "myPair is greater than or equal to myOtherPair\n";
  }

  // operator==
  if (myOtherPair == myThirdPair) {
    cout << "myOtherPair is equal to myThirdPair\n";
  } else {
    cout << "myOtherPair is not equal to myThirdPair\n";
  }

  pair<int, int> aPair = make_pair(5, 10);

  return (0);
}
