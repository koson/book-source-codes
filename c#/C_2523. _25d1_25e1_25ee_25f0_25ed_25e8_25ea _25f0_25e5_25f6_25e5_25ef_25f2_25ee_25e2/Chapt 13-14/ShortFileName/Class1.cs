using System;
using System.Runtime.InteropServices;
using System.Text;

namespace ShortFileName
{
	class Class1
	{
		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		public static extern int GetLongPathName(
			[MarshalAs(UnmanagedType.LPTStr)]
			string path,
			[MarshalAs(UnmanagedType.LPTStr)]
			StringBuilder longPath,
			int longPathLength
		);

		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		public static extern int GetShortPathName(
			[MarshalAs(UnmanagedType.LPTStr)]
			string path,
			[MarshalAs(UnmanagedType.LPTStr)]
			StringBuilder shortPath,
			int shortPathLength
		);


		[STAThread]
		static void Main(string[] args)
		{
			// Из длинного в короткое
			StringBuilder shortPath = new StringBuilder(255);
			GetShortPathName(@"F:\Documents and Settings\PVA\My Documents\Книга1.xls", shortPath, shortPath.Capacity);
			Console.WriteLine(shortPath.ToString());

			// Из короткого в длинное
			StringBuilder longPath = new StringBuilder(255);
			GetLongPathName(shortPath.ToString(), longPath, longPath.Capacity);
			Console.WriteLine(longPath.ToString());

			Console.ReadLine();
		}
	}
}
