using System;
using System.Windows.Forms;

public class frmMain : Form
{
    DateTime myTime;
    clsBuilding myBldg = new clsBuilding();
    clsApartment myApt;
    clsCommercial myComm;
    clsHome myHome;

    private ListBox lstMessages;
    private Button btnShow;
    private Button btnRemoveSnow;
    private Button btnClose;
    #region Windows code
    private void InitializeComponent()
    {
        this.lstMessages = new System.Windows.Forms.ListBox();
        this.btnShow = new System.Windows.Forms.Button();
        this.btnClose = new System.Windows.Forms.Button();
        this.btnRemoveSnow = new System.Windows.Forms.Button();
        this.SuspendLayout();
        // 
        // lstMessages
        // 
        this.lstMessages.FormattingEnabled = true;
        this.lstMessages.Location = new System.Drawing.Point(12, 67);
        this.lstMessages.Name = "lstMessages";
        this.lstMessages.Size = new System.Drawing.Size(581, 238);
        this.lstMessages.TabIndex = 2;
        // 
        // btnShow
        // 
        this.btnShow.Location = new System.Drawing.Point(12, 19);
        this.btnShow.Name = "btnShow";
        this.btnShow.Size = new System.Drawing.Size(112, 23);
        this.btnShow.TabIndex = 3;
        this.btnShow.Text = "&Show Properties";
        this.btnShow.UseVisualStyleBackColor = true;
        this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
        // 
        // btnClose
        // 
        this.btnClose.Location = new System.Drawing.Point(518, 19);
        this.btnClose.Name = "btnClose";
        this.btnClose.Size = new System.Drawing.Size(75, 23);
        this.btnClose.TabIndex = 4;
        this.btnClose.Text = "&Close";
        this.btnClose.UseVisualStyleBackColor = true;
        this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
        // 
        // btnRemoveSnow
        // 
        this.btnRemoveSnow.Location = new System.Drawing.Point(141, 19);
        this.btnRemoveSnow.Name = "btnRemoveSnow";
        this.btnRemoveSnow.Size = new System.Drawing.Size(112, 23);
        this.btnRemoveSnow.TabIndex = 5;
        this.btnRemoveSnow.Text = "&Remove Snow";
        this.btnRemoveSnow.UseVisualStyleBackColor = true;
        this.btnRemoveSnow.Click += new System.EventHandler(this.btnRemoveSnow_Click);
        // 
        // frmMain
        // 
        this.ClientSize = new System.Drawing.Size(605, 322);
        this.Controls.Add(this.btnRemoveSnow);
        this.Controls.Add(this.btnClose);
        this.Controls.Add(this.btnShow);
        this.Controls.Add(this.lstMessages);
        this.Name = "frmMain";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Inheritance";
        this.ResumeLayout(false);

    }
    #endregion

    public frmMain()
    {
        InitializeComponent();
        myTime = DateTime.Now;

        myApt = new clsApartment("123 Ann Dotson Dr., Lexington, KY 40502", 550000, 6000,
                                             15000, 3400, myTime, 1);
        myComm = new clsCommercial("4442 Parker Place, York, SC 29745", 1200000, 9000,
                                        22000, 8000, myTime, 2);
        myHome = new clsHome("657 Dallas St, Ringgold, GA 30736", 260000, 1100,
                                    1750, 900, myTime, 3);
    }

    public static void Main()
    {
        frmMain main = new frmMain();
        Application.Run(main);
    }


    private void ShowProperty(string[] str)
    {
        int i;


        for (i = 0; i < str.Length; i++)
            lstMessages.Items.Add(str[i]);
    }
    private void btnClose_Click(object sender, EventArgs e)
    {
        Close();
    }
    // Show the properties
    private void btnShow_Click(object sender, EventArgs e)
    {
        string[] desc = new string[3];

        myApt.PropertySummary(desc);
        ShowProperty(desc);

        myComm.PropertySummary(desc);
        ShowProperty(desc);

        myHome.PropertySummary(desc);
        ShowProperty(desc);

    }

    private void btnRemoveSnow_Click(object sender, EventArgs e)
    {
        lstMessages.Items.Add(myApt.RemoveSnow());
        lstMessages.Items.Add(myComm.RemoveSnow());
        lstMessages.Items.Add(myHome.RemoveSnow());
        lstMessages.Items.Add("");
    }
}