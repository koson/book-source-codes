using System;
using System.Runtime.InteropServices;
using System.Text;

namespace InternetAvailable
{
	class InernetChecker
	{
		[DllImport("WININET", CharSet=CharSet.Auto)]
		static extern bool InternetGetConnectedState(
			ref InternetConnectionState lpdwFlags, 
			int dwReserved);

		[Flags]
		enum InternetConnectionState: int
		{
			INTERNET_CONNECTION_MODEM      = 0x1,
			INTERNET_CONNECTION_LAN        = 0x2,
			INTERNET_CONNECTION_PROXY      = 0x4,
			INTERNET_RAS_INSTALLED         = 0x10,
			INTERNET_CONNECTION_OFFLINE    = 0x20,
			INTERNET_CONNECTION_CONFIGURED = 0x40
		}

		static void Main()
		{
			InternetConnectionState flags = 0;

			Console.WriteLine(
				"InternetGetConnectedState : {0} - {1}",
				(InternetGetConnectedState(ref flags, 0)?"ONLINE":"OFFLINE"),
				flags
				);
			Console.ReadLine();
		}
	}
}

