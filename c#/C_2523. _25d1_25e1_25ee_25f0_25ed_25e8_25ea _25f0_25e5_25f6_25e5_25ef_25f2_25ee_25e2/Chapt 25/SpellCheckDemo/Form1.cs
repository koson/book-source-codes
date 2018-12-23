using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;
using Application = Microsoft.Office.Interop.Word.Application;

namespace SpellCheckDemo
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : Form
	{
		private TextBox textBox1;
		private Button button1;
		private Label label1;
		private Container components = null;

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
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(40, 40);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBox1.Size = new System.Drawing.Size(344, 136);
			this.textBox1.TabIndex = 0;
			this.textBox1.Text = "";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(392, 40);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(96, 23);
			this.button1.TabIndex = 1;
			this.button1.Text = "Check Spelling";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(40, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(336, 16);
			this.label1.TabIndex = 2;
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(496, 205);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.textBox1);
			this.Name = "Form1";
			this.Text = "SpellCheckDemo";
			this.ResumeLayout(false);

		}
		#endregion

		[STAThread]
		static void Main() 
		{
			System.Windows.Forms.Application.Run(new Form1());
		}

		private void button1_Click(object sender, EventArgs e)
		{
			string input = textBox1.Text;
			label1.Text = fSpellCheck(ref input);
			textBox1.Text = input;
		}

		public string fSpellCheck(ref string text)
		{
			string result = string.Empty;

			int iErrorCount = 0;
			Application  app = new Application();				
			if (text.Length > 0)
			{
				app.Visible=false; 

				object template=Missing.Value; 
				object newTemplate=Missing.Value; 
				object documentType=Missing.Value; 
				object visible=true; 
				object optional = Missing.Value;
			
				_Document doc = app.Documents.Add(ref template, ref newTemplate, ref documentType, ref visible); 
				doc.Words.First.InsertBefore (text); 
				ProofreadingErrors we =  doc.SpellingErrors;
				iErrorCount = we.Count;

				doc.CheckSpelling( ref optional, ref optional, ref optional, ref optional, 
					ref optional, ref optional, ref optional, 
					ref optional, ref optional, ref optional, ref optional, ref optional); 
	
				if (iErrorCount == 0)
					result = "Нет ошибок";
				else 
					result = string.Format("Исправлено {0} ошибок.", iErrorCount);
				object first=0; 
				object last=doc.Characters.Count -1; 			
			
				text = doc.Range(ref first, ref last).Text; 				
			}
			object saveChanges = false; 
			object originalFormat = Missing.Value; 
			object routeDocument = Missing.Value; 
			app.Quit(ref saveChanges, ref originalFormat, ref routeDocument); 

			return result;
		}
	}
}
