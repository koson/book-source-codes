#include <stdexcept>
#include <new>
using std::bad_alloc;
using std::out_of_range;

class GamePiece {};

class GameBoard
{
 public:
  GameBoard(int inWidth = kDefaultWidth, int inHeight = kDefaultHeight)
    throw(bad_alloc);
  GameBoard(const GameBoard& src) throw(bad_alloc);
  ~GameBoard() throw();

  GameBoard& operator=(const GameBoard& rhs) throw(bad_alloc);

  void setPieceAt(int x, int y, const GamePiece& inPiece)
    throw(out_of_range);
  GamePiece& getPieceAt(int x, int y) throw(out_of_range);
  const GamePiece& getPieceAt(int x, int y) const throw(out_of_range);

  int getHeight() const throw() { return mHeight; }
  int getWidth() const throw() { return mWidth; }


  static const int kDefaultWidth = 100;
  static const int kDefaultHeight = 100;

 protected:
  void copyFrom(const GameBoard& src) throw(bad_alloc);

  GamePiece** mCells;
  int mWidth, mHeight;
};
