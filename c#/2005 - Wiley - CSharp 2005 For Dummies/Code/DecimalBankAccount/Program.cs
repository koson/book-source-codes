// DecimalBankAccount - create a bank account using a decimal
//                     variable to store the account balance;
using System;

namespace DecimalBankAccount
{
  public class Program
  {
    public static void Main(string[] args)
    {
      // open a bank account
      Console.WriteLine("Create a bank account object");
      BankAccount ba = new BankAccount();
      ba.InitBankAccount();
      
      // make a deposit
      double dDeposit = 123.454;
      Console.WriteLine("Depositing {0:C}", dDeposit);
      ba.Deposit(dDeposit);
      
      // account balance
      Console.WriteLine("Account = {0}", ba.GetString());
                        
      // now add in a very small amount
      double dAddition = 0.002;
      Console.WriteLine("Adding {0:C}", dAddition);
      ba.Deposit(dAddition);
      
      // resulting balance
      Console.WriteLine("Resulting account = {0}", ba.GetString());

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
    
    // maintain the balance as a single decimal variable
    private decimal mBalance;

    // Init - initialize a bank account with the next
    //        account id and a balance of 0
    public void InitBankAccount()
    {
      nAccountNumber = ++nNextAccountNumber;
      mBalance = 0;
    }

    // GetBalance - return the current balance
    public double GetBalance()
    {
      return (double)mBalance;
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
        // round off the double to the nearest cent before depositing
        decimal mTemp = (decimal)dAmount;
        mTemp = Decimal.Round(mTemp, 2);

        mBalance += mTemp;
      }

    }

    // Withdraw - you can withdraw any amount up to the
    //            balance; return the amount withdrawn
    public decimal Withdraw(decimal dWithdrawal)
    {
      if (mBalance <= dWithdrawal)
      {
        dWithdrawal = mBalance;
      }

      mBalance -= dWithdrawal;
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
