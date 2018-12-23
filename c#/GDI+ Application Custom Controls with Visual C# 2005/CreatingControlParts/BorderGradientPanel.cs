using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace CreatingControlParts
{
    public partial class BorderGradientPanel : Control
    {
      /*  public BorderGradientPanel()
        {
            InitializeComponent();
        }*/
        private Border3DStyle borderStyle = Border3DStyle.Sunken;
        private Color startColor = SystemColors.Control;
        private Color endColor = SystemColors.Control;
        public Color EndColor
        {
            get
            {
                return endColor;
            }
            set
            {
                if (endColor != value)
                {
                    endColor = value;
                    Invalidate();
                }
            }
        }
        public Color StartColor
        {
            get
            {
                return startColor;
            }
            set
            {
                if (startColor != value)
                {
                    startColor = value;
                    Invalidate();
                }
            }
            
        }
        public Border3DStyle BorderStyle
        {
            get
            {
                return borderStyle;
            }
            set
            {
                if (borderStyle != value)
                {
                    borderStyle = value;
                    Invalidate();
                }
            }
        }
        public BorderGradientPanel()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            LinearGradientBrush brush = new LinearGradientBrush(new Point(0, 0), new Point(0, Height), startColor, endColor);
            e.Graphics.FillRectangle(brush, ClientRectangle);
            ControlPaint.DrawBorder3D(e.Graphics, ClientRectangle, borderStyle);
            brush.Dispose();
        }



    }
}
