using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using System.Data;
using SpeechLib;

namespace SAPI5
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button btnExit;
		private System.Windows.Forms.Button btnSpeak;
		private System.Windows.Forms.CheckBox cbSaveToWave;
		private System.Windows.Forms.ComboBox cbVoices;
		private System.Windows.Forms.TrackBar Volume;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TrackBar Rate;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		SpVoice m_voice;

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
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.cbSaveToWave = new System.Windows.Forms.CheckBox();
			this.btnSpeak = new System.Windows.Forms.Button();
			this.btnExit = new System.Windows.Forms.Button();
			this.cbVoices = new System.Windows.Forms.ComboBox();
			this.Volume = new System.Windows.Forms.TrackBar();
			this.label1 = new System.Windows.Forms.Label();
			this.Rate = new System.Windows.Forms.TrackBar();
			this.label2 = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Volume)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Rate)).BeginInit();
			this.SuspendLayout();
			// 
			// textBox1
			// 
			this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBox1.HideSelection = false;
			this.textBox1.Location = new System.Drawing.Point(0, 0);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(504, 420);
			this.textBox1.TabIndex = 2;
			this.textBox1.Text = "";
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.Rate);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.Volume);
			this.panel1.Controls.Add(this.cbVoices);
			this.panel1.Controls.Add(this.cbSaveToWave);
			this.panel1.Controls.Add(this.btnSpeak);
			this.panel1.Controls.Add(this.btnExit);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 292);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(504, 128);
			this.panel1.TabIndex = 4;
			// 
			// cbSaveToWave
			// 
			this.cbSaveToWave.Location = new System.Drawing.Point(88, 8);
			this.cbSaveToWave.Name = "cbSaveToWave";
			this.cbSaveToWave.TabIndex = 5;
			this.cbSaveToWave.Text = "SaveToWave";
			// 
			// btnSpeak
			// 
			this.btnSpeak.Location = new System.Drawing.Point(8, 8);
			this.btnSpeak.Name = "btnSpeak";
			this.btnSpeak.TabIndex = 4;
			this.btnSpeak.Tag = "0";
			this.btnSpeak.Text = "SPEAK";
			this.btnSpeak.Click += new System.EventHandler(this.btnSpeak_Click);
			// 
			// btnExit
			// 
			this.btnExit.Location = new System.Drawing.Point(424, 8);
			this.btnExit.Name = "btnExit";
			this.btnExit.TabIndex = 1;
			this.btnExit.Text = "Exit";
			this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
			// 
			// cbVoices
			// 
			this.cbVoices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbVoices.Location = new System.Drawing.Point(240, 8);
			this.cbVoices.Name = "cbVoices";
			this.cbVoices.Size = new System.Drawing.Size(121, 21);
			this.cbVoices.TabIndex = 6;
			// 
			// Volume
			// 
			this.Volume.Location = new System.Drawing.Point(8, 64);
			this.Volume.Maximum = 100;
			this.Volume.Name = "Volume";
			this.Volume.Size = new System.Drawing.Size(184, 42);
			this.Volume.TabIndex = 7;
			this.Volume.TickFrequency = 10;
			this.Volume.Scroll += new System.EventHandler(this.Volume_Scroll);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 40);
			this.label1.Name = "label1";
			this.label1.TabIndex = 8;
			this.label1.Text = "Volume:";
			// 
			// Rate
			// 
			this.Rate.Location = new System.Drawing.Point(208, 56);
			this.Rate.Maximum = 100;
			this.Rate.Name = "Rate";
			this.Rate.Size = new System.Drawing.Size(184, 42);
			this.Rate.TabIndex = 9;
			this.Rate.TickFrequency = 10;
			this.Rate.Scroll += new System.EventHandler(this.Rate_Scroll);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(208, 40);
			this.label2.Name = "label2";
			this.label2.TabIndex = 10;
			this.label2.Text = "Speed:";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(504, 420);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.textBox1);
			this.Name = "Form1";
			this.Text = "Speak for me!";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Volume)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Rate)).EndInit();
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

		private void btnSpeak_Click(object sender, System.EventArgs e)
		{
			if ((Convert.ToInt32(btnSpeak.Tag) == 0) && (sender != null))
			{
				btnSpeak.Tag  = 1;
				btnSpeak.Text = "STOP";

				try 
				{
					if (cbVoices.Text.Length >0)
					{
						m_voice.Voice = m_voice.GetVoices(string.Format("Name={0}", cbVoices.Text), "Language=409").Item(0);
					}

					if (cbSaveToWave.Checked)
					{
						SaveFileDialog sfd = new SaveFileDialog();
						sfd.Filter = "All files (*.*)|*.*|wav files (*.wav)|*.wav";
						sfd.Title = "Save to a wave file";
						sfd.FilterIndex = 2;
						sfd.RestoreDirectory = true;
						if (sfd.ShowDialog()== DialogResult.OK) 
						{
							SpeechStreamFileMode SpFileMode = SpeechStreamFileMode.SSFMCreateForWrite;
							SpFileStream SpFileStream = new SpFileStream();
							SpFileStream.Open(sfd.FileName, SpFileMode, false);
							m_voice.AudioOutputStream = SpFileStream;
							m_voice.Speak(textBox1.Text, SpeechVoiceSpeakFlags.SVSFlagsAsync);
							m_voice.WaitUntilDone(Timeout.Infinite);
							SpFileStream.Close();
						}
					}
					else
					{
						m_voice.Speak(textBox1.Text, SpeechVoiceSpeakFlags.SVSFlagsAsync);
					}
				}
				catch
				{
					MessageBox.Show("Speak error");
				}
			}
			else 
			{
				btnSpeak.Tag = 0;
				btnSpeak.Text = "SPEAK";
				m_voice.Speak(null, (SpeechVoiceSpeakFlags)2);
				m_voice.Resume();
			}
		
		}

		private void btnExit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
			m_voice = new SpVoice();
			m_voice.EndStream+=new _ISpeechVoiceEvents_EndStreamEventHandler(m_voice_EndStream);

			foreach(ISpeechObjectToken t in m_voice.GetVoices("",""))
			{
				cbVoices.Items.Add(t.GetAttribute("Name"));
			}
		}

		private void Volume_Scroll(object sender, System.EventArgs e)
		{
			m_voice.Volume = Volume.Value;
		}

		private void Rate_Scroll(object sender, System.EventArgs e)
		{
			m_voice.Rate   = Rate.Value;
		}

		private void m_voice_EndStream(int StreamNumber, object StreamPosition)
		{
			btnSpeak_Click(null, null);
		}
	}
}
