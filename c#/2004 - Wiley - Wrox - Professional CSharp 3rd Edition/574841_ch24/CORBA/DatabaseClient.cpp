#include "database.hh"
#include <iostream>
#include <fstream>
using namespace std;

const char* objRefFile = "OBJ_REF_FILE.dat";

int main(int argc, char** argv)
{
  // Try to initialize the orb
  CORBA::ORB_var orb;
  try {
    orb = CORBA::ORB_init(argc, argv);
  } catch(CORBA::SystemException&) {
    cerr << "Unable to initialize the ORB\n";
    exit(1);
  }

  // Read the server object reference from the file
  ifstream istr(objRefFile);
  if (istr.fail()) {
    cerr << "No object reference file!\n";
    exit(1);
  }
  char objRef[1024];
  istr.getline(objRef, 1024);

  // Construct an object reference from the string 
  database_var dbref;
  try {
    CORBA::Object_var obj = orb->string_to_object(objRef);
    dbref = database::_narrow(obj);
    if(CORBA::is_nil(dbref) ) {
      cerr << "Can't narrow reference to type database\n";
      exit (1);
    }
  } catch(CORBA::SystemException&) {
    cerr << "Unable to find the object reference\n";
  }

  // Make calls on the object reference, which are translated to
  // calls on the server object in the server process
  try { 
    dbref->addRecord("key1", "value1");
    const char* lookup = dbref->lookupRecord("key1");
    if (strcmp(lookup, "value1") == 0) {
      cout << "Success!\n";
    } else {
      cout << "strings don't match\n";
    }
  }
  catch(CORBA::COMM_FAILURE& ex) {
    cerr << "Communication error\n";
    exit(1);
  } catch(CORBA::SystemException&) {
    cerr << "Communication error (SystemException)\n";
    exit(1);
  }

  // We're done
  orb->destroy();

  return (0);
}
