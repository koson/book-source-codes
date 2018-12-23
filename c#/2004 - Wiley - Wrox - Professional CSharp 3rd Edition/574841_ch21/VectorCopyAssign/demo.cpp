#include <vector>
using namespace std;

int main(int argc, char** argv)
{
  vector<int> intVector(10, 0);
  // other code...
  intVector.assign(5, 100);

  vector<int> vectorOne(10, 0);
  vector<int> vectorTwo(5, 100);

  vectorOne.swap(vectorTwo);
  // vectorOne now has 5 elements with the value 100.
  // vectorTwo now has 10 elements with the value 0

  return (0);
} 
