using System;
using System.Runtime.InteropServices;
using System.Web;

namespace ConsoleApplication8
{
	class Class1
	{
		[DllImport("kernel32.dll")]
			// frequency of the sound, 37 - 32767, duration in milliseconds
		static extern bool Beep (int freq, int duration);

		[STAThread]
		static void Main(string[] args)
		{
		    Beep(1000, 100);
		}
	}
}
