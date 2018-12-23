//------------------------------------------------------------------------------
// Comobox Data Binding 
//------------------------------------------------------------------------------
namespace Akadia.ComboBoxBinding 
{

	using System;
	using System.ComponentModel;
	using System.Drawing;
	using System.Windows.Forms;
	using System.Data;
	using System.Data.SqlClient;

	public class ComboBoxBinding : System.Windows.Forms.Form 
	{
		private System.ComponentModel.Container components;
		private Akadia.ComboBoxBinding.CustomersDataSet customersDataSet1;
		private System.Windows.Forms.TextBox textBoxZip;
		private System.Windows.Forms.ComboBox comboBoxState;
		private System.Windows.Forms.TextBox textBoxCity;
		private System.Windows.Forms.Label labelState;
		private System.Windows.Forms.Label labelZip;
		private System.Windows.Forms.Label labelCity;
		private System.Windows.Forms.TextBox textBoxPosition;
		private System.Windows.Forms.Button buttonMoveFirst;
		private System.Windows.Forms.Button buttonMovePrev;
		private System.Windows.Forms.Button buttonMoveNext;
		private System.Windows.Forms.Button buttonMoveLast;
		private System.Windows.Forms.TextBox textBoxCompany;
		private System.Windows.Forms.Label labelCompanyName;
		private System.Windows.Forms.TextBox textBoxAddress;
		private System.Windows.Forms.TextBox textBoxTitle;
		private System.Windows.Forms.TextBox textBoxContact;
		private System.Windows.Forms.TextBox textBoxID;
		private System.Windows.Forms.Label labelAddress;
		private System.Windows.Forms.Label labelContactTitle;
		private System.Windows.Forms.Label labelContact;
		private System.Windows.Forms.Label labelID;
		private System.Windows.Forms.Panel panelVCRControl;

		// Lookup "Table" with States for the Comobox
		public struct State 
		{
			private string _shortName, _longName;

			public State(string longName , string shortName) 
			{
				this._shortName = shortName; 
				this._longName = longName;
			}

			public string ShortName { get { return _shortName; } }
			public string LongName { get { return _longName; } }
		}

		// Define the Array of States for the DropDown List
		public State[] States  = new State[] {
												 new State("Alabama","AL")
												 ,new State("Alaska","AK")
												 ,new State("Arizona" ,"AZ")
												 ,new State("Arkansas","AR")
												 ,new State("California" ,"CA")
												 ,new State("Colorado","CO")
												 ,new State("Connecticut","CT")
												 ,new State("Delaware","DE")
												 ,new State("District of Columbia","DC")
												 ,new State("Florida" ,"FL")
												 ,new State("Georgia" ,"GA")
												 ,new State("Hawaii"  ,"HI")
												 ,new State("Idaho","ID")
												 ,new State("Illinois","IL")
												 ,new State("Indiana" ,"IN")
												 ,new State("Iowa","IA")
												 ,new State("Kansas"  ,"KS")
												 ,new State("Kentucky","KY")
												 ,new State("Louisiana"  ,"LA")
												 ,new State("Maine","ME")
												 ,new State("Maryland","MD")
												 ,new State("Massachusetts","MA")
												 ,new State("Michigan","MI")
												 ,new State("Minnesota"  ,"MN")
												 ,new State("Mississippi","MS")
												 ,new State("Missouri","MO")
												 ,new State("Montana" ,"MT")
												 ,new State("Nebraska","NE")
												 ,new State("Nevada"  ,"NV")
												 ,new State("New Hampshire","NH")
												 ,new State("New Jersey" ,"NJ")
												 ,new State("New Mexico" ,"NM")
												 ,new State("New York","NY")
												 ,new State("North Carolina","NC")
												 ,new State("North Dakota" ,"ND")
												 ,new State("Ohio","OH")
												 ,new State("Oklahoma","OK")
												 ,new State("Oregon"  ,"OR")
												 ,new State("Pennsylvania" ,"PA")
												 ,new State("Rhode Island" ,"RI")
												 ,new State("South Carolina","SC")
												 ,new State("South Dakota" ,"SD")
												 ,new State("Tennessee"  ,"TN")
												 ,new State("Texas","TX")
												 ,new State("Utah","UT")
												 ,new State("Vermont" ,"VT")
												 ,new State("Virginia","VA")
												 ,new State("Washington" ,"WA")
												 ,new State("West Virginia","WV")
												 ,new State("Wisconsin"  ,"WI")
												 ,new State("Wyoming" ,"WY")
											 } ;

