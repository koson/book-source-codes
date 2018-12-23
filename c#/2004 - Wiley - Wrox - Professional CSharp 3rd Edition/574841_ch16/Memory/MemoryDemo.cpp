#include "MemoryDemo.h"
#include <iostream>
using namespace std;

void* MemoryDemo::operator new(size_t size) throw(bad_alloc)
{
  cout << "operator new\n";
  return (::operator new(size));
}

void MemoryDemo::operator delete(void* ptr, size_t size) throw()
{
  cout << "operator delete with size\n";
  ::operator delete(ptr);
}

/*
void MemoryDemo::operator delete(void* ptr) throw()
{
  cout << "operator delete\n";
  ::operator delete(ptr);
}
*/

void* MemoryDemo::operator new[](size_t size) throw(bad_alloc)
{
  cout << "operator new[]\n";
  return (::operator new[](size));
}

void MemoryDemo::operator delete[](void* ptr) throw()
{
  cout << "operator delete[]\n";
  ::operator delete[](ptr);
}

void* MemoryDemo::operator new(size_t size, const nothrow_t&) throw()
{
  cout << "operator new nothrow\n";
  return (::operator new(size, nothrow));
}

void MemoryDemo::operator delete(void* ptr, const nothrow_t&) throw()
{
  cout << "operator delete nothrow\n";
  ::operator delete[](ptr, nothrow);
}

void* MemoryDemo::operator new[](size_t size, const nothrow_t&) throw()
{
  cout << "operator new[] nothrow\n";
  return (::operator new[](size, nothrow));
}

void MemoryDemo::operator delete[](void* ptr, const nothrow_t&) throw()
{
  cout << "operator delete[] nothrow\n";
  ::operator delete[](ptr, nothrow);
}

void* MemoryDemo::operator new(size_t size, int extra) throw(bad_alloc)
{
  cout << "operator new with extra int arg\n";
  return (::operator new(size));
}

