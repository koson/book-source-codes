using System;
using System.Windows.Forms;
using System.Collections.Generic;

public class frmMain : Form
{
    const int INTEGER = 1;          // Symbolic constants
    const int LONG = 2;
    const int DOUBLE = 3;
    const int STRING = 4;
    const int MAXVALUE = 1000;

    const int UNSORTED = 1;
    const int SORTED = 2;

    int items;                      // Values to generate
    int choice;                     // Which data type selected
    int whichListbox;               // Which listbox getting this data

    int[] iData;                    // Data arrays
    long[] lData;
    double[] dData;
    string[] sData;

    Random rnd = new Random();

    private GroupBox groupBox1;
    private RadioButton rbString;
    private RadioButton rbDouble;
    private RadioButton rbLong;
    private Label label1;
    private TextBox txtItems;
    private TextBox txtString;
    private ListBox lstUnsorted;
    private ListBox lstSorted;
    private Button btnRaw;
    private Button btnSort;
    private Button btnClose;
    private RadioButton rbInt;
    #region Windows code
    private void InitializeComponent()
    {
        this.groupBox1 = new System.Windows.Forms.GroupBox();
        this.rbString = new System.Windows.Forms.RadioButton();
        this.rbDouble = new System.Windows.Forms.RadioButton();
        this.rbLong = new System.Windows.Forms.RadioButton();
        this.rbInt = new System.Windows.Forms.RadioButton();
        this.label1 = new System.Windows.Forms.Label();
        this.txtItems = new System.Windows.Forms.TextBox();
        this.txtString = new System.Windows.Forms.TextBox();
        this.lstUnsorted = new System.Windows.Forms.ListBox();
        this.lstSorted = new System.Windows.Forms.ListBox();
        this.btnRaw = new System.Windows.Forms.Button();
        this.btnSort = new System.Windows.Forms.Button();
        this.btnClose = new System.Windows.Forms.Button();
        this.groupBox1.SuspendLayout();
        this.SuspendLayout();
        // 
        // groupBox1
        // 
        this.groupBox1.Controls.Add(this.rbString);
        this.groupBox1.Controls.Add(this.rbDouble);
        this.groupBox1.Controls.Add(this.rbLong);
        this.groupBox1.Controls.Add(this.rbInt);
        this.groupBox1.Location = new System.Drawing.Point(12, 12);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new System.Drawing.Size(200, 100);
        this.groupBox1.TabIndex = 0;
        this.groupBox1.TabStop = false;
        this.groupBox1.Text = "Data Type to Sort:";
        // 
        // rbString
        // 
        this.rbString.AutoSize = true;
        this.rbString.Location = new System.Drawing.Point(105, 61);
        this.rbString.Name = "rbString";
        this.rbString.Size = new System.Drawing.Size(52, 17);
        this.rbString.TabIndex = 3;
        this.rbString.TabStop = true;
        this.rbString.Text = "String";
        this.rbString.UseVisualStyleBackColor = true;
        this.rbString.Click += new System.EventHandler(this.rbString_Click);
        // 
        // rbDouble
        // 
        this.rbDouble.AutoSize = true;
        this.rbDouble.Location = new System.Drawing.Point(105, 28);
        this.rbDouble.Name = "rbDouble";
        this.rbDouble.Size = new System.Drawing.Size(59, 17);
        this.rbDouble.TabIndex = 2;
        this.rbDouble.TabStop = true;
        this.rbDouble.Text = "Double";
        this.rbDouble.UseVisualStyleBackColor = true;
        this.rbDouble.Click += new System.EventHandler(this.rbDouble_Click);
        // 
        // rbLong
        // 
        this.rbLong.AutoSize = true;
        this.rbLong.Location = new System.Drawing.Point(16, 61);
        this.rbLong.Name = "rbLong";
        this.rbLong.Size = new System.Drawing.Size(49, 17);
        this.rbLong.TabIndex = 1;
        this.rbLong.TabStop = true;
        this.rbLong.Text = "Long";
        this.rbLong.UseVisualStyleBackColor = true;
        this.rbLong.Click += new System.EventHandler(this.rbLong_Click);
        // 
        // rbInt
        // 
        this.rbInt.AutoSize = true;
        this.rbInt.Location = new System.Drawing.Point(16, 28);
        this.rbInt.Name = "rbInt";
        this.rbInt.Size = new System.Drawing.Size(58, 17);
        this.rbInt.TabIndex = 0;
        this.rbInt.TabStop = true;
        this.rbInt.Text = "Integer";
        this.rbInt.UseVisualStyleBackColor = true;
        this.rbInt.Click += new System.EventHandler(this.rbInt_Click);
        // 
        // label1
        // 
        this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.label1.Location = new System.Drawing.Point(269, 12);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(53, 20);
        this.label1.TabIndex = 1;
        this.label1.Text = "Items:";
        this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // txtItems
        // 
        this.txtItems.Location = new System.Drawing.Point(328, 12);
        this.txtItems.Name = "txtItems";
        this.txtItems.Size = new System.Drawing.Size(63, 20);
        this.txtItems.TabIndex = 2;
        this.txtItems.Text = "10";
        // 
        // txtString
        // 
        this.txtString.Location = new System.Drawing.Point(14, 127);
        this.txtString.Name = "txtString";
        this.txtString.Size = new System.Drawing.Size(377, 20);
        this.txtString.TabIndex = 3;
        this.txtString.Text = "Tammy,Tom,Abe,Fred,Issy,Pam,Ted,Diane,Hailey,Katie";
        // 
        // lstUnsorted
        // 
        this.lstUnsorted.FormattingEnabled = true;
        this.lstUnsorted.Location = new System.Drawing.Point(14, 210);
        this.lstUnsorted.Name = "lstUnsorted";
        this.lstUnsorted.Size = new System.Drawing.Size(178, 225);
        this.lstUnsorted.TabIndex = 4;
        // 
        // lstSorted
        // 
        this.lstSorted.FormattingEnabled = true;
        this.lstSorted.Location = new System.Drawing.Point(213, 210);
        this.lstSorted.Name = "lstSorted";
        this.lstSorted.Size = new System.Drawing.Size(178, 225);
        this.lstSorted.TabIndex = 5;
        // 
        // btnRaw
        // 
        this.btnRaw.Location = new System.Drawing.Point(17, 168);
        this.btnRaw.Name = "btnRaw";
        this.btnRaw.Size = new System.Drawing.Size(111, 23);
        this.btnRaw.TabIndex = 6;
        this.btnRaw.Text = "Show &Unsorted";
        this.btnRaw.UseVisualStyleBackColor = true;
        this.btnRaw.Click += new System.EventHandler(this.btnRaw_Click);
        // 
        // btnSort
        // 
        this.btnSort.Location = new System.Drawing.Point(146, 168);
        this.btnSort.Name = "btnSort";
        this.btnSort.Size = new System.Drawing.Size(111, 23);
        this.btnSort.TabIndex = 7;
        this.btnSort.Text = "Show &Sorted";
        this.btnSort.UseVisualStyleBackColor = true;
        this.btnSort.Click += new System.EventHandler(this.btnSort_Click);
        // 
        // btnClose
        // 
        this.btnClose.Location = new System.Drawing.Point(280, 168);
        this.btnClose.Name = "btnClose";
        this.btnClose.Size = new System.Drawing.Size(111, 23);
        this.btnClose.TabIndex = 8;
        this.btnClose.Text = "&Close";
        this.btnClose.UseVisualStyleBackColor = true;
        this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
        // 
        // frmMain
        // 
        this.ClientSize = new System.Drawing.Size(400, 452);
        this.Controls.Add(this.btnClose);
        this.Controls.Add(this.btnSort);
        this.Controls.Add(this.btnRaw);
        this.Controls.Add(this.lstSorted);
        this.Controls.Add(this.lstUnsorted);
        this.Controls.Add(this.txtString);
        this.Controls.Add(this.txtItems);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.groupBox1);
        this.Name = "frmMain";
        this.Text = "Generic Sort";
        this.groupBox1.ResumeLayout(false);
        this.groupBox1.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();

    }
    #endregion

    public frmMain()
    {
        InitializeComponent();
        rbInt.Checked = true;
        choice = INTEGER;
    }

    public static void Main()
    {
        frmMain main = new frmMain();
        Application.Run(main);
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void btnRaw_Click(object sender, EventArgs e)
    {
        bool flag;
        int i;

        flag = int.TryParse(txtItems.Text, out items);
        if (flag == false)
        {
            MessageBox.Show("Numeric only. Re-enter", "Input Error");
            txtItems.Focus();
            return;
        }

        lstUnsorted.Items.Clear();
        lstSorted.Items.Clear();
        whichListbox = UNSORTED;

        switch (choice)
        {
            case INTEGER:                  // Integer

                iData = new int[items];
                for (i = 0; i < items; i++)
                {
                    iData[i] = rnd.Next(MAXVALUE);
                }
                break;
            case LONG:
                lData = new long[items];
                for (i = 0; i < items; i++)
                {
                    lData[i] = (long)rnd.Next(MAXVALUE);
                }
                break;
            case DOUBLE:
                dData = new double[items];
                for (i = 0; i < items; i++)
                {
                    dData[i] = rnd.NextDouble() * MAXVALUE;
                }
                break;
            case STRING:
                sData = txtString.Text.Split(',');
                break;
        }

        ShowData();

    }

    private void btnSort_Click(object sender, EventArgs e)
    {
        whichListbox = SORTED;

        switch (choice)
        {
            case INTEGER:                  // Integer
                clsQuickSort<int> iSort = new clsQuickSort<int>(iData);
                iSort.Sort();
                break;

            case LONG:
                clsQuickSort<long> lSort = new clsQuickSort<long>(lData);
                lSort.Sort();
                break;

            case DOUBLE:
                clsQuickSort<double> dSort = new clsQuickSort<double>(dData);
                dSort.Sort();
                break;

            case STRING:
                clsQuickSort<string> sSort = new clsQuickSort<string>(sData);
                sSort.Sort();
                break;
        }
        ShowData();
    }
    private void ShowData()
    {
        int i;

        switch (choice)
        {
            case INTEGER:                  // Integer
                for (i = 0; i < items; i++)
                {
                    if (whichListbox == SORTED)
                        lstSorted.Items.Add(iData[i].ToString());
                    else
                        lstUnsorted.Items.Add(iData[i].ToString());
                }
                break;
            case LONG:
                for (i = 0; i < items; i++)
                {
                    if (whichListbox == SORTED)
                        lstSorted.Items.Add(lData[i].ToString());
                    else
                        lstUnsorted.Items.Add(lData[i].ToString());
                }
                break;
            case DOUBLE:
                for (i = 0; i < items; i++)
                {
                    if (whichListbox == SORTED)
                        lstSorted.Items.Add(dData[i].ToString());
                    else
                        lstUnsorted.Items.Add(dData[i].ToString());
                }
                break;
            case STRING:
                for (i = 0; i < sData.Length; i++)
                {
                    if (whichListbox == SORTED)
                        lstSorted.Items.Add(sData[i].ToString());
                    else
                        lstUnsorted.Items.Add(sData[i].ToString());
                }
                break;
        }
    }

    private void rbInt_Click(object sender, EventArgs e)
    {
        choice = INTEGER;
    }

    private void rbLong_Click(object sender, EventArgs e)
    {
        choice = LONG;
    }

    private void rbDouble_Click(object sender, EventArgs e)
    {
        choice = DOUBLE;
    }

    private void rbString_Click(object sender, EventArgs e)
    {
        choice = STRING;
    }
}