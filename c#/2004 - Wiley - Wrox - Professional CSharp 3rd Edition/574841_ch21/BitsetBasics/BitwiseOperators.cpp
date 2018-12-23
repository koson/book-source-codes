#include <bitset>
#include <iostream>
using namespace std;

int main(int argc, char** argv)
{
  string str1 = "0011001100";
  string str2 = "0000111100";
  bitset<10> bitsOne(str1), bitsTwo(str2);

  bitset<10> bitsThree = bitsOne & bitsTwo;
  cout << bitsThree << endl;
  bitsThree <<= 4;
  cout << bitsThree << endl;

  return (0);
}
