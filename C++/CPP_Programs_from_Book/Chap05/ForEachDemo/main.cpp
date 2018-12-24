// ForEachDemo - C++ includes a form of the "for each"
//               which iterates through each member of
//               a list
#include <cstdio>
#include <cstdlib>
#include <iostream>
using namespace std;

int main(int nNumberofArgs, char* pszArgs[])
{
    cout << "The primes less than 20 are:" << endl;
    for(int n : {1, 2, 3, 5, 7, 11, 13, 17, 19})
    {
        cout << n << ", ";
    }
    cout << endl;

    // wait until user is ready before terminating program
    // to allow the user to see the program results
    cout << "Press Enter to continue..." << endl;
    cin.ignore(10, '\n');
    cin.get();
    return 0;
}
