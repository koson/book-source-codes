using System;
using System.Security.Principal;
using System.Threading;

class Threads_Principal
{
	static void Main(string[] args)
	{
		// ����� � ������� �� ���������
		Thread t = new Thread(new ThreadStart(PrintPrincipalInformation));
		t.Start();
		t.Join();

		// ������� WindowsPrincipal
		AppDomain currentDomain = AppDomain.CurrentDomain;
		currentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);
            
		// �������� ������ � ������� WindowsPrincipal
		t = new Thread(new ThreadStart(PrintPrincipalInformation));
		t.Start();
		t.Join();

		// ������� ���� NewUser
		IIdentity identity = new GenericIdentity("NewUser");
		IPrincipal principal = new GenericPrincipal(identity, null);
		currentDomain.SetThreadPrincipal(principal);
            
		// ����� � ������� ������������ NewUser
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
			Console.WriteLine("���: " + curPrincipal.GetType().Name);
			Console.WriteLine("���: " + curPrincipal.Identity.Name);
			Console.WriteLine("�������������: " + curPrincipal.Identity.IsAuthenticated);
			Console.WriteLine();
		}
	}
}