using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;


namespace EmployeeTestProject
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest]
    public class CodedUITest1
    {
        public CodedUITest1()
        {
        }

        [DeploymentItem("EmployeeTestProject\\EmpData.csv"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\EmpData.csv", "EmpData#csv", DataAccessMethod.Sequential), TestMethod]
        public void CodedUITestMethod1()
        {

            // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
            // For more information on generated code, see http://go.microsoft.com/fwlink/?LinkId=179463
            this.UIMap.FromtheListpageclickonEmployees();
            this.UIMap.InsertEmployeeoption();
            this.UIMap.ClickonInsertnewemployeeoption();
            this.UIMap.EnterallthedetailsfortheemployeeFirst_NameLast_NameMiddle_NameDepartmentOccupationGenderCityStateCountryPhoneParams.UIFirst_NameEditText = TestContext.DataRow["First_Name"].ToString();
            this.UIMap.EnterallthedetailsfortheemployeeFirst_NameLast_NameMiddle_NameDepartmentOccupationGenderCityStateCountryPhoneParams.UILast_NameEditText = TestContext.DataRow["Last_Name"].ToString();
            this.UIMap.EnterallthedetailsfortheemployeeFirst_NameLast_NameMiddle_NameDepartmentOccupationGenderCityStateCountryPhoneParams.UIMiddle_NameEditText = TestContext.DataRow["Middle_Name"].ToString();
            this.UIMap.EnterallthedetailsfortheemployeeFirst_NameLast_NameMiddle_NameDepartmentOccupationGenderCityStateCountryPhoneParams.UIDepartmentEditText = TestContext.DataRow["Department"].ToString();
            this.UIMap.EnterallthedetailsfortheemployeeFirst_NameLast_NameMiddle_NameDepartmentOccupationGenderCityStateCountryPhoneParams.UIOccupationEditText = TestContext.DataRow["Occupation"].ToString();
            this.UIMap.EnterallthedetailsfortheemployeeFirst_NameLast_NameMiddle_NameDepartmentOccupationGenderCityStateCountryPhoneParams.UIGenderEditText = TestContext.DataRow["Gender"].ToString();
            this.UIMap.EnterallthedetailsfortheemployeeFirst_NameLast_NameMiddle_NameDepartmentOccupationGenderCityStateCountryPhoneParams.UICityEditText = TestContext.DataRow["City"].ToString();
            this.UIMap.EnterallthedetailsfortheemployeeFirst_NameLast_NameMiddle_NameDepartmentOccupationGenderCityStateCountryPhoneParams.UIStateEditText = TestContext.DataRow["State"].ToString();
            this.UIMap.EnterallthedetailsfortheemployeeFirst_NameLast_NameMiddle_NameDepartmentOccupationGenderCityStateCountryPhoneParams.UICountryEditText = TestContext.DataRow["Country"].ToString();
            this.UIMap.EnterallthedetailsfortheemployeeFirst_NameLast_NameMiddle_NameDepartmentOccupationGenderCityStateCountryPhoneParams.UIPhoneEditText = TestContext.DataRow["Phone"].ToString();
            this.UIMap.EnterallthedetailsfortheemployeeFirst_NameLast_NameMiddle_NameDepartmentOccupationGenderCityStateCountryPhone();
            this.UIMap.AssertMethod1();
           // this.UIMap.AssertMethod2();
            this.UIMap.Afterenteringallthedetailssaveemployeedetail();

        }

        #region Additional test attributes

        // You can use the following additional attributes as you write your tests:

        ////Use TestInitialize to run code before running each test 
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{        
        //    // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        //    // For more information on generated code, see http://go.microsoft.com/fwlink/?LinkId=179463
        //}

        ////Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{        
        //    // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        //    // For more information on generated code, see http://go.microsoft.com/fwlink/?LinkId=179463
        //}

        #endregion

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
        private TestContext testContextInstance;

        public UIMap UIMap
        {
            get
            {
                if ((this.map == null))
                {
                    this.map = new UIMap();
                }

                return this.map;
            }
        }

        private UIMap map;
    }
}
