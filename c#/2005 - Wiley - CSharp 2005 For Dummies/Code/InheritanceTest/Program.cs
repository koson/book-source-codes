// InheritanceTest - examine the way that the virtual keyword can 
//                   be used to start a new inheritance ladder
namespace InheritanceTest
{
  using System;

  public class Program
  {
    public static void Main(string[] strings)
    {
      Console.WriteLine("\nPassing a BankAccount");
      BankAccount ba = new BankAccount();
      Test1(ba);
      
      Console.WriteLine("\nPassing a SavingsAccount");
      SavingsAccount sa = new SavingsAccount();
      Test1(sa);
      Test2(sa);
      
      Console.WriteLine("\nPassing a SpecialSaleAccount");
      SpecialSaleAccount ssa = new SpecialSaleAccount();
      Test1(ssa);
      Test2(ssa);
      Test3(ssa);
      
      Console.WriteLine("\nPassing a SaleSpecialCustomer");
      SaleSpecialCustomer ssc = new SaleSpecialCustomer();
      Test1(ssc);
      Test2(ssc);
      Test3(ssc);
      Test4(ssc);
      
      // wait for user to acknowledge
      Console.WriteLine();
      Console.WriteLine("Press Enter to terminate...");
      Console.Read();
    }
    
    public static void Test1(BankAccount account)
    {
      Console.WriteLine("\tto Test(BankAccount)");
      account.Withdraw(100);
    }
    
    public static void Test2(SavingsAccount account)
    {
      Console.WriteLine("\tto Test(SavingsAccount)");
      account.Withdraw(100);
    }
    
    public static void Test3(SpecialSaleAccount account)
    {
      Console.WriteLine("\tto Test(SpecialSaleAccount)");
      account.Withdraw(100);
    }
    
    public static void Test4(SaleSpecialCustomer account)
    {
      Console.WriteLine("\tto Test(SaleSpecialCustomer)");
      account.Withdraw(100);
    }
  }

  // BankAccount - simulate a bank account each of which
  //               carries an account id (which is assigned
  //               upon creation) and a balance
  public class BankAccount
  {
    // Withdrawal - you can withdraw any amount up to the
    //              balance; return the amount withdrawn
    virtual public void Withdraw(decimal dWithdraw)
    {
      Console.WriteLine("\t\tcalls BankAccount.Withdraw()");
    }

  }

  // SavingsAccount - a bank account that draws interest
  public class SavingsAccount : BankAccount
  {
    override public void Withdraw(decimal mWithdrawal)
    {
      Console.WriteLine("\t\tcalls SavingsAccount.Withdraw()");
    }
  }
  
  // SpecialSaleAccount - account used only during a sale
  public class SpecialSaleAccount : SavingsAccount
  {
    new virtual public void Withdraw(decimal mWithdrawal)
    {
      Console.WriteLine("\t\tcalls SpecialSaleAccount.Withdraw()");
    }
  }
  
  // SaleSpecialCustomer - account used for special customers
  //                       during the sale period
  public class SaleSpecialCustomer : SpecialSaleAccount
  {
    override public void Withdraw(decimal mWithdrawal)
    {
      Console.WriteLine("\t\tcalls SaleSpecialCustomer.Withdraw()");
    }
  }
}

