#include <algorithm>
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
  vector<int> vectOne, vectTwo;

  populateContainer(vectOne);

  vectTwo.resize(vectOne.size());
  copy(vectOne.begin(), vectOne.end(), vectTwo.begin());
  for_each(vectTwo.begin(), vectTwo.end(), &print);

  return (0);
}
