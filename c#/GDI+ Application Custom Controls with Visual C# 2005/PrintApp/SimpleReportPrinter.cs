using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;


namespace PrintApp
{
    public class SimpleReportPrinter
    {
        Image _header = null;
        string _text = null;
        int _pageNumber = 0;
        PrintDocument _prtdoc = null;
        TextDispenser _textDisp = null;
        public SimpleReportPrinter(Image header, string text, Font fnt)
        {
            _header = (Image)(header.Clone());
            _text = text;
            _prtdoc = new PrintDocument();
            _prtdoc.PrintPage += new PrintPageEventHandler(_prtdoc_PrintPage);
            _textDisp = new TextDispenser(_text, fnt);
        }
        public void Print(bool hardcopy)
        {
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
            //draw the header image
            if (_pageNumber++ == 0)
            {
                e.Graphics.DrawImage(_header, x, y);
                y += _header.Height + 30;
            }
            RectangleF mainTextArea = RectangleF.FromLTRB(x, y, e.MarginBounds.Right, e.MarginBounds.Bottom);
            //draw the main part of the report
            if (_textDisp.DrawText(e.Graphics, e.PageSettings.PrinterSettings.CreateMeasurementGraphics(), mainTextArea, Brushes.Black))
            {
                e.HasMorePages = false; //the end has been reached
                _pageNumber = 0;
            }
            else
                e.HasMorePages = true;
            //watermark
            e.Graphics.TranslateTransform(200, 200);
            e.Graphics.RotateTransform(e.PageSettings.Landscape ? 30 : 60);
            e.Graphics.DrawString("CONFIDENTIAL", new Font("Courier New", 75, FontStyle.Bold), new SolidBrush(Color.FromArgb(64, Color.Black)), 0, 0);
        }

    }
}
