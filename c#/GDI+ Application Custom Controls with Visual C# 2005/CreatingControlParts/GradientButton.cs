using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CreatingControlParts
{
    public partial class GradientButton : BorderGradientPanel
    {
        public GradientButton()
        {
            UpdateAppearance();
            InitializeComponent();
        }
        private bool clicked = false;
        private void UpdateAppearance()
        {
            if (clicked)
            {
                StartColor = SystemColors.Control;
                EndColor = SystemColors.ControlLight;
                BorderStyle = Border3DStyle.Sunken;
            }
            else
            {
                StartColor = SystemColors.ControlLight;
                EndColor = SystemColors.Control;
                BorderStyle = Border3DStyle.Raised;
            }

        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                clicked = true;
                UpdateAppearance();
            }
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                clicked = false;
                UpdateAppearance();
            }
        }
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            Brush foreBrush = new SolidBrush(ForeColor);
            SizeF size = pe.Graphics.MeasureString(Text, Font);
            PointF pt = new PointF((Width - size.Width) / 2, (Height - size.Height) / 2);
            if (clicked)
            {
                pt.X += 2;
                pt.Y += 2;
            }
            pe.Graphics.DrawString(Text, Font, foreBrush, pt);
            foreBrush.Dispose();
        }
    }



    
}
