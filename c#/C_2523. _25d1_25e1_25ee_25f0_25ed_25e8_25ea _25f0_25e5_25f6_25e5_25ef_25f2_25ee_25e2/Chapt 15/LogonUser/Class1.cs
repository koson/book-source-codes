using System;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace LogonUser
{
	class Class1
	{
		private const int LOGON32_LOGON_INTERACTIVE		  = 2; 
		private const int LOGON32_LOGON_NETWORK_CLEARTEXT = 3;
		private const int LOGON32_PROVIDER_DEFAULT		  = 0; 
	 
		[DllImport("advapi32.dll", CharSet=CharSet.Auto)] 
		static extern int LogonUser(string lpszUserName, string lpszDomain, string lpszPassword, int dwLogonType, int dwLogonProvider, ref IntPtr phToken);
	 
		[DllImport("advapi32.dll", CharSet=CharSet.Auto, SetLastError=true)] 
		static extern int DuplicateToken(IntPtr hToken, int impersonationLevel, ref IntPtr hNewToken);

		[STAThread]
		static void Main(string[] args)
		{
			// Печатаем текущего пользователя
			WindowsIdentity wi = WindowsIdentity.GetCurrent();
			Console.WriteLine("Name={0} --> {1}", wi.Name, wi.IsAuthenticated);

			string UserName = "Administrator";
			string Password = "123";

			// Для сохранения текущей имперсонации
			WindowsImpersonationContext impersonationContext = null;

			try
			{
				// Имперсонируем другого пользователя
				WindowsIdentity newIdentity; 
				IntPtr token = IntPtr.Zero; 
				IntPtr tokenDuplicate = IntPtr.Zero; 
				if(LogonUser(UserName, Environment.MachineName, Password, 
					LOGON32_LOGON_NETWORK_CLEARTEXT, LOGON32_PROVIDER_DEFAULT, ref token) != 0)
				{ 
					if(DuplicateToken(token, 2, ref tokenDuplicate) != 0) 
					{ 
						newIdentity = new WindowsIdentity(tokenDuplicate); 
						// При имперсонации возвращается текущее значение
						impersonationContext = newIdentity.Impersonate(); 
					} 
				}
			}
			catch(Exception Ex)
			{
				Console.WriteLine("Ошибка ммперсонации пользователя {0}: {1}", UserName, Ex);
			}

			// Новая имперсонация
			WindowsIdentity wi1 = WindowsIdentity.GetCurrent();
			Console.WriteLine("Name={0} --> {1}", wi1.Name, wi1.IsAuthenticated);

			// Возвращаем предыдущую имперсонацию
			if (impersonationContext != null)
				impersonationContext.Undo();

			// Печатаем снова
			WindowsIdentity wi2 = WindowsIdentity.GetCurrent();
			Console.WriteLine("Name={0} --> {1}", wi2.Name, wi2.IsAuthenticated);

			Console.ReadLine();
		}
	}
}
