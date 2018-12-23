using System;
using System.Linq;
using System.Windows.Forms;

public class frmMain : Form
{
    static int passes = 0;
    static int count;

    private Button btnClose;
    private ListBox lstOutput;
    private ListBox lstFull;
    private Label label1;
    private TextBox txtState;
    private Button btnCalc;

    #region Windows code
    private void InitializeComponent()
    {
        this.btnCalc = new System.Windows.Forms.Button();
        this.btnClose = new System.Windows.Forms.Button();
        this.lstOutput = new System.Windows.Forms.ListBox();
        this.lstFull = new System.Windows.Forms.ListBox();
        this.label1 = new System.Windows.Forms.Label();
        this.txtState = new System.Windows.Forms.TextBox();
        this.SuspendLayout();
        // 
        // btnCalc
        // 
        this.btnCalc.Location = new System.Drawing.Point(172, 81);
        this.btnCalc.Name = "btnCalc";
        this.btnCalc.Size = new System.Drawing.Size(75, 23);
        this.btnCalc.TabIndex = 0;
        this.btnCalc.Text = "Calculate";
        this.btnCalc.UseVisualStyleBackColor = true;
        this.btnCalc.Click += new System.EventHandler(this.btnCalc_Click);
        // 
        // btnClose
        // 
        this.btnClose.Location = new System.Drawing.Point(172, 284);
        this.btnClose.Name = "btnClose";
        this.btnClose.Size = new System.Drawing.Size(75, 23);
        this.btnClose.TabIndex = 2;
        this.btnClose.Text = "&Close";
        this.btnClose.UseVisualStyleBackColor = true;
        this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
        // 
        // lstOutput
        // 
        this.lstOutput.FormattingEnabled = true;
        this.lstOutput.Location = new System.Drawing.Point(261, 17);
        this.lstOutput.Name = "lstOutput";
        this.lstOutput.Size = new System.Drawing.Size(141, 290);
        this.lstOutput.TabIndex = 3;
        // 
        // lstFull
        // 
        this.lstFull.FormattingEnabled = true;
        this.lstFull.Location = new System.Drawing.Point(12, 17);
        this.lstFull.Name = "lstFull";
        this.lstFull.Size = new System.Drawing.Size(141, 290);
        this.lstFull.TabIndex = 4;
        // 
        // label1
        // 
        this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.label1.Location = new System.Drawing.Point(172, 17);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(75, 20);
        this.label1.TabIndex = 6;
        this.label1.Text = "State to Find:";
        this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // txtState
        // 
        this.txtState.Location = new System.Drawing.Point(172, 40);
        this.txtState.Name = "txtState";
        this.txtState.Size = new System.Drawing.Size(75, 20);
        this.txtState.TabIndex = 5;
        this.txtState.Text = "IN";
        // 
        // frmMain
        // 
        this.ClientSize = new System.Drawing.Size(414, 319);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.txtState);
        this.Controls.Add(this.lstFull);
        this.Controls.Add(this.lstOutput);
        this.Controls.Add(this.btnClose);
        this.Controls.Add(this.btnCalc);
        this.Name = "frmMain";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Test LINQ: Strings";
        this.Load += new System.EventHandler(this.frmMain_Load);
        this.ResumeLayout(false);
        this.PerformLayout();

    }
    #endregion

    public frmMain()
    {
        InitializeComponent();
        ShowAll();
    }

    public static void Main()
    {
        frmMain main = new frmMain();
        Application.Run(main);
    }

    private void btnCalc_Click(object sender, EventArgs e)
    {
        ShowAll();
    }

    private void ShowAll()
    {
        int i;
        var friends = new[] {
            new {name = "Tom", state = "IN"},
            new {name = "Alice", state = "VA"},
            new {name = "Tammy", state = "IN"},
            new {name = "Ann", state = "KY"},
            new {name = "Don", state = "IN"},
            new {name = "John", state = "IN"},
            new {name = "Debbie", state = "IN"},
        };

        if (passes == 0)
        {
            count = friends.GetUpperBound(0);
            i = 0;
            for (i = 0; i < count; i++)
            {
                lstFull.Items.Add(friends[i].name + "  " + friends[i].state);
            }
            passes++;
        }
        else
        {
            lstOutput.Items.Clear();
            var query = from p in friends               // The "Query"
                        where p.state == txtState.Text
                        select p;
            foreach (var val in query)                  // Display results
                lstOutput.Items.Add(val.name + "  " + val.state);
        }
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
        Close();
    }
}