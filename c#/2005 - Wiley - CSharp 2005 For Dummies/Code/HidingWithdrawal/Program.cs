// HidingWithdrawal - hide the withdraw method in the
//                    base class with a method in the
//                    subclass of the same name
using System;

namespace HidingWithdrawal
{
  // BankAccount - a very basic bank account
  public class BankAccount
  {
    protected decimal mBalance;
  
    public BankAccount(decimal mInitialBalance)
    {
      mBalance = mInitialBalance;
    }
  
    public decimal Balance
    {
      get { return mBalance; }
    }
    
    public decimal Withdraw(decimal mAmount)
    {
      decimal mAmountToWithdraw = mAmount;
      if (mAmountToWithdraw > Balance)
      {
        mAmountToWithdraw = Balance;
      }
      mBalance -= mAmountToWithdraw;
      return mAmountToWithdraw;
    }
  }
  
  // SavingsAccount - a bank account that draws interest
  public class SavingsAccount : BankAccount
  {
    public decimal mInterestRate;
  
    // SavingsAccount - input the rate expressed as a
    //                  rate between 0 and 100
    public SavingsAccount(decimal mInitialBalance,
                          decimal mInterestRate)
    : base(mInitialBalance)
    {
      this.mInterestRate = mInterestRate / 100;
    }
  
    // AccumulateInterest - invoke once per period
    public void AccumulateInterest()
    {
      mBalance = Balance + (Balance * mInterestRate);
    }
  
    // Withdraw - you can withdraw any amount up to the
    //            balance; return the amount withdrawn
    public decimal Withdraw(decimal mWithdrawal)
    {
      // take our $1.50 off the top
      base.Withdraw(1.5M);

      // now you can withdraw from what's left
      return base.Withdraw(mWithdrawal);
    }
  }

  public class Program
  {
    public static void MakeAWithdrawal(BankAccount ba,
                                       decimal mAmount)
    {
      ba.Withdraw(mAmount);
    }
 
    public static void Main(string[] args)
    {
      BankAccount ba;
      SavingsAccount sa;

      // create a bank account, withdraw $100, and
      // display the results
      ba = new BankAccount(200M);
      ba.Withdraw(100M);

      // try the same trick with a savings account
      sa = new SavingsAccount(200M, 12);
      sa.Withdraw(100M);

      // display the resulting balance
      Console.WriteLine("When invoked directly:");
      Console.WriteLine("BankAccount balance is {0:C}",
                        ba.Balance);
      Console.WriteLine("SavingsAccount balance is {0:C}",
                        sa.Balance);

      // wait for user to acknowledge the results
      Console.WriteLine("Press Enter to terminate...");
      Console.Read();
    }
  }
}

