using System;
using System.Windows.Forms;
using System.Collections;

public class frmMain : Form
{
    private Label label1;
    private TextBox txtName;
    private Button btnAdd;
    private Button btnShow;
    private Button btnClose;
    private ListBox lstNames;
    ArrayList names = new ArrayList();

    #region Windows code
    public void InitializeComponent()
    {
        this.label1 = new System.Windows.Forms.Label();
        this.txtName = new System.Windows.Forms.TextBox();
        this.btnAdd = new System.Windows.Forms.Button();
        this.btnShow = new System.Windows.Forms.Button();
        this.btnClose = new System.Windows.Forms.Button();
        this.lstNames = new System.Windows.Forms.ListBox();
        this.SuspendLayout();
        // 
        // label1
        // 
        this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.label1.Location = new System.Drawing.Point(12, 20);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(100, 20);
        this.label1.TabIndex = 0;
        this.label1.Text = "Enter name:";
        this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // txtName
        // 
        this.txtName.Location = new System.Drawing.Point(118, 21);
        this.txtName.Name = "txtName";
        this.txtName.Size = new System.Drawing.Size(162, 20);
        this.txtName.TabIndex = 1;
        // 
        // btnAdd
        // 
        this.btnAdd.Location = new System.Drawing.Point(12, 61);
        this.btnAdd.Name = "btnAdd";
        this.btnAdd.Size = new System.Drawing.Size(75, 23);
        this.btnAdd.TabIndex = 2;
        this.btnAdd.Text = "&Add";
        this.btnAdd.UseVisualStyleBackColor = true;
        this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
        // 
        // btnShow
        // 
        this.btnShow.Location = new System.Drawing.Point(93, 61);
        this.btnShow.Name = "btnShow";
        this.btnShow.Size = new System.Drawing.Size(75, 23);
        this.btnShow.TabIndex = 3;
        this.btnShow.Text = "&Show";
        this.btnShow.UseVisualStyleBackColor = true;
        this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
        // 
        // btnClose
        // 
        this.btnClose.Location = new System.Drawing.Point(205, 61);
        this.btnClose.Name = "btnClose";
        this.btnClose.Size = new System.Drawing.Size(75, 23);
        this.btnClose.TabIndex = 4;
        this.btnClose.Text = "&Close";
        this.btnClose.UseVisualStyleBackColor = true;
        this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
        // 
        // lstNames
        // 
        this.lstNames.FormattingEnabled = true;
        this.lstNames.Location = new System.Drawing.Point(12, 111);
        this.lstNames.Name = "lstNames";
        this.lstNames.Size = new System.Drawing.Size(268, 147);
        this.lstNames.TabIndex = 5;
        // 
        // frmMain
        // 
        this.ClientSize = new System.Drawing.Size(292, 266);
        this.Controls.Add(this.lstNames);
        this.Controls.Add(this.btnClose);
        this.Controls.Add(this.btnShow);
        this.Controls.Add(this.btnAdd);
        this.Controls.Add(this.txtName);
        this.Controls.Add(this.label1);
        this.Name = "frmMain";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Use ArrayLists";
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

    private void btnAdd_Click(object sender, EventArgs e)
    {
        if (txtName.Text.Length != 0)
        {
            names.Add(txtName.Text);    // Add new name
            txtName.Clear();            // Clear it out
            txtName.Focus();            // Get ready for another name
        }
        else
        {
            MessageBox.Show("Please enter a name.", "Input Error");
            return;
        }
    }

    private void btnShow_Click(object sender, EventArgs e)
    {
        foreach (string str in names)
        {
            lstNames.Items.Add(str);
        }
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
        Close();
    }
}
