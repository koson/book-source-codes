using System;
using System.Windows.Forms;


public class frmMain : Form
{
    private const int MAXLETTERS = 26;            // Symbolic constants
    private const int MAXCHARS = MAXLETTERS - 1;
    private const int LETTERA = 65;

    private TextBox txtInput;
    private Button btnCalc;
    private Button btnClose;
    private ListView lsvOutput;
    private ColumnHeader columnHeader1;
    private ColumnHeader columnHeader2;
    private Label label1;
    #region Windows code
    private void InitializeComponent()
    {
        this.label1 = new System.Windows.Forms.Label();
        this.txtInput = new System.Windows.Forms.TextBox();
        this.btnCalc = new System.Windows.Forms.Button();
        this.btnClose = new System.Windows.Forms.Button();
        this.lsvOutput = new System.Windows.Forms.ListView();
        this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
        this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
        this.SuspendLayout();
        // 
        // label1
        // 
        this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.label1.Location = new System.Drawing.Point(12, 9);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(251, 20);
        this.label1.TabIndex = 0;
        this.label1.Text = "Enter text:";
        this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // txtInput
        // 
        this.txtInput.Location = new System.Drawing.Point(12, 43);
        this.txtInput.Multiline = true;
        this.txtInput.Name = "txtInput";
        this.txtInput.Size = new System.Drawing.Size(251, 160);
        this.txtInput.TabIndex = 1;
        this.txtInput.Text = "qwertyuiopasdfghjklzxcvbnm,safhuipwey8fhsvjklhrehawsvkdlanjkleawhuifwbnfxcv";
        // 
        // btnCalc
        // 
        this.btnCalc.Location = new System.Drawing.Point(12, 209);
        this.btnCalc.Name = "btnCalc";
        this.btnCalc.Size = new System.Drawing.Size(75, 23);
        this.btnCalc.TabIndex = 2;
        this.btnCalc.Text = "Ca&lculate";
        this.btnCalc.UseVisualStyleBackColor = true;
        this.btnCalc.Click += new System.EventHandler(this.btnCalc_Click);
        // 
        // btnClose
        // 
        this.btnClose.Location = new System.Drawing.Point(188, 209);
        this.btnClose.Name = "btnClose";
        this.btnClose.Size = new System.Drawing.Size(75, 23);
        this.btnClose.TabIndex = 3;
        this.btnClose.Text = "&Close";
        this.btnClose.UseVisualStyleBackColor = true;
        this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
        // 
        // lsvOutput
        // 
        this.lsvOutput.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
        this.lsvOutput.Location = new System.Drawing.Point(12, 262);
        this.lsvOutput.Name = "lsvOutput";
        this.lsvOutput.Size = new System.Drawing.Size(251, 133);
        this.lsvOutput.TabIndex = 4;
        this.lsvOutput.UseCompatibleStateImageBehavior = false;
        this.lsvOutput.View = System.Windows.Forms.View.Details;
        // 
        // columnHeader1
        // 
        this.columnHeader1.Text = "Letter";
        this.columnHeader1.Width = 115;
        // 
        // columnHeader2
        // 
        this.columnHeader2.Text = "Count";
        this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
        this.columnHeader2.Width = 115;
        // 
        // frmMain
        // 
        this.ClientSize = new System.Drawing.Size(277, 408);
        this.Controls.Add(this.lsvOutput);
        this.Controls.Add(this.btnClose);
        this.Controls.Add(this.btnCalc);
        this.Controls.Add(this.txtInput);
        this.Controls.Add(this.label1);
        this.Name = "frmMain";
        this.Text = "Count letters";
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
        char oneLetter;
        int index;
        int i;
        int length;
        int[] count = new int[MAXLETTERS];
        string input;
        string buff;

        length = txtInput.Text.Length;
        if (length == 0)    // Anything to count??
        {
            MessageBox.Show("You need to enter some text.", "Missing Input");
            txtInput.Focus();
            return;
        }
        input = txtInput.Text;
        input = input.ToUpper();


        for (i = 0; i < input.Length; i++)    // Examine all letters.
        {
            oneLetter = input[i];               // Get a character
            index = oneLetter - LETTERA;        // Make into an index
            if (index < 0 || index > MAXCHARS)  // A letter??
                continue;                         // Nope.
            count[index]++;                     // Yep.
        }

        ListViewItem which;
        for (i = 0; i < MAXLETTERS; i++)
        {
            oneLetter = (char)(i + LETTERA);
            which = new ListViewItem(oneLetter.ToString());
            which.SubItems.Add(count[i].ToString());
            lsvOutput.Items.Add(which);
        }
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
        Close();
    }
}