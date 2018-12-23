using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace RasEnumConnections
{
	class Class1
	{
		const int INTERNET_RAS_INSTALLED = 0x10;

		[DllImport("WININET", CharSet=CharSet.Auto)]
		static extern bool InternetGetConnectedState(
			ref int lpdwFlags, 
			int dwReserved);

		const int MAX_PATH = 260;
		const int RAS_MaxDeviceType = 16;
		const int RAS_MaxPhoneNumber = 128;
		const int RAS_MaxEntryName = 256;
		const int RAS_MaxDeviceName = 128;

		const int RAS_Connected = 0x2000;

		[DllImport("RASAPI32", SetLastError=true, CharSet=CharSet.Auto)]
		static extern int RasEnumConnections(
			[In, Out] RASCONN[] lprasconn, 
			ref int lpcb, 
			ref int lpcConnections);

		[DllImport("RASAPI32", SetLastError=true, CharSet=CharSet.Auto)]
		static extern int RasGetConnectStatus(
			IntPtr hrasconn, 
			ref RASCONNSTATUS lprasconnstatus);

		[StructLayout(LayoutKind.Sequential,CharSet=CharSet.Auto)]
			struct RASCONN
		{ 
			public int     dwSize; 
			public IntPtr  hrasconn; 
			[MarshalAs(UnmanagedType.ByValTStr,SizeConst=RAS_MaxEntryName+1)]
			public string    szEntryName; 
			[MarshalAs(UnmanagedType.ByValTStr,SizeConst=RAS_MaxDeviceType+1)]
			public string    szDeviceType; 
			[MarshalAs(UnmanagedType.ByValTStr,SizeConst=RAS_MaxDeviceName+1)]
			public string    szDeviceName; 
			[MarshalAs(UnmanagedType.ByValTStr,SizeConst=MAX_PATH)]
			public string    szPhonebook;
			public int          dwSubEntry;
		}

		[StructLayout(LayoutKind.Sequential,CharSet=CharSet.Auto)]
			struct RASCONNSTATUS
		{
			public int dwSize;
			public int rasconnstate;
			public int dwError;
			[MarshalAs(UnmanagedType.ByValTStr,SizeConst=RAS_MaxDeviceType+1)]
			public string szDeviceType;
			[MarshalAs(UnmanagedType.ByValTStr,SizeConst=RAS_MaxDeviceName+1)]
			public string szDeviceName;
			[MarshalAs(UnmanagedType.ByValTStr,SizeConst=RAS_MaxPhoneNumber+1)]
			public string szPhoneNumber;
		}



		[STAThread]
		static void Main(string[] args)
		{
			//make sure that RAS is installed
			int flags = 0;
			InternetGetConnectedState(ref flags, 0);
			if (! ((flags & INTERNET_RAS_INSTALLED) == INTERNET_RAS_INSTALLED) )
				throw new NotSupportedException();

			//create array of structures to pass to API
			int ret;
			int conns = 0;
			RASCONN[] rarr = new RASCONN[256];
			rarr.Initialize();
			rarr[0].dwSize = Marshal.SizeOf(typeof(RASCONN));
			int lr = rarr[0].dwSize * rarr.Length;

			//call RasEnumConnections to loop all RAS connections
			ret = RasEnumConnections(rarr, ref lr, ref conns);
			if (ret != 0) throw new Win32Exception(ret);

			//loop through each RASCONN struct
			for(int i=0;i<conns;i++)
			{
				//retrieve RASCONN struct
				RASCONN r = rarr[i];

				//if connection bad, handle will be 0
				if (r.hrasconn == IntPtr.Zero) continue;

				//get status of RAS connection
				RASCONNSTATUS rcs = new RASCONNSTATUS();
				rcs.dwSize = Marshal.SizeOf(typeof(RASCONNSTATUS));

				ret = RasGetConnectStatus(r.hrasconn, ref rcs);
				if (ret != 0) throw new Win32Exception(ret);

				//print useful information to the console for each RAS connection
				Console.WriteLine("RAS CONNECTION {0}:", i+1);
				Console.WriteLine("\tDevice Name : {0}", r.szDeviceName); 
				Console.WriteLine("\tDevice Type : {0}", r.szDeviceType);
				Console.WriteLine("\tPhone Number: {0}", rcs.szPhoneNumber);
				Console.WriteLine("\tConnected?  : {0}", 
					rcs.rasconnstate == RAS_Connected);
				Console.WriteLine("\tEntry Name  : {0}", r.szEntryName);
				Console.WriteLine("\tPhone Book  : {0}", r.szPhonebook);
				Console.WriteLine();
			}

			Console.ReadLine();
		}
	
	}
}
