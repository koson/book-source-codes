using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DragAndDrop
{
    public partial class DrawingPanel : UserControl
    {
        int left = 0;
        int top = 0;
        int count = 0;
        int maxPictures = 0;
        Size picSize;

        public DrawingPanel()
        {
            InitializeComponent();
            picSize = new Size(90, 90);
            this.maxPictures = (this.Height / picSize.Height) *
                               (this.Width / picSize.Width);

        }
        public void Clear()
        {
            drawingArea.Controls.Clear();
            this.count = 0;
            this.left = 0;
            this.top = 0;
        }

        private void drawingArea_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop) && count < maxPictures)
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }

        }

        private void drawingArea_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] fileData = (string[])e.Data.GetData(DataFormats.FileDrop);
                try
                {
                    Bitmap bitmap = new Bitmap(fileData[0]);
                    AddPictureBox(bitmap);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error has occurred: " + ex.Message,
                      "Error",
                      MessageBoxButtons.OK,
                      MessageBoxIcon.Error);
                }
            }

        }
        private void AddPictureBox(Bitmap bitmap)
        {
            PictureBox pictureBox = new PictureBox();
            drawingArea.Controls.Add(pictureBox);
            if (bitmap.Width > picSize.Width - 20 || bitmap.Height > picSize.Height - 20)
            {
                bitmap = ResizeBitmap(bitmap);
            }

            pictureBox.BackgroundImage = bitmap;
            pictureBox.BackgroundImageLayout = ImageLayout.Center;
            pictureBox.Height = picSize.Height; pictureBox.Width = picSize.Width;
            pictureBox.Location = new Point(left, top);
            if (count < maxPictures)
            {
                this.left += picSize.Width;
                count++;
                if (left == drawingArea.Width)
                {
                    this.top += picSize.Height;
                    this.left = 0;
                }
            }
        }
        private Bitmap ResizeBitmap(Bitmap bitmap)
        {
            double ratio;
            int height;
            int width;
            Size size;
            if (bitmap.Height >= bitmap.Width)
            {
                ratio = (float)bitmap.Height / 70;
            }
            else
            {
                ratio = ((double)bitmap.Width) / 70.0;
            }
            height = Convert.ToInt32(bitmap.Height / ratio);
            width = Convert.ToInt32(bitmap.Width / ratio);
            size = new Size(width, height);
            Bitmap newBitmap = new Bitmap(bitmap, size);
            return newBitmap;
        }



    }
}
