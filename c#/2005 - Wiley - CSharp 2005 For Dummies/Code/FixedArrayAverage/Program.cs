// FixedArrayAverage - average a fixed array of
//                     numbers using a loop
namespace FixedArrayAverage
{
  using System;

  public class Program
  {
    public static void Main(string[] args)
    {
      double[] dArray = 
              {5, 2, 7, 3.5, 6.5, 8, 1, 9, 1, 3};

      // accumulate the values in the array 
      // into the variable dSum
      double dSum = 0;
      for (int i = 0; i < 10; i++)
      {
        dSum = dSum + dArray[i];
      }

      // now calculate the average
      double dAverage = dSum / 10;
      Console.WriteLine(dAverage);

      // wait for user to acknowledge the results
      Console.WriteLine("Press Enter to terminate...");
      Console.Read();
    }
  }
}
