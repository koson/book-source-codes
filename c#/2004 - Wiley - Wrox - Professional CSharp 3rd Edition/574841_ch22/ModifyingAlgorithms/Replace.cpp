#include <algorithm>
#include <functional>
#include <vector>
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

void print(int elem)
{
  cout << elem << " ";
}

int main(int argc, char** argv)
{
  vector<int> myVector;
  populateContainer(myVector);
  replace_if(myVector.begin(), myVector.end(), bind2nd(less<int>(), 0), 0);
  replace_if(myVector.begin(), myVector.end(), bind2nd(greater<int>(), 100),
	     100);
  for_each(myVector.begin(), myVector.end(), &print);
  cout << endl;

  return (0);
}
