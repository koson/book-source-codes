using System;
using System.Windows.Forms;

public class frmMain : Form
{
    private TextBox txtMax;
    private Button btnCalc;
    private Button btnClose;
    private ListView lsvTable;
    private ColumnHeader columnHeader1;
    private ColumnHeader columnHeader2;
    private ColumnHeader columnHeader3;
    private Label label1;
    #region Windows code
    private void InitializeComponent()
    {
        this.label1 = new System.Windows.Forms.Label();
        this.txtMax = new System.Windows.Forms.TextBox();
        this.btnCalc = new System.Windows.Forms.Button();
        this.btnClose = new System.Windows.Forms.Button();
        this.lsvTable = new System.Windows.Forms.ListView();
        this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
        this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
        this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
        this.SuspendLayout();
        // 
        // label1
        // 
        this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.label1.Location = new System.Drawing.Point(12, 20);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(147, 20);
        this.label1.TabIndex = 0;
        this.label1.Text = "Table to run from 0 through:";
        this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // txtMax
        // 
        this.txtMax.Location = new System.Drawing.Point(165, 21);
        this.txtMax.Name = "txtMax";
        this.txtMax.Size = new System.Drawing.Size(47, 20);
        this.txtMax.TabIndex = 1;
        // 
        // btnCalc
        // 
        this.btnCalc.Location = new System.Drawing.Point(12, 54);
        this.btnCalc.Name = "btnCalc";
        this.btnCalc.Size = new System.Drawing.Size(75, 23);
        this.btnCalc.TabIndex = 2;
        this.btnCalc.Text = "Ca&lculate";
        this.btnCalc.UseVisualStyleBackColor = true;
        this.btnCalc.Click += new System.EventHandler(this.btnCalc_Click);
        // 
        // btnClose
        // 
        this.btnClose.Location = new System.Drawing.Point(137, 54);
        this.btnClose.Name = "btnClose";
        this.btnClose.Size = new System.Drawing.Size(75, 23);
        this.btnClose.TabIndex = 3;
        this.btnClose.Text = "&Close";
        this.btnClose.UseVisualStyleBackColor = true;
        this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
        // 
        // lsvTable
        // 
        this.lsvTable.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
        this.lsvTable.Location = new System.Drawing.Point(12, 93);
        this.lsvTable.Name = "lsvTable";
        this.lsvTable.Size = new System.Drawing.Size(201, 227);
        this.lsvTable.TabIndex = 4;
        this.lsvTable.UseCompatibleStateImageBehavior = false;
        this.lsvTable.View = System.Windows.Forms.View.Details;
        // 
        // columnHeader1
        // 
        this.columnHeader1.Text = "N";
        // 
        // columnHeader2
        // 
        this.columnHeader2.Text = "N * N";
        this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
        // 
        // columnHeader3
        // 
        this.columnHeader3.Text = "N * N * N";
        this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
        // 
        // frmMain
        // 
        this.ClientSize = new System.Drawing.Size(228, 332);
        this.Controls.Add(this.lsvTable);
        this.Controls.Add(this.btnClose);
        this.Controls.Add(this.btnCalc);
        this.Controls.Add(this.txtMax);
        this.Controls.Add(this.label1);
        this.Name = "frmMain";
        this.Text = "Table of Numbers";
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
        int number;
        int i;
        ListViewItem item;

        flag = int.TryParse(txtMax.Text, out number); // check input
        if (flag == false)
        {
            MessageBox.Show("Numeric data only.", "Input Error");
            txtMax.Focus();
            return;
        }
        if (number < 0)         // Make sure it's positive
        {
            number = number * -1;
        }
        number++;   // Do this because of N - 1 Rule

        int[,] myData = new int[number, 3];     // Define array

        for (i = 0; i < number; i++)
        {
            myData[i, 0] = i;           // first column of table
            myData[i, 1] = i * i;       // second column of table
            myData[i, 2] = i * i * i;   // third column of table
        }


        for (i = 0; i < number; i++)    // Now show it
        {
            item = new ListViewItem(myData[i, 0].ToString());
            item.SubItems.Add(myData[i, 1].ToString());
            item.SubItems.Add(myData[i, 2].ToString());
            lsvTable.Items.Add(item);
        }
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
        Close();
    }
}