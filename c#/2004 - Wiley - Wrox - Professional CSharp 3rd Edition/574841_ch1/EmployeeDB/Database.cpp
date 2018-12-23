// Database.cpp

#include <iostream>
#include <stdexcept>
#include <string>

#include "Database.h"

using namespace std;

namespace Records {

  Database::Database()
  {
    mNextSlot = 0;
    mNextEmployeeNumber = kFirstEmployeeNumber;
  }
  Database::~Database()
  {
  }
  Employee& Database::addEmployee(string inFirstName, string inLastName)
  {
    if (mNextSlot >= kMaxEmployees) {
      cerr << "There is no more room to add the new employee!" << endl;
      throw exception();
    }

    Employee& theEmployee = mEmployees[mNextSlot++];
    theEmployee.setFirstName(inFirstName);
    theEmployee.setLastName(inLastName);
    theEmployee.setEmployeeNumber(mNextEmployeeNumber++);
    theEmployee.hire();

    return theEmployee;
  }
  Employee& Database::getEmployee(int inEmployeeNumber)
  {
    for (int i = 0; i < mNextSlot; i++) {
      if (mEmployees[i].getEmployeeNumber() == inEmployeeNumber) {
	return mEmployees[i];
      }
    }

    cerr << "No employee with employee number " << inEmployeeNumber << endl;
    throw exception();
  }

  Employee& Database::getEmployee(string inFirstName, string inLastName)
  {
    for (int i = 0; i < mNextSlot; i++) {
      if (mEmployees[i].getFirstName() == inFirstName &&
	  mEmployees[i].getLastName() == inLastName) {
	return mEmployees[i];
      }
    }

    cerr << "No match with name " << inFirstName << " " << inLastName << endl;
    throw exception();
  }
  void Database::displayAll()
  {
    for (int i = 0; i < mNextSlot; i++) {
      mEmployees[i].display();
    }
  }

  void Database::displayCurrent()
  {
    for (int i = 0; i < mNextSlot; i++) {
      if (mEmployees[i].getIsHired()) {
	mEmployees[i].display();
      }
    }
  }

  void Database::displayFormer()
  {
    for (int i = 0; i < mNextSlot; i++) {
      if (!mEmployees[i].getIsHired()) {
	mEmployees[i].display();
      }
    }
  }
}
