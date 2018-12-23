using System;

namespace EscapeChars
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
/*
\b Ц символ Backspace
\f Ц перевод страницы (Form feed) 
\n Ц перевод строки
\r Ц возврат каретки 
\t Ц горизонтальна€ табул€ци€
\v Ц вертикальна€ кавычка
\uxxxx Ц символ Unicode с указанным шестнадцатеричным кодом 
\xn[n][n][n] Ц символ Unicode с указанным кодом 
\Uxxxxxxxx Ц символ Unicode с указанным кодом
*/
			Console.Write("\'\"\0\\");   // кавычки и нулевой символ
			Console.Write("\a"); // сигнал
			Console.Write("test\b\n\r\t\v\v\n");
			Console.Write("\u0024"); 
			Console.Write("\x24"); 
			Console.Write("\U00000024"); 


			Console.ReadLine();
		}
	}
}
