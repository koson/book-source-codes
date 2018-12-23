#include "hashmap.h"
#include <iostream>
using namespace std;

int main(int argc, char** argv)
{
  hashmap<int, int> myHash;
  myHash.insert(make_pair(4, 40));
  myHash.insert(make_pair(6, 60));

  hashmap<int, int>::value_type* x = myHash.find(4);
  if (x != NULL) {
    cout << "4 maps to " << x->second << endl;
  } else {
    cout << "cannot find 4 in map\n";
  }

  myHash.erase(4);

  hashmap<int, int>::value_type* x2 = myHash.find(4);
  if (x2 != NULL) { 
    cout << "4 maps to " << x2->second << endl;
  } else {
    cout << "cannot find 4 in map\n";
  }

  myHash[4] = 35;
  myHash[4] = 60;

  hashmap<int, int>::value_type* x3 = myHash.find(4);
  if (x3 != NULL) { 
    cout << "4 maps to " << x3->second << endl;
  } else {
    cout << "cannot find 4 in map\n";
  }

  return (0);
}
