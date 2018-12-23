using System;
using System.Drawing;

namespace ImageNegative
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			Bitmap bmp = new Bitmap(@"Add_IWshRuntimeLibrary.bmp");
			for(int x = 0; x < bmp.Width; x++) 
			{ 
				for(int y = 0; y < bmp.Height; y++) 
				{ 
					System.Drawing.Color c = bmp.GetPixel(x,y); 
					bmp.SetPixel(x,y, System.Drawing.Color.FromArgb(255 - c.R, 255 - c.G, 255 - c.B)); 
				} 
			} 
			bmp.Save("out.bmp");
		}
	}
}
