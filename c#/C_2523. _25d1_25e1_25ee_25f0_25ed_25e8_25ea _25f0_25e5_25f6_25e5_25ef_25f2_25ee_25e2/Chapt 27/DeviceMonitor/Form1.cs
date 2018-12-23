using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using System.Runtime.InteropServices;

namespace DeviceMonitor
{
	public class Form1 : System.Windows.Forms.Form
	{
		private System.ComponentModel.Container components = null;

		public Form1()
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
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.SuspendLayout();
			// 
			// listBox1
			// 
			this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listBox1.Location = new System.Drawing.Point(0, 0);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(292, 264);
			this.listBox1.TabIndex = 0;
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.Controls.Add(this.listBox1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);

		}
		#endregion

		private System.Windows.Forms.ListBox listBox1;

		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		const int WM_DEVICECHANGE			= 0x0219;
		const int DBT_DEVICEARRIVAL			= 0x8000; 
		const int DBT_DEVICEREMOVECOMPLETE	= 0x8004;
		
		[StructLayout(LayoutKind.Sequential)]
		public struct DEV_BROADCAST_HDR
		{
			public int dbch_size;
			public int dbch_devicetype;
			public int dbch_reserved;
		}

		protected override void WndProc(ref Message m)
		{
			if (m.Msg == WM_DEVICECHANGE)
			{
				int EventCode = m.WParam.ToInt32();
				Log(string.Format("WM_DEVICECHANGE. Код={0}", EventCode));

				switch (EventCode)
				{
					case DBT_DEVICEARRIVAL:
					{
						Log("Добавление устройства");
						break;
					}
					case DBT_DEVICEREMOVECOMPLETE:
					{
						Log("Удаление устройства");
						break;
					}
				}
			}
			base.WndProc (ref m);

		}

		private void Log(string s)
		{
			listBox1.Items.Add(s);
			listBox1.SelectedIndex = listBox1.Items.Count-1;
		}

	}
}
