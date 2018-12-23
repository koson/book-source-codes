#include <iostream>
#include <fstream>

using namespace std;

int main(int argc, char** argv)
{
  ofstream fout("test.out");

  // 1. output the string "12345"
  fout << "12345";

  // 2. verify that the marker is at the end
  long curPos = fout.tellp();
  if (curPos == 5) {
    cout << "Test passed: Currently at position 5" << endl;
  } else {
    cout << "Test failed: Not at position 5" << endl;
  }

  // 3. move to position 2 in the stream
  fout.seekp(2, ios_base::beg);

  // 4. output a 0 in position 2
  fout << 0;
  fout.flush();

  // 5. open an input stream on test.out
  ifstream fin("test.out");

  // 6. read the first token as an integer
  int testVal;
  fin >> testVal;

  // 7. confirm that the value is 12045
  if (testVal == 12045) {
    cout << "Test passed: Value is 12045" << endl;
  } else {
    cout << "Test failed: Value is not 12045 (it was " << testVal << ")" << endl;
  }
}
