using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace AnimatedGif
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			// Загружаем GIF
			Image img = Image.FromFile(@"o009.gif");
			
			// Число фреймов в анимированном gif
			FrameDimension dimension = new FrameDimension(img.FrameDimensionsList[0]);  
			int frameCount = img.GetFrameCount(dimension); 
			Console.WriteLine("Фреймов: {0}", frameCount);

			// Переписываем gif в набор bmp
			for (int i=0; i<frameCount; i++)
			{
				img.SelectActiveFrame(dimension, i);
				MemoryStream ms = new MemoryStream();
				img.Save(ms, ImageFormat.Bmp);
				Image outImg = Image.FromStream(ms);
				outImg.Save(string.Format("out{0}.bmp", i));
			}

			Console.ReadLine();
		}
	}
}
