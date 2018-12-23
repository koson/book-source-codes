using System;
using System.Windows.Forms;

public class frmMain : Form
{
  private Label label2;
  private TextBox txtStart;
  private TextBox txtEnd;
  private Button btnCalculate;
  private Button btnClear;
  private Button btnClose;
  private ListBox lstOutput;
  private Label label3;
  private Label label4;
  private Label label1;
  #region Windows code
  private void InitializeComponent()
  {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtStart = new System.Windows.Forms.TextBox();
            this.txtEnd = new System.Windows.Forms.TextBox();
            this.btnCalculate = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lstOutput = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(12, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ending table value:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Starting table value:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtStart
            // 
            this.txtStart.Location = new System.Drawing.Point(147, 9);
            this.txtStart.Name = "txtStart";
            this.txtStart.Size = new System.Drawing.Size(64, 20);
            this.txtStart.TabIndex = 2;
            this.txtStart.Text = "0";
            // 
            // txtEnd
            // 
            this.txtEnd.Location = new System.Drawing.Point(147, 30);
            this.txtEnd.Name = "txtEnd";
            this.txtEnd.Size = new System.Drawing.Size(64, 20);
            this.txtEnd.TabIndex = 3;
            this.txtEnd.Text = "100";
            // 
            // btnCalculate
            // 
            this.btnCalculate.Location = new System.Drawing.Point(12, 65);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Size = new System.Drawing.Size(64, 23);
            this.btnCalculate.TabIndex = 4;
            this.btnCalculate.Text = "Ca&lculate";
            this.btnCalculate.UseVisualStyleBackColor = true;
            this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(82, 65);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(70, 23);
            this.btnClear.TabIndex = 5;
            this.btnClear.Text = "Clea&r";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(158, 65);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(59, 23);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lstOutput
            // 
            this.lstOutput.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstOutput.FormattingEnabled = true;
            this.lstOutput.ItemHeight = 14;
            this.lstOutput.Location = new System.Drawing.Point(12, 140);
            this.lstOutput.Name = "lstOutput";
            this.lstOutput.ScrollAlwaysVisible = true;
            this.lstOutput.Size = new System.Drawing.Size(205, 144);
            this.lstOutput.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Location = new System.Drawing.Point(12, 117);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "Number";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label4.Location = new System.Drawing.Point(93, 117);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 20);
            this.label4.TabIndex = 9;
            this.label4.Text = "Number Squared";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmMain
            // 
            this.ClientSize = new System.Drawing.Size(229, 296);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lstOutput);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnCalculate);
            this.Controls.Add(this.txtEnd);
            this.Controls.Add(this.txtStart);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Table of Squares";
            this.ResumeLayout(false);
            this.PerformLayout();

  }
  #endregion

  public frmMain()
  {
    InitializeComponent();
  }

  public static void Main()
  {
    frmMain main = new frmMain();
    Application.Run(main);
  }

  private void btnCalculate_Click(object sender, EventArgs e)
  {
    bool flag;
    int i;
    int start;
    int end;
    string buff;

    //======================= Gather inputs ======================
    flag = int.TryParse(txtStart.Text, out start);    // Convert start from text to int
    if (flag == false)
    {
      MessageBox.Show("Numeric data only", "Input Error");
      txtStart.Focus();
      return;
    }

    flag = int.TryParse(txtEnd.Text, out end);    // Convert end from text to int
    if (flag == false)
    {
      MessageBox.Show("Numeric data only", "Input Error");
      txtEnd.Focus();
      return;
    }

    if (start >= end)                             // Reasonable values?
    {
        MessageBox.Show("Start should be less than end.", "Input Error");
        txtStart.Focus();
        return;
    }

    //====================== Process and Display ==============

    for (i = start; i <= end; i++)
    {
      buff = string.Format("{0, 5}{1, 20}", i, i * i);
      lstOutput.Items.Add(buff);
    }


  }

  private void btnClose_Click(object sender, EventArgs e)
  {
    Close();
  }

  private void btnClear_Click(object sender, EventArgs e)
  {
    txtStart.Clear();
    txtEnd.Clear();
    lstOutput.Items.Clear();
  }
}