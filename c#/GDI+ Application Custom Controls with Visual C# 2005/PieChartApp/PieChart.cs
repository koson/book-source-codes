using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Printing;

namespace PieChartApp
{
    public partial class PieChart : UserControl
    {
        int totalCount;
        ArrayList mySlices;
        PrintDocument pieChartPrintDoc = null;

        public PieChart()
        {
            InitializeComponent();
            pieChartPrintDoc = new PrintDocument();
            pieChartPrintDoc.PrintPage += new PrintPageEventHandler(_pieChartPrintDoc_PrintPage);

        }
        public ArrayList SetArray
        {
            get
            {
                return mySlices;
            }
            set
            {
                if (mySlices != value)
                    mySlices = value;
                Invalidate();
            }
        }
        private void SetValues()
        {
            totalCount = 0;
            if (mySlices != null)
            {
                foreach (Slice slice in mySlices)
                    totalCount += slice.GetSliceRange();
            }
            //mySlicesPercent.Clear();
        }
        public bool AddSlice(Slice slice)
        {
            bool isThisSlice = false;
            if (mySlices == null)
            {
                mySlices = new ArrayList();
                mySlices.Add(slice);
                return true;
            }
            foreach (Slice sliceTemp in mySlices)
            {
                if (sliceTemp.GetSliceName() == slice.GetSliceName())
                    isThisSlice = true;
            }
            if (isThisSlice == false)
            {
                mySlices.Add(slice);
                Invalidate();
                return true;
            }
            return false;
        }

        public bool RemoveSlice(string sliceName)
        {
            bool isThisSliceName = false;
            foreach (Slice sliceTemp in mySlices)
            {
                if (sliceName == sliceTemp.GetSliceName())
                {
                    mySlices.Remove(sliceTemp);
                    isThisSliceName = true;
                    break;
                }
            }
            if (isThisSliceName)
                Invalidate();
            return isThisSliceName;
        }

