namespace FlickerFree
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
            this.scrollingText = new FlickerFree.ScrollingText();
            this.grpTrackerBars = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbAmount = new System.Windows.Forms.TrackBar();
            this.tbInterval = new System.Windows.Forms.TrackBar();
            this.grpTrackerBars.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbInterval)).BeginInit();
            this.SuspendLayout();
            // 
            // scrollingText
            // 
            this.scrollingText.BackColor = System.Drawing.Color.LightGray;
            this.scrollingText.Font = new System.Drawing.Font("Verdana", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scrollingText.ForeColor = System.Drawing.Color.Navy;
            this.scrollingText.Location = new System.Drawing.Point(38, 29);
            this.scrollingText.Margin = new System.Windows.Forms.Padding(12, 10, 12, 10);
            this.scrollingText.Name = "scrollingText";
            this.scrollingText.ScrollPixelAmount = 10;
            this.scrollingText.ScrollTimeInterval = 100;
            this.scrollingText.Size = new System.Drawing.Size(300, 80);
            this.scrollingText.TabIndex = 0;
            this.scrollingText.Text = "Scrolling Text that Scrolls";
            // 
            // grpTrackerBars
            // 
            this.grpTrackerBars.Controls.Add(this.tbInterval);
            this.grpTrackerBars.Controls.Add(this.tbAmount);
            this.grpTrackerBars.Controls.Add(this.label2);
            this.grpTrackerBars.Controls.Add(this.label1);
            this.grpTrackerBars.Location = new System.Drawing.Point(38, 132);
            this.grpTrackerBars.Name = "grpTrackerBars";
            this.grpTrackerBars.Size = new System.Drawing.Size(322, 183);
            this.grpTrackerBars.TabIndex = 1;
            this.grpTrackerBars.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Scroll Amount";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 134);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Scroll Interval";
            // 
            // tbAmount
            // 
            this.tbAmount.Location = new System.Drawing.Point(101, 19);
            this.tbAmount.Maximum = 20;
            this.tbAmount.Name = "tbAmount";
            this.tbAmount.Size = new System.Drawing.Size(200, 45);
            this.tbAmount.TabIndex = 2;
            this.tbAmount.Value = 1;
            this.tbAmount.Scroll += new System.EventHandler(this.tbAmount_Scroll);
            // 
            // tbInterval
            // 
            this.tbInterval.Location = new System.Drawing.Point(101, 123);
            this.tbInterval.Maximum = 500;
            this.tbInterval.Minimum = 10;
            this.tbInterval.Name = "tbInterval";
            this.tbInterval.Size = new System.Drawing.Size(200, 45);
            this.tbInterval.TabIndex = 3;
            this.tbInterval.TickFrequency = 10;
            this.tbInterval.Value = 100;
            this.tbInterval.Scroll += new System.EventHandler(this.tbInterval_Scroll);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(456, 332);
            this.Controls.Add(this.grpTrackerBars);
            this.Controls.Add(this.scrollingText);
            this.Name = "Form1";
            this.Text = "Form1";
            this.grpTrackerBars.ResumeLayout(false);
            this.grpTrackerBars.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbInterval)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ScrollingText scrollingText;
        private System.Windows.Forms.GroupBox grpTrackerBars;
        private System.Windows.Forms.TrackBar tbInterval;
        private System.Windows.Forms.TrackBar tbAmount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}

