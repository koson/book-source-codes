// GameBoard.h

class GamePiece {};

class GameBoard
{
 public:
  // The general-purpose GameBoard allows the user to specify its dimensions
  GameBoard(int inWidth = kDefaultWidth, int inHeight = kDefaultHeight);
  GameBoard(const GameBoard& src); // copy constructor
  ~GameBoard();
  GameBoard& operator=(const GameBoard& rhs); // assignment operator

  void setPieceAt(int x, int y, const GamePiece& inPiece);
  GamePiece& getPieceAt(int x, int y);
  const GamePiece& getPieceAt(int x, int y) const;

  int getHeight() const { return mHeight; }
  int getWidth() const { return mWidth; }
  static const int kDefaultWidth = 10;
  static const int kDefaultHeight = 10;

 protected:
  void copyFrom(const GameBoard& src);
  // objects dynamically allocate space for the game pieces.
  GamePiece** mCells;
  int mWidth, mHeight;
};
