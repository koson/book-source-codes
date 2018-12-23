//------------------------------------------------------------------------------
// Data Binding in Windows Forms
//
// - Simple Data Binding to Textbox
// - Create own Customer List (ILIST)
// - Format and Parse DateTime using Bindings Format and Parse Event
// - Hook into Form's BindingContext Position Changed Event Handler
//------------------------------------------------------------------------------
namespace Akadia.SimpleBinding.Data 
{
	using System;
	using System.ComponentModel;
	using System.Drawing;
	using System.Windows.Forms;
	using System.Data;

	public class SimpleBinding : System.Windows.Forms.Form 
	{
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.TextBox textBoxDOB;
		private System.Windows.Forms.Label labelDOB;
		private System.Windows.Forms.TextBox textBoxPosition;
		private System.Windows.Forms.Button buttonMoveFirst;
		private System.Windows.Forms.Button buttonMovePrev;
		private System.Windows.Forms.Button buttonMoveNext;
		private System.Windows.Forms.Button buttonMoveLast;
		private System.Windows.Forms.TextBox textBoxTitle;
		private System.Windows.Forms.Label labelTitle;
		private System.Windows.Forms.TextBox textBoxAddress;
		private System.Windows.Forms.TextBox textBoxLastName;
		private System.Windows.Forms.TextBox textBoxFirstName;
		private System.Windows.Forms.TextBox textBoxID;
		private System.Windows.Forms.Label labelAddress;
		private System.Windows.Forms.Label labelLastName;
		private System.Windows.Forms.Label labelFirstName;
		private System.Windows.Forms.Label labelID;
		private System.Windows.Forms.Panel panelVCRControl;
		private CustomerList custList;

		public SimpleBinding() 
		{
			// Required by the Windows Forms Designer
			InitializeComponent();

			// Get the list of customers
			custList = CustomerList.GetCustomers();

			// Set up the bindings so that each field on the form is
			// bound to a property of Customer
			textBoxID.DataBindings.Add("Text", custList, "ID");
			textBoxTitle.DataBindings.Add("Text", custList, "Title");
			textBoxLastName.DataBindings.Add("Text", custList, "LastName");
			textBoxFirstName.DataBindings.Add("Text", custList, "FirstName");
			textBoxAddress.DataBindings.Add("Text", custList, "Address");

			// We want to format the DateOfBirth, so process the format and
			// parse events for the DateOfBirth text box.
			Binding dobBinding = new Binding("Text", custList, "DateOfBirth");
			dobBinding.Format += new ConvertEventHandler(this.textBoxDOB_FormatDate) ;
			dobBinding.Parse += new ConvertEventHandler(this.textBoxDOB_ParseDate) ;
			textBoxDOB.DataBindings.Add(dobBinding);

			// We want to handle position changing events for the DATA textBoxPosition
	        // on the VCR Panel. Position is managed by the Form's BindingContext
			// so hook the position changed event on the BindingContext.

			// Compact Form ...
			// this.BindingContext[custList].PositionChanged += new System.EventHandler(customers_PositionChanged);

			// Get the BindingManagerBase for the Customers List. 
			BindingManagerBase bmCustomers = this.BindingContext[custList];

			// Add the delegate for the PositionChanged event.
			bmCustomers.PositionChanged += new EventHandler(customers_PositionChanged);

			// Set up the initial text for the DATA VCR Panel
			textBoxPosition.Text = String.Format("Record {0} of {1}", (this.BindingContext[custList].Position + 1), custList.Count);

			// Set the minimum form size to the client size + the height of the title bar
			this.MinimumSize = new Size(368, (413 + SystemInformation.CaptionHeight));
		}

		// Format the Date Field to short date form for display in the TextBox
		private void textBoxDOB_FormatDate(object sender, ConvertEventArgs e) 
		{
			// We only deal with converting to strings from dates
			if (e.DesiredType != typeof(string)) return ;
			if (e.Value.GetType() != typeof(DateTime)) return ;

			DateTime dt = (DateTime)e.Value;
			e.Value = dt.ToShortDateString();
		}

		// Parse the textbox contents and turn them back into a date
		private void textBoxDOB_ParseDate(object sender, ConvertEventArgs e) 
		{
			// We only deal with converting to dates and strings
			if (e.DesiredType != typeof(DateTime)) return ;
			if (e.Value.GetType() != typeof(string)) return ;

			string value = (string)e.Value;

			try 
			{
				e.Value = DateTime.Parse(value);
			}
			catch(Exception ex) 
			{
				MessageBox.Show(ex.Message);
			}
		}

