// NamespaceExample
#include <cstdio>
#include <cstdlib>
#include <iostream>

#include <cmath>
using namespace std;

namespace Mathematics
{
    double log(double x)
    {
        return log10(x);
    }
}
namespace SystemLog
{
    int log(double x)
    {
        cout << "Logged " << x << endl;
        return 0;
    }
}

int main(int nNumberofArgs, char* pszArgs[])
{
    double dl = Mathematics::log(10);
    SystemLog::log(dl);

    // wait until user is ready before terminating program
    // to allow the user to see the program results
    cout << "Press Enter to continue..." << endl;
    cin.ignore(10, '\n');
    cin.get();
    return 0;
}

