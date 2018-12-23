#include <algorithm>
#include <vector>
#include <iostream>
using namespace std;

bool perfectScore(int num)
{
  return (num >= 100);
}

int main(int argc, char** argv)
{
  int num;

  vector<int> myVector;
  while (true) {
    cout << "Enter a test score to add (0 to stop): ";
    cin >> num;
    if (num == 0) {
      break;
    }
    myVector.push_back(num);
  }

  vector<int>::iterator it = find_if(myVector.begin(), myVector.end(),
				     perfectScore);
  if (it == myVector.end()) {
    cout << "No perfect scores\n";
  } else {
    cout << "Found a \"perfect\" score of " << *it << endl;
  }
  return (0);
}
