using System;
using System.Management;

namespace Win32_Desktop
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			// Получить настройки рабочего стола
			WqlObjectQuery query = new WqlObjectQuery(
				"SELECT * FROM Win32_Desktop WHERE Name = '.Default'");
			ManagementObjectSearcher find = 
				new ManagementObjectSearcher(query);
			foreach (ManagementObject mo in find.Get())
			{
				// Значения могут быть изменены 
				// в реестре "HKEY_CURRENT_USER\Control Panel\Desktop"
				Console.WriteLine("Width of window borders." +
					mo["BorderWidth"]);
				Console.WriteLine("ALT+TAB task switching allowed." +
					mo["CoolSwitch"]);
				// Значения в мс
				Console.WriteLine("Lenght of time between cursor blincks. " +
					mo["CursorBlinkRate"]);
				Console.WriteLine("Show content of windows when are draged." + 
					mo["DragFullWindows"]);
				Console.WriteLine("Grid settings for dragging windows." +
					mo["GridGranularity"]);
				Console.WriteLine("Grid settings for icon spacing. " +
					mo["IconSpacing"]);
				Console.WriteLine("Font used for the names of icons." +
					mo["IconTitleFaceName"]);
				Console.WriteLine("Icon ront size. " + mo["IconTitleSize"]);
				Console.WriteLine("Wrapping of icon title." +
					mo["IconTitleWrap"]);
				Console.WriteLine("Name of the desktop profile." +
					mo["Name"]);
				Console.WriteLine("Screen saver is active." +
					mo["ScreenSaverActive"]);
				Console.WriteLine("Name of the screen saver executable." +
					mo["ScreenSaverExecutable"]);
				Console.WriteLine("Is screen saver protected with password." +
					mo["ScreenSaverSecure"]);
				Console.WriteLine("Time to pass to activate screen saver." +
					mo["ScreenSaverTimeout"]);
				Console.WriteLine("File name for desktop background." +
					mo["Wallpaper"]);
				Console.WriteLine("Wallpaper fills entire screen." +
					mo["WallpaperStretched"]);
				Console.WriteLine("Wallpaper is tiled." +
					mo["WallpaperTiled"]);
			}
		}
	}
}
