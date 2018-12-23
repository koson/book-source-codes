using System;

namespace StringSample
{
   class RemoveCharacters
   {
      static void Main(string[] args)
      {
         string sUsersFullName = "Austin Joseph Ryan"; 

         string[] sNameParts = sUsersFullName.Split(
            char.Parse(" "));

         if (sNameParts.GetUpperBound(0)==2)
         {
            string sShortName =
               sUsersFullName.Remove(sNameParts[0].Length,
                  sNameParts[1].Length + 1);
            Console.WriteLine(sShortName);
         }
      }
   }
}
