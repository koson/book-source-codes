#include <set>
#include <string>
#include <list>
using std::set;
using std::string;
using std::list;

class AccessList
{
 public:
  AccessList() {}

  //
  // Adds the user to the permissions list.
  //
  void addUser(const string& user);

  //
  // Removes the user from the permissions list.
  //
  void removeUser(const string& user);

  //
  // Returns true if user is in the permissionns list.
  //
  bool isAllowed(const string& user) const;

  //
  // Returns a list of all the users who have permissions.
  //
  list<string> getAllUsers() const;

 protected:
  set<string> mAllowed;
};
