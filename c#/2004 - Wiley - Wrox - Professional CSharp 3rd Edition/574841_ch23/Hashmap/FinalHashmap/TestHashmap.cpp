#include "hashmap.h"
#include <iostream>
#include <map>
using namespace std;

int main(int argc, char** argv)
{
  hashmap<string, int> myHash;
  myHash.insert(make_pair("KeyOne", 100));
  myHash.insert(make_pair("KeyTwo", 200));
  myHash.insert(make_pair("KeyThree", 300));

  for (hashmap<string, int>::iterator it = myHash.begin();
       it != myHash.end(); ++it) {
    // Use both -> and * to test the operations
    cout << it->first << " maps to " << (*it).second << endl;
  }

  // Create a map with all the elements in the hashmap
  map<string, int> myMap(myHash.begin(), myHash.end());
  for (map<string, int>::iterator it = myMap.begin();
       it != myMap.end(); ++it) {
    // Use both -> and * to test the operations
    cout << it->first << " maps to " << (*it).second << endl;
  }

  hashmap<string, int> myHash2;
  myHash.swap(myHash2);
  hashmap<string, int>myHash3(myHash2);

  cout << myHash2.size() << endl;
  cout << myHash2.max_size() << endl;

  return (0);
}
