using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace TestApp
{
	// Test Application for the Login Validation User Control
	public class TestApp : System.Windows.Forms.Form
	{
		private Akadia.LoginControl.LoginControl loginControl;
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
			this.loginControl = new Akadia.LoginControl.LoginControl();
			this.SuspendLayout();
			// 
			// loginControl
			// 
			this.loginControl.LabelName = "Name";
			this.loginControl.LabelPassword = "Password";
			this.loginControl.Location = new System.Drawing.Point(62, 14);
			this.loginControl.LoginButtonText = "Login";
			this.loginControl.Name = "loginControl";
			this.loginControl.Size = new System.Drawing.Size(265, 176);
			this.loginControl.TabIndex = 0;
			this.loginControl.LoginFailed += new Akadia.LoginControl.LoginControl.EventHandler(this.LoginFailed);
			this.loginControl.LoginSuccess += new Akadia.LoginControl.LoginControl.EventHandler(this.LoginSuccess);
			// 
			// TestApp
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(388, 215);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.loginControl});
			this.Name = "TestApp";
			this.Text = "Test Application for the Login Validation User Control";
			this.ResumeLayout(false);

		}
		#endregion

		[STAThread]
		static void Main() 
		{
			Application.Run(new TestApp());
		}

		// This Event is fired by the Login Validation User Control
		private void LoginFailed(object sender, System.EventArgs e)
		{
			MessageBox.Show("Login falied ....", "Login Validation",
				MessageBoxButtons.OK, MessageBoxIcon.Exclamation);		
		}

		// This Event is fired by the Login Validation User Control
		private void LoginSuccess(object sender, System.EventArgs e)
		{
			MessageBox.Show("Login success ....", "Login Validation",
				MessageBoxButtons.OK, MessageBoxIcon.Exclamation);				
		}
	}
}
