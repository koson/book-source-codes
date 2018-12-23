using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.Sql;
using System.Collections;
using System.Data.SqlClient;


public class frmServerSelect : Form
{

    private const int SYSTEMDBTYPES = 4;

    #region Windows stuff
    private Label label3;
    private ComboBox cmbServer;
    private ComboBox cmbDatabase;
    private Label lblDb;
    private Button btnServer;
    private Button btnClose;

    private void InitializeComponent()
    {
            this.label3 = new System.Windows.Forms.Label();
            this.cmbServer = new System.Windows.Forms.ComboBox();
            this.cmbDatabase = new System.Windows.Forms.ComboBox();
            this.lblDb = new System.Windows.Forms.Label();
            this.btnServer = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Location = new System.Drawing.Point(24, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "Server to Use:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbServer
            // 
            this.cmbServer.FormattingEnabled = true;
            this.cmbServer.Location = new System.Drawing.Point(130, 17);
            this.cmbServer.Name = "cmbServer";
            this.cmbServer.Size = new System.Drawing.Size(640, 21);
            this.cmbServer.TabIndex = 1;
            // 
            // cmbDatabase
            // 
            this.cmbDatabase.FormattingEnabled = true;
            this.cmbDatabase.Location = new System.Drawing.Point(130, 120);
            this.cmbDatabase.Name = "cmbDatabase";
            this.cmbDatabase.Size = new System.Drawing.Size(640, 21);
            this.cmbDatabase.TabIndex = 3;
            this.cmbDatabase.Visible = false;
            // 
            // lblDb
            // 
            this.lblDb.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDb.Location = new System.Drawing.Point(24, 121);
            this.lblDb.Name = "lblDb";
            this.lblDb.Size = new System.Drawing.Size(100, 20);
            this.lblDb.TabIndex = 2;
            this.lblDb.Text = "Database to Use:";
            this.lblDb.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblDb.Visible = false;
            // 
            // btnServer
            // 
            this.btnServer.Location = new System.Drawing.Point(643, 53);
            this.btnServer.Name = "btnServer";
            this.btnServer.Size = new System.Drawing.Size(127, 23);
            this.btnServer.TabIndex = 5;
            this.btnServer.Text = "&Select Server";
            this.btnServer.UseVisualStyleBackColor = true;
            this.btnServer.Click += new System.EventHandler(this.btnServer_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(643, 173);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(127, 23);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmServerSelect
            // 
            this.ClientSize = new System.Drawing.Size(795, 222);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnServer);
            this.Controls.Add(this.cmbDatabase);
            this.Controls.Add(this.lblDb);
            this.Controls.Add(this.cmbServer);
            this.Controls.Add(this.label3);
            this.Name = "frmServerSelect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select Server and Database";
            this.ResumeLayout(false);

    }
    #endregion

    private frmMain mdiParent;

    string serverToUse;
 
    //==================================== Constructor ===========================
    public frmServerSelect(frmMain me)
    {
        InitializeComponent();
        this.mdiParent = me;
        string serverName;
            
         try
        {
           clsSqlServerList SqlSL = new clsSqlServerList();
           SqlDataSourceEnumerator instance = SqlDataSourceEnumerator.Instance;
           DataTable mySources = instance.GetDataSources();
           foreach (DataRow row in mySources.Rows)
            {
                SqlSL = new clsSqlServerList();
                serverName = row[0].ToString();
                cmbServer.Items.Add(serverName);
            }
           cmbServer.SelectedIndex = 0;
        } catch (Exception ex)
        {
            MessageBox.Show("Error: " + ex.Message);
        }

    }

    /*****
     * Purpose: List the available DB's
     * 
     * Parameter list:
     *  object sender   control that caused the event
     *  EventArgs e     details about the sender
     *  
     * Return value:
     *  void
     ******/
    private void btnServer_Click(object sender, EventArgs e)
    {
        short i;
 
        cmbServer.SelectedIndex = 0;                    // Use the server they selected to list databases
        serverToUse = cmbServer.SelectedItem.ToString();
 
        string conn = "Data Source=" + serverToUse + "; Integrated Security=True;";
        cmbDatabase.Visible = true;
        lblDb.Visible = true;

        try
        {
            using (SqlConnection sqlConn = new SqlConnection(conn))
            {
                sqlConn.Open();
                DataTable tblDbs = sqlConn.GetSchema("Databases");
                sqlConn.Close();

                foreach (DataRow row in tblDbs.Rows)
                {
                    i = (short)row.ItemArray[1];
                    if (i > SYSTEMDBTYPES)
                        cmbDatabase.Items.Add(row["database_name"].ToString());
                }
            }
            cmbDatabase.SelectedIndex = 0;
        } catch (SqlException ex)
        {
            MessageBox.Show("Error occurred while reading database data: " + ex.Message);
        }      
    }


    private void btnClose_Click(object sender, EventArgs e)
    {
        // Now send the selections back to the parent.
        this.mdiParent.getServerName = cmbServer.SelectedItem.ToString();
        this.mdiParent.getDatabaseName = cmbDatabase.SelectedItem.ToString();
        Close();
    }
}
