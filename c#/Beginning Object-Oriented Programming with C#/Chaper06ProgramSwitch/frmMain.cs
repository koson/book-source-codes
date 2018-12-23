using System;
using System.Windows.Forms;

public class frmMain : Form
{
    private TextBox txtDay;
    private Button btnCalc;
    private Button btnExit;
    private Label lblResult;
    private Label label1;
    #region Windows Code
    private void InitializeComponent()
    {
            this.label1 = new System.Windows.Forms.Label();
            this.txtDay = new System.Windows.Forms.TextBox();
            this.btnCalc = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblResult = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(185, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Enter a day of the week (1 thru 7):";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDay
            // 
            this.txtDay.Location = new System.Drawing.Point(203, 20);
            this.txtDay.Name = "txtDay";
            this.txtDay.Size = new System.Drawing.Size(53, 20);
            this.txtDay.TabIndex = 1;
            // 
            // btnCalc
            // 
            this.btnCalc.Location = new System.Drawing.Point(12, 96);
            this.btnCalc.Name = "btnCalc";
            this.btnCalc.Size = new System.Drawing.Size(75, 23);
            this.btnCalc.TabIndex = 2;
            this.btnCalc.Text = "&Calc";
            this.btnCalc.UseVisualStyleBackColor = true;
            this.btnCalc.Click += new System.EventHandler(this.btnCalc_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(181, 96);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "E&xit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblResult
            // 
            this.lblResult.Location = new System.Drawing.Point(12, 58);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(244, 23);
            this.lblResult.TabIndex = 4;
            this.lblResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmMain
            // 
            this.ClientSize = new System.Drawing.Size(284, 144);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnCalc);
            this.Controls.Add(this.txtDay);
            this.Controls.Add(this.label1);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();

    }
    #endregion


    public frmMain()
    {
        InitializeComponent();
    }


    [STAThread]
    public static void Main()
    {
        frmMain main = new frmMain();
        Application.Run(main);
    }

    private void btnCalc_Click(object sender, EventArgs e)
    {
        bool flag;
        int myDay;
        string msg = "Today is ";   // No sense duplicating this 7 times

        flag = int.TryParse(txtDay.Text, out myDay);    // Make text into int
        if (flag == false)
        {
            MessageBox.Show("Numeric only, 1 thru 7");
            txtDay.Focus();                             // Send 'em back to try again
            return;
        }

        switch (myDay)
        {
            case 1:
                lblResult.Text = msg + "Monday";
                break;
            case 2:
                lblResult.Text = msg + "Tuesday";
                break;
            case 3:
                lblResult.Text = msg + "Wednesday";
                break;
            case 4:
                lblResult.Text = msg + "Thursday";
                break;
            case 5:
                lblResult.Text = msg + "Friday";
                break;
            case 6:
                lblResult.Text = msg + "Saturday";
                break;
            case 7:
                lblResult.Text = msg + "Sunday";
                break;
            default:
                lblResult.Text = "You should never get here";
                break;

        }
    }

    private void btnExit_Click(object sender, EventArgs e)
    {
        Close();
    }
}
