using System;
using System.Windows.Forms;
using System.Data.SqlClient;

public class frmEditFriend : Form
{
    Int32 records;

    private string connectStr;

    clsDB myDB;
    clsFriend myData;

    private GroupBox groupBox1;
    private CheckBox chkStatus;
    private TextBox txtLastContact;
    private Label label13;
    private Label label6;
    private TextBox txtZip;
    private Label label7;
    private TextBox txtState;
    private Label label8;
    private TextBox txtCity;
    private Label label5;
    private TextBox txtAddr2;
    private Label label4;
    private TextBox txtAddr1;
    private Label label3;
    private TextBox txtLastName;
    private Label label1;
    private TextBox txtFirstName;
    private Label label2;
    private TextBox txtFindRecordNumber;
    private Button btnSave;
    private Button btnClear;
    private Button btnClose;
    private Button btnFindRecord;


    #region Windows code
    private void InitializeComponent()
    {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkStatus = new System.Windows.Forms.CheckBox();
            this.txtLastContact = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtZip = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtState = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtCity = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtAddr2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtAddr1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFindRecordNumber = new System.Windows.Forms.TextBox();
            this.btnFindRecord = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkStatus);
            this.groupBox1.Controls.Add(this.txtLastContact);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtZip);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtState);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtCity);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtAddr2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtAddr1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtLastName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtFirstName);
            this.groupBox1.Location = new System.Drawing.Point(12, 54);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(512, 136);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Personal Information:";
            // 
            // chkStatus
            // 
            this.chkStatus.AutoSize = true;
            this.chkStatus.Checked = true;
            this.chkStatus.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkStatus.Location = new System.Drawing.Point(440, 108);
            this.chkStatus.Name = "chkStatus";
            this.chkStatus.Size = new System.Drawing.Size(56, 17);
            this.chkStatus.TabIndex = 28;
            this.chkStatus.Text = "Active";
            this.chkStatus.UseVisualStyleBackColor = true;
            // 
            // txtLastContact
            // 
            this.txtLastContact.Location = new System.Drawing.Point(90, 102);
            this.txtLastContact.Name = "txtLastContact";
            this.txtLastContact.Size = new System.Drawing.Size(83, 20);
            this.txtLastContact.TabIndex = 25;
            this.txtLastContact.Text = "1/22/2012";
            // 
            // label13
            // 
            this.label13.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label13.Location = new System.Drawing.Point(6, 102);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(83, 20);
            this.label13.TabIndex = 24;
            this.label13.Text = "Last Contact:";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label6.Location = new System.Drawing.Point(340, 83);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 20);
            this.label6.TabIndex = 15;
            this.label6.Text = "Zip Code:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtZip
            // 
            this.txtZip.Location = new System.Drawing.Point(413, 82);
            this.txtZip.Name = "txtZip";
            this.txtZip.Size = new System.Drawing.Size(83, 20);
            this.txtZip.TabIndex = 14;
            this.txtZip.Text = "33495";
            // 
            // label7
            // 
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label7.Location = new System.Drawing.Point(262, 82);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 20);
            this.label7.TabIndex = 13;
            this.label7.Text = "State:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtState
            // 
            this.txtState.Location = new System.Drawing.Point(306, 82);
            this.txtState.Name = "txtState";
            this.txtState.Size = new System.Drawing.Size(32, 20);
            this.txtState.TabIndex = 12;
            this.txtState.Text = "fl";
            // 
            // label8
            // 
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label8.Location = new System.Drawing.Point(6, 82);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 20);
            this.label8.TabIndex = 11;
            this.label8.Text = "City:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCity
            // 
            this.txtCity.Location = new System.Drawing.Point(90, 82);
            this.txtCity.Name = "txtCity";
            this.txtCity.Size = new System.Drawing.Size(170, 20);
            this.txtCity.TabIndex = 10;
            this.txtCity.Text = "Boca Raton";
            // 
            // label5
            // 
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label5.Location = new System.Drawing.Point(6, 61);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 20);
            this.label5.TabIndex = 9;
            this.label5.Text = "Address2:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAddr2
            // 
            this.txtAddr2.Location = new System.Drawing.Point(90, 61);
            this.txtAddr2.Name = "txtAddr2";
            this.txtAddr2.Size = new System.Drawing.Size(406, 20);
            this.txtAddr2.TabIndex = 8;
            this.txtAddr2.Text = "Unit 1409";
            // 
            // label4
            // 
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label4.Location = new System.Drawing.Point(6, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "Address1:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAddr1
            // 
            this.txtAddr1.Location = new System.Drawing.Point(90, 40);
            this.txtAddr1.Name = "txtAddr1";
            this.txtAddr1.Size = new System.Drawing.Size(406, 20);
            this.txtAddr1.TabIndex = 6;
            this.txtAddr1.Text = "171 Newport St";
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Location = new System.Drawing.Point(192, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Last Name:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtLastName
            // 
            this.txtLastName.Location = new System.Drawing.Point(269, 18);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(227, 20);
            this.txtLastName.TabIndex = 4;
            this.txtLastName.Text = "Yourfriend";
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(6, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "First Name:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFirstName
            // 
            this.txtFirstName.Location = new System.Drawing.Point(90, 19);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(100, 20);
            this.txtFirstName.TabIndex = 0;
            this.txtFirstName.Text = "Lynne";
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 20);
            this.label2.TabIndex = 10;
            this.label2.Text = "Record to find:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFindRecordNumber
            // 
            this.txtFindRecordNumber.Location = new System.Drawing.Point(116, 9);
            this.txtFindRecordNumber.Name = "txtFindRecordNumber";
            this.txtFindRecordNumber.Size = new System.Drawing.Size(100, 20);
            this.txtFindRecordNumber.TabIndex = 11;
            this.txtFindRecordNumber.Text = "1";
            // 
            // btnFindRecord
            // 
            this.btnFindRecord.Location = new System.Drawing.Point(425, 6);
            this.btnFindRecord.Name = "btnFindRecord";
            this.btnFindRecord.Size = new System.Drawing.Size(99, 23);
            this.btnFindRecord.TabIndex = 12;
            this.btnFindRecord.Text = "&Find";
            this.btnFindRecord.UseVisualStyleBackColor = true;
            this.btnFindRecord.Click += new System.EventHandler(this.btnFindRecord_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(12, 213);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(99, 23);
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(166, 213);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(99, 23);
            this.btnClear.TabIndex = 14;
            this.btnClear.Text = "Clear &Fields";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(425, 213);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(99, 23);
            this.btnClose.TabIndex = 15;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmEditFriend
            // 
            this.ClientSize = new System.Drawing.Size(567, 284);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnFindRecord);
            this.Controls.Add(this.txtFindRecordNumber);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmEditFriend";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Friend Data";
            this.Load += new System.EventHandler(this.frmEditFriend_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }
    #endregion


    //=================================== Constructors =========================
    public frmEditFriend()
    {
        InitializeComponent();
        ClearFields();
    }

    public frmEditFriend(string connect)
        : this()
    {
        connectStr = connect;
        myDB = new clsDB(connectStr);
        records = myDB.ReadRecordCount(connectStr);
        txtFindRecordNumber.Focus();
    }



    #region Reading-Displaying data

    //==================================== Helper Methods ============================

    /*****
     * Purpose: Move the record data into the textboxes
     * 
     * Parameter list:
     *  n/a
     *  
     * Return value:
     *  void
     ******/
    private void ShowOneRecord()
    {
        txtFindRecordNumber.Text = myData.ID.ToString();
        txtFirstName.Text = myData.FirstName;
        txtLastName.Text = myData.LastName;
        txtAddr1.Text = myData.Address1;
        txtAddr2.Text = myData.Address2;
        txtCity.Text = myData.City;
        txtState.Text = myData.State;
        txtZip.Text = myData.Zip;
        txtLastContact.Text = myData.LastContact;
        int status = myData.Status;
        if (status == 1)
            chkStatus.Checked = true;
        else
            chkStatus.Checked = false;

    }

    #endregion



    #region Save-Add-Delete buttons code


    /*****
     * Purpose: Clears out the textboxes and gets ready to accept new record
     * 
     * Parameter list:
     *  object sender   control that caused the event
     *  EventArgs e     details about the sender
     *  
     * Return value:
     *  void
     ******/
    private void btnAdd_Click(object sender, EventArgs e)
    {
        ClearFields();

    }

    private void ClearFields()
    {
        txtFindRecordNumber.Text = "";
        txtFirstName.Text = "";
        txtLastName.Text = "";
        txtAddr1.Text = "";
        txtAddr2.Text = "";
        txtCity.Text = "";
        txtState.Text = "";
        txtZip.Text = "";
        txtLastContact.Text = "";

        txtLastName.Focus();
    }

    #endregion

    /*****
     * Purpose: Find info for a record.
     * 
     * Parameter list:
     *  object sender   control that caused the event
     *  EventArgs e     details about the sender
     *  
     * Return value:
     *  void
     ******/
    private void btnFindRecord_Click(object sender, EventArgs e)
    {
        int retValue;
        string sql;

        if (txtLastName.Text.Length == 0)
            sql = "SELECT * FROM Friends WHERE ID = " + txtFindRecordNumber.Text;           // Search by record ID
        else
            sql = "SELECT * FROM Friends WHERE LastName = '" + txtLastName.Text + "'";      // Search by Name

        myData = new clsFriend();
        retValue = myData.ReadOneRecord(sql, connectStr);
        if (retValue > 0)
            ShowOneRecord();
        else
            MessageBox.Show("Could not read friend record");

    }

    private void frmEditFriend_Load(object sender, EventArgs e)
    {

    }

    private void btnClear_Click(object sender, EventArgs e)
    {
        ClearFields();
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
        Close();
    }

    /*****
     * Purpose: Save textbox info as a record in Friends table.
     * 
     * Parameter list:
     *  object sender   control that caused the event
     *  EventArgs e     details about the sender
     *  
     * Return value:
     *  void
     ******/
    private void btnSave_Click(object sender, EventArgs e)
    {
        int status;
        int flag;
        string sqlCommand;


        if (chkStatus.Checked == true)
            status = 1;
        else
            status = 0;

        myData = new clsFriend(connectStr);

        // Build UPDATE command
        sqlCommand = "UPDATE Friends SET " +
                     "FirstName = '" + txtFirstName.Text + "'," +
                     "LastName = '" + txtLastName.Text + "'," +
                     "Addr1 = '" + txtAddr1.Text + "'," +
                     "Addr2 = '" + txtAddr2.Text + "'," +
                     "City = '" + txtCity.Text + "'," +
                     "State = '" + txtState.Text.ToUpper() + "'," +
                     "Zip = '" + txtZip.Text + "'," +
                     "LastContact = '" + txtLastContact.Text + "'," +
                     "Status = " + status.ToString() + 
                     " WHERE ID = " + txtFindRecordNumber.Text;
        try
        {
            flag = myData.ProcessCommand(sqlCommand);
            if (flag > 0)
            {
                MessageBox.Show("Record updated successfully.");
            }
            else
            {
                MessageBox.Show("Failed to update data.", "Process Error");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error: " + ex.Message);
        }
    }

}