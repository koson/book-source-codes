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
                        // Входной файл
                        Bitmap inBmp = new Bitmap(@"Add_IWshRuntimeLibrary.bmp"); 

                        // Преобразование в пиктограмму заданного размера
                        Bitmap outBmp = (Bitmap) inBmp.GetThumbnailImage(75, 75, 
                                new Image.GetThumbnailImageAbort(ThumbnailCallback), IntPtr.Zero); 
 
                        // Сохраняем выходной файл
                        outBmp.Save("out.bmp");
                }
        }
}
