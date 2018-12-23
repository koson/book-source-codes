#include "Spreadsheet.h"

void printSpreadsheet(Spreadsheet s)
{
  // code omitted for brevity
}

int main(int argc, char** argv)
{
  //Spreadsheet s1(4, 3);
  //printSpreadsheet(s1);
  
  Spreadsheet s1(2, 2), s2(4, 3);
  s1 = s2;

  return (0);
}
