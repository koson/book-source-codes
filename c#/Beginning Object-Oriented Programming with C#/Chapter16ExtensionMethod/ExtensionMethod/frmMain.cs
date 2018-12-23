using System;
using System.Windows.Forms;
using StringExtensionMethods;



public class frmMain : Form
{
    private Label label1;
    private Label lblResult;
    private TextBox txtSSN;
    private Button btnVerify;
    private Button btnClose;

    #region Windows Code
    private void InitializeComponent()
    {
            this.label1 = new System.Windows.Forms.Label();
            this.lblResult = new System.Windows.Forms.Label();
            this.txtSSN = new System.Windows.Forms.TextBox();
            this.btnVerify = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "SSN:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblResult
            // 
            this.lblResult.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblResult.Location = new System.Drawing.Point(12, 29);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(100, 20);
            this.lblResult.TabIndex = 1;
            this.lblResult.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSSN
            // 
            this.txtSSN.Location = new System.Drawing.Point(118, 9);
            this.txtSSN.Name = "txtSSN";
            this.txtSSN.Size = new System.Drawing.Size(100, 20);
            this.txtSSN.TabIndex = 2;
            this.txtSSN.Text = "123-45-6789";
            // 
            // btnVerify
            // 
            this.btnVerify.Location = new System.Drawing.Point(12, 84);
            this.btnVerify.Name = "btnVerify";
            this.btnVerify.Size = new System.Drawing.Size(75, 23);
            this.btnVerify.TabIndex = 3;
            this.btnVerify.Text = "&Verify";
            this.btnVerify.UseVisualStyleBackColor = true;
            this.btnVerify.Click += new System.EventHandler(this.btnVerify_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(143, 84);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmMain
            // 
            this.ClientSize = new System.Drawing.Size(244, 132);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnVerify);
            this.Controls.Add(this.txtSSN);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.label1);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Verify SSN";
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

    private void btnVerify_Click(object sender, EventArgs e)
    {
        string str = txtSSN.Text;

        if (str.CheckValidSSN())
            lblResult.Text = "Valid";
        else
            lblResult.Text = "Invalid";

    }

    private void btnClose_Click(object sender, EventArgs e)
    {
        Close();
    }
}





