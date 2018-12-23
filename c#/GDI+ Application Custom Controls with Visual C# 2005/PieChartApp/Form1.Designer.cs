namespace PieChartApp
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
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.addSliceButton = new System.Windows.Forms.Button();
            this.addSliceColorButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.addSliceSizeTextBox = new System.Windows.Forms.TextBox();
            this.addSliceNameTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.removeSliceButton = new System.Windows.Forms.Button();
            this.removeSliceNameTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.pieChart1 = new PieChartApp.PieChart();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(294, 383);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Print";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.addSliceButton);
            this.groupBox1.Controls.Add(this.addSliceColorButton);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.addSliceSizeTextBox);
            this.groupBox1.Controls.Add(this.addSliceNameTextBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(51, 398);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 139);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Add Slice";
            // 
            // addSliceButton
            // 
            this.addSliceButton.Location = new System.Drawing.Point(52, 110);
            this.addSliceButton.Name = "addSliceButton";
            this.addSliceButton.Size = new System.Drawing.Size(75, 23);
            this.addSliceButton.TabIndex = 6;
            this.addSliceButton.Text = "Add Slice";
            this.addSliceButton.UseVisualStyleBackColor = true;
            this.addSliceButton.Click += new System.EventHandler(this.addSliceButton_Click);
            // 
            // addSliceColorButton
            // 
            this.addSliceColorButton.Location = new System.Drawing.Point(52, 73);
            this.addSliceColorButton.Name = "addSliceColorButton";
            this.addSliceColorButton.Size = new System.Drawing.Size(75, 23);
            this.addSliceColorButton.TabIndex = 5;
            this.addSliceColorButton.Text = "Set Color";
            this.addSliceColorButton.UseVisualStyleBackColor = true;
            this.addSliceColorButton.Click += new System.EventHandler(this.addSliceColorButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Color:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Size:";
            // 
            // addSliceSizeTextBox
            // 
            this.addSliceSizeTextBox.Location = new System.Drawing.Point(52, 47);
            this.addSliceSizeTextBox.Name = "addSliceSizeTextBox";
            this.addSliceSizeTextBox.Size = new System.Drawing.Size(100, 20);
            this.addSliceSizeTextBox.TabIndex = 2;
            // 
            // addSliceNameTextBox
            // 
            this.addSliceNameTextBox.Location = new System.Drawing.Point(52, 20);
            this.addSliceNameTextBox.Name = "addSliceNameTextBox";
            this.addSliceNameTextBox.Size = new System.Drawing.Size(100, 20);
            this.addSliceNameTextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.removeSliceButton);
            this.groupBox2.Controls.Add(this.removeSliceNameTextBox);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(423, 398);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 130);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Remove Slice";
            // 
            // removeSliceButton
            // 
            this.removeSliceButton.Location = new System.Drawing.Point(32, 80);
            this.removeSliceButton.Name = "removeSliceButton";
            this.removeSliceButton.Size = new System.Drawing.Size(124, 23);
            this.removeSliceButton.TabIndex = 2;
            this.removeSliceButton.Text = "Remove Slice";
            this.removeSliceButton.UseVisualStyleBackColor = true;
            this.removeSliceButton.Click += new System.EventHandler(this.removeSliceButton_Click);
            // 
            // removeSliceNameTextBox
            // 
            this.removeSliceNameTextBox.Location = new System.Drawing.Point(50, 25);
            this.removeSliceNameTextBox.Name = "removeSliceNameTextBox";
            this.removeSliceNameTextBox.Size = new System.Drawing.Size(100, 20);
            this.removeSliceNameTextBox.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Name:";
            // 
            // pieChart1
            // 
            this.pieChart1.Location = new System.Drawing.Point(37, 25);
            this.pieChart1.Name = "pieChart1";
            this.pieChart1.SetArray = null;
            this.pieChart1.Size = new System.Drawing.Size(538, 352);
            this.pieChart1.TabIndex = 0;
            this.pieChart1.Load += new System.EventHandler(this.pieChart1_Load);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(673, 581);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pieChart1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private PieChart pieChart1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button addSliceButton;
        private System.Windows.Forms.Button addSliceColorButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox addSliceSizeTextBox;
        private System.Windows.Forms.TextBox addSliceNameTextBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button removeSliceButton;
        private System.Windows.Forms.TextBox removeSliceNameTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ColorDialog colorDialog1;
    }
}

