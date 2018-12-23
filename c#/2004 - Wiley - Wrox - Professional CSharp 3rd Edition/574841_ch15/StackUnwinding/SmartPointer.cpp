#include <fstream>
#include <iostream>
#include <stdexcept>
using namespace std;

void funcOne() throw(exception);
void funcTwo() throw(exception);

int main(int argc, char** argv)
{
  try {
    funcOne();
  } catch (exception& e) {
    cerr << "Exception caught!\n";
    exit(1);
  }

  return (0);
}

#include <memory>
using namespace std;

void funcOne() throw(exception)
{
  string str1;
  auto_ptr<string> str2(new string("hello"));
  funcTwo();
}

void funcTwo() throw(exception)
{
  ifstream istr;
  istr.open("filename");
  throw exception();
  istr.close();
}
