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
			// �������� ����
			string fromFileName = @"IMGP0517.jpg";
			// ���� ����������
			string toFileName   = @"IMGP0517_.jpg";

			// ������ �������
			int sizeX = 200;
			int sizeY = 200;

			// ��������� �������� ��������
			Image image = Image.FromFile(fromFileName);

			// ������� bitmap ������� �������
			Bitmap bmp = new Bitmap(sizeX, sizeY);
			bmp.MakeTransparent();
	
			// ������ �� bitmap ��������
			Graphics graphics = Graphics.FromImage(bmp);
			graphics.CompositingQuality = CompositingQuality.HighQuality;
			graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
			graphics.DrawImage(image, 0, 0, sizeX, sizeY);
			graphics.Flush();
	
			// ��������� ��������� (� ����������� RawFormat)
			File.Delete(toFileName);
			FileStream stream = new FileStream(toFileName, FileMode.Create);
			bmp.Save(stream, image.RawFormat);
			stream.Close();

			// image ������ �� �����
			image.Dispose();
		}
	}
}
