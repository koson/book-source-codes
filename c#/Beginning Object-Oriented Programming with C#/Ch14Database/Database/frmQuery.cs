using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

public class frmQuery : Form
{
    private const int MAXCOLUMNS = 1000;    // Probably safe

    string conString;
    string name;
    clsDB myDB;
    OleDbConnection conn;
    OleDbCommand command;
    OleDbDataAdapter adapter;
    DataSet ds;

    private string dbName;
    private string dbTable;
    private DataGridView dataGridView1;
    private Label label1;
    private TextBox txtQuery;
    private Button btnExecute;
    private ListBox lstTables;
    private Label label2;
    private Button btnClose;
 
    #region Windows code
    private void InitializeComponent()
    {
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
        this.dataGridView1 = new System.Windows.Forms.DataGridView();
        this.label1 = new System.Windows.Forms.Label();
        this.txtQuery = new System.Windows.Forms.TextBox();
        this.btnExecute = new System.Windows.Forms.Button();
        this.btnClose = new System.Windows.Forms.Button();
        this.lstTables = new System.Windows.Forms.ListBox();
        this.label2 = new System.Windows.Forms.Label();
        ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
        this.SuspendLayout();
        // 
        // dataGridView1
        // 
        dataGridViewCellStyle1.BackColor = System.Drawing.Color.PaleGreen;
        this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
        this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
        this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dataGridView1.Cursor = System.Windows.Forms.Cursors.Cross;
        this.dataGridView1.Location = new System.Drawing.Point(12, 188);
        this.dataGridView1.Name = "dataGridView1";
        this.dataGridView1.Size = new System.Drawing.Size(509, 288);
        this.dataGridView1.TabIndex = 0;
        // 
        // label1
        // 
        this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.label1.Location = new System.Drawing.Point(149, 12);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(115, 20);
        this.label1.TabIndex = 2;
        this.label1.Text = "Enter SQL Query:";
        this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // txtQuery
        // 
        this.txtQuery.AcceptsReturn = true;
        this.txtQuery.Location = new System.Drawing.Point(149, 35);
        this.txtQuery.Name = "txtQuery";
        this.txtQuery.Size = new System.Drawing.Size(372, 20);
        this.txtQuery.TabIndex = 3;
        // 
        // btnExecute
        // 
        this.btnExecute.Location = new System.Drawing.Point(149, 98);
        this.btnExecute.Name = "btnExecute";
        this.btnExecute.Size = new System.Drawing.Size(100, 23);
        this.btnExecute.TabIndex = 4;
        this.btnExecute.Text = "&Execute";
        this.btnExecute.UseVisualStyleBackColor = true;
        this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
        // 
        // btnClose
        // 
        this.btnClose.Location = new System.Drawing.Point(421, 98);
        this.btnClose.Name = "btnClose";
        this.btnClose.Size = new System.Drawing.Size(100, 23);
        this.btnClose.TabIndex = 5;
        this.btnClose.Text = "&Close";
        this.btnClose.UseVisualStyleBackColor = true;
        this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
        // 
        // lstTables
        // 
        this.lstTables.FormattingEnabled = true;
        this.lstTables.Location = new System.Drawing.Point(12, 35);
        this.lstTables.Name = "lstTables";
        this.lstTables.Size = new System.Drawing.Size(120, 147);
        this.lstTables.TabIndex = 6;
        this.lstTables.DoubleClick += new System.EventHandler(this.lstTables_DoubleClick);
        // 
        // label2
        // 
        this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.label2.Location = new System.Drawing.Point(12, 12);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(120, 20);
        this.label2.TabIndex = 7;
        this.label2.Text = "Database Tables";
        this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // frmQuery
        // 
        this.ClientSize = new System.Drawing.Size(529, 488);
        this.Controls.Add(this.label2);
        this.Controls.Add(this.lstTables);
        this.Controls.Add(this.btnClose);
        this.Controls.Add(this.btnExecute);
        this.Controls.Add(this.txtQuery);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.dataGridView1);
        this.Name = "frmQuery";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Friend Table Results";
        this.Load += new System.EventHandler(this.frmQuery_Load);
        ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();

    }
    #endregion

    public frmQuery(string dbN)
    {
        int index;

        InitializeComponent();
        dbName = dbN;

        index = dbName.LastIndexOf("\\");   // Get just table name
        name = dbName.Substring(index + 1);

        // Copy DB name and table to title bar
        this.Text = "Query " + name;

        myDB = new clsDB(dbName);           // Instantiate objects
        conString = myDB.getConnectString + dbName;
        conn = new OleDbConnection(conString);
        command = conn.CreateCommand();

        txtQuery.Text = "SELECT * FROM "; // Default query

    }
    private void btnExecute_Click(object sender, EventArgs e)
    {
        DoQuery();
    }

    private void DoQuery()
    {
        try
        {
            ds = new DataSet();     // Instantiate DataSet object
            conn.Open();
            command.CommandText = txtQuery.Text;

            adapter = new OleDbDataAdapter(command);

            adapter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];

            command.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error: " + ex.Message);
        }
        finally
        {
            conn.Close();
        }
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void frmQuery_Load(object sender, EventArgs e)
    {
        int i = 0;
        string[] colNames = new string[MAXCOLUMNS];

        myDB.GetTableInfo(colNames);    // Get the table names from database

        while (true)
        {
            if (colNames[i] != null)
                lstTables.Items.Add(colNames[i++]); // Show tables
            else
                break;
        }
    }

    private void lstTables_DoubleClick(object sender, EventArgs e)
    {
        dbTable = (string)lstTables.SelectedItem;
        txtQuery.Text = "SELECT * FROM " + dbTable;
        DoQuery();      // Comment this call out to stop auto-query
    }
   
}

