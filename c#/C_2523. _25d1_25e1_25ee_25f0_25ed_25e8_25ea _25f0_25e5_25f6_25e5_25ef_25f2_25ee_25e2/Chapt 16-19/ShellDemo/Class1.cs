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

			// ������ � ����������� ������ ����������
			shell.ControlPanelItem("desk.cpl");

			// ��������� ������� ���� "������"
			shell.CascadeWindows();

			Console.ReadLine();
		}
	}
}
