using System;
using System.Runtime.InteropServices;

namespace DeviceEnumerator
{
	class Class1
	{
		[STAThread]
		static unsafe void Main(string[] args)
		{
			int PnPHandle = SetupAPI.SetupDiGetClassDevs(
				null,
				null, 
				null,
				SetupAPI.ClassDevsFlags.DIGCF_ALLCLASSES | SetupAPI.ClassDevsFlags.DIGCF_PRESENT
			);
			Console.WriteLine("PnPHandle={0}", PnPHandle);

			int	result		= -1;
			int DeviceIndex = 0;

			while (result != 0)
			{
				SetupAPI.SP_DEVINFO_DATA DeviceInfoData = new SetupAPI.SP_DEVINFO_DATA();
				DeviceInfoData.cbSize = Marshal.SizeOf(DeviceInfoData);

				result = SetupAPI.SetupDiEnumDeviceInfo(PnPHandle, DeviceIndex, ref DeviceInfoData);

				if (result == 1)
				{
					int RequiredSize = 0;
					SetupAPI.DATA_BUFFER Buffer = new SetupAPI.DATA_BUFFER();
					result = SetupAPI.SetupDiGetDeviceRegistryProperty(
						PnPHandle,
						ref DeviceInfoData,
						SetupAPI.RegPropertyType.SPDRP_DEVICEDESC,
						null, 
						ref Buffer,
						1024,
						ref RequiredSize
						);


					Console.WriteLine("{0}", Buffer.Buffer);

				}

				DeviceIndex++;
			}

			Console.ReadLine();
		}
	}
}
