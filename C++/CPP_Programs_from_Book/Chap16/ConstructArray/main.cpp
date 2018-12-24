//
//  ConstructArray - construct an array of objects
//
#include <cstdio>
#include <cstdlib>
#include <iostream>
#include <string.h>

using namespace std;
class Student
{
  public:
    Student(const char *pName)
    {
        cout << "constructing freshman " << pName << endl;
        name = pName;
        semesterHours = 0;
        gpa = 0;
    }
    Student(const char *pName, int xfrHours,double xfrGPA)
    {
        cout << "constructing transfer " << pName << endl;
        name = pName;
        semesterHours = xfrHours;
        gpa = xfrGPA;
    }

  protected:
    string  name;
    int     semesterHours;
    double  gpa;
};

int main(int argcs, char* pArgs[])
{
    // the following invokes three different constructors
    Student s[]{"Marian Haste", "Pikup Andropov"};
    Student t[]{{"Jack", 0, 0.0}, {"Scruffy", 12, 2.5}};

    // wait until user is ready before terminating program
    // to allow the user to see the program results
    cout << "Press Enter to continue..." << endl;
    cin.ignore(10, '\n');
    cin.get();
    return 0;
}
