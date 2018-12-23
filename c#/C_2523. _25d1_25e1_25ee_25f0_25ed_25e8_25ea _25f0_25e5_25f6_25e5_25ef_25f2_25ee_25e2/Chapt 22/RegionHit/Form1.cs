using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace RegionHit
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label1;
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel1.Location = new System.Drawing.Point(8, 8);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(280, 176);
			this.panel1.TabIndex = 0;
			this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
			this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(72, 200);
			this.label1.Name = "label1";
			this.label1.TabIndex = 1;
			this.label1.Text = "label1";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.panel1);
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

		public System.Drawing.Drawing2D.GraphicsPath path;

		private void Form1_Load(object sender, System.EventArgs e)
		{
			// Создаем некоторую область
			path = new System.Drawing.Drawing2D.GraphicsPath();
			path.AddLine(0,0,100,100);
			path.AddLine(100,100,50,20);
			path.AddLine(50,20,0,0);
		}

		private void panel1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			// Рисуем
			e.Graphics.DrawPath(new Pen(Color.Red), path);
		}

		private void panel1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			// Проверяем на попадание точки в область
			Region region = new Region(path);
			if (region.IsVisible(e.X, e.Y))
				label1.Text = "IN";
			else
				label1.Text = "OUT";
		}
	}
}
