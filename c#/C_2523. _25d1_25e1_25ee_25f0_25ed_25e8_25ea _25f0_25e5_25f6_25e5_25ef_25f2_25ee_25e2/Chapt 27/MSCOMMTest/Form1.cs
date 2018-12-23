using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace MSCOMMTest
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private AxMSCommLib.AxMSComm axMSComm1;
		private System.Windows.Forms.ListBox lbLog;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			InitializeComponent();
			InitPort();
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Form1));
			this.axMSComm1 = new AxMSCommLib.AxMSComm();
			this.lbLog = new System.Windows.Forms.ListBox();
			((System.ComponentModel.ISupportInitialize)(this.axMSComm1)).BeginInit();
			this.SuspendLayout();
			// 
			// axMSComm1
			// 
			this.axMSComm1.Enabled = true;
			this.axMSComm1.Location = new System.Drawing.Point(160, 72);
			this.axMSComm1.Name = "axMSComm1";
			this.axMSComm1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMSComm1.OcxState")));
			this.axMSComm1.Size = new System.Drawing.Size(38, 38);
			this.axMSComm1.TabIndex = 0;
			// 
			// lbLog
			// 
			this.lbLog.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbLog.Location = new System.Drawing.Point(0, 0);
			this.lbLog.Name = "lbLog";
			this.lbLog.Size = new System.Drawing.Size(424, 251);
			this.lbLog.TabIndex = 1;
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(424, 261);
			this.Controls.Add(this.axMSComm1);
			this.Controls.Add(this.lbLog);
			this.Name = "Form1";
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.axMSComm1)).EndInit();
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

		void InitPort()
		{
			// Задаем номер порта
			axMSComm1.CommPort = 1;
    
			// Закрыть порт, если он открыт
			if (axMSComm1.PortOpen) axMSComm1.PortOpen = false;
    
			// Trigger the OnComm event whenever data is received
			axMSComm1.RThreshold = 1;  
    
			// Настройка порта: 9600, no parity, 8 bits, 1 stop bit
			axMSComm1.Settings = "9600,n,8,1";

			// Режим приема данных - бинарный или текстовый
			axMSComm1.InputMode = MSCommLib.InputModeConstants.comInputModeBinary;
			//axMSComm1.InputMode = MSCommLib.InputModeConstants.comInputModeText;
    
			// Режим ожидания данных
			axMSComm1.InputLen = 0;

			// Не игнорировать 0x00
			axMSComm1.NullDiscard = false;
    
			// Добавляем обработчик получения данных
			axMSComm1.OnComm += new System.EventHandler(this.OnComm);
    
			// Открываем порт
			axMSComm1.PortOpen = true;  
		}

		//  Обработчик получения данных данных
		private void OnComm(object sender, EventArgs e)  
		{
			Application.DoEvents();

			// Обрабока изменения состояния линии CTS
			if (axMSComm1.CommEvent == (short)MSCommLib.OnCommConstants.comEvCTS)
			{
				Log("Изменение состояния линии CTS");
			}

			// Если есть данные в буфере
			if (axMSComm1.InBufferCount > 0) 
			{
				// Если включен режим comInputModeText
				// мы получим строку
				if (axMSComm1.Input is String)
				{
					string Input = (string) axMSComm1.Input;
					Log(Input);
				}
				// Если включен режим comInputModeBinary мы получим
				// System.Array 
				else
				{
					byte [] Input = (byte[])axMSComm1.Input;
					foreach (byte b in	Input)
					{
						Log(string.Format("{0}", b));
					}
				}
			}
		}

		private void Log(string str)
		{
			lbLog.Items.Add(str);
			lbLog.SelectedIndex = lbLog.Items.Count-1;
		}
	}
}
