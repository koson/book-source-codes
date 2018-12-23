using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
public class ImageWarper : Component
{
    public void DrawWarpedPicture(
    Graphics surface,  // the surface to draw on
    Image img,         // the image to draw
    PointF sourceAxle, // pivot point passing through image.
    PointF destAxle,   // pivot point's position on destination surface
    double degrees,    // degrees through which the image is rotated clockwise
    double scale,      // size multiplier
    SizeF skew         // the slanting effect size, applies BEFORE scaling or rotation
  )
    {
        // give this array temporary coords that will be overwritten in the loop below
        // the skewing is done here orthogonally, before any trigonometry is applied
        PointF[] temp = new PointF[3] {  new PointF(skew.Width, -skew.Height),
     new PointF((img.Width - 1) + skew.Width, skew.Height),
     new PointF(-skew.Width,(img.Height - 1) - skew.Height) };
        double ang, dist;
        double radians = degrees * (Math.PI / 180);
        // convert the images corner points into scaled, rotated, skewed and translated points
        for (int i = 0; i < 3; i++)
        {
            // measure the angle to the image's corner and then add the rotation value to it
            ang = GetBearingRadians(sourceAxle, temp[i], out dist) + radians;
            dist *= scale; // scale
            temp[i] = new PointF((Single)((Math.Cos(ang) * dist) + destAxle.X), (Single)((Math.Sin(ang) * dist) + destAxle.Y));
        }
        surface.DrawImage(img, temp);
    }

    private double GetBearingRadians(PointF reference, PointF target, out double distance)
    {
        double dx = target.X - reference.X;
        double dy = target.Y - reference.Y;
        double result = Math.Atan2(dy, dx);
        distance = Math.Sqrt((dx * dx) + (dy * dy));
        if (result < 0)
            result += (Math.PI * 2); // add the negative number to 360 degrees to correct the atan2 value
        return result;
    }
}
