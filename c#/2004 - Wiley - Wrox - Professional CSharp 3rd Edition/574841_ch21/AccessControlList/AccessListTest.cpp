#include "AccessList.h"
#include <iostream>
#include <iterator>
using namespace std;

int main(int argc, char** argv)
{
  AccessList fileX;

  fileX.addUser("nsolter");
  fileX.addUser("klep");
  fileX.addUser("baduser");
  fileX.removeUser("baduser");

  if (fileX.isAllowed("nsolter")) {
    cout << "nsolter has permissions\n";
  }

  if (fileX.isAllowed("baduser")) {
    cout << "baduser has permissions\n";
  }

  list<string> users = fileX.getAllUsers();
  for (list<string>::const_iterator it = users.begin();
       it != users.end(); ++it) {
    cout << *it << " ";
  }
  cout << endl;

  return (0);
}
