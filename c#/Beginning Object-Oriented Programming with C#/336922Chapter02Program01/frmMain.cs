using System;
using System.Windows.Forms;

public class frmMain : Form
{
    private TextBox txtName;
    private TextBox txtAddress;
    private Label label2;
    private TextBox txtCity;
    private Label label3;
    private Label label4;
    private TextBox txtZip;
    private Label label5;
    private Button btnDisplayOutput;
    private Button btnExit;
    private TextBox txtDisplayOutput;
    private TextBox txtState;
    private Label label1;
    #region Windows code
    private void InitializeComponent()
    {
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCity = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtZip = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnDisplayOutput = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.txtDisplayOutput = new System.Windows.Forms.TextBox();
            this.txtState = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(93, 18);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(297, 20);
            this.txtName.TabIndex = 1;
            this.txtName.Text = "Jane Holcar";
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(93, 44);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(297, 20);
            this.txtAddress.TabIndex = 3;
            this.txtAddress.Text = "123 Oak Street";
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(12, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Address:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCity
            // 
            this.txtCity.Location = new System.Drawing.Point(93, 71);
            this.txtCity.Name = "txtCity";
            this.txtCity.Size = new System.Drawing.Size(115, 20);
            this.txtCity.TabIndex = 5;
            this.txtCity.Text = "Anytown";
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Location = new System.Drawing.Point(12, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "City:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label4.Location = new System.Drawing.Point(211, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "State:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtZip
            // 
            this.txtZip.Location = new System.Drawing.Point(337, 71);
            this.txtZip.Name = "txtZip";
            this.txtZip.Size = new System.Drawing.Size(53, 20);
            this.txtZip.TabIndex = 9;
            this.txtZip.Text = "45245";
            // 
            // label5
            // 
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label5.Location = new System.Drawing.Point(292, 71);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 20);
            this.label5.TabIndex = 8;
            this.label5.Text = "Zip:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnDisplayOutput
            // 
            this.btnDisplayOutput.Location = new System.Drawing.Point(12, 112);
            this.btnDisplayOutput.Name = "btnDisplayOutput";
            this.btnDisplayOutput.Size = new System.Drawing.Size(75, 23);
            this.btnDisplayOutput.TabIndex = 10;
            this.btnDisplayOutput.Text = "Display";
            this.btnDisplayOutput.UseVisualStyleBackColor = true;
            this.btnDisplayOutput.Click += new System.EventHandler(this.btnDisplayOutput_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(315, 112);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 11;
            this.btnExit.Text = "E&xit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // txtDisplayOutput
            // 
            this.txtDisplayOutput.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDisplayOutput.Location = new System.Drawing.Point(12, 157);
            this.txtDisplayOutput.Multiline = true;
            this.txtDisplayOutput.Name = "txtDisplayOutput";
            this.txtDisplayOutput.ReadOnly = true;
            this.txtDisplayOutput.Size = new System.Drawing.Size(378, 97);
            this.txtDisplayOutput.TabIndex = 13;
            // 
            // txtState
            // 
            this.txtState.Location = new System.Drawing.Point(257, 70);
            this.txtState.Name = "txtState";
            this.txtState.Size = new System.Drawing.Size(29, 20);
            this.txtState.TabIndex = 6;
            this.txtState.Text = "OH";
            // 
            // frmMain
            // 
            this.ClientSize = new System.Drawing.Size(414, 266);
            this.Controls.Add(this.txtState);
            this.Controls.Add(this.txtDisplayOutput);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnDisplayOutput);
            this.Controls.Add(this.txtZip);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtCity);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label1);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mailing Label Program";
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

    private void btnDisplayOutput_Click(object sender, EventArgs e)
    {
        String buffer;

        buffer = "Mailing Label:" + Environment.NewLine + Environment.NewLine;
        buffer = buffer + "   Name: " + txtName.Text + Environment.NewLine;
        buffer = buffer + "Address: " + txtAddress.Text + Environment.NewLine;
        buffer = buffer + "   City: " + txtCity.Text + 
                        "  State: " + txtState.Text +
                        "  Zip: " + txtZip.Text;
        txtDisplayOutput.Text = buffer;
    }

    private void btnExit_Click(object sender, EventArgs e)
    {
        Close();
    }

}