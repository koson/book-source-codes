// StreamInput - simple input from a file using fstream
#include <cstdio>
#include <cstdlib>
#include <fstream>
#include <iostream>
using namespace std;

ifstream& openFile()
{
    ifstream* pFileStream = 0;
    for(;;)
    {
        // open the file specified by the user
        string sFileName;
        cout << "Enter the name of a file with integers:";
        cin >> sFileName;

        //open file for reading
        pFileStream = new ifstream(sFileName.c_str());
        if (pFileStream->good())
        {
            pFileStream->seekg(0);
            cerr << "Successfully opened "
                 << sFileName << endl;
            break;
        }
        cerr << "Couldn't open " << sFileName << endl;
        delete pFileStream;
    }
    return *pFileStream;
}

int main(int nNumberofArgs, char* pszArgs[])
{
    // get a file stream
    ifstream& fileStream = openFile();

    // stop when no more data in file
    while (!fileStream.eof())
    {
        // read a value
        int nValue = 0;
        fileStream >> nValue;

        // stop if the file read failed (probably because
        // we ran upon something that's not an int or
        // because we found a newline with nothing after
        // it)
        if (fileStream.fail())
        {
            break;
        }

        // output the value just read
        cout << nValue << endl;
    }

    cout << "Press Enter to continue..." << endl;
    cin.ignore(10, '\n');
    cin.get();
    return 0;
}
