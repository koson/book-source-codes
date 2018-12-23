using System;

namespace Reflection2
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			// Загрузить модуль
			System.Reflection.Assembly assembly = System.Reflection.Assembly.LoadFrom("Reflection1.exe");
			// Получить список всех классов модуля
			Type[] types = assembly.GetTypes();
			// Отобразить список классов модуля
			foreach (Type t in types)
			{
				Console.WriteLine(t.FullName);
			}

			object Obj = Activator.CreateInstance("Reflection1", "Reflection1.TestClass");

			Console.ReadLine();
		}
	}
}
