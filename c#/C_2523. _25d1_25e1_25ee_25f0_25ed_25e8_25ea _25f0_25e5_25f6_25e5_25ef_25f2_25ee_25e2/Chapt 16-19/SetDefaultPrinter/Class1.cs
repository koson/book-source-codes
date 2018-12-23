using System;
using IWshRuntimeLibrary;

namespace SetDefaultPrinter
{
	class DefaultPrinter
	{
		[STAThread]
		static void Main(string[] args)
		{
			WshNetwork network = new WshNetwork();

			// получить список принтеров
			IWshCollection Printers = network.EnumPrinterConnections();
			if (Printers.Count() > 0)
			{
				// индекс устанавливаемого принтера в списке принтеров
				object index = (object)"1";
				// установка принтера с выбранным индексом
				// как принтера по умолчанию
				network.SetDefaultPrinter((string)Printers.Item(ref index));
			}
		}
	}
}
