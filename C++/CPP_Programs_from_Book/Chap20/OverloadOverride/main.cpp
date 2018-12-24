//
//  OverloadOverride - demonstrate when a function is
//     overloaded at compile time vs. overriden at runtime
//
#include <cstdio>
#include <cstdlib>
#include <iostream>
using namespace std;

class Student
{
  public:
    // uncomment one or the other of the next
    // two lines; one binds calcTuition() early and
    // the other late
//          void calcTuition()
    virtual void calcTuition()
    {
        cout << "We're in Student::calcTuition" << endl;
    }
};

class GraduateStudent : public Student
{
  public:
    void calcTuition()
    {
      cout<<"We're in GraduateStudent::calcTuition"<<endl;
    }
};

void fn(Student& x)
{
    x.calcTuition(); // which calcTuition()?
}

int main(int nNumberofArgs, char* pszArgs[])
{
    // pass a base class object to function
    // (to match the declaration)
    Student s;
    fn(s);

    // pass a specialization of the base class instead
    GraduateStudent gs;
    fn(gs);

    // wait until user is ready before terminating program
    // to allow the user to see the program results
    cout << "Press Enter to continue..." << endl;
    cin.ignore(10, '\n');
    cin.get();
    return 0;
}
