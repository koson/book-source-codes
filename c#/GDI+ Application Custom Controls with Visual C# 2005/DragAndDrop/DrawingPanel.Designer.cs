namespace DragAndDrop
{
    partial class DrawingPanel
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.drawingArea = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // drawingArea
            // 
            this.drawingArea.AllowDrop = true;
            this.drawingArea.BackColor = System.Drawing.Color.WhiteSmoke;
            this.drawingArea.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.drawingArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.drawingArea.Location = new System.Drawing.Point(0, 0);
            this.drawingArea.Name = "drawingArea";
            this.drawingArea.Size = new System.Drawing.Size(360, 180);
            this.drawingArea.TabIndex = 0;
            this.drawingArea.DragDrop += new System.Windows.Forms.DragEventHandler(this.drawingArea_DragDrop);
            this.drawingArea.DragEnter += new System.Windows.Forms.DragEventHandler(this.drawingArea_DragEnter);
            // 
            // DrawingPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.drawingArea);
            this.Name = "DrawingPanel";
            this.Size = new System.Drawing.Size(360, 180);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel drawingArea;
    }
}