        private void PieChart_Paint(object sender, PaintEventArgs e)
        {
            {
                Pen penCircle = Pens.Black;
                Pen penLine = Pens.BlanchedAlmond;
                SetValues();
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                e.Graphics.DrawEllipse(penCircle, new Rectangle(1, 1, this.Width / 2 - 5, this.Width / 2 - 5));
                if (mySlices != null)
                {
                    int actualCount = 0;
                    // draw each slice
                    foreach (Slice slice in mySlices)
                    {
                        Pen penSlice = new Pen(slice.GetSliceColor());
                        int actualRangeSlice = slice.GetSliceRange();
                        int startAngle = (int)((actualCount / (double)totalCount) * 360);
                        int widthAngle = (int)(((actualRangeSlice) / (double)totalCount) * 360) + 1;
                        Brush br = new SolidBrush(slice.GetSliceColor());
                        e.Graphics.FillPie(br, new Rectangle(1, 1, this.Width / 2 - 5, this.Width / 2 - 5), startAngle, widthAngle);
                        e.Graphics.DrawPie(penCircle, new Rectangle(1, 1, this.Width / 2 - 5, this.Width / 2 - 5), startAngle, widthAngle);
                        actualCount += slice.GetSliceRange();
                    }
                    // draw the text within the legend
                    string itemName;
                    int itemFontSize = 64;
                    Font itemFont = new Font("SansSerif", itemFontSize);
                    StringFormat itemFormatName = new StringFormat(StringFormatFlags.NoClip);
                    itemFormatName.Alignment = StringAlignment.Near;
                    itemFormatName.LineAlignment = StringAlignment.Near;
                    int verticalPosition = 50;
                    // check if the text fits the legend
                    // if not -> modify the font
                    foreach (Slice slice in mySlices)
                    {
                        itemName = "  ";
                        itemName += slice.GetSliceName();
                        itemName += " - " + string.Format("{0:f}", (double)(slice.GetSliceRange() / (double)totalCount * 100)) + "%";
                        SizeF itemSize = e.Graphics.MeasureString(itemName, itemFont);
                        Point position = new Point(this.Width / 2 + 40, verticalPosition);
                        while ((e.Graphics.MeasureString(itemName, itemFont).Width > (this.Width / 2 - 40)))
                        {
                            if (itemFontSize > 4)
                                itemFont = new Font("SansSerif", itemFontSize--);
                            else
                                return;
                        }
                        while ((50 + mySlices.Count * (e.Graphics.MeasureString(itemName, itemFont).Height + 5)) > (this.Height))
                        {
                            if (itemFontSize > 4)
                                itemFont = new Font("SansSerif", itemFontSize--);
                            else
                                return;
                        }
                    }
                    verticalPosition = 50;
                    // draw the legend outline
                    Font legendTitleFont = new Font("SansSerif", itemFontSize + 5);
                    e.Graphics.DrawString("Legend", legendTitleFont, Brushes.Black, new Point(this.Width / 2 + 20, 10));
                    int legendHeight = (int)(e.Graphics.MeasureString("Legend", legendTitleFont).Height) * 2;
                    // draw items text and colored rectangle
                    foreach (Slice slice in mySlices)
                    {
                        itemName = "  ";
                        itemName += slice.GetSliceName();
                        itemName += " - " + string.Format("{0:f}", (double)(slice.GetSliceRange() / (double)totalCount * 100)) + "%";
                        SizeF itemSize = e.Graphics.MeasureString(itemName, itemFont);
                        Point position = new Point(this.Width / 2 + 40, verticalPosition);
                        e.Graphics.DrawRectangle(Pens.Black, new Rectangle(this.Width / 2 + 20, verticalPosition, 15, (int)itemSize.Height));
                        e.Graphics.FillRectangle(new SolidBrush(slice.GetSliceColor()), new Rectangle(this.Width / 2 + 20, verticalPosition, 15, (int)itemSize.Height));
                        e.Graphics.DrawString(itemName, itemFont, Brushes.Black, position, itemFormatName);
                        verticalPosition += (int)itemSize.Height + 5;
                    }
                    // draw the reactangle to include the legend
                    e.Graphics.DrawRectangle(Pens.Black, new Rectangle(this.Width / 2 + 5, 5, this.Width / 2 - 10, verticalPosition));

                    string stringName = "";
                    Font fontName = new Font("SansSerif", 8);
                    StringFormat formatName = new StringFormat(StringFormatFlags.NoClip);
                    formatName.Alignment = StringAlignment.Center;
                    formatName.LineAlignment = StringAlignment.Center;
                    Point pointName = new Point(this.Width / 8, 0);
                    double actualAngle = 0;
                    e.Graphics.TranslateTransform((float)(this.Width / 4.0), (float)(this.Width / 4.0));
                    // draw the text and percent for each slice
                    foreach (Slice slice in mySlices)
                    {
                        Pen penSlice = new Pen(slice.GetSliceColor());
                        double actualRangeSlice = slice.GetSliceRange();
                        double rotateAngle = ((((actualRangeSlice) / (double)totalCount) * 360)) / 2.0;
                        Brush br = new SolidBrush(Color.FromArgb(0, 0, 0));
                        e.Graphics.RotateTransform((float)(rotateAngle + actualAngle));
                        stringName = "";
                        stringName = string.Format("{0:f}", (double)(slice.GetSliceRange() / (double)totalCount * 100));
                        stringName += "% " + slice.GetSliceName();
                        if (e.Graphics.MeasureString(stringName, fontName).Width < (Width / 4))
                            e.Graphics.DrawString(stringName, fontName, br, pointName, formatName);
                        actualAngle = rotateAngle;
                    }
                }

                e.Graphics.Dispose();
            }
        }


        public Slice GetSlice(string sliceName)
        {
            foreach (Slice sliceTemp in mySlices)
            {
                if (sliceName == sliceTemp.GetSliceName())
                {
                    return sliceTemp;
                }
            }
            // if there is no slice by a given name this function
            // will return a null text, zero range, white slice
            return new Slice("", 0, Color.FromArgb(255, 255, 255));
        }

