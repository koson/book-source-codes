using System;

namespace Jagged
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class Class1
	{
		static void Main(string[] args)
		{
			int[][] Numbers = new int[3][];
			Numbers[0] = new int[4] {2,4,6,8};
			Numbers[1] = new int[4] {3,5,7,9};
			Numbers[2] = new int[2] {0,1};
	
			for (int a=0; a < Numbers.Length; a++) 
			{
				Console.Write("Element {0}: ", a);

				for (int b = 0 ; b < Numbers[a].Length ; b++)
					Console.Write("{0}{1}", Numbers[a][b],
						b == (Numbers[a].Length-1) ? "" : " ");

				Console.WriteLine();
			}
		}
	}
}
