namespace ClockTest00
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
            this.clock1 = new ClockTest00.Clock();
            this.SuspendLayout();
            // 
            // clock1
            // 
            this.clock1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.clock1.HourColor = System.Drawing.Color.Black;
            this.clock1.Location = new System.Drawing.Point(67, 49);
            this.clock1.MinuteColor = System.Drawing.Color.Black;
            this.clock1.Name = "clock1";
            this.clock1.SecondColor = System.Drawing.Color.Black;
            this.clock1.Size = new System.Drawing.Size(150, 150);
            this.clock1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.clock1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private Clock clock1;
    }
}

