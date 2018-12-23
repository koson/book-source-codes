protected override void OnPaint(PaintEventArgs e)
    {
      e.Graphics.DrawEllipse(Pens.Red, ClientRectangle);
      Font stringFont = new Font("Arial", 12);
      e.Graphics.DrawString("Second custom control in the Windows Control Library", stringFont, Brushes.LightPink,         ClientRectangle);
      base.OnPaint(e);
    }
