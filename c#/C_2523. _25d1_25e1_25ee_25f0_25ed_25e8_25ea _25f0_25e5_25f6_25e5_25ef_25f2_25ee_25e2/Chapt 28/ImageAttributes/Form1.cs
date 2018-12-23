using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace WindowsApplication9
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.PictureBox pictureBox1;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Form1));
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(0, 0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(292, 240);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(104, 248);
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
			this.Controls.Add(this.pictureBox1);
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

		private System.Drawing.Imaging.ColorMatrix CreateSepiaMatrix() 
		{
			// Задаем преобразование
			// New Red   = R*.1 + G*.4 + B*.7
			// New Green = R*.2 + G*.5 + B*.8
			// New Blue  = R*.3 + G*.6 + B*.9
			return new System.Drawing.Imaging.ColorMatrix(new float[][]{
					new float[] {0.1f, 0.4f, 0.7f, 0, 0},
					new float[] {0.2f, 0.5f, 0.8f, 0, 0},
					new float[] {0.3f, 0.6f, 0.9f, 0, 0},
					new float[] {   0,    0,    0, 1, 0},
					new float[] {   0,    0,    0, 0, 1}
			});
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			// Получить изображение (jpg или gif)
			Image img = pictureBox1.Image;

			// Создать объект атрибутов изображения
			System.Drawing.Imaging.ImageAttributes imageAttrs = 
				new System.Drawing.Imaging.ImageAttributes();
			// Создать атрибуты по матрице преобразования
			imageAttrs.SetColorMatrix(CreateSepiaMatrix());
			// Нарисовать новое изображение с помощью преобразования
			using(Graphics g = Graphics.FromImage(img))
			{
				g.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height), 
					0, 0, img.Width, img.Height, GraphicsUnit.Pixel, imageAttrs);
			}
			// Обновить рисунок
			pictureBox1.Invalidate();
		}
	}
}
