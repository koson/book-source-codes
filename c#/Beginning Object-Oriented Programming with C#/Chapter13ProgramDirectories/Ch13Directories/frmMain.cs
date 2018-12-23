using System;
using System.IO;
using System.Collections;
using System.Windows.Forms;

public class frmMain : Form
{
    private ListBox lstDirectories;
    private Label label1;
    private TextBox txtStartingPath;
    private Button btnClose;
    private Label lblDriveInfo;
    private Button btnList;
    #region Windows code
    private void InitializeComponent()
    {
        this.lstDirectories = new System.Windows.Forms.ListBox();
        this.btnList = new System.Windows.Forms.Button();
        this.label1 = new System.Windows.Forms.Label();
        this.txtStartingPath = new System.Windows.Forms.TextBox();
        this.btnClose = new System.Windows.Forms.Button();
        this.lblDriveInfo = new System.Windows.Forms.Label();
        this.SuspendLayout();
        // 
        // lstDirectories
        // 
        this.lstDirectories.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lstDirectories.FormattingEnabled = true;
        this.lstDirectories.ItemHeight = 14;
        this.lstDirectories.Location = new System.Drawing.Point(12, 103);
        this.lstDirectories.Name = "lstDirectories";
        this.lstDirectories.Size = new System.Drawing.Size(342, 298);
        this.lstDirectories.TabIndex = 4;
        this.lstDirectories.TabStop = false;
        // 
        // btnList
        // 
        this.btnList.Location = new System.Drawing.Point(12, 49);
        this.btnList.Name = "btnList";
        this.btnList.Size = new System.Drawing.Size(75, 23);
        this.btnList.TabIndex = 1;
        this.btnList.Text = "&List";
        this.btnList.UseVisualStyleBackColor = true;
        this.btnList.Click += new System.EventHandler(this.btnList_Click);
        // 
        // label1
        // 
        this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.label1.Location = new System.Drawing.Point(12, 17);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(100, 20);
        this.label1.TabIndex = 2;
        this.label1.Text = "Starting directory:";
        this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // txtStartingPath
        // 
        this.txtStartingPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.txtStartingPath.Location = new System.Drawing.Point(118, 18);
        this.txtStartingPath.Name = "txtStartingPath";
        this.txtStartingPath.Size = new System.Drawing.Size(236, 20);
        this.txtStartingPath.TabIndex = 0;
        this.txtStartingPath.Text = "c:\\temp";
        // 
        // btnClose
        // 
        this.btnClose.Location = new System.Drawing.Point(279, 49);
        this.btnClose.Name = "btnClose";
        this.btnClose.Size = new System.Drawing.Size(75, 23);
        this.btnClose.TabIndex = 2;
        this.btnClose.Text = "&Close";
        this.btnClose.UseVisualStyleBackColor = true;
        this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
        // 
        // lblDriveInfo
        // 
        this.lblDriveInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lblDriveInfo.Location = new System.Drawing.Point(12, 80);
        this.lblDriveInfo.Name = "lblDriveInfo";
        this.lblDriveInfo.Size = new System.Drawing.Size(342, 20);
        this.lblDriveInfo.TabIndex = 5;
        this.lblDriveInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // frmMain
        // 
        this.ClientSize = new System.Drawing.Size(368, 413);
        this.Controls.Add(this.lblDriveInfo);
        this.Controls.Add(this.btnClose);
        this.Controls.Add(this.txtStartingPath);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.btnList);
        this.Controls.Add(this.lstDirectories);
        this.Name = "frmMain";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Directories";
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

    private void btnList_Click(object sender, EventArgs e)
    {
        string startingPath;
        int count;
        int i;
        ArrayList dirs = new ArrayList();

        startingPath = @txtStartingPath.Text;

        try
        {
            DirectoryInfo myDirInfo = new DirectoryInfo(startingPath);
            if (myDirInfo.Exists == false)
            {
                MessageBox.Show("Cannot find directory. Re-enter.", "Directory Not Found");
                txtStartingPath.Focus();
                return;
            }
            clsDirectory myDirs = new clsDirectory();

            ShowDriveInfo();

            lstDirectories.Items.Clear();

            count = myDirs.ShowDirectory(myDirInfo, 0, dirs);
            for (i = 0; i < dirs.Count; i++)
            {
                lstDirectories.Items.Add(dirs[i]);
            }
            this.Text = "Directories found: " + count.ToString();

        }
        catch (Exception ex)
        {
            MessageBox.Show("Error: " + ex.Message, "IO Error");
            return;
        }
    }

    /*****
     * Purpose: This shows some size info about the drive selected.
     * 
     * Parameter list:
     *  n/a
     *  
     * Return type:
     *  void
     ******/
    private void ShowDriveInfo()
    {
        int pos;
        long driveBytes;
        string buff;

        try
        {
            pos = txtStartingPath.Text.IndexOf('\\');       // Get drive name
            buff = txtStartingPath.Text.Substring(0, pos);

            DriveInfo myDrive = new DriveInfo(@buff);       // Get its info

            driveBytes = myDrive.AvailableFreeSpace;
            driveBytes = myDrive.TotalSize / 1000000;
            lblDriveInfo.Text = "Drive " + buff + " has " + 
                                driveBytes.ToString() + "MB bytes, with " +
                                 myDrive.TotalFreeSpace/1000000
                                 + "MB bytes free.";
        }
        catch
        {
            txtStartingPath.Text = "";
        }

    }
    private void btnClose_Click(object sender, EventArgs e)
    {
        Close();
    }
}
