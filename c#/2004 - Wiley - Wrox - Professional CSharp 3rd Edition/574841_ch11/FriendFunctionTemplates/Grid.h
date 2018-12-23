// Grid.h

#include <iostream>
using std::ostream;

// Forward declare Grid template.
template <typename T> class Grid;

// Prototype for templatized operator<<. 
template<typename T>
ostream& operator<<(ostream& ostr, const Grid<T>& grid);

template <typename T>
class Grid
{
 public:
  Grid(int inWidth = kDefaultWidth, int inHeight = kDefaultHeight);
  Grid(const Grid<T>& src);
  ~Grid();
  Grid<T>& operator=(const Grid<T>& rhs);

  void setElementAt(int x, int y, const T& inElem);
  T& getElementAt(int x, int y);
  const T& getElementAt(int x, int y) const;

  int getHeight() const { return mHeight; }
  int getWidth() const { return mWidth; }
  static const int kDefaultWidth = 10;
  static const int kDefaultHeight = 10;

  friend ostream& operator<< <T>(ostream& ostr, const Grid<T>& grid);

 protected:
  void copyFrom(const Grid<T>& src);
  T** mCells;
  int mWidth, mHeight;
};

template <typename T>
Grid<T>::Grid(int inWidth, int inHeight) : mWidth(inWidth), mHeight(inHeight)
{
  mCells = new T* [mWidth];
  for (int i = 0; i < mWidth; i++) {
    mCells[i] = new T[mHeight];
  }
}

template <typename T>
const int Grid<T>::kDefaultWidth;

template <typename T>
const int Grid<T>::kDefaultHeight;

template <typename T>
Grid<T>::Grid(const Grid<T>& src)
{
  copyFrom(src);
}

template <typename T>
Grid<T>::~Grid()
{
  // free the old memory
  for (int i = 0; i < mWidth; i++) {
    delete [] mCells[i];
  }
  delete [] mCells;
}

template <typename T>
void Grid<T>::copyFrom(const Grid<T>& src)
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

template <typename T>
Grid<T>& Grid<T>::operator=(const Grid<T>& rhs)
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

template <typename T>
void Grid<T>::setElementAt(int x, int y, const T& inElem)
{
  mCells[x][y] = inElem;
}

template <typename T>
T& Grid<T>::getElementAt(int x, int y)
{
  return (mCells[x][y]);
}

template <typename T>
const T& Grid<T>::getElementAt(int x, int y) const
{
  return (mCells[x][y]);
}

template <typename T>
ostream& operator<<(ostream& ostr, const Grid<T>& grid)
{
  for (int i = 0; i < grid.mHeight; i++) {
    for (int j = 0; j < grid.mWidth; j++) {
      // add a tab between each element of a row
      ostr << grid.mCells[j][i] << "\t"; 
    }
    ostr << std::endl; // add a newline between each row
  }
  return (ostr);
}
