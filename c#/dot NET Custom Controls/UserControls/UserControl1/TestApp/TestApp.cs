using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace TestApp
{
	// Test Application for the Submit Button User Control
	public class TestApp : System.Windows.Forms.Form
	{
		private Akadia.SubmitButton.SubmitButtonControl submitButtonControl;
		private System.ComponentModel.Container components = null;

		public TestApp()
		{
			InitializeComponent();
		}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.submitButtonControl = new Akadia.SubmitButton.SubmitButtonControl();
			this.SuspendLayout();
			// 
			// submitButtonControl
			// 
			this.submitButtonControl.Location = new System.Drawing.Point(74, 57);
			this.submitButtonControl.Name = "submitButtonControl";
			this.submitButtonControl.Size = new System.Drawing.Size(233, 126);
			this.submitButtonControl.TabIndex = 0;
			this.submitButtonControl.UserName = "";
			this.submitButtonControl.SubmitClicked += new Akadia.SubmitButton.SubmitButtonControl.SubmitClickedHandler(this.SubmitClicked);
			// 
			// TestApp
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(381, 273);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.submitButtonControl});
			this.Name = "TestApp";
			this.Text = "Test Application for Submit Button User Control";
			this.ResumeLayout(false);

		}
		#endregion

		[STAThread]
		static void Main() 
		{
			Application.Run(new TestApp());
		}

		// Handle the SubmitClicked Event
		private void SubmitClicked()
		{
			MessageBox.Show(String.Format("Hello, {0}!",
				submitButtonControl.UserName));
			this.Close();		
		}
	}
}
