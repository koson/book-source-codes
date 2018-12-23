using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Runtime.InteropServices; 
using System.Reflection; 

namespace ImageCapture
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
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
			this.button1 = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(136, 104);
			this.button1.Name = "button1";
			this.button1.TabIndex = 0;
			this.button1.Text = "button1";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(72, 48);
			this.label1.Name = "label1";
			this.label1.TabIndex = 1;
			this.label1.Text = "label1";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(120, 176);
			this.label2.Name = "label2";
			this.label2.TabIndex = 2;
			this.label2.Text = "label2";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.button1);
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

		[System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")] 
 
		private static extern bool BitBlt( 
			IntPtr hdcDest, // handle to destination DC 
			int nXDest, // x-coord of destination upper-left corner 
 			int nYDest, // y-coord of destination upper-left corner 
 			int nWidth, // width of destination rectangle 
 			int nHeight, // height of destination rectangle 
 			IntPtr hdcSrc, // handle to source DC 
 			int nXSrc, // x-coordinate of source upper-left corner 
 			int nYSrc, // y-coordinate of source upper-left corner 
 			System.Int32 dwRop // raster operation code 
 		); 
 

		private void button1_Click(object sender, System.EventArgs e)
		{
			Graphics g1 = this.CreateGraphics(); 
 			Image MyImage = new Bitmap(this.ClientRectangle.Width, this.ClientRectangle.Height, g1); 
 			Graphics g2 = Graphics.FromImage(MyImage); 
 			IntPtr dc1 = g1.GetHdc(); 
 			IntPtr dc2 = g2.GetHdc(); 
 			BitBlt(dc2, 0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height, dc1, 0, 0, 13369376); 
 			g1.ReleaseHdc(dc1); 
 			g2.ReleaseHdc(dc2); 
 			MyImage.Save("out.bmp", ImageFormat.Bmp); 
		}
	}
}
