template <typename T, int WIDTH = 10, int HEIGHT = 10>
class Grid
{
 public:
  void setElementAt(int x, int y, const T& inElem);
  T& getElementAt(int x, int y);
  const T& getElementAt(int x, int y) const;
  int getHeight() const { return HEIGHT; }
  int getWidth() const { return WIDTH; }

 protected:
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
