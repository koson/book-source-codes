//
//  ConstructStatic - demonstrate that statics are only
//                    constructed once
//
#include <cstdio>
#include <cstdlib>
#include <iostream>
using namespace std;

class DoNothing
{
  public:
    DoNothing(int initial) : nValue(initial)
    {
        cout << "DoNothing constructed with a value of "
             << initial << endl;
    }
    ~DoNothing()
    {
        cout << "DoNothing object destructed" << endl;
    }
    int nValue;
};
void fn(int i)
{
    cout << "Function fn passed a value of " << i << endl;
    static DoNothing dn(i);
}

int main(int argcs, char* pArgs[])
{
    fn(10);
    fn(20);
    cout << "Press Enter to continue..." << endl;
    cin.ignore(10, '\n');
    cin.get();
    return 0;
}
