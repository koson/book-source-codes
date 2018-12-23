#include <algorithm>
#include <iostream>
using namespace std;

int main(int argc, char** argv)
{
  int x = 4, y = 5;
  cout << "x is " << x << " and y is " << y << endl;
  cout << "Max is " << max(x, y) << endl;
  cout << "Min is " << min(x, y) << endl;
  swap(x, y);
  cout << "x is " << x << " and y is " << y << endl;
  cout << "Max is " << max(x, y) << endl;
  cout << "Min is " << min(x, y) << endl;

  return (0);
}
