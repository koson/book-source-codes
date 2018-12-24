// MaxTemplate - create a template max() function
//               that returns the greater of two types
#include <cstdio>
#include <cstdlib>
#include <iostream>

using namespace std;

template <class T> T maximum(T t1, T t2)
{
    return (t1 > t2) ? t1 : t2;
}

int main(int argc, char* pArgs[])
{
    // find the maximum of two int's;
    // here C++ creates maximum(int, int)
    cout << "maximum(-1, 2) = "<<maximum(-1, 2) << endl;

    // repeat for two doubles;
    // in this case, we have to provide T explicitly since
    // the types of the arguments are different
    cout << "maximum(1, 2.5) = "<<maximum<double>(1, 2.5)
         << endl;

    cout << "Press Enter to continue..." << endl;
    cin.ignore(10, '\n');
    cin.get();
    return 0;
}
