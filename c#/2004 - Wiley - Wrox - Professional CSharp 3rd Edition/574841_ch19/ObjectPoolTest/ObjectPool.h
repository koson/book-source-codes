#include <queue>
#include <stdexcept>

using namespace std;

//
// template class ObjectPool
//
// Provides an object pool that can be used with any class that provides a
// default constructor.
//
// The object pool constructor creates a pool of objects, which it hands out
// to clients when requested via the acquireObject() method. When a client is
// finished with the object it calls releaseObject() to put the object back
// into the object pool.
//
// The constructor and destructor on each object in the pool will be called only
// once each for the lifetime of the program, not once per acquisition and release.
//
// The primary use of an object pool is to avoid creating and deleting objects
// over and over. The object pool is most suited to applications that use large 
// numbers of objects for short periods of time.
//
// For efficiency, the object pool doesn't perform sanity checks.
// Expects the user to release every acquired object exactly once.
// Expects the user to avoid using any objects that he or she
// has released.
//
// Expects the user not to delete the object pool until every object
// that was acquired has been released. Deleting the object pool invalidates
// any objects that the user had acquired, even if they had not yet been released.
//
template <typename T>
class ObjectPool
{
 public:
  //
  // Creates an object pool with chunkSize objects.
  // Whenever the object pool runs out of objects, chunkSize
  // more objects will be added to the pool. The pool only grows:
  // objects are never removed from the pool (freed), until
  // the pool is destroyed.
  //
  // chunkSize must be greater than 0.
  //
  ObjectPool(int chunkSize = kDefaultChunkSize)
    throw(invalid_argument, bad_alloc);

  //
  // Frees all the allocated objects. Invalidates any objects that have
  // been aquired for use.
  //
  ~ObjectPool();

  //
  // Reserve an object for use. The reference to the object is invalidated
  // if the object pool itself is freed.
  // 
  // Clients must not free the object!
  //
  T &acquireObject();

  //
  // Return the object to the pool. Clients must not use the object after
  // it has been returned to the pool.
  //
  void releaseObject(T &obj);

 protected:
  //
  // mFreeList stores the objects that are not currently in use
  // by clients.
  //
  queue<T *> mFreeList;
  //
  // mAllObjects stores pointers to all the objects, in use
  // or not. This vector is needed in order to ensure that all
  // objects are freed properly in the destructor.
  //
  vector<T *> mAllObjects;

  int mChunkSize;
  static const int kDefaultChunkSize;

  //
  // Allocate mChunkSize new objects and adds them
  // to the mFreeList.
  //
  void allocateChunk();
  static void arrayDeleteObject(T *obj);

 private:
  // prevent assignment and pass-by-value
  ObjectPool(const ObjectPool<T> &src);
  ObjectPool<T> &operator=(const ObjectPool<T> &rhs);
};
