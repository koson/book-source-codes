using System;
using Microsoft.DirectX;
using Microsoft.DirectX.DirectInput;

namespace USB.DirectX.HIDDeviceList
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{

			// Получаем DeviceList
			DeviceList dl = Microsoft.DirectX.DirectInput.Manager.Devices;
			Console.WriteLine("Всего устройств: {0}", dl.Count);

			// Для мыши выводит type=Mouse, subtype=1, usage=page=0
			// Для клав выводит type=Keyboard, subtype=4, usage=page=0
			// Для HID  выводит type=Device, subtype=0, usage=1, page=65280

			foreach (DeviceInstance d in dl)
			{
				Console.WriteLine(
					"\n\nType={0} SubType={1} Usage={2} Page={3} Guid={4} ProductName={5}",
					d.DeviceType,
					d.DeviceSubType,
					(UInt16)d.Usage,
					(UInt16)d.UsagePage,
					d.InstanceGuid,
					d.ProductName
					);

			}

			Console.ReadLine();
		}
	}
}