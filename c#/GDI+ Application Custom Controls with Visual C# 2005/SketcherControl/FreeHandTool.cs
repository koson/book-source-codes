using System;
using System.Drawing;
using System.Windows.Forms;

namespace Sketch
{
	/// <summary>
	/// Simple mouse freehand tool.
	/// </summary>
	public class FreehandTool : DrawingToolBase
	{
		public FreehandTool(Control bind, Graphics drawToSurface, Pen style)
			: base(bind, drawToSurface, style)
		{
			suggestedCursor = Cursors.Hand;
			bound.Cursor = suggestedCursor;
		}

		public override void DoMouseInput(MouseEventArgs e, MouseEventCategory met)
		{
			Point thisPt = new Point(e.X, e.Y);

			switch(met)
			{
				case MouseEventCategory.MouseButtonDown:
					if(e.Button == MouseButtons.Left)
					{
						previousPt = thisPt;
						Cursor.Clip = bound.RectangleToScreen(new Rectangle(new Point(), 							      bound.Size));
					}

					break;

				case MouseEventCategory.MouseMove:
					if(e.Button == MouseButtons.Left)
					{
						surface.DrawLine(drawPen, previousPt, thisPt);
						previousPt = new Point(e.X,e.Y);
						bound.Invalidate();
					}

					break;

				case MouseEventCategory.MouseButtonUp:
					//release the mouse
					Cursor.Clip = Rectangle.Empty;

					break;
			}
		}
	}
}
