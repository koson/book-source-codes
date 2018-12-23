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
  pair<map<int, Data>::iterator, bool> ret;

  ret = dataMap.insert(make_pair(1, Data(4)));
  if (ret.second) {
    cout << "Insert succeeded!\n";
  } else {
    cout << "Insert failed!\n";
  }
  
  ret = dataMap.insert(make_pair(1, Data(6)));
  if (ret.second) {
    cout << "Insert succeeded!\n";
  } else {
    cout << "Insert failed!\n";
  }
  return (0);
}
