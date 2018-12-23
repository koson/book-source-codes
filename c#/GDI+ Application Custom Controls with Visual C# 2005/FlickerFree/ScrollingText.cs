using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FlickerFree
{
    public partial class ScrollingText : UserControl
    {
        private string text;
        private int scrollAmount = 10;
        private int position = 10;

        public ScrollingText()
        {
            InitializeComponent();
        }

        private void ScrollingText_Load(object sender, EventArgs e)
        {
            this.ResizeRedraw = true;
            if (!this.DesignMode)
            {
                scrollTimer.Enabled = true;
            }

        }

        private void scrollTimer_Tick(object sender, EventArgs e)
        {
            position += scrollAmount;
            this.Invalidate();

        }
        [Browsable(true),
    DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
                this.Invalidate();
            }
        }

        public int ScrollTimeInterval
        {
            get
            {
                return scrollTimer.Interval;
            }
            set
            {
                scrollTimer.Interval = value;
            }
        }

        public int ScrollPixelAmount
        {
            get
            {
                return scrollAmount;
            }
            set
            {
                scrollAmount = value;
            }
        }

        private void ScrollingText_Paint(object sender, PaintEventArgs e)
        {

        }
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            // to avoid a design-time error we need to add the following line
            if (e.ClipRectangle.Width == 0)
            {
                return;
            }

            base.OnPaint(e);
            if (position > this.Width)
            {
                // Reset the text to scroll back onto the control.
                position = -(int)e.Graphics.MeasureString(text, this.Font).Width;
            }

            // Create the drawing area in memory.
            // Double buffering is used to prevent flicker.
            Bitmap bufl = new Bitmap(e.ClipRectangle.Width, e.ClipRectangle.Height);
            Graphics g = Graphics.FromImage(bufl);
            g.FillRectangle(new SolidBrush(this.BackColor), e.ClipRectangle);
            g.DrawString(text, this.Font, new SolidBrush(this.ForeColor), position, 0);
            // Render the finished image on the form.  
            e.Graphics.DrawImageUnscaled(bufl, 0, 0);
            g.Dispose();
        }


    }
}
