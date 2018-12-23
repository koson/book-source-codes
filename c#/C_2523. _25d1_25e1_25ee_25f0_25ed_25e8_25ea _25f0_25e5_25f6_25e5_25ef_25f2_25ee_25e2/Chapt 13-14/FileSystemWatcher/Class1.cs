using System;
using System.IO;

namespace FileSystemWatcherTest
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			// ������� �����������
			FileSystemWatcher watcher = new FileSystemWatcher();
			watcher.Path = @"E:\1\";
			// ����� ������� �� ������������ �� ���������� �������,
			// ������� ������ � �������������� ����� ��� ����������
			watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite 
				| NotifyFilters.FileName | NotifyFilters.DirectoryName;
			// ����� ������� ������ �� txt �������
			watcher.Filter = "*.txt";

			// ��������� ����������� �������
			watcher.Changed += new FileSystemEventHandler(OnChanged);
			watcher.Created += new FileSystemEventHandler(OnChanged);
			watcher.Deleted += new FileSystemEventHandler(OnChanged);
			watcher.Renamed += new RenamedEventHandler(OnRenamed);

			// �������� ����������
			watcher.EnableRaisingEvents = true;

			// ���� ���� ������������ �� ������ ������� q
			Console.WriteLine("������� \'q\' ��� ������.");
			while(Console.Read()!='q');
		}

		private static void OnChanged(object source, FileSystemEventArgs e)
		{
			// ���� ���������, ������ ��� ������
			Console.WriteLine("File: " +  e.FullPath + " " + e.ChangeType);
		}

		private static void OnRenamed(object source, RenamedEventArgs e)
		{
			// ���� ������������
			Console.WriteLine("File: {0} renamed to {1}", e.OldFullPath, e.FullPath);
		}
	}
}
