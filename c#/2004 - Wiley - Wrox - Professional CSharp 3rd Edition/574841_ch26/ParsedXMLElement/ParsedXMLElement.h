// ParsedXMLElement.h

#include <string>
#include <vector>

#include <xercesc/util/PlatformUtils.hpp>
#include <xercesc/parsers/XercesDOMParser.hpp>
#include <xercesc/dom/DOM.hpp>

XERCES_CPP_NAMESPACE_USE

/**
 * Note: If the root element is deleted, subelements become
 * invalid.
 */
class ParsedXMLElement
{
 public:
  ParsedXMLElement(const std::string& inFilename);
  ~ParsedXMLElement();

  std::string getName() const;
  std::string getTextData() const;
  std::string getAttributeValue(const std::string& inKey) const;
  // The caller is responsible for freeing the ParsedXMLElements
  // pointed to by the elements of the vector.
  std::vector<ParsedXMLElement*> getSubElements() const;

 protected:
  // This constructor is used internally to create subelements.
  ParsedXMLElement(DOMElement* inElement);

  XercesDOMParser* mParser;
  DOMElement*      mElement;
  static int       sReferences;

 private:
  // Disallow copy construction and op=.
  ParsedXMLElement(const ParsedXMLElement&);
  ParsedXMLElement& operator=(const ParsedXMLElement& rhs);
};
