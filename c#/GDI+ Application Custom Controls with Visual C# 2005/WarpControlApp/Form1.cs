using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WarpControlApp1
{
    public partial class Form1 : Form
    {
        Image img = null;
        public Form1()
        {
            InitializeComponent();
        }
        private Image CreatePicture()
        {
            // Create a new Bitmap object, 50 x 50 pixels in size
            Image canvas = new Bitmap(50, 50);
            // create an object that will do the drawing operations
            Graphics artist = Graphics.FromImage(canvas);
            // draw a few shapes on the canvas picture
            artist.Clear(Color.Lime);
            artist.FillEllipse(Brushes.Red, 3, 30, 30, 30);
            artist.DrawBezier(new Pen(Color.Blue, 3), 0, 0, 40, 15, 10, 35, 50, 50);
            // now the drawing is done, we can discard the artist object
            artist.Dispose();
            //return the picture
            return canvas;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            img = CreatePicture();
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            Random rand = new Random(); // randomises our drawing parameters
            // set up all our parameters first before calling DrawWarpedPicture.
            Graphics target = this.CreateGraphics(); // draw onto the form's surface
            PointF pivotOnImage = new PointF(img.Width / 2, img.Height / 2);
            PointF pivotOnTarget = new PointF((Single)e.X, (Single)e.Y);
            double rotate = rand.NextDouble() * 360;
            double scaleFactor = 0.2 + (rand.NextDouble() * 2);
            SizeF skewing = new SizeF(rand.Next(-20, 21), rand.Next(-20, 21));
            // draw it!
            ImageWarper warper = new ImageWarper();
            warper.DrawWarpedPicture(target, img, pivotOnImage, pivotOnTarget, rotate, scaleFactor, skewing);

        }
    }
}

        
