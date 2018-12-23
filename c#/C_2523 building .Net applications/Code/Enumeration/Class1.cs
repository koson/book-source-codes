using System;

namespace Enumeration
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class Class1
	{
		enum Seasons {Spring=1, Summer, Autumn, Winter};
		static void Main(string[] args)
		{
			int a = (int) Seasons.Autumn;
			Console.WriteLine("Autumn is season {0}.", a);
		}
	}
}
