using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace ClockTest00
{
    public partial class Clock : UserControl
    {

        public Clock()
        {
            //Sets the rendering mode of the control to double buffer to stop flickering
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            InitializeComponent();
            //Enables the timer so the clock refreshes every second
            timer.Enabled = true;
            MakeRound();
        }

        private Color hourColor = Color.Black;
        private Color minuteColor = Color.Black;
        private Color secondColor = Color.Black;
        public Color HourColor
        {
            get
            {
                return hourColor;
            }
            set
            {
                if (hourColor != value)
                {
                    hourColor = value;
                    Invalidate();
                }
            }
        }
        public Color MinuteColor
        {
            get
            {
                return minuteColor;
            }
            set
            {
                if (minuteColor != value)
                {
                    minuteColor = value;
                    Invalidate();
                }
            }
        }
        public Color SecondColor
        {
            get
            {
                return secondColor;
            }
            set
            {
                if (secondColor != value)
                {
                    secondColor = value;
                    Invalidate();
                }
            }
        }



        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            //Smooths out the appearance of the control
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            //The center of the control, that is used as center for the clock
            PointF center = new PointF(this.Width / 2, this.Height / 2);
            //The distace of the text from the center
            float textRadius = (Math.Min(Width, Height) - Font.Height) / 2;
            //The distance of the margin points from the center
            float outerRadius = Math.Min(Width, Height) / 2 - Font.Height;
            //The length of the hour line
            float hourRadius = outerRadius * 6 / 9;
            //The length of the minute line
            float minuteRadius = outerRadius * 7 / 9;
            //The length of the second line
            float secondRadius = outerRadius * 8 / 9;
            for (int i = 1; i <= 60; i++)
            {
                //Gets the angle of the outer dot
                float angle = GetAngle(i / 5f, 12);
                //Gets the location of the outer dot
                PointF dotPoint = GetPoint(center, outerRadius, angle);
                //Indicates the size of the point
                int pointSize = 2;

                //Is true when a large dot needs to be rendered
                if (i % 5 == 0)
                {
                    //Sets the size of the point to make it bigger
                    pointSize = 4;
                    //The hour number
                    string text = (i / 5).ToString();
                    SizeF sz = e.Graphics.MeasureString(text, Font);
                    //The point where the text should be rendered
                    PointF textPoint = GetPoint(center, textRadius, angle);
                    //Offsets the text location so it is centered in that point.
                    textPoint.X -= sz.Width / 2;
                    textPoint.Y -= sz.Height / 2;

                    //Draws the hour number
                    e.Graphics.DrawString(text, Font, new SolidBrush(this.ForeColor), textPoint);
                }

                Pen pen = new Pen(new SolidBrush(this.ForeColor), 1);
                //Draws the outer dot of the clock
                e.Graphics.DrawEllipse(pen, dotPoint.X - pointSize / 2, dotPoint.Y - pointSize / 2, pointSize, pointSize);
                pen.Dispose();
            }

            //Gets the system time
            DateTime dt = DateTime.Now;
            //Calculates the hour offset from the large outer dot
            float min = ((float)dt.Minute) / 60;
            //Calculates the angle of the hour line
            float hourAngle = GetAngle(dt.Hour + min, 12);
            //Calculates the angle of the minute line
            float minuteAngle = GetAngle(dt.Minute, 60);
            //Calculates the angle of the second line
            float secondAngle = GetAngle(dt.Second, 60);
            //Draws the clock lines
            DrawLine(e.Graphics, this.secondColor, 1, center, secondRadius, secondAngle);
            DrawLine(e.Graphics, this.minuteColor, 2, center, minuteRadius, minuteAngle);
            DrawLine(e.Graphics, this.hourColor, 3, center, hourRadius, hourAngle);
            e.Graphics.ResetTransform();
        }

        private PointF GetPoint(PointF center, float radius, float angle)
        {
            //Calculates the X coordinate of the point
            float x = (float)Math.Cos(2 * Math.PI * angle / 360) * radius + center.X;
            //Calculates the Y coordinate of the point
            float y = -(float)Math.Sin(2 * Math.PI * angle / 360) * radius + center.Y;
            return new PointF(x, y);
        }

        private void DrawLine(Graphics g, Color color, int penWidth, PointF center, float radius, float angle)
        {
            //Calculates the end point of the line
            PointF endPoint = GetPoint(center, radius, angle);
            //Creates the pen used to render the line
            Pen pen = new Pen(new SolidBrush(color), penWidth);
            //Renders the line
            g.DrawLine(pen, center, endPoint);
            pen.Dispose();
        }

        private void MakeRound()
        {
            GraphicsPath gp = new GraphicsPath();
            float min = Math.Min(Width, Height);
            //Creates the ellipse shape
            gp.AddEllipse((Width - min) / 2, (Height - min) / 2, min, min);
            //Creates the ellipse region
            Region rgn = new Region(gp);
            //Sets the ellipse region to the control
            this.Region = rgn;
        }

        private float GetAngle(float clockValue, float divisions)
        {
            //Calculates the angle
            return 360 - (360 * (clockValue) / divisions) + 90;
        }


        private void timer_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void Clock_SizeChanged(object sender, EventArgs e)
        {
            MakeRound();
            Invalidate();
        }

        private void Clock_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
