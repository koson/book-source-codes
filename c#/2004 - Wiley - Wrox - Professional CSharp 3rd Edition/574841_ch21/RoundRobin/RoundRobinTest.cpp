#include "RoundRobin.h"
#include <stdexcept>
#include <iostream>
using namespace std;

//
// Forward declaration for NetworkRequest
// Implementation details omitted
//
class NetworkRequest {};

//
// Simple Host class that serves as a proxy for a physical machine.
// Implementation details omitted.
//
class Host
{
public:
  //
  // Implementation of processRequest would forward
  // the request to the network host represented by the
  // object. Omitted here.
  //
  void processRequest(NetworkRequest& request) {} 
};

//
// Simple load balancer that distributes incoming requests
// to its hosts using a round-robin scheme.
//
class LoadBalancer
{
public:
  //
  // Constructor takes a vector of hosts.
  //
  LoadBalancer(const vector<Host>& hosts);
  ~LoadBalancer() {}

  //
  // Ship the incoming request to the next host using
  // a round-robin scheduling algorithm
  //
  void distributeRequest(NetworkRequest& request);

protected:
  RoundRobin<Host> rr;
};

LoadBalancer::LoadBalancer(const vector<Host>& hosts)
{
  // add the hosts
  for (size_t i = 0; i < hosts.size(); ++i) {
    rr.add(hosts[i]);
  }
}

void LoadBalancer::distributeRequest(NetworkRequest& request)
{
  try {
    rr.getNext().processRequest(request);
  } catch (out_of_range& e) {
    cerr << "No more hosts.\n";
  }
}

int main(int argc, char** argv)
{
  vector<Host> hosts;
  hosts.push_back(Host());
  hosts.push_back(Host());
  hosts.push_back(Host());
  LoadBalancer lb(hosts);
  NetworkRequest r;
  lb.distributeRequest(r);
  lb.distributeRequest(r);
  lb.distributeRequest(r);
  lb.distributeRequest(r);
  lb.distributeRequest(r);
  lb.distributeRequest(r);
}
