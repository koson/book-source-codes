using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace GradientLabelTest00
{
    public partial class GradientLabel : Control
    {
        private Color backColor2 = SystemColors.ControlLight;
        public GradientLabel()
        {
            InitializeComponent();
        }
        public Color BackColor2
        {
            get
            {
                return backColor2;
            }
            set
            {
                if (backColor2 != value)
                {
                    backColor2 = value;
                    Invalidate();
                }
            }
        }

        private void GradientLabel_Paint(object sender, PaintEventArgs e)
        {
        }
            protected override void OnPaint(PaintEventArgs e)
            {
                         
                base.OnPaint(e);
                LinearGradientBrush brush = new LinearGradientBrush(new Point(0, 0), new Point(0, Height), BackColor, BackColor2);
                e.Graphics.FillRectangle(brush, ClientRectangle);
                Brush foreBrush = new SolidBrush(ForeColor);
                SizeF textSize = e.Graphics.MeasureString(Text, Font);
                e.Graphics.DrawString(Text, Font, foreBrush, (Width - textSize.Width) / 2, (Height - textSize.Height) / 2);
                brush.Dispose();
                foreBrush.Dispose();
            }

        

    }
}
