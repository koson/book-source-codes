#include <algorithm>
#include <iostream>
#include <vector>
using namespace std;

int main(int argc, char** argv)
{
  // The list of elements to be searched
  int elems[] = {5, 6, 9, 8, 8, 3};

  // Construct a vector from the list, exploiting the
  // fact that pointers are iterators too.
  vector<int> myVector(elems, elems + 6); 
  vector<int>::const_iterator it, it2;  

  // Find the min and max elements in the vector
  it = min_element(myVector.begin(), myVector.end());
  it2 = max_element(myVector.begin(), myVector.end());
  cout << "The min is " << *it << " and the max is " << *it2 << endl;

  // Find the first pair of matching consecutive elements
  it = adjacent_find(myVector.begin(), myVector.end());
  if (it != myVector.end()) {
    cout << "Found two consecutive equal elements of value "
	 << *it << endl;
  }

  // Find the first of two values
  int targets[] = {8, 9};
  it = find_first_of(myVector.begin(), myVector.end(), targets,
		     targets + 2);

  if (it != myVector.end()) {
    cout << "Found one of 8 or 9: " << *it << endl;
  }

  // Find the first subsequence
  int sub[] = {8, 3};
  it = search(myVector.begin(), myVector.end(), sub, sub + 2);
  if (it != myVector.end()) {
    cout << "Found subsequence 8, 3 at position " << it - myVector.begin()
	 << endl;
  }

  // Find the last subsequence (which should be the same as the first)
  it2 = find_end(myVector.begin(), myVector.end(), sub, sub + 2);
  if (it != it2) {
    cout << "Error: search and find_end found different subsequences "
	 << " even though there is only one match.\n";
  }

  // Find the first subsequence of two consecutive 8s
  it = search_n(myVector.begin(), myVector.end(), 2, 8);
  if (it != myVector.end()) {
    cout << "Found two consecutive 8s starting at position "
	 << it - myVector.begin() << endl;
  }

  return (0);
}
