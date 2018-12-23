#include <iostream>
#include <fstream>
#include <string>

using namespace std;

void changeNumberForID(const string& inFileName, int inID,
		       const string& inNewNumber);

int main(int argc, char** argv)
{
  changeNumberForID("data", 263, "415-555-3333");
}

void changeNumberForID(const string& inFileName, int inID,
		       const string& inNewNumber)
{
  fstream ioData(inFileName.c_str());
  if (!ioData) {
    cerr << "Error while opening file " << inFileName << endl;
    exit(1);
  }

  // loop Loop until the end of file
  while (ioData.good()) {
    int id;
    string number;

    // read Read the next idID.
    ioData >> id;

    // check Check to see if the current record is the one being changed.
    if (id == inID) {
      // move the output pointer to the current position
	  ioData.seekp(ioData.tellg());
      // output Output a space, then the new number.
      ioData << " " << inNewNumber;
      break;
    }

    // read Read the current number to advance the stream.
    ioData >> number;
  }
}
