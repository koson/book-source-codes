// ConstructorSavingsAccount - implement a SavingsAccount as 
//                        a form of BankAccount; don't use any 
//                        virtual methods but do implement the
//                        constructors properly
using System;

namespace ConstructorSavingsAccount
{
  // BankAccount - simulate a bank account each of which
  //               carries an account ID (which is assigned
  //               upon creation) and a balance
  public class BankAccount
  {
    // bank accounts start at 1000 and increase sequentially from there
    public static int nNextAccountNumber = 1000;
  
    // maintain the account number and balance for each object
    public int nAccountNumber;
    public decimal mBalance;
  
    // Constructors
    public BankAccount():this(0)
    {
    }
    public BankAccount(decimal mInitialBalance)
    {
      nAccountNumber = ++nNextAccountNumber;
      mBalance = mInitialBalance;
    }

    // Balance
    public decimal Balance
    {
      get { return mBalance;}
    }
  
    // Deposit - any positive deposit is allowed
    public void Deposit(decimal mAmount)
    {
      if (mAmount > 0)
      {
        mBalance += mAmount;
      }
    }
  
    // Withdraw - you can withdraw any amount up to the
    //            balance; return the amount withdrawn
    public decimal Withdraw(decimal mWithdrawal)
    {
      if (Balance <= mWithdrawal)
      {
        mWithdrawal = Balance;
      }
  
      mBalance -= mWithdrawal;
      return mWithdrawal;
    }

    // ToString - stringify the account
    public string ToBankAccountString()
    {
      return String.Format("{0} - {1:C}", 
        nAccountNumber, Balance);
    }
  }

  // SavingsAccount - a bank account that draws interest
  public class SavingsAccount : BankAccount
  {
    public decimal mInterestRate;
  
    // InitSavingsAccount - input the rate expressed as a
    //                      rate between 0 and 100
    public SavingsAccount(decimal mInterestRate) : this(mInterestRate, 0)
    {
    }
    public SavingsAccount(decimal mInterestRate, decimal mInitial) : 
                                                    base(mInitial)
    {
      this.mInterestRate = mInterestRate / 100;
    }
  
    // AccumulateInterest - invoke once per period
    public void AccumulateInterest()
    {
      mBalance = Balance + (decimal)(Balance * mInterestRate);
    }

    // ToString - stringify the account
    public string ToSavingsAccountString()
    {
      return String.Format("{0} ({1}%)", 
        ToBankAccountString(), mInterestRate * 100);
    }
  }

  public class Program
  {
    // DirectDeposit - deposit my paycheck automatically
    public static void DirectDeposit(BankAccount ba, decimal mPay)
    {
      ba.Deposit(mPay);
    }
  
    public static void Main(string[] args)
    {
      // create a bank account and display it
      BankAccount ba = new BankAccount(100);
      DirectDeposit(ba, 100);
      Console.WriteLine("Account {0}", ba.ToBankAccountString());
  
      // now a savings account
      SavingsAccount sa = new SavingsAccount(12.5M);
      DirectDeposit(sa, 100);
      sa.AccumulateInterest();
      Console.WriteLine("Account {0}", sa.ToSavingsAccountString());
  
      // wait for user to acknowledge the results
      Console.WriteLine("Press Enter to terminate...");
      Console.Read();
    }
  }
}

