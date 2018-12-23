using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ImageWarperApp
{
   
    public partial class ImageWarperControl : UserControl
    {
        public ImageWarperControl()
        {
            InitializeComponent();
        }

        private void ImageWarperControl_Load(object sender, EventArgs e)
        {
            
            img = CreatePicture();
        }

        private void ImageWarperControl_Paint(object sender, PaintEventArgs e)
        {
                  // set up all our parameters first before calling DrawWarpedPicture.
      Graphics target = this.CreateGraphics(); //draw onto the form's surface
      PointF pivotOnImage = new PointF(img.Width / 2, img.Height / 2);
      PointF pivotOnTarget = new PointF(this.Width / 2, this.Height / 2);
      double rotate = imageAngle;
      double scaleFactor = imageScale;
      SizeF skewing = imageSkew;
      DrawWarpedPicture(target, img, pivotOnImage, pivotOnTarget, rotate, scaleFactor, skewing);
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
            // return the picture
            return canvas;
        }
        public void DrawWarpedPicture(
            Graphics surface,   //the surface to draw on
            Image img,    //the image to draw
            PointF sourceAxle,  //pivot point passing through image.
            PointF destAxle,  //pivot point's position on destination surface
            double degrees,  //degrees through which the image is rotated clockwise
            double scale,     //size multiplier
            SizeF skew      //the slanting effect size, applies BEFORE scaling or rotation
          )
        {
            //give this array temporary coords that will be overwritten in the loop below
            //the skewing is done here orthogonally, before any trigonometry is applied
            PointF[] temp = new PointF[3] {  new PointF(skew.Width, -skew.Height),
                    new PointF((img.Width - 1) + skew.Width, skew.Height),
                    new PointF(-skew.Width,(img.Height - 1) - skew.Height) };
            double ang, dist;
            //convert the images corner points into scaled, rotated, skewed and translated points
            for (int i = 0; i < 3; i++)
            {
                //measure the angle to the image's corner and then add the rotation value to it
                ang = GetBearingRadians(sourceAxle, temp[i], out dist) + degrees;
                dist *= scale; //scale
                temp[i] = new PointF((Single)((Math.Cos(ang) * dist) + destAxle.X), (Single)((Math.Sin(ang) * dist) + destAxle.Y));
            }
            surface.DrawImage(img, temp);
        }
        private static double GetBearingRadians(PointF reference, PointF target, out double distance)
        {
            double dx = target.X - reference.X;
            double dy = target.Y - reference.Y;
            double result = Math.Atan2(dy, dx);
            distance = Math.Sqrt((dx * dx) + (dy * dy));
            if (result < 0)
                result += (Math.PI * 2); //add  the negative number to 360 degrees to correct the atan2 value
            return result;
        }
        private double imageAngle;
        private double imageScale;
        private SizeF imageSkew;
        private Image img = null;
        public double ImageAngle
        {
            get
            {
                return imageAngle;
            }
            set
            {
                if (imageAngle != value)
                {
                    imageAngle = value;
                    Invalidate();
                }
            }
        }
        public double ImageScale
        {
            get
            {
                return imageScale;
            }
            set
            {
                if (imageScale != value)
                {
                    imageScale = value;
                    Invalidate();
                }
            }
        }
        public SizeF ImageSkew
        {
            get
            {
                return imageSkew;
            }
            set
            {
                if (imageSkew != value)
                {
                    imageSkew = value;
                    Invalidate();
                }
            }
        }


        
    }
}
