using System;
namespace StringSample
{
class AssignmentAndLength
   {
   static void Main()
      {
         String sSpacesCount = "     6789";
         int iSpacesCount = sSpacesCount.Length;

         Console.WriteLine (
         "The greeting: \n{0}\nis {1} characters long.",
            sSpacesCount, iSpacesCount);
      }
   }
}
