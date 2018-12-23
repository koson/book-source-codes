using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace SQLServersEnum 
{
	internal class Class1 
	{
		[DllImport("netapi32.dll", EntryPoint="NetServerEnum")]
		public static extern NERR NetServerEnum([MarshalAs(UnmanagedType.LPWStr)] string ServerName, int Level, out IntPtr BufPtr, int PrefMaxLen, ref int EntriesRead, ref int TotalEntries, uint ServerType, [MarshalAs(UnmanagedType.LPWStr)] string Domain, int ResumeHandle);

		[DllImport("netapi32.dll", EntryPoint="NetApiBufferFree")]
		public static extern NERR NetApiBufferFree(IntPtr Buffer);

		// “ип сервера
		const uint SV_TYPE_SQLSERVER = 0x00000004;

		[StructLayout(LayoutKind.Sequential)]
		public struct SERVER_INFO_101 
		{
			[MarshalAs(UnmanagedType.U4)] public uint sv101_platform_id;
			[MarshalAs(UnmanagedType.LPWStr)] public string sv101_name;
			[MarshalAs(UnmanagedType.U4)] public uint sv101_version_major;
			[MarshalAs(UnmanagedType.U4)] public uint sv101_version_minor;
			[MarshalAs(UnmanagedType.U4)] public uint sv101_type;
			[MarshalAs(UnmanagedType.LPWStr)] public string sv101_comment;
		}

		
		// оперционна€ система
		public enum PLATFORM_ID : uint 
		{
			PLATFORM_ID_DOS = 300,
			PLATFORM_ID_OS2 = 400,
			PLATFORM_ID_NT = 500,
			PLATFORM_ID_OSF = 600,
			PLATFORM_ID_VMS = 700,
		}

		// список ошибок, возвращаемых NetServerEnum
		public enum NERR 
		{
			NERR_Success = 0, // Success
			ERROR_ACCESS_DENIED = 5,
			ERROR_NOT_ENOUGH_MEMORY = 8,
			ERROR_BAD_NETPATH = 53,
			ERROR_NETWORK_BUSY = 54,
			ERROR_INVALID_PARAMETER = 87,
			ERROR_INVALID_LEVEL = 124,
			ERROR_MORE_DATA = 234,
			ERROR_EXTENDED_ERROR = 1208,
			ERROR_NO_NETWORK = 1222,
			ERROR_INVALID_HANDLE_STATE = 1609,
			ERROR_NO_BROWSER_SERVERS_FOUND = 6118,
		}

		// ѕолучить список SQL серверов
		public static ArrayList GetSQLServerList() 
		{
			SERVER_INFO_101 si;
			IntPtr pInfo = IntPtr.Zero;
			int etriesread = 0;
			int totalentries = 0;
			ArrayList srvs = new ArrayList();

			try 
			{
				NERR err = NetServerEnum(null, 101, out pInfo, -1, ref etriesread, ref totalentries, SV_TYPE_SQLSERVER, null, 0);
				if ((err == NERR.NERR_Success || err == NERR.ERROR_MORE_DATA) && pInfo != IntPtr.Zero) 
				{
					int ptr = pInfo.ToInt32();
					for (int i = 0; i < etriesread; i++) 
					{
						si = (SERVER_INFO_101) Marshal.PtrToStructure(new IntPtr(ptr), typeof (SERVER_INFO_101));
						srvs.Add(si.sv101_name); // добавл€ем им€ сервера в список

						ptr += Marshal.SizeOf(si);
					}
				}
			} 
			catch (Exception) 
			{ 
			} 
			finally 
			{ // освобождаем выделенную пам€ть
				if (pInfo != IntPtr.Zero) 
				{
					NetApiBufferFree(pInfo);
				}
			}
			return (srvs);
		}

		[STAThread]
		static void Main() 
		{
			ArrayList list = GetSQLServerList();
			foreach (string name in list) 
			{
				Console.WriteLine(name);	
			}
			Console.ReadLine();
		}
	}
}