template <typename T, int N>
class NDGrid
{
    public:
        NDGrid();
        NDGrid(int inSize);
        NDGrid(const NDGrid<T, N>& src);
        ~NDGrid();

        NDGrid<T, N>& operator=(const NDGrid<T, N>& rhs);
        void resize(int newSize);
        NDGrid<T, N-1>& operator[](int x);
        const NDGrid<T, N-1>& operator[](int x) const;
        int getSize() const { return mSize; }
        static const int kDefaultSize = 10;
    protected:
        void copyFrom(const NDGrid<T, N>& src);
        NDGrid<T, N-1>* mElems;
        int mSize;
};

template <typename T>
class NDGrid<T, 1>
{
    public:
        NDGrid(int inSize = kDefaultSize);
        NDGrid(const NDGrid<T, 1>& src);
        ~NDGrid();
        NDGrid<T, 1>& operator=(const NDGrid<T, 1>& rhs);
        void resize(int newSize);
        T& operator[](int x);
        const T& operator[](int x) const;
        int getSize() const { return mSize; }
        static const int kDefaultSize = 10;
    protected:
        void copyFrom(const NDGrid<T, 1>& src);
        T* mElems;
        int mSize;
};

template <typename T, int N>
const int NDGrid<T, N>::kDefaultSize;

template <typename T, int N>
NDGrid<T, N>::NDGrid(int inSize) : mSize(inSize)
{
    mElems = new NDGrid<T, N-1>[mSize];
    // Allocating the array above calls the 0-argument
    // constructor for the NDGrid<T, N-1>, which constructs
    // it with the default size. Thus, we must explicitly call
    // resize() on each of the elements.
    for (int i = 0; i < mSize; i++) {
        mElems[i].resize(inSize);
    }
}

template <typename T, int N>
NDGrid<T, N>::NDGrid() : mSize(kDefaultSize)
{
    mElems = new NDGrid<T, N-1>[mSize];
}


template <typename T, int N>
NDGrid<T, N>::NDGrid(const NDGrid<T, N>& src)
{
    copyFrom(src);
}

template <typename T, int N>
NDGrid<T, N>::~NDGrid()
{
    delete [] mElems;
}

template <typename T, int N>
void NDGrid<T, N>::copyFrom(const NDGrid<T, N>& src)
{
     mSize = src.mSize;
    mElems = new NDGrid<T, N-1>[mSize];
    for (int i = 0; i < mSize; i++) {
        mElems[i] = src.mElems[i];
    }
}

template <typename T, int N>
NDGrid<T, N>& NDGrid<T, N>::operator=(const NDGrid<T, N>& rhs)
{
    // check for self-assignment
    if (this == &rhs) {
        return (*this);
    }
    // free the old memory
    delete [] mElems;
    // copy the new memory
    copyFrom(rhs);
    return (*this);
}

template <typename T, int N>
void NDGrid<T, N>::resize(int newSize)
{
    // allocate the new array with the new size
    NDGrid<T, N - 1>* newElems = new NDGrid<T, N - 1>[newSize];
    // Copy all the elements, handling the cases where newSize is
    // larger than mSize and smaller than mSize.
    for (int i = 0; i < newSize && i < mSize; i++) {
        newElems[i] = mElems[i];
        // Resize the nested Grid elements recursively.
        newElems[i].resize(newSize);
    }
    // Store the new size and pointer to the new array.
    // Free the memory for the old array first.
    mSize = newSize;
    delete [] mElems;
    mElems = newElems;
}

template <typename T, int N>
NDGrid<T, N-1>& NDGrid<T, N>::operator[](int x)
{
    return (mElems[x]);
}

template <typename T, int N>
const NDGrid<T, N-1>& NDGrid<T, N>::operator[](int x) const
{
    return (mElems[x]);
}

template <typename T>
const int NDGrid<T, 1>::kDefaultSize;

template <typename T>
NDGrid<T, 1>::NDGrid(int inSize) : mSize(inSize)
{
    mElems = new T[mSize];
}

template <typename T>
NDGrid<T, 1>::NDGrid(const NDGrid<T, 1>& src)
{
    copyFrom(src);
}

template <typename T>
NDGrid<T, 1>::~NDGrid()
{
    delete [] mElems;
}

template <typename T>
void NDGrid<T, 1>::copyFrom(const NDGrid<T, 1>& src)
{
    mSize = src.mSize;
    mElems = new T[mSize];
    for (int i = 0; i < mSize; i++) {
        mElems[i] = src.mElems[i];
    }
}

template <typename T>
NDGrid<T, 1>& NDGrid<T, 1>::operator=(const NDGrid<T, 1>& rhs)
{
    // check for self-assignment
    if (this == &rhs) {
        return (*this);
    }
    // free the old memory
    delete [] mElems;
    // copy the new memory
    copyFrom(rhs);
    return (*this);
}

template <typename T>
void NDGrid<T, 1>::resize(int newSize)
{
    T* newElems = new T[newSize];

    for (int i = 0; i < newSize && i < mSize; i++) {
        newElems[i] = mElems[i];
        // don't need to resize recursively, because this is the base case
    }
    mSize = newSize;
    delete [] mElems;
    mElems = newElems;
}

template <typename T>
T& NDGrid<T, 1>::operator[](int x)
{
    return (mElems[x]);
}

template <typename T>
const T& NDGrid<T, 1>::operator[](int x) const
{
  return (mElems[x]);
}


