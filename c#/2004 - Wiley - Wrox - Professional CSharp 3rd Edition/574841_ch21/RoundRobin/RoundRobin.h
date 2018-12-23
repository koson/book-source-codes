#include <stdexcept>
#include <vector>
using std::vector;

//
// class template RoundRobin
//
// Provides simple round-robin semantics for a list of elements.
// Clients add elements to the end of the list with add().
//
// getNext() returns the next element in the list, starting with the first,
// and cycling back to the first when the end of the list is reached.
//
// remove() removes the element matching the argument.
//
template <typename T>
class RoundRobin
{
 public:
  //
  // Client can give a hint as to the number of expected elements for
  // incrased efficiency.
  //
  RoundRobin(int numExpected = 0);
  ~RoundRobin();

  //
  // Appends elem to the end of the list. May be called
  // between calls to getNext().
  //
  void add(const T& elem);

  //
  // Removes the first (and only the first) element
  // in the list that is equal (with operator==) to elem.
  // May be called between calls to getNext().
  //
  void remove(const T& elem);

  //
  // Returns the next element in the list, starting from 0 and continuously
  // cycling, taking into account elements that are added or removed.
  //
  T& getNext() throw(std::out_of_range);
 protected:
  vector<T> mElems;
  typename std::vector<T>::iterator mCurElem;

 private:
  // prevent assignment and pass-by-reference
  RoundRobin(const RoundRobin& src);
  RoundRobin& operator=(const RoundRobin& rhs);
};

template <typename T>
RoundRobin<T>::RoundRobin(int numExpected)
{
  // If the client gave a guideline, reserve that much space.
  mElems.reserve(numExpected);

  // Initialize mCurElem even though it isn't used until
  // there's at least one element.
  mCurElem = mElems.begin();
}

template <typename T>
RoundRobin<T>::~RoundRobin()
{
  // nothing to do here -- the vector will delete all the elements
}

//
// Always add the new element at the end
//
template <typename T>
void RoundRobin<T>::add(const T& elem)
{
  //
  // Even though we add the element at the end,
  // the vector could reallocate and invalidate the iterator.
  // Take advantage of the random access iterator features to save our
  // spot.
  //
  int pos = mCurElem - mElems.begin();

  // add the element
  mElems.push_back(elem);

  // If it's the first element, initialize the iterator to the beginning
  if (mElems.size() == 1) { 
    mCurElem = mElems.begin();
  } else {
    // set it back to our spot
    mCurElem = mElems.begin() + pos;
  }
}

template <typename T>
void RoundRobin<T>::remove(const T& elem)
{
  for (typename std::vector<T>::iterator it = mElems.begin(); it != mElems.end(); ++it) {
    if (*it == elem) {
      //
      // Removing an element will invalidate our mCurElem iterator if
      // it refers to an element past the point of the removal.
      // Take advantage of the random access features of the iterator
      // to track the position of the current element after the removal.
      //
      int newPos;

      // If the current iterator is before or at the one we're removing,
      // the new position is the same as before.
      if (mCurElem <= it) {
	newPos = mCurElem - mElems.begin();
      } else {
	// otherwise, it's one less than before
	newPos = mCurElem - mElems.begin() - 1;
      }
      // erase the element (and ignore the return value)
      mElems.erase(it);

      // Now reset our iterator
      mCurElem = mElems.begin() + newPos;

      // If we were pointing to the last element and it was removed,
      // we need to loop back to the first.
      if (mCurElem == mElems.end()) {
	mCurElem = mElems.begin();
      }
      return;
    }
  }
}

template <typename T>
T& RoundRobin<T>::getNext()throw(std::out_of_range)
{
  // First, make sure there are any elements.
  if (mElems.empty()) {
    throw std::out_of_range("No elements in the list");
  }

  // retrieve a reference to return
  T& retVal = *mCurElem;

  // Increment the iterator modulo the number of elements
  ++mCurElem;
  if (mCurElem == mElems.end()) {
    mCurElem = mElems.begin();
  }

  // return the reference
  return (retVal);
}
