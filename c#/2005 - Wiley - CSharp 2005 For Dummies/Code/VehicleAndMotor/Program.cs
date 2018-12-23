// VehicleAndMotor - create a car and attach a motor.
//                   Demonstrate how to access members of the
//                   vehicle and the motor.
using System;

namespace VehicleAndMotor
{
  public class Program
  {
    public static void Main(string[] args)
    {
      // first create a Motor
      Motor largerMotor = new Motor();
      largerMotor.nPower = 230;
      largerMotor.displacement = 4.0;

      // now create the car
      Vehicle sonsCar = new Vehicle();
      sonsCar.sModel = "Cherokee Sport";
      sonsCar.sManufacturer = "Jeep";
      sonsCar.nNumOfDoors = 2;
      sonsCar.nNumOfWheels = 4;

      // attach the motor to the car
      sonsCar.motor = largerMotor;

      Motor m = sonsCar.motor;
      Console.WriteLine("The motor displacement is "
                       + m.displacement);

      Console.WriteLine("The motor displacement is "
                       + sonsCar.motor.displacement);
 
      // wait for user to acknowledge the results
      Console.WriteLine("Press Enter to terminate...");
      Console.Read();
    }
  }
  public class Motor
  {
    public int nPower;
    public double displacement;    // engine displacement [liter]
  }
  public class Vehicle
  {
    public string sModel;		// name of the model
    public string sManufacturer;   // ditto
    public int nNumOfDoors;		// the number of doors on the vehicle
    public int nNumOfWheels;		// you get the idea
    public Motor motor;
  }
}

