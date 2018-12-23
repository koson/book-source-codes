using System;
using System.Threading;

namespace OneInstanceApplication
{
	class Class1
	{
		static Mutex m_mutex;

		[STAThread]
		static void Main(string[] args)
		{
			if (InstanceExists())
			{ 
				return; 
			}
			Console.WriteLine("Приложение запущено. Нажмите ENTER для выхода");
			Console.ReadLine();
		}

		static bool InstanceExists() 
		{ 
			bool createdNew; 
			m_mutex = new Mutex( false, "OneInstanceApplication", out createdNew); 
			return(!createdNew);
		} 

	}
}
