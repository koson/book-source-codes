// BankAccountContructorsAndFunction - provide our trusty bank account with a number of
//                    constructors, one for every occasion
using System;

namespace BankAccountContructorsAndFunction
{
  using System;

  public class Program
  {
    public static void Main(string[] args)
    {
      // create a bank account with valid initial values
      BankAccount ba1 = new BankAccount();
      Console.WriteLine(ba1.GetString());
      
      BankAccount ba2 = new BankAccount(100);
      Console.WriteLine(ba2.GetString());
      
      BankAccount ba3 = new BankAccount(1234, 200);
      Console.WriteLine(ba3.GetString());
                          
      // wait for user to acknowledge the results
      Console.WriteLine("Press Enter to terminate...");
      Console.Read();
    }
  }
  
  // BankAccount - simulate a simple bank account
  public class BankAccount
  {
    // bank accounts start at 1000 and increase sequentially from there
    static int nNextAccountNumber = 1000;
  
    // maintain the account number and balance
    int nAccountNumber;
    double dBalance;
  
    // place all the real initialization code in a separate, 
    // conventional function, called from constructors
    public BankAccount() // you create this one, not automatic
    {
      Init(++nNextAccountNumber, 0.0);
    }
    
    public BankAccount(double dInitialBalance)
    {
      Init(++nNextAccountNumber, dInitialBalance);
    }
    
    // the most specific constructor does all of the real work
    public BankAccount(int nInitialAccountNumber, double dInitialBalance)
    {
      // really should validate nInitialAccountNumber here
      // so it (a) doesn't duplicate existing numbers and
      // (b) is 1000 or greater
      Init(nInitialAccountNumber, dInitialBalance);
    }

    private void Init(int nInitialAccountNumber, double dInitialBalance)
    {
      nAccountNumber = nInitialAccountNumber;
      
      // start with an initial balance as long as it's positive
      if (dInitialBalance < 0)
      {
        dInitialBalance = 0;
      }
      dBalance = dInitialBalance;
    }
    
    public string GetString()
    {
      return String.Format("#{0} = {1:N}", nAccountNumber, dBalance);                     
    }
  }
}

