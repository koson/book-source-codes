// CascadingException - the following program demonstrates
//              an example of stack unwinding
#include <cstdio>
#include <cstdlib>
#include <iostream>
using namespace std;

// prototypes of some functions that we will need later
void f1();
void f2();
void f3();

class Obj
{
  public:
    Obj(char c) : label(c)
    { cout << "Constructing object " << label << endl;}
    ~Obj()
    { cout << "Destructing object " << label << endl; }

  protected:
    char label;
};

int main(int nNumberofArgs, char* pszArgs[])
{
    f1();

    // wait until user is ready before terminating program
    // to allow the user to see the program results
    cout << "Press Enter to continue..." << endl;
    cin.ignore(10, '\n');
    cin.get();
    return 0;
}

void f1()
{
    Obj a('a');
    try
    {
        Obj b('b');
        f2();
    }
    catch(float f)
    {
        cout << "Float catch" << endl;
    }
    catch(int i)
    {
        cout << "Int catch" << endl;
    }
    catch(...)
    {
        cout << string("Generic catch") << endl;
    }
}

void f2()
{
    try
    {
        Obj c('c');
        f3();
    }
    catch(string msg)
    {
        cout << "String catch" << endl;
    }
}

void f3()
{
    Obj d('d');
    throw 10;
}
