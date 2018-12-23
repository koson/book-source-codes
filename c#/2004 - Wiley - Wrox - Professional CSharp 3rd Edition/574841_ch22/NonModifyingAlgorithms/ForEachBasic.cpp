#include <algorithm>
#include <map>
#include <iostream>
using namespace std;

void printPair(const pair<int, int>& elem)
{
  cout << elem.first << "->" << elem.second << endl;
}

int main(int argc, char** argv)
{
  map<int, int> myMap;
  myMap.insert(make_pair(4, 40));
  myMap.insert(make_pair(5, 50));
  myMap.insert(make_pair(6, 60));
  myMap.insert(make_pair(7, 70));
  myMap.insert(make_pair(8, 80));

  for_each(myMap.begin(), myMap.end(), &printPair); 

  return (0);
}
