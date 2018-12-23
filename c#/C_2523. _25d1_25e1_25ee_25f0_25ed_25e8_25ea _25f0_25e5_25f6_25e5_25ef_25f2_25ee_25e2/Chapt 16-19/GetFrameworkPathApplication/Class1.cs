using System;
using System.Runtime.InteropServices;

namespace GetFrameworkPathApplication
{
	class MainClass
	{
		[DllImport("mscoree.dll")] 
		internal static extern void GetCORSystemDirectory(
			 [MarshalAs(UnmanagedType.LPTStr)]
			  System.Text.StringBuilder Buffer, 
			  int BufferLength, ref int Length
		); 

		[STAThread]
		static void Main(string[] args)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder(1024); 
			int size = 0; 
			
			// Вызов
			GetCORSystemDirectory(sb, sb.Capacity, ref size);

			// Напечатает, например, "F:\WINXP\Microsoft.NET\Framework\v1.1.4322\"
			Console.WriteLine(sb);

			Console.ReadLine();
 
		}
	}
}
