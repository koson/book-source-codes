using System;
using System.Runtime.InteropServices; 
using System.Reflection; 
using System.Drawing;
using System.Drawing.Imaging;

namespace ImageFromClipboard
{
	class Class1
	{
		public const uint CF_METAFILEPICT = 3; 
 		public const uint CF_ENHMETAFILE = 14; 
  
		[DllImport("user32.dll", CharSet=CharSet.Auto, ExactSpelling=true)] 
 		public static extern bool OpenClipboard(IntPtr hWndNewOwner); 
 		[DllImport("user32.dll", CharSet=CharSet.Auto, ExactSpelling=true)] 
 		public static extern bool CloseClipboard(); 
 		[DllImport("user32.dll", CharSet=CharSet.Auto, ExactSpelling=true)] 
		public static extern IntPtr GetClipboardData(uint format); 
 		[DllImport("user32.dll", CharSet=CharSet.Auto, ExactSpelling=true)] 
		public static extern bool IsClipboardFormatAvailable(uint format); 
 
		[STAThread]
		static void Main(string[] args)
		{

			if (OpenClipboard(new IntPtr(0))) 
			{ 
 				if (IsClipboardFormatAvailable(CF_ENHMETAFILE)) 
 				{ 
 					IntPtr ptr = GetClipboardData(CF_ENHMETAFILE); 
 					if (!ptr.Equals(new IntPtr(0))) 
 					{ 
 						Metafile metafile = new Metafile(ptr,true); 
						metafile.Save("out.bmp");
					} 
				} 
				CloseClipboard(); 
			}
		}
	}
}
