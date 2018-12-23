namespace SoundPlayerTest
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
            this.playButton = new System.Windows.Forms.Button();
            this.tinyNoiseMaker1 = new SoundPlayerTest.TinyNoiseMaker();
            this.SuspendLayout();
            // 
            // playButton
            // 
            this.playButton.Location = new System.Drawing.Point(100, 22);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(82, 23);
            this.playButton.TabIndex = 1;
            this.playButton.Text = "Play My File";
            this.playButton.UseVisualStyleBackColor = true;
            this.playButton.Click += new System.EventHandler(this.playButton_Click);
            // 
            // tinyNoiseMaker1
            // 
            this.tinyNoiseMaker1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tinyNoiseMaker1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tinyNoiseMaker1.FileName = null;
            this.tinyNoiseMaker1.Location = new System.Drawing.Point(21, 68);
            this.tinyNoiseMaker1.Name = "tinyNoiseMaker1";
            this.tinyNoiseMaker1.Size = new System.Drawing.Size(268, 37);
            this.tinyNoiseMaker1.TabIndex = 0;
            this.tinyNoiseMaker1.PlayStart += new System.EventHandler(this.tinyNoiseMaker1_PlayStart);
            this.tinyNoiseMaker1.PlayStop += new System.EventHandler(this.tinyNoiseMaker1_PlayStop);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(316, 163);
            this.Controls.Add(this.playButton);
            this.Controls.Add(this.tinyNoiseMaker1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private TinyNoiseMaker tinyNoiseMaker1;
        private System.Windows.Forms.Button playButton;
    }
}

