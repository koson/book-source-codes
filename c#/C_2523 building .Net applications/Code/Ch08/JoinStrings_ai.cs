using System;
using System.Globalization;

namespace StringSample 
{
   class WeekDays 
   {
      static void Main()
      {
         string [] sDaysOfTheWeek = new string[7];
         DateTimeFormatInfo dtfInfo = 
            new DateTimeFormatInfo();
         sDaysOfTheWeek = dtfInfo.DayNames;
         string sWeekDays = String.Join
            (", ",sDaysOfTheWeek,1,5 );
         Console.WriteLine
            ("The week days are: " + sWeekDays);      
      }
   }
}
