using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace ImageChangeSize
{
        class Class1
        {
 
                [STAThread]
                static void Main(string[] args)
                {
                        // ������� ����
                        Bitmap inBmp = new Bitmap(@"Add_IWshRuntimeLibrary.bmp"); 

                                                Bitmap outBmp;
                       
                        // ������� �������������� �������
                        outBmp = new Bitmap(inBmp, inBmp.Width/2, inBmp.Height/2); 
                        outBmp.Save("out_2.bmp");

                        // ����������������� �������������� �������
                        outBmp = new Bitmap(inBmp.Width/2, inBmp.Height/2, PixelFormat.Format24bppRgb); 
                        Graphics g = Graphics.FromImage(outBmp); 
                        g.InterpolationMode = InterpolationMode.HighQualityBicubic; 
                        g.DrawImage(inBmp, 0, 0, outBmp.Width, outBmp.Height); 
                        outBmp.Save("out_inter.bmp");
                }
        }
}
