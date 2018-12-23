#include <stdexcept>
#include <iostream>

using namespace std;

class Base
{
public:
  virtual void func() throw(runtime_error) { cout << "Base!\n"; }
};

class Derived : public Base
{
public:
  virtual void func() throw(exception) { cout << "Derived!\n"; } // ERROR!
};

int main(int argc, char** argv)
{
  return (0);
}
