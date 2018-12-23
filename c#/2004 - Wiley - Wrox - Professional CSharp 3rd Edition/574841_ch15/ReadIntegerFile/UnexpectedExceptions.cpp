#include <fstream>
#include <iostream>
#include <vector>
#include <string>
#include <stdexcept>

using namespace std;

void readIntegerFile(const string& fileName, vector<int>& dest)
  throw (invalid_argument, runtime_error)
{
  throw (5);
}

void myUnexpected()
{
  cout << "Unexpected exception!\n";
  throw runtime_error("");
}

int main(int argc, char** argv)
{
  vector<int> myInts;
  const string fileName = "IntegerFile.txt";

  unexpected_handler old_handler = set_unexpected(myUnexpected);
  try {
    readIntegerFile(fileName, myInts);
  } catch (const invalid_argument& e) {
    cerr << "Unable to open file " << fileName << endl;
    exit (1);
  } catch (const runtime_error& e) {
    cerr << "Error reading file " << fileName << endl;
    exit (1);
  } catch (int x) {
    cout << "Caught int\n";
  }
  set_unexpected(old_handler);

  for (size_t i = 0; i < myInts.size(); i++) {
    cout << myInts[i] << " ";
  }
  cout << endl;

  return (0);
}
