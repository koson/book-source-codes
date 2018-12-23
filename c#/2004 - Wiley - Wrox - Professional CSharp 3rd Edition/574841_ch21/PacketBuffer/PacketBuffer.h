#include <queue>
#include <stdexcept>
using std::queue;

template <typename T>
class PacketBuffer
{
 public:
  //
  // If maxSize is non-positive, the size is unlimited.
  // Otherwise only maxSize packets are allowed in
  // the buffer at any one time.
  //
  PacketBuffer(int maxSize = -1);

  //
  // Stores the packet in the buffer.
  // Throws overflow_error is the buffer is full.
  //
  void bufferPacket(const T& packet);

  //
  // Returns the next packet. Throws out_of_range
  // if the buffer is empty.
  //
  T getNextPacket() throw (std::out_of_range);

 protected:
  queue<T> mPackets;
  int mMaxSize;

 private:
  // prevent assignment and pass-by-value
  PacketBuffer(const PacketBuffer& src);
  PacketBuffer& operator=(const PacketBuffer& rhs);
};

template <typename T>
PacketBuffer<T>::PacketBuffer(int maxSize)
{
  mMaxSize = maxSize;
}

template <typename T>
void PacketBuffer<T>::bufferPacket(const T& packet)
{
  if (mMaxSize > 0 && mPackets.size() == static_cast<size_t>(mMaxSize)) {
    // No more space. Just drop the packet.
    return;
  }

  mPackets.push(packet);
}

template <typename T>
T PacketBuffer<T>::getNextPacket() throw (std::out_of_range)
{
  if (mPackets.empty()) {
    throw (std::out_of_range("Buffer is empty"));
  }
  // retrieve the head element
  T temp = mPackets.front();
  // pop the head element
  mPackets.pop();
  // return the head element
  return (temp);
}
