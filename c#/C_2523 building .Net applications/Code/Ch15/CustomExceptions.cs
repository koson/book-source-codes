using System;

namespace CustomExceptions
{
	public class AccountOverdrawn : System.Exception
	{
		// Standard Constructors for an Exception.
		public AccountOverdrawn(){}

		public AccountOverdrawn(string message)
			: base(message){}

		public AccountOverdrawn(string message, Exception innerEx)
			: base(message, innerEx){}
	}
}
