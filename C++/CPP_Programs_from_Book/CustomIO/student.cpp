#include "student.h"

ostream& operator<<(ostream& out, const Student& s)
{
    out << s.getName()
        << " (";

    // force the id to be a six digit field
    char prevFill = out.fill('0');
    out.width(6);
    out << s.getID();
    out.fill(prevFill);

    // now output the rest of the Student
    int prevPrec = out.precision(3);
    ios_base::fmtflags prev=out.setf(ios_base::showpoint);
    out << ")" << "/"  << s.getGPA();
    out.precision(prevPrec);
    out.setf(prev);
    return out;
}

istream& operator>>(istream& in, Student& s)
{
    // read values (ignore extra white space)
    string name;
    long nID;
    double dGPA;
    char openParen = 0, closedParen = 0, slash = 0;
    ios_base::fmtflags prev = in.setf(ios_base::skipws);
    in >> name
       >> openParen >> nID >> closedParen
       >> slash >> dGPA;
    in.setf(prev);

    // if the markers don't match...
    if (openParen!='(' || closedParen!=')' || slash!='/')
    {
        // ...then this isn't a legal Student
        in.setstate(ios_base::failbit);
        return in;
    }

    // try to set the student values
    try
    {
        s.setName(name);
        s.setID(nID);
        s.setGPA(dGPA);
    }
    catch (...)
    {
        // something's not right - flag the failure
        in.setstate(ios_base::failbit);
        throw;
    }
    return in;
}
