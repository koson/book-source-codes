#include "NDGrid.h"

#include <iostream>
using namespace std;

int main(int argc, char** argv)
{
  NDGrid<int, 3> my3DGrid;
  my3DGrid[2][1][2] = 5;
  my3DGrid[1][1][1] = 5;

  cout << my3DGrid[2][1][2] << endl;

  return (0);
}
