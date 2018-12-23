using System;
using System.Reflection;

namespace Reflection4
{
	class Class1
	{
		public struct Account
		{
			public string	FirstName;
			public string	LastName;
			public int		Age;
			public string  Login;
			public string  Password;

			private long     HashCode;
			private string   Passport;
		}


		[STAThread]
		static void Main(string[] args)
		{
			
			Console.WriteLine("Public:");
			foreach(FieldInfo fi in typeof(Account).GetFields() )
			{
				Console.WriteLine("\t"+ fi.Name );
			}
			Console.WriteLine("NonPublic:");
			foreach(FieldInfo fi in typeof(Account).GetFields(BindingFlags.NonPublic | BindingFlags.Instance) )
			{
				Console.WriteLine("\t"+ fi.Name );
			}

			Console.ReadLine();
		}
	}
}
