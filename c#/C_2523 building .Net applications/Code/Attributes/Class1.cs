#define Trace
using System;
using System.Diagnostics;

namespace Attributes
{
	public class Class1 {
		[Conditional("Trace")]
		public static void Output(string output){
			Console.WriteLine(output); }
	}
	public class Class2 {
		static void One( )
		{
			Class1.Output("I'm in Class1 now.");
		}
		[Obsolete ("You're going the wrong way.", true)]
		static void OldMethod( ) {Console.WriteLine("Oops!");}
		static void NewMethod( ) {Console.WriteLine("Much better.");}
		static void Main(string[] args)
		{
			Class1.Output("I'm in Main first.");
			One( );
			NewMethod( );
		}
	}
}
