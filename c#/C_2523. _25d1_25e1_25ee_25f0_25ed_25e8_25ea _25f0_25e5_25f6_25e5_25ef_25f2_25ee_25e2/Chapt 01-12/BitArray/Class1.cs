using System;

namespace BitArray_example
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			bool[] bits1 = {true, false, false, true, true};
			System.Collections.BitArray Bits1 = new System.Collections.BitArray(bits1);

			bool[] bits2 = {false, false, false, true, false};
			System.Collections.BitArray Bits2 = new System.Collections.BitArray(bits2);

			foreach (object obj in Bits1)
			{
				Console.WriteLine(obj.ToString());
			}

			Console.ReadLine();
		}
	}
}
