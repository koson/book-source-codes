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
			// ��������� GIF
			Image img = Image.FromFile(@"o009.gif");
			
			// ����� ������� � ������������� gif
			FrameDimension dimension = new FrameDimension(img.FrameDimensionsList[0]);  
			int frameCount = img.GetFrameCount(dimension); 
			Console.WriteLine("�������: {0}", frameCount);

			// ������������ gif � ����� bmp
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
