#include <sstream>
#include <xercesc/util/PlatformUtils.hpp>
#include <xercesc/framework/MemBufInputSource.hpp>
#include <xercesc/dom/DOM.hpp>
#include <xercesc/parsers/XercesDOMParser.hpp>
#include <xercesc/util/XMLString.hpp>

#include "Simple.h"
#include "XMLElement.h"

XERCES_CPP_NAMESPACE_USE
using namespace std;

string Simple::toXML() 
{
  XMLElement simpleElement;
  simpleElement.setElementName("simple");
  simpleElement.setAttribute("name", mName);

  // convert the int into a string
  ostringstream tempStream;
  tempStream << mPriority;
  simpleElement.setAttribute("priority", tempStream.str());

  // add the data as a text node
  simpleElement.setTextNode(mData);

  // convert the XMLElement into a string
  ostringstream resultStream;
  resultStream << simpleElement;

  return resultStream.str();
}

void Simple::fromXML(const string& inString)
{
  static const char* bufID = "simple buffer";

  MemBufInputSource src((const XMLByte*)inString.c_str(), inString.length(), bufID);
  XercesDOMParser* parser = new XercesDOMParser();
  parser->parse(src);

  DOMNode* node = parser->getDocument();
  DOMDocument* document = dynamic_cast<DOMDocument*>(node);
  if (document == NULL) {
    delete parser;
    return;
  }

  // document should be the <simple> element
  try {
    const DOMElement& elementNode = dynamic_cast<const DOMElement&>(*document->getDocumentElement());

    // get the name attribute
    XMLCh* nameKey = XMLString::transcode("name");
    char* name = XMLString::transcode(elementNode.getAttribute(nameKey));
    XMLString::release(&nameKey);
    mName = name;
    XMLString::release(&name);

    // get the priority attribute
    XMLCh* priorityKey = XMLString::transcode("priority");
    char* priorityStr = XMLString::transcode(elementNode.getAttribute(priorityKey));
    XMLString::release(&priorityKey);
    // parse the priority number
    istringstream tmpStream(priorityStr);
    tmpStream >> mPriority;
    XMLString::release(&priorityStr);

    // get the data as a text node
    const XMLCh* textData = elementNode.getTextContent();
    char* data = XMLString::transcode(textData);
    mData = data;
    XMLString::release(&data);

  } catch (bad_cast) {
    cerr << "cast exception while parsing Simple object from XML" << endl;
  } catch (...) {
    cerr << "an unknown error occurred while parsing a Simple object from XML" << endl;
  }
  
  delete parser;
}

int main()
{
  XMLPlatformUtils::Initialize();

  Simple test;
  test.mName = "myname";
  test.mPriority = 7;
  test.mData = "my data";

  string xmlData = test.toXML();

  Simple test2;
  test2.fromXML(xmlData);

  if (test.mName == test2.mName) {
    cout << "Names are equivalent!" << endl;
  } else {
    cout << "ERROR: Names are not equivalent!" << endl;
  }

  if (test.mPriority == test2.mPriority) {
    cout << "Priorities are equivalent!" << endl;
  } else {
    cout << "ERROR: Priorities are not equivalent!" << endl;
  } 

  if (test.mData == test2.mData) {
    cout << "Data is equivalent!" << endl;
  } else {
    cout << "ERROR: Data is not equivalent!" << endl;
  }

  XMLPlatformUtils::Terminate();
}
