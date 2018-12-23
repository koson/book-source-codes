#include "NameDB.h"

#include <iostream>
using namespace std;

int main(int argc, char** argv)
{
  NameDB boys("../boys_long.txt");

  cout << boys.getNameRank("Daniel") << endl;
  cout << boys.getNameRank("Jacob") << endl;
  cout << boys.getNameRank("William") << endl;

  cout << boys.getAbsoluteNumber("Daniel") << endl;

  return (0);
}
