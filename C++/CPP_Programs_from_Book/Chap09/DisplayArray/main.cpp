// DisplayArray - display the contents of an array using
//                pointer arithmetic
#include <cstdio>
#include <cstdlib>
#include <iostream>
using namespace std;

// displayArray - display the members of an
//                array of length nSize
void displayArray(int intArray[], int nSize)
{
    cout << "The value of the array is:\n";

    // initialize the pointer pArray with the
    // the address of the array intArray
    int* pArray = intArray;
    for(int n = 0; n < nSize; n++, pArray++)
    {
        cout << n << ": " << *pArray << "\n";
    }
    cout << endl;
}

int main(int nNumberofArgs, char* pszArgs[])
{
    int array[] = {4, 3, 2, 1};
    displayArray(array, 4);

    // wait until user is ready before terminating program
    // to allow the user to see the program results
    cout << "Press Enter to continue..." << endl;
    cin.ignore(10, '\n');
    cin.get();
    return 0;
}

