// MyTestClass - contains test methods for NUnit to
//               run; these test our program's classes
//               and methods
using System;
using NUnit.Framework;    // need this directive
namespace NUnitTestExample
{
  [TestFixture] // NUnit term for a test class
  public class MyTestClass
  {
    // put any data needed by most or all tests here
    // set up the test data
    // e.g. inputs common to most tests below
    private string[] stringsIn;  // defined in SetUp method
    // actual outputs for those inputs when we call the method
    // that we're testing
    private string[] stringsOut; // defined in SetUp method
    // expected outputs - we test by comparing actual results
    // against these
    private string[] stringsExpected;  // defined in SetUp

    // constructor (if needed)
    public MyTestClass() {  }

    // Setup method - called before each test method
    //                used to give tests identical starting point
    //                so no test affects results of later tests
    [SetUp]  // SetUp attribute on this one (note spelling)
    public void MySetup()
    {
      // for the TrimAndPad() tests, we know what strings we will
      // throw at the method to test it, and we know exactly 
      // what output we expect for each one; so we can set those
      // arrays up in advance, and this is a pretty good place
      // to do that (constructor or data member initializations
      // would also work in this case); for some programs, you 
      // might open a database or Internet connection here, for example

      // create the input array
      stringsIn = new string[3];
      stringsIn[0] = "Joe   ";
      stringsIn[1] = "Rumpelstiltskin";
      stringsIn[2] = "  Vanderbilt";

      // create the expected results array
      stringsExpected = new string[3];
      stringsExpected[0] = "Joe             "; // with padding
      stringsExpected[1] = "Rumpelstiltskin ";
      stringsExpected[2] = "Vanderbilt      ";

      // we could even make the call to TrimAndPad() here,
      // but we'll call it fresh in each test instead
    }

    // write test methods here
    // StringLengthsGoodTest - tests normal input (as opposed to
    //                         faulty input)
    // test methods have the "attribute" [Test]
    [Test]
    public void StringLengthsGoodTest()  // always public void
    {
      Console.WriteLine("Test 1. Normal input - string lengths");
      Console.WriteLine("StringLengthsGoodTest: ");
      // Test 1. Normal input
      // check for expected string lengths
      // call the method we're testing
      // stringsOut contains the actual results of the call
      // (stringsOut is defined as class data member)
      stringsOut = Program.TrimAndPad(stringsIn);
      // check each string against expected result length 16
      // NUnit.Framework provides Assert class with IsTrue method
      // first param is a boolean condition
      // second param is a message to show in NUnit if test fails
      Assert.IsTrue(stringsOut[0].Length == 16, "string0 bad length");
      Assert.IsTrue(stringsOut[1].Length == 16, "string1 bad length");
      Assert.IsTrue(stringsOut[2].Length == 16, "string2 bad length");
    }

    // CountSpacesTest - check each string for expected amount
    //                   of blank "padding" at end
    [Test]
    public void CountSpacesTest() 
    {
      Console.WriteLine("Test 2. Normal input - space count");
      Console.WriteLine("CountSpacesTest: ");
      // how many spaces each string should end up with
      int[] spacesExpected = new int[] { 13, 1, 6 };
      // call the method we're testing
      stringsOut = Program.TrimAndPad(stringsIn);
      // results of testing each string against expected spaces
      // stringsIn defined as class data member
      bool[] bSpaces = new bool[stringsIn.Length];
      // count spaces per string and test against expectations
      for(int i = 0; i < stringsOut.Length; i++)
      {
        int nSpaces = CountSpaces(stringsOut[i]);
        // testing assertion for each item in stringsOut
        Assert.IsTrue(nSpaces == spacesExpected[i], 
          "string " + i + " has bad space count");
      }
    }
    // support method for Normal-case tests - not a test
      private int CountSpaces(string s)
    {
      int nCount = 0;
      for(int i = 0; i < s.Length; i++)
      {
        if(s[i] == ' ')
        {
          nCount++;
        }
      }
      return nCount;
    }

    // NullInputTest - what if we pass an unitialized array
    //                      to TrimAndPad()?
    // this test has two attributes
    // ExpectedException allows test for exception
    [Test, ExpectedException(typeof(NullReferenceException))]
    public void NullInputTest()
    {
      Console.WriteLine("Test 3: Null input");
      Console.WriteLine("NullInputTest: ");
      // throws a NullReferenceException
      // the logic of this test is reversed:
      // pass only if the expected exception IS thrown
      // (that's the TrimAndPad() behavior we want)
      // force exception to occur by passing null for the array
      // use a locally-defined output array instead of the
      // stringsOut class data member - to isolate this test
      // from the others, since it fails to fill stringsOut
      string[] stringsOut = Program.TrimAndPad(null);
      // get this result if no exception (test fails):
      Assert.Fail("Test 2, null input exception");
      // note another member of Assert class
    }

    // FailingTest - a test that will fail, to show you what
    //               that looks like in NUnit
    [Test]
    public void FailingTest()
    {
      Console.WriteLine("Test 4. Bad result - deliberate failure");
      Console.WriteLine("FailingTest: ");
      // this test deliberately fails: we fake results that
      // haven't changed from the inputs, as if TrimAndPad()
      // didn't do its job
      stringsOut[0] = "Joe   ";
      stringsOut[1] = "Rumpelstiltskin";
      stringsOut[1] = "  Vanderbilt";
      Assert.IsTrue(stringsOut[0].Length == 16, "string0 bad length");
      Assert.IsTrue(stringsOut[1].Length == 16, "string1 bad length");
      Assert.IsTrue(stringsOut[2].Length == 16, "string2 bad length");
    }
    // other tests...
  } 
}
