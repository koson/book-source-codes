#include "Serial.h"

Serial::Serial()
{
  mSerialNumber = sNextSerial++;
}

int Serial::getSerialNumber() const
{
  return mSerialNumber;
}

int Serial::sNextSerial = 0;
