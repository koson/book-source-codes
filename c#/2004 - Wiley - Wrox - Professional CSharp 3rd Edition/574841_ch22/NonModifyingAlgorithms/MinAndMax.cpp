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

class MinAndMax : public unary_function<int, void>
{
public:
  MinAndMax();
  void operator()(int elem);

  // make min and max public for easy access
  int min, max;

protected:
  bool first;
};

MinAndMax::MinAndMax() : min(-1), max(-1), first(true)
{
}

void MinAndMax::operator()(int elem)
{
  if (first) {
    min = max = elem;
  } else if (elem < min) {
    min = elem;
  } else if (elem > max) {
    max = elem;
  }
  first = false;
}

int main(int argc, char** argv)
{
  vector<int> myVector;
  populateContainer(myVector);

  MinAndMax func;
  func = for_each(myVector.begin(), myVector.end(), func);
  cout << "The max is " << func.max << endl;
  cout << "The min is " << func.min << endl;

  return (0);
}
