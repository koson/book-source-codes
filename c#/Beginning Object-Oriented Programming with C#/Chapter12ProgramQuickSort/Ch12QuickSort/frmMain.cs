#define DEBUG
#undef DEBUG

using System;
using System.Windows.Forms;

public class frmMain : Form
{
    private const int MAXVAL = 1000;    // Max random number to generate

    private int[] data;                 // Data array to sort
    private int number;                 // Number of elements in array

    private TextBox txtNumber;
    private Button btnGen;
    private ListBox lstOutput;
    private Button btnClose;
    private Button btnSort;
    private ListBox lstSorted;
    private Label label1;
    #region Windows code
    private void InitializeComponent()
    {
        this.label1 = new System.Windows.Forms.Label();
        this.txtNumber = new System.Windows.Forms.TextBox();
        this.btnGen = new System.Windows.Forms.Button();
        this.lstOutput = new System.Windows.Forms.ListBox();
        this.btnClose = new System.Windows.Forms.Button();
        this.btnSort = new System.Windows.Forms.Button();
        this.lstSorted = new System.Windows.Forms.ListBox();
        this.SuspendLayout();
        // 
        // label1
        // 
        this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.label1.Location = new System.Drawing.Point(12, 9);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(100, 20);
        this.label1.TabIndex = 0;
        this.label1.Text = "Number of items:";
        this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // txtNumber
        // 
        this.txtNumber.Location = new System.Drawing.Point(118, 9);
        this.txtNumber.Name = "txtNumber";
        this.txtNumber.Size = new System.Drawing.Size(100, 20);
        this.txtNumber.TabIndex = 1;
        this.txtNumber.Text = "100";
        // 
        // btnGen
        // 
        this.btnGen.Location = new System.Drawing.Point(12, 43);
        this.btnGen.Name = "btnGen";
        this.btnGen.Size = new System.Drawing.Size(75, 23);
        this.btnGen.TabIndex = 2;
        this.btnGen.Text = "&Generate";
        this.btnGen.UseVisualStyleBackColor = true;
        this.btnGen.Click += new System.EventHandler(this.btnGen_Click);
        // 
        // lstOutput
        // 
        this.lstOutput.FormattingEnabled = true;
        this.lstOutput.Location = new System.Drawing.Point(12, 82);
        this.lstOutput.Name = "lstOutput";
        this.lstOutput.Size = new System.Drawing.Size(134, 199);
        this.lstOutput.TabIndex = 3;
        // 
        // btnClose
        // 
        this.btnClose.Location = new System.Drawing.Point(238, 43);
        this.btnClose.Name = "btnClose";
        this.btnClose.Size = new System.Drawing.Size(75, 23);
        this.btnClose.TabIndex = 4;
        this.btnClose.Text = "&Close";
        this.btnClose.UseVisualStyleBackColor = true;
        this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
        // 
        // btnSort
        // 
        this.btnSort.Location = new System.Drawing.Point(118, 43);
        this.btnSort.Name = "btnSort";
        this.btnSort.Size = new System.Drawing.Size(75, 23);
        this.btnSort.TabIndex = 5;
        this.btnSort.Text = "&Sort";
        this.btnSort.UseVisualStyleBackColor = true;
        this.btnSort.Click += new System.EventHandler(this.btnSort_Click);
        // 
        // lstSorted
        // 
        this.lstSorted.FormattingEnabled = true;
        this.lstSorted.Location = new System.Drawing.Point(179, 82);
        this.lstSorted.Name = "lstSorted";
        this.lstSorted.Size = new System.Drawing.Size(134, 199);
        this.lstSorted.TabIndex = 6;
        // 
        // frmMain
        // 
        this.ClientSize = new System.Drawing.Size(325, 298);
        this.Controls.Add(this.lstSorted);
        this.Controls.Add(this.btnSort);
        this.Controls.Add(this.btnClose);
        this.Controls.Add(this.lstOutput);
        this.Controls.Add(this.btnGen);
        this.Controls.Add(this.txtNumber);
        this.Controls.Add(this.label1);
        this.Name = "frmMain";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Sort Numbers with Quicksort";
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

    private void btnGen_Click(object sender, EventArgs e)
    {
        bool flag;
        int i;

        flag = int.TryParse(txtNumber.Text, out number);
        if (flag == false)
        {
            MessageBox.Show("Enter whole digits only.", "Input Error");
            txtNumber.Focus();
            return;
        }
#if DEBUG
    Random rnd = new Random(number);    // For testing purposes
#else
        Random rnd = new Random();
#endif

        data = new int[number];

        lstOutput.Items.Clear();
        lstSorted.Items.Clear();

        for (i = 0; i < data.Length; i++)
        {
            data[i] = rnd.Next(MAXVAL);
            lstOutput.Items.Add(data[i].ToString());
        }

    }



    private void btnClose_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void btnSort_Click(object sender, EventArgs e)
    {
        int i;
        clsSort mySort = new clsSort(data);

        mySort.quickSort(0, data.Length - 1);

        for (i = 0; i < data.Length; i++)
        {
            lstSorted.Items.Add(data[i].ToString());
        }

    }
}