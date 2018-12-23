using System;
using System.Reflection;

namespace ResolveAssembly
{
	class MainClass
	{
		static MainClass() 
		{
			AppDomain.CurrentDomain.TypeResolve += new ResolveEventHandler(CurrentDomain_TypeResolve);
			AppDomain.CurrentDomain.AssemblyResolve +=new ResolveEventHandler(currentDomain_AssemblyResolve);
		}

		[STAThread]
		static void Main(string[] args)
		{
			Type t = Type.GetType("ClassLibrary1.Class1", false);
			if (t != null) 
			{
				Console.WriteLine(t.Name);
			}
			else
			{
				Console.WriteLine("Тип не найден");
			}


			ClassLibrary2.Class2 c2 = new ClassLibrary2.Class2();
			Console.WriteLine(c2.ToString());

			Console.ReadLine();
		}

		private static Assembly CurrentDomain_TypeResolve(object sender, ResolveEventArgs args)
		{
			Console.WriteLine("Ищу тип: {0}...", args.Name);

			if (args.Name.Equals("ClassLibrary1.Class1")) 
			{
				return Assembly.LoadFrom(@"..\ClassLibrary1\bin\ClassLibrary1.dll");
			}

			return null;
		}

		private static Assembly currentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
		{
			Console.WriteLine("Ищу сборку {0}...", args.Name);
			if (args.Name.IndexOf("ClassLibrary2") >= 0) 
			{
				return Assembly.LoadFrom(@"..\ClassLibrary2\bin\ClassLibrary2.dll");
			}
			return null;
		}
	}
}
