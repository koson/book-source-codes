#include <functional>
#include <algorithm>
#include <string>
#include <vector>
#include <iostream>
using namespace std;

void findEmptyString(const vector<string*>& strings)
{
  vector<string*>::const_iterator it = find_if(strings.begin(), strings.end(),
					       mem_fun(&string::empty));

  if (it == strings.end()) {
    cout << "No empty strings!\n";
  } else {
    cout << "Empty string at position: " << it - strings.begin() << endl;
  }
}

int main(int argc, char **argv)
{
  vector<string *> myVector;

  string one = "blah";
  string two = "";
  myVector.push_back(&one);
  myVector.push_back(&one);
  myVector.push_back(&two);
  myVector.push_back(&one);

  findEmptyString(myVector);

  return (0);
}