        public void Print(bool hardcopy)
        {
            //create a PrintDialog based on the PrintDocument
            PrintDialog pdlg = new PrintDialog();
            pdlg.Document = pieChartPrintDoc;
            //show the PrintDialog
            if (pdlg.ShowDialog() == DialogResult.OK)
            {
                //create a PageSetupDialog based on the PrintDocument and PrintDialog
                PageSetupDialog psd = new PageSetupDialog();
                psd.EnableMetric = true; //Ensure all dialog measurements are in metric
                psd.Document = pdlg.Document;
                psd.PageSettings.Landscape = true; //Ensure landscape view
                //show the PageSetupDialog
                if (psd.ShowDialog() == DialogResult.OK)
                {
                    //apply the settings of both dialogs
                    pieChartPrintDoc.DefaultPageSettings = psd.PageSettings;
                    //decide what action to take
                    if (hardcopy)
                    {
                        //actually print hardcopy
                        pieChartPrintDoc.Print();
                    }
                    else
                    {
                        //preview onscreen instead
                        PrintPreviewDialog prvw = new PrintPreviewDialog();
                        prvw.Document = pieChartPrintDoc;
                        prvw.ShowDialog();
                    }
                }
            }
        }
        private void _pieChartPrintDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            Pen penCircle = Pens.Black;
            Pen penLine = Pens.BlanchedAlmond;

            e.Graphics.Clip = new Region(e.MarginBounds);
            Single x = e.MarginBounds.Left;
            Single y = e.MarginBounds.Top;
            int leftMargin = (int)x;
            int topMargin = (int)y;

            RectangleF mainTextArea = RectangleF.FromLTRB(x, y, e.MarginBounds.Right, e.MarginBounds.Bottom);
            e.HasMorePages = false;
            if ((this.Height > mainTextArea.Height) || (this.Width > mainTextArea.Width))
            {
                MessageBox.Show("The control doesn't fit in the page. Resize the control then try again printing");
                return;
            }

