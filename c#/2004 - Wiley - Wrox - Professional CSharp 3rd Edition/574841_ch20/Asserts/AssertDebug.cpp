// STDebug.cpp
#include <exception>
#include <fstream>
#include <iostream>
#include <cassert>
using namespace std;

ofstream debugOstr;
bool debug = false;

const char* debugFileName = "debugfile.out";

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

bool isDebugSet(int argc, char** argv);
ostream& operator<<(ostream& ostr, const ComplicatedClass& src);
ostream& operator<<(ostream& ostr, const UserCommand& src);
UserCommand getNextCommand(ComplicatedClass* obj);
void processUserCommand(UserCommand& cmd);
void trickyFunction(ComplicatedClass* obj) throw(exception);

int main(int argc, char** argv)
{

  debug = isDebugSet(argc, argv);
  if (debug) {
    // open the output stream
    debugOstr.open(debugFileName);
    if (debugOstr.fail()) {
      cout << "Unable to open debug file!\n";
      return (1);
    }

    // Print the command-line arguments
    for (int i = 0; i < argc; i++) {
      debugOstr << argv[i] << " ";
      debugOstr << endl;
    }
  }

  ComplicatedClass obj;
  trickyFunction(NULL);

  // Rest of the function not shown
  return (0);
}

bool isDebugSet(int argc, char** argv)
{
  for (int i = 0; i < argc; i++) {
    if (strcmp(argv[i], "-d") == 0) {
      return (true);
    }
  }
  return (false);
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

  if (debug) {
    // If in debug mode, print the values with which this function starts
    debugOstr << "trickyFunction(): given argument: " << *obj << endl;
  }

  while (true) {
    UserCommand cmd = getNextCommand(obj);
    if (debug) {
      debugOstr << "trickyFunction(): retrieved cmd " << cmd << endl;
    }
    try {
      processUserCommand(cmd);
    } catch (exception& e) {
      if (debug) {
	debugOstr << "trickyFunction(): "
		  << " received exception from procesUserCommand(): "
		  << e.what() << endl;
      }
      throw;
    }
  }
}
