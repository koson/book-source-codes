// LoopThroughFiles - loop through all of the files contained in a directory; this
//                    time perform a hex dump though it could have been anything
using System;
using System.IO;

namespace LoopThroughFiles
{
  public class Program
  {
    public static void Main(string[] args)
    {
      // if no directory name provided...
      string sDirectoryName;
      if (args.Length == 0)
      {
        // ...get the name of the current directory...
        sDirectoryName = Directory.GetCurrentDirectory();
      }
      else
      {
        // ...otherwise, assume that the first argument
        // is the name of the directory to use
        sDirectoryName = args[0];
      }
      Console.WriteLine(sDirectoryName);

      // get a list of all of the files in that directory
      FileInfo[] files = GetFileList(sDirectoryName);

      // now iterate through the files in that list
      // performing a hex dump of each file
      foreach(FileInfo file in files)
      {
        // write out the name of the file
        Console.WriteLine("\n\nHex dump of file {0}:", file.FullName);
                          
        // now "dump" the file to the console
        DumpHex(file);

        // wait before outputting next file
        Console.WriteLine("\nEnter return to continue to next file");
        Console.ReadLine();
      }

      // that's it!
      Console.WriteLine("\nNo files left");

      // wait for user to acknowledge the results
      Console.WriteLine("Press Enter to terminate...");
      Console.Read();
    }

    // GetFileList - get a list of all of the files in the specified directory
    public static FileInfo[] GetFileList(string sDirectoryName)
    {
      // start with an empy list
      FileInfo[] files = new FileInfo[0];
      try
      {
        // get directory information
        DirectoryInfo di = new DirectoryInfo(sDirectoryName);

        // that information object has a list of the contents
        files = di.GetFiles();
      }
      catch(Exception e)
      {
        Console.WriteLine("Directory \" + sDirectoryName" + "\" invalid");             
        Console.WriteLine(e.Message);
      }
      return files;
    }

    // DumpHex - given a file, dump out the contents of the file to the console
    public static void DumpHex(FileInfo file)
    {
      // open the file
      FileStream fs;
      try
      {
        fs = file.OpenRead();
      }
      catch(Exception e)
      {
        Console.WriteLine("\nCan't read from \"" + file.FullName + "\"");
        Console.WriteLine(e.Message);
        return;
      }

      // iterate through the contents of the file one line at a time
      for(int nLine = 1; true; nLine++)
      {
        // read up another 10 bytes across (that's all that will fit in a single 
        // line) return when there isn't any more data
        byte[] buffer = new byte[10];
        int numBytes = fs.Read(buffer, 0, buffer.Length);
        if (numBytes == 0)
        {
          return;
        }

        // write out the data just read in a single line preceded by line number
        Console.Write("{0:D3} - ", nLine);
        DumpBuffer(buffer, numBytes);

        // stop every 20 lines so the data doesn't scroll
        // off the top of the Console screen
        if ((nLine % 20) == 0)
        {
          Console.WriteLine("Enter return to continue another 20 lines");
          Console.ReadLine();
        }
      }
    }
 
    // DumpBuffer - write a buffer of characters as a single line in hex format
    public static void DumpBuffer(byte[] buffer, int numBytes)
    {
      for(int index = 0; index < numBytes; index++)
      {
        byte b = buffer[index];
        Console.Write("{0:X2}, ", b);
      }
      Console.WriteLine();
    }
  }
}
