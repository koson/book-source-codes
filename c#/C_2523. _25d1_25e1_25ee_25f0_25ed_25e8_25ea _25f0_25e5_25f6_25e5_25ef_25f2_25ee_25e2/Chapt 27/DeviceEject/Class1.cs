using System;
using System.Runtime.InteropServices;

namespace DeviceEnumerator
{
	class Class1
	{
		[STAThread]
		static unsafe void Main(string[] args)
		{
			Guid UsbGuid = new Guid("{36FC9E60-C465-11CF-8056-444553540000}");

			int PnPHandle = SetupAPI.SetupDiGetClassDevs(
				ref UsbGuid,
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
					if (IsRemovable(DeviceInfoData))
					{
						Console.WriteLine("{0}", GetRegistryProperty(PnPHandle, ref DeviceInfoData, SetupAPI.RegPropertyType.SPDRP_DEVICEDESC));
						if (SetupAPI.CM_Request_Device_Eject(DeviceInfoData.DevInst, null, null, 0, 0) == 0)
							Console.WriteLine("���������� ������� ���������.");
					}
				}

				DeviceIndex++;
			}

			Marshal.FreeHGlobal((System.IntPtr)PnPHandle);

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

		public unsafe static bool IsRemovable(SetupAPI.SP_DEVINFO_DATA DevData)
		{
			int Status	= 0;
			int Problem	= 0;

			SetupAPI.CM_Get_DevNode_Status(ref Status, ref Problem, DevData.DevInst, 0);
			return ((Status & SetupAPI.DN_REMOVABLE)!=0);
		}

	}
}
