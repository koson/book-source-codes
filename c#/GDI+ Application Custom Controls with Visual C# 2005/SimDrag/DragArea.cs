using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Design;

namespace SimDrag
{
    public partial class DragArea : UserControl
    {
        private int clickOffsetX, clickOffsetY;
        public DragArea()
        {
            InitializeComponent();
        }

        private void draggingIcon_LocationChanged(object sender, EventArgs e)
        {
            //lblX.Text = "X: " + draggingIcon.Location.X.ToString();
            //lblY.Text = "Y: " + draggingIcon.Location.Y.ToString();
            lblX.Text = "X: " + draggingIcon.Location.X.ToString();
            lblY.Text = "Y: " + draggingIcon.Location.Y.ToString();
            // fire the event if anyone's listening)
            if (LocationChanged != null)
            {
                LocationChangedEventArgs lcea =
                  new LocationChangedEventArgs(draggingIcon.Location.X, draggingIcon.Location.Y);
                LocationChanged(lcea);
            }


        }

        private void draggingIcon_MouseDown(object sender, MouseEventArgs e)
        {
            if (chkDragging.Checked)
            {
                clickOffsetX = e.X;
                clickOffsetY = e.Y;
            }

        }

        private void draggingIcon_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && chkDragging.Checked)
            {
                draggingIcon.Left = e.X + draggingIcon.Left - clickOffsetX;
                draggingIcon.Top = e.Y + draggingIcon.Top - clickOffsetY;
            }

        }
        private void ResetPosition()
        {
            draggingIcon.Left = (panelDraggingZone.Width - draggingIcon.Width) / 2;
            draggingIcon.Top = (panelDraggingZone.Height - draggingIcon.Height) / 2;
        }
        public new event LocationChangedEvent LocationChanged;

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetPosition();
        }
        [Browsable(true),
         DefaultValue(typeof(Color)),
         Description("The control background")]

        public Color CanvasColor
        {
            get
            {
                return panelDraggingZone.BackColor;
            }
            set
            {
                panelDraggingZone.BackColor = value;
            }
        }
        [Browsable(true),
     DefaultValue(90),
     Description("Picture size (width and height are equal)"),
     Editor(typeof(PictureSizeUITypeEditor), typeof(UITypeEditor))]
        public Size PictureSize
        {
            get
            {
                return draggingIcon.Size;
            }
            set
            {
                draggingIcon.Size = value;
            }
        }




    }
    public delegate void LocationChangedEvent(LocationChangedEventArgs e);
    public class LocationChangedEventArgs : EventArgs
    {
        int x;
        int y;

        public int X
        {
            get
            {
                return x;
            }
        }

        public int Y
        {
            get
            {
                return y;
            }
        }

        internal LocationChangedEventArgs(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

}
