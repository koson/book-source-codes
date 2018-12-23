using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace TestApp
{
	// Test Application for the Edit mask User Control
	public class TestApp : System.Windows.Forms.Form
	{
		private Akadia.FormatMask.EditMask editMask;
		private System.Windows.Forms.Label lblText;
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
			this.editMask = new Akadia.FormatMask.EditMask();
			this.lblText = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// editMask
			// 
			this.editMask.Location = new System.Drawing.Point(93, 63);
			this.editMask.Mask = "[###]-(##)-#####";
			this.editMask.Name = "editMask";
			this.editMask.TabIndex = 0;
			this.editMask.Text = "";
			// 
			// lblText
			// 
			this.lblText.Location = new System.Drawing.Point(91, 47);
			this.lblText.Name = "lblText";
			this.lblText.Size = new System.Drawing.Size(72, 12);
			this.lblText.TabIndex = 1;
			this.lblText.Text = "Enter ID";
			// 
			// TestApp
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 152);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.lblText,
																		  this.editMask});
			this.Name = "TestApp";
			this.Text = "Masked Edit Text Box";
			this.ResumeLayout(false);

		}
		#endregion

		[STAThread]
		static void Main() 
		{
			Application.Run(new TestApp());
		}
	}
}
