// FileWrite - write input from the Console into a text file
using System;
using System.IO;

namespace FileWrite
{
  public class Program
  {
    public static void Main(string[] args)
    {
      // create the filename object - the while loop allows us to 
      // keep trying with different filenames until we succeed
      StreamWriter sw = null;
      string sFileName = "";
      while(true)
      {
        try
        {
          // enter output filename (simply hit Enter to quit)
          Console.Write("Enter filename "
                      + "(Enter blank filename to quit):");
          sFileName = Console.ReadLine();
          if (sFileName.Length == 0)
          {
            // no filename - this jumps beyond the while
            // loop to safety
            break;
          }

          // open file for writing; throw an exception if the
          // file already exists:
          //   FileMode.CreateNew to create a file if it
          //                   doesn't already exist or throw
          //                   an exception if file exists
          //   FileMode.Append to create a new file or append
          //                   to an existing file
          //   FileMode.Create to create a new file or 
          //                   truncate an existing file

          //   FileAccess possibilities are:
          //                   FileAccess.Read, 
          //                   FileAccess.Write,
          //                   FileAccess.ReadWrite
          FileStream fs = File.Open(sFileName,
                                    FileMode.CreateNew,
                                    FileAccess.Write);

          // generate a file stream with UTF8 characters
          // second parameter defaults to UTF8, so can be omitted
          sw = new StreamWriter(fs, System.Text.Encoding.UTF8);

          // read one string at a time, outputting each to the
          // FileStream open for writing
          Console.WriteLine("Enter text; enter blank line to stop");
          while (true)
          {
            // read next line from Console; quit if line is blank
            string sInput = Console.ReadLine();
            if (sInput.Length == 0)
            {
              break;
            }
            // write the line just read to output file
            sw.WriteLine(sInput);
          }
          // close the file we just created
          sw.Close();
          sw = null;
        }
        catch (IOException fe)
        {
          // whoops -- an error occurred somewhere during the processing of the 
          // file - tell the user what the full name of the file is:
          // tack the name of the default directory onto the filename
          string sDir = Directory.GetCurrentDirectory();
          string s = Path.Combine(sDir, sFileName); // useful Path class
          Console.WriteLine("Error on file {0}", s);

          // now output the error message in the exception
          Console.WriteLine(fe.Message);
        }
      }

      // wait for user to acknowledge the results
      Console.WriteLine("Press Enter to terminate...");
      Console.Read();
    }
  }
}
