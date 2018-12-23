#include "Order.h"
#include <iostream>
#include <sstream>

using namespace std;

Order::Order() : mItemNumber(-1), mQuantity(-1), mCustomerNumber(-1)
{
}

int Order::getItemNumber() 
{ 
  return mItemNumber;
}

void Order::setItemNumber(int inItemNumber) 
{
  mItemNumber = inItemNumber;
}

int Order::getQuantity() 
{ 
  return mQuantity; 
}

void Order::setQuantity(int inQuantity) 
{ 
  mQuantity = inQuantity; 
}

int Order::getCustomerNumber() 
{ 
  return mCustomerNumber; 
}

void Order::setCustomerNumber(int inCustomerNumber) 
{ 
  mCustomerNumber = inCustomerNumber; 
}

string Order::serialize()
{
  ostringstream outStream;

  outStream << getItemNumber() << "\t" <<
    getQuantity() << "\t" <<
    getCustomerNumber();

  return outStream.str();
}

void Order::deserialize(const string& inData)
{
  // Create a string stream based on the data
  istringstream inStream(inData);
  if (!inStream.good()) {
    cerr << "Error deserializing!" << endl;
  } else {
    inStream >> mItemNumber;
    inStream >> mQuantity;
    inStream >> mCustomerNumber;
  }
}


int main()
{
  Order myOrder;

  myOrder.setItemNumber(7);
  myOrder.setQuantity(2);
  myOrder.setCustomerNumber(178);

  string data = myOrder.serialize();

  Order myOrder2;

  myOrder2.deserialize(data);

  // myOrder2 should be the same as myOrder
  cout << myOrder2.getItemNumber() << "\n"
       << myOrder2.getQuantity() << "\n"
       << myOrder2.getCustomerNumber() << "\n";
}
