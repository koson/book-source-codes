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
  vector<int> vectorOne, vectorTwo, vectorMerged;
  cout << "Enter values for first vector:\n";
  populateContainer(vectorOne);
  cout << "Enter values for second vector:\n";
  populateContainer(vectorTwo);

  sort(vectorOne.begin(), vectorOne.end());
  sort(vectorTwo.begin(), vectorTwo.end());
  // make sure the vector is large enough to hold the values
  // from both source vectors
  vectorMerged.resize(vectorOne.size() + vectorTwo.size());
  merge(vectorOne.begin(), vectorOne.end(), vectorTwo.begin(),
        vectorTwo.end(), vectorMerged.begin());

  cout << "Merged vector: ";
  for_each(vectorMerged.begin(), vectorMerged.end(), &print);
  cout << endl;

  while (true) {
    int num;
    cout << "Enter a number to find (0 to quit): ";
    cin >> num;
    if (num == 0) {
      break;
    }
    if (binary_search(vectorMerged.begin(), vectorMerged.end(), num)) {
      cout << "That number is in the vector.\n";
    } else {
      cout << "That number is not in the vector\n";
    }
  }

  return (0);
}
