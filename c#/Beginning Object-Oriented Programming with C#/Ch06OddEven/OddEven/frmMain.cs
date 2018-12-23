using System;
using System.Windows.Forms;

public class frmMain : Form
{
  private TextBox txtNumber;
  private Button btnCalc;
  private Button btnClose;
  private Label lblOutput;
  private Label label1;
  #region Windows code
  private void InitializeComponent()
  {
      this.label1 = new System.Windows.Forms.Label();
      this.txtNumber = new System.Windows.Forms.TextBox();
      this.btnCalc = new System.Windows.Forms.Button();
      this.btnClose = new System.Windows.Forms.Button();
      this.lblOutput = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.label1.Location = new System.Drawing.Point(12, 20);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(128, 20);
      this.label1.TabIndex = 0;
      this.label1.Text = "Enter a whole number:";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // txtNumber
      // 
      this.txtNumber.Location = new System.Drawing.Point(146, 21);
      this.txtNumber.Name = "txtNumber";
      this.txtNumber.Size = new System.Drawing.Size(100, 20);
      this.txtNumber.TabIndex = 1;
      // 
      // btnCalc
      // 
      this.btnCalc.Location = new System.Drawing.Point(12, 90);
      this.btnCalc.Name = "btnCalc";
      this.btnCalc.Size = new System.Drawing.Size(75, 23);
      this.btnCalc.TabIndex = 2;
      this.btnCalc.Text = "Ca&lculate";
      this.btnCalc.UseVisualStyleBackColor = true;
      this.btnCalc.Click += new System.EventHandler(this.btnCalc_Click);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(171, 90);
      this.btnClose.Name = "btnClose";
      this.btnClose.Size = new System.Drawing.Size(75, 23);
      this.btnClose.TabIndex = 3;
      this.btnClose.Text = "&Close";
      this.btnClose.UseVisualStyleBackColor = true;
      this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
      // 
      // lblOutput
      // 
      this.lblOutput.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.lblOutput.Location = new System.Drawing.Point(12, 55);
      this.lblOutput.Name = "lblOutput";
      this.lblOutput.Size = new System.Drawing.Size(234, 20);
      this.lblOutput.TabIndex = 4;
      this.lblOutput.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // frmMain
      // 
      this.ClientSize = new System.Drawing.Size(262, 135);
      this.Controls.Add(this.lblOutput);
      this.Controls.Add(this.btnClose);
      this.Controls.Add(this.btnCalc);
      this.Controls.Add(this.txtNumber);
      this.Controls.Add(this.label1);
      this.Name = "frmMain";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Odd or Even number";
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

  private void btnCalc_Click(object sender, EventArgs e)
  {
    bool flag;
    int val;
    string output = "Number is even";   // Default response

    // Convert from text to number
    flag = int.TryParse(txtNumber.Text, out val);
    if (flag == false)
    {
      MessageBox.Show("Not a number. Re-enter.");
      txtNumber.Clear();
      txtNumber.Focus();
      return;
    }

    // See if odd or even
    if (val % 2 == 1)
    {
      output = "Number is odd";
    }
    // Show result
    lblOutput.Text = output;

  }

    private void btnClose_Click(object sender, EventArgs e)
    {
        Close();
    }
}