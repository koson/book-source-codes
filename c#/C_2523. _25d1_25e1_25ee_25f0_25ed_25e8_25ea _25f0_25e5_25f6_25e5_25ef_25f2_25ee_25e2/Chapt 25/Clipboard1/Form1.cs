using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace WindowsApplication4
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button1;
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
			this.label1 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(264, 240);
			this.label1.TabIndex = 0;
			this.label1.Text = "label1";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(160, 120);
			this.button1.Name = "button1";
			this.button1.TabIndex = 1;
			this.button1.Text = "button1";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);

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
			System.Windows.Forms.IDataObject o = System.Windows.Forms.Clipboard.GetDataObject(); 
 
			if(o.GetDataPresent(System.Windows.Forms.DataFormats.CommaSeparatedValue)) 
			{ 
				System.IO.StreamReader sr = new System.IO.StreamReader((System.IO.Stream) o.GetData(DataFormats.CommaSeparatedValue)); 
				string s = sr.ReadToEnd(); 
				sr.Close(); 
				label1.Text= s;
			} 
		
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			String csv = "1;2;3" + Environment.NewLine + "6;8;3"; 
			byte[] blob = System.Text.Encoding.UTF8.GetBytes(csv); 
			System.IO.MemoryStream s = new System.IO.MemoryStream(blob); 
			DataObject data = new DataObject(); 
			data.SetData(DataFormats.CommaSeparatedValue, s); 
			Clipboard.SetDataObject(data, true); 
		}
	}
}
