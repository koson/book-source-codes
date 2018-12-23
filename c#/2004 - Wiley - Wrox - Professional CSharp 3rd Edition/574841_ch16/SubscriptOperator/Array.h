class Array
{
 public:
  Array();
  ~Array();
  int& operator[](int x);
  const int& operator[](int x) const;
 protected:
  static const int kAllocSize = 4;
  void resize(int newSize);
  int* mElems;
  int mSize;
 private:
  // Disallow assignment and pass-by-value
  Array(const Array& src);
  Array& operator=(const Array& rhs);
};
