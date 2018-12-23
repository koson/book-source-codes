#include <vector>
#include <iostream>
using namespace std;

int main(int argc, char** argv)
{
  vector<double> doubleVector(10, 0); // creates vector of 10 doubles of value 0
  double max;
  int i;

  // No longer need to initialize each element: the ctor did it for you

  // Read the first score before the loop in order to initialize max
  cout << "Enter score 1: ";
  cin >> doubleVector[0];
  max = doubleVector[0];

  for (i = 1; i < 10; i++) {
    cout << "Enter score " << i + 1 << ": ";
    cin >> doubleVector[i];
    if (doubleVector[i] > max) {
      max = doubleVector[i];
    }
  }

  max /= 100;
  for (i = 0; i < 10; i++) {
    doubleVector[i] /= max;
    cout << doubleVector[i] << " ";
  }
  cout << endl;
  return (0);
}
