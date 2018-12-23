using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;

namespace ImageResize
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			// Исходный файл
			string fromFileName = @"IMGP0517.jpg";
			// Файл результата
			string toFileName   = @"IMGP0517_.jpg";

			// Нужные размеры
			int sizeX = 200;
			int sizeY = 200;

			// Загружаем исходную картинку
			Image image = Image.FromFile(fromFileName);

			// Создаем bitmap нужного размера
			Bitmap bmp = new Bitmap(sizeX, sizeY);
			bmp.MakeTransparent();
	
			// Рисуем на bitmap картинку
			Graphics graphics = Graphics.FromImage(bmp);
			graphics.CompositingQuality = CompositingQuality.HighQuality;
			graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
			graphics.DrawImage(image, 0, 0, sizeX, sizeY);
			graphics.Flush();
	
			// Сохраняем результат (с сохранением RawFormat)
			File.Delete(toFileName);
			FileStream stream = new FileStream(toFileName, FileMode.Create);
			bmp.Save(stream, image.RawFormat);
			stream.Close();

			// image больше не нужен
			image.Dispose();
		}
	}
}
