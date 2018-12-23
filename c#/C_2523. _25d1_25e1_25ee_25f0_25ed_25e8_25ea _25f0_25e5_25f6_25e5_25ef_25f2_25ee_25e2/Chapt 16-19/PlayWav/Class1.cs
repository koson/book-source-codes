using System;
using System.Runtime.InteropServices;


namespace PlayWav
{
	class Class1
	{
		[DllImportAttribute("winmm.dll")]
		public static extern long PlaySound(String lpszName, long hModule, long dwFlags); 

		[STAThread]
		static void Main(string[] args)
		{
			PlaySound(@"C:\WINDOWS\Media\recycle.wav" ,0 ,0); 
		}
	}
}
