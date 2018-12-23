#include <list>
#include <string>
#include <iostream>
using namespace std;

int main(int argc, char** argv)
{
  list<string> dictionary, bWords;

  // Add the a words
  dictionary.push_back("aardvark");
  dictionary.push_back("ambulance");
  dictionary.push_back("archive");

  // Add the c words
  dictionary.push_back("canticle");
  dictionary.push_back("consumerism");
  dictionary.push_back("czar");

  // create another list of the b words
  bWords.push_back("bathos");
  bWords.push_back("balderdash");
  bWords.push_back("brazen");

  // splice the b words into the main dictionary.
  list<string>::iterator it;
  int i;

  // Iterate up to the spot where we want to insert bs
  // for loop body intentionally empty -- we're just moving up three elements
  for (it = dictionary.begin(), i = 0; i < 3; ++it, ++i);

  // Add in the bwords. This action removes the elements from bWords.
  dictionary.splice(it, bWords);

  // print out the dictionary
  for (it = dictionary.begin(); it != dictionary.end(); ++it) {
    cout << *it << endl;
  }

  return (0);
}
