using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

public class frmMain : Form
{
    private Label label1;
    private Label label2;
    private Label label3;
    private TextBox txtHomePhone;
    private TextBox txtCellPhone;
    private TextBox txtWorkPhone;
    private Button btnCheck;
    private Button btnExit;

   // Regex myRegex = new Regex();

    #region Windows Code
    private void InitializeComponent()
    {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtHomePhone = new System.Windows.Forms.TextBox();
            this.txtCellPhone = new System.Windows.Forms.TextBox();
            this.txtWorkPhone = new System.Windows.Forms.TextBox();
            this.btnCheck = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(12, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Home phone:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(12, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Cell phone:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Location = new System.Drawing.Point(12, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Work phone:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtHomePhone
            // 
            this.txtHomePhone.Location = new System.Drawing.Point(113, 28);
            this.txtHomePhone.Multiline = true;
            this.txtHomePhone.Name = "txtHomePhone";
            this.txtHomePhone.Size = new System.Drawing.Size(100, 20);
            this.txtHomePhone.TabIndex = 3;
            this.txtHomePhone.Leave += new System.EventHandler(this.txtHomePhone_Leave);
            // 
            // txtCellPhone
            // 
            this.txtCellPhone.Location = new System.Drawing.Point(113, 48);
            this.txtCellPhone.Multiline = true;
            this.txtCellPhone.Name = "txtCellPhone";
            this.txtCellPhone.Size = new System.Drawing.Size(100, 20);
            this.txtCellPhone.TabIndex = 4;
            this.txtCellPhone.Leave += new System.EventHandler(this.txtCellPhone_Leave);
            // 
            // txtWorkPhone
            // 
            this.txtWorkPhone.Location = new System.Drawing.Point(113, 67);
            this.txtWorkPhone.Multiline = true;
            this.txtWorkPhone.Name = "txtWorkPhone";
            this.txtWorkPhone.Size = new System.Drawing.Size(100, 20);
            this.txtWorkPhone.TabIndex = 5;
            this.txtWorkPhone.Leave += new System.EventHandler(this.txtWorkPhone_Leave);
            // 
            // btnCheck
            // 
            this.btnCheck.Location = new System.Drawing.Point(12, 113);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(75, 23);
            this.btnCheck.TabIndex = 6;
            this.btnCheck.Text = "&Check";
            this.btnCheck.UseVisualStyleBackColor = true;
            this.btnCheck.Visible = false;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(138, 113);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 7;
            this.btnExit.Text = "E&xit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // frmMain
            // 
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnCheck);
            this.Controls.Add(this.txtWorkPhone);
            this.Controls.Add(this.txtCellPhone);
            this.Controls.Add(this.txtHomePhone);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmMain";
            this.Text = "Check Phone Number";
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

    private void txtHomePhone_Leave(object sender, System.EventArgs e)
    {
        Regex regexObj = new Regex("\\d{3}-\\d{4}");

        if (regexObj.IsMatch(txtHomePhone.Text) == false)
        {
            // Wrong format
            MessageBox.Show("Phone number must be in XXX-NNNN format.", "Format Error");
            txtHomePhone.Focus();
            return;
        }
    }

    private void txtWorkPhone_Leave(object sender, EventArgs e)
    {
        Regex regexObj = new Regex("\\d{3}-\\d{4}");

        if (regexObj.IsMatch(txtWorkPhone.Text) == false)
        {
           
            // Wrong format
            MessageBox.Show("Phone number must be in XXX-NNNN format.", "Format Error");
            txtWorkPhone.Focus();
            return;
        }
   
    }

    private void txtCellPhone_Leave(object sender, EventArgs e)
    {
        Regex regexObj = new Regex("\\d{3}-\\d{4}");

        if (regexObj.IsMatch(txtCellPhone.Text) == false)
        {
            // Wrong format
            MessageBox.Show("Phone number must be in XXX-NNNN format.", "Format Error");
            txtCellPhone.Focus();
            return;
        }
     
    }

    private void btnExit_Click(object sender, EventArgs e)
    {
        Close();
    }


}


