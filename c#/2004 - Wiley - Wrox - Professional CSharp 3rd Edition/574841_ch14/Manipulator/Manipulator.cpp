#include <iostream>
#include <iomanip>

using namespace std;

int main(int argc, char** argv)
{
  bool myBool = true;

  cout << "This is the default: " << myBool << endl;
  cout << "This should be true: " << boolalpha << myBool << endl;
  cout << "This should be 1: " << noboolalpha << myBool << endl;

  double dbl = 1.452;
  double dbl2 = 5;

  cout << "This should be 5: " << setw(2) << noshowpoint << dbl2 << endl;
  cout << "This should be @@1.452: " << setw(7) << setfill('@') << dbl << endl;
}
