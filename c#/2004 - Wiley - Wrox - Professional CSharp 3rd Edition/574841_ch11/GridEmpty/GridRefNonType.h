// Grid.h

template <typename T, const T& EMPTY>
class Grid
{
 public:
  Grid(int inWidth = kDefaultWidth, int inHeight = kDefaultHeight);
  Grid(const Grid<T, EMPTY>& src);
  ~Grid();
  Grid<T, EMPTY>& operator=(const Grid<T, EMPTY>& rhs);

  void setElementAt(int x, int y, const T& inElem);
  T& getElementAt(int x, int y);
  const T& getElementAt(int x, int y) const;

  int getHeight() const { return mHeight; }
  int getWidth() const { return mWidth; }
  static const int kDefaultWidth = 10;
  static const int kDefaultHeight = 10;

 protected:
  void copyFrom(const Grid<T, EMPTY>& src);
  T** mCells;
  int mWidth, mHeight;
};

template <typename T, const T& EMPTY>
Grid<T, EMPTY>::Grid(int inWidth, int inHeight) :
  mWidth(inWidth), mHeight(inHeight)
{
  mCells = new T* [mWidth];
  for (int i = 0; i < mWidth; i++) {
    mCells[i] = new T[mHeight];
    for (int j = 0; j < mHeight; j++) {
      mCells[i][j] = EMPTY;
    }
  }
}

template <typename T, const T& EMPTY>
const int Grid<T, EMPTY>::kDefaultWidth;

template <typename T, const T& EMPTY>
const int Grid<T, EMPTY>::kDefaultHeight;

template <typename T, const T& EMPTY>
Grid<T, EMPTY>::Grid(const Grid<T, EMPTY>& src)
{
  copyFrom(src);
}

template <typename T, const T& EMPTY>
Grid<T, EMPTY>::~Grid()
{
  // free the old memory
  for (int i = 0; i < mWidth; i++) {
    delete [] mCells[i];
  }
  delete [] mCells;
}

template <typename T, const T& EMPTY>
void Grid<T, EMPTY>::copyFrom(const Grid<T, EMPTY>& src)
{
  int i, j;
  mWidth = src.mWidth;
  mHeight = src.mHeight;

  mCells = new T* [mWidth];
  for (i = 0; i < mWidth; i++) {
    mCells[i] = new T[mHeight];
  }

  for (i = 0; i < mWidth; i++) {
    for (j = 0; j < mHeight; j++) {
      mCells[i][j] = src.mCells[i][j];
    }
  }
}

template <typename T, const T& EMPTY>
Grid<T, EMPTY>& Grid<T, EMPTY>::operator=(const Grid<T, EMPTY>& rhs)
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

template <typename T, const T& EMPTY>
void Grid<T, EMPTY>::setElementAt(int x, int y, const T& inElem)
{
  mCells[x][y] = inElem;
}

template <typename T, const T& EMPTY>
T& Grid<T, EMPTY>::getElementAt(int x, int y)
{
  return (mCells[x][y]);
}

template <typename T, const T& EMPTY>
const T& Grid<T, EMPTY>::getElementAt(int x, int y) const
{
  return (mCells[x][y]);
}
