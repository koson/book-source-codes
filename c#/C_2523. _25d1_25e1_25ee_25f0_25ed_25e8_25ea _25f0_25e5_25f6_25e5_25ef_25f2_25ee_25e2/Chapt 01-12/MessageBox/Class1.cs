using System;
using System.Runtime.InteropServices; 

namespace MessageBox
{
	class Class1
	{
		[DllImport("user32.dll", EntryPoint="MessageBox",  SetLastError=true,
			 CharSet=CharSet.Auto)]     
		public static extern int MessageBox(int hWnd, String strMessage, 
			String strCaption, uint uiType);


		[STAThread]
		static void Main(string[] args)
		{
			MessageBox(0, "Вызов функции Win32!", ".NET", 0); 
		}
	}
}
