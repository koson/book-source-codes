#include <vector>
#include <iostream>
using namespace std;

int main(int argc, char** argv)
{
  vector<double> doubleVector;
  double max, temp;
  int i;

  // Read the first score before the loop in order to initialize max
  cout << "Enter score 1: ";
  cin >> max;
  doubleVector.push_back(max);

  for (i = 1; true; i++) {
    cout << "Enter score " << i + 1 << " (-1 to stop): ";
    cin >> temp;
    if (temp == -1) {
      break;
    }
    doubleVector.push_back(temp);
    if (temp > max) {
      max = temp;
    }
  }

  max /= 100;
  for (vector<double>::iterator it = doubleVector.begin();
       it != doubleVector.end(); ++it) {
    *it /= max;
    cout << *it << " ";
  }
  cout << endl;
  return (0);
}
