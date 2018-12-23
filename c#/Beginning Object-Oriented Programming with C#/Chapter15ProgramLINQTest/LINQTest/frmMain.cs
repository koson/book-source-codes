using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

public class frmMain : Form
{
    private const int MAXNUM = 100;     // Symbolic constant

    static List<int> numbers = new List<int>(); // static member

    private Button btnClose;
    private ListBox lstOutput;
    private ListBox lstFull;
    private TextBox txtLow;
    private Label label1;
    private Label label2;
    private TextBox txtHi;
    private Label label3;
    private Label label4;
    private Label label5;
    private Button btnCalc;

    #region Windows code
    private void InitializeComponent()
    {
            this.btnCalc = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lstOutput = new System.Windows.Forms.ListBox();
            this.lstFull = new System.Windows.Forms.ListBox();
            this.txtLow = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtHi = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCalc
            // 
            this.btnCalc.Location = new System.Drawing.Point(163, 137);
            this.btnCalc.Name = "btnCalc";
            this.btnCalc.Size = new System.Drawing.Size(231, 23);
            this.btnCalc.TabIndex = 0;
            this.btnCalc.Text = "Calculate";
            this.btnCalc.UseVisualStyleBackColor = true;
            this.btnCalc.Click += new System.EventHandler(this.btnCalc_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(163, 289);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(231, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lstOutput
            // 
            this.lstOutput.FormattingEnabled = true;
            this.lstOutput.Location = new System.Drawing.Point(419, 32);
            this.lstOutput.Name = "lstOutput";
            this.lstOutput.Size = new System.Drawing.Size(125, 290);
            this.lstOutput.TabIndex = 3;
            // 
            // lstFull
            // 
            this.lstFull.FormattingEnabled = true;
            this.lstFull.Location = new System.Drawing.Point(12, 32);
            this.lstFull.Name = "lstFull";
            this.lstFull.Size = new System.Drawing.Size(125, 290);
            this.lstFull.TabIndex = 4;
            // 
            // txtLow
            // 
            this.txtLow.Location = new System.Drawing.Point(163, 32);
            this.txtLow.Name = "txtLow";
            this.txtLow.Size = new System.Drawing.Size(231, 20);
            this.txtLow.TabIndex = 5;
            this.txtLow.Text = "10";
            this.txtLow.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(163, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(231, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Find those value with a Lower Limit of:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(163, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(231, 20);
            this.label2.TabIndex = 8;
            this.label2.Text = "and an Upper Limit of:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtHi
            // 
            this.txtHi.Location = new System.Drawing.Point(163, 78);
            this.txtHi.Name = "txtHi";
            this.txtHi.Size = new System.Drawing.Size(231, 20);
            this.txtHi.TabIndex = 7;
            this.txtHi.Text = "25";
            this.txtHi.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 20);
            this.label3.TabIndex = 9;
            this.label3.Text = "List of Random Values";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label4.Location = new System.Drawing.Point(163, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(231, 20);
            this.label4.TabIndex = 10;
            this.label4.Text = "and display them in the listbox on the right.";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label5.Location = new System.Drawing.Point(419, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(125, 20);
            this.label5.TabIndex = 11;
            this.label5.Text = "Query Result:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmMain
            // 
            this.ClientSize = new System.Drawing.Size(573, 341);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtHi);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtLow);
            this.Controls.Add(this.lstFull);
            this.Controls.Add(this.lstOutput);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnCalc);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Test LINQ";
            this.ResumeLayout(false);
            this.PerformLayout();

    }
    #endregion

    //========================== Constructor ===============================
    public frmMain()
    {
        InitializeComponent();
        GenerateRandomValues();
    }

    //=========================== Program Start ============================
    public static void Main()
    {
        frmMain main = new frmMain();
        Application.Run(main);
    }

    //========================= Helper Methods ==============================
    private void btnCalc_Click(object sender, EventArgs e)
    {

        int lo;
        int hi;

        lstOutput.Items.Clear();

        SetTheLimits(out lo, out hi);
        DoLINQQuery(lo, hi);
    }

    /****
     * Purpose: To generate a MAXNUM sequence of random integer values
     * 
     * Parameter list:
     *  int lo          the lower limit for query
     *  int hi          the upper limit for query
     *  
     * Return value:
     *  void
     *  
     ****/
    private void DoLINQQuery(int lo, int hi)
    {
        var query = from p in numbers               // The "Query"
                    where p > lo && p < hi
                    select p;

        foreach (var val in query)                  // Display results
        {
            lstOutput.Items.Add(val.ToString());
        }
  
    }

    /****
     * Purpose: Set the upper and lower limits of the query
     * 
     * Parameter list:
     *  out int lo          reference to the lower limit
     *  out int hi          reference to the upper limit
     *  
     * Return value:
     *  void
     *  
     ****/
    private void SetTheLimits(out int lo, out int hi)
    {
        bool flag = int.TryParse(txtLow.Text, out lo);   // Input validation
        if (flag == false)
        {
            MessageBox.Show("Numeric only, 0 to 100", "Input Error");
            txtLow.Focus();
        }
        flag = int.TryParse(txtHi.Text, out hi);
        if (flag == false)
        {
            MessageBox.Show("Numeric only, 0 to 100", "Input Error");
            txtHi.Focus();
        }
    }

    /****
     * Purpose: To generate a MAXNUM sequence of random integer values
     * 
     * Parameter list:
     *  void
     *  
     * Return value:
     *  void
     *  
     ****/
    private void GenerateRandomValues()
    {
        int temp;
        DateTime current = DateTime.Now;
        Random rnd = new Random((int)current.Ticks);

        for (int i = 0; i < MAXNUM; i++)            // Random values
        {
            temp = rnd.Next(MAXNUM);
            numbers.Add(temp);                      // Copy into list
            lstFull.Items.Add(temp.ToString());
        }
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
        Close();
    }

}