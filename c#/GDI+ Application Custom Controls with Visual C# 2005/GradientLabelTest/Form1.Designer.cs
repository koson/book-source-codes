namespace GradientLabelTest00
{
    partial class Form1
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
            this.gradientLabel1 = new GradientLabelTest00.GradientLabel();
            this.SuspendLayout();
            // 
            // gradientLabel1
            // 
            this.gradientLabel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.gradientLabel1.BackColor2 = System.Drawing.SystemColors.ControlDarkDark;
            this.gradientLabel1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.gradientLabel1.Location = new System.Drawing.Point(66, 68);
            this.gradientLabel1.Name = "gradientLabel1";
            this.gradientLabel1.Size = new System.Drawing.Size(150, 47);
            this.gradientLabel1.TabIndex = 0;
            this.gradientLabel1.Text = "Gradient Label";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.gradientLabel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private GradientLabel gradientLabel1;
    }
}

