using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Sketch;

namespace PencilSketch
{
	public partial class Sketcher : UserControl
	{
		Bitmap pal = null;
		Bitmap backBuf = null;
		Graphics grBuf = null;

		DrawingToolBase currentTool = null;
		Pen tempPen;

		public Sketcher()
		{
			InitializeComponent();
			backBuf = new Bitmap(picPaper.Width, picPaper.Height);
			grBuf = Graphics.FromImage(backBuf);
			grBuf.Clear(picPaper.BackColor); //clear the buffer
			grBuf.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

			Application.EnableVisualStyles();
			pal = new Bitmap(picPalette.Image);
			cmdFreehand_Click(null, null);
			DrawPen = new Pen(Brushes.Black);
		}

		private void picPalette_MouseUp(object sender, MouseEventArgs e)
		{
			ChangePenColor(pal.GetPixel(e.X, e.Y)); 
			Cursor.Clip = Rectangle.Empty;
		}

		private void picPalette_MouseDown(object sender, MouseEventArgs e)
		{
			Cursor.Clip = picPalette.RectangleToScreen(picPalette.ClientRectangle);
			ChangePenColor(pal.GetPixel(e.X, e.Y));
		}

		private void picPalette_MouseMove(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
				ChangePenColor(pal.GetPixel(e.X, e.Y));
		}

		private void picPaper_MouseDown(object sender, MouseEventArgs e)
		{
			currentTool.DoMouseInput(e, DrawingToolBase.MouseEventCategory.MouseButtonDown);
		}

		private void picPaper_MouseMove(object sender, MouseEventArgs e)
		{
			currentTool.DoMouseInput(e, DrawingToolBase.MouseEventCategory.MouseMove);
		}

		private void picPaper_MouseUp(object sender, MouseEventArgs e)
		{
			currentTool.DoMouseInput(e, DrawingToolBase.MouseEventCategory.MouseButtonUp);
		}

		private void cmdFreehand_Click(object sender, EventArgs e)
		{
			CurrentTool = new FreehandTool(picPaper, grBuf, tempPen);
		}

		private void cmdLine_Click(object sender, EventArgs e)
		{
			CurrentTool = new StraightLineTool(picPaper, grBuf, tempPen);
		}

		private void picSwatch_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
			
			//show a preview as either a straight line or a bezier curve
			if(currentTool is StraightLineTool)
				e.Graphics.DrawLine(currentTool.DrawPen, 
						new Point((int)(picSwatch.Width * .1f), (int)(picSwatch.Height * .1f)),
						new Point((int)(picSwatch.Width * .9f), (int)(picSwatch.Height * .9f))
						);
			else
				if(currentTool is FreehandTool)
					e.Graphics.DrawBezier(currentTool.DrawPen,
						new Point((int)(picSwatch.Width * .1f), (int)(picSwatch.Height * .1f)),
						new Point((int)(picSwatch.Width * .2f), (int)(picSwatch.Height * .9f)),
						new Point((int)(picSwatch.Width * .8f), (int)(picSwatch.Height * .1f)),
						new Point((int)(picSwatch.Width * .9f), (int)(picSwatch.Height * .9f))
						);
		}

		private void trkWidth_Scroll(object sender, EventArgs e)
		{
			ChangePenWidth(trkWidth.Value);
		}

		private void ChangePenWidth(int w)
		{
			tempPen.Width = w;
			DrawPen = tempPen;
		}

		private void ChangePenColor(Color col)
		{
			tempPen.Color = col;
			DrawPen = tempPen;
		}

		public Pen DrawPen
		{
			get
			{
				return tempPen;
			}
			set
			{
				currentTool.DrawPen = tempPen = value;
				picSwatch.Invalidate();
			}
		}

		public DrawingToolBase CurrentTool
		{
			get
			{
				return currentTool; 
			}
			set
			{
				currentTool = value;
				picSwatch.Invalidate();
			}
		}

		private void picPaper_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.DrawImageUnscaled(backBuf, 0, 0);
		}

		private void cmdClear_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Do you really want to ERASE your drawing?",
								"Erase drawing?",
								MessageBoxButtons.YesNo,
								MessageBoxIcon.Question,
								MessageBoxDefaultButton.Button2
								) == DialogResult.Yes
				)
			{
				grBuf.Clear(picPaper.BackColor);
				picPaper.Invalidate();
			}
		}
	}
}
