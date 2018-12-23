// VehicleDataOnly - create a Vehicle object, populate its
//									 members from	the keyboard and then write it
//									 back	out
using System;

namespace VehicleDataOnly
{
  public class Vehicle
  {
		public string	sModel;				 //	name of the model
		public string	sManufacturer; // ditto
		public int nNumOfDoors;			 //	the number of doors on the vehicle
		public int nNumOfWheels;		 //	you get the idea
  }

  public class Program
  {
		// This	is where the program starts
		static void	Main(string[] args)
	  {
			// prompt	user to enter her name
			Console.WriteLine("Enter the properties of your vehicle");
		  
			// create	an instance of Vehicle
			Vehicle	myCar = new Vehicle();
		  
			// populate	a data member via a temporary variable
			Console.Write("Model name = ");
			string s = Console.ReadLine();
			myCar.sModel = s;
		  
			// or	you can populate the data member directly
			Console.Write("Manufacturer name = ");
			myCar.sManufacturer	= Console.ReadLine();

			// enter the remainder of	the data
      // a temp is useful for reading ints
			Console.Write("Number of doors = ");
			s	= Console.ReadLine();
			myCar.nNumOfDoors	= Convert.ToInt32(s);

			Console.Write("Number of wheels = ");
			s	= Console.ReadLine();
			myCar.nNumOfWheels = Convert.ToInt32(s);
		  
			// now display the results
			Console.WriteLine("\nYour vehicle is a ");
			Console.WriteLine(myCar.sManufacturer	+ " " + myCar.sModel);
			Console.WriteLine("with " + myCar.nNumOfDoors + " doors, "
											+	"riding on " + myCar.nNumOfWheels
											+	" wheels");

			// wait	for user to acknowledge the results
			Console.WriteLine("Press Enter to terminate...");
			Console.Read();
	  }
  }
}

