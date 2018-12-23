using System;

namespace StringSample
{
   /// <summary>
   /// Take the same Greeting and pad with a '-' instead of
   /// a space.  Do this with taking the string length plus 
   /// padding amount.
   /// </summary>
   class Pad {
      static void Main()
      {  string sGreeting = 
            "Welcome to 'My Personal Photo Album'";
         string sGreetingPadded;

         sGreetingPadded = sGreeting.PadLeft
            ((sGreeting.Length + 5),char.Parse("-"));
         sGreetingPadded = sGreetingPadded.PadRight
            ((sGreetingPadded.Length + 5),char.Parse("-"));

         Console.WriteLine(sGreetingPadded);
      }
   }
}
