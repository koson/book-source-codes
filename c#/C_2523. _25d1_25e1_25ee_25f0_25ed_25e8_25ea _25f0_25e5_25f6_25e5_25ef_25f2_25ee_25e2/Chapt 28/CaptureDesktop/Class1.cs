using System;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace CaptureDesktop
{
	class Class1
	{
		[DllImport("user32.dll")] 
 		public extern static IntPtr GetDesktopWindow(); 
  
		[System.Runtime.InteropServices.DllImport("user32.dll")] 
 		public static extern IntPtr GetWindowDC(IntPtr hwnd); 
  
		[System.Runtime.InteropServices.DllImport("gdi32.dll")] 
 		public static extern UInt64 BitBlt 
 			(IntPtr hDestDC, 
 			int x, int y, int nWidth, int nHeight, 
 			IntPtr hSrcDC, 
 			int xSrc, int ySrc, 
 			System.Int32 dwRop); 
 
		[STAThread]
		static void Main(string[] args)
		{
			Image myImage = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height); 
 			Graphics gr1 = Graphics.FromImage(myImage); 
 
			IntPtr dc1 = gr1.GetHdc(); 
 			IntPtr dc2 = GetWindowDC(GetDesktopWindow()); 
 
			BitBlt(dc1, 0, 0, Screen.PrimaryScreen.Bounds.Width, 
 				Screen.PrimaryScreen.Bounds.Height, dc2, 0, 0, 13369376); 
 
			gr1.ReleaseHdc(dc1); 
 
			myImage.Save("screenshot.jpg", ImageFormat.Jpeg); 
 
		}
	}
}
