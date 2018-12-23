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

			// �������� ������ ���������
			IWshCollection Printers = network.EnumPrinterConnections();
			if (Printers.Count() > 0)
			{
				// ������ ���������������� �������� � ������ ���������
				object index = (object)"1";
				// ��������� �������� � ��������� ��������
				// ��� �������� �� ���������
				network.SetDefaultPrinter((string)Printers.Item(ref index));
			}
		}
	}
}
