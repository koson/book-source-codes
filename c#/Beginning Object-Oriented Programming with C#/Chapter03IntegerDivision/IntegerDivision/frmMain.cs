using System;
using System.Windows.Forms;

public class frmMain : Form
{
  private TextBox txtOperand1;
  private TextBox txtOperand2;
  private Label label2;
  private Button btnCalc;
  private Button btnExit;
  private TextBox txtResult;
  private Label label1;
  #region Windows code
  private void InitializeComponent()
  {
    this.label1 = new System.Windows.Forms.Label();
    this.txtOperand1 = new System.Windows.Forms.TextBox();
    this.txtOperand2 = new System.Windows.Forms.TextBox();
    this.label2 = new System.Windows.Forms.Label();
    this.btnCalc = new System.Windows.Forms.Button();
    this.btnExit = new System.Windows.Forms.Button();
    this.txtResult = new System.Windows.Forms.TextBox();
    this.SuspendLayout();
    // 
    // label1
    // 
    this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
    this.label1.Location = new System.Drawing.Point(5, 22);
    this.label1.Name = "label1";
    this.label1.Size = new System.Drawing.Size(169, 20);
    this.label1.TabIndex = 0;
    this.label1.Text = "Enter first integer value:";
    this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
    // 
    // txtOperand1
    // 
    this.txtOperand1.Location = new System.Drawing.Point(180, 22);
    this.txtOperand1.Name = "txtOperand1";
    this.txtOperand1.Size = new System.Drawing.Size(100, 20);
    this.txtOperand1.TabIndex = 1;
    // 
    // txtOperand2
    // 
    this.txtOperand2.Location = new System.Drawing.Point(180, 48);
    this.txtOperand2.Name = "txtOperand2";
    this.txtOperand2.Size = new System.Drawing.Size(100, 20);
    this.txtOperand2.TabIndex = 3;
    // 
    // label2
    // 
    this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
    this.label2.Location = new System.Drawing.Point(5, 47);
    this.label2.Name = "label2";
    this.label2.Size = new System.Drawing.Size(169, 20);
    this.label2.TabIndex = 2;
    this.label2.Text = "Enter second integer value:";
    this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
    // 
    // btnCalc
    // 
    this.btnCalc.Location = new System.Drawing.Point(5, 120);
    this.btnCalc.Name = "btnCalc";
    this.btnCalc.Size = new System.Drawing.Size(75, 23);
    this.btnCalc.TabIndex = 4;
    this.btnCalc.Text = "&Calculate";
    this.btnCalc.UseVisualStyleBackColor = true;
    this.btnCalc.Click += new System.EventHandler(this.btnCalc_Click);
    // 
    // btnExit
    // 
    this.btnExit.Location = new System.Drawing.Point(205, 120);
    this.btnExit.Name = "btnExit";
    this.btnExit.Size = new System.Drawing.Size(75, 23);
    this.btnExit.TabIndex = 5;
    this.btnExit.Text = "E&xit";
    this.btnExit.UseVisualStyleBackColor = true;
    this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
    // 
    // txtResult
    // 
    this.txtResult.Location = new System.Drawing.Point(5, 82);
    this.txtResult.Name = "txtResult";
    this.txtResult.ReadOnly = true;
    this.txtResult.Size = new System.Drawing.Size(275, 20);
    this.txtResult.TabIndex = 6;
    this.txtResult.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
    this.txtResult.Visible = false;
    // 
    // frmMain
    // 
    this.ClientSize = new System.Drawing.Size(292, 165);
    this.Controls.Add(this.txtResult);
    this.Controls.Add(this.btnExit);
    this.Controls.Add(this.btnCalc);
    this.Controls.Add(this.txtOperand2);
    this.Controls.Add(this.label2);
    this.Controls.Add(this.txtOperand1);
    this.Controls.Add(this.label1);
    this.Name = "frmMain";
    this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
    this.Text = "Integer Division";
    this.ResumeLayout(false);
    this.PerformLayout();

  }
  #endregion

  public frmMain()
  {
    InitializeComponent();
    txtResult.Visible = false;
  }

  public static void Main()
  {
    frmMain main = new frmMain();
    Application.Run(main);
  }


  private void btnCalc_Click(object sender, EventArgs e)
  {
     
    bool flag;

    decimal operand1;
    decimal operand2;
    decimal answer;

    flag = decimal.TryParse(txtOperand1.Text, out operand1);
    if (flag == false)
    {
      MessageBox.Show("Enter a whole number", "Input Error");
      txtOperand1.Focus();
      return;
    }

    flag = decimal.TryParse(txtOperand2.Text, out operand2);
    if (flag == false)
    {
      MessageBox.Show("Enter a whole number", "Input Error");
      txtOperand2.Focus();
      return;
    }

    answer = operand1 / operand2;



    txtResult.Text = operand1.ToString() + " divided by " + operand2.ToString() + 
                     " equals " +answer;
    txtResult.Visible = true;

  }

  private void btnExit_Click(object sender, EventArgs e)
  {
    Close();
  }


}