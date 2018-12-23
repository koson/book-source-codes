#include <map>
#include <iostream>
using namespace std;

class Data
{
public:
  Data(int val = 0) { mVal = val; }
  int getVal() const { return mVal; }
  void setVal(int val) {mVal = val; }
  // remainder of definition omitted 
protected:
  int mVal;
};

int main(int argc, char** argv)
{
  map<int, Data> dataMap;

  dataMap[1] = Data(4);
  dataMap[1] = Data(6); // replaces the element with key 1

  for (map<int, Data>::iterator it = dataMap.begin();
       it != dataMap.end(); ++it) {
    cout << it->second.getVal() << endl;
  }
  return (0);
}
