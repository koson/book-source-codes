using System;
using Microsoft.Win32;

namespace MyExtShell
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			RegistryKey newkey;
			newkey = Registry.ClassesRoot.CreateSubKey("myprog_file");
			newkey = newkey.CreateSubKey("shell");
			newkey = newkey.CreateSubKey("Запустить через myprog");
			newkey = newkey.CreateSubKey("command");
			newkey.SetValue ("", @"c:\myfolder\myprog.exe %1");
			newkey = Registry.ClassesRoot.CreateSubKey(".myext");
			newkey.SetValue ("", "myprog_file");
		}
	}
}
