using System;
using System.Globalization;
using System.Threading;

namespace DecimalSeparator
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			Console.WriteLine(1.5F); // Выведет 1.5

			//Нельзя напрямую менять CurrentCulture
			//CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator= "#";

			//Для изменения нужно создать новый экземпляр CultureInfor
			//и изменить в нем нужные поля
			CultureInfo newCInfo = (CultureInfo) Thread.CurrentThread.CurrentCulture.Clone();
			newCInfo.NumberFormat.NumberDecimalSeparator = "#";
			Thread.CurrentThread.CurrentCulture = newCInfo;

			Console.WriteLine(1.5F); // Выведет 1#5

			Console.ReadLine();
		}
	}
}
