#include "CableCompany.h"
using namespace std;

void CableCompany::addPackage(const string& packageName,
			      const bitset<kNumChannels>& channels)
{
  // Just make a key/value pair and insert it into the packages map
  mPackages.insert(make_pair(packageName, channels));
}

void CableCompany::removePackage(const string& packageName)
{
  // Just erase the package from the package map
  mPackages.erase(packageName);
}

void CableCompany::newCustomer(const string& name, const string& package)
  throw (out_of_range)
{
  // Get a reference to the specified package
  MapType::const_iterator it = mPackages.find(package);
  if (it == mPackages.end()) {
    // That package doesn't exist. Throw an exception.
    throw (out_of_range("Invalid package"));
  } else {
    // Create the account with the bitset representing that package
    // Note that it refers to a name/bitset pair. The bitset is the
    // second field.
    mCustomers.insert(make_pair(name, it->second));
  }
}

void CableCompany::newCustomer(const string& name,
			       const bitset<kNumChannels>& channels)
{
  // Just add the customer/channels pair to the customers map
  mCustomers.insert(make_pair(name, channels));
}

void CableCompany::addChannel(const string& name, int channel)
{
  // Find a reference to the customers
  MapType::iterator it = mCustomers.find(name);
  if (it != mCustomers.end()) {
    // We found this customer; set the channel
    // Note that it is a reference to a name/bitset pair.
    // The bitset is the second field.
    it->second.set(channel);
  }
}

void CableCompany::removeChannel(const string& name, int channel)
{
  // Find a reference to the customers
  MapType::iterator it = mCustomers.find(name);
  if (it != mCustomers.end()) {
    // We found this customer; remove the channel
    // Note that it is a refernce to a name/bitset pair.
    // The bitset is the second field.
    it->second.reset(channel);
  }
}

void CableCompany::addPackageToCustomer(const string& name, const string& package)
{
  // Find the package
  MapType::iterator itPack = mPackages.find(package);
  // Find the customer
  MapType::iterator itCust = mCustomers.find(name);
  if (itCust != mCustomers.end() && itPack != mPackages.end()) {
    // Only if both package and customer are found, can we do the update.
    // Or-in the package to the customers existing channels.
    // Note that it is a reference to a name/bitset pair.
    // The bitset is the second field.
    itCust->second |= itPack->second; 
  }
}

void CableCompany::deleteCustomer(const string& name)
{
  // Remove the customer with this name
  mCustomers.erase(name);
}

bitset<kNumChannels>& CableCompany::getCustomerChannels(const string& name)
  throw (out_of_range)
{
  // Find the customer
  MapType::iterator it = mCustomers.find(name);
  if (it != mCustomers.end()) {
    // Found it!
    // Note that it is a reference to a name/bitset pair.
    // The bitset is the second field.
    return (it->second);
  }
  // Didn't find it. Throw an exception.
  throw (out_of_range("No customer of that name"));
} 
