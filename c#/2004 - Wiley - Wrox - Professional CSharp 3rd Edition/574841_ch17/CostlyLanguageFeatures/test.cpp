// test.cpp
#include <iostream>
#include <exception>
using namespace std;

class base
{
public:
  base() {}
  virtual ~base() {}
};

class derived : public base {};

int main(int argc, char** argv)
{
  base* b = new derived();
  derived* d = dynamic_cast<derived*>(b); // use RTTI
  if (d == NULL) {
    throw exception(); // use exceptions
  }
  return (0);
}
