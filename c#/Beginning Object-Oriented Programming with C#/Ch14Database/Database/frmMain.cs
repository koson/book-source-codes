using System;
using System.Windows.Forms;

public class frmMain : Form
{
    private string dbName;
    private string tableName;
    clsDB myDB;

    private MenuStrip menuStrip1;
    private ToolStripMenuItem mnuFile;
    private ToolStripMenuItem mnuNew;
    private ToolStripMenuItem mnuOpen;
    private ToolStripMenuItem mnuClose;
    private ToolStripSeparator toolStripMenuItem1;
    private ToolStripMenuItem mnuDB;
    private ToolStripMenuItem mnuCreateDBTable;
    private ToolStripMenuItem mnuAddRecord;
    private ToolStripMenuItem mnuEdit;
    private ToolStripMenuItem reportsToolStripMenuItem;
    private ToolStripMenuItem mnuQuery;
    private ToolStripMenuItem helpToolStripMenuItem;
    private ToolStripMenuItem mnuAbout;
    private ToolStripMenuItem mnuSelectTable;
    private ToolStripMenuItem mnuExit;
    #region Windows code
    private void InitializeComponent()
    {
        this.menuStrip1 = new System.Windows.Forms.MenuStrip();
        this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
        this.mnuNew = new System.Windows.Forms.ToolStripMenuItem();
        this.mnuOpen = new System.Windows.Forms.ToolStripMenuItem();
        this.mnuClose = new System.Windows.Forms.ToolStripMenuItem();
        this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
        this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
        this.mnuDB = new System.Windows.Forms.ToolStripMenuItem();
        this.mnuCreateDBTable = new System.Windows.Forms.ToolStripMenuItem();
        this.mnuSelectTable = new System.Windows.Forms.ToolStripMenuItem();
        this.mnuAddRecord = new System.Windows.Forms.ToolStripMenuItem();
        this.mnuEdit = new System.Windows.Forms.ToolStripMenuItem();
        this.reportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.mnuQuery = new System.Windows.Forms.ToolStripMenuItem();
        this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.mnuAbout = new System.Windows.Forms.ToolStripMenuItem();
        this.menuStrip1.SuspendLayout();
        this.SuspendLayout();
        // 
        // menuStrip1
        // 
        this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuDB,
            this.reportsToolStripMenuItem,
            this.helpToolStripMenuItem});
        this.menuStrip1.Location = new System.Drawing.Point(0, 0);
        this.menuStrip1.Name = "menuStrip1";
        this.menuStrip1.Size = new System.Drawing.Size(606, 24);
        this.menuStrip1.TabIndex = 8;
        this.menuStrip1.Text = "menuStrip1";
        // 
        // mnuFile
        // 
        this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuNew,
            this.mnuOpen,
            this.mnuClose,
            this.toolStripMenuItem1,
            this.mnuExit});
        this.mnuFile.Name = "mnuFile";
        this.mnuFile.Size = new System.Drawing.Size(35, 20);
        this.mnuFile.Text = "&File";
        // 
        // mnuNew
        // 
        this.mnuNew.Name = "mnuNew";
        this.mnuNew.Size = new System.Drawing.Size(111, 22);
        this.mnuNew.Text = "&New";
        this.mnuNew.Click += new System.EventHandler(this.mnuNew_Click);
        // 
        // mnuOpen
        // 
        this.mnuOpen.Name = "mnuOpen";
        this.mnuOpen.Size = new System.Drawing.Size(111, 22);
        this.mnuOpen.Text = "&Open";
        this.mnuOpen.Click += new System.EventHandler(this.mnuOpen_Click);
        // 
        // mnuClose
        // 
        this.mnuClose.Name = "mnuClose";
        this.mnuClose.Size = new System.Drawing.Size(111, 22);
        this.mnuClose.Text = "&Close";
        // 
        // toolStripMenuItem1
        // 
        this.toolStripMenuItem1.Name = "toolStripMenuItem1";
        this.toolStripMenuItem1.Size = new System.Drawing.Size(108, 6);
        // 
        // mnuExit
        // 
        this.mnuExit.Name = "mnuExit";
        this.mnuExit.Size = new System.Drawing.Size(111, 22);
        this.mnuExit.Text = "E&xit";
        this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
        // 
        // mnuDB
        // 
        this.mnuDB.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCreateDBTable,
            this.mnuSelectTable,
            this.mnuAddRecord,
            this.mnuEdit});
        this.mnuDB.Name = "mnuDB";
        this.mnuDB.Size = new System.Drawing.Size(65, 20);
        this.mnuDB.Text = "Database";
        // 
        // mnuCreateDBTable
        // 
        this.mnuCreateDBTable.Name = "mnuCreateDBTable";
        this.mnuCreateDBTable.Size = new System.Drawing.Size(183, 22);
        this.mnuCreateDBTable.Text = "Create New Table";
        this.mnuCreateDBTable.Click += new System.EventHandler(this.mnuCreateDBTable_Click);
        // 
        // mnuSelectTable
        // 
        this.mnuSelectTable.Name = "mnuSelectTable";
        this.mnuSelectTable.Size = new System.Drawing.Size(183, 22);
        this.mnuSelectTable.Text = "Select Existing Table";
        this.mnuSelectTable.Click += new System.EventHandler(this.mnuSelectTable_Click);
        // 
        // mnuAddRecord
        // 
        this.mnuAddRecord.Name = "mnuAddRecord";
        this.mnuAddRecord.Size = new System.Drawing.Size(183, 22);
        this.mnuAddRecord.Text = "Add New Record";
        this.mnuAddRecord.Click += new System.EventHandler(this.mnuAddRecord_Click);
        // 
        // mnuEdit
        // 
        this.mnuEdit.Name = "mnuEdit";
        this.mnuEdit.Size = new System.Drawing.Size(183, 22);
        this.mnuEdit.Text = "Edit Record";
        this.mnuEdit.Visible = false;
        this.mnuEdit.Click += new System.EventHandler(this.mnuEdit_Click);
        // 
        // reportsToolStripMenuItem
        // 
        this.reportsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuQuery});
        this.reportsToolStripMenuItem.Name = "reportsToolStripMenuItem";
        this.reportsToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
        this.reportsToolStripMenuItem.Text = "Reports";
        // 
        // mnuQuery
        // 
        this.mnuQuery.Name = "mnuQuery";
        this.mnuQuery.Size = new System.Drawing.Size(152, 22);
        this.mnuQuery.Text = "Do Query";
        this.mnuQuery.Click += new System.EventHandler(this.mnuQuery_Click);
        // 
        // helpToolStripMenuItem
        // 
        this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAbout});
        this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
        this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
        this.helpToolStripMenuItem.Text = "Help";
        // 
        // mnuAbout
        // 
        this.mnuAbout.Name = "mnuAbout";
        this.mnuAbout.Size = new System.Drawing.Size(114, 22);
        this.mnuAbout.Text = "About";
        this.mnuAbout.Click += new System.EventHandler(this.mnuAbout_Click);
        // 
        // frmMain
        // 
        this.ClientSize = new System.Drawing.Size(606, 430);
        this.Controls.Add(this.menuStrip1);
        this.IsMdiContainer = true;
        this.MainMenuStrip = this.menuStrip1;
        this.Name = "frmMain";
        this.Text = "Database Management Subsystem";
        this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
        this.menuStrip1.ResumeLayout(false);
        this.menuStrip1.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();

    }
    #endregion

    public frmMain()
    {
        InitializeComponent();
    }
    [STAThread]
    public static void Main()
    {
        frmMain main = new frmMain();
        Application.Run(main);
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void mnuExit_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void mnuNew_Click(object sender, EventArgs e)
    {
        frmCreateDB myNewDB = new frmCreateDB(dbName);
        myNewDB.ShowDialog();
    }

    private void mnuCreateDBTable_Click(object sender, EventArgs e)
    {
        frmCreateDBTable newDBT = new frmCreateDBTable(dbName);
        newDBT.ShowDialog();
    }

    private void mnuOpen_Click(object sender, EventArgs e)
    {
        OpenFileDialog fileOpen = new OpenFileDialog();

        fileOpen.InitialDirectory = Application.StartupPath;
        fileOpen.Title = "Select file to open:";
        fileOpen.Filter = "(*.mdb)|*.mdb|All files (*.*)|*.*";

        if (fileOpen.ShowDialog() == DialogResult.OK)
        {
            dbName = fileOpen.FileName;
            this.Text += ":  Open database = " + dbName;
            myDB = new clsDB(dbName);
        }
    }

    private void mnuAddRecord_Click(object sender, EventArgs e)
    {
        frmAddFriend newRec = new frmAddFriend(dbName, tableName);
        newRec.ShowDialog();
    }

    private void mnuEdit_Click(object sender, EventArgs e)
    {
        frmEditFriend editRec = new frmEditFriend(dbName, tableName);
        editRec.ShowDialog();
    }


    private void mnuAbout_Click(object sender, EventArgs e)
    {
        frmAbout myAbout = new frmAbout();
        myAbout.ShowDialog();
    }

  public void mnuSelectTable_Click(object sender, EventArgs e)
  {
    frmSelectTable myTable = new frmSelectTable(dbName);
    myTable.ShowDialog();

    tableName = myTable.TableName;  // Get table name from the form
    mnuEdit.Visible = true;         // Now they can edit data in a table
    mnuQuery.Visible = true;
  }

  private void mnuQuery_Click(object sender, EventArgs e)
  {
      if (dbName == null)     // See if they opened a DB
      {
          MessageBox.Show("You must open a database first");
          return;
      }
      frmQuery myQuery = new frmQuery(dbName);
      myQuery.ShowDialog();
  }

}