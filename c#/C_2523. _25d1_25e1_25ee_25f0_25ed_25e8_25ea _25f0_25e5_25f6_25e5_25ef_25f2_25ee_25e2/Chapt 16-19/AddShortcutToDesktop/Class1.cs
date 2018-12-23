using System;
using IWshRuntimeLibrary;

namespace AddShortcutToDesktop
{
	class ShortcutToDesktop
	{
		[STAThread]
		static void Main(string[] args)
		{
			// ������� ����� �� ������� �����
			object shortAdr = (object)"Desktop";
    
			WshShell shell = new WshShell();
			// �������� ������ ����� ������
			string link = ((string) shell.SpecialFolders.Item(
				ref shortAdr)) + @"\Calc.lnk";
			// ������� ������ ������
			IWshShortcut shortcut =
				(IWshShortcut)shell.CreateShortcut(link);
			// ��������
			shortcut.Description = "����� ��� ������������";
			// ���������� ������� �������
			shortcut.Hotkey = "CTRL+SHIFT+A";
			// �������� ���� ��� ��������� "�����������"
			shortcut.TargetPath =
				Environment.GetFolderPath(Environment.SpecialFolder.System) +
				@"\Calc.exe";
			// ������� �����
			shortcut.Save();
		}
	}
}
