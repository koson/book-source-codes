using System;
using System.Security.Principal;
using System.Threading;

class Threads_Principal
{
	static void Main(string[] args)
	{
		// Поток с правами по умолчанию
		Thread t = new Thread(new ThreadStart(PrintPrincipalInformation));
		t.Start();
		t.Join();

		// Задание WindowsPrincipal
		AppDomain currentDomain = AppDomain.CurrentDomain;
		currentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);
            
		// Создание потока с правами WindowsPrincipal
		t = new Thread(new ThreadStart(PrintPrincipalInformation));
		t.Start();
		t.Join();

		// Задание прав NewUser
		IIdentity identity = new GenericIdentity("NewUser");
		IPrincipal principal = new GenericPrincipal(identity, null);
		currentDomain.SetThreadPrincipal(principal);
            
		// Поток с правами пользователя NewUser
		t = new Thread(new ThreadStart(PrintPrincipalInformation));
		t.Start();
		t.Join();
        
		// Wait for user input before terminating.
		Console.ReadLine();
	}

	static void PrintPrincipalInformation()
	{
		IPrincipal curPrincipal = Thread.CurrentPrincipal;
		if(curPrincipal != null)
		{
			Console.WriteLine("Тип: " + curPrincipal.GetType().Name);
			Console.WriteLine("Имя: " + curPrincipal.Identity.Name);
			Console.WriteLine("Идентификация: " + curPrincipal.Identity.IsAuthenticated);
			Console.WriteLine();
		}
	}
}