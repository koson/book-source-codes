#include <vector>
#include <iostream>
#include <functional>
#include <algorithm>
using namespace std;

template <typename InputIterator, typename Predicate>
vector<InputIterator>
find_all(InputIterator first, InputIterator last, Predicate pred)
{
  vector<InputIterator> res;

  while (true) {
    // find the next match in the current range
    first = find_if(first, last, pred);
    // check if find_if failed to find a match
    if (first == last) {
      break;
    }
    // store this match
    res.push_back(first);
    // shorten the range to start at one past the current match
    ++first;
  }
  return (res);
}

int main(int argc, char** argv)
{
  int arr[] = {3, 4, 5, 4, 5, 6, 5, 8};
  vector<int*> all = find_all(arr, arr + 8, bind2nd(equal_to<int>(), 5)); 
  
  cout << "Found " << all.size() << " matching elements: ";
  
  for (vector<int*>::iterator it = all.begin(); it != all.end(); ++it) {
    cout << **it << " ";
  }
  cout << endl;

  return (0);
}
