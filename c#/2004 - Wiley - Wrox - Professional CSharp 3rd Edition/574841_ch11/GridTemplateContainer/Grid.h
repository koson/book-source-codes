// Grid.h

template <typename T, typename Container>
class Grid
{
 public:
  Grid(int inWidth = kDefaultWidth, int inHeight = kDefaultHeight);
  Grid(const Grid<T, Container>& src);
  ~Grid();
  Grid<T, Container>& operator=(const Grid<T, Container>& rhs);

  void setElementAt(int x, int y, const T& inElem);
  T& getElementAt(int x, int y);
  const T& getElementAt(int x, int y) const;

  int getHeight() const { return mHeight; }
  int getWidth() const { return mWidth; }
  static const int kDefaultWidth = 10;
  static const int kDefaultHeight = 10;

 protected:
  void copyFrom(const Grid<T, Container>& src);
  Container* mCells;
  int mWidth, mHeight;
};

template <typename T, typename Container>
Grid<T, Container>::Grid(int inWidth, int inHeight) :
  mWidth(inWidth), mHeight(inHeight)
{
  // Dynamically allocate the array of mWidth containers
  mCells = new Container[mWidth];
  for (int i = 0; i < mWidth; i++) {
    // Resize each container so that it can hold mHeight elements.
    mCells[i].resize(mHeight);
  }
}

template <typename T, typename Container>
const int Grid<T, Container>::kDefaultWidth;

template <typename T, typename Container>
const int Grid<T, Container>::kDefaultHeight;

template <typename T, typename Container>
Grid<T, Container>::Grid(const Grid<T, Container>& src)
{
  copyFrom(src);
}

template <typename T, typename Container>
  Grid<T, Container>::~Grid()
{
  delete [] mCells;
}

template <typename T, typename Container>
void Grid<T, Container>::copyFrom(const Grid<T, Container>& src)
{
  int i, j;
  mWidth = src.mWidth;
  mHeight = src.mHeight;

  mCells = new Container[mWidth];
  for (i = 0; i < mWidth; i++) {
    // Resize each element, as in the constructor.
    mCells[i].resize(mHeight);
  }
  for (i = 0; i < mWidth; i++) {
    for (j = 0; j < mHeight; j++) {
      mCells[i][j] = src.mCells[i][j];
    }
  }
}

template <typename T, typename Container>
Grid<T, Container>& Grid<T, Container>::operator=(const Grid<T, Container>& rhs)
{
  // check for self-assignment
  if (this == &rhs) {
    return (*this);
  }
  // free the old memory
  delete [] mCells;

  // copy the new memory
  copyFrom(rhs);

  return (*this);
}

template <typename T, typename Container>
void Grid<T, Container>::setElementAt(int x, int y, const T& inElem)
{
  mCells[x][y] = inElem;
}

template <typename T, typename Container>
T& Grid<T, Container>::getElementAt(int x, int y)
{
  return (mCells[x][y]);
}

template <typename T, typename Container>
const T& Grid<T, Container>::getElementAt(int x, int y) const
{
  return (mCells[x][y]);
}
