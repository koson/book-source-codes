using System;
using System.Collections;
using IWshRuntimeLibrary;

namespace EnumNetworkDrives
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{

			WshNetwork network = new WshNetwork();

			foreach (IEnumerable driver in network.EnumNetworkDrives())
			{
				Console.WriteLine(driver.ToString());
			}

			Console.ReadLine();
		}
	}
}
