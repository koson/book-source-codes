using System;
using System.Windows.Forms;
using System.ComponentModel;


public class frmMain : Form
{
  const int MAXVAL = 52;
  const int MAXELEMENTS = 100;
  static string stars = "************************************************************";

  int[] data = new int[MAXELEMENTS];
  private Button btnSort;
  private Button btnClose;
  private ListBox lstResult;
  private Button btnCalc;
  #region Windows code
  private void InitializeComponent()
  {
    this.btnCalc = new System.Windows.Forms.Button();
    this.btnSort = new System.Windows.Forms.Button();
    this.btnClose = new System.Windows.Forms.Button();
    this.lstResult = new System.Windows.Forms.ListBox();
    this.SuspendLayout();
    // 
    // btnCalc
    // 
    this.btnCalc.Location = new System.Drawing.Point(12, 12);
    this.btnCalc.Name = "btnCalc";
    this.btnCalc.Size = new System.Drawing.Size(75, 23);
    this.btnCalc.TabIndex = 0;
    this.btnCalc.Text = "Ca&lculate";
    this.btnCalc.UseVisualStyleBackColor = true;
    this.btnCalc.Click += new System.EventHandler(this.btnCalc_Click);
    // 
    // btnSort
    // 
    this.btnSort.Location = new System.Drawing.Point(104, 12);
    this.btnSort.Name = "btnSort";
    this.btnSort.Size = new System.Drawing.Size(75, 23);
    this.btnSort.TabIndex = 1;
    this.btnSort.Text = "&Sort";
    this.btnSort.UseVisualStyleBackColor = true;
    this.btnSort.Click += new System.EventHandler(this.btnSort_Click);
    // 
    // btnClose
    // 
    this.btnClose.Location = new System.Drawing.Point(205, 12);
    this.btnClose.Name = "btnClose";
    this.btnClose.Size = new System.Drawing.Size(75, 23);
    this.btnClose.TabIndex = 2;
    this.btnClose.Text = "&Close";
    this.btnClose.UseVisualStyleBackColor = true;
    this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
    // 
    // lstResult
    // 
    this.lstResult.FormattingEnabled = true;
    this.lstResult.Location = new System.Drawing.Point(12, 60);
    this.lstResult.Name = "lstResult";
    this.lstResult.Size = new System.Drawing.Size(268, 303);
    this.lstResult.TabIndex = 3;
    // 
    // frmMain
    // 
    this.ClientSize = new System.Drawing.Size(292, 375);
    this.Controls.Add(this.lstResult);
    this.Controls.Add(this.btnClose);
    this.Controls.Add(this.btnSort);
    this.Controls.Add(this.btnCalc);
    this.Name = "frmMain";
    this.Text = "Random Values";
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
    string buff;
    Random rd = new Random(5);    // Define a random object

    lstResult.Items.Clear();
    for (i = 0; i < data.Length; i++)
    {
      data[i] = rd.Next(MAXVAL);  // Get a random value
      buff = data[i].ToString() + " " + stars.Substring(0, data[i]);
      lstResult.Items.Add(buff);  // Put in listbox
    }
  }

  private void btnSort_Click(object sender, EventArgs e)
  {
    int i;
    string buff;
  
    Array.Sort(data);                   // Sort the data      

    lstResult.Items.Clear();            // Clear out old data

    for (i = 0; i < data.Length; i++)   // Show it
    {
      buff = data[i].ToString() + " " + stars.Substring(0, data[i]);
      lstResult.Items.Add(buff);  // Put in listbox
    }

  }
}