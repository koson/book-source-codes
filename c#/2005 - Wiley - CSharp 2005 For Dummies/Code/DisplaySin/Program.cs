// DisplaySin - generates a sine wave on the console
//              by iterating through the angles from 0 to
//              360, and displaying '*' when j = sin(i)
using System;

namespace DisplaySin
{
  public class Program
  {
    public static void Main(string[] args)
    {
      // we'll need this constant later
      double dTwoPI = 2.0 * Math.PI;
      
      // enter the loop which iterates through the angle
      // from 1 to 360 deqrees (step 15 degrees at a time)
      for(double dDegrees = 0.0; dDegrees <= 360.0; dDegrees += 15.0)
      {
        // now convert that angle into radian measure
        double dRadian = dTwoPI * (dDegrees / 360.0);
        
        // convert that angle into a sin which ranges
        // from -1 to 1
        double dSin = Math.Sin(dRadian);
        
        // finally convert that into a percentage that ranges
        // from 0 to 1
        double dPercentage = (dSin + 1.0) / 2.0;

        // let's plot that by placing a '*' at the location
        // from left to right that best fits our angle
        int nWidthInColumns = 79;
        int nTarget = (int)(nWidthInColumns * dPercentage);
        for (int j = 0; j <= nWidthInColumns; j++)
        {
          char c = ' ';
          if (j == nTarget)
          {
            c = '*';
          }
          Console.Write(c);
        }
        Console.WriteLine();
      }

      // wait for user to acknowledge the results
      Console.WriteLine("Press Enter to terminate...");
      Console.Read();
    }
  }
}

