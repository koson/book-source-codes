using System;
using System.Collections;
using IWshRuntimeLibrary;

namespace EnumPrinterConnections
{
	class AllPrintersClass
	{
		[STAThread]
		static void Main(string[] args)
		{
			WshNetwork network = new WshNetwork();
			foreach (IEnumerable printer in
				network.EnumPrinterConnections())
			{
				Console.WriteLine(printer.ToString());
			}

			Console.ReadLine();
		}
	}
}
