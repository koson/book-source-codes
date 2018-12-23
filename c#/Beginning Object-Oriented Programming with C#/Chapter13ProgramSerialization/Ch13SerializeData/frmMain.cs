using System;
using System.Windows.Forms;

public class frmMain : Form
{
    clsSerial myFriend = new clsSerial();

    private Label label1;
    private TextBox txtName;
    private TextBox txtEmail;
    private TextBox txtStatus;
    private Label label3;
    private Button btnSerial;
    private Button btnDisplay;
    private Button btnClose;
    private ListBox lstOutput;
    private Label label2;
    #region Windows code
    private void InitializeComponent()
    {
        this.label1 = new System.Windows.Forms.Label();
        this.txtName = new System.Windows.Forms.TextBox();
        this.txtEmail = new System.Windows.Forms.TextBox();
        this.label2 = new System.Windows.Forms.Label();
        this.txtStatus = new System.Windows.Forms.TextBox();
        this.label3 = new System.Windows.Forms.Label();
        this.btnSerial = new System.Windows.Forms.Button();
        this.btnDisplay = new System.Windows.Forms.Button();
        this.btnClose = new System.Windows.Forms.Button();
        this.lstOutput = new System.Windows.Forms.ListBox();
        this.SuspendLayout();
        // 
        // label1
        // 
        this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.label1.Location = new System.Drawing.Point(26, 9);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(74, 20);
        this.label1.TabIndex = 0;
        this.label1.Text = "Name:";
        this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // txtName
        // 
        this.txtName.Location = new System.Drawing.Point(106, 9);
        this.txtName.Name = "txtName";
        this.txtName.Size = new System.Drawing.Size(219, 20);
        this.txtName.TabIndex = 1;
        this.txtName.Text = "John Smith";
        // 
        // txtEmail
        // 
        this.txtEmail.Location = new System.Drawing.Point(106, 35);
        this.txtEmail.Name = "txtEmail";
        this.txtEmail.Size = new System.Drawing.Size(219, 20);
        this.txtEmail.TabIndex = 3;
        this.txtEmail.Text = "jsmith@email.com";
        // 
        // label2
        // 
        this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.label2.Location = new System.Drawing.Point(26, 35);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(74, 20);
        this.label2.TabIndex = 2;
        this.label2.Text = "Email:";
        this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // txtStatus
        // 
        this.txtStatus.Location = new System.Drawing.Point(106, 61);
        this.txtStatus.Name = "txtStatus";
        this.txtStatus.Size = new System.Drawing.Size(38, 20);
        this.txtStatus.TabIndex = 5;
        this.txtStatus.Text = "1";
        // 
        // label3
        // 
        this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.label3.Location = new System.Drawing.Point(26, 61);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(74, 20);
        this.label3.TabIndex = 4;
        this.label3.Text = "Status:";
        this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // btnSerial
        // 
        this.btnSerial.Location = new System.Drawing.Point(26, 97);
        this.btnSerial.Name = "btnSerial";
        this.btnSerial.Size = new System.Drawing.Size(75, 23);
        this.btnSerial.TabIndex = 6;
        this.btnSerial.Text = "&Serialize";
        this.btnSerial.UseVisualStyleBackColor = true;
        this.btnSerial.Click += new System.EventHandler(this.btnSerial_Click);
        // 
        // btnDisplay
        // 
        this.btnDisplay.Location = new System.Drawing.Point(146, 97);
        this.btnDisplay.Name = "btnDisplay";
        this.btnDisplay.Size = new System.Drawing.Size(75, 23);
        this.btnDisplay.TabIndex = 7;
        this.btnDisplay.Text = "&Display";
        this.btnDisplay.UseVisualStyleBackColor = true;
        this.btnDisplay.Click += new System.EventHandler(this.btnDisplay_Click);
        // 
        // btnClose
        // 
        this.btnClose.Location = new System.Drawing.Point(250, 97);
        this.btnClose.Name = "btnClose";
        this.btnClose.Size = new System.Drawing.Size(75, 23);
        this.btnClose.TabIndex = 8;
        this.btnClose.Text = "&Close";
        this.btnClose.UseVisualStyleBackColor = true;
        this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
        // 
        // lstOutput
        // 
        this.lstOutput.FormattingEnabled = true;
        this.lstOutput.Location = new System.Drawing.Point(26, 136);
        this.lstOutput.Name = "lstOutput";
        this.lstOutput.Size = new System.Drawing.Size(299, 121);
        this.lstOutput.TabIndex = 9;
        // 
        // frmMain
        // 
        this.ClientSize = new System.Drawing.Size(355, 266);
        this.Controls.Add(this.lstOutput);
        this.Controls.Add(this.btnClose);
        this.Controls.Add(this.btnDisplay);
        this.Controls.Add(this.btnSerial);
        this.Controls.Add(this.txtStatus);
        this.Controls.Add(this.label3);
        this.Controls.Add(this.txtEmail);
        this.Controls.Add(this.label2);
        this.Controls.Add(this.txtName);
        this.Controls.Add(this.label1);
        this.Name = "frmMain";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Serialize -  Deserialize";
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

    private void btnSerial_Click(object sender, EventArgs e)
    {
        int flag;

        MoveTextToClass(myFriend);
        flag = myFriend.SerializeFriend(myFriend);
        if (flag == 1)
        {
            MessageBox.Show("Data Serialized successfully", "Data Write");
        }
        else
        {
            MessageBox.Show("Serialization failure", "Data Error");
        }
    }

    private void btnDisplay_Click(object sender, EventArgs e)
    {
        clsSerial newFriend = new clsSerial();
        newFriend = newFriend.DeserializeFriend();
        lstOutput.Items.Clear();
        lstOutput.Items.Add(newFriend.Name);
        lstOutput.Items.Add(newFriend.Email);
        lstOutput.Items.Add(newFriend.Status.ToString());

    }

    private void MoveTextToClass(clsSerial obj)
    {
        bool flag;
        int val;

        obj.Name = txtName.Text;
        obj.Email = txtEmail.Text;
        flag = int.TryParse(txtStatus.Text, out val);
        if (flag == false)
        {
            MessageBox.Show("Must be 1 or 0", "Input Error");
            txtStatus.Focus();
            return;
        }
        obj.Status = val;
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
        Close();
    }
}