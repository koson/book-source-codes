using System;
using System.Runtime.InteropServices;

namespace ColorText
{
	public class ConsoleColorManager
	{
		private Handles m_Handle;

		// Константы консольных потоков
		public enum Handles
		{
			STD_INPUT_HANDLE  = -10,
			STD_OUTPUT_HANDLE = -11,
			STD_ERROR_HANDLE  = -12
		}

		// Импортируем метод GetStdHandle
		[DllImportAttribute("Kernel32.dll")]
		private static extern IntPtr GetStdHandle
		(
			int nStdHandle      // тип консольного потока
		);

		// Испортируем метод SetConsoleTextAttribute
		[DllImportAttribute("Kernel32.dll")]
		private static extern bool SetConsoleTextAttribute
		(
			IntPtr hConsoleOutput,  // дескриптор потока
			int wAttributes         // цвет текста и фона 
		);

		// Цвета (флаги)
		[Flags]
		public enum Color
		{
			Black	= 0x0000,
			Blue	= 0x0001,
			Green	= 0x0002, 
			Cyan	= 0x0003,
			Red		= 0x0004,
			Magenta = 0x0005,
			Yellow	= 0x0006,
			Grey	= 0x0007,
			White	= 0x0008
		}

		// По умолчанию будем управлять потоком OUTPUT 
		public ConsoleColorManager()
		{
			m_Handle = Handles.STD_OUTPUT_HANDLE;
		}

		public ConsoleColorManager(Handles Handle)
		{
			m_Handle = Handle;
		}

		// установка цвета по умолчанию
		public bool SetDefaultColor()
		{
			return SetColor(Color.Grey, true);
		}

		// установка цвета текста
		// флаг fLight задает яркость цвета
		public bool SetColor(Color aColor, bool fLight)
		{
			// получаем дескриптор
			IntPtr ConsoleHandle = GetStdHandle((int)m_Handle);
      
			// получаем значение цвета
			int colorMask = (int)aColor;

			// если нужен яркий цвет, добавляем White
			if (fLight)
				colorMask |= (int)Color.White;

			// устанавливаем
			return SetConsoleTextAttribute(ConsoleHandle, colorMask);
		}
	}


	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			ConsoleColorManager colorManager = new ConsoleColorManager();
			// Ярко-зеленый
			colorManager.SetColor(ConsoleColorManager.Color.Green, true);
			Console.WriteLine("Text in green");

			// Темно-красный
			colorManager.SetColor(ConsoleColorManager.Color.Red  , false);
			Console.WriteLine("Text in red");

			// Восстанавливаем цвет по умолчанию
			colorManager.SetDefaultColor();

			Console.ReadLine();
		}
	}
}
