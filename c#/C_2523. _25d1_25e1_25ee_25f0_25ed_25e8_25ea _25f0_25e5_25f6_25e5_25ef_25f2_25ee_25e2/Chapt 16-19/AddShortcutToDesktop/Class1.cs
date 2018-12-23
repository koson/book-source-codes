using System;
using IWshRuntimeLibrary;

namespace AddShortcutToDesktop
{
	class ShortcutToDesktop
	{
		[STAThread]
		static void Main(string[] args)
		{
			// Создать ярлык на рабочем столе
			object shortAdr = (object)"Desktop";
    
			WshShell shell = new WshShell();
			// Получить полный адрес ярлыка
			string link = ((string) shell.SpecialFolders.Item(
				ref shortAdr)) + @"\Calc.lnk";
			// Создать объект ярлыка
			IWshShortcut shortcut =
				(IWshShortcut)shell.CreateShortcut(link);
			// Описание
			shortcut.Description = "Ярлык для калькулятора";
			// Установить горячую клавишу
			shortcut.Hotkey = "CTRL+SHIFT+A";
			// Получить путь для программы "Калькулятор"
			shortcut.TargetPath =
				Environment.GetFolderPath(Environment.SpecialFolder.System) +
				@"\Calc.exe";
			// Создать ярлык
			shortcut.Save();
		}
	}
}
