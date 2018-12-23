// AirlineTicketTest.cpp

#include <iostream>
#include "AirlineTicket.h"

using namespace std;

int main(int argc, char** argv)
{
  AirlineTicket myTicket;  // stack-based AirlineTicket
    
  myTicket.setPassengerName("Sherman T. Socketwrench");
  myTicket.setNumberOfMiles(700);
  int cost = myTicket.calculatePriceInDollars();
  cout << "This ticket will cost $" << cost << endl;

  AirlineTicket* myTicket2; // heap-based AirlineTicket

  myTicket2 = new AirlineTicket(); // allocate a new object
  myTicket2->setPassengerName("Laudimore M. Hallidue");
  myTicket2->setNumberOfMiles(2000);
  myTicket2->setHasEliteSuperRewardsStatus(true);
  int cost2 = myTicket2->calculatePriceInDollars();
  cout << "This other ticket will cost $" << cost2 << endl;
  delete myTicket2;

  return 0;
} 
