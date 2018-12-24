// BUDGET5 - Identical to other Budget programs except
//           for the use of an STL list container to
//           hold budget objects (rather than a fixed
//           array or a home made linked list)

#include <cstdio>
#include <cstdlib>
#include <iostream>
#include <list>
using namespace std;

/** \class Account
 *  \brief an abstract bank account.
 *  \details This abstract class incorporates
 *         propertiescommon to both account types:
 *         Checking and Savings. However, it's missing the
 *         concept withdrawal(), which is different
 *         between the two
 */
class Account
{
  public:
    /** \brief Account constructor
     *
     * \param accNo - the account number to use
     */
    Account(unsigned accNo)
    {
      // initialize the data members of the object
      accountNumber = accNo;
      balance = 0;
      count++;
    }

    // access functions
    /** \brief accountNo - return the account number
     * \return int
     */
    int accountNo() { return accountNumber; }
    /** \brief acntBalance - return the account balance
     * \return double
     */
    double acntBalance() { return balance; }
    /** \brief noAccounts - return the number of accounts
     * \return int
     */
    static int noAccounts() { return count; }

    // transaction functions
    /** \brief deposit - deposit an amount to the account
     */
    void deposit(double amount) { balance += amount; }

    /** \brief withdrawal - withdraw an amount from the
     *                      account
     *
     * The function first checks to make sure that
     * sufficient funds are available. If not, the
     * function outputs an error message and returns a
     * false.
     * \return true if the function executed properly
     */
    virtual bool withdrawal(double amount)
    {
        if (balance < amount )
        {
             cout << "Insufficient funds: balance " << balance
                  << ", withdrawal "                << amount
                  << endl;
              return false;
        }
        balance -= amount;
        return true;
    }

    // display function for displaying self on 'cout'
    /** \brief display - display the current object on
     *         the standard out
     */
    void display()
    {
        cout << type()
             << " account " << accountNumber
             << " = "   << balance
             << endl;
    }
    /** \brief type - return an indication of what type
     *         of Account the current object is
     *
     * \return a string containing the name of the class
     *
     */
     virtual const char* type() { return "Account"; }

  protected:
    static int count;/**< the number of account objects*/
    unsigned accountNumber;/**< the number of the current*/
                           /**< account */
    double   balance;/**< the balance in current account*/
};

// allocate space for statics
int Account::count = 0;

/** \class Checking
 * \brief the unique properties of a checking account.
 * \details The majority of the properties are in the base
 * class Account. This one of the two subclasses of
 * Account. Savings implements the details of a savings
 * account at our mythical bank.
 */
class Checking : public Account
{
  public:
    Checking(unsigned accNo) :
      Account(accNo)
    { }

    // overload pure virtual functions
    virtual bool withdrawal(double amount);
    virtual const char* type() { return "Checking"; }
};

/** \brief withdrawal - a withdrawal from checking account
 * \details A checking account differs from a standard
 * account in that it charges 20 cents per withdrawal if
 * the balance falls below $500.
 *
 * \param amount amount to withdraw
 * \return bool true if the withdrawal works
 */
bool Checking::withdrawal(double amount)
{
    bool success = Account::withdrawal(amount);

    // if balance falls too low, charge service fee
    if (success && balance < 500.00)
    {
        balance -= 0.20;
    }
    return success;
}

/** \class Savings
 * \brief a Savings account
 *
 * \details This one of the two subclasses of Account.
 * Savingsimplements the details of a savings account at
 * our mythical bank.
 */
class Savings : public Account
{
  public:
    /** \brief constructor
     *
     * \param accNo account number
     */
    Savings(unsigned accNo) : Account(accNo)
    { noWithdrawals = 0; }

    // transaction functions
    virtual bool withdrawal(double amount);
    virtual const char* type() { return "Savings"; }

  protected:
    int noWithdrawals;   /**< the number of withdrawals */
};

