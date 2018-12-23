#include "database.hh"
#include <map>
#include <string>

class DatabaseServer : public POA_database,
		       public PortableServer::RefCountServantBase
{
public:
  DatabaseServer();
  virtual ~DatabaseServer();
  virtual void addRecord(const char* key, const char* record);
  virtual char* lookupRecord(const char* key);

protected:
  std::map<std::string, std::string> mDb;
};
