#ifndef STUDENT_H
#define STUDENT_H
#include <iostream>
#include <string>
using namespace std;
class Student
{
  public:
    explicit Student(const char* pszName = "",
                     long  id = 0,
                     double gpa = 0.0) : sName(pszName),
                                         nID(id),
                                         dGPA(gpa) {}
    string getName() const { return sName; }
    void setName(string& s) { sName = s; }

    long getID() const { return nID; }
    void setID(long nID) { this->nID = nID; }

    double getGPA() const { return dGPA; }
    void setGPA(double dGPA) { this->dGPA = dGPA; }

  protected:
    string sName;
    long nID;
    double dGPA;
};

ostream& operator<<(ostream& out, const Student& s);
istream& operator>>(istream& in, Student& s);

#endif // STUDENT_H
