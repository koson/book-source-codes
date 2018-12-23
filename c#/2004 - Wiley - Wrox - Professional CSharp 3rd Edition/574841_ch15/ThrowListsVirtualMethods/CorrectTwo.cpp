#include <stdexcept>
#include <iostream>

using namespace std;

class Base
{
public:
  virtual void func() throw(exception) { cout << "Base!\n"; }
};

class Derived : public Base
{
public:
  virtual void func() throw(exception, runtime_error)
  { cout << "Derived!\n"; }
};

int main(int argc, char** argv)
{
  return (0);
}
