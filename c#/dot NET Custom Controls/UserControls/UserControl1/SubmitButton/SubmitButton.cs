using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace Akadia
{
	namespace SubmitButton
	{
		// User Control which contain a text box for your
		// name and a button that will fire an event. 
		public class SubmitButtonControl : System.Windows.Forms.UserControl
		{
			private System.Windows.Forms.TextBox txtName;
			private System.Windows.Forms.Label lblName;
			private System.Windows.Forms.Button btnSubmit;
			private System.ComponentModel.Container components = null;

			// Declare delegate for submit button clicked.
			// 
			// Most action events (like the Click event) in Windows Forms 
			// use the EventHandler delegate and the EventArgs arguments. 
			// We will define our own delegate that does not specify parameters.
			// Mostly, we really don't care what the conditions of the
			// click event for the Submit button were, we just care that
			// the Submit button was clicked.
			public delegate void SubmitClickedHandler();

			// Constructor
			public SubmitButtonControl()
			{
				// Create visual controls
				InitializeComponent();
			}

			// Clean up any resources being used.
			protected override void Dispose( bool disposing )
			{
				if( disposing )
				{
					if( components != null )
						components.Dispose();
				}
				base.Dispose( disposing );
			}

		#region Component Designer generated code
			/// <summary>
			/// Required method for Designer support - do not modify 
			/// the contents of this method with the code editor.
			/// </summary>
			private void InitializeComponent()
			{
				this.txtName = new System.Windows.Forms.TextBox();
				this.lblName = new System.Windows.Forms.Label();
				this.btnSubmit = new System.Windows.Forms.Button();
				this.SuspendLayout();
				// 
				// txtName
				// 
				this.txtName.Location = new System.Drawing.Point(74, 15);
				this.txtName.Name = "txtName";
				this.txtName.TabIndex = 0;
				this.txtName.Text = "";
				// 
				// lblName
				// 
				this.lblName.Location = new System.Drawing.Point(25, 17);
				this.lblName.Name = "lblName";
				this.lblName.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
				this.lblName.Size = new System.Drawing.Size(39, 14);
				this.lblName.TabIndex = 1;
				this.lblName.Text = "Name";
				// 
				// btnSubmit
				// 
				this.btnSubmit.Location = new System.Drawing.Point(79, 78);
				this.btnSubmit.Name = "btnSubmit";
				this.btnSubmit.TabIndex = 2;
				this.btnSubmit.Text = "Submit";
				this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
				// 
				// SubmitButtonControl
				// 
				this.Controls.AddRange(new System.Windows.Forms.Control[] {
																			  this.btnSubmit,
																			  this.lblName,
																			  this.txtName});
				this.Name = "SubmitButtonControl";
				this.Size = new System.Drawing.Size(233, 126);
				this.ResumeLayout(false);

			}
		#endregion

			// Declare the event, which is associated with our
			// delegate SubmitClickedHandler(). Add some attributes
			// for the Visual C# control property.
			[Category("Action")]
			[Description("Fires when the Submit button is clicked.")]
			public event SubmitClickedHandler SubmitClicked;

			// Add a protected method called OnSubmitClicked(). 
			// You may use this in child classes instead of adding
			// event handlers.
			protected virtual void OnSubmitClicked()
			{
				// If an event has no subscribers registerd, it will 
				// evaluate to null. The test checks that the value is not
				// null, ensuring that there are subsribers before
				// calling the event itself.
				if (SubmitClicked != null) 
				{
					SubmitClicked();  // Notify Subscribers
				}
			}

			// Handler for Submit Button. Do some validation before
			// calling the event.
			private void btnSubmit_Click(object sender, System.EventArgs e)
			{
				if (txtName.Text.Length == 0) 
				{
					MessageBox.Show("Please enter your name.");
				}
				else 
				{
					OnSubmitClicked();
				}
			}
		
			// Read / Write Property for the User Name. This Property
			// will be visible in the containing application.
			[Category("Appearance")]
			[Description("Gets or sets the name in the text box")]
			public string UserName
			{
				get { return txtName.Text; }
				set { txtName.Text = value; }
			}
		}
	}
}
