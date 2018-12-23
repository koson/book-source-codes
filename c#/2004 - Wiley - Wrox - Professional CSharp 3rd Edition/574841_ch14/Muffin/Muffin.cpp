#include <iostream>
#include <sstream>
#include <string>

using namespace std;

class Muffin 
{
    public:
        string    getDescription() const;
        void      setDescription(const string& inDescription);
        int       getSize() const;
        void      setSize(int inSize);
        bool      getHasChocolateChips() const;
        void      setHasChocolateChips(bool inChips);

        void output();
    protected:
        string    mDescription;
        int       mSize;
        bool      mHasChocolateChips;
};

string Muffin::getDescription() const { return mDescription; }
void Muffin::setDescription(const string& inDescription) { mDescription = inDescription; }
int Muffin::getSize() const { return mSize; }
void Muffin::setSize(int inSize) { mSize = inSize; }
bool Muffin::getHasChocolateChips() const { return mHasChocolateChips; }
void Muffin::setHasChocolateChips(bool inChips) { mHasChocolateChips = inChips; }

void Muffin::output()
{
  printf("%s, Size is %d, %s\n", getDescription().c_str(), getSize(),    
	 (getHasChocolateChips() ? "has chips" : "no chips"));
}

Muffin createMuffin(istringstream& inStream);

int main(int argc, char** argv)
{
  cout << "First, let's create a muffin in code and output it." << endl;

  Muffin m;
  m.setDescription("Giant_Blueberry_Muffin");
  m.setSize(42);
  m.setHasChocolateChips(false);

  m.output();

  cout << "Now we'll create a muffin from a string stream" << endl;

  istringstream instream("My_Muffin 2 true");
  Muffin m2 = createMuffin(instream);

  m2.output();
}

Muffin createMuffin(istringstream& inStream)
{
  Muffin muffin;
  // assume Assume data is properly formatted:
  // description Description size chips

  string description;
  int size;
  bool hasChips;

  // Read all three values. Note that chips is represented
  // by the strings "true" and "false"
  inStream >> description >> size >> boolalpha >> hasChips;
  muffin.setSize(size);
  muffin.setDescription(description);
  muffin.setHasChocolateChips(hasChips);

  return muffin;
}
