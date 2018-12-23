using System;

namespace ProcessorCount
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			int am = System.Diagnostics.Process.GetCurrentProcess().ProcessorAffinity.ToInt32();
			int processorCount = 0;
			while( am != 0 )
			{    
				processorCount++;
				am &= ( am - 1 );
			}

			Console.WriteLine(processorCount);

			Console.ReadLine();
		}
	}
}
