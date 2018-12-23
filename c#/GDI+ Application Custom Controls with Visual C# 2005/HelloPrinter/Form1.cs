using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace HelloPrinter
{
    
    public partial class Form1 : Form
    {
        PrintDocument _prtDoc = null;
        public Form1()
        {
            InitializeComponent();
        }

       
        private void Form1_Load(object sender, EventArgs e)
        {
            _prtDoc = new PrintDocument();
            _prtDoc.PrintPage += new PrintPageEventHandler(_prtDoc_PrintPage);
        }
        void _prtDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics tapeMeasure = e.PageSettings.PrinterSettings.CreateMeasurementGraphics();
            Single left = e.MarginBounds.Left;
            Single top = e.MarginBounds.Top;
            Single width = e.MarginBounds.Width;
            Single height = e.MarginBounds.Height;

            string message = "Hello Printer!";
            Font messageFont = new Font("Times New Roman", 30, FontStyle.Bold | FontStyle.Italic);
            SizeF messageSize = tapeMeasure.MeasureString(message, messageFont);
            PointF messagePosition = new PointF(((width - messageSize.Width) / 2) + left,
                              ((height - messageSize.Height) / 2) + top);
            e.Graphics.DrawString(message, messageFont, Brushes.Black, messagePosition);
            e.HasMorePages = false;
        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            _prtDoc.Print();
        }



    }
}