namespace SimDrag
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dragArea1 = new SimDrag.DragArea();
            this.SuspendLayout();
            // 
            // dragArea1
            // 
            this.dragArea1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dragArea1.Location = new System.Drawing.Point(23, 25);
            this.dragArea1.Name = "dragArea1";
            this.dragArea1.Size = new System.Drawing.Size(270, 300);
            this.dragArea1.TabIndex = 0;
            this.dragArea1.LocationChanged += new SimDrag.LocationChangedEvent(this.dragArea1_LocationChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(336, 345);
            this.Controls.Add(this.dragArea1);
            this.Name = "MainForm";
            this.Text = "Sim Drag";
            this.ResumeLayout(false);

        }

        #endregion

        private DragArea dragArea1;
    }
}

