#include "Grid.h"

int main(int argc, char** argv)
{
  Grid<int> grid;
  grid.setElementAt(2, 3, 1);

  std::cout << grid;

  return (0);
}
