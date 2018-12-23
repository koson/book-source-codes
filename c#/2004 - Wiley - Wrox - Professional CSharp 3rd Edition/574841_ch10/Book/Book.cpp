#include <iostream>
#include <string>

using namespace std;

class Book
{
public:
  virtual string getDescription() { return "Book"; }
};

class Paperback : public Book
{
public:
  virtual string getDescription() { 
    return "Paperback " + Book::getDescription(); 
  }
};

class Romance : public Paperback
{
public:
  virtual string getDescription() { 
    return "Romance " + Paperback::getDescription(); 
  }
};

class Technical : public Book
{
public:
  virtual string getDescription() { 
    return "Technical " + Book::getDescription();
  }
};

int main()
{
  Romance novel;
  Book book;
  cout << novel.getDescription() << endl; // outputs "Romance Paperback Book"
  cout << book.getDescription() << endl;  // outputs "Book"
}
