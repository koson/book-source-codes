#include "CableCompany.h"
#include <iostream>
using namespace std;

int main(int argc, char** argv)
{
  CableCompany myCC;
  string basic_pkg = "1111000000";
  string premium_pkg = "1111111111";
  string sports_pkg = "0000100111";

  myCC.addPackage("basic", bitset<kNumChannels>(basic_pkg));
  myCC.addPackage("premium", bitset<kNumChannels>(premium_pkg));
  myCC.addPackage("sports", bitset<kNumChannels>(sports_pkg));

  myCC.newCustomer("Nicholas Solter", "basic");
  myCC.addPackageToCustomer("Nicholas Solter", "sports");
  cout << myCC.getCustomerChannels("Nicholas Solter") << endl;

  return (0);
}
