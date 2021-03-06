using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace CreatingControlParts
{
    public class ControlPart : Control
    {
        private ButtonState buttonState = ButtonState.Flat;
        private CheckState checkState = CheckState.Unchecked;
        //indicates wheter the mouse is hovering over the control
        protected bool mouseOver = false;
        public ControlPart()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
        }
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            buttonState = ButtonState.Normal;
            mouseOver = true;
            Invalidate(true);
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            buttonState = ButtonState.Flat;
            mouseOver = false;
            Invalidate(true);
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            this.Focus();
            if (!(e.Button == MouseButtons.Left)) return;
            buttonState = ButtonState.Pushed;
            switch (checkState)
            {
                case CheckState.Checked: checkState = CheckState.Unchecked; break;
                case CheckState.Unchecked: checkState = CheckState.Checked; break;
                case CheckState.Indeterminate: checkState = CheckState.Unchecked; break;
            }
            Invalidate(true);
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (!((e.Button & MouseButtons.Left) == MouseButtons.Left)) return;
            buttonState = ButtonState.Normal;
            Invalidate(true);
        }
        protected virtual void RenderControl(Graphics g, ButtonState buttonState, CheckState checkState)
        {
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            RenderControl(e.Graphics, buttonState, checkState);
        }

    }
}
