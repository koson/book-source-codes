using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;

namespace TestLibrary
{
    public class Class1
    {

        //This method is modified to have two paramters to have data driven tests with multiple data   
        public double CalculateTotalPrice(double untPrice, double quantity)
        {
            double totalPrice;
            double tax = 0.12;
            totalPrice = untPrice * quantity + (untPrice * tax * quantity);  
            return totalPrice;
        }

        //This method is modfied from the original sample present in the book. Modified for Data Driven test sample.
        public void GetTotalPrice()
        {
            double unitPrice = 10.5;
            int qty = 5;
            double totalPrice = CalculateTotalPrice(unitPrice, qty);
            Console.WriteLine("Total Price: " + totalPrice);
        }

        // Sample method for generating unit test for Internal method 
        internal static bool MethodforInternalExample(string str)
        {
            bool result = false;
            if (str == "return true") result = true;
            if (str == "return false") result = false;
            return result;
        }
        public Item GetObjectToCompare()
        {
            Item objA = new Item();
            objA.ItemID = 100;
            objA.ItemType = "Electronics";
            objA.ItemPrice = 10.99;
            return objA;
        }

        public void SampleTestmethodforAsserts()
        { 
        
        }
    }

    public class Item
    {
        public int ItemID { get; set; }
        public string ItemType { get; set; }
        public double ItemPrice { get; set; }
    }

    public class Employee : IComparable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int ID { get; set; }

        public Employee(string firstName, string lastName,
                         int employeeID)
        {
            FirstName = firstName;
            LastName = lastName;
            ID = employeeID;
        }

        public int CompareTo(Object obj)
        {
            Employee emp = (Employee)obj;
            return FirstName.CompareTo(emp.FirstName);
        }

        public class EmployeeComparer : IComparer
        {
            public int Compare(Object one, Object two)
            {
                Employee emp1 = (Employee)one;
                Employee emp2 = (Employee)two;
                return emp1.CompareTo(emp2);
            }
        }
    }
}

