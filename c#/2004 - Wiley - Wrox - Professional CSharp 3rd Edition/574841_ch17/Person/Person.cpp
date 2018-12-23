#include <iostream>
#include <string>
using namespace std;

class Person
{
public:
  Person();
  Person(const string& inFirstName, const string& inLastName, int inAge);
  ~Person();
  Person(const Person& rhs);
  string getFirstName() { return firstName; }
  string getLastName() { return lastName; }
  int getAge() { return age; }

  friend ostream& operator<<(ostream& ostr, const Person& p);

private:
  string firstName, lastName;
  int age;
};

Person::Person() : age(0)
{
}

Person::Person(const string& inFirstName, const string& inLastName, int inAge)
  : firstName(inFirstName), lastName(inLastName), age(inAge)
{
  cout << "Ctor\n";
}

Person::~Person()
{
  cout << "dtor\n";
}

Person::Person(const Person& rhs)
{
  cout << "Copy ctor\n";
}

ostream& operator<<(ostream& ostr, const Person& p)
{
  ostr << p.firstName << " " << p.lastName <<
    " " << p.age << endl;

  return (ostr);
}


/*
void processPerson(Person p)
{
  // process the person
}
*/

void processPerson(const Person& p)
{
  // process the person
}

Person createPerson()
{
  Person newP;
  return (newP);
}

int main(int argc, char** argv)
{
  Person me("Nicholas", "Solter", 28);
  processPerson(me);

  cout << createPerson();
  createPerson();

  return (0);
}
