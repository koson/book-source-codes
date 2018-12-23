// OutputFormatControls - allow the user to reformat input
//                        numbers using a variety of format
//                        controls input at run time
namespace OutputFormatControls
{
  using System;

  public class Program
  {
    public static void Main(string[] args)
    {
      // keep looping - inputting numbers until the user 
      // enters a blank line rather than a number
      for(;;)
      {
        // first input a number - terminate when the user 
        // inputs nothing but a blank line
        Console.WriteLine("Enter a double number");
        string sNumber = Console.ReadLine();
        if (sNumber.Length == 0)
        {
          break;
        }
        double dNumber = Double.Parse(sNumber);

        // now input the control codes; split them
        // using spaces as dividers
        Console.WriteLine("Enter the control codes"
                          + " separated by a blank");
        char[] separator = {' '};
        string sFormatString = Console.ReadLine();
        string[] sFormats = sFormatString.Split(separator);

        // loop through the individual format controls
        foreach(string s in sFormats)
        {
          if (s.Length != 0)
          {
            // create a complete format control
            // from the control letters entered earlier
            string sFormatCommand = "{0:" + s + "}";

            // output the number entered using the
            // reconstructed format control
            Console.Write(
                  "The format control {0} results in ", sFormatCommand);
            try
            {
              Console.WriteLine(sFormatCommand, dNumber);
            }
            catch(Exception)
            {
              Console.WriteLine("<illegal control>");
            }
            Console.WriteLine();
          }
        }
      }

      // wait for user to acknowledge
      Console.WriteLine("Press Enter to terminate...");
      Console.Read();
    }
  }
}

