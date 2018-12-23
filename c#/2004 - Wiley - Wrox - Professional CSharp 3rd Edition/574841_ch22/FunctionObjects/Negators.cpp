#include <algorithm>
#include <vector>
#include <iostream>
#include <functional>
using namespace std;

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
				     not1(bind2nd(greater_equal<int>(), 100)));
  if (it == myVector.end()) {
    cout << "All perfect scores\n";
  } else {
    cout << "Found a \"less-than-perfect\" score of " << *it << endl;
  }
  return (0);
}
