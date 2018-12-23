#include "BankDB.h"
#include <iostream>
using namespace std;

int main(int argc, char** argv)
{
  BankDB db;

  db.addAccount(BankAccount(100, "Nicholas Solter"));
  db.addAccount(BankAccount(200, "Scott Kleper"));

  try {
    BankAccount &acct = db.findAccount(100);
    cout << "Found account 100\n";
    acct.setClientName("Nicholas A Solter");
    BankAccount &acct2 = db.findAccount("Scott Kleper");
    cout << "Found account of Scott Kelper\n";
    BankAccount &acct3 = db.findAccount(1000);
  } catch (out_of_range &) {
    cout << "Unable to find account\n";
  }

  return (0);
}