/** \brief overload the Account::withdrawal() member
 *  \details function to charge a $5.00 fee after the
 *           first withdrawals of the month
 *
 * \param amount double the amount to withdraw
 * \return bool true if the withdrawal worked
 */
bool Savings::withdrawal(double amount)
{
    if (++noWithdrawals > 1)
    {
        balance -= 5.00;
    }
    return Account::withdrawal(amount);
}

/**
 * \brief pointer to an Account
 */
typedef Account* AccountPtr;

// prototype declarations
unsigned getAccntNo();
void     process(AccountPtr pAccount);
void     getAccounts(list<AccountPtr>& accList);
void     displayResults(list<AccountPtr>& accList);

int main(int argcs, char* pArgs[])
{
    // create a link list to attach accounts to
    list<AccountPtr> listAccounts;

    // read accounts from user
    getAccounts(listAccounts);

    // display the linked list of accounts
    displayResults(listAccounts);

    // wait until user is ready before terminating program
    // to allow the user to see the program results
    cout << "Press Enter to continue..." << endl;
    cin.ignore(10, '\n');
    cin.get();
    return 0;
}

/** \brief getAccounts - inputs accounts from the keyboard
 * \details This function reads input from the keyboard.
 * For every S or C entered, the function creates a new
 * Savings or Checking account object and adds it to the
 * account list. An X terminates the entry. Any other
 * input is assumed to be a deposit (numbers greater than
 * 0) or a withdrawal (numbers less than 0).
 *
 * \param accList list<AccountPtr>& the list of account
 *                objects created by getAccounts()
 * \return void
 */
void getAccounts(list<AccountPtr>& accList)
{
    AccountPtr pA;

    // loop until someone enters 'X' or 'x'
    char   accountType;     // S or C
    while (true)
    {
        cout << "Enter S for Savings, "
             << "C for Checking, X for exit:";
        cin >> accountType;
        pA = nullptr;
        switch (accountType)
        {
          case 'c':
          case 'C':
            pA = new Checking(getAccntNo());
            break;

          case 's':
          case 'S':
            pA = new Savings(getAccntNo());
            break;

          case 'x':
          case 'X':
            return;

          default:
            cout << "I didn't get that.\n";
        }

        // now process the object we just created
        if (pA)
        {
            accList.push_back(pA);
            process(pA);
        }
    }
}

/** \brief displayResults - display the accounts found
 *             in the Account link list
 *
 * \param accntList list<AccountPtr>& the list of account
 * \return void
 */
void displayResults(list<AccountPtr>& accntList)
{
    // now present totals
    double total = 0.0;
    cout << "\nAccount totals:\n";

    // create an iterator and iterate through the list
    // (the type of iter is list<AccountPtr>::iterator)
    for (list<AccountPtr>::iterator iter = accntList.begin();
         iter != accntList.end();
         iter++)
    {
        AccountPtr pAccount = *iter;
        pAccount->display();
        total += pAccount->acntBalance();
    }
    cout << "Total worth = " << total << endl;
}

//
/** \brief getAccntNo - return the account number to
 *             create account
 * \return unsigned the account number entered by the user
 */
unsigned getAccntNo()
{
    unsigned accntNo;
    cout << "Enter account number:";
    cin  >> accntNo;
    return accntNo;
}

/** \brief process(Account) - input the data for an account
 *
 * \param pAccount AccountPtr an account to process
 * \return void
 */
void process(AccountPtr pAccount)
{
    cout << "Enter positive number for deposit,\n"
         << "negative for withdrawal, 0 to terminate\n";
    double transaction;
    while(true)
    {
        cout << ":";
        cin >> transaction;
        if (transaction == 0)
        {
            break;
        }

        // deposit
        if (transaction > 0)
        {
            pAccount->deposit(transaction);
        }
        // withdrawal
        if (transaction < 0)
        {
            pAccount->withdrawal(-transaction);
        }
    }
}
