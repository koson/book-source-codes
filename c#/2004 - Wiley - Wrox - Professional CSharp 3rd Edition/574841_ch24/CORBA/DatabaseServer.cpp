#include "DatabaseServer.h"
#include <iostream>
#include <fstream>
using namespace std;

const char* objRefFile = "OBJ_REF_FILE.dat";

DatabaseServer::DatabaseServer()
{
}

DatabaseServer::~DatabaseServer()
{
}

void DatabaseServer::addRecord(const char* key, const char* record)
{
  mDb[key] = record;
}

char* DatabaseServer::lookupRecord(const char* key)
{
  return (CORBA::string_dup(mDb[key].c_str()));
}

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

  // Obtain a reference to the "Portable Object Adapter" and downcast
  // it to the appropriate type
  CORBA::Object_var obj = orb->resolve_initial_references("RootPOA");
  PortableServer::POA_var poa = PortableServer::POA::_narrow(obj);
  
  // Create the DatabaseServer object and register/activate it
  // with the portable object adapter
  DatabaseServer* myDb = new DatabaseServer();
  PortableServer::ObjectId_var dbid = poa->activate_object(myDb);

  // Write a string version of the object reference to a
  // file so clients can find us 
  CORBA::Object_var dbobj = myDb->_this();
  CORBA::String_var sior(orb->object_to_string(dbobj));

  ofstream ostr(objRefFile);
  if (ostr.fail()) {
    cerr << "Unable to open object reference file for writing.\n";
    exit(1);
  } 
  ostr << (char*)sior;
  ostr.close();

  // Tell the reference counter that we're done with the object.
  // Now only the POA has a reference to it.
  myDb->_remove_ref();

  // Move the POA from holding to active state, so that it will process
  // incoming requests.
  PortableServer::POAManager_var pman = poa->the_POAManager();
  pman->activate();

  // Wait for incoming requests
  orb->run();

  // shouldn't return from the run call, but if we do, we
  // need to clean up
  orb->destroy();
  return (0);
}
