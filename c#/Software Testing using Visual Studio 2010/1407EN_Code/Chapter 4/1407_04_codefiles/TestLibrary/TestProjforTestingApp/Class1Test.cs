using TestLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Diagnostics;

namespace TestProjforTestingApp
{
    
    
    /// <summary>
    ///This is a test class for Class1Test and is intended
    ///to contain all Class1Test Unit Tests
    ///</summary>
    [TestClass()]
    public class Class1Test
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for CalculateTotalPrice
        ///</summary>
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\Data.csv", "Data#csv", DataAccessMethod.Sequential), DeploymentItem("TestProjforTestingApp\\Data.csv"), TestMethod()]
        public void CalculateTotalPriceTest()
        {
            Class1 target = new Class1(); 
            double uPrice = 0F; 
            int Qty = 0; 
            double expected = 0F; 
            double actual;
            expected = Convert.ToDouble(testContextInstance.DataRow["ExpectedTotalPrice"]);
            actual = target.CalculateTotalPrice(Convert.ToDouble(testContextInstance.DataRow["UnitPrice"]), Convert.ToInt32(testContextInstance.DataRow["Quantity"]));
            Assert.AreEqual(expected, actual, "The expected value is {0} but the actual value is {1}", expected, actual);
            Trace.WriteLine("Expected:" + expected + "; Actual:"+ actual);

        }

        /// <summary>
        ///A test for GetTotalPrice
        ///</summary>
        [TestMethod()]
        public void GetTotalPriceTest()
        {
            Class1 target = new Class1(); // TODO: Initialize to an appropriate value
            target.GetTotalPrice();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for MethodforInternalExample
        ///</summary>
        [TestMethod()]
        public void MethodforInternalExampleTest()
        {
            string str = string.Empty; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = Class1.MethodforInternalExample(str);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetObjectToCompare
        /// Few Assert methods are presented here for sample. Additional Assert methods are explained in the book.  
        ///</summary>
        [TestMethod()]
        public void GetObjectToCompareTest()
        {
            Class1 target = new Class1(); // TODO: Initialize to an appropriate value
            Item expected = new Item();
            expected.ItemID = 100;
            expected.ItemType = "Electronics";
            expected.ItemPrice = 10.39;
            Item actual;
            actual = target.GetObjectToCompare();
            //Assert.AreEqual(expected, actual, "Objects are Not Equal");
            //Assert.AreEqual(expected, actual, "Objects {0} and {1} are not equal", "ObjA", "ObjB");
            //Assert.Inconclusive("Verify the correctness of this test method.");
            Assert.AreEqual(expected.ItemPrice, actual.ItemPrice, 0.5, "The values {0} and {1} does not match within the accuracy", expected.ItemPrice, actual.ItemPrice);
        }

        /// <summary>
        ///A test for SampleTestmethodforAsserts
        /// Few Assert methods are presented here for sample. Additional Assert methods are explained in the book.  
        ///</summary>
        [TestMethod()]
        public void SampleTestmethodforAssertsTest()
        {
            ArrayList firstArray = new ArrayList(3);
            firstArray.Add("FirstName");
            firstArray.Add("LastName");

            ArrayList secondArray = new ArrayList(3);
            secondArray = firstArray;
            secondArray.Add("MiddleName");

            ArrayList thirdArray = new ArrayList(3);
            thirdArray.Add("FirstName");
            thirdArray.Add("MiddleName");
            thirdArray.Add("LastName");

            ArrayList fourthArray = new ArrayList(3);
            fourthArray.Add("FirstName");
            fourthArray.Add("MiddleName");
            
            //Class1 target = new Class1(); // TODO: Initialize to an appropriate value
            //target.SampleTestmethodforAsserts();
            //Assert.Inconclusive("A method that does not return a value cannot be verified.");
            firstArray.Add(null);
            //CollectionAssert.AllItemsAreNotNull(firstArray);
            thirdArray.Remove("MiddleName");
            thirdArray.Add("FullName");
            //CollectionAssert.AreNotEquivalent(thirdArray, secondArray);
            //CollectionAssert.AllItemsAreInstancesOfType(thirdArray, typeof(string));
            //CollectionAssert.IsSubsetOf(fourthArray, thirdArray);


            ArrayList EmployeesListOne = new ArrayList();
            EmployeesListOne.Add(new TestLibrary.Employee("Richard",
                                 "King", 1801));
            EmployeesListOne.Add(new TestLibrary.Employee("James",
                                 "Miller", 1408));
            EmployeesListOne.Add(new TestLibrary.Employee("Jim",
                                 "Tucker", 3234));
            EmployeesListOne.Add(new TestLibrary.Employee("Murphy",
                                 "Young", 3954));
            EmployeesListOne.Add(new TestLibrary.Employee("Shelly",
                                 "Watts", 7845));

            ArrayList EmployeesListTwo = new ArrayList();
            EmployeesListTwo.Add(new TestLibrary.Employee("Richard",
                                 "King", 1801));
            EmployeesListTwo.Add(new TestLibrary.Employee("James",
                                 "Miller", 1408));
            EmployeesListTwo.Add(new TestLibrary.Employee("Jim",
                                 "Tucker", 3234));
            EmployeesListTwo.Add(new TestLibrary.Employee("Murphy",
                                 "Young", 3954));
            EmployeesListTwo.Add(new TestLibrary.Employee("Shelly",
                                 "Watts", 7845));
            TestLibrary.Employee.EmployeeComparer comparer = new  TestLibrary.Employee.EmployeeComparer();
            CollectionAssert.AreEqual(EmployeesListOne, EmployeesListTwo, comparer, "The collections '{0}' and '{1}' are not equal", "EmployeesListOne", "EmployeesListTwo");

            try
            {
                CollectionAssert.Contains(fourthArray, "Phone Number");
            }
            catch (AssertFailedException e)
            {
                Trace.WriteLine(e.Message);
                Trace.WriteLine("THe fourth array list does not contain the string 'Phone Number' ");
                //throw;
            }

        }
     }
}
