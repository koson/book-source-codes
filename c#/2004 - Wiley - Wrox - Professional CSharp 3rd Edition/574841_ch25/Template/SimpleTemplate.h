/**
 * SimpleTemplate.h
 */

template <typename T>
class SimpleTemplate
{
 public:
  SimpleTemplate(T &inObject);

  const T& get();
  void set(T& inObject);

 protected:
  T& mObject;
};

#include "SimpleTemplate.cpp" // include the implementation!
