//
//  ConstructMembersWithInitializers - this program
//          demonstrates what happens when a data member
//          with an initializer is constructed
//
#include <cstdio>
#include <cstdlib>
#include <iostream>
using namespace std;

class StudentId
{
  public:
    StudentId(int id) : value(id)
    {
        cout << "id = " << value << endl;
    }

  protected:
    int value;
};

int nextStudentId = 1000;
class Student
{
  public:
    Student(const char *pName, int ssId)
      : name(pName), id(ssId)
    {
        cout << "constructing student " << pName << endl;
    }
    Student(const char *pName): name(pName)
    {
        cout << "constructing student " << pName << endl;
    }
  protected:
    string name;
    StudentId id = nextStudentId++;
};

int main(int argcs, char* pArgs[])
{
    Student s1("Jack", 1234);
    Student s2("Scruffy");

    // wait until user is ready before terminating program
    // to allow the user to see the program results
    cout << "Press Enter to continue..." << endl;
    cin.ignore(10, '\n');
    cin.get();
    return 0;
}
