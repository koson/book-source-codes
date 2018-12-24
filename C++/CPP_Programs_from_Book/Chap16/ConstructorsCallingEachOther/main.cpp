//
//  ConstructorsCallingEachOther - new for 2011,
//          one constructor can invoke another constructor
//          in the same class
//
#include <cstdio>
#include <cstdlib>
#include <iostream>
using namespace std;

class Student

{
  public:
    Student(const char *pName,
            int xfrHours,
            double xfrGPA)
    {
        cout << "constructing student " << pName << endl;
        name = pName;
        semesterHours = xfrHours;
        gpa = xfrGPA;
    }
    Student() : Student("No Name", 0, 0.0) {}
    Student(const char *pName): Student(pName, 0, 0.0){}

  protected:
    string  name;
    int     semesterHours;
    double  gpa;
};

int main(int argcs, char* pArgs[])
{
    // the following invokes three different constructors
    Student noName;
    Student freshman("Marian Haste");
    Student xferStudent("Pikup Andropov", 80, 2.5);

    // wait until user is ready before terminating program
    // to allow the user to see the program results
    cout << "Press Enter to continue..." << endl;
    cin.ignore(10, '\n');
    cin.get();
    return 0;
}
