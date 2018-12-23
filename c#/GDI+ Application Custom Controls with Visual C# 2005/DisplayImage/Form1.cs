using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DisplayImage1
{
    public partial class Form1 : Form
    {
        private Image myPicture = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            myPicture = CreatePicture();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            //draw the image onto the picturebox using the supplied graphics object
            e.Graphics.DrawImage(myPicture, 30, 50); //draw the image

        }
        private Image CreatePicture()
        {
            //Create a new Bitmap object, 50 x 50 pixels in size
            Image canvas = new Bitmap(50, 50);
            //create an object that will do the drawing operations
            Graphics artist = Graphics.FromImage(canvas);
            //draw a few shapes on the canvas picture
            artist.Clear(Color.Lime);
            artist.FillEllipse(Brushes.Red, 3, 30, 30, 30);
            artist.DrawBezier(new Pen(Color.Blue, 3), 0, 0, 40, 15, 10, 35, 50, 50);
            //now the drawing is done, we can discard the artist object
            artist.Dispose();
            //return the picture
            return canvas;
        }

    }
}