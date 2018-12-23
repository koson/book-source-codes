#include <xercesc/util/PlatformUtils.hpp>

#include <xercesc/dom/DOM.hpp>
#include <xercesc/parsers/XercesDOMParser.hpp>
#include <xercesc/util/XMLString.hpp>

#include <iostream>

XERCES_CPP_NAMESPACE_USE
using namespace std;

void printNode(const DOMNode* inNode);

int main()
{
  XMLPlatformUtils::Initialize();

  XercesDOMParser* parser = new XercesDOMParser();
  parser->parse("test.xml");

  DOMNode* node = parser->getDocument();
  DOMDocument* document = dynamic_cast<DOMDocument*>(node);
  printNode(document->getDocumentElement());

  delete parser;
  XMLPlatformUtils::Terminate();
}

void printNode(const DOMNode* inNode)
{
  try {
    const DOMText& textNode = dynamic_cast<const DOMText&>(*inNode);
    char* text = XMLString::transcode(textNode.getData());
    cout << "Found text data: " << text << endl;
    XMLString::release(&text);
  } catch (bad_cast) {
    // not a text node..
  }

  try {
    const DOMElement& elementNode = dynamic_cast<const DOMElement&>(*inNode);
    char* tagName = XMLString::transcode(elementNode.getTagName());
    cout << "Found tag named: " << tagName << endl;
    XMLString::release(&tagName);

    // Look at the attribute list
    DOMNamedNodeMap* attributes = elementNode.getAttributes();
    for (int i = 0; i < attributes->getLength(); i++) {
      try {
	const DOMAttr& attrNode = dynamic_cast<const DOMAttr&>(*attributes->item(i));
	char* name = XMLString::transcode(attrNode.getName());
	char* value = XMLString::transcode(attrNode.getValue());
	cout << "Found attribute pair: (" << name << "=" << value << ")" << endl;
	XMLString::release(&name);
	XMLString::release(&value);
      } catch (bad_cast) {
	cerr << "A non-attribute node was found in the attribute list!" << endl;
      }
    }
  } catch (bad_cast) {
    // not an element node...
  }

  // print any sub-elements
  DOMNodeList* children = inNode->getChildNodes();
  for (int i = 0; i < children->getLength(); i++) {
    printNode(children->item(i));
  }
  
}
