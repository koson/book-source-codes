#include "Grid.h"

template <typename T>
class GameBoard : public Grid<T>
{
 public:
  GameBoard(int inWidth = Grid<T>::kDefaultWidth,
	    int inHeight = Grid<T>::kDefaultHeight);
  void move(int xSrc, int ySrc, int xDest, int yDest);
};

template <typename T>
GameBoard<T>::GameBoard(int inWidth, int inHeight) :
  Grid<T>(inWidth, inHeight)
{
}

template <typename T>
void GameBoard<T>::move(int xSrc, int ySrc, int xDest, int yDest)
{
  this->mCells[xDest][yDest] = this->mCells[xSrc][ySrc];
  this->mCells[xSrc][ySrc] = T(); // zero-initialize the src cell
}
