#include <iostream>

using namespace std;

typedef bool(*YesNoFcn)(int, int);

bool intEqual(int inItem1, int inItem2);
bool bothOdd(int inItem1, int inItem2);
void findMatches(int values1[], int values2[], int numValues, YesNoFcn inFunction);

void findMatches(int values1[], int values2[], int numValues, YesNoFcn inFunction)
{
  for (int i = 0; i < numValues; i++) {
    if (inFunction(values1[i], values2[i])) {
      cout << "Match found at position " << i << " (" << values1[i] << ", " << values2[i] << ")" << endl;
    }
  }
}

bool intEqual(int inItem1, int inItem2)
{
  return (inItem1 == inItem2);
}

bool bothOdd(int inItem1, int inItem2)
{
  return (inItem1 % 2 == 1 && inItem2 % 2 == 1);
}

int main()
{
  int arr1[7] = {2, 5, 6, 9, 10, 1, 1};
  int arr2[7] = {4, 4, 2, 9, 0, 3, 4};

  cout << "Calling findMatches() using intEqual():" << endl;
  findMatches(arr1, arr2, 7, &intEqual);

  cout << endl;

  cout << "Calling findMatches() using bothOdd():" << endl;
  findMatches(arr1, arr2, 7, &bothOdd);
}


