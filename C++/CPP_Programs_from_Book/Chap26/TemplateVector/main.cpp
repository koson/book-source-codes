// TemplateVector - implement a vector that uses a
//                  template type
#include <cstdlib>
#include <cstdio>
#include <iostream>
#include "templatevector.h"
using namespace std;

// intFn() - manipulate a collection of integers
void intFn()
{
    // create a vector of integers
    TemplateVector<int> integers(10);

    // add values to the vector
    cout << "Enter integer values to add to a vector\n"
         << "(Enter a negative number to terminate):"
         << endl;
    for(;;)
    {
        int n;
        cin  >> n;

        if (n < 0) { break; }
        integers.add(n);
    }

    cout << "\nHere are the numbers you entered:" << endl;
    for(int i = 0; i < integers.size(); i++)
    {
        cout << i << ":" << integers.get() << endl;
    }
}

// Names - create and manipulate a vector of names
class Name
{
  public:
    Name() = default;
    Name(string s) : name(s) {}
    const string& display() { return name; }
  protected:
    string name;
};

void nameFn()
{
    // create a vector of Name objects
    TemplateVector<Name> names(20);

    // add values to the vector
    cout << "Enter names to add to a second vector\n"
         << "(Enter an 'x' to quit):" << endl;
    for(;;)
    {
        string s;
        cin >> s;
        if (s == "x" || s == "X") { break; }
        names.add(Name(s));
    }

    cout << "\nHere are the names you entered" << endl;
    for(int i = 0; i < names.size(); i++)
    {
        Name& name = names.get();
        cout << i << ":" << name.display() << endl;
    }
}

int main(int argc, char* pArgs[])
{
    intFn();
    nameFn();

    cout << "Press Enter to continue..." << endl;
    cin.ignore(10, '\n');
    cin.get();
    return 0;
}
