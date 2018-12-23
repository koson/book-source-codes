#include "ParsedXMLElement.h"

#include <xercesc/util/XMLString.hpp>

#include <iostream>

XERCES_CPP_NAMESPACE_USE
using namespace std;

// No references by default
int ParsedXMLElement::sReferences = 0;

ParsedXMLElement::ParsedXMLElement(const std::string& inFilename)
{
  if (sReferences == 0) {
    // First element -- initialize the library
    XMLPlatformUtils::Initialize();
  }
  sReferences++;

  mParser = new XercesDOMParser();
  mParser->parse(inFilename.c_str());

  DOMNode* node = mParser->getDocument();
  DOMDocument* document = dynamic_cast<DOMDocument*>(node);
  if (document == NULL) {
    cerr << "WARNING: No XML document!" << endl;
    return;
  }

  mElement = dynamic_cast<const DOMElement*>(document->getDocumentElement());
  if (mElement == NULL) {
    cerr << "WARNING: XML Document had no root element!" << endl;
  }
}

ParsedXMLElement::~ParsedXMLElement()
{
  if (mParser != NULL) {
    // This is the root element.
    delete mParser;

    sReferences--;
    if (sReferences == 0) {
      // Last element destroyed
      XMLPlatformUtils::Terminate();
    }
  }
}

string ParsedXMLElement::getName() const
{
  char* tagName = XMLString::transcode(mElement->getTagName());
  string result(tagName);
  XMLString::release(&tagName);

  return result;
}

string ParsedXMLElement::getTextData() const
{
  // We assume that the first text node we reach is the one we want. 
  DOMNodeList* children = mElement->getChildNodes();
  for (int i = 0; i < children->getLength(); i++) {
    DOMText* textNode = dynamic_cast<DOMText*>(children->item(i));
    if (textNode != NULL) {
      char* textData = XMLString::transcode(textNode->getData());
      string result(textData);
      XMLString::release(&textData);
      return result;
    }
  }

  // No text nodes were found.
  return "";
}

string ParsedXMLElement::getAttributeValue(const std::string& inKey) const
{
  XMLCh* key = XMLString::transcode(inKey.c_str());

  const XMLCh* value = mElement->getAttribute(key);
  XMLString::release(&key);

  char* valueString = XMLString::transcode(value);
  string result(valueString);
  XMLString::release(&valueString);

  return result;
}

vector<ParsedXMLElement*> ParsedXMLElement::getSubElements() const
{
  vector<ParsedXMLElement*> result;

  DOMNodeList* children = mElement->getChildNodes();
  for (int i = 0; i < children->getLength(); i++) {
    DOMElement* elNode = dynamic_cast<DOMElement*>(children->item(i));
    if (elNode != NULL) {
      result.push_back(new ParsedXMLElement(elNode));
    }
  }

  return result;
}

ParsedXMLElement::ParsedXMLElement(DOMElement* inElement)
{
  mParser = NULL; // No parser for a subelement
  mElement = inElement;
} 

int main(int argc, char** argv)
{
  ParsedXMLElement e("test.xml");
  cout << "root name: " << e.getName() << endl;

  vector<ParsedXMLElement*> subelements = e.getSubElements();
  for (vector<ParsedXMLElement*>::iterator it = subelements.begin();
       it != subelements.end(); ++it) {
    cout << "subelement name: " << (*it)->getName() << endl;
    cout << "subelement speaker: " << (*it)->getAttributeValue("speaker") 
	 << endl;
    cout << "subelement text data: " << (*it)->getTextData() << endl;
  }

  for (vector<ParsedXMLElement*>::iterator it = subelements.begin();
       it != subelements.end(); ++it) {
    delete *it;
  }

  return 0;
}
