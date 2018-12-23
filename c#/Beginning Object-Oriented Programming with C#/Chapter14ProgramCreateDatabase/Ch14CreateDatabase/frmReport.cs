using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Windows.Forms;


public class frmReport : Form
{
    private Label label1;
    private ListBox lstTables;
    private Label label2;
    private TextBox txtQuery;
    private Button btnExecute;
    private Button btnClose;
    private DataGridView dgvFriends;

    private frmMain mdiParent;
    private string connectString;
    private string serverName;
    private string databaseName;
    private Button btnNew;
    private string sql;
 
    public frmReport(frmMain me)
    {
        InitializeComponent();
        this.mdiParent = me;
        serverName = me.getServerName;
        databaseName = me.getDatabaseName;
        connectString = me.getConnectString;


        sql = "SELECT * FROM " + databaseName + ".sys.tables";

        try
        {
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                conn.Open();
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand(sql, conn);
                myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    if (myReader[0].Equals("sysdiagrams"))  // Don't want system stuff here
                    {
                        continue;
                    }

                    lstTables.Items.Add(myReader[0]);

                }
                myReader.Close();
                conn.Close();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error: " + ex.Message);
        }

   }

    #region Windows code
    private void InitializeComponent()
    {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.lstTables = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtQuery = new System.Windows.Forms.TextBox();
            this.btnExecute = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.dgvFriends = new System.Windows.Forms.DataGridView();
            this.btnNew = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFriends)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Database Tables";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lstTables
            // 
            this.lstTables.FormattingEnabled = true;
            this.lstTables.Location = new System.Drawing.Point(12, 32);
            this.lstTables.Name = "lstTables";
            this.lstTables.Size = new System.Drawing.Size(141, 147);
            this.lstTables.TabIndex = 1;
            this.lstTables.DoubleClick += new System.EventHandler(this.lstTables_DoubleClick);
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(159, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(616, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "SQL Query";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtQuery
            // 
            this.txtQuery.Location = new System.Drawing.Point(159, 32);
            this.txtQuery.Name = "txtQuery";
            this.txtQuery.Size = new System.Drawing.Size(616, 20);
            this.txtQuery.TabIndex = 3;
            this.txtQuery.Text = "SELECT * FROM";
            // 
            // btnExecute
            // 
            this.btnExecute.Location = new System.Drawing.Point(159, 78);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(138, 23);
            this.btnExecute.TabIndex = 4;
            this.btnExecute.Text = "&Execute Query";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(637, 78);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(138, 23);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // dgvFriends
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.dgvFriends.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvFriends.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvFriends.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFriends.Location = new System.Drawing.Point(12, 203);
            this.dgvFriends.Name = "dgvFriends";
            this.dgvFriends.Size = new System.Drawing.Size(763, 307);
            this.dgvFriends.TabIndex = 6;
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(340, 78);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(138, 23);
            this.btnNew.TabIndex = 7;
            this.btnNew.Text = "&New Query";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // frmReport
            // 
            this.ClientSize = new System.Drawing.Size(787, 522);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.dgvFriends);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnExecute);
            this.Controls.Add(this.txtQuery);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lstTables);
            this.Controls.Add(this.label1);
            this.Name = "frmReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Database Report";
            this.Load += new System.EventHandler(this.frmReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFriends)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }
    #endregion

    /*****
     * Purpose: Fills the data grid with the results of the query
     * 
     * Parameter list:
     *  object sender   control that caused the event
     *  EventArgs e     details about the sender
     *  
     * Return value:
     *  void
     *  
     * CAUTION: This code has the ability to execcute most queries, including DELETEs. 
     ******/
    private void btnExecute_Click(object sender, EventArgs e)
    {

        try
        {
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                DataSet myDataSet = new DataSet();
                SqlDataAdapter myAdapter = new SqlDataAdapter(txtQuery.Text, conn);
                myAdapter.Fill(myDataSet);
                dgvFriends.AutoGenerateColumns = true;
                dgvFriends.DataSource = myDataSet.Tables[0];
                conn.Close();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error: " + ex.Message);
        }
    }

    private void lstTables_DoubleClick(object sender, EventArgs e)
    {
        txtQuery.Text += " " + lstTables.SelectedItem;  // Need the space for query string
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void btnNew_Click(object sender, EventArgs e)
    {
        txtQuery.Text = "SELECT * FROM ";
        dgvFriends.DataSource = null;
    }

    private void frmReport_Load(object sender, EventArgs e)
    {

    }

}