            Pen contourPen = new Pen(Color.FromArgb(0, 0, 0), 2);
            e.Graphics.DrawRectangle(contourPen, leftMargin, topMargin, this.Width, this.Height);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.DrawEllipse(penCircle, new Rectangle(leftMargin + 1, topMargin + 1, this.Width / 2 - 5, this.Width / 2 - 5));
            if (mySlices != null)
            {
                int actualCount = 0;
                // draw each slice
                foreach (Slice slice in mySlices)
                {
                    Pen penSlice = new Pen(slice.GetSliceColor());
                    int actualRangeSlice = slice.GetSliceRange();
                    int startAngle = (int)((actualCount / (double)totalCount) * 360);
                    int widthAngle = (int)(((actualRangeSlice) / (double)totalCount) * 360) + 1;
                    Brush br = new SolidBrush(slice.GetSliceColor());
                    e.Graphics.FillPie(br, new Rectangle(leftMargin + 1, topMargin + 1, this.Width / 2 - 5, this.Width / 2 - 5), startAngle, widthAngle);
                    e.Graphics.DrawPie(penCircle, new Rectangle(leftMargin + 1, topMargin + 1, this.Width / 2 - 5, this.Width / 2 - 5), startAngle, widthAngle);
                    actualCount += slice.GetSliceRange();
                }

                // draw the text within the legend
                string itemName;
                int itemFontSize = 64;
                Font itemFont = new Font("SansSerif", itemFontSize);
                StringFormat itemFormatName = new StringFormat(StringFormatFlags.NoClip);
                itemFormatName.Alignment = StringAlignment.Near;
                itemFormatName.LineAlignment = StringAlignment.Near;
                int verticalPosition = 50;
                // check if the text fits the legend
                // if not -> modify the font
                foreach (Slice slice in mySlices)
                {
                    itemName = "  ";
                    itemName += slice.GetSliceName();
                    itemName += " - " + string.Format("{0:f}", (double)(slice.GetSliceRange() / (double)totalCount * 100)) + "%";
                    SizeF itemSize = e.Graphics.MeasureString(itemName, itemFont);
                    Point position = new Point(this.Width / 2 + 40 + leftMargin, verticalPosition + topMargin);
                    while ((e.Graphics.MeasureString(itemName, itemFont).Width > (this.Width / 2 - 40)))
                    {
                        if (itemFontSize > 4)
                            itemFont = new Font("SansSerif", itemFontSize--);
                        else
                            return;
                    }

                    while ((50 + mySlices.Count * (e.Graphics.MeasureString(itemName, itemFont).Height + 5)) > (this.Height))
                    {
                        if (itemFontSize > 4)
                            itemFont = new Font("SansSerif", itemFontSize--);
                        else
                            return;
                    }
                }

                verticalPosition = 50;

                // draw the legend title
                Font legendTitleFont = new Font("SansSerif", itemFontSize + 5);
                e.Graphics.DrawString("Legend", legendTitleFont, Brushes.Black, new Point(leftMargin + this.Width / 2 + 20, topMargin + 10));
                int legendHeight = (int)(e.Graphics.MeasureString("Legend", legendTitleFont).Height) * 2 + topMargin;
                // draw items text and colored rectangle
                foreach (Slice slice in mySlices)
                {
                    itemName = "  ";
                    itemName += slice.GetSliceName();
                    itemName += " - " + string.Format("{0:f}", (double)(slice.GetSliceRange() / (double)totalCount * 100)) + "%";
                    SizeF itemSize = e.Graphics.MeasureString(itemName, itemFont);
                    Point position = new Point(leftMargin + this.Width / 2 + 40, topMargin + verticalPosition);
                    e.Graphics.DrawRectangle(Pens.Black, new Rectangle(leftMargin + this.Width / 2 + 20, topMargin + verticalPosition, 15, (int)itemSize.Height));
                    e.Graphics.FillRectangle(new SolidBrush(slice.GetSliceColor()), new Rectangle(leftMargin + this.Width / 2 + 20, topMargin + verticalPosition, 15, (int)itemSize.Height));
                    e.Graphics.DrawString(itemName, itemFont, Brushes.Black, position, itemFormatName);
                    verticalPosition += (int)itemSize.Height + 5;
                }

                // draw the legend outline
                e.Graphics.DrawRectangle(Pens.Black, new Rectangle(leftMargin + this.Width / 2 + 5, topMargin + 5, this.Width / 2 - 10, verticalPosition));

                string stringName = "";
                Font fontName = new Font("SansSerif", 8);
                StringFormat formatName = new StringFormat(StringFormatFlags.NoClip);
                formatName.Alignment = StringAlignment.Center;
                formatName.LineAlignment = StringAlignment.Center;
                Point pointName = new Point(this.Width / 8, 0);
                double actualAngle = 0;
                e.Graphics.TranslateTransform((float)(this.Width / 4.0 + leftMargin), (float)(this.Width / 4.0 + topMargin));
                // draw the text and percent for each slice
                foreach (Slice slice in mySlices)
                {
                    Pen penSlice = new Pen(slice.GetSliceColor());
                    double actualRangeSlice = slice.GetSliceRange();
                    double rotateAngle = ((((actualRangeSlice) / (double)totalCount) * 360)) / 2.0;
                    Brush br = new SolidBrush(Color.FromArgb(0, 0, 0));
                    e.Graphics.RotateTransform((float)(rotateAngle + actualAngle));
                    stringName = "";
                    stringName = string.Format("{0:f}", (double)(slice.GetSliceRange() / (double)totalCount * 100));
                    stringName += "% " + slice.GetSliceName();
                    if (e.Graphics.MeasureString(stringName, fontName).Width < (Width / 4))
                        e.Graphics.DrawString(stringName, fontName, br, pointName, formatName);
                    actualAngle = rotateAngle;
                }
            }
        }




    }
}
