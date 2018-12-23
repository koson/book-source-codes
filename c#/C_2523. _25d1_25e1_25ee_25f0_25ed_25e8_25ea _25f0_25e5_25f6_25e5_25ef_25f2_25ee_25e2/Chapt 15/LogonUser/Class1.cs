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
			// �������� �������� ������������
			WindowsIdentity wi = WindowsIdentity.GetCurrent();
			Console.WriteLine("Name={0} --> {1}", wi.Name, wi.IsAuthenticated);

			string UserName = "Administrator";
			string Password = "123";

			// ��� ���������� ������� ������������
			WindowsImpersonationContext impersonationContext = null;

			try
			{
				// ������������� ������� ������������
				WindowsIdentity newIdentity; 
				IntPtr token = IntPtr.Zero; 
				IntPtr tokenDuplicate = IntPtr.Zero; 
				if(LogonUser(UserName, Environment.MachineName, Password, 
					LOGON32_LOGON_NETWORK_CLEARTEXT, LOGON32_PROVIDER_DEFAULT, ref token) != 0)
				{ 
					if(DuplicateToken(token, 2, ref tokenDuplicate) != 0) 
					{ 
						newIdentity = new WindowsIdentity(tokenDuplicate); 
						// ��� ������������ ������������ ������� ��������
						impersonationContext = newIdentity.Impersonate(); 
					} 
				}
			}
			catch(Exception Ex)
			{
				Console.WriteLine("������ ������������ ������������ {0}: {1}", UserName, Ex);
			}

			// ����� ������������
			WindowsIdentity wi1 = WindowsIdentity.GetCurrent();
			Console.WriteLine("Name={0} --> {1}", wi1.Name, wi1.IsAuthenticated);

			// ���������� ���������� ������������
			if (impersonationContext != null)
				impersonationContext.Undo();

			// �������� �����
			WindowsIdentity wi2 = WindowsIdentity.GetCurrent();
			Console.WriteLine("Name={0} --> {1}", wi2.Name, wi2.IsAuthenticated);

			Console.ReadLine();
		}
	}
}
