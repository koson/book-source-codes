// Database.h

#include <iostream>
#include "Employee.h"

namespace Records {

  const int kMaxEmployees = 100;
  const int kFirstEmployeeNumber = 1000;
    class Database
      {
      public:
	Database();
	~Database();

	Employee& addEmployee(std::string inFirstName, std::string inLastName);
	Employee& getEmployee(int inEmployeeNumber);
	Employee& getEmployee(std::string inFirstName, std::string inLastName);
	void        displayAll();
	void        displayCurrent();
	void        displayFormer();
      protected:
	Employee    mEmployees[kMaxEmployees];
	int         mNextSlot;
	int         mNextEmployeeNumber;
      };
}
