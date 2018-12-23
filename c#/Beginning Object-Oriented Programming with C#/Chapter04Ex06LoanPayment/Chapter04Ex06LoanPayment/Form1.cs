using System;
using System.Windows.Forms;

public class frmMain : Form
{
    private TextBox txtAmount;
    private TextBox txtMonths;
    private Label label2;
    private Button btnCalc;
    private Button btnExit;
    private TextBox txtResult;
    private TextBox txtInterestRate;
    private Label label3;
    private Label label1;
    #region Windows code
    private void InitializeComponent()
    {
            this.label1 = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.txtMonths = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCalc = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.txtInterestRate = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(5, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(169, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Loan amount:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(180, 22);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(100, 20);
            this.txtAmount.TabIndex = 1;
            this.txtAmount.Text = "150000";
            // 
            // txtMonths
            // 
            this.txtMonths.Location = new System.Drawing.Point(180, 48);
            this.txtMonths.Name = "txtMonths";
            this.txtMonths.Size = new System.Drawing.Size(100, 20);
            this.txtMonths.TabIndex = 3;
            this.txtMonths.Text = "360";
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(5, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(169, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Number of months for loan:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnCalc
            // 
            this.btnCalc.Location = new System.Drawing.Point(5, 167);
            this.btnCalc.Name = "btnCalc";
            this.btnCalc.Size = new System.Drawing.Size(75, 23);
            this.btnCalc.TabIndex = 4;
            this.btnCalc.Text = "&Calculate";
            this.btnCalc.UseVisualStyleBackColor = true;
            this.btnCalc.Click += new System.EventHandler(this.btnCalc_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(205, 167);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "E&xit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(5, 129);
            this.txtResult.Name = "txtResult";
            this.txtResult.ReadOnly = true;
            this.txtResult.Size = new System.Drawing.Size(275, 20);
            this.txtResult.TabIndex = 6;
            this.txtResult.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtResult.Visible = false;
            // 
            // txtInterestRate
            // 
            this.txtInterestRate.Location = new System.Drawing.Point(180, 74);
            this.txtInterestRate.Name = "txtInterestRate";
            this.txtInterestRate.Size = new System.Drawing.Size(100, 20);
            this.txtInterestRate.TabIndex = 8;
            this.txtInterestRate.Text = "6";
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Location = new System.Drawing.Point(5, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(169, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "interest rate (e.g., .06):";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frmMain
            // 
            this.ClientSize = new System.Drawing.Size(292, 222);
            this.Controls.Add(this.txtInterestRate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnCalc);
            this.Controls.Add(this.txtMonths);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.label1);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Figure Sales Tax Due";
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

        decimal amount;
        decimal months;
        decimal interest;
        decimal denominator;
        decimal payment;


        flag = decimal.TryParse(txtAmount.Text, out amount);
        if (flag == false) {
            MessageBox.Show("Enter a whole number", "Input Error");
            txtAmount.Focus();
            return;
        }

        flag = decimal.TryParse(txtMonths.Text, out months);
        if (flag == false) {
            MessageBox.Show("Enter a whole number", "Input Error");
            txtMonths.Focus();
            return;
        }
        flag = decimal.TryParse(txtInterestRate.Text, out interest);
        if (flag == false) {
            MessageBox.Show("Enter decimal number: 6 percent interest rate is .06", "Input Error");
            txtInterestRate.Focus();
            return;
        }

        /*
         *   payment = (rate +  (interestRate / (1.0 + interestrate)^months - 1) * amount
                       
         */

        interest /= 1200.0M;
        denominator = (decimal) (Math.Pow((double) (1.0M + interest), (double) months) - 1.0D);
        payment = (interest + (interest / denominator)) * amount;


        txtResult.Text = "The monthly payment is: " + payment.ToString("C");
        txtResult.Visible = true;

    }

    private void btnExit_Click(object sender, EventArgs e)
    {
        Close();
    }

}
