using System;
using System.Data;
using System.Windows.Forms;

public class frmSelectTable : Form
{
    //============ Symbolic constants ============
    private const int MAXCOLUMNS = 1000;    // Probably safe

    clsDB myDB = new clsDB();
    private string tableName;

    private System.Windows.Forms.ListBox lstTableNames;
    private System.Windows.Forms.Label lblTableName;
    private Label label1;

    //============ Property methods ============

    public string TableName
    {
        get
        {
            return tableName;   // So we can send back to main()
        }
    }
    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        this.lstTableNames = new System.Windows.Forms.ListBox();
        this.lblTableName = new System.Windows.Forms.Label();
        this.label1 = new System.Windows.Forms.Label();
        this.SuspendLayout();
        // 
        // lstTableNames
        // 
        this.lstTableNames.FormattingEnabled = true;
        this.lstTableNames.Location = new System.Drawing.Point(12, 88);
        this.lstTableNames.Name = "lstTableNames";
        this.lstTableNames.Size = new System.Drawing.Size(201, 173);
        this.lstTableNames.TabIndex = 0;
        this.lstTableNames.DoubleClick += new System.EventHandler(this.lstTableNames_DoubleClick);
        // 
        // lblTableName
        // 
        this.lblTableName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lblTableName.Location = new System.Drawing.Point(12, 64);
        this.lblTableName.Name = "lblTableName";
        this.lblTableName.Size = new System.Drawing.Size(201, 20);
        this.lblTableName.TabIndex = 1;
        this.lblTableName.Text = "Table Names";
        this.lblTableName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // label1
        // 
        this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.label1.Location = new System.Drawing.Point(12, 9);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(201, 55);
        this.label1.TabIndex = 3;
        this.label1.Text = "Double-click on the desired database table from the list below. Dialog will  clos" +
            "e automatically after selection.";
        this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // frmSelectTable
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(225, 273);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.lblTableName);
        this.Controls.Add(this.lstTableNames);
        this.Name = "frmSelectTable";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Select Table from: ";
        this.ResumeLayout(false);

    }

    #endregion


  public frmSelectTable(string name)
  {
    InitializeComponent();
    int i = 0;
    string[] colNames = new string[MAXCOLUMNS];

    clsDB myDB = new clsDB(name);
    myDB.GetTableInfo(colNames);    // Get the table names from database

    while (true)
    {
      if (colNames[i] != null)
        lstTableNames.Items.Add(colNames[i++]); // Show tables
      else
        break;
    }
  }

  private void lstTableNames_DoubleClick(object sender, EventArgs e)
  {
      tableName = (string) lstTableNames.SelectedItem;
      Close();
  }
}
