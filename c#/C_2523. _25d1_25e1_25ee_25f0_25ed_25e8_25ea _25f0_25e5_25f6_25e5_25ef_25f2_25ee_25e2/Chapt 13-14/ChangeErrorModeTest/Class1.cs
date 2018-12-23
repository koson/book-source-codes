using System;
using System.Runtime.InteropServices;

namespace ChangeErrorModeTest
{
	class GetFilesClass
	{
		[Flags]
			public enum ErrorModes
		{
			Default = 0x0,
			FailCriticalErrors = 0x1,
			NoGpFaultErrorBox = 0x2,
			NoAlignmentFaultExcept = 0x4,
			NoOpenFileErrorBox = 0x8000
		}

		public struct ChangeErrorMode : IDisposable
		{
			private int _oldMode;

			public ChangeErrorMode(ErrorModes mode)
			{
				_oldMode = SetErrorMode((int)mode);
			}

			void IDisposable.Dispose() { SetErrorMode(_oldMode); }

			[DllImport("kernel32.dll")]
			private static extern int SetErrorMode(int newMode); 
		}

		[STAThread]
		static void Main(string[] args)
		{
			using(new ChangeErrorMode(ErrorModes.FailCriticalErrors))
			{
				try
				{
					string [] files = System.IO.Directory.GetFiles(@"A:\");
					foreach (string file in files)
					{
						Console.WriteLine(file);
					}
				}
				catch
				{
					Console.WriteLine("Устройство не готово");
				}
			}
		}
	}
}
