// CustomException - create a custom exception that displays the 
//                   information we want in a more friendly format
using System;

namespace CustomException
{
  public class CustomException : ApplicationException
  {
    private MathClass mathobject;
    private string sMessage;

    public CustomException(string sMsg, MathClass mo)
    {
      mathobject = mo;
      sMessage =  sMsg;
    }

    override public string Message
    {
      get{return String.Format("Message is <{0}>, Object is {1}",
                               sMessage, mathobject.ToString());}
    }

    override public string ToString()
    {
      string s = Message;
      s += "\nException thrown from ";
      s += TargetSite;
      return s;
    }
  }

  // MathClass - a collection of mathematical functions
  //             we created (it's not much to look at yet)
  public class MathClass
  {
    private int nValueOfObject;
    private string sObjectDescription;

    public MathClass(string sDescription, int nValue)
    {
      nValueOfObject = nValue;
      sObjectDescription = sDescription;
    }

    public int Value {get {return nValueOfObject;}}

    // Message - display the message with the value of the
    //           attached MathClass object
    public string Message 
    {
      get
      {
        return String.Format("({0} = {1})", sObjectDescription, nValueOfObject);
      }
    }

    // ToString - extend our custom Message with the 
    //            original Message in the base exception class 
    override public string ToString()
    {
      string s = Message + "\n";
      s += base.ToString();
      return s;
    }

    // Inverse - return 1/x 
    public double Inverse()
    {
      if (nValueOfObject == 0)
      {
        throw new CustomException("Can't take inverse of 0", this);
      }
      return 1.0 / (double)nValueOfObject;
    }
}

  public class Program
  {
    public static void Main(string[] args)
    {
      try
      {
        // take the inverse of 0
        MathClass mathObject = new MathClass("Value", 0);
        Console.WriteLine("The inverse of d.Value is {0}",
                          mathObject.Inverse());
      }
      catch(Exception e)
      {
        Console.WriteLine("\nUnknown fatal error:\n{0}", e.ToString());              
      }
  
      // wait for user to acknowledge
      Console.WriteLine("Press Enter to terminate...");
      Console.Read();
    }
  }
}

