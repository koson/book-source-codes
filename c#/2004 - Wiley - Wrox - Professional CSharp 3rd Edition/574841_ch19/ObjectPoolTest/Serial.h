// Serial.h

class Serial
{
 public:
  Serial();

  int getSerialNumber() const;

 protected:
  static int sNextSerial;

  int mSerialNumber;
};

