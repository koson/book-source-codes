template <typename T>
class OneDGrid
{
    public:
        OneDGrid(int inSize = kDefaultSize);
        OneDGrid(const OneDGrid<T>& src);
        ~OneDGrid();

        OneDGrid<T> &operator=(const OneDGrid<T>& rhs);
        void resize(int newSize);

        T& operator[](int x);
        const T& operator[](int x) const;
        int getSize() const { return mSize; }
        static const int kDefaultSize = 10;
    protected:
        void copyFrom(const OneDGrid<T>& src);
        T* mElems;
        int mSize;
};

template <typename T>
const int OneDGrid<T>::kDefaultSize;

template <typename T>
OneDGrid<T>::OneDGrid(int inSize) : mSize(inSize)
{
    mElems = new T[mSize];
}

template <typename T>
OneDGrid<T>::OneDGrid(const OneDGrid<T>& src)
{
    copyFrom(src);
}

template <typename T>
OneDGrid<T>::~OneDGrid()
{
    delete [] mElems;
}

template <typename T>
void OneDGrid<T>::copyFrom(const OneDGrid<T>& src)
{
    mSize = src.mSize;
    mElems = new T[mSize];

    for (int i = 0; i < mSize; i++) {
        mElems[i] = src.mElems[i];
    }
}

template <typename T>
OneDGrid<T>& OneDGrid<T>::operator=(const OneDGrid<T>& rhs)
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
void OneDGrid<T>::resize(int newSize)
{
    T* newElems = new T[newSize]; // Allocate the new array of the new size

    // Handle the new size being smaller or bigger than the old size
    for (int i = 0; i < newSize && i < mSize; i++) {
        // Copy the elements from the old array to the new one
        newElems[i] = mElems[i];
    }
    mSize = newSize; // store the new size
    delete [] mElems; // free the memory for the old array
    mElems = newElems; // Store the pointer to the new array
}

template <typename T>
T& OneDGrid<T>::operator[](int x)
{
    return (mElems[x]);
}

template <typename T>
const T& OneDGrid<T>::operator[](int x) const
{
    return (mElems[x]);
}

