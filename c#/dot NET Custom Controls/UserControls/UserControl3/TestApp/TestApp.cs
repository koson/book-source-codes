using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace TestApp
{
	// Test Application for the toggled CheckBox und Button
	public class TestApp : System.Windows.Forms.Form
	{
		private Akadia.ToggleButton.ToggleButton btnToggle1;
		private Akadia.ToggleButton.ToggleButton btnToggle2;
		private Akadia.ToggleButton.ToggleButton btnToggle3;
		private System.Windows.Forms.Label lblText1;
		private System.Windows.Forms.Label lblText2;
		private Akadia.ToggleButton.ToggleButton btnToggle4;
		private System.ComponentModel.Container components = null;

		public TestApp()
		{
			InitializeComponent();

			// Set Appearance to CheckBox
			btnToggle1.Appearance = Appearance.Normal;
			btnToggle2.Appearance = Appearance.Normal;
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
			this.btnToggle1 = new Akadia.ToggleButton.ToggleButton();
			this.btnToggle2 = new Akadia.ToggleButton.ToggleButton();
			this.btnToggle3 = new Akadia.ToggleButton.ToggleButton();
			this.lblText1 = new System.Windows.Forms.Label();
			this.lblText2 = new System.Windows.Forms.Label();
			this.btnToggle4 = new Akadia.ToggleButton.ToggleButton();
			this.SuspendLayout();
			// 
			// btnToggle1
			// 
			this.btnToggle1.BackColor = System.Drawing.Color.Green;
			this.btnToggle1.CheckedColor = System.Drawing.Color.Red;
			this.btnToggle1.CheckedText = "On";
			this.btnToggle1.ForeColor = System.Drawing.Color.White;
			this.btnToggle1.Location = new System.Drawing.Point(16, 36);
			this.btnToggle1.Name = "btnToggle1";
			this.btnToggle1.Size = new System.Drawing.Size(104, 14);
			this.btnToggle1.TabIndex = 0;
			this.btnToggle1.Text = "Off";
			this.btnToggle1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.btnToggle1.UncheckedColor = System.Drawing.Color.Green;
			this.btnToggle1.UncheckedText = "Off";
			// 
			// btnToggle2
			// 
			this.btnToggle2.CheckedColor = System.Drawing.Color.Gray;
			this.btnToggle2.CheckedText = "Checked";
			this.btnToggle2.Location = new System.Drawing.Point(16, 56);
			this.btnToggle2.Name = "btnToggle2";
			this.btnToggle2.Size = new System.Drawing.Size(187, 21);
			this.btnToggle2.TabIndex = 1;
			this.btnToggle2.Text = "Off";
			this.btnToggle2.UncheckedColor = System.Drawing.SystemColors.Control;
			this.btnToggle2.UncheckedText = "Unchecked";
			// 
			// btnToggle3
			// 
			this.btnToggle3.Appearance = System.Windows.Forms.Appearance.Button;
			this.btnToggle3.CheckedColor = System.Drawing.Color.Gray;
			this.btnToggle3.CheckedText = "Checked";
			this.btnToggle3.Location = new System.Drawing.Point(20, 136);
			this.btnToggle3.Name = "btnToggle3";
			this.btnToggle3.TabIndex = 2;
			this.btnToggle3.Text = "Off";
			this.btnToggle3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.btnToggle3.UncheckedColor = System.Drawing.SystemColors.Control;
			this.btnToggle3.UncheckedText = "Unchecked";
			// 
			// lblText1
			// 
			this.lblText1.Location = new System.Drawing.Point(21, 8);
			this.lblText1.Name = "lblText1";
			this.lblText1.Size = new System.Drawing.Size(119, 15);
			this.lblText1.TabIndex = 3;
			this.lblText1.Text = "Apperance = Normal";
			// 
			// lblText2
			// 
			this.lblText2.Location = new System.Drawing.Point(21, 112);
			this.lblText2.Name = "lblText2";
			this.lblText2.Size = new System.Drawing.Size(119, 12);
			this.lblText2.TabIndex = 3;
			this.lblText2.Text = "Apperance = Button";
			// 
			// btnToggle4
			// 
			this.btnToggle4.Appearance = System.Windows.Forms.Appearance.Button;
			this.btnToggle4.BackColor = System.Drawing.Color.Green;
			this.btnToggle4.CheckedColor = System.Drawing.Color.Red;
			this.btnToggle4.CheckedText = "On";
			this.btnToggle4.ForeColor = System.Drawing.Color.White;
			this.btnToggle4.Location = new System.Drawing.Point(20, 173);
			this.btnToggle4.Name = "btnToggle4";
			this.btnToggle4.TabIndex = 4;
			this.btnToggle4.Text = "Off";
			this.btnToggle4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.btnToggle4.UncheckedColor = System.Drawing.Color.Green;
			this.btnToggle4.UncheckedText = "Off";
			// 
			// TestApp
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(216, 214);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.btnToggle4,
																		  this.lblText1,
																		  this.btnToggle3,
																		  this.btnToggle2,
																		  this.btnToggle1,
																		  this.lblText2});
			this.Name = "TestApp";
			this.Text = "Inherited User Control";
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
