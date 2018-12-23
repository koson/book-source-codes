namespace HostApplication
{
    partial class FontPicker
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
            this.colorLabel = new System.Windows.Forms.Label();
            this.colorCombo = new System.Windows.Forms.ComboBox();
            this.fontLabel = new System.Windows.Forms.Label();
            this.fontCombo = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // colorLabel
            // 
            this.colorLabel.AutoSize = true;
            this.colorLabel.Location = new System.Drawing.Point(20, 20);
            this.colorLabel.Name = "colorLabel";
            this.colorLabel.Size = new System.Drawing.Size(34, 13);
            this.colorLabel.TabIndex = 0;
            this.colorLabel.Text = "Color:";
            // 
            // colorCombo
            // 
            this.colorCombo.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.colorCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.colorCombo.FormattingEnabled = true;
            this.colorCombo.Location = new System.Drawing.Point(60, 15);
            this.colorCombo.Name = "colorCombo";
            this.colorCombo.Size = new System.Drawing.Size(121, 21);
            this.colorCombo.TabIndex = 1;
            this.colorCombo.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.colorCombo_DrawItem);
            this.colorCombo.SelectedIndexChanged += new System.EventHandler(this.colorCombo_SelectedIndexChanged);
            // 
            // fontLabel
            // 
            this.fontLabel.AutoSize = true;
            this.fontLabel.Location = new System.Drawing.Point(200, 20);
            this.fontLabel.Name = "fontLabel";
            this.fontLabel.Size = new System.Drawing.Size(31, 13);
            this.fontLabel.TabIndex = 2;
            this.fontLabel.Text = "Font:";
            // 
            // fontCombo
            // 
            this.fontCombo.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.fontCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fontCombo.FormattingEnabled = true;
            this.fontCombo.Location = new System.Drawing.Point(240, 15);
            this.fontCombo.Name = "fontCombo";
            this.fontCombo.Size = new System.Drawing.Size(121, 21);
            this.fontCombo.TabIndex = 3;
            this.fontCombo.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.fontCombo_DrawItem);
            this.fontCombo.SelectedIndexChanged += new System.EventHandler(this.fontCombo_SelectedIndexChanged);
            // 
            // FontPicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.fontCombo);
            this.Controls.Add(this.fontLabel);
            this.Controls.Add(this.colorCombo);
            this.Controls.Add(this.colorLabel);
            this.Name = "FontPicker";
            this.Size = new System.Drawing.Size(398, 48);
            this.Load += new System.EventHandler(this.FontPicker_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label colorLabel;
        private System.Windows.Forms.ComboBox colorCombo;
        private System.Windows.Forms.Label fontLabel;
        private System.Windows.Forms.ComboBox fontCombo;
    }
}
