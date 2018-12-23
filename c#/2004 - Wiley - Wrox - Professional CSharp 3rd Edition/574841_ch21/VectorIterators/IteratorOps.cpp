#include <vector>
using namespace std;

int main(int argc, char** argv)
{
  vector<int> intVector(10, 0);

  vector<int>::iterator it = intVector.begin();
  it += 5;
  --it;
  *it = 4;

  return (0);
}
