#include <map>
#include <string>
#include <stdexcept>
using std::map;
using std::string;
using std::out_of_range;

class BankAccount
{
 public:
  BankAccount(int acctNum, const string& name) :
    mAcctNum(acctNum), mClientName(name) {}
  void setAcctNum(int acctNum) { mAcctNum = acctNum; }
  int getAcctNum() const {return (mAcctNum); }
  void setClientName(const string& name) { mClientName = name; }
  string getClientName() const { return mClientName; }

  // Other public methods omitted

 protected:
  int mAcctNum;
  string mClientName;
  // other data members omitted
};

class BankDB
{
 public:
  BankDB() {}

  // Adds acct to the bank database. If an account
  // exists already with that number, the new account is
  // not added. Returns true if the account is added, false
  // if it's not.
  bool addAccount(const BankAccount& acct);

  // Removes the account acctNum from the database.
  void deleteAccount(int acctNum);

  // Returns a reference to the account represented
  // by its number or the client name.
  // Throws out_of_range if the account is not found
  BankAccount& findAccount(int acctNum) throw(out_of_range);
  BankAccount& findAccount(const string& name) throw(out_of_range);

  // Adds all the accounts from db to this database.
  // Deletes all the accounts in db.
  void mergeDatabase(BankDB& db);

 protected:
  map<int, BankAccount> mAccounts;
};

