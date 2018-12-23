using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


public class frmCreateDB : Form
{
    string dbName;
    private TextBox txtDBName;
    private Button btnCreateDB;
    private Button btnClose;
    private Label label1;

    #region Init code
    private void InitializeComponent()
    {
        this.txtDBName = new System.Windows.Forms.TextBox();
        this.label1 = new System.Windows.Forms.Label();
        this.btnCreateDB = new System.Windows.Forms.Button();
        this.btnClose = new System.Windows.Forms.Button();
        this.SuspendLayout();
        // 
        // txtDBName
        // 
        this.txtDBName.Location = new System.Drawing.Point(145, 31);
        this.txtDBName.Name = "txtDBName";
        this.txtDBName.Size = new System.Drawing.Size(170, 20);
        this.txtDBName.TabIndex = 3;
        this.txtDBName.Text = "jacksdb";
        // 
        // label1
        // 
        this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.label1.Location = new System.Drawing.Point(12, 30);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(127, 20);
        this.label1.TabIndex = 2;
        this.label1.Text = "New Database name:";
        this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // btnCreateDB
        // 
        this.btnCreateDB.Location = new System.Drawing.Point(12, 68);
        this.btnCreateDB.Name = "btnCreateDB";
        this.btnCreateDB.Size = new System.Drawing.Size(106, 23);
        this.btnCreateDB.TabIndex = 4;
        this.btnCreateDB.Text = "C&reate New DB";
        this.btnCreateDB.UseVisualStyleBackColor = true;
        this.btnCreateDB.Click += new System.EventHandler(this.btnCreateDB_Click);
        // 
        // btnClose
        // 
        this.btnClose.Location = new System.Drawing.Point(209, 68);
        this.btnClose.Name = "btnClose";
        this.btnClose.Size = new System.Drawing.Size(106, 23);
        this.btnClose.TabIndex = 5;
        this.btnClose.Text = "&Close";
        this.btnClose.UseVisualStyleBackColor = true;
        this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
        // 
        // frmCreateDB
        // 
        this.ClientSize = new System.Drawing.Size(342, 126);
        this.Controls.Add(this.btnClose);
        this.Controls.Add(this.btnCreateDB);
        this.Controls.Add(this.txtDBName);
        this.Controls.Add(this.label1);
        this.Name = "frmCreateDB";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Create New Database";
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    public frmCreateDB(string dbn)
    {
        InitializeComponent();
        dbName = dbn;
    }


    private void btnClose_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void btnCreateDB_Click(object sender, EventArgs e)
    {
        int flag;

        if (txtDBName.Text.Length == 0)
        {
            MessageBox.Show("You must enter a database name.", "Input Error");
            txtDBName.Focus();
            return;
        }

        clsDB myDB = new clsDB(txtDBName.Text); // Pass in new name
        flag = myDB.CreateNewDB(txtDBName.Text);
        if (flag == 1)
        {
            MessageBox.Show("New database created successfully.");
        }
        else
        {
            MessageBox.Show("Could not create new database.");
        }
        dbName = txtDBName.Text;
    }

}

