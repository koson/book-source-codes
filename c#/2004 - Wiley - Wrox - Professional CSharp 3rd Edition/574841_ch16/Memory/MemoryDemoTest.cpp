#include "MemoryDemo.h"

int main(int argc, char** argv)
{
  MemoryDemo* mem = new MemoryDemo();
  delete mem;

  mem = new MemoryDemo[10];
  delete [] mem;

  mem = new (nothrow) MemoryDemo();
  delete mem;

  mem = new (nothrow) MemoryDemo[10];
  delete [] mem;

  int x = 5;
  MemoryDemo* memp = new(5) MemoryDemo();
  delete memp;

  return (0);
}
