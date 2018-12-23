using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace CreatingControlParts
{
    public class ScrollArrowButton : ControlPart
    {
        protected override void RenderControl(Graphics g, ButtonState buttonState, CheckState checkState)
        {
            //ControlPaint.DrawScrollButton(g, ClientRectangle, ScrollButton.Up,buttonState);
            ControlPaint.DrawScrollButton(g, ClientRectangle, sButton, buttonState);
        }
        private ScrollButton sButton = ScrollButton.Up;
        public ScrollButton ButtonType
        {
            get
            {
                return sButton;
            }
            set
            {
                if (sButton != value)
                {
                    sButton = value;
                    Invalidate();
                }
            }
        }


    }
}
