using System;

namespace ExceptionHandling
{
	class ThrowException
	{
		static void Main()
		{			
			try
			{
				double dCurrentBalance = 10.00;
				double dRequestWithdrawl = 100.00;

				Console.WriteLine("The bank is going to attempt to withdraw " +
					dRequestWithdrawl + " dollars from an account with " +
					dCurrentBalance + " dollars in the bank.");
				Console.WriteLine();

				if (dRequestWithdrawl > dCurrentBalance)
				{
					throw new Exception("The requested withdrawl is more than the funds in the account.");
				}
			}
			catch (Exception e)
			{
				Console.WriteLine ("The following exception occurred:  \r\n {0}", e);
			}	
		}
	}
}
