using System;
using System.Reflection;

namespace FindMethod
{
	class Test
	{
		string name;
  
		Test (string name)
		{
			this.name = name;
		}

		public void ShowName()
		{
			Console.WriteLine(name);
		}

		static void Main()
		{
			// Создать экземпляр объекта
			Test t = new Test ("The name");
			// Получить метод, который хотим вызвать
			MethodInfo mi = typeof(Test).GetMethod("ShowName");
			// Вызвать
			mi.Invoke(t, null);

			Console.ReadLine();
		}
	}
}