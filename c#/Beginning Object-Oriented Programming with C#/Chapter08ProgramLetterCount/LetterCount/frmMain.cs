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
  private Label label2;
  private Label label3;
  private ListBox lstOutput;
  private Label label1;
  #region Windows code
  private void InitializeComponent()
  {
            this.label1 = new System.Windows.Forms.Label();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.btnCalc = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lstOutput = new System.Windows.Forms.ListBox();
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
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(12, 258);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Letter";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Location = new System.Drawing.Point(129, 258);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Count";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lstOutput
            // 
            this.lstOutput.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstOutput.FormattingEnabled = true;
            this.lstOutput.ItemHeight = 14;
            this.lstOutput.Location = new System.Drawing.Point(12, 281);
            this.lstOutput.Name = "lstOutput";
            this.lstOutput.Size = new System.Drawing.Size(251, 158);
            this.lstOutput.TabIndex = 4;
            // 
            // frmMain
            // 
            this.ClientSize = new System.Drawing.Size(292, 451);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lstOutput);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnCalc);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.label1);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
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

    for (i = 0; i < MAXLETTERS; i++)
    {
      buff = string.Format("{0, 4} {1,20}[{2}]", (char)(i + LETTERA)," ",count[i]);
      lstOutput.Items.Add(buff);
    }
  }

  private void btnClose_Click(object sender, EventArgs e)
  {
    Close();
  }
}