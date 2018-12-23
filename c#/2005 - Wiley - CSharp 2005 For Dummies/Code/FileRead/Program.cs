// FileRead - read a text file and write it out to the Console
using System;
using System.IO;

namespace FileRead
{
  public class Program
  {
    public static void Main(string[] args)
    {
      // we'll need a file reader object
      StreamReader sr;
      string sFileName = "";

      // keep trying to get a real filename until we can find
      // one (the user can only quit by pressing Ctrl+C)
      while(true)
      {
        try
        {
          // enter input filename
          Console.Write("Enter the name of a text file to read:");
          sFileName = Console.ReadLine();

          // user didn't enter anything; throw an exception
          // to indicate that this is not acceptable
          if (sFileName.Length == 0)
          {
            throw new IOException("Null filename entered");
          }

          // open a file stream for reading; don't create the
          // file if it doesn't already exist
          FileStream fs = File.Open(sFileName, FileMode.Open, FileAccess.Read);
             
          // convert this into a StreamReader - this will use
          // the first three bytes of the file to indicate the
          // encoding used (but not the language)
          sr = new StreamReader(fs, true); 
          break;
        }

          // error - report the filename and the error
        catch(IOException fe)
        {
          Console.WriteLine("{0}\n\n", fe.Message);
        }
      }

      // read the contents of the file
      Console.WriteLine("\nContents of file:");
      try
      {
        // read one line at a time
        while(true)
        {
          // read a line
          string sInput = sr.ReadLine();

          // quit when we don't get anything back
          if (sInput == null)
          {
            break;
          }

          // write whatever we read to the console
          Console.WriteLine(sInput);
        }
      }
      catch(IOException fe)
      {

        // snag any read/write errors and report them
        // (this also breaks us out of the loop)
        Console.Write(fe.Message);
      }

      // close the file now that we're done with it
      // (ignore any error)
      try 
      { 
        sr.Close(); 
      } 
      catch {}
      sr = null;

      // wait for user to acknowledge the results
      Console.WriteLine("Press Enter to terminate...");
      Console.Read();
    }
  }
}
