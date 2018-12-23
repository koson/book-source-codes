#include "CarFactory.h"
#include <vector>

using namespace std;

CarFactory* getLeastBusyFactory(const vector<CarFactory*>& inFactories)
{
  if (inFactories.size() == 0) return NULL;

  CarFactory* bestSoFar = inFactories[0];


  for (size_t i = 1; i < inFactories.size(); i++)
    {
      if (inFactories[i]->getNumCarsInProduction() <
	  bestSoFar->getNumCarsInProduction()) {
	bestSoFar = inFactories[i];
      }
    }

  return bestSoFar;
}

int main(int argc, char** argv)
{
  vector<CarFactory*> factories;

  // Create 3 Ford factories and 1 Toyota factory. 
  FordFactory* factory1 = new FordFactory();
  FordFactory* factory2 = new FordFactory();
  FordFactory* factory3 = new FordFactory();
  ToyotaFactory* factory4 = new ToyotaFactory();

  // To get more interesting results, pre-preorder some cars.
  factory1->requestCar();
  factory1->requestCar();
  factory2->requestCar();
  factory4->requestCar();

  // Add the factories to a vector.
  factories.push_back(factory1);
  factories.push_back(factory2);
  factories.push_back(factory3);
  factories.push_back(factory4);

  // Build 10 cars from the least busy factory.
  for (int i = 0; i < 10; i++) {
    CarFactory* currentFactory = getLeastBusyFactory(factories);
    Car* theCar = currentFactory->requestCar();
    theCar->info();
  }
}
