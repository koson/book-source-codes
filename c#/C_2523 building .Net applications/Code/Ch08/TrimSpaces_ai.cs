using System;

namespace StringSample
{
   /// <summary>
   /// The Trim can accept an array of unicode chars that 
   /// will be used to trim from either end of the string.
   /// Note that it does not matter what order the chars 
   /// are set.
   /// </summary>
   class TrimSpaces
   {
      static void Main()
      {
         String sGreeting = 
            " Welcome to My Shared Photo Album! " ;

         Console.Write(sGreeting.Trim(
            char.Parse(" "), char.Parse("!")));        
      }
   }
}
