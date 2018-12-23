/**
 * Note to reader:
 * 
 * These classes are a great starting point for testing out 
 * the edge cases of multiple inheritance. For example, you
 * can start with these classes to build a diamond hierarchy 
 * and turn it into a virtual base class.
 */

#include <iostream>

using namespace std;

class Dog
{
public:
  virtual void bark() { cout << "woof!" << endl; }
};

class Bird
{
public:
  virtual void chirp() { cout << "chirp!" << endl; }
};

class DogBird : public Dog, public Bird
{
};

int main(int argc, char** argv)
{
  DogBird myConfusedAnimal;

  myConfusedAnimal.bark();
  myConfusedAnimal.chirp();
} 
