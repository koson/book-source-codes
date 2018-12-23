#include "ObjectPool.h"

class UserRequest
{
public:
  UserRequest() {}
  ~UserRequest() {}

  // Methods to populate the request with specific information.
  // Methods to retrieve the request data.
  // (not shown)
         
protected:
  // data members (not shown)
};

UserRequest& obtainUserRequest(ObjectPool<UserRequest>& pool)
{
  // Obtain a UserRequest object from the pool
  UserRequest& request = pool.acquireObject();

  // populate the request with user input
  // (not shown)

  return (request);
}

void processUserRequest(ObjectPool<UserRequest>& pool, UserRequest& req)
{
  // process the request
  // (not shown)

  // return the request to the pool
  pool.releaseObject(req);
}

int main(int argc, char** argv)
{
  ObjectPool<UserRequest> requestPool(1000);

  // Set up program
  // (not shown)

  while (true /* program is running */) {
    UserRequest& req = obtainUserRequest(requestPool);
    processUserRequest(requestPool, req);
  }

  return (0);
}