		public ComboBoxBinding() 
		{

			// Required by the Windows Forms Designer
			InitializeComponent();

			// Fill the DataSet
			String ConnectionString = "data source=xeon;uid=sa;password=manager;database=northwind";
			SqlConnection con = new SqlConnection(ConnectionString);
			SqlDataAdapter cmd = new SqlDataAdapter("Select * from Customers where country='USA'", con);
			cmd.Fill(customersDataSet1, "Customers");

			// Set up the Combobox bindings
			comboBoxState.DataSource = States;            // Populate the list
			comboBoxState.DisplayMember = "LongName";     // Define the field to be displayed
			comboBoxState.ValueMember = "ShortName";      // Define the field to be used as the value

			// Bind the selected value of the the ComboBox to the
			// Region field of the current Customer
			comboBoxState.DataBindings.Add("SelectedValue", customersDataSet1, "Customers.Region");

			// Set up the rest of the form bindings
			textBoxID.DataBindings.Add("Text", customersDataSet1, "Customers.CustomerID");
			textBoxCity.DataBindings.Add("Text", customersDataSet1, "Customers.City");
			textBoxTitle.DataBindings.Add("Text", customersDataSet1, "Customers.ContactTitle");
			textBoxAddress.DataBindings.Add("Text", customersDataSet1, "Customers.Address");
			textBoxCompany.DataBindings.Add("Text", customersDataSet1, "Customers.CompanyName");
			textBoxContact.DataBindings.Add("Text", customersDataSet1, "Customers.ContactName");
			textBoxZip.DataBindings.Add("Text", customersDataSet1, "Customers.PostalCode");

			// We want to handle position changing events for the DATA VCR Panel
			this.BindingContext[customersDataSet1,"Customers"].PositionChanged += new System.EventHandler(customers_PositionChanged);

			// Set up the initial text for the DATA VCR Panel
			textBoxPosition.Text = "Record " + (this.BindingContext[customersDataSet1,"Customers"].Position + 1) + " of " + customersDataSet1.Customers.Count;

			// Set the minimum form size
			this.MinimumSize = new Size(375, 361);
		}


		// When the MoveFirst button is clicked set the position for the Customers table
		// to the first record
		private void buttonMoveFirst_Click(object sender, System.EventArgs e) 
		{
			this.BindingContext[customersDataSet1,"Customers"].Position = 0 ;
		}


		// When the MoveLast button is clicked set the position for the Customers table
		// to the last record
		private void buttonMoveLast_Click(object sender, System.EventArgs e) 
		{
			this.BindingContext[customersDataSet1,"Customers"].Position = customersDataSet1.Customers.Count - 1;
		}


		// When the MoveNext button is clicked increment the position for the Customers table
		private void buttonMoveNext_Click(object sender, System.EventArgs e) 
		{
			if (this.BindingContext[customersDataSet1,"Customers"].Position < customersDataSet1.Customers.Count - 1) 
			{
				this.BindingContext[customersDataSet1,"Customers"].Position++;
			}
		}


		// When the MovePrev button is clicked decrement the position for the Customers table
		private void buttonMovePrev_Click(object sender, System.EventArgs e) 
		{
			if (this.BindingContext[customersDataSet1,"Customers"].Position > 0) 
			{
				this.BindingContext[customersDataSet1,"Customers"].Position--;
			}
		}


