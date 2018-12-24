// TemplateVector - a templatize vector class with an
//                  initializer list
#include <initializer_list>
template <class T>
class TemplateVector
{
  public:
    TemplateVector(int nArraySize)
    {
        // store off the number of elements
        nSize = nArraySize;
        array = new T[nArraySize];
        reset();
    }
    TemplateVector(const std::initializer_list<T> il) :
        TemplateVector(il.size())
    {
        // copy the contents of il into the vector
        for(const T* p = il.begin(); p < il.end(); p++)
        {
            add(*p);
        }
    }

    int size() { return nWriteIndex; }
    void reset() { nWriteIndex = 0; nReadIndex = 0; }
    void add(const T& object)
    {
        if (nWriteIndex < nSize)
        {
            array[nWriteIndex++] = object;
        }
    }
    T& get()
    {
        return array[nReadIndex++];
    }

  protected:
    int nSize;
    int nWriteIndex;
    int nReadIndex;
    T* array;
};
