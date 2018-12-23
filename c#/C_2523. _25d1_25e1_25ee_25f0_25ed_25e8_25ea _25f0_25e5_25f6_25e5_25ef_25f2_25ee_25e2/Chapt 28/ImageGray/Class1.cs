using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace ImageGray
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			// Загружаем входной файл
			Bitmap bmp = new Bitmap(@"Add_IWshRuntimeLibrary.bmp");

			// Атрибуты серого изображения
			ImageAttributes ia = new ImageAttributes(); 
			ColorMatrix cm = new ColorMatrix(); 
			cm.Matrix00 = 1/3f; 
			cm.Matrix01 = 1/3f; 
			cm.Matrix02 = 1/3f; 
			cm.Matrix10 = 1/3f; 
			cm.Matrix11 = 1/3f; 
			cm.Matrix12 = 1/3f; 
			cm.Matrix20 = 1/3f; 
			cm.Matrix21 = 1/3f; 
			cm.Matrix22 = 1/3f; 
			ia.SetColorMatrix(cm); 

			// Рисуем серое
			Graphics g = Graphics.FromImage(bmp);
			g.DrawImage(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, 
				bmp.Width, bmp.Height, 
 				GraphicsUnit.Pixel, ia); 
 
			// Сохраняем выходной файл
			bmp.Save("out.bmp");
		}
	}
}
