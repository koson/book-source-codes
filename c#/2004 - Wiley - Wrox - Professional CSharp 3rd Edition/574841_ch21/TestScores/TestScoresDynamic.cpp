#include <vector>
#include <iostream>
using namespace std;

int main(int argc, char** argv)
{
  vector<double> doubleVector; // create a vector with zero elements
  double max, temp;
  size_t i;

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
  for (i = 0; i < doubleVector.size(); i++) { 
    doubleVector[i] /= max;
    cout << doubleVector[i] << " ";
  }
  cout << endl;
  return (0);
}
