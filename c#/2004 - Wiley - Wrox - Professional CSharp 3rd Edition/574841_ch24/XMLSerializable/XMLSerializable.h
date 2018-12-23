#include <string>

class XMLSerializable
{
 public:
  virtual std::string toXML() = 0;
  virtual void fromXML(const std::string& inXML) = 0;
};
