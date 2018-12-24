// LayoutError - demonstrate the results of
//               messing up a pointer
#include <cstdio>
#include <cstdlib>
#include <iostream>
using namespace std;

int main(int nNumberofArgs, char* pszArgs[])
{
    int    array[3] = {0, 13, 0};

    // output the values of the three variables before...
    cout << "The initial values are" << endl;
    cout << "array[0] = " << array[0] << endl;
    cout << "array[1] = " << array[1] << endl;
    cout << "array[2] = " << array[2] << endl;

    // now store a double into the space
    // allocated for an int
    cout << "\nStoring 13.33 into the location &array[1]"
         <<endl;
    double* pD = (double*)&(array[1]);
    *pD = 13.33;

    // display the results
    cout << "\nThe final results are:" << endl;
    cout << "array[0] = " << array[0] << endl;
    cout << "array[1] = " << array[1] << endl;
    cout << "array[2] = " << array[2] << endl;

    // wait until user is ready before terminating program
    // to allow the user to see the program results
    cout << "Press Enter to continue..." << endl;
    cin.ignore(10, '\n');
    cin.get();
    return 0;
}
