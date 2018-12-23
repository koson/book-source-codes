#include "PacketBuffer.h"
#include <iostream>
using namespace std;

class IPPacket {};

int main(int argc, char** argv)
{
  PacketBuffer<IPPacket> ipPackets(3);

  ipPackets.bufferPacket(IPPacket());
  ipPackets.bufferPacket(IPPacket());
  ipPackets.bufferPacket(IPPacket());
  ipPackets.bufferPacket(IPPacket());

  while (true) {
    try {
      IPPacket packet = ipPackets.getNextPacket();
    } catch (out_of_range&) {
      cout << "Processed all packets!" << endl;
      break;
    }
  }
  return (0);
}
