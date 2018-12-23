#include <iostream>
using namespace std;

int name = 3;

int main(int argc, char** argv)
{
  int name = 4;

  cout << name << endl; // accesses local name
  cout << ::name << endl; // accesses global name

  return (0);
}
