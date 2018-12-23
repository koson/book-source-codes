namespace ImageWarperApp
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
            this.angleBox = new System.Windows.Forms.TextBox();
            this.scaleBox = new System.Windows.Forms.TextBox();
            this.skewHorizontalBox = new System.Windows.Forms.TextBox();
            this.skewVerticalBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.applyButton = new System.Windows.Forms.Button();
            this.imageWarperControl1 = new ImageWarperApp.ImageWarperControl();
            this.SuspendLayout();
            // 
            // angleBox
            // 
            this.angleBox.Location = new System.Drawing.Point(128, 42);
            this.angleBox.Name = "angleBox";
            this.angleBox.Size = new System.Drawing.Size(100, 20);
            this.angleBox.TabIndex = 1;
            // 
            // scaleBox
            // 
            this.scaleBox.Location = new System.Drawing.Point(128, 100);
            this.scaleBox.Name = "scaleBox";
            this.scaleBox.Size = new System.Drawing.Size(100, 20);
            this.scaleBox.TabIndex = 2;
            // 
            // skewHorizontalBox
            // 
            this.skewHorizontalBox.Location = new System.Drawing.Point(128, 157);
            this.skewHorizontalBox.Name = "skewHorizontalBox";
            this.skewHorizontalBox.Size = new System.Drawing.Size(100, 20);
            this.skewHorizontalBox.TabIndex = 3;
            // 
            // skewVerticalBox
            // 
            this.skewVerticalBox.Location = new System.Drawing.Point(128, 217);
            this.skewVerticalBox.Name = "skewVerticalBox";
            this.skewVerticalBox.Size = new System.Drawing.Size(100, 20);
            this.skewVerticalBox.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Rotation Angle (deg)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Scale Factor (%)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 160);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Skew Horizontal Factor";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 220);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Skew Vertical Factor";
            // 
            // applyButton
            // 
            this.applyButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.applyButton.Location = new System.Drawing.Point(61, 285);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(118, 34);
            this.applyButton.TabIndex = 9;
            this.applyButton.Text = "Apply New Settings";
            this.applyButton.UseVisualStyleBackColor = false;
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            // 
            // imageWarperControl1
            // 
            this.imageWarperControl1.ImageAngle = 0;
            this.imageWarperControl1.ImageScale = 0;
            this.imageWarperControl1.ImageSkew = new System.Drawing.SizeF(0F, 0F);
            this.imageWarperControl1.Location = new System.Drawing.Point(253, 45);
            this.imageWarperControl1.Name = "imageWarperControl1";
            this.imageWarperControl1.Size = new System.Drawing.Size(185, 192);
            this.imageWarperControl1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 342);
            this.Controls.Add(this.applyButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.skewVerticalBox);
            this.Controls.Add(this.skewHorizontalBox);
            this.Controls.Add(this.scaleBox);
            this.Controls.Add(this.angleBox);
            this.Controls.Add(this.imageWarperControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ImageWarperControl imageWarperControl1;
        private System.Windows.Forms.TextBox angleBox;
        private System.Windows.Forms.TextBox scaleBox;
        private System.Windows.Forms.TextBox skewHorizontalBox;
        private System.Windows.Forms.TextBox skewVerticalBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button applyButton;
    }
}

