namespace SimDrag
{
    partial class DragArea
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
            this.panelDraggingZone = new System.Windows.Forms.Panel();
            this.draggingIcon = new System.Windows.Forms.PictureBox();
            this.chkDragging = new System.Windows.Forms.CheckBox();
            this.lblX = new System.Windows.Forms.Label();
            this.lblY = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.panelDraggingZone.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.draggingIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // panelDraggingZone
            // 
            this.panelDraggingZone.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelDraggingZone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelDraggingZone.Controls.Add(this.draggingIcon);
            this.panelDraggingZone.Location = new System.Drawing.Point(10, 10);
            this.panelDraggingZone.Name = "panelDraggingZone";
            this.panelDraggingZone.Size = new System.Drawing.Size(250, 200);
            this.panelDraggingZone.TabIndex = 0;
            // 
            // draggingIcon
            // 
            this.draggingIcon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.draggingIcon.Image = global::SimDrag.Properties.Resources.Zapotec;
            this.draggingIcon.Location = new System.Drawing.Point(68, 66);
            this.draggingIcon.Name = "draggingIcon";
            this.draggingIcon.Size = new System.Drawing.Size(96, 96);
            this.draggingIcon.TabIndex = 0;
            this.draggingIcon.TabStop = false;
            this.draggingIcon.LocationChanged += new System.EventHandler(this.draggingIcon_LocationChanged);
            this.draggingIcon.MouseDown += new System.Windows.Forms.MouseEventHandler(this.draggingIcon_MouseDown);
            this.draggingIcon.MouseMove += new System.Windows.Forms.MouseEventHandler(this.draggingIcon_MouseMove);
            // 
            // chkDragging
            // 
            this.chkDragging.AutoSize = true;
            this.chkDragging.Checked = true;
            this.chkDragging.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDragging.Location = new System.Drawing.Point(10, 220);
            this.chkDragging.Name = "chkDragging";
            this.chkDragging.Size = new System.Drawing.Size(80, 17);
            this.chkDragging.TabIndex = 1;
            this.chkDragging.Text = "checkBox1";
            this.chkDragging.UseVisualStyleBackColor = true;
            // 
            // lblX
            // 
            this.lblX.AutoSize = true;
            this.lblX.Location = new System.Drawing.Point(10, 250);
            this.lblX.Name = "lblX";
            this.lblX.Size = new System.Drawing.Size(14, 13);
            this.lblX.TabIndex = 2;
            this.lblX.Text = "X";
            // 
            // lblY
            // 
            this.lblY.AutoSize = true;
            this.lblY.Location = new System.Drawing.Point(10, 270);
            this.lblY.Name = "lblY";
            this.lblY.Size = new System.Drawing.Size(14, 13);
            this.lblY.TabIndex = 3;
            this.lblY.Text = "Y";
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(160, 260);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(100, 23);
            this.btnReset.TabIndex = 4;
            this.btnReset.Text = "Reset Position";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // DragArea
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.lblY);
            this.Controls.Add(this.lblX);
            this.Controls.Add(this.chkDragging);
            this.Controls.Add(this.panelDraggingZone);
            this.Name = "DragArea";
            this.Size = new System.Drawing.Size(270, 300);
            this.panelDraggingZone.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.draggingIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelDraggingZone;
        private System.Windows.Forms.PictureBox draggingIcon;
        private System.Windows.Forms.CheckBox chkDragging;
        private System.Windows.Forms.Label lblX;
        private System.Windows.Forms.Label lblY;
        private System.Windows.Forms.Button btnReset;
    }
}
