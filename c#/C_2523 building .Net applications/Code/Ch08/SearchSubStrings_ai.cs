using System;
using System.Text.RegularExpressions;
namespace StringSample
{
   class Search
   {
       static void Main()
      {
         string[] sFileNames = new string[3] {
            "allphotos.aspx",
            "lri_familyreunion_jan2001_001.jpg", 
            "hri_familyreunion_jan2001_001.jpg"};
         Regex rePictureFile = new Regex(".jpg");

         foreach (string sFileName in sFileNames)
         {
            if (rePictureFile.Match(sFileName).Success)
               Console.WriteLine("{0} is a photo file.",
                  sFileName);
         }        
      }
   }
}