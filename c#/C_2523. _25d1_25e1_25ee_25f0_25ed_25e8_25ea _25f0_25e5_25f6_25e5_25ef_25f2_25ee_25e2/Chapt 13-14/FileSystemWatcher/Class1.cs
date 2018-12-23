using System;
using System.IO;

namespace FileSystemWatcherTest
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			// Создаем наблюдателя
			FileSystemWatcher watcher = new FileSystemWatcher();
			watcher.Path = @"E:\1\";
			// Будем следить за измненениями по последнему доступу,
			// времени записи и переименованию файла или директорий
			watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite 
				| NotifyFilters.FileName | NotifyFilters.DirectoryName;
			// Будем следить только за txt файлами
			watcher.Filter = "*.txt";

			// Добавляем обработчики событий
			watcher.Changed += new FileSystemEventHandler(OnChanged);
			watcher.Created += new FileSystemEventHandler(OnChanged);
			watcher.Deleted += new FileSystemEventHandler(OnChanged);
			watcher.Renamed += new RenamedEventHandler(OnRenamed);

			// Включаем наблюдение
			watcher.EnableRaisingEvents = true;

			// Ждем пока пользователь не нажмет клавишу q
			Console.WriteLine("Нажмите \'q\' для выхода.");
			while(Console.Read()!='q');
		}

		private static void OnChanged(object source, FileSystemEventArgs e)
		{
			// Файл изменился, создан или удален
			Console.WriteLine("File: " +  e.FullPath + " " + e.ChangeType);
		}

		private static void OnRenamed(object source, RenamedEventArgs e)
		{
			// Файл переименован
			Console.WriteLine("File: {0} renamed to {1}", e.OldFullPath, e.FullPath);
		}
	}
}
