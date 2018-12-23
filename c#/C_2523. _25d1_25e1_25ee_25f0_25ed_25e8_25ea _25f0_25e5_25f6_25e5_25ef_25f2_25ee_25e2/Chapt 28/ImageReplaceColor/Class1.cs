using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace ImageReplaceColor
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			// Загружаем входной файл
			Bitmap bmp = new Bitmap(@"Add_IWshRuntimeLibrary.bmp");

			// Атрибуты серого изображения
			ImageAttributes
				ia = new ImageAttributes();
			ColorMap[] clrmap = new ColorMap[1]{ new ColorMap() };
			clrmap[0].OldColor = Color.Black;
			clrmap[0].NewColor = Color.Red;
			ia.SetRemapTable(clrmap);
			// Рисуем 
			Graphics g = Graphics.FromImage(bmp);
			g.DrawImage(bmp, new Rectangle(0, 0, 
				bmp.Width, bmp.Height), 0, 0,
				bmp.Width, bmp.Height,
				GraphicsUnit.Pixel, ia);
			// Сохраняем выходной файл
			bmp.Save("out.bmp");
		}
	}
}
