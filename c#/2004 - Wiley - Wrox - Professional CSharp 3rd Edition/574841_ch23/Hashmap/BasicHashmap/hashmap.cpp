// Throws invalid_argument if numBuckets is non-positive
template <typename T>
DefaultHash<T>::DefaultHash(int numBuckets) throw (invalid_argument)
{
  if (numBuckets <= 0) {
    throw (invalid_argument("numBuckets must be > 0"));
  }
  mNumBuckets = numBuckets;
}

// Uses the division method for hashing.
// Treats the key as a sequence of bytes, sums the ASCII
// values of the bytes, and mods the total by the number
// of buckets.
template <typename T>
int DefaultHash<T>::hash(const T& key) const
{
  int bytes = sizeof(key);
  unsigned long res = 0;
  for (int i = 0; i < bytes; ++i) {
    res += *((char*)&key + i);
  }
  return (res % mNumBuckets);
}

// Throws invalid_argument if numBuckets is non-positive
DefaultHash<string>::DefaultHash(int numBuckets) throw (invalid_argument)
{
  if (numBuckets <= 0) {
    throw (invalid_argument("numBuckets must be > 0"));
  }
  mNumBuckets = numBuckets;
}

// Uses the division method for hashing after summing the
// ASCII values of all the characters in key.
int DefaultHash<string>::hash(const string& key) const
{
  int sum = 0;

  for (size_t i = 0; i < key.size(); i++) {
    sum += key[i];
  }
  return (sum % mNumBuckets);
}

// Construct mElems with the number of buckets.
template <typename Key, typename T, typename Compare, typename Hash>
hashmap<Key, T, Compare, Hash>::hashmap(
					const Compare& comp, const Hash& hash) throw(invalid_argument) :
  mSize(0), mComp(comp), mHash(hash)
{
  if (mHash.numBuckets() <= 0) {
    throw (invalid_argument("Number of buckets must be positive"));
  }
  mElems = new vector<list<value_type> >(mHash.numBuckets());
}

template <typename Key, typename T, typename Compare, typename Hash>
hashmap<Key, T, Compare, Hash>::~hashmap()
{
  delete mElems;
}

template <typename Key, typename T, typename Compare, typename Hash>
hashmap<Key, T, Compare, Hash>::hashmap(const hashmap<
					Key, T, Compare, Hash>& src) :
  mSize(src.mSize), mComp(src.mComp), mHash(src.mHash)
{
  // Don't need to bother checking if numBuckets is positive, because
  // we know src checked.

  // Use the vector copy constructor
  mElems = new vector<list<value_type> >(*(src.mElems));
}

template <typename Key, typename T, typename Compare, typename Hash>
hashmap<Key, T, Compare, Hash>& hashmap<Key, T, Compare, Hash>::operator=(
									  const hashmap<Key, T, Compare, Hash>& rhs)
{
  // check for self-assignment
  if (this != &rhs) {
    delete mElems;
    mSize = rhs.mSize;
    mComp = rhs.mComp;
    mHash = rhs.mHash;
    // Don't need to bother checking if numBuckets is positive, because
    // we know rhs checked

    // Use the vector copy constructor
    mElems = new vector<list<value_type> >(*(rhs.mElems));
  }
}

template <typename Key, typename T, typename Compare, typename Hash>
typename list<pair<const Key, T> >::iterator
hashmap<Key, T, Compare, Hash>::findElement(const key_type& x, int& bucket) const
{
  // hash the key to get the bucket
  bucket = mHash.hash(x);

  // Look for the key in the bucket
  for (typename ListType::iterator it = (*mElems)[bucket].begin();
       it != (*mElems)[bucket].end(); ++it) {
    if (mComp(it->first, x)) {
      return (it);
    }
  }
  return ((*mElems)[bucket].end());
}

template <typename Key, typename T, typename Compare, typename Hash>
typename hashmap<Key, T, Compare, Hash>::value_type*
hashmap<Key, T, Compare, Hash>::find(const key_type& x)
{
  int bucket;
  // Use the findElement() helper
  typename ListType::iterator it = findElement(x, bucket);
  if (it == (*mElems)[bucket].end()) {
    // we didn't find the element -- return NULL
    return (NULL);
  }
  // We found the element. Return a pointer to it.
  return (&(*it));
}

template <typename Key, typename T, typename Compare, typename Hash>
T& hashmap<Key, T, Compare, Hash>::operator[] (const key_type& x)
{
  // Try to find the element.
  // If it doesn't exist, add a new element.
  value_type* found = find(x);
  if (found == NULL) {
    insert(make_pair(x, T()));
    found = find(x);
  }
  return (found->second);
}

template <typename Key, typename T, typename Compare, typename Hash>
void hashmap<Key, T, Compare, Hash>::insert(const value_type& x)
{
  int bucket;
  // Try to find the element
  typename ListType::iterator it = findElement(x.first, bucket);

  if (it != (*mElems)[bucket].end()) {
    // The element already exists
    return;
  } else {
    // We didn't find the element, so insert a new one.
    mSize++;
    (*mElems)[bucket].insert((*mElems)[bucket].end(), x);
  }
}

template <typename Key, typename T, typename Compare, typename Hash>
void
hashmap<Key, T, Compare, Hash>::erase(const key_type& x)
{
  int bucket;

  // First, try to find the element
  typename ListType::iterator it = findElement(x, bucket);

  if (it != (*mElems)[bucket].end()) {
    // The element already exists -- erase it
    (*mElems)[bucket].erase(it);
    mSize--;
  }
}
