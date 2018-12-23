#include <vector>
#include <iostream>
using namespace std;

int main(int argc, char** argv)
{
  vector<int> vectorOne(10, 0);
  vector<int> vectorTwo(10, 0);

  if (vectorOne == vectorTwo) {
    cout << "equal!\n";
  } else {
    cout << "not equal!\n";
  }

  vectorOne[3] = 50;

  if (vectorOne < vectorTwo) {
    cout << "vectorOne is less than vectorTwo\n";
  } else {
    cout << "vectorOne is not less than vectorTwo\n";
  }
  return (0);
}
