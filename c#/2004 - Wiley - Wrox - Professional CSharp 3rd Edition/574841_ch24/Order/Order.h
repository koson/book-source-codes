#include <string>

class Order
{
 public:
  Order();

  int getItemNumber();
  void setItemNumber(int inItemNumber);

  int getQuantity();
  void setQuantity(int inQuantity);

  int getCustomerNumber();
  void setCustomerNumber(int inCustomerNumber);

  /**
   * Converts the object into raw data that can be sent over the 
   * network. 
   *
   * @return           a string representing this object
   */
  std::string serialize();

  /**
   * Adjusts this object to represent the data in inData
   *
   * @param inData     a string representing Order data
   */
  void deserialize(const std::string& inData);

 private:
  int mItemNumber;
  int mQuantity;
  int mCustomerNumber;
};
