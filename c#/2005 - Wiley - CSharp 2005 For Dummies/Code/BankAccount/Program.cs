// BankAccount - create a bank account using a double variable 
//               to store the account balance; (keep the balance 
//               in a private variable to hide its implementation
//               from the outside world)
// Note: Until you correct it, this program fails to compile 
// because Main() refers to a private member of class BankAccount.
using System;

namespace BankAccount
{
  public class Program
  {
    public static void Main(string[] args)
    {
      Console.WriteLine("This program doesn't compile in its present state.");
      // open a bank account
      Console.WriteLine("Create a bank account object");
      BankAccount ba = new BankAccount();
      ba.InitBankAccount();
      
      // accessing the balance via the Deposit() method is OK - 
      // Deposit() has access to all of the data members
      ba.Deposit(10);
  
      // accessing the data member directly is a compile
      // time error
      Console.WriteLine("Just in case you get this far"
                      + "\nThe following is supposed to "
                      + "generate a compile error");
      ba.dBalance += 10;


      // wait for user to acknowledge the results
      Console.WriteLine("Press Enter to terminate...");
      Console.Read();
    }
  }
    
  // BankAccount - define a class that represents a simple account
  public class BankAccount
  {
    private static int nNextAccountNumber = 1000;
    private int nAccountNumber;
    
    // maintain the balance as a single double variable
    private double dBalance;

    // Init - initialize a bank account with the next
    //        account id and a balance of 0
    public void InitBankAccount()
    {
      nAccountNumber = ++nNextAccountNumber;
      dBalance = 0.0;
    }

    // GetBalance - return the current balance
    public double GetBalance()
    {
      return dBalance;
    }
    
    // AccountNumber
    public int GetAccountNumber()
    {
      return nAccountNumber;
    }
    public void SetAccountNumber(int nAccountNumber)
    {
      this.nAccountNumber = nAccountNumber;
    }

    // Deposit - any positive deposit is allowed
    public void Deposit(double dAmount)
    {
      if (dAmount > 0.0)
      {
        dBalance += dAmount;
      }
    }

    // Withdraw - you can withdraw any amount up to the
    //            balance; return the amount withdrawn
    public double Withdraw(double dWithdrawal)
    {
      if (dBalance <= dWithdrawal)
      {
        dWithdrawal = dBalance;
      }

      dBalance -= dWithdrawal;
      return dWithdrawal;
    }

    // GetString - return the account data as a string
    public string GetString()
    {
      string s = String.Format("#{0} = {1:C}",
                               GetAccountNumber(),
                               GetBalance());
      return s;
    }
  }
}
