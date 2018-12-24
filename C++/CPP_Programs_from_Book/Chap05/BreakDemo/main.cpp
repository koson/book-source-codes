// BreakDemo - input a series of numbers.
//             Continue to accumulate the sum
//             of these numbers until the user
//             enters a negative number.
#include <cstdio>
#include <cstdlib>
#include <iostream>
using namespace std;

int main(int nNumberofArgs, char* pszArgs[])
{
    // input the loop count
    int accumulator = 0;
    cout << "This program sums values from the user\n"
         << "Terminate by entering a negative number"
         << endl;

    // loop "forever"
    for(;;)
    {
        // fetch another number
        int nValue = 0;
        cout << "Enter next number: ";
        cin  >> nValue;

        // if it's negative...
        if (nValue < 0)
        {
            // ...then exit
            break;
        }

        // ...otherwise add the number to the accumulator
        accumulator += nValue;
    }

    // now that we've exited the loop
    // output the accumulated result
    cout << "\nThe total is "
         << accumulator
         << endl;

    // wait until user is ready before terminating program
    // to allow the user to see the program results
    cout << "Press Enter to continue..." << endl;
    cin.ignore(10, '\n');
    cin.get();
    return 0;
}
