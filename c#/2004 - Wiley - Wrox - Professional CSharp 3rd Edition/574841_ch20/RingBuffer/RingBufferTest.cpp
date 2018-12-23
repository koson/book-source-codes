#include "RingBuffer.h"
#include <exception>
#include <fstream>
#include <iostream>
#include <cassert>
#include <sstream>
using namespace std;

RingBuffer debugBuf;

class ComplicatedClass
{
public:
  ComplicatedClass() {}
  ~ComplicatedClass() {}
};

class UserCommand
{
public:
  UserCommand() {}
};

ostream& operator<<(ostream& ostr, const ComplicatedClass& src);
ostream& operator<<(ostream& ostr, const UserCommand& src);
UserCommand getNextCommand(ComplicatedClass* obj);
void processUserCommand(UserCommand& cmd);
void trickyFunction(ComplicatedClass* obj) throw(exception);

int main(int argc, char** argv)
{
  // Print the command-line arguments
  for (int i = 0; i < argc; i++) {
    debugBuf.addEntry(argv[i]);
  }

  trickyFunction(new ComplicatedClass());

  // Print the current contents of the debug buffer to cout
  cout << debugBuf;

  return (0);
}

ostream& operator<<(ostream& ostr, const ComplicatedClass& src)
{
  ostr << "ComplicatedClass";
  return (ostr);
}

ostream& operator<<(ostream& ostr, const UserCommand& src)
{
  ostr << "UserCommand";
  return (ostr);
}

UserCommand getNextCommand(ComplicatedClass* obj)
{
  UserCommand cmd;
  return (cmd);
}

void processUserCommand(UserCommand& cmd)
{
  // details omitted for brevity
}

void trickyFunction(ComplicatedClass* obj) throw(exception)
{
  assert(obj != NULL);

  // trace log the values with which this function starts
  ostringstream ostr;
  ostr << "trickyFunction(): given argument: " << *obj;
  debugBuf.addEntry(ostr.str());

  while (true) {
    UserCommand cmd = getNextCommand(obj);

    ostringstream ostr;
    ostr << "trickyFunction(): retrieved cmd " << cmd;
    debugBuf.addEntry(ostr.str());

    try {
      processUserCommand(cmd);
    } catch (exception& e) {
      string msg = "trickyFunction(): received exception from procesUserCommand():";
      msg += e.what();
      debugBuf.addEntry(msg);
      throw;
    }
    break;
  }
}
