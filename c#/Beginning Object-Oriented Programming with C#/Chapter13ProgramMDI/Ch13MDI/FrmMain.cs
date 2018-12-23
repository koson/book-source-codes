using System;
using System.Windows.Forms;

public class frmMain : Form
{
    string selectFile;

    private ToolStripMenuItem mnuFile;
    private ToolStripMenuItem mnuOpen;
    private ToolStripMenuItem mnuEdit;
    private ToolStripSeparator toolStripMenuItem1;
    private ToolStripMenuItem mnuExit;
    private MenuStrip menuStrip1;
    #region Windows code
    private void InitializeComponent()
    {
        this.menuStrip1 = new System.Windows.Forms.MenuStrip();
        this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
        this.mnuOpen = new System.Windows.Forms.ToolStripMenuItem();
        this.mnuEdit = new System.Windows.Forms.ToolStripMenuItem();
        this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
        this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
        this.menuStrip1.SuspendLayout();
        this.SuspendLayout();
        // 
        // menuStrip1
        // 
        this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile});
        this.menuStrip1.Location = new System.Drawing.Point(0, 0);
        this.menuStrip1.Name = "menuStrip1";
        this.menuStrip1.Size = new System.Drawing.Size(292, 24);
        this.menuStrip1.TabIndex = 1;
        this.menuStrip1.Text = "menuStrip1";
        // 
        // mnuFile
        // 
        this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuOpen,
            this.mnuEdit,
            this.toolStripMenuItem1,
            this.mnuExit});
        this.mnuFile.Name = "mnuFile";
        this.mnuFile.Size = new System.Drawing.Size(35, 20);
        this.mnuFile.Text = "&File";
        this.mnuFile.Click += new System.EventHandler(this.mnuFile_Click);
        // 
        // mnuOpen
        // 
        this.mnuOpen.Name = "mnuOpen";
        this.mnuOpen.Size = new System.Drawing.Size(152, 22);
        this.mnuOpen.Text = "&Open";
        this.mnuOpen.Click += new System.EventHandler(this.mnuOpen_Click);
        // 
        // mnuEdit
        // 
        this.mnuEdit.Name = "mnuEdit";
        this.mnuEdit.Size = new System.Drawing.Size(152, 22);
        this.mnuEdit.Text = "&Edit";
        this.mnuEdit.Click += new System.EventHandler(this.mnuEdit_Click);
        // 
        // toolStripMenuItem1
        // 
        this.toolStripMenuItem1.Name = "toolStripMenuItem1";
        this.toolStripMenuItem1.Size = new System.Drawing.Size(149, 6);
        // 
        // mnuExit
        // 
        this.mnuExit.Name = "mnuExit";
        this.mnuExit.Size = new System.Drawing.Size(152, 22);
        this.mnuExit.Text = "E&xit";
        // 
        // frmMain
        // 
        this.ClientSize = new System.Drawing.Size(292, 266);
        this.Controls.Add(this.menuStrip1);
        this.IsMdiContainer = true;
        this.MainMenuStrip = this.menuStrip1;
        this.Name = "frmMain";
        this.Text = "MDI Project";
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

    private void mnuFile_Click(object sender, EventArgs e)
    {

    }

    private void mnuOpen_Click(object sender, EventArgs e)
    {
        OpenFileDialog fileOpen = new OpenFileDialog();
        
        fileOpen.Title = "Select file to open:";
        fileOpen.Filter = "(*.bin)|*.bin|(*.txt)|*.txt|All files (*.*)|*.*";

        if (fileOpen.ShowDialog() == DialogResult.OK)
        {
            selectFile = fileOpen.FileName;
        }
    }

    private void mnuEdit_Click(object sender, EventArgs e)
    {
        frmEditFriend frm = new frmEditFriend();
        frm.ShowDialog();
    }

   
}