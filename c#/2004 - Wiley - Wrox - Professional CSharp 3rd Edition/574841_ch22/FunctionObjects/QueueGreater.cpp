#include <queue>
#include <functional>
#include <iostream>
using namespace std;

int main(int argc, char** argv)
{
  priority_queue<int, vector<int>, greater<int> > myQueue;

  myQueue.push(3);
  myQueue.push(4);
  myQueue.push(2);
  myQueue.push(1);

  while (!myQueue.empty()) {
    cout << myQueue.top() << endl;
    myQueue.pop();
  }

  return (0);
}
