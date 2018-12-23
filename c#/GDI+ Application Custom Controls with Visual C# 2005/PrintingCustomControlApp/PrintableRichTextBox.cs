using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace PrintingCustomControlApp1
{
    public partial class PrintableRichTextBox : RichTextBox
    {
        public PrintableRichTextBox()
        {
            InitializeComponent();
            _prtdoc = new PrintDocument();
            _prtdoc.PrintPage += new PrintPageEventHandler(_prtdoc_PrintPage);

        }
        string _text = null;
        int _pageNumber = 0;
        int _start = 0;
        PrintDocument _prtdoc = null;

        private bool DrawText(Graphics target, Graphics measurer, RectangleF r, Brush brsh)
        {
            if (r.Height < this.Font.Height)
                throw new ArgumentException("The rectangle is not tall enough to fit a single line of text inside.");
            int charsFit = 0;
            int linesFit = 0;
            int cut = 0;
            string temp = _text.Substring(_start);
            StringFormat format = new StringFormat(StringFormatFlags.FitBlackBox | StringFormatFlags.LineLimit);
            //measure how much of the string we can fit into the rectangle
            measurer.MeasureString(temp, this.Font, r.Size, format, out charsFit, out linesFit);
            cut = BreakText(temp, charsFit);
            if (cut != charsFit)
                temp = temp.Substring(0, cut);
            bool h = true;
            h &= true;
            target.DrawString(temp.Trim(' '), this.Font, brsh, r, format);
            _start += cut;
            if (_start == _text.Length)
            {
                _start = 0; //reset the location so we can repeat the document
                return true; //finished printing
            }
            else
                return false;
        }
        private static int BreakText(string text, int approx)
        {
            if (approx == 0)
                throw new ArgumentException();
            if (approx < text.Length)
            {
                //are we in the middle of a word?
                if (char.IsLetterOrDigit(text[approx]) && char.IsLetterOrDigit(text[approx - 1]))
                {
                    int temp = text.LastIndexOf(' ', approx, approx + 1);
                    if (temp >= 0)
                        return temp;
                }
            }
            return approx;
        }
        public void Print(bool hardcopy)
        {
            _text = this.Text;
            //create a PrintDialog based on the PrintDocument
            PrintDialog pdlg = new PrintDialog();
            pdlg.Document = _prtdoc;
            //show the PrintDialog
            if (pdlg.ShowDialog() == DialogResult.OK)
            {
                //create a PageSetupDialog based on the PrintDocument and PrintDialog
                PageSetupDialog psd = new PageSetupDialog();
                psd.EnableMetric = true; //Ensure all dialog measurements are in metric
                psd.Document = pdlg.Document;
                //show the PageSetupDialog
                if (psd.ShowDialog() == DialogResult.OK)
                {
                    //apply the settings of both dialogs
                    _prtdoc.DefaultPageSettings = psd.PageSettings;
                    //decide what action to take
                    if (hardcopy)
                    {
                        //actually print hardcopy
                        _prtdoc.Print();
                    }
                    else
                    {
                        //preview onscreen instead
                        PrintPreviewDialog prvw = new PrintPreviewDialog();
                        prvw.Document = _prtdoc;
                        prvw.ShowDialog();
                    }
                }
            }
        }
        private void _prtdoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.Clip = new Region(e.MarginBounds);

            //this method does all our printing work
            Single x = e.MarginBounds.Left;
            Single y = e.MarginBounds.Top;

            if (_pageNumber++ == 0)
                y += 30;

            RectangleF mainTextArea = RectangleF.FromLTRB(x, y, e.MarginBounds.Right, e.MarginBounds.Bottom);

            //draw the text
            if (DrawText(e.Graphics, e.PageSettings.PrinterSettings.CreateMeasurementGraphics(), mainTextArea, Brushes.Black))
            {
                e.HasMorePages = false; //the end has been reached
                _pageNumber = 0;
            }
            else
                e.HasMorePages = true;
        }



    }
}
