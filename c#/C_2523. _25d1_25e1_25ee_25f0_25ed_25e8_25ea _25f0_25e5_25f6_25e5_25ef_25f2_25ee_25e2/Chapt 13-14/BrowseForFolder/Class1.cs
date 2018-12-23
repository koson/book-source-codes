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

			// open dialog for desktop folder
			// use appropriate constant for folder type - ShellSpecialFolderConstants
			Folder folder = shell.BrowseForFolder(0, "Get some folder from user...", 0, ShellSpecialFolderConstants.ssfDESKTOP);

			if (folder != null)
			{
				foreach (FolderItem fi in folder.Items())
				{
					Console.WriteLine(fi.Name+fi.Size.ToString());
					
				}
			}

			Console.ReadLine();
		}
	}
}
