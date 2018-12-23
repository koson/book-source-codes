#include <iostream>
using namespace std;

void performTask()
{
  static bool inited = false;

  if (!inited) {
    cout << "initing\n";
    // perform initialization
    inited = true;
  }

  // perform the desired task
}

int main(int argc, char** argv)
{
  performTask();
  performTask();

  return (0);
}
