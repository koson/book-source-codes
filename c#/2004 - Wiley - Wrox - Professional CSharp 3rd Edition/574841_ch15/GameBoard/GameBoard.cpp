// GameBoard.cpp
#include "GameBoard.h"

using namespace std;

// Microsoft Visual Studio requires you to omit the following two lines.
// However, some compilers require them.
//const int GameBoard::kDefaultWidth;
//const int GameBoard::kDefaultHeight;

GameBoard::GameBoard(int inWidth, int inHeight) throw(bad_alloc) :
  mWidth(inWidth), mHeight(inHeight)
{
  int i, j;
  mCells = new GamePiece* [mWidth];

  try {
    for (i = 0; i < mWidth; i++) {
      mCells[i] = new GamePiece[mHeight];
    }
  } catch (...) {
    //
    // Cleanup any memory we already allocated, because the destructor
    // will never get called. The upper bound of the for loop is the index
    // of the last element in the mCells array that we tried to allocate
    // (the one that failed). All indices before that one store pointers to
    // allocated memory that must be freed.
    //
    for (j = 0; j < i; j++) { 
      delete [] mCells[j];
    }
    delete [] mCells;

    // translate any exception to bad_alloc
    throw bad_alloc();
  }
}

GameBoard::GameBoard(const GameBoard& src) throw (bad_alloc)
{
  copyFrom(src);
}

GameBoard::~GameBoard() throw()
{
  // free the old memory
  for (int i = 0; i < mWidth; i++) {
    delete [] mCells[i];
  }

  delete [] mCells;
}

GameBoard& GameBoard::operator=(const GameBoard& rhs) throw (bad_alloc)
{
  // check for self-assignment
  if (this == &rhs) {
    return (*this);
  }
  // free the old memory
  for (int i = 0; i < mWidth; i++) {
    delete [] mCells[i];
  }
  delete [] mCells;

  // copy the new memory
  copyFrom(rhs);

  return (*this);
}

void GameBoard::copyFrom(const GameBoard& src) throw(bad_alloc)
{
  int i, j;
  mWidth = src.mWidth;
  mHeight = src.mHeight;

  mCells = new GamePiece *[mWidth];

  try {
    for (i = 0; i < mWidth; i++) {
      mCells[i] = new GamePiece[mHeight];
    }
  } catch (...) {
    // Cleanup any memory we already allocated.
    // If this function is called from the copy constructor,
    // the destructor will never get called.
    // Use the same upper bound on the loop as described in the constructor.
    for (j = 0; j < i; j++) {
      delete [] mCells[j];
    }
    delete [] mCells;

    // Set mCells and mWidth to values that will allow the
    // destructor to run without harming anything.
    // This function is called from operator=, in which case the
    // object was already constructed, so the destructor will get
    // called.
    mCells = NULL;
    mWidth = 0;
    throw bad_alloc();
  }

  for (i = 0; i < mWidth; i++) {
    for (j = 0; j < mHeight; j++) {
      mCells[i][j] = src.mCells[i][j];
    }
  }
}

void GameBoard::setPieceAt(int x, int y, const GamePiece& inElem)
  throw(out_of_range)
{
  // Check for out of range arguments
  if (x < 0 || x >= mWidth || y < 0 || y >= mHeight) {
    throw out_of_range("Invalid width or height");
  }
  mCells[x][y] = inElem;
}

GamePiece& GameBoard::getPieceAt(int x, int y) throw(out_of_range)
{
  // Check for out of range arguments
  if (x < 0 || x >= mWidth || y < 0 || y >= mHeight) {
    throw out_of_range("Invlalid width or height");
  }
  return (mCells[x][y]);
}

const GamePiece& GameBoard::getPieceAt(int x, int y) const throw(out_of_range)
{
  // Check for out of range arguments
  if (x < 0 || x >= mWidth || y < 0 || y >= mHeight) {
    throw out_of_range("Invlalid width or height");
  }
  return (mCells[x][y]);
}

