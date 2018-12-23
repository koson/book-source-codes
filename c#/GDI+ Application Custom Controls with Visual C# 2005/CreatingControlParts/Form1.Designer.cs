namespace CreatingControlParts
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
            this.gradientButton1 = new CreatingControlParts.GradientButton();
            this.SuspendLayout();
            // 
            // gradientButton1
            // 
            this.gradientButton1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.gradientButton1.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.gradientButton1.EndColor = System.Drawing.SystemColors.ActiveCaption;
            this.gradientButton1.Location = new System.Drawing.Point(91, 53);
            this.gradientButton1.Name = "gradientButton1";
            this.gradientButton1.Size = new System.Drawing.Size(100, 40);
            this.gradientButton1.StartColor = System.Drawing.SystemColors.ControlLightLight;
            this.gradientButton1.TabIndex = 0;
            this.gradientButton1.Text = "Gradient Button";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.gradientButton1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private GradientButton gradientButton1;




    }
}

