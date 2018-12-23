using System;
using System.Windows.Forms;

public class frmMain : Form
{
    private string dbName;
    public string whichServer;
    private string connectionString;

    #region Windows code

    private MenuStrip menuStrip1;
    private ToolStripMenuItem mnuFile;
    private ToolStripMenuItem mnuDB;
    private ToolStripMenuItem mnuAddRecord;
    private ToolStripMenuItem mnuEdit;
    private ToolStripMenuItem mnuSendReceivedReport;
    private ToolStripMenuItem mnuListFriends;
    private ToolStripMenuItem helpToolStripMenuItem;
    private ToolStripMenuItem mnuAbout;
    private ToolStripMenuItem mnuSelectServer;
    private ToolStripSeparator toolStripSeparator1;
    private Label lblWaiting;
    private ToolStripMenuItem mnuAddNewCard;
    private ToolStripMenuItem mnuAddNewCardType;
    private ToolStripSeparator toolStripSeparator2;
    private ToolStripSeparator toolStripSeparator3;
    private ToolStripMenuItem cardsSentReceivedToolStripMenuItem;
    private ToolStripMenuItem mnuExit;
    private void InitializeComponent()
    {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSelectServer = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDB = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAddNewCardType = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAddRecord = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuAddNewCard = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSendReceivedReport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuListFriends = new System.Windows.Forms.ToolStripMenuItem();
            this.cardsSentReceivedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.lblWaiting = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuDB,
            this.mnuSendReceivedReport,
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
            this.mnuSelectServer,
            this.toolStripSeparator1,
            this.mnuExit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(37, 20);
            this.mnuFile.Text = "&File";
            // 
            // mnuSelectServer
            // 
            this.mnuSelectServer.Name = "mnuSelectServer";
            this.mnuSelectServer.Size = new System.Drawing.Size(152, 22);
            this.mnuSelectServer.Text = "Select Server";
            this.mnuSelectServer.Click += new System.EventHandler(this.mnuSelectServer_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // mnuExit
            // 
            this.mnuExit.Name = "mnuExit";
            this.mnuExit.Size = new System.Drawing.Size(152, 22);
            this.mnuExit.Text = "E&xit";
            this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
            // 
            // mnuDB
            // 
            this.mnuDB.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAddNewCardType,
            this.mnuAddRecord,
            this.toolStripSeparator2,
            this.mnuAddNewCard,
            this.toolStripSeparator3,
            this.mnuEdit});
            this.mnuDB.Name = "mnuDB";
            this.mnuDB.Size = new System.Drawing.Size(67, 20);
            this.mnuDB.Text = "Database";
            // 
            // mnuAddNewCardType
            // 
            this.mnuAddNewCardType.Name = "mnuAddNewCardType";
            this.mnuAddNewCardType.Size = new System.Drawing.Size(180, 22);
            this.mnuAddNewCardType.Text = "Add New Card Type";
            this.mnuAddNewCardType.Click += new System.EventHandler(this.mnuAddNewCardType_Click);
            // 
            // mnuAddRecord
            // 
            this.mnuAddRecord.Name = "mnuAddRecord";
            this.mnuAddRecord.Size = new System.Drawing.Size(180, 22);
            this.mnuAddRecord.Text = "Add New Friend";
            this.mnuAddRecord.Click += new System.EventHandler(this.mnuAddRecord_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(177, 6);
            // 
            // mnuAddNewCard
            // 
            this.mnuAddNewCard.Name = "mnuAddNewCard";
            this.mnuAddNewCard.Size = new System.Drawing.Size(180, 22);
            this.mnuAddNewCard.Text = "Add New Card";
            this.mnuAddNewCard.Click += new System.EventHandler(this.mnuAddNewCard_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(177, 6);
            // 
            // mnuEdit
            // 
            this.mnuEdit.Name = "mnuEdit";
            this.mnuEdit.Size = new System.Drawing.Size(180, 22);
            this.mnuEdit.Text = "Edit Friend Record";
            this.mnuEdit.Click += new System.EventHandler(this.mnuEdit_Click);
            // 
            // mnuSendReceivedReport
            // 
            this.mnuSendReceivedReport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuListFriends,
            this.cardsSentReceivedToolStripMenuItem});
            this.mnuSendReceivedReport.Name = "mnuSendReceivedReport";
            this.mnuSendReceivedReport.Size = new System.Drawing.Size(59, 20);
            this.mnuSendReceivedReport.Text = "Reports";
            // 
            // mnuListFriends
            // 
            this.mnuListFriends.Name = "mnuListFriends";
            this.mnuListFriends.Size = new System.Drawing.Size(182, 22);
            this.mnuListFriends.Text = "List a Table";
            this.mnuListFriends.Click += new System.EventHandler(this.mnuListFriends_Click);
            // 
            // cardsSentReceivedToolStripMenuItem
            // 
            this.cardsSentReceivedToolStripMenuItem.Name = "cardsSentReceivedToolStripMenuItem";
            this.cardsSentReceivedToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.cardsSentReceivedToolStripMenuItem.Text = "Cards Sent/Received";
            this.cardsSentReceivedToolStripMenuItem.Click += new System.EventHandler(this.cardsSentReceivedToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAbout});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // mnuAbout
            // 
            this.mnuAbout.Name = "mnuAbout";
            this.mnuAbout.Size = new System.Drawing.Size(107, 22);
            this.mnuAbout.Text = "About";
            this.mnuAbout.Click += new System.EventHandler(this.mnuAbout_Click);
            // 
            // lblWaiting
            // 
            this.lblWaiting.Location = new System.Drawing.Point(204, 204);
            this.lblWaiting.Name = "lblWaiting";
            this.lblWaiting.Size = new System.Drawing.Size(194, 33);
            this.lblWaiting.TabIndex = 10;
            this.lblWaiting.Text = "Initializing Server List. Please wait...";
            this.lblWaiting.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblWaiting.Visible = false;
            // 
            // frmMain
            // 
            this.ClientSize = new System.Drawing.Size(606, 430);
            this.Controls.Add(this.lblWaiting);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.Text = "Database Management Subsystem";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMain_Load);
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


    //================================ Property Methods ====================================
    public string getServerName
    {
        get
        {
            return whichServer;
        }
        set
        {
            whichServer = value;
        }
    }
    public string getDatabaseName
    {
        get
        {
            return dbName;
        }
        set
        {
            dbName = value;
        }
    }
    public string getConnectString
    {
        get
        {
            return connectionString;
        }
        set
        {
            connectionString = value;
        }
    } 
    private void btnClose_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void mnuExit_Click(object sender, EventArgs e)
    {
        Close();
    }




    private void mnuAddRecord_Click(object sender, EventArgs e)
    {
        frmAddFriend newRec = new frmAddFriend(connectionString);
        newRec.ShowDialog();
    }

    private void mnuEdit_Click(object sender, EventArgs e)
    {
        frmEditFriend editRec = new frmEditFriend(connectionString);
        editRec.ShowDialog();
    }

    private void mnuListFriends_Click(object sender, EventArgs e)
    {
        frmReport myFriendReport = new frmReport(this);
        myFriendReport.ShowDialog();
    }

    private void mnuAbout_Click(object sender, EventArgs e)
    {
        frmAbout myAbout = new frmAbout();
        myAbout.ShowDialog();
    }

    private void mnuSelectServer_Click(object sender, EventArgs e)
    {

        //### Debug code set here:
        /*
        whichServer = "ECONJACK-PC";
                dbName = "Cards";
                connectionString = connectionString = "server=" + whichServer + ";integrated security=SSPI;database=" + dbName;
        */
        
        frmServerSelect myServer = new frmServerSelect(this);
        myServer.ShowDialog();
        if (whichServer.Length == 0 || dbName.Length == 0)
        {
            MessageBox.Show("You must select a server and database name.");
            return;
        }
        this.Text = "Database Management Subsystem:      Server:  " + whichServer + "     Database:  " + dbName;
        connectionString = "server=" + whichServer + ";integrated security=SSPI;database=" + dbName;
        
    }

    private void mnuAddNewCardType_Click(object sender, EventArgs e)
    {
        frmAddNewCardType myNewCard = new frmAddNewCardType(this);
        myNewCard.ShowDialog();
    }

    private void mnuAddNewCard_Click(object sender, EventArgs e)
    {
        frmCardExchange myExchange = new frmCardExchange(this);
        myExchange.ShowDialog();
    }

    private void cardsSentReceivedToolStripMenuItem_Click(object sender, EventArgs e)
    {
        frmCardReport myReport = new frmCardReport(this);
        myReport.ShowDialog();
    }

    private void frmMain_Load(object sender, EventArgs e)
    {

    }

}