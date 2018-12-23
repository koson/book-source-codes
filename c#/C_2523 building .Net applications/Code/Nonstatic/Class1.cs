using System;

namespace Nonstatic
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	/// 
	class First
	{
		public virtual void One() { Console.WriteLine("First.One"); }
	}
	class Second: First
	{
		public override void One() { Console.WriteLine("Second.One"); }
	}
	class Output
	{
		static void Main() 
		{
			Second y = new Second();
			First x = y;
			x.One();
			y.One();
		}
	}
}
