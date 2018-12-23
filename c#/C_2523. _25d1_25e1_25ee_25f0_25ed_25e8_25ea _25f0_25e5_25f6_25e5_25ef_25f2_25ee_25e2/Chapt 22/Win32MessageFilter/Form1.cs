using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace Win32MessageFilter
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListBox lbLog;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public MainForm()
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
			this.lbLog = new System.Windows.Forms.ListBox();
			this.SuspendLayout();
			// 
			// lbLog
			// 
			this.lbLog.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbLog.Location = new System.Drawing.Point(0, 0);
			this.lbLog.Name = "lbLog";
			this.lbLog.Size = new System.Drawing.Size(292, 264);
			this.lbLog.TabIndex = 0;
			// 
			// MailForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 272);
			this.Controls.Add(this.lbLog);
			this.Name = "MailForm";
			this.Text = "Win32MessageFilter";
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new MainForm());
		}

		protected override void WndProc(ref Message m)
		{
			lbLog.Items.Add(String.Format("Msg={0} L={1} W={2}", m.Msg.ToString(), m.LParam.ToString(), m.WParam.ToString()));
			lbLog.SelectedIndex = lbLog.Items.Count-1;

			// Обработка сообщения с кодом m.Msg
			base.WndProc(ref m);
		}

	}
}
