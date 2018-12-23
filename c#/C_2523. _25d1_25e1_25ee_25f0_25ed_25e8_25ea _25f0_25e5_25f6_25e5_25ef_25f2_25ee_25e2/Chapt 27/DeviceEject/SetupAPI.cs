namespace DeviceEnumerator
{
	using System;
	using System.Runtime.InteropServices;

	public class SetupAPI
	{
		[DllImport("hid.dll", SetLastError=true)]
		public static extern  unsafe void  HidD_GetHidGuid(
			ref Guid lpHidGuid
			);

		 
		[DllImport("setupapi.dll", SetLastError=true)]
		public static extern  unsafe int SetupDiGetClassDevs(
			ref Guid  lpGuid,
			int*	  Enumerator,
			int*      hwndParent,
			ClassDevsFlags  Flags
			);

		[DllImport("setupapi.dll", SetLastError=true)]
		public static extern  unsafe int SetupDiGetClassDevs(
			int*  guid,
			int*  Enumerator,
			int*  hwndParent,
			ClassDevsFlags  Flags
			);

		[StructLayout(LayoutKind.Sequential)]
		public struct SP_DEVINFO_DATA
		{
			public int  cbSize;
			public Guid ClassGuid;
			public int  DevInst;
			public int  Reserved;
		}


		[DllImport("setupapi.dll", SetLastError=true)]
		public static extern unsafe int SetupDiEnumDeviceInfo(
			int  DeviceInfoSet,
			int	 Index, 
			ref  SP_DEVINFO_DATA DeviceInfoData
			);


		[Flags]
			public enum ClassDevsFlags
		{
			DIGCF_DEFAULT			= 0x00000001,
			DIGCF_PRESENT			= 0x00000002,
			DIGCF_ALLCLASSES		= 0x00000004,
			DIGCF_PROFILE			= 0x00000008,
			DIGCF_DEVICEINTERFACE	= 0x00000010,
		}

		// Device interface data
		[StructLayout(LayoutKind.Sequential)]
			public unsafe struct SP_DEVICE_INTERFACE_DATA 
		{
			public int cbSize;
			public Guid InterfaceClassGuid;
			public int Flags;
			public int Reserved;
		}

		// Device interface detail data
		[StructLayout(LayoutKind.Sequential, CharSet= CharSet.Ansi)]
		public unsafe struct PSP_DEVICE_INTERFACE_DETAIL_DATA
		{
			public int  cbSize;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst= 256)]
			public string DevicePath;
		}


		[DllImport("setupapi.dll", SetLastError=true)]
		public static extern  unsafe int SetupDiEnumDeviceInterfaces(
			int  DeviceInfoSet,
			int  DeviceInfoData,
			ref  Guid  lpHidGuid,
			int  MemberIndex,
			ref  SP_DEVICE_INTERFACE_DATA lpDeviceInterfaceData);


		[DllImport("setupapi.dll", SetLastError=true)]
		public static extern  unsafe int SetupDiGetDeviceInterfaceDetail(
			int  DeviceInfoSet,
			ref SP_DEVICE_INTERFACE_DATA lpDeviceInterfaceData,
			int* aPtr,
			int detailSize,
			ref int requiredSize,
			int* bPtr);

		[DllImport("setupapi.dll", SetLastError=true)]
		public static extern  unsafe int SetupDiGetDeviceInterfaceDetail(
			int  DeviceInfoSet,
			ref SP_DEVICE_INTERFACE_DATA lpDeviceInterfaceData,
			ref PSP_DEVICE_INTERFACE_DETAIL_DATA myPSP_DEVICE_INTERFACE_DETAIL_DATA,
			int detailSize,
			ref int requiredSize,
			int* bPtr);

		public enum RegPropertyType
		{
			SPDRP_DEVICEDESC                  = 0x00000000, // DeviceDesc (R/W)
			SPDRP_HARDWAREID                  = 0x00000001, // HardwareID (R/W)
			SPDRP_COMPATIBLEIDS               = 0x00000002, // CompatibleIDs (R/W)
			SPDRP_UNUSED0                     = 0x00000003, // unused
			SPDRP_SERVICE                     = 0x00000004, // Service (R/W)
			SPDRP_UNUSED1                     = 0x00000005, // unused
			SPDRP_UNUSED2                     = 0x00000006, // unused
			SPDRP_CLASS                       = 0x00000007, // Class (R--tied to ClassGUID)
			SPDRP_CLASSGUID                   = 0x00000008, // ClassGUID (R/W)
			SPDRP_DRIVER                      = 0x00000009, // Driver (R/W)
			SPDRP_CONFIGFLAGS                 = 0x0000000A, // ConfigFlags (R/W)
			SPDRP_MFG                         = 0x0000000B, // Mfg (R/W)
			SPDRP_FRIENDLYNAME                = 0x0000000C, // FriendlyName (R/W)
			SPDRP_LOCATION_INFORMATION        = 0x0000000D ,// LocationInformation (R/W)
			SPDRP_PHYSICAL_DEVICE_OBJECT_NAME = 0x0000000E, // PhysicalDeviceObjectName (R)
			SPDRP_CAPABILITIES                = 0x0000000F, // Capabilities (R)
			SPDRP_UI_NUMBER                   = 0x00000010, // UiNumber (R)
			SPDRP_UPPERFILTERS                = 0x00000011, // UpperFilters (R/W)
			SPDRP_LOWERFILTERS                = 0x00000012, // LowerFilters (R/W)
			SPDRP_BUSTYPEGUID                 = 0x00000013, // BusTypeGUID (R)
			SPDRP_LEGACYBUSTYPE               = 0x00000014, // LegacyBusType (R)
			SPDRP_BUSNUMBER                   = 0x00000015, // BusNumber (R)
			SPDRP_ENUMERATOR_NAME             = 0x00000016, // Enumerator Name (R)
			SPDRP_SECURITY                    = 0x00000017, // Security (R/W, binary form)
			SPDRP_SECURITY_SDS                = 0x00000018, // Security (W, SDS form)
			SPDRP_DEVTYPE                     = 0x00000019, // Device Type (R/W)
			SPDRP_EXCLUSIVE                   = 0x0000001A, // Device is exclusive-access (R/W)
			SPDRP_CHARACTERISTICS             = 0x0000001B, // Device Characteristics (R/W)
			SPDRP_ADDRESS                     = 0x0000001C, // Device Address (R)
			SPDRP_UI_NUMBER_DESC_FORMAT       = 0x0000001E, // UiNumberDescFormat (R/W)
			SPDRP_MAXIMUM_PROPERTY            = 0x0000001F  // Upper bound on ordinals
		}

		[DllImport("setupapi.dll", SetLastError=true)]
		public  static extern unsafe int SetupDiGetDeviceRegistryProperty(
			int					DeviceInfoSet,
			ref SP_DEVINFO_DATA DeviceInfoData,
			RegPropertyType		Property, 
			int*				PropertyRegDataType, 
			int*				PropertyBuffer, 
			int					PropertyBufferSize, 
			ref int				RequiredSize
		);

		// Device interface detail data
		[StructLayout(LayoutKind.Sequential, CharSet= CharSet.Ansi)]
		public unsafe struct DATA_BUFFER
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst= 1024)]
			public string Buffer;
		}

		[DllImport("setupapi.dll", SetLastError=true)]
		public  static extern unsafe int SetupDiGetDeviceRegistryProperty(
			int					DeviceInfoSet,
			ref SP_DEVINFO_DATA DeviceInfoData,
			RegPropertyType		Property, 
			int*				PropertyRegDataType, 
			ref DATA_BUFFER		PropertyBuffer, 
			int					PropertyBufferSize, 
			ref int				RequiredSize
		);

		public const int DN_HAS_PROBLEM				= 0x0400;
		public const int CM_PROB_DISABLED			= 0x0016;
		public const int CM_PROB_HARDWARE_DISABLED	= 0x001D;
		public const int DN_DISABLEABLE				= 0x2000;
		public const int DN_REMOVABLE				= 0x4000;


		[DllImport("setupapi.dll", SetLastError=true)]
		public static extern unsafe int CM_Get_DevNode_Status(
			ref int pulStatus, 
			ref int pulProblemNumber, 
			int		DevInst, 
			int		ulFlags
		);

		[DllImport("setupapi.dll", SetLastError=true)]
		public static extern unsafe int CM_Request_Device_Eject(
			int					dnDevInst,
			int	*			    pVetoType,
			int *				pszVetoName, 
			int					ulNameLength,
			int					ulFlags
			); 

	}
}
