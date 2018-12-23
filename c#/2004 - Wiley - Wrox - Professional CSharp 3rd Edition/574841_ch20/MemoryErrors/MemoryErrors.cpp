#include <cstdlib>
#include <iostream>
using namespace std;

void memoryLeak()
{
    int* ip = new int[1000];
    return; // Bug! Not freeing ip.
}

void mismatchedFree()
{
    int* ip1 = (int *)malloc(sizeof(int));
    int* ip2 = new int;
    int* ip3 = new int[1000];
    delete ip1; // BUG! Should use free
    delete [] ip2; // BUG! Should use delete
    free (ip3); // BUG! Should use delete []
}

void doubleFree()
{
    int* ip1 = new int[1000];
    delete [] ip1;
    int* ip2 = new int[1000];
    delete [] ip1; // BUG! freeing ip1 twice
}

void freeUnallocated()
{
    int* ip1 =
        reinterpret_cast<int*>(10000);    // BUG! ip1 is not a valid pointer.
    delete ip1;
}

void freeStack()
{
    int x;
    int* ip = &x;
    delete ip; // BUG! Freeing stack memory
}

void accessInvalid()
{
    int* ip1 =
        reinterpret_cast<int*>(10000);    // BUG! ip1 is not a valid pointer
    *ip1 = 5;
}

void accessFreed()
{
    int* ip1 = new int;
    delete ip1;
    int* ip2 = new int;
    // BUG! The memory pointed to by ip1
    // has been freed
    *ip1 = 5;
}

void accessElsewhere()
{
    int x, y[10], z;
    x = 0;
    z = 0;
    // BUG! element 10 is past the
    // end of the array
    for (int i = 0; i <= 10; i++) {
        y[i] = 10;
     }
}

void readUninitialized()
{
    int* ip;
    cout << *ip << endl; // BUG! ip is uninitialized
}

int main(int argc, char** argv)
{
  cout << "Memory leak\n";
  memoryLeak();
  cout << "Mismatched Free\n";
  mismatchedFree();
  cout << "Double Free\n";
  doubleFree();
  cout << "Free Unallocated\n";
  freeUnallocated();
  cout << "Free Stack\n";
  freeStack();
  cout << "Access Invalid\n";
  accessInvalid();
  cout << "Access Freed\n";
  accessFreed();
  cout << "Access Elsewhere\n";
  accessElsewhere();
  cout << "Read Uninitialized\n";
  readUninitialized();
}
