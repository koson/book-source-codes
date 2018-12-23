using System;
using System.Drawing;
using System.Windows.Forms;

namespace Sketch
{
	/// <summary>
	/// Lets the user draw a straight line by dragging the mouse.  Right clicking draws rays from the origin point.
	/// </summary>
	public class StraightLineTool : DrawingToolBase
	{
		protected Point originPt;
		protected bool isInitialized = false;

		public StraightLineTool(Control bind, Graphics drawToSurface, Pen style)
			: base(bind, drawToSurface, style)
		{
			suggestedCursor = Cursors.Cross;
			bound.Cursor = suggestedCursor;
		}

		public override void DoMouseInput(MouseEventArgs e, MouseEventCategory met)
		{
			Point thisPt = new Point(e.X, e.Y);

			switch (met)
			{
				case MouseEventCategory.MouseButtonDown:
					if (e.Button == MouseButtons.Left)
					{
						previousPt = originPt = thisPt;
						Cursor.Clip = bound.RectangleToScreen(new Rectangle(new Point(), 							      bound.Size));
						isInitialized = true;
					}

					break;

				case MouseEventCategory.MouseMove:
					if (e.Button == MouseButtons.Left)
					{
						ControlPaint.DrawReversibleLine(bound.PointToScreen(originPt), 								bound.PointToScreen(previousPt), Color.White);
						ControlPaint.DrawReversibleLine(bound.PointToScreen(originPt), 								bound.PointToScreen(thisPt), Color.White);
						previousPt = thisPt;
					}

					break;

				case MouseEventCategory.MouseButtonUp:

					if (isInitialized)
					{
						surface.DrawLine(drawPen, originPt, thisPt);

						//release the mouse
						Cursor.Clip = Rectangle.Empty;
						bound.Invalidate();
					}

					break;
			}
		}
	}

}














