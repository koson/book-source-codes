#include <vector>
#include <list>
#include <stdexcept>
using namespace std;

// Any Hash Class must provide two methods: hash() and numBuckets().
template <typename T>
class DefaultHash
{
 public:
  // throws invalid_argument if numBuckets is non-positive
  DefaultHash(int numBuckets = 101) throw (invalid_argument);
  int hash(const T& key) const;
  int numBuckets() const { return mNumBuckets; }

 protected:
  int mNumBuckets;
};

// Specialization for strings
template <>
class DefaultHash<string>
{
 public:
  // throws invalid_argument if numBuckets is non-positive
  DefaultHash(int numBuckets = 101) throw (invalid_argument);
  int hash(const string& key) const;
  int numBuckets() const { return mNumBuckets; }

 protected:
  int mNumBuckets;
};

template <typename Key, typename T, typename Compare = std::equal_to<Key>,
    typename Hash = DefaultHash<Key> >
class hashmap
{
public:
  typedef Key key_type;
  typedef T mapped_type;
  typedef pair<const Key, T> value_type;

  // Constructors
  // throws invalid_argument if the hash object specifies a non-positive
  // number of buckets.   
  explicit hashmap(const Compare& comp = Compare(),
		   const Hash& hash = Hash()) throw(invalid_argument);

  // destructor, copy constructor, assignment operator
  ~hashmap();
  hashmap(const hashmap<Key, T, Compare, Hash>& src);
  hashmap<Key, T, Compare, Hash>& operator=(const hashmap<
					    Key, T, Compare, Hash>& rhs);

  // element insert
  // inserts the key/value pair x
  void insert(const value_type& x);

  // element delete
  // removes the element with key x, if it exists
void erase(const key_type& x);

  // element lookup
  // find returns a pointer to the element with key x.
  // Returns NULL if no element with that key exists.
value_type* find(const key_type& x);

  // operator[] finds the element with key x or inserts an
  // element with that key if none exists yet. Returns a reference to the
  // value corresponding to that key.
  T& operator[] (const key_type& x);

protected:
typename list<pair<const Key, T> >::iterator
findElement(const key_type& x, int& bucket) const;

  typedef list<value_type> ListType;

  // In this first implementation, it would be easier to use a vector
  // instead of a pointer to a vector, which requires dynamic allocation.
  // However, we use a ptr to a vector so that, in the final
  // implementation, swap() can be implemented in constant time.
  vector<ListType>* mElems;
  int mSize;
  Compare mComp;
  Hash mHash;
};

#include "hashmap.cpp"
