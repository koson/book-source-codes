#include <vector>
#include <list>
#include <stdexcept>
#include <iterator> 
#include <string>
using std::vector;
using std::list;
using std::invalid_argument;
using std::string;
using std::pair;

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

template <typename Key, typename T, typename Compare, typename Hash>
class hashmap;


// HashIterator class definition
template<typename Key, typename T, typename Compare, typename Hash>
class HashIterator : public std::iterator<std::bidirectional_iterator_tag,
    pair<const Key, T> >
{
  // The iterator class needs access to protected members of the hashmap
  friend class hashmap<Key, T, Compare, Hash>;

    public:
        HashIterator(); // bi-directional iterators must supply default ctors
        HashIterator(int bucket,
            typename list<pair<const Key, T> >::iterator listIt,
            const hashmap<Key, T, Compare, Hash>* inHashmap);

        pair<const Key, T>& operator*() const;

        // Return type must be something to which -> can be applied.
        // Return a pointer to a pair<const Key, T>, to which the compiler will
        // apply -> again
        pair<const Key, T>* operator->() const;
        HashIterator<Key, T, Compare, Hash>& operator++();
        const HashIterator<Key, T, Compare, Hash> operator++(int);

        HashIterator<Key, T, Compare, Hash>& operator--();
        const HashIterator<Key, T, Compare, Hash> operator--(int);

        // don't need to define a copy constructor or operator= because the
        // default behavior is what we want.

        // don't need destructor because the default behavior
        // (not deleting mHashmap) is what we want.

        // These are ok as member functions because we don't support
        // comparisons of different types to this one
        bool operator==(const HashIterator& rhs) const;
        bool operator!=(const HashIterator& rhs) const;

    protected:
        int mBucket;
        typename list<pair<const Key, T> >::iterator mIt;
        const hashmap<Key, T, Compare, Hash>* mHashmap;

        // Helper methods for operator++ and operator--
        void increment();
        void decrement();
};

template <typename Key, typename T, typename Compare = std::equal_to<Key>,
	  typename Hash = DefaultHash<Key> >
class hashmap
{
  public:
  typedef Key key_type;
  typedef T mapped_type;
  typedef pair<const Key, T> value_type;
  typedef Compare key_compare;
  typedef pair<const Key, T>& reference;
  typedef const pair<const Key, T>& const_reference;
  typedef size_t size_type;
  typedef ptrdiff_t difference_type;
  typedef HashIterator<Key, T, Compare, Hash> iterator;
  typedef HashIterator<Key, T, Compare, Hash> const_iterator;

  // Required class definition for associative containers
  class value_compare :
  public std::binary_function<value_type, value_type, bool>
  {
    friend class hashmap<Key, T, Compare, Hash>;
  public:
    bool operator() (const value_type& x, const value_type& y) const
      {
	return comp(x.first, y.first);
      }
  protected:
    Compare comp;
    value_compare(Compare c) : comp(c) {}
  };

  // The iterator class needs access to protected members of the hashmap
  friend class HashIterator<Key, T, Compare, Hash>;

  // Iterator methods
  iterator begin();
  iterator end();
  const_iterator begin() const;
  const_iterator end() const;


  // Constructors
  // throws invalid_argument if the hash object specifies a non-positive
  // number of buckets.   
  explicit hashmap(const Compare& comp = Compare(),
		   const Hash& hash = Hash()) throw(invalid_argument);

  template <class InputIterator>
  hashmap(InputIterator first, InputIterator last,
	  const Compare& comp = Compare(), const Hash& hash = Hash())
  throw(invalid_argument);

  // destructor, copy constructor, assignment operator
  ~hashmap();
  hashmap(const hashmap<Key, T, Compare, Hash>& src);
  hashmap<Key, T, Compare, Hash>& operator=(const hashmap<
					    Key, T, Compare, Hash>& rhs);

  // Size methods
  bool empty() const;
  size_type size() const;
  size_type max_size() const;

  // element insert methods
  T& operator[] (const key_type& x);
  pair<iterator, bool> insert(const value_type& x);
  iterator insert(iterator position, const value_type& x);
        template <class InputIterator>
  void insert(InputIterator first, InputIterator last);

  // element delete methods
  void erase(iterator position);
  size_type erase(const key_type& x);
  void erase(iterator first, iterator last);


  // other modifying utilities
  void swap(hashmap<Key, T, Compare, Hash>& hashIn);

  void clear();

  // access methods for STL conformity
  key_compare key_comp() const;
  value_compare value_comp() const;

  // Lookup methods
  iterator find(const key_type& x);
  const_iterator find(const key_type& x) const;
  size_type count(const key_type& x) const;

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
