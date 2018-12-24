// FileCopy - make backup copies of the files passed
//            to the program
#include <cstdio>
#include <cstdlib>
#include <fstream>
#include <iostream>
using namespace std;

int main(int nNumberofArgs, char* pszArgs[])
{
    // repeat the process for every file passed
    for (int n = 1; n < nNumberofArgs; n++)
    {
        // create a filename and a ".backup" name
        string szSource(pszArgs[n]);
        string szTarget = szSource + ".backup";

        // now open the source for reading and the
        // target for writing
        ifstream input(szSource.c_str(),
                       ios_base::in|ios_base::binary);

        ofstream output(szTarget.c_str(),
          ios_base::out|ios_base::binary|ios_base::trunc);
        if (input.good() && output.good())
        {
            cout << "Backing up " << szSource << "...";

            // read and write 4k blocks until either an
            // error occurs or the file reaches EOF
            while(!input.eof() && input.good())
            {
                char buffer[4096];
                input.read(buffer, 4096);
                output.write(buffer, input.gcount());
            }
            cout << "finished" << endl;
        }
        else
        {
            cerr << "Couldn't copy " << szSource << endl;
        }
    }

    cout << "Press Enter to continue..." << endl;
    cin.ignore(10, '\n');
    cin.get();
    return 0;
}
