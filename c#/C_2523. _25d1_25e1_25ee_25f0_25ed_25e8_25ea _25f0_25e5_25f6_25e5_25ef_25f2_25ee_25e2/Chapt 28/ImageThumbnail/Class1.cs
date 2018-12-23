using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace ImageThumbnail
{
        class Class1
        {
                public static bool ThumbnailCallback() 
                { 
                        return false; 
                } 
 

                [STAThread]
                static void Main(string[] args)
                {
                        // ������� ����
                        Bitmap inBmp = new Bitmap(@"Add_IWshRuntimeLibrary.bmp"); 

                        // �������������� � ����������� ��������� �������
                        Bitmap outBmp = (Bitmap) inBmp.GetThumbnailImage(75, 75, 
                                new Image.GetThumbnailImageAbort(ThumbnailCallback), IntPtr.Zero); 
 
                        // ��������� �������� ����
                        outBmp.Save("out.bmp");
                }
        }
}
