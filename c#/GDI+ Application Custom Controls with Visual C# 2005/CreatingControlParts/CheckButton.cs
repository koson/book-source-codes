using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace CreatingControlParts
{
    public class CheckButton : ControlPart
    {
        protected override void RenderControl(Graphics g, ButtonState buttonState, CheckState checkState)
        {
            ButtonState bstate = buttonState;
            switch (checkState)
            {
                case CheckState.Checked: bstate = ButtonState.Checked; break;
                case CheckState.Indeterminate: bstate = ButtonState.All; break;
            }
            ControlPaint.DrawCheckBox(g, ClientRectangle, bstate);
        }

    }
}
