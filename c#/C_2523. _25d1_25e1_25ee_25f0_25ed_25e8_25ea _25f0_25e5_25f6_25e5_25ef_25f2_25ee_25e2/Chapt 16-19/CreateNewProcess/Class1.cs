using System;

namespace CreateNewProcess
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			// Создать новый процесс
			System.Diagnostics.Process proc = new System.Diagnostics.Process();
			// Приложение которое будем запускать
			proc.StartInfo.FileName = "Notepad.exe";
			proc.EnableRaisingEvents = true;
			proc.Exited +=new EventHandler(proc_Exited);
			proc.Start();

			Console.ReadLine();
		}

		// Будет вызываться при завершении notepad
		private static void proc_Exited(object sender, EventArgs e)
		{
			Console.WriteLine("proc_Exited");
		}
	}
}
