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
  dataMap[1] = Data(6);

  map<int, Data>::iterator it = dataMap.find(1);
  if (it != dataMap.end()) {
    it->second.setVal(100);
  }

  return (0);
}
