using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CustomControls_ClassLibrary
{
    public partial class ThirdControl : UserControl
    {
        public ThirdControl()
        {
            InitializeComponent();
        }
        
	protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.DrawEllipse(Pens.Green, ClientRectangle);
            Font stringFont = new Font("Arial", 12);
            e.Graphics.DrawString("First custom control in the Class Library. It is named ThirdControl for this example, but 	   is actually the first one in the CustomControls_ClassLibrary.", stringFont, Brushes.GreenYellow, ClientRectangle);
            base.OnPaint(e);
        }

    }
}
