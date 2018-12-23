/**
 * TemplateTest.cpp
 */

#include <iostream>
#include <string>

#include "SimpleTemplate.h" 

using namespace std;

int main(int argc, char** argv)
{
  // Try wrapping in integer
  int i = 7;
  SimpleTemplate<int> intWrapper(i);
  i = 2;
  cout << "wrapper value is " << intWrapper.get() << endl;

  // Try wrapping a string
  string str = "test";
  SimpleTemplate<string> stringWrapper(str);
  str += "!";
  cout << "wrapper value is " << stringWrapper.get() << endl;
}
