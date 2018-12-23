#include <functional>
#include <algorithm>
#include <string>
#include <vector>
#include <iostream>
using namespace std;

void removeEmptyStrings(vector<string>& strings)
{
  vector<string>::iterator it = remove_if(strings.begin(), strings.end(),
					  mem_fun_ref(&string::empty));
  // erase the removed elements
  strings.erase(it, strings.end());
}

void printString(const string& str)
{
  cout << str << " ";
}

int main(int argc, char** argv)
{
  vector<string> myVector;
  myVector.push_back("");
  myVector.push_back("stringone");
  myVector.push_back("");
  myVector.push_back("stringtwo");
  myVector.push_back("stringthree");
  myVector.push_back("stringfour");

  removeEmptyStrings(myVector);
  cout << "Size is " << myVector.size() << endl;
  for_each(myVector.begin(), myVector.end(), &printString);
  cout << endl;
  return (0);
}