		// When the MoveFirst button is clicked set the position for the CustomersList
		// to the first record
		private void buttonMoveFirst_Click(object sender, System.EventArgs e) 
		{
			this.BindingContext[custList].Position = 0 ;
		}

		// When the MoveLast button is clicked set the position for the CustomersList
		// to the last record
		private void buttonMoveLast_Click(object sender, System.EventArgs e) 
		{
			this.BindingContext[custList].Position = custList.Count - 1;
		}

		// When the MoveNext button is clicked increment the position for the CustomersList
		private void buttonMoveNext_Click(object sender, System.EventArgs e) 
		{
			if (this.BindingContext[custList].Position < custList.Count - 1) 
			{
				this.BindingContext[custList].Position++;
			}
		}

		// When the MovePrev button is clicked decrement the position for the CustomersList
		private void buttonMovePrev_Click(object sender, System.EventArgs e) 
		{
			if (this.BindingContext[custList].Position > 0) 
			{
				this.BindingContext[custList].Position--;
			}
		}

		// Position has changed - update the DATA VCR panel
		private void customers_PositionChanged(object sender, System.EventArgs e) 
		{
			textBoxPosition.Text = String.Format("Record {0} of {1}", (this.BindingContext[custList].Position + 1), custList.Count);
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
			System.Configuration.AppSettingsReader configurationAppSettings = new System.Configuration.AppSettingsReader();
			this.textBoxTitle = new System.Windows.Forms.TextBox();
			this.labelFirstName = new System.Windows.Forms.Label();
			this.textBoxID = new System.Windows.Forms.TextBox();
			this.buttonMoveNext = new System.Windows.Forms.Button();
			this.buttonMovePrev = new System.Windows.Forms.Button();
			this.labelTitle = new System.Windows.Forms.Label();
			this.textBoxLastName = new System.Windows.Forms.TextBox();
			this.labelDOB = new System.Windows.Forms.Label();
			this.textBoxPosition = new System.Windows.Forms.TextBox();
			this.textBoxDOB = new System.Windows.Forms.TextBox();
			this.panelVCRControl = new System.Windows.Forms.Panel();
			this.buttonMoveFirst = new System.Windows.Forms.Button();
			this.buttonMoveLast = new System.Windows.Forms.Button();
			this.textBoxAddress = new System.Windows.Forms.TextBox();
			this.labelLastName = new System.Windows.Forms.Label();
			this.labelID = new System.Windows.Forms.Label();
			this.labelAddress = new System.Windows.Forms.Label();
			this.textBoxFirstName = new System.Windows.Forms.TextBox();
			this.panelVCRControl.SuspendLayout();
			this.SuspendLayout();
			// 
			// textBoxTitle
			// 
			this.textBoxTitle.Location = new System.Drawing.Point(88, 70);
			this.textBoxTitle.Name = "textBoxTitle";
			this.textBoxTitle.Size = new System.Drawing.Size(70, 20);
			this.textBoxTitle.TabIndex = 4;
			this.textBoxTitle.Text = "";
			// 
			// labelFirstName
			// 
			this.labelFirstName.Location = new System.Drawing.Point(8, 112);
			this.labelFirstName.Name = "labelFirstName";
			this.labelFirstName.Size = new System.Drawing.Size(64, 16);
			this.labelFirstName.TabIndex = 5;
			this.labelFirstName.Text = "&First Name:";
			// 
			// textBoxID
			// 
			this.textBoxID.Enabled = false;
			this.textBoxID.Location = new System.Drawing.Point(88, 30);
			this.textBoxID.Name = "textBoxID";
			this.textBoxID.ReadOnly = true;
			this.textBoxID.Size = new System.Drawing.Size(203, 20);
			this.textBoxID.TabIndex = 2;
			this.textBoxID.TabStop = false;
			this.textBoxID.Text = "";
			// 
			// buttonMoveNext
			// 
			this.buttonMoveNext.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.buttonMoveNext.Location = new System.Drawing.Point(184, 8);
			this.buttonMoveNext.Name = "buttonMoveNext";
			this.buttonMoveNext.Size = new System.Drawing.Size(32, 32);
			this.buttonMoveNext.TabIndex = 33;
			this.buttonMoveNext.Text = ">";
			this.buttonMoveNext.Click += new System.EventHandler(this.buttonMoveNext_Click);
			// 
			// buttonMovePrev
			// 
			this.buttonMovePrev.Location = new System.Drawing.Point(48, 8);
			this.buttonMovePrev.Name = "buttonMovePrev";
			this.buttonMovePrev.Size = new System.Drawing.Size(32, 32);
			this.buttonMovePrev.TabIndex = 31;
			this.buttonMovePrev.Text = "<";
			this.buttonMovePrev.Click += new System.EventHandler(this.buttonMovePrev_Click);
			// 
			// labelTitle
			// 
			this.labelTitle.Location = new System.Drawing.Point(8, 72);
			this.labelTitle.Name = "labelTitle";
			this.labelTitle.Size = new System.Drawing.Size(64, 16);
			this.labelTitle.TabIndex = 3;
			this.labelTitle.Text = "&Title:";
			// 
			// textBoxLastName
			// 
			this.textBoxLastName.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.textBoxLastName.Location = new System.Drawing.Point(88, 152);
			this.textBoxLastName.Name = "textBoxLastName";
			this.textBoxLastName.Size = new System.Drawing.Size(243, 20);
			this.textBoxLastName.TabIndex = 8;
			this.textBoxLastName.Text = "";
			// 
			// labelDOB
			// 
			this.labelDOB.Location = new System.Drawing.Point(8, 194);
			this.labelDOB.Name = "labelDOB";
			this.labelDOB.Size = new System.Drawing.Size(92, 16);
			this.labelDOB.TabIndex = 9;
			this.labelDOB.Text = "&Date of Birth:";
			// 
			// textBoxPosition
			// 
			this.textBoxPosition.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.textBoxPosition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBoxPosition.Enabled = false;
			this.textBoxPosition.Location = new System.Drawing.Point(88, 14);
			this.textBoxPosition.Name = "textBoxPosition";
			this.textBoxPosition.ReadOnly = true;
			this.textBoxPosition.Size = new System.Drawing.Size(88, 20);
			this.textBoxPosition.TabIndex = 0;
			this.textBoxPosition.TabStop = false;
			this.textBoxPosition.Text = "";
			// 
			// textBoxDOB
			// 
			this.textBoxDOB.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.textBoxDOB.Location = new System.Drawing.Point(88, 192);
			this.textBoxDOB.Name = "textBoxDOB";
			this.textBoxDOB.Size = new System.Drawing.Size(243, 20);
			this.textBoxDOB.TabIndex = 10;
			this.textBoxDOB.Text = "";
			// 
			// panelVCRControl
			// 
			this.panelVCRControl.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.panelVCRControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelVCRControl.Controls.AddRange(new System.Windows.Forms.Control[] {
																						  this.textBoxPosition,
																						  this.buttonMoveFirst,
																						  this.buttonMovePrev,
																						  this.buttonMoveNext,
																						  this.buttonMoveLast});
			this.panelVCRControl.Location = new System.Drawing.Point(88, 344);
			this.panelVCRControl.Name = "panelVCRControl";
			this.panelVCRControl.Size = new System.Drawing.Size(264, 48);
			this.panelVCRControl.TabIndex = 32;
			this.panelVCRControl.Text = "panel1";
			// 
			// buttonMoveFirst
			// 
			this.buttonMoveFirst.Location = new System.Drawing.Point(8, 8);
			this.buttonMoveFirst.Name = "buttonMoveFirst";
			this.buttonMoveFirst.Size = new System.Drawing.Size(32, 32);
			this.buttonMoveFirst.TabIndex = 30;
			this.buttonMoveFirst.Text = "|<";
			this.buttonMoveFirst.Click += new System.EventHandler(this.buttonMoveFirst_Click);
			// 
			// buttonMoveLast
			// 
			this.buttonMoveLast.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.buttonMoveLast.Location = new System.Drawing.Point(224, 8);
			this.buttonMoveLast.Name = "buttonMoveLast";
			this.buttonMoveLast.Size = new System.Drawing.Size(32, 32);
			this.buttonMoveLast.TabIndex = 34;
			this.buttonMoveLast.Text = ">|";
			this.buttonMoveLast.Click += new System.EventHandler(this.buttonMoveLast_Click);
			// 
			// textBoxAddress
			// 
			this.textBoxAddress.AcceptsReturn = true;
			this.textBoxAddress.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.textBoxAddress.Location = new System.Drawing.Point(88, 232);
			this.textBoxAddress.Multiline = true;
			this.textBoxAddress.Name = "textBoxAddress";
			this.textBoxAddress.Size = new System.Drawing.Size(264, 88);
			this.textBoxAddress.TabIndex = 12;
			this.textBoxAddress.Text = "";
			// 
			// labelLastName
			// 
			this.labelLastName.Location = new System.Drawing.Point(8, 154);
			this.labelLastName.Name = "labelLastName";
			this.labelLastName.Size = new System.Drawing.Size(64, 16);
			this.labelLastName.TabIndex = 7;
			this.labelLastName.Text = "&Last Name:";
			// 
			// labelID
			// 
			this.labelID.Location = new System.Drawing.Point(8, 32);
			this.labelID.Name = "labelID";
			this.labelID.Size = new System.Drawing.Size(64, 16);
			this.labelID.TabIndex = 1;
			this.labelID.Text = "ID:";
			// 
			// labelAddress
			// 
			this.labelAddress.Location = new System.Drawing.Point(8, 232);
			this.labelAddress.Name = "labelAddress";
			this.labelAddress.Size = new System.Drawing.Size(64, 16);
			this.labelAddress.TabIndex = 11;
			this.labelAddress.Text = "&Address:";
			// 
			// textBoxFirstName
			// 
			this.textBoxFirstName.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.textBoxFirstName.Location = new System.Drawing.Point(88, 112);
			this.textBoxFirstName.Name = "textBoxFirstName";
			this.textBoxFirstName.Size = new System.Drawing.Size(243, 20);
			this.textBoxFirstName.TabIndex = 6;
			this.textBoxFirstName.Text = "";
			// 
			// SimpleBinding
			// 
			this.AccessibleName = ((string)(configurationAppSettings.GetValue("SimpleBinding.AccessibleName", typeof(string))));
			this.AllowDrop = ((bool)(configurationAppSettings.GetValue("SimpleBinding.AllowDrop", typeof(bool))));
			this.AutoScale = ((bool)(configurationAppSettings.GetValue("SimpleBinding.AutoScale", typeof(bool))));
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.AutoScroll = ((bool)(configurationAppSettings.GetValue("SimpleBinding.AutoScroll", typeof(bool))));
			this.CausesValidation = ((bool)(configurationAppSettings.GetValue("SimpleBinding.CausesValidation", typeof(bool))));
			this.ClientSize = new System.Drawing.Size(368, 413);
			this.ControlBox = ((bool)(configurationAppSettings.GetValue("SimpleBinding.ControlBox", typeof(bool))));
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.textBoxDOB,
																		  this.labelDOB,
																		  this.panelVCRControl,
																		  this.textBoxTitle,
																		  this.labelTitle,
																		  this.textBoxAddress,
																		  this.textBoxLastName,
																		  this.textBoxFirstName,
																		  this.textBoxID,
																		  this.labelAddress,
																		  this.labelLastName,
																		  this.labelFirstName,
																		  this.labelID});
			this.Enabled = ((bool)(configurationAppSettings.GetValue("SimpleBinding.Enabled", typeof(bool))));
			this.HelpButton = ((bool)(configurationAppSettings.GetValue("SimpleBinding.HelpButton", typeof(bool))));
			this.IsMdiContainer = ((bool)(configurationAppSettings.GetValue("SimpleBinding.IsMdiContainer", typeof(bool))));
			this.KeyPreview = ((bool)(configurationAppSettings.GetValue("SimpleBinding.KeyPreview", typeof(bool))));
			this.MaximizeBox = ((bool)(configurationAppSettings.GetValue("SimpleBinding.MaximizeBox", typeof(bool))));
			this.MinimizeBox = ((bool)(configurationAppSettings.GetValue("SimpleBinding.MinimizeBox", typeof(bool))));
			this.Name = "SimpleBinding";
			this.Opacity = ((System.Double)(configurationAppSettings.GetValue("SimpleBinding.Opacity", typeof(System.Double))));
			this.ShowInTaskbar = ((bool)(configurationAppSettings.GetValue("SimpleBinding.ShowInTaskbar", typeof(bool))));
			this.Text = ((string)(configurationAppSettings.GetValue("SimpleBinding.Text", typeof(string))));
			this.TopMost = ((bool)(configurationAppSettings.GetValue("SimpleBinding.TopMost", typeof(bool))));
			this.panelVCRControl.ResumeLayout(false);
			this.ResumeLayout(false);

		}


		[STAThread]
		public static void Main(string[] args) 
		{
			Application.Run(new SimpleBinding());
		}

	}
}