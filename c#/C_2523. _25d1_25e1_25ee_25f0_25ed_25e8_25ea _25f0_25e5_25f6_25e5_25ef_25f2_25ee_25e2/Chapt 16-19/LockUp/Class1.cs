using System;
using System.Runtime.InteropServices;

namespace LockUp
{
	class Class1
	{

		[DllImport("user32.dll")]
		public static extern void LockWorkStation();

		[STAThread]
		static void Main(string[] args)
		{
			LockWorkStation();
		}
	}
}
