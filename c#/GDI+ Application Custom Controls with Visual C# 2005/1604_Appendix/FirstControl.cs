protected override void OnPaint(PaintEventArgs e)
    {
      e.Graphics.DrawEllipse(Pens.Black, ClientRectangle);
      Font stringFont = new Font("Arial", 12);
      e.Graphics.DrawString("First custom control in the Windows Control Library", stringFont, Brushes.Blue,         ClientRectangle);
      base.OnPaint(e);
    }
