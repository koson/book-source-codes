#include <algorithm>
#include <vector>
#include <list>
#include <iostream>
using namespace std;

// Function template to populate a container of ints.
// The container must support push_back().
template<typename Container>
void populateContainer(Container& cont)
{
  int num;

  while (true) {
    cout << "Enter a number (0 to quit): ";
    cin >> num;
    if (num == 0) {
      break;
    }
    cont.push_back(num);
  }
}

int main(int argc, char** argv)
{
  vector<int> myVector;
  list<int> myList;

  cout << "Populate the vector:\n";
  populateContainer(myVector);
  cout << "Populate the list:\n";
  populateContainer(myList);
  if (myList.size() < myVector.size()) {
    cout << "Sorry, the list is not long enough.\n";
    return (0);
  }

  // compare the two containers
  if (equal(myVector.begin(), myVector.end(), myList.begin())) {
    cout << "The two containers have equal elements\n";
  } else {
    // If the containers were not equal, find out why not
    pair<vector<int>::iterator, list<int>::iterator> miss =
      mismatch(myVector.begin(), myVector.end(), myList.begin());
    cout << "The first mismatch is at position "
	 << miss.first - myVector.begin() << ". The vector has value "
	 << *(miss.first) << " and the list has value " << *(miss.second)
	 << endl;
  }

  // Now order them
  if (lexicographical_compare(myVector.begin(), myVector.end(), myList.begin(),
			      myList.end())) {
    cout << "The vector is lexicographically first.\n";
  } else {
    cout << "The list is lexicographically first.\n";
  }

  return (0);
}
