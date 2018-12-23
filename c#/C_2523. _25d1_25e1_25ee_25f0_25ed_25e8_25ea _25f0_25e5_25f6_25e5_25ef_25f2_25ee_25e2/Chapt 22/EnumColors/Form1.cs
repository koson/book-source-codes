using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace WindowsApplication6
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListBox listBox1;
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
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// listBox1
			// 
			this.listBox1.Location = new System.Drawing.Point(8, 8);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(168, 264);
			this.listBox1.TabIndex = 0;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(184, 8);
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
			this.Controls.Add(this.listBox1);
			this.Name = "Form1";
			this.Text = "Form1";
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

		private void button1_Click(object sender, System.EventArgs e)
		{
			
			string[] colors= Enum.GetNames(typeof(System.Drawing.KnownColor)); 
			foreach (string color in colors)
			{
				if(Color.FromKnownColor((KnownColor)Enum.Parse(typeof(KnownColor), color)).IsSystemColor)
				{
					// системные цвета
					listBox1.Items.Add(color);
				}
				else
				{
					// остальные цвета
				}
			}

			listBox1.Items.Add("========================");
			System.Reflection.PropertyInfo[] arr_pi = Color.Empty.GetType().GetProperties(); 
			foreach(System.Reflection.PropertyInfo p in arr_pi)
			{
				if(p.PropertyType != typeof(System.Drawing.Color))
					listBox1.Items.Add(p.Name);
			} 

			listBox1.Items.Add("========================");
			System.Reflection.PropertyInfo[] Props = listBox1.GetType().GetProperties();
			foreach(System.Reflection.PropertyInfo prop in Props)
			{
				listBox1.Items.Add(prop.PropertyType.ToString()+ ":"+ prop.Name);
			} 

		}
	}
}
