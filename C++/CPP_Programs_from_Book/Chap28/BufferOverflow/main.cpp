// BufferOverflow - this program demonstrates how a
//                  program that reads data into a fixed
//                  length buffer without checking can be
//                  hacked
#include <cstdio>
#include <cstdlib>
#include <fstream>
#include <iostream>
#include <cstring>
#include <string>

using namespace std;

// getString - read a string of input from the user prompt and
//             return it to the caller
char* getString(istream& cin)
{
    char buffer[64];

    // now input a string from the file
    char* pB;
    for(pB = buffer;*pB = cin.get(); pB++)
    {
        if (cin.eof())
        {
            break;
        }
    }
    *pB = '\0';

    // return a copy of the string to the caller
    pB = new char[strlen(buffer) + 1];
    strcpy(pB, buffer);
    return pB;
}

int main(int argc, char* pArgv[])
{
    // get the name of the file to read
    cout <<"This program reads input from an input file\n"
           "Enter the name of the file:";
    string sName;
    cin >> sName;

    // open the file
    ifstream c(sName.c_str());
    if (!c)
    {
        cout << "\nError opening input file" << endl;
        exit(-1);
    }

    // read the file's content into a string
    char* pB = getString(c);

    // output what we got
    cout << "\nWe successfully read in:\n" << pB << endl;

    cout << "Press Enter to continue..." << endl;
    cin.ignore(10, '\n');
    cin.get();
    printf("Done!");
    exit(0);
    return 0;
}
