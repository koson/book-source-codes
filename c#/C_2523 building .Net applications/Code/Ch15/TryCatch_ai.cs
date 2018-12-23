using System;
using System.IO;

namespace ExceptionHandling
{
   /// <summary>
   /// Example of executing code that does not have error handling.
   /// Run this from the commandline and compare to same code that
   /// runs in try/catch blocks
   /// </summary>
   class TryCatch
   {
      static void Main()
      {
         string sTextFile = "missingtextfile.txt";
         String sLine;
         
         StreamReader srTest = File.OpenText(sTextFile);

         Console.WriteLine("Preparing to write file contents....");

         while ((sLine=srTest.ReadLine()) != null) 
         {
            Console.WriteLine(sLine);
         }
      }
   }
}



