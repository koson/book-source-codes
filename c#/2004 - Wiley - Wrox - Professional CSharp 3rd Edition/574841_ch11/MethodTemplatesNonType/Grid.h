template <typename T, int WIDTH = 10, int HEIGHT = 10>
class Grid
  {
  public:

  // Writing a copy constructor (even a templatized one)
  // prevents the compiler from generating a default constructor
  Grid() {}

  template <typename E, int WIDTH2, int HEIGHT2>
  Grid(const Grid<E, WIDTH2, HEIGHT2>& src);

  template <typename E, int WIDTH2, int HEIGHT2>
  Grid<T, WIDTH, HEIGHT>& operator=(const Grid<E, WIDTH2, HEIGHT2>& rhs);

  void setElementAt(int x, int y, const T& inElem);
  T& getElementAt(int x, int y);
  const T& getElementAt(int x, int y) const;
  int getHeight() const { return HEIGHT; }
  int getWidth() const { return WIDTH; }

 protected:
  template <typename E, int WIDTH2, int HEIGHT2>
  void copyFrom(const Grid<E, WIDTH2, HEIGHT2>& src);

  T mCells[WIDTH][HEIGHT];
};

template <typename T, int WIDTH, int HEIGHT>
void Grid<T, WIDTH, HEIGHT>::setElementAt(int x, int y, const T& inElem)
{
  mCells[x][y] = inElem;
}

template <typename T, int WIDTH, int HEIGHT>
T& Grid<T, WIDTH, HEIGHT>::getElementAt(int x, int y)
{
  return (mCells[x][y]);
}

template <typename T, int WIDTH, int HEIGHT>
const T& Grid<T, WIDTH, HEIGHT>::getElementAt(int x, int y) const
{
  return (mCells[x][y]);
}

template <typename T, int WIDTH, int HEIGHT>
template <typename E, int WIDTH2, int HEIGHT2>
Grid<T, WIDTH, HEIGHT>::Grid(const Grid<E, WIDTH2, HEIGHT2>& src)
{
  copyFrom(src);
}

template <typename T, int WIDTH, int HEIGHT>
template <typename E, int WIDTH2, int HEIGHT2>
Grid<T, WIDTH, HEIGHT>& Grid<T, WIDTH, HEIGHT>::operator=(
    const Grid<E, WIDTH2, HEIGHT2>& rhs)
{
  // no need to check for self-assignment because this version of
  // assignment is never called when T and E are the same

  // No need to free any memory first
  copyFrom(rhs);
  return (*this);
}

template <typename T, int WIDTH, int HEIGHT>
  template <typename E, int WIDTH2, int HEIGHT2>
  void Grid<T, WIDTH, HEIGHT>::copyFrom(
    const Grid<E, WIDTH2, HEIGHT2>& src)
  {
    int i, j;

  for (i = 0; i < WIDTH; i++) {
    for (j = 0; j < HEIGHT; j++) {
      if (i < WIDTH2 && j < HEIGHT2) {
	mCells[i][j] = src.getElementAt(i, j);
      } else {
	mCells[i][j] = T();
      }
    }

  }
  }
