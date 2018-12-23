using System;

namespace Pointers
{
	class Class1
	{
		unsafe static void TestPointer() 
		{
			int *p;	 // указатель
			int i;   // переменная

			i = 10;  // инициализация переменной
			p = &i;  // указатель на переменную


			// выводим значение переменной напряму и через указатель
			Console.WriteLine(i);
			Console.WriteLine(*p);
			Console.WriteLine("----------");

			// присваиваем переменную через указатель
			*p = 333;
			Console.WriteLine(i);
			Console.WriteLine(*p);
			Console.WriteLine("----------");

			// изменяем переменную через собственный указатель
			i = *(&i) + 10;
			Console.WriteLine(i);
			Console.WriteLine(*p);
		}

		[STAThread]
		static void Main(string[] args)
		{
			TestPointer();
			Console.ReadLine();
		}
	}
}
