using System;
using System.Text;
namespace StringSample
{
   class Concatenate
   {
      static void Main()
      {
         StringBuilder sbPersonalGreeting = 
            new StringBuilder("Hello, how are you today");

         sbPersonalGreeting.Insert(0,"Danny - ");
         sbPersonalGreeting.Append("?");
         
         Console.WriteLine(sbPersonalGreeting);
      }
   }
}
