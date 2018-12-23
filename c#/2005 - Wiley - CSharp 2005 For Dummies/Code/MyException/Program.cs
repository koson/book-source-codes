// MyException - demonstrate how a new exception class can be
//							 created and demonstrate how functions can catch 
//							 just	the requirements that they're prepared to	handle
using System;

namespace MyException
{
  // introduce some type of 'MyClass'
  public class MyClass{}

  // MyException -  @md add a reference to MyClass to the standard
  //									 application exception class
  public class MyException : ApplicationException
  {
		private	MyClass myobject;
  
		public MyException(string	sMsg, MyClass mo) : base(sMsg)
	  {
			myobject = mo;
	  }
  
		// give	outside classes access to an informative class
		public MyClass MyCustomObject{ get {return myobject;}}
  }
  
  public class Program
  {
		// f1	-	 @md catch generic exception object
		public void	f1(bool bExceptionType)
	  {
			try
		  {
				f2(bExceptionType);
		  }
			catch(Exception	e)
		  {
				Console.WriteLine("Caught a generic exception in f1()");
				Console.WriteLine(e.Message);
		  }
	  }
  
		// f2	-	 @md be prepared to catch a MyException
		public void	f2(bool bExceptionType)
	  {
			try
		  {
				f3(bExceptionType);
		  }
			catch(MyException	me)
		  {
				Console.WriteLine("Caught a MyException in f2()");
				Console.WriteLine(me.Message);
		  }
	  }
  
		// f3	-	 @md don't bother to catch any error exceptions
		public void	f3(bool bExceptionType)
	  {
			f4(bExceptionType);
	  }
  
		// f4	-	 @md throw one of two types of exceptions
		public void	f4(bool bExceptionType)
	  {
			// we're working with	some local object
			MyClass	mc = new MyClass();

			if(bExceptionType)
		  {
				// error occurs	- throw the object with the exception
				throw	new MyException("MyException thrown in f4()", mc);
																	
		  }
			throw	new Exception("Generic Exception thrown in f4()");
	  }
  
		public static	void Main(string[] args)
	  {
			// throw a generic exception first
			Console.WriteLine("Throw a generic exception first");
			new	Program().f1(false);

			// now throw one of	my exceptions
			Console.WriteLine("\nThrow a specific exception first");
			new	Program().f1(true);
  
			// wait	for user to acknowledge
			Console.WriteLine("Press Enter to terminate...");
			Console.Read();
	  }
  }
}
