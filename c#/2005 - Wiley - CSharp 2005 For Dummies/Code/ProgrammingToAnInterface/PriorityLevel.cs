using System;
using System.Collections.Generic;
namespace IPackageExample2
{
   public class PriorityLevel
   {
      private int priority;
      public PriorityLevel(int priority)
      {
         this.priority = priority;
      }

      public int Priority
      {
         get { return priority; }
      }
   }
   // here's the old enum
   //  public enum Priorities
   //  {
   //    // equivalent to 0, 1, 2, 3, 4
   //    // given Priorites p = Priorities.Manana,
   //    // the cast (int)p == 0
   //    // (a bit of a hack)
   //    Manana, Low, Medium, High, Urgent
   //  }
}
