// CTDebug.cpp
#include <exception>
#include <fstream>
#include <iostream>
using namespace std;

#ifdef DEBUG_MODE
ofstream debugOstr;

const char* debugFileName = "debugfile.out";
#endif

class ComplicatedClass
{
public:
  ComplicatedClass() {}

  // class details omitted for brevity
};

class UserCommand
{
public:
  UserCommand() {}

  // class details not shown for brevity
};

ostream& operator<<(ostream& ostr, const ComplicatedClass& src);
ostream& operator<<(ostream& ostr, const UserCommand& src);
UserCommand getNextCommand(ComplicatedClass* obj);
void processUserCommand(UserCommand& cmd);
void trickyFunction(ComplicatedClass* obj) throw(exception);

int main(int argc, char** argv)
{
#ifdef DEBUG_MODE
  // open the output stream
  debugOstr.open(debugFileName);
  if (debugOstr.fail()) {
    cout << "Unable to open debug file!\n";
    return (1);
  }

  // Print the command-line arguments to the trace
  for (int i = 0; i < argc; i++) {
    debugOstr << argv[i] << " ";
    debugOstr << endl;
  }
#endif

  ComplicatedClass obj;
  trickyFunction(&obj);
  // Rest of the function not shown
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
#ifdef DEBUG_MODE
  // If in debug mode, print the values with which this function starts
  debugOstr << "trickyFunction(): given argument: " << *obj << endl;
#endif

  while (true) {
    UserCommand cmd = getNextCommand(obj);

#ifdef DEBUG_MODE
    debugOstr << "trickyFunction(): retrieved cmd " << cmd << endl;
#endif
    try {
      processUserCommand(cmd);
    } catch (exception& e) {
#ifdef DEBUG_MODE
      debugOstr << "trickyFunction(): "
                << " received exception from procesUserCommand(): "
                << e.what() << endl;
#endif
      throw;
    }
  }
}
