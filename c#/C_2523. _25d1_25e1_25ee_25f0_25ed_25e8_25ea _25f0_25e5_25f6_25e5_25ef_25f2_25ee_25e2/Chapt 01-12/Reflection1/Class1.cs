using System;

namespace Reflection1
{
	public class TestClass
	{
		public TestClass()
		{
			Console.WriteLine("Конструктор TestClass");
		}

	}

	class ReflectionDemoClass
	{
		[STAThread]
		static void Main(string[] args)
		{
			// Получить ссылку на тип по имени
			Type TestType = Type.GetType("Reflection1.TestClass", false, true);
			// Если класс найден
			if (TestType != null)
			{
				// Получаем конструктор
				System.Reflection.ConstructorInfo ci = TestType.GetConstructor(new Type[]{});
				// Вызываем конструктор
				object Obj = ci.Invoke(new object[]{});
			}
			else
			{
				Console.WriteLine("Класс не найден");
			}
			Console.ReadLine();
		}
	}
}
