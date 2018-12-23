using System;
using System.Windows.Forms;

public class frmMain : Form
{
    private TextBox txtYear;
    private Button btnCalc;
    private Button btnClose;
    private Label lblLeapYearResult;
    private Label lblEasterResult;
    private Label label1;
    #region Windows code
    private void InitializeComponent()
    {
        this.label1 = new System.Windows.Forms.Label();
        this.txtYear = new System.Windows.Forms.TextBox();
        this.btnCalc = new System.Windows.Forms.Button();
        this.btnClose = new System.Windows.Forms.Button();
        this.lblLeapYearResult = new System.Windows.Forms.Label();
        this.lblEasterResult = new System.Windows.Forms.Label();
        this.SuspendLayout();
        // 
        // label1
        // 
        this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.label1.Location = new System.Drawing.Point(12, 19);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(100, 20);
        this.label1.TabIndex = 0;
        this.label1.Text = "Enter year:";
        this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // txtYear
        // 
        this.txtYear.Location = new System.Drawing.Point(118, 19);
        this.txtYear.Name = "txtYear";
        this.txtYear.Size = new System.Drawing.Size(100, 20);
        this.txtYear.TabIndex = 1;
        // 
        // btnCalc
        // 
        this.btnCalc.Location = new System.Drawing.Point(12, 62);
        this.btnCalc.Name = "btnCalc";
        this.btnCalc.Size = new System.Drawing.Size(75, 23);
        this.btnCalc.TabIndex = 2;
        this.btnCalc.Text = "Ca&lculate";
        this.btnCalc.UseVisualStyleBackColor = true;
        this.btnCalc.Click += new System.EventHandler(this.btnCalc_Click);
        // 
        // btnClose
        // 
        this.btnClose.Location = new System.Drawing.Point(143, 62);
        this.btnClose.Name = "btnClose";
        this.btnClose.Size = new System.Drawing.Size(75, 23);
        this.btnClose.TabIndex = 3;
        this.btnClose.Text = "&Close";
        this.btnClose.UseVisualStyleBackColor = true;
        this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
        // 
        // lblLeapYearResult
        // 
        this.lblLeapYearResult.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lblLeapYearResult.Location = new System.Drawing.Point(12, 109);
        this.lblLeapYearResult.Name = "lblLeapYearResult";
        this.lblLeapYearResult.Size = new System.Drawing.Size(206, 20);
        this.lblLeapYearResult.TabIndex = 4;
        this.lblLeapYearResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // lblEasterResult
        // 
        this.lblEasterResult.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lblEasterResult.Location = new System.Drawing.Point(12, 139);
        this.lblEasterResult.Name = "lblEasterResult";
        this.lblEasterResult.Size = new System.Drawing.Size(206, 20);
        this.lblEasterResult.TabIndex = 5;
        this.lblEasterResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // frmMain
        // 
        this.ClientSize = new System.Drawing.Size(233, 191);
        this.Controls.Add(this.lblEasterResult);
        this.Controls.Add(this.lblLeapYearResult);
        this.Controls.Add(this.btnClose);
        this.Controls.Add(this.btnCalc);
        this.Controls.Add(this.txtYear);
        this.Controls.Add(this.label1);
        this.Name = "frmMain";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Class Design";
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
        int year;
        int leap;
        //clsDates myDate = new clsDates();

        flag = int.TryParse(txtYear.Text, out year);    // Convert and validate int
        if (flag == false)
        {
            MessageBox.Show("Digit characters only in YYYY format.", "Input Error");
            txtYear.Focus();
            return;
        }
        clsDates myDate = new clsDates(year);
        
        leap = myDate.getLeapYear(year);
        lblEasterResult.Text = myDate.getEaster(year);        
        lblLeapYearResult.Text = year.ToString() + " is " + ((leap == 1)? "":"not ") + "a leap year";

    }

    private void btnClose_Click(object sender, EventArgs e)
    {
        Close();
    }
}