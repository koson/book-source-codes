using System;

namespace StringSample
{
   class Compare
   {
      static void Main()
      {
         Console.WriteLine("Please enter your password " + 
            "to enter the specified Photo Gallery:");
         
         string sPassword = Console.ReadLine();
         string sDatabasedPassword = "opensaysme";

         if (sDatabasedPassword.CompareTo(sPassword)==0)
            Console.WriteLine("You can view the photos");
         else
            Console.WriteLine("You do not have permission" 
               + " to view the photos");
      }
   }
}
