using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PrintApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Image imageHeader = new Bitmap(50, 50);
            Graphics img = Graphics.FromImage(imageHeader);
            img.DrawEllipse(Pens.Black, 0, 0, 45, 45);
            img.DrawString("LOGO", this.Font, Brushes.Black, new PointF(7, 16));
            img.Dispose();
            string textDocument;
            textDocument = "";
            for (int i = 0; i < 60; i++)
            {
                for (int j = 0; j < i; j++)
                    textDocument += " ";
                textDocument += "The quick brown fox jumps over the lazy dog\n";
            }
            SimpleReportPrinter printDocument = new SimpleReportPrinter(imageHeader, textDocument, this.Font);
            printDocument.Print(false);

        }
    }
}