using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.DirectX.DirectInput;

public class MainForm : System.Windows.Forms.Form
{
	#region Web controls
	private System.Windows.Forms.Button btnRead;
	private System.Windows.Forms.TextBox tbResult;

	public static void Main(string[] args)
	{
		Application.Run(new MainForm());
	}

	private void InitializeComponent()
	{
		this.tbResult = new System.Windows.Forms.TextBox();
		this.btnRead = new System.Windows.Forms.Button();
		this.SuspendLayout();
		// 
		// tbResult
		// 
		this.tbResult.BackColor = System.Drawing.SystemColors.Control;
		this.tbResult.Location = new System.Drawing.Point(8, 8);
		this.tbResult.Name = "tbResult";
		this.tbResult.ReadOnly = true;
		this.tbResult.Size = new System.Drawing.Size(280, 20);
		this.tbResult.TabIndex = 0;
		this.tbResult.Text = "";
		// 
		// btnRead
		// 
		this.btnRead.Location = new System.Drawing.Point(8, 40);
		this.btnRead.Name = "btnRead";
		this.btnRead.Size = new System.Drawing.Size(280, 23);
		this.btnRead.TabIndex = 1;
		this.btnRead.Text = "Прочитать данные";
		this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
		// 
		// MainForm
		// 
		this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
		this.ClientSize = new System.Drawing.Size(292, 77);
		this.Controls.Add(this.btnRead);
		this.Controls.Add(this.tbResult);
		this.Name = "MainForm";
		this.Text = "DirectInput: сканкоды клавиатуры";
		this.ResumeLayout(false);

	}
	#endregion

	private Device device = null;
	private BufferedDataCollection dataCollection = null;
				
	public MainForm()
	{
		InitializeComponent();

		// Создаем объект устройства
		device = new Device(SystemGuid.Keyboard);
		// Режим работы
		device.SetCooperativeLevel(this, CooperativeLevelFlags.Background | CooperativeLevelFlags.NonExclusive);
		// Размер буфера
		device.Properties.BufferSize = 11;
		// Начало обмена
		device.Acquire();
	}

	private void btnRead_Click(object sender, System.EventArgs e)
	{
		string result = String.Empty;
		// получаем буфер данных
		dataCollection = device.GetBufferedData();
		
		// если есть данные - отображаем
		if(dataCollection != null)
		{				
			foreach (BufferedData d in dataCollection)
			{
				// выводим смещение и код
				result += String.Format(" 0x{0:X2}=0x{1:X2} ", d.Offset, d.Data);
			}								
		}
		tbResult.Text = result;

	}

}
