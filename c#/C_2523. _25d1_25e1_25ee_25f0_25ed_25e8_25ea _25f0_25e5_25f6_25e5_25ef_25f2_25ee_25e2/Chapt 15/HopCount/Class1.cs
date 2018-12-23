using System;
using System.Net;
using System.Runtime.InteropServices;

namespace HopCount
{
	class Class1
	{
		[DllImport("iphlpapi.dll", ExactSpelling=true, SetLastError=true)]
		private static extern bool GetRTTAndHopCount(uint DestIpAddress, out int HopCount, int MaxHops, out int RTT );

		private static int GetHopCount(string pIpAddresseStr) 
		{
			IPAddress lIpAddressEntry;
			byte[] lIpAddressArray;
			uint lIpAddress;

			int lTTL;
			int lHopCount;
			int lErrorCode;

			lIpAddressEntry = IPAddress.Parse(pIpAddresseStr);
			lIpAddressArray = lIpAddressEntry.GetAddressBytes();
			lIpAddress = 0;
			for(int i = 0; i < lIpAddressArray.Length; i++) 
			{
				lIpAddress += (uint)(lIpAddressArray[i] << (8 * i)); 
			}

			if (!GetRTTAndHopCount(lIpAddress, out lHopCount, 60, out lTTL)) 
			{
				lErrorCode = Marshal.GetLastWin32Error();
				throw new Exception("Ошибка. Код " + lErrorCode);
			}
			return lHopCount;
		}

		[STAThread]
		static void Main(string[] args)
		{
			try
			{
				Console.WriteLine(GetHopCount("127.0.0.1"));
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
			
			Console.ReadLine();

		}
	}
}
