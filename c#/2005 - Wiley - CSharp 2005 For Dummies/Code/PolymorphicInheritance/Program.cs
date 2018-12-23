// PolymorphicInheritance - hide a method in the
//                     base class polymorphically
using System;

namespace PolymorphicInheritance
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
    
    public virtual decimal Withdraw(decimal mAmount)
    {
      Console.WriteLine("In BankAccount.Withdraw() for ${0}...", mAmount);
                          
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
    override public decimal Withdraw(decimal mWithdrawal)
    {
      Console.WriteLine("In SavingsAccount.Withdraw()...");
      Console.WriteLine("Invoking base-class Withdraw twice...");
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

      // display the resulting balance
      Console.WriteLine("Withdrawal: MakeAWithdrawal(ba, ...)");
      ba = new BankAccount(200M);
      MakeAWithdrawal(ba, 100M);
      Console.WriteLine("BankAccount balance is {0:C}", ba.Balance);

      Console.WriteLine("Withdrawal: MakeAWithdrawal(sa, ...)");
      sa = new SavingsAccount(200M, 12);
      MakeAWithdrawal(sa, 100M);
      Console.WriteLine("SavingsAccount balance is {0:C}", sa.Balance);

      // wait for user to acknowledge the results
      Console.WriteLine("Press Enter to terminate...");
      Console.Read();
    }
  }
}

