// CustomIO - develop a custom inserter and extractor
#include <cstdlib>
#include <cstdio>
#include <iostream>
#include "student.h"
using namespace std;

int main(int argc, char* pArgs[])
{
    Student s1("Davis", 123456, 3.5);
    cout << s1 << endl;

    Student s2("Eddins", 1, 3);
    cout << s2 << endl;

    cout << "Input a student object:";
    cin >> s1;
    if (cin.fail())
    {
        cout << "Error reading student " << endl;
        cin.clear();
    }
    else
    {
        cout << "Read: " << s1 << endl;
    }

    cout << "Press Enter to continue..." << endl;
    cin.ignore(10, '\n');
    cin.get();
    return 0;
}
