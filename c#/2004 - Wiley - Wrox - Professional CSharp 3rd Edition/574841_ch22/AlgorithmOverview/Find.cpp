#include <algorithm>
#include <vector>
#include <iostream>
using namespace std;

int main(int argc, char** argv)
{
  int num;

  vector<int> myVector;
  while (true) {
    cout << "Enter a number to add (0 to stop): ";
    cin >> num;
    if (num == 0) {
      break;
    }
    myVector.push_back(num);
  }

  while (true) {
    cout << "Enter a number to lookup (0 to stop): ";
    cin >> num;
    if (num == 0) {
      break;
    }
    vector<int>::iterator it = find(myVector.begin(), myVector.end(), num);
    if (it == myVector.end()) {
      cout << "Could not find " << num << endl;
    } else {
      cout << "Found " << *it << endl;
    }
  }

  return (0);
}
