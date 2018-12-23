using System;
using System.Windows.Forms;

public class frmMain : Form
{
    private Button btnCalc;
    private Button btnClose;
    private ListBox lstTest;
    #region Windows code
    private void InitializeComponent()
    {
        this.lstTest = new System.Windows.Forms.ListBox();
        this.btnCalc = new System.Windows.Forms.Button();
        this.btnClose = new System.Windows.Forms.Button();
        this.SuspendLayout();
        // 
        // lstTest
        // 
        this.lstTest.FormattingEnabled = true;
        this.lstTest.Location = new System.Drawing.Point(12, 12);
        this.lstTest.Name = "lstTest";
        this.lstTest.Size = new System.Drawing.Size(120, 264);
        this.lstTest.TabIndex = 0;
        // 
        // btnCalc
        // 
        this.btnCalc.Location = new System.Drawing.Point(174, 21);
        this.btnCalc.Name = "btnCalc";
        this.btnCalc.Size = new System.Drawing.Size(75, 23);
        this.btnCalc.TabIndex = 1;
        this.btnCalc.Text = "Ca&lculate";
        this.btnCalc.UseVisualStyleBackColor = true;
        this.btnCalc.Click += new System.EventHandler(this.btnCalc_Click);
        // 
        // btnClose
        // 
        this.btnClose.Location = new System.Drawing.Point(174, 73);
        this.btnClose.Name = "btnClose";
        this.btnClose.Size = new System.Drawing.Size(75, 23);
        this.btnClose.TabIndex = 2;
        this.btnClose.Text = "&Close";
        this.btnClose.UseVisualStyleBackColor = true;
        this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
        // 
        // frmMain
        // 
        this.ClientSize = new System.Drawing.Size(292, 285);
        this.Controls.Add(this.btnClose);
        this.Controls.Add(this.btnCalc);
        this.Controls.Add(this.lstTest);
        this.Name = "frmMain";
        this.Text = "Collections";
        this.ResumeLayout(false);

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

    private void btnClose_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void btnCalc_Click(object sender, EventArgs e)
    {
        int i;
        int[] days = new int[] { 0, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 31 };
        int[][] samples = new int[3][];
        string[] weekDays = new string[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };

        samples[0] = new int[3];
        samples[1] = new int[10];
        samples[2] = new int[5];

        for (i = 0; i < 3; i++)
        {
            samples[0][i] = i;
            samples[1][i] = i;
            samples[2][i] = i;
        }

        foreach (string str in weekDays)
            lstTest.Items.Add(str);
        foreach (int val in days)
            lstTest.Items.Add(val.ToString());
    }
}