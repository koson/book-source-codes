using System;
using System.Reflection;

namespace Reflection3
{
	class Class1
	{
		public class Account
		{
			private		long privatevalue = 0;
			protected	long protectedval = 0;
			
			public long GetPrivatevalue()
			{
				return privatevalue;
			}

			public long GetProtectedval()
			{
				return protectedval;
			}
		}


		[STAThread]
		static void Main(string[] args)
		{
			Account a = new Account();
			Console.WriteLine("a.privatevalue={0}", a.GetPrivatevalue());
			Console.WriteLine("a.protectedval={0}", a.GetProtectedval());

			FieldInfo fi1 = a.GetType().GetField("privatevalue", BindingFlags.NonPublic | BindingFlags.Instance);
			if (fi1 != null)
			{
				fi1.SetValue(a, 1);
			}

			FieldInfo fi2 = a.GetType().GetField("protectedval", BindingFlags.NonPublic | BindingFlags.Instance);
			if (fi2 != null)
			{
				fi2.SetValue(a, 2);
			}

			Console.WriteLine("a.privatevalue={0}", a.GetPrivatevalue());
			Console.WriteLine("a.protectedval={0}", a.GetProtectedval());

			Console.ReadLine();
		}
	}
}
