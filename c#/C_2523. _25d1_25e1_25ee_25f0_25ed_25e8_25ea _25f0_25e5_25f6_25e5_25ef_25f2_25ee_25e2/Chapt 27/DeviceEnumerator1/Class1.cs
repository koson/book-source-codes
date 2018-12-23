using System;
using System.Runtime.InteropServices;

namespace DeviceEnumerator1
{
	class Class1
	{
		[STAThread]
		static unsafe void Main(string[] args)
		{
			//	DisplayGuid : TGUID = '{4d36e968-e325-11ce-bfc1-08002be10318}';
			//	HidGuid     : TGUID = '{745a17a0-74d3-11d0-b6fe-00a0c90f57da}';
			//	USBGuid     : TGUID = '{36FC9E60-C465-11CF-8056-444553540000}';

			Guid guid = new Guid("{36FC9E60-C465-11CF-8056-444553540000}");

			int PnPHandle = SetupAPI.SetupDiGetClassDevs(
				ref guid,
				null, 
				null,
				SetupAPI.ClassDevsFlags.DIGCF_PRESENT
			);

			int	result		= -1;
			int DeviceIndex = 0;

			while (result != 0)
			{
				SetupAPI.SP_DEVINFO_DATA DeviceInfoData = new SetupAPI.SP_DEVINFO_DATA();
				DeviceInfoData.cbSize = Marshal.SizeOf(DeviceInfoData);
				result = SetupAPI.SetupDiEnumDeviceInfo(PnPHandle, DeviceIndex, ref DeviceInfoData);

				if (result == 1)
				{
					Console.WriteLine("{0}:\n\t{1}\n\t{2}\n\t{3}\n\t{4}", 
						GetRegistryProperty(PnPHandle, ref DeviceInfoData, SetupAPI.RegPropertyType.SPDRP_DEVICEDESC),
						GetRegistryProperty(PnPHandle, ref DeviceInfoData, SetupAPI.RegPropertyType.SPDRP_CLASS		),
						GetRegistryProperty(PnPHandle, ref DeviceInfoData, SetupAPI.RegPropertyType.SPDRP_CLASSGUID	),
						GetRegistryProperty(PnPHandle, ref DeviceInfoData, SetupAPI.RegPropertyType.SPDRP_DRIVER	),
						GetRegistryProperty(PnPHandle, ref DeviceInfoData, SetupAPI.RegPropertyType.SPDRP_MFG		)
						
					);
				}

				DeviceIndex++;
			}

			Console.ReadLine();
		}

		public unsafe static string GetRegistryProperty(int PnPHandle, ref SetupAPI.SP_DEVINFO_DATA DeviceInfoData, SetupAPI.RegPropertyType Property)
		{
			int RequiredSize = 0;
			SetupAPI.DATA_BUFFER Buffer = new SetupAPI.DATA_BUFFER();

			int result = SetupAPI.SetupDiGetDeviceRegistryProperty(
				PnPHandle,
				ref DeviceInfoData,
				Property,
				null, 
				ref Buffer,
				1024,
				ref RequiredSize
				);

			return	Buffer.Buffer;

		}
	}
}
