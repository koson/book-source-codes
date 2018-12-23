using System;
using Shell32;

namespace ShellDemo
{
	class ShellDemoClass
	{
		[STAThread]
		static void Main(string[] args)
		{
			Shell shell = new Shell();

			// Доступ к компонентам панели управления
			shell.ControlPanelItem("desk.cpl");

			// Установка порядка окон "каскад"
			shell.CascadeWindows();

			Console.ReadLine();
		}
	}
}
