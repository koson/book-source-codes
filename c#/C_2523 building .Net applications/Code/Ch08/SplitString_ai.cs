using System;

namespace StringSample
{
   /// <summary>
   /// Taking the task further, we can evaluate the first member of the 
   /// string array for the photo type to get a full photo type description.
   /// Then take all the parts and create a custom string.
   /// </summary>
   class Split
   {
      static void Main()
      {
         string sFileName = "hri_disney_jan2001_001.jpg";

         string[] sFileParts = new string[4];
         sFileParts = sFileName.Split(new Char[] {'_'},4);

         // Because the photo filenames have a specified
         // naming convention, it is known that the third
         // member will have the date.
         Console.WriteLine("The photo was taken " + sFileParts[2]);

         // ApplyIt
         
         string sPhotoType;
         string sPhotoEvent = sFileParts[1];
         string sPhotoDate = sFileParts[2];
         string sPhotoIndex = sFileParts[3].Remove(3,4);

         switch (sFileParts[0])
         {
            case "src" :
               sPhotoType = "source image";
               break;

            case "hri" :
               sPhotoType = "high resolution image";
               break;

            case "lri" :
               sPhotoType = "low resolution image";
               break;

            case "tni" :
               sPhotoType = "thumbnail image";
               break;

            default :
               sPhotoType = "unknown image type";
               break;
         }

         Console.WriteLine("The " + sPhotoType + " selected was index " + sPhotoIndex +
            " of pictures at " + sPhotoEvent + " which was taken " + sPhotoDate + "."); 

      }
   }
}
