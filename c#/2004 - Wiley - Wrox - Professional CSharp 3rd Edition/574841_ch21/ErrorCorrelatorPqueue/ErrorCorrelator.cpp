#include "ErrorCorrelator.h"
using namespace std;

bool operator<(const Error& lhs, const Error& rhs)
{
  return (lhs.mPriority < rhs.mPriority);
}

ostream& operator<<(ostream& str, const Error& err)
{
  str << err.mError << " (priority " << err.mPriority << ")";
  return (str);
}

void ErrorCorrelator::addError(const Error& error)
{
  mErrors.push(error);
}

Error ErrorCorrelator::getError() throw (out_of_range)
{
  //
  // If there are no more errors, throw an exception.
  //
  if (mErrors.empty()) {
    throw (out_of_range("No elements!"));
  }
  
  // Save the top element
  Error top = mErrors.top();
  // Remove the top element.
  mErrors.pop();
  // Return the saved element.
  return (top);
}
