// DemoMoveOperator - demonstrate the move operator
#include <cstdio>
#include <cstdlib>
#include <iostream>
#include <cstring>

using namespace std;
class MyContainer
{
  public:
    MyContainer(int nS, const char* pS) : nSize(nS)
    {
        pString = new char[nSize];
        strcpy(pString, pS);
    }
    ~MyContainer()
    {
        delete pString;
        pString = nullptr;
    }

    //copy constructor
    MyContainer(const MyContainer& s)
    {
        copyIt(*this, s);
    }
    MyContainer& operator=(MyContainer& s)
    {
        delete pString;
        copyIt(*this, s);
        return *this;
    }

    // move constructor
    MyContainer(MyContainer&& s)
    {
        moveIt(*this, s);
    }
    MyContainer& operator=(MyContainer&& s)
    {
        delete pString;
        moveIt(*this, s);
        return *this;
    }

  protected:
    static void moveIt(MyContainer& tgt, MyContainer& src)
    {
        cout << "Moving " << src.pString << endl;
        tgt.nSize = src.nSize;
        tgt.pString = src.pString;
        src.nSize = 0;
        src.pString = nullptr;
    }
    static void copyIt(      MyContainer& tgt,
                       const MyContainer& src)
    {
        cout << "Copying " << src.pString << endl;
        delete tgt.pString;
        tgt.nSize = src.nSize;
        tgt.pString = new char[tgt.nSize];
        strncpy(tgt.pString, src.pString, tgt.nSize);
    }
    int nSize;
    char* pString;
};

MyContainer fn(int n, const char* pString)
{
    MyContainer b(n, pString);
    return b;
}

int main(int nNumberofArgs, char* pszArgs[])
{
    MyContainer mc(100, "Original");

    mc = fn(100, "Created in fn()");

    // wait until user is ready before terminating program
    // to allow the user to see the program results
    cout << "Press Enter to continue..." << endl;
    cin.ignore(10, '\n');
    cin.get();
    return 0;
}
