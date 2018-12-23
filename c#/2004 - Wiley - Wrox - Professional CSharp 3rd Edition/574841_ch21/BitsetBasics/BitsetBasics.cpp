#include <bitset>
#include <iostream>
using namespace std;

int main(int argc, char** argv)
{
  bitset<10> myBitset;

  myBitset.set(3);
  myBitset.set(6);
  myBitset[8] = true;
  myBitset[9] = myBitset[3];

  if (myBitset.test(3)) {
    cout << "Bit 3 is set!\n";
  }
  cout << myBitset << endl;

  return (0);
}
