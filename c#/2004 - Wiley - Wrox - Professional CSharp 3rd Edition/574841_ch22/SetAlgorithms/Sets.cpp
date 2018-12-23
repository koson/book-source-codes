#include <algorithm>
#include <iostream>
#include <vector>
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

void print(int elem)
{
  cout << elem << " ";
}

int main(int argc, char** argv)
{
  vector<int> setOne, setTwo, setThree;
  cout << "Enter set one:\n";
  populateContainer(setOne);
  cout << "Enter set two:\n";
  populateContainer(setTwo);

  // set algorithms work on sorted ranges
  sort(setOne.begin(), setOne.end());
  sort(setTwo.begin(), setTwo.end());

  if (includes(setOne.begin(), setOne.end(), setTwo.begin(), setTwo.end())) {
    cout << "The second set is a subset of the first\n";
  }
  if (includes(setTwo.begin(), setTwo.end(), setOne.begin(), setOne.end())) {
    cout << "The first set is a subset of the second\n";
  }

  setThree.resize(setOne.size() + setTwo.size());
  vector<int>::iterator newEnd;
  newEnd = set_union(setOne.begin(), setOne.end(), setTwo.begin(),
		     setTwo.end(), setThree.begin());
  cout << "The union is: ";
  for_each(setThree.begin(), newEnd, &print);
  cout << endl;

  newEnd = set_intersection(setOne.begin(), setOne.end(), setTwo.begin(),
			    setTwo.end(), setThree.begin());
  cout << "The intersection is: ";
  for_each(setThree.begin(), newEnd, &print);
  cout << endl;

  newEnd = set_difference(setOne.begin(), setOne.end(), setTwo.begin(),
			  setTwo.end(), setThree.begin());
  cout << "The difference between set one and set two is: ";
  for_each(setThree.begin(), newEnd, &print);
  cout << endl;

  newEnd = set_symmetric_difference(setOne.begin(), setOne.end(), setTwo.begin(),
				    setTwo.end(), setThree.begin());
  cout << "The symmetric difference is: ";
  for_each(setThree.begin(), newEnd, &print);
  cout << endl;

  return (0);
}
