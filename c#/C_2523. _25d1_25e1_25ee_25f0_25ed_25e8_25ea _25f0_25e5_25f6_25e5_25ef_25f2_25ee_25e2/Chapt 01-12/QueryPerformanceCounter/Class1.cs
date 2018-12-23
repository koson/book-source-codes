using System;

namespace QueryPerformanceCounter
{
	class Class1
	{
		[System.Runtime.InteropServices.DllImport("kernel32.dll")]
		extern static short QueryPerformanceCounter(ref long x);
		[System.Runtime.InteropServices.DllImport("kernel32.dll")]
		extern static short QueryPerformanceFrequency(ref long x);

		[System.Diagnostics.Conditional("DEBUG")]
		static void DbgMsg()
		{
			Console.WriteLine("DEBUG!!!");
		}

		[STAThread]
		static void Main(string[] args)
		{
			System.Collections.Stack stack = new System.Collections.Stack();

			// TickCount is less accurate property
			int start = Environment.TickCount;
			// Ticks are measuring in 100 nanoseconds intervals
			long startLong = DateTime.Now.Ticks;

			long ctr1 = 0, ctr2 = 0, freq = 0;
			if (QueryPerformanceCounter(ref ctr1) != 0)	// Begin timing.
			{
				for (int i=0; i<800000; i++)
				{
					stack.Push(i);
				}
				QueryPerformanceCounter(ref ctr2);	// Finish timing.
				QueryPerformanceFrequency(ref freq);
				Console.WriteLine("QueryPerformanceCounter minimum resolution: 1/" + freq + " seconds.");
				Console.WriteLine("100 Increment time: " + (ctr2 - ctr1) * 1.0 / freq + " seconds.");
			}

			int end = Environment.TickCount;
			long endLong = DateTime.Now.Ticks;
			Console.WriteLine("TickCount property:" + (end - start));
			Console.WriteLine("Ticks property:" + (endLong - startLong));

			Console.ReadLine();
		}
	}
}
