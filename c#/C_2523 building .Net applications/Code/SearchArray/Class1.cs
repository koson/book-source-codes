using System;

namespace SearchArray
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class Class1
	{
		static void Main(string[] args)
		{
			Array a=Array.CreateInstance( typeof(string), 4);
			a.SetValue("My", 0);
			a.SetValue("dog",1);
			a.SetValue("has",2);
			a.SetValue("fleas",3);
			string Search = "has";
			int Index = Array.IndexOf(a, Search);
			Console.WriteLine("The word {0} is at {1}.",Search,Index);
		}

		public static void Output(Array a)  
		{
			for ( int n = a.GetLowerBound(0); n <= a.GetUpperBound(0); n++ )
				Console.WriteLine( "\t[{0}]:\t{1}", n, a.GetValue( n ) );
		}
	}
}