		// Position has changed - update the DATA VCR panel
		private void customers_PositionChanged(object sender, System.EventArgs e) 
		{
			textBoxPosition.Text = "Record " + (this.BindingContext[customersDataSet1,"Customers"].Position + 1) + " of " + customersDataSet1.Customers.Count;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing) 
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent() 
		{
			this.labelCity = new System.Windows.Forms.Label();
			this.buttonMoveNext = new System.Windows.Forms.Button();
			this.customersDataSet1 = new Akadia.ComboBoxBinding.CustomersDataSet();
			this.labelContact = new System.Windows.Forms.Label();
			this.buttonMoveFirst = new System.Windows.Forms.Button();
			this.textBoxID = new System.Windows.Forms.TextBox();
			this.textBoxCity = new System.Windows.Forms.TextBox();
			this.labelContactTitle = new System.Windows.Forms.Label();
			this.labelCompanyName = new System.Windows.Forms.Label();
			this.textBoxTitle = new System.Windows.Forms.TextBox();
			this.textBoxPosition = new System.Windows.Forms.TextBox();
			this.labelState = new System.Windows.Forms.Label();
			this.buttonMovePrev = new System.Windows.Forms.Button();
			this.labelZip = new System.Windows.Forms.Label();
			this.textBoxAddress = new System.Windows.Forms.TextBox();
			this.textBoxCompany = new System.Windows.Forms.TextBox();
			this.panelVCRControl = new System.Windows.Forms.Panel();
			this.buttonMoveLast = new System.Windows.Forms.Button();
			this.comboBoxState = new System.Windows.Forms.ComboBox();
			this.labelAddress = new System.Windows.Forms.Label();
			this.labelID = new System.Windows.Forms.Label();
			this.textBoxContact = new System.Windows.Forms.TextBox();
			this.textBoxZip = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.customersDataSet1)).BeginInit();
			this.panelVCRControl.SuspendLayout();
			this.SuspendLayout();
			// 
			// labelCity
			// 
			this.labelCity.Location = new System.Drawing.Point(18, 178);
			this.labelCity.Name = "labelCity";
			this.labelCity.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.labelCity.Size = new System.Drawing.Size(64, 16);
			this.labelCity.TabIndex = 11;
			this.labelCity.Text = "City";
			// 
			// buttonMoveNext
			// 
			this.buttonMoveNext.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.buttonMoveNext.Location = new System.Drawing.Point(212, 8);
			this.buttonMoveNext.Name = "buttonMoveNext";
			this.buttonMoveNext.Size = new System.Drawing.Size(32, 32);
			this.buttonMoveNext.TabIndex = 2;
			this.buttonMoveNext.Text = ">";
			this.buttonMoveNext.Click += new System.EventHandler(this.buttonMoveNext_Click);
			// 
			// customersDataSet1
			// 
			this.customersDataSet1.DataSetName = "CustomersDataSet";
			this.customersDataSet1.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// labelContact
			// 
			this.labelContact.Location = new System.Drawing.Point(18, 82);
			this.labelContact.Name = "labelContact";
			this.labelContact.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.labelContact.Size = new System.Drawing.Size(64, 16);
			this.labelContact.TabIndex = 7;
			this.labelContact.Text = "Contact";
			// 
			// buttonMoveFirst
			// 
			this.buttonMoveFirst.Location = new System.Drawing.Point(8, 8);
			this.buttonMoveFirst.Name = "buttonMoveFirst";
			this.buttonMoveFirst.Size = new System.Drawing.Size(32, 32);
			this.buttonMoveFirst.TabIndex = 0;
			this.buttonMoveFirst.Text = "|<";
			this.buttonMoveFirst.Click += new System.EventHandler(this.buttonMoveFirst_Click);
			// 
			// textBoxID
			// 
			this.textBoxID.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.textBoxID.Enabled = false;
			this.textBoxID.Location = new System.Drawing.Point(88, 16);
			this.textBoxID.Name = "textBoxID";
			this.textBoxID.ReadOnly = true;
			this.textBoxID.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.textBoxID.Size = new System.Drawing.Size(230, 20);
			this.textBoxID.TabIndex = 0;
			this.textBoxID.Text = "";
			// 
			// textBoxCity
			// 
			this.textBoxCity.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.textBoxCity.Location = new System.Drawing.Point(88, 176);
			this.textBoxCity.Name = "textBoxCity";
			this.textBoxCity.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.textBoxCity.Size = new System.Drawing.Size(148, 20);
			this.textBoxCity.TabIndex = 5;
			this.textBoxCity.Text = "";
			// 
			// labelContactTitle
			// 
			this.labelContactTitle.Location = new System.Drawing.Point(18, 114);
			this.labelContactTitle.Name = "labelContactTitle";
			this.labelContactTitle.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.labelContactTitle.Size = new System.Drawing.Size(64, 16);
			this.labelContactTitle.TabIndex = 8;
			this.labelContactTitle.Text = "Title";
			// 
			// labelCompanyName
			// 
			this.labelCompanyName.Location = new System.Drawing.Point(18, 50);
			this.labelCompanyName.Name = "labelCompanyName";
			this.labelCompanyName.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.labelCompanyName.Size = new System.Drawing.Size(64, 16);
			this.labelCompanyName.TabIndex = 6;
			this.labelCompanyName.Text = "Company";
			// 
			// textBoxTitle
			// 
			this.textBoxTitle.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.textBoxTitle.Location = new System.Drawing.Point(88, 112);
			this.textBoxTitle.Name = "textBoxTitle";
			this.textBoxTitle.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.textBoxTitle.Size = new System.Drawing.Size(148, 20);
			this.textBoxTitle.TabIndex = 3;
			this.textBoxTitle.Text = "";
			// 
			// textBoxPosition
			// 
			this.textBoxPosition.Anchor = (System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			this.textBoxPosition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBoxPosition.Enabled = false;
			this.textBoxPosition.ForeColor = System.Drawing.Color.Black;
			this.textBoxPosition.Location = new System.Drawing.Point(88, 15);
			this.textBoxPosition.Name = "textBoxPosition";
			this.textBoxPosition.ReadOnly = true;
			this.textBoxPosition.Size = new System.Drawing.Size(116, 20);
			this.textBoxPosition.TabIndex = 1;
			this.textBoxPosition.Text = "";
			this.textBoxPosition.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// labelState
			// 
			this.labelState.Location = new System.Drawing.Point(18, 210);
			this.labelState.Name = "labelState";
			this.labelState.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.labelState.Size = new System.Drawing.Size(64, 16);
			this.labelState.TabIndex = 13;
			this.labelState.Text = "State";
			// 
			// buttonMovePrev
			// 
			this.buttonMovePrev.Location = new System.Drawing.Point(48, 8);
			this.buttonMovePrev.Name = "buttonMovePrev";
			this.buttonMovePrev.Size = new System.Drawing.Size(32, 32);
			this.buttonMovePrev.TabIndex = 1;
			this.buttonMovePrev.Text = "<";
			this.buttonMovePrev.Click += new System.EventHandler(this.buttonMovePrev_Click);
			// 
			// labelZip
			// 
			this.labelZip.Location = new System.Drawing.Point(18, 242);
			this.labelZip.Name = "labelZip";
			this.labelZip.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.labelZip.Size = new System.Drawing.Size(64, 16);
			this.labelZip.TabIndex = 12;
			this.labelZip.Text = "Zip";
			// 
			// textBoxAddress
			// 
			this.textBoxAddress.AcceptsReturn = true;
			this.textBoxAddress.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.textBoxAddress.Location = new System.Drawing.Point(88, 144);
			this.textBoxAddress.Name = "textBoxAddress";
			this.textBoxAddress.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.textBoxAddress.Size = new System.Drawing.Size(230, 20);
			this.textBoxAddress.TabIndex = 4;
			this.textBoxAddress.Text = "";
			// 
			// textBoxCompany
			// 
			this.textBoxCompany.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.textBoxCompany.Location = new System.Drawing.Point(88, 48);
			this.textBoxCompany.Name = "textBoxCompany";
			this.textBoxCompany.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.textBoxCompany.Size = new System.Drawing.Size(230, 20);
			this.textBoxCompany.TabIndex = 1;
			this.textBoxCompany.Text = "";
			// 
			// panelVCRControl
			// 
			this.panelVCRControl.Anchor = ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.panelVCRControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelVCRControl.Controls.AddRange(new System.Windows.Forms.Control[] {
																						  this.textBoxPosition,
																						  this.buttonMoveFirst,
																						  this.buttonMovePrev,
																						  this.buttonMoveNext,
																						  this.buttonMoveLast});
			this.panelVCRControl.Location = new System.Drawing.Point(52, 288);
			this.panelVCRControl.Name = "panelVCRControl";
			this.panelVCRControl.Size = new System.Drawing.Size(292, 48);
			this.panelVCRControl.TabIndex = 8;
			this.panelVCRControl.Text = "panel1";
			// 
			// buttonMoveLast
			// 
			this.buttonMoveLast.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.buttonMoveLast.Location = new System.Drawing.Point(252, 8);
			this.buttonMoveLast.Name = "buttonMoveLast";
			this.buttonMoveLast.Size = new System.Drawing.Size(32, 32);
			this.buttonMoveLast.TabIndex = 3;
			this.buttonMoveLast.Text = ">|";
			this.buttonMoveLast.Click += new System.EventHandler(this.buttonMoveLast_Click);
			// 
			// comboBoxState
			// 
			this.comboBoxState.Location = new System.Drawing.Point(88, 208);
			this.comboBoxState.Name = "comboBoxState";
			this.comboBoxState.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.comboBoxState.Size = new System.Drawing.Size(176, 21);
			this.comboBoxState.TabIndex = 6;
			// 
			// labelAddress
			// 
			this.labelAddress.Location = new System.Drawing.Point(18, 146);
			this.labelAddress.Name = "labelAddress";
			this.labelAddress.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.labelAddress.Size = new System.Drawing.Size(64, 16);
			this.labelAddress.TabIndex = 9;
			this.labelAddress.Text = "Address";
			// 
			// labelID
			// 
			this.labelID.Location = new System.Drawing.Point(18, 18);
			this.labelID.Name = "labelID";
			this.labelID.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.labelID.Size = new System.Drawing.Size(64, 16);
			this.labelID.TabIndex = 5;
			this.labelID.Text = "ID";
			// 
			// textBoxContact
			// 
			this.textBoxContact.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.textBoxContact.Location = new System.Drawing.Point(88, 80);
			this.textBoxContact.Name = "textBoxContact";
			this.textBoxContact.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.textBoxContact.Size = new System.Drawing.Size(230, 20);
			this.textBoxContact.TabIndex = 2;
			this.textBoxContact.Text = "";
			// 
			// textBoxZip
			// 
			this.textBoxZip.Location = new System.Drawing.Point(88, 240);
			this.textBoxZip.Name = "textBoxZip";
			this.textBoxZip.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.textBoxZip.Size = new System.Drawing.Size(112, 20);
			this.textBoxZip.TabIndex = 7;
			this.textBoxZip.Text = "";
			// 
			// ComboBoxBinding
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(396, 357);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.textBoxZip,
																		  this.comboBoxState,
																		  this.textBoxCity,
																		  this.labelState,
																		  this.labelZip,
																		  this.labelCity,
																		  this.panelVCRControl,
																		  this.textBoxCompany,
																		  this.labelCompanyName,
																		  this.textBoxAddress,
																		  this.textBoxTitle,
																		  this.textBoxContact,
																		  this.textBoxID,
																		  this.labelAddress,
																		  this.labelContactTitle,
																		  this.labelContact,
																		  this.labelID});
			this.Name = "ComboBoxBinding";
			this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.Text = "Combobox Data Binding - Bind Region to Selected Item in Combobox";
			((System.ComponentModel.ISupportInitialize)(this.customersDataSet1)).EndInit();
			this.panelVCRControl.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		[STAThread]
		public static void Main(string[] args) 
		{
			Application.Run(new ComboBoxBinding());
		}

	}
}