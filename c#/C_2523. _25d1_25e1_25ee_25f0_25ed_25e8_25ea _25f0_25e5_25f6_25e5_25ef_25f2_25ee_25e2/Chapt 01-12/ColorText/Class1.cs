using System;
using System.Runtime.InteropServices;

namespace ColorText
{
	public class ConsoleColorManager
	{
		private Handles m_Handle;

		// ��������� ���������� �������
		public enum Handles
		{
			STD_INPUT_HANDLE  = -10,
			STD_OUTPUT_HANDLE = -11,
			STD_ERROR_HANDLE  = -12
		}

		// ����������� ����� GetStdHandle
		[DllImportAttribute("Kernel32.dll")]
		private static extern IntPtr GetStdHandle
		(
			int nStdHandle      // ��� ����������� ������
		);

		// ����������� ����� SetConsoleTextAttribute
		[DllImportAttribute("Kernel32.dll")]
		private static extern bool SetConsoleTextAttribute
		(
			IntPtr hConsoleOutput,  // ���������� ������
			int wAttributes         // ���� ������ � ���� 
		);

		// ����� (�����)
		[Flags]
		public enum Color
		{
			Black	= 0x0000,
			Blue	= 0x0001,
			Green	= 0x0002, 
			Cyan	= 0x0003,
			Red		= 0x0004,
			Magenta = 0x0005,
			Yellow	= 0x0006,
			Grey	= 0x0007,
			White	= 0x0008
		}

		// �� ��������� ����� ��������� ������� OUTPUT 
		public ConsoleColorManager()
		{
			m_Handle = Handles.STD_OUTPUT_HANDLE;
		}

		public ConsoleColorManager(Handles Handle)
		{
			m_Handle = Handle;
		}

		// ��������� ����� �� ���������
		public bool SetDefaultColor()
		{
			return SetColor(Color.Grey, true);
		}

		// ��������� ����� ������
		// ���� fLight ������ ������� �����
		public bool SetColor(Color aColor, bool fLight)
		{
			// �������� ����������
			IntPtr ConsoleHandle = GetStdHandle((int)m_Handle);
      
			// �������� �������� �����
			int colorMask = (int)aColor;

			// ���� ����� ����� ����, ��������� White
			if (fLight)
				colorMask |= (int)Color.White;

			// �������������
			return SetConsoleTextAttribute(ConsoleHandle, colorMask);
		}
	}


	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			ConsoleColorManager colorManager = new ConsoleColorManager();
			// ����-�������
			colorManager.SetColor(ConsoleColorManager.Color.Green, true);
			Console.WriteLine("Text in green");

			// �����-�������
			colorManager.SetColor(ConsoleColorManager.Color.Red  , false);
			Console.WriteLine("Text in red");

			// ��������������� ���� �� ���������
			colorManager.SetDefaultColor();

			Console.ReadLine();
		}
	}
}
