// VariableArrayAverage - average an array whose size is
//                        determined by the user at run time.
//                        Accumulating the values in an array
//                        allows them to be referenced as often
//                        as desired. In this case, the array
//                        creates an attractive output.
namespace VariableArrayAverage
{
  using System;

  public class Program
  {
    public static void Main(string[] args)
    {
      // first read in the number of doubles
      // the user intends to enter
      Console.Write("Enter the number of values to average: ");
      string sNumElements = Console.ReadLine();
      int numElements = Convert.ToInt32(sNumElements);
      Console.WriteLine();

      // now declare an array of that size
      double[] dArray = new double[numElements];

      // accumulate the values into an array
      for (int i = 0; i < numElements; i++)
      {
        // prompt the user for another double
        Console.Write("enter double #" + (i + 1) + ": ");
        string sVal = Console.ReadLine();
        double dValue = Convert.ToDouble(sVal);

        // add this to the array
        dArray[i] = dValue;
      }


      // accumulate 'numElements' values from
      // the array in the variable dSum
      double dSum = 0;
      for (int i = 0; i < numElements; i++)
      {
        dSum = dSum + dArray[i];
      }

      // now calculate the average
      double dAverage = dSum / numElements;

      // output the results in an attractive format
      Console.WriteLine();
      Console.Write(dAverage 
                  + " is the average of (" 
                  + dArray[0]);
      for (int i = 1; i < numElements; i++)
      {
        Console.Write(" + " + dArray[i]);
      }
      Console.WriteLine(") / " + numElements);

      // wait for user to acknowledge the results
      Console.WriteLine("Press Enter to terminate...");
      Console.Read();
    }
  }
}
