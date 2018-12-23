using System;
namespace StringSample
{  class Extract
   {  static void Main(string[] args)
      {  string sPhoto = "src_fmlyreunion_jan2001_001.jpg";
         string sFilePrefix;        
         string sBasePhoto =sPhoto.Substring(4);
         Console.WriteLine(sBasePhoto);
         Console.WriteLine("Please choose format to view?");
         Console.WriteLine("[0]Low Resolution");
         Console.WriteLine("[1]High Resolution");
         Console.Write("?: ");

         string sSelection = Console.ReadLine();
         switch (sSelection)
         {  case "0" :
               sFilePrefix = "lri_"; break;
            case "1" :
               sFilePrefix = "hri_"; break;
            default :
               sFilePrefix = "src_"; break;}
         string sFullFile = sFilePrefix + sBasePhoto;
         Console.WriteLine("You will view {0}", sFullFile);
}}}
