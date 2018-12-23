#include <vector>
using namespace std;

class Element
{
public:
  Element() {}
  ~Element() {}
};

int main(int argc, char** argv)
{
  vector<Element>*  elementVector = new vector<Element>(10);
  delete elementVector;
  return (0);
}
