using System;

namespace Interface
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	interface Int1 	{
		int a {
			get;
			set;
		}
	}
	class Variable: Int1 
	{
		private int GetVar;
		public Variable(int a) 
		{
			GetVar = a;
		}
		public int a 
		{
			get 
			{
				return GetVar;
			}
			set
			{
				GetVar = value;
			}
		}
	}
	public class Output
	{
		public static void Main(string[] args)
		{
			Variable x = new Variable(4);
			Console.WriteLine("The variable is {0}",x.a);
		}
	}
}
