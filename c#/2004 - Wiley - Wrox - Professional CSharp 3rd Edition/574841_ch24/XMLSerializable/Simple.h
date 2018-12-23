#include "XMLSerializable.h"

class Simple : public XMLSerializable
{
 public:
  std::string mName;
  int    mPriority;

  std::string mData;

  virtual std::string toXML();
  virtual void fromXML(const std::string& inXML);
};
