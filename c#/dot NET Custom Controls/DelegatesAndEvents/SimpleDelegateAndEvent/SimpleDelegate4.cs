using System;
using System.IO;

namespace Akadia.SimpleDelegate
{
    // Delegate Specification
    public class MyClass
    {
        // Declare a delegate that takes a single string parameter
        // and has no return type.
        public delegate void LogHandler(string message);

        // The use of the delegate is just like calling a function directly,
        // though we need to add a check to see if the delegate is null
        // (that is, not pointing to a function) before calling the function.
        public void Process(LogHandler logHandler)
        {
            if (logHandler != null)
            {
                logHandler("Process() begin");
            }

            if (logHandler != null)
            {
                logHandler ("Process() end");
            }
        }
    }

    // The FileLogger class merely encapsulates the file I/O
    public class FileLogger
    {
        FileStream fileStream;
        StreamWriter streamWriter;

        // Constructor
        public FileLogger(string filename)
        {
            fileStream = new FileStream(filename, FileMode.Create);
            streamWriter = new StreamWriter(fileStream);
        }

        // Member Function which is used in the Delegate
        public void Logger(string s)
        {
            streamWriter.WriteLine(s);
        }

        public void Close()
        {
            streamWriter.Close();
            fileStream.Close();
        }
    }

    // Main() is modified so that the delegate points to the Logger()
    // function on the fl instance of a FileLogger. When this delegate
    // is invoked from Process(), the member function is called and
    // the string is logged to the appropriate file.
    public class TestApplication
    {
        static void Logger(string s)
        {
            Console.WriteLine(s);
        }

        static void Main(string[] args)
        {
            FileLogger fl = new FileLogger("process.log");

            MyClass myClass = new MyClass();

            // Crate an instance of the delegates, pointing to the static
            // Logger() function defined in the TestApplication class and
            // then to member function on the fl instance of a FileLogger.
            MyClass.LogHandler myLogger = null;
            myLogger += new MyClass.LogHandler(Logger);
            myLogger += new MyClass.LogHandler(fl.Logger);

            myClass.Process(myLogger);
            fl.Close();
        }
    }
}
