#include <stdexcept>
#include <iostream>

void throwIf(bool inShouldThrow) throw (std::runtime_error) 
{
  if (inShouldThrow) {
    throw std::runtime_error("Here's my exception");
  }
}

int main(int argc, char** argv)
{
  try {
    throwIf(false); // doesn't throw
    throwIf(true);  // throws!
  } catch (const std::runtime_error& exception) {
    std::cerr << "Caught exception: " << exception.what() << std::endl;
  }
}
