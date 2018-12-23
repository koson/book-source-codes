#include <vector>
#include <string>
using namespace std;

int main(int argc, char** argv)
{
  vector<string> stringVector(10, "hello");

  for (vector<string>::iterator it = stringVector.begin();
       it != stringVector.end(); ++it) {
    it->append(" there");
  }
}
