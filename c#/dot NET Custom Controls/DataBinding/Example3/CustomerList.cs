//------------------------------------------------------------------------------
//
//    This file contains 2 classes - Customer and CustomerList
//
//    Customer represents a single customer
//    CustomerList represents a collection of customers
//
//    CustomerList is a strongly typed List - that is it implements IList
//    and has accessors which are of type Customer
//
//    In order to enable design time support CustomerList also implements
//    IComponent - the implementation is delegated to an instance of Component
//
//------------------------------------------------------------------------------
namespace Akadia.SimpleBinding.Data 
{
	using System;
	using System.ComponentModel;
	using System.IO;
	using System.Collections;

	// CustomerList represents a collection of customers
	public class CustomerList : System.Collections.CollectionBase 
	{
		private Component _compImpl = new Component();

		public static CustomerList GetCustomers() 
		{
			CustomerList cl = new CustomerList();
			cl.Add(Customer.ReadCustomer1());
			cl.Add(Customer.ReadCustomer2());
			cl.Add(Customer.ReadCustomer3());
			cl.Add(Customer.ReadCustomer4());
			cl.Add(Customer.ReadCustomer5());
			return cl;
		}

		// Component implemenmtation so that we can design this puppy
		public virtual void Dispose() 
		{
			_compImpl.Dispose();
		}

		// Implement ILIST
		[
		Browsable(false),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)
		]
		public ISite Site  
		{
			get { return _compImpl.Site;}
			set { _compImpl.Site = value;}
		}

		public Customer this[int index] 
		{
			get {return (Customer)(List[index]);}
			set {List[index] = value;}
		}

		public int Add(Customer value) 
		{
			return List.Add(value);
		}

		public void Insert(int index, Customer value) 
		{
			List.Insert(index, value);
		}

		public int IndexOf(Customer value) 
		{
			return List.IndexOf(value);
		}

		public bool Contains(Customer value) 
		{
			return List.Contains(value);
		}

		public void Remove(Customer value) 
		{
			List.Remove(value);
		}

		public void CopyTo(Customer[] array, int index) 
		{
			List.CopyTo(array, index);
		}
	}

	// Customer represents a single customer
	public class Customer : Component 
	{

		private string _id;
		private string _title;
		private string _firstName;
		private string _lastName;
		private string _address;
		private DateTime _dateOfBirth;

		internal static Customer ReadCustomer1() 
		{
			Customer cust = new Customer("536-45-1245");
			cust.Title = "Mr.";
			cust.FirstName = "Otis";
			cust.LastName = "Redding";
			cust.DateOfBirth = DateTime.Parse("9/9/1941", System.Globalization.CultureInfo.CreateSpecificCulture("en-US").DateTimeFormat);
			cust.Address = "1 Dock Street,\r\nThe Bay,\r\nGeorgia,\r\nUSA";
			return cust;
		}

		internal static Customer ReadCustomer2() 
		{
			Customer cust = new Customer("246-12-5645");
			cust.Title = "Mr.";
			cust.FirstName = "James";
			cust.LastName = "Brown";
			// cust.DateOfBirth = DateTime.Parse("5/3/1933", System.Globalization.CultureInfo.CreateSpecificCulture("en-US").DateTimeFormat);
			cust.DateOfBirth = DateTime.Parse("8.3.1954");

			cust.Address = "45 New Bag Street,\r\nBarnwell,\r\nSouth Carolina,\r\nUSA";
			return cust;
		}

		internal static Customer ReadCustomer3() 
		{
			Customer cust = new Customer("651-27-8117");
			cust.Title = "Mz.";
			cust.FirstName = "Aretha";
			cust.LastName = "Franklin";
			cust.DateOfBirth = DateTime.Parse("3/25/1942", System.Globalization.CultureInfo.CreateSpecificCulture("en-US").DateTimeFormat);
			cust.Address = "21 Chain Ave.,\r\nMemphis,\r\nTennessee,\r\nUSA";
			return cust;
		}

		internal static Customer ReadCustomer4() 
		{
			Customer cust = new Customer("786-34-2114");
			cust.Title = "Mr.";
			cust.FirstName = "Louis";
			cust.LastName = "Armstrong";
			cust.DateOfBirth = DateTime.Parse("8/4/1901", System.Globalization.CultureInfo.CreateSpecificCulture("en-US").DateTimeFormat);
			cust.Address = "The West End,\r\nNew Orleans,\r\nLousiana,\r\nUSA";
			return cust;
		}

		internal static Customer ReadCustomer5() 
		{
			Customer cust = new Customer("445-34-4332");
			cust.Title = "Mz.";
			cust.FirstName = "Billie";
			cust.LastName = "Holiday";
			cust.DateOfBirth = DateTime.Parse("4/17/1915", System.Globalization.CultureInfo.CreateSpecificCulture("en-US").DateTimeFormat);
			cust.Address = "Southern Breeze Ave.,\r\nBaltimore,\r\nMaryland,\r\nUSA";
			return cust;
		}

		internal Customer(string ID): base() 
		{
			this._id = ID ;
		}

		public string ID 
		{
			get  
			{
				return _id ;
			}
		}

		public string FirstName 
		{
			get  
			{
				return _firstName ;
			}
			set 
			{
				_firstName = value ;
			}
		}

		public string Title 
		{
			get  
			{
				return _title ;
			}
			set 
			{
				_title = value ;
			}
		}

		public string LastName 
		{
			get  
			{
				return _lastName ;
			}
			set 
			{
				_lastName = value ;
			}
		}

		public string Address 
		{
			get  
			{
				return _address ;
			}
			set 
			{
				_address = value ;
			}
		}

		public DateTime DateOfBirth 
		{
			get  
			{
				return _dateOfBirth ;
			}
			set 
			{
				_dateOfBirth = value ;
			}
		}

		public override string ToString() 
		{
			StringWriter sb = new StringWriter() ;
			sb.WriteLine("Customer: \n");
			sb.WriteLine(this.ID);
			sb.Write(this.Title);
			sb.Write(this.FirstName);
			sb.WriteLine(this.LastName);
			sb.WriteLine(this.DateOfBirth.ToString());
			sb.WriteLine(this.Address);
			return sb.ToString();
		}
	}
}