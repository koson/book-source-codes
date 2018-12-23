using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace WindowsApplication11
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
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
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
			Microsoft.Win32.SystemEvents.DisplaySettingsChanged += new System.EventHandler(displaySettingsChanged); 
			Microsoft.Win32.SystemEvents.UserPreferenceChanged += 
                     new Microsoft.Win32.UserPreferenceChangedEventHandler(userPreferencesChanged); 
 		}

		private void displaySettingsChanged(object sender, EventArgs e) 
 		{ 
 			MessageBox.Show(e.ToString()); 
 		} 
 
		private void userPreferencesChanged(object sender, Microsoft.Win32.UserPreferenceChangedEventArgs e) 
 		{ 
 			switch(e.Category) 
 			{ 
				case Microsoft.Win32.UserPreferenceCategory.Locale: 
 					MessageBox.Show("Changed locale"); 
 					break; 
 				default: 
 					MessageBox.Show(e.Category.ToString()); 
 					break; 
 			} 
 		}
 
	}
}
