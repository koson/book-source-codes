//
//  MoveCopy  - demonstrate the principle of moving a
//              temporary rather than creating a copy
//
#include <cstdio>
#include <cstdlib>
#include <iostream>
using namespace std;

class Person
{
  public:
    Person(const char *pN)
    {
        pName = new string(pN);
        cout << "Constructing " << *pName << endl;
    }
    Person(Person& p)
    {
        cout << "Copying " << *p.pName << endl;
        pName = new string("Copy of ");
        *pName += *p.pName;
    }
    Person(Person&& p)
    {
        cout << "Moving " << *p.pName << endl;
        pName = p.pName;
        p.pName = nullptr;
    }
    ~Person()
    {
        if (pName)
        {
            cout << "Destructing " << *pName << endl;
            delete pName;
        }
        else
        {
            cout << "Destructing null object" << endl;
        }
    }

 protected:
    string* pName;
};

Person fn2(Person p)
{
    cout << "Entering fn2" << endl;
    return p;
}

Person fn1(const char* pName)
{
    cout << "Entering fn1" << endl;
    return fn2(*new Person(pName));
}

int main(int argcs, char* pArgs[])
{
    Person s(fn1("Scruffy"));

    // wait until user is ready before terminating program
    // to allow the user to see the program results
    cout << "Press Enter to continue..." << endl;
    cin.ignore(10, '\n');
    cin.get();
    return 0;
}
