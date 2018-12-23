using System;

namespace ArrayCopyTo
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			int [] iarray = new int[10]; // Исходный массив 
			for (int i=0; i< iarray.Length; i++)
				iarray[i] = i;

			double[] darray = new double[10]; // Новый массив
			iarray.CopyTo(darray, 0);

			for (int i=0; i< darray.Length; i++)
				Console.WriteLine(darray[i]);
			
			string[] sarray = new string[10];
			iarray.CopyTo(sarray, 0);

			for (int i=0; i< sarray.Length; i++)
				Console.WriteLine(sarray[i]);


			Console.ReadLine();
		}
	}
}
