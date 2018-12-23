using System;
using System.Windows.Forms;

public class frmEditFriend : Form
{
    const string TESTDATAFILE = "Friends.bin";

    long recs;
    long currentRecord = 1;
    clsRandomAccess myData = new clsRandomAccess(TESTDATAFILE);

    #region Initialization Step code
    private GroupBox groupBox1;
    private Label label2;
    private TextBox txtMI;
    private Label label1;
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
    private TextBox txtWork;
    private Label label11;
    private TextBox txtCell;
    private Label label10;
    private TextBox txtHome;
    private Label label9;
    private Label label13;
    private Label label12;
    private TextBox txtEmail;
    private CheckBox chkStatus;
    private TextBox txtAnniversary;
    private Label label14;
    private TextBox txtBirthday;
    private Button btnAdd;
    private Button btnSave;
    private Button btnDelete;
    private Button btnClose;
    private GroupBox groupBox2;
    private Button btnFirst;
    private Button btnLast;
    private Button btnPrevious;
    private Button btnNext;
    private Label label15;
    private TextBox txtRecord;
    private TextBox txtFirstName;
    #region Windows code
    private void InitializeComponent()
    {
        this.groupBox1 = new System.Windows.Forms.GroupBox();
        this.chkStatus = new System.Windows.Forms.CheckBox();
        this.txtAnniversary = new System.Windows.Forms.TextBox();
        this.label14 = new System.Windows.Forms.Label();
        this.txtBirthday = new System.Windows.Forms.TextBox();
        this.label13 = new System.Windows.Forms.Label();
        this.label12 = new System.Windows.Forms.Label();
        this.txtEmail = new System.Windows.Forms.TextBox();
        this.txtWork = new System.Windows.Forms.TextBox();
        this.label11 = new System.Windows.Forms.Label();
        this.txtCell = new System.Windows.Forms.TextBox();
        this.label10 = new System.Windows.Forms.Label();
        this.txtHome = new System.Windows.Forms.TextBox();
        this.label9 = new System.Windows.Forms.Label();
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
        this.label2 = new System.Windows.Forms.Label();
        this.txtMI = new System.Windows.Forms.TextBox();
        this.label1 = new System.Windows.Forms.Label();
        this.txtFirstName = new System.Windows.Forms.TextBox();
        this.btnAdd = new System.Windows.Forms.Button();
        this.btnSave = new System.Windows.Forms.Button();
        this.btnDelete = new System.Windows.Forms.Button();
        this.btnClose = new System.Windows.Forms.Button();
        this.groupBox2 = new System.Windows.Forms.GroupBox();
        this.btnLast = new System.Windows.Forms.Button();
        this.btnPrevious = new System.Windows.Forms.Button();
        this.btnNext = new System.Windows.Forms.Button();
        this.btnFirst = new System.Windows.Forms.Button();
        this.label15 = new System.Windows.Forms.Label();
        this.txtRecord = new System.Windows.Forms.TextBox();
        this.groupBox1.SuspendLayout();
        this.groupBox2.SuspendLayout();
        this.SuspendLayout();
        // 
        // groupBox1
        // 
        this.groupBox1.Controls.Add(this.chkStatus);
        this.groupBox1.Controls.Add(this.txtAnniversary);
        this.groupBox1.Controls.Add(this.label14);
        this.groupBox1.Controls.Add(this.txtBirthday);
        this.groupBox1.Controls.Add(this.label13);
        this.groupBox1.Controls.Add(this.label12);
        this.groupBox1.Controls.Add(this.txtEmail);
        this.groupBox1.Controls.Add(this.txtWork);
        this.groupBox1.Controls.Add(this.label11);
        this.groupBox1.Controls.Add(this.txtCell);
        this.groupBox1.Controls.Add(this.label10);
        this.groupBox1.Controls.Add(this.txtHome);
        this.groupBox1.Controls.Add(this.label9);
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
        this.groupBox1.Controls.Add(this.label2);
        this.groupBox1.Controls.Add(this.txtMI);
        this.groupBox1.Controls.Add(this.label1);
        this.groupBox1.Controls.Add(this.txtFirstName);
        this.groupBox1.Location = new System.Drawing.Point(12, 12);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new System.Drawing.Size(512, 176);
        this.groupBox1.TabIndex = 0;
        this.groupBox1.TabStop = false;
        this.groupBox1.Text = "Personal Information:";
        // 
        // chkStatus
        // 
        this.chkStatus.AutoSize = true;
        this.chkStatus.Checked = true;
        this.chkStatus.CheckState = System.Windows.Forms.CheckState.Checked;
        this.chkStatus.Location = new System.Drawing.Point(440, 148);
        this.chkStatus.Name = "chkStatus";
        this.chkStatus.Size = new System.Drawing.Size(56, 17);
        this.chkStatus.TabIndex = 28;
        this.chkStatus.Text = "Active";
        this.chkStatus.UseVisualStyleBackColor = true;
        // 
        // txtAnniversary
        // 
        this.txtAnniversary.Location = new System.Drawing.Point(259, 146);
        this.txtAnniversary.Name = "txtAnniversary";
        this.txtAnniversary.Size = new System.Drawing.Size(79, 20);
        this.txtAnniversary.TabIndex = 27;
        this.txtAnniversary.Text = "n/a";
        // 
        // label14
        // 
        this.label14.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.label14.Location = new System.Drawing.Point(175, 146);
        this.label14.Name = "label14";
        this.label14.Size = new System.Drawing.Size(83, 20);
        this.label14.TabIndex = 26;
        this.label14.Text = "Anniversary:";
        this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // txtBirthday
        // 
        this.txtBirthday.Location = new System.Drawing.Point(90, 146);
        this.txtBirthday.Name = "txtBirthday";
        this.txtBirthday.Size = new System.Drawing.Size(83, 20);
        this.txtBirthday.TabIndex = 25;
        this.txtBirthday.Text = "5/10/1949";
        // 
        // label13
        // 
        this.label13.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.label13.Location = new System.Drawing.Point(6, 145);
        this.label13.Name = "label13";
        this.label13.Size = new System.Drawing.Size(83, 20);
        this.label13.TabIndex = 24;
        this.label13.Text = "Birthday:";
        this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // label12
        // 
        this.label12.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.label12.Location = new System.Drawing.Point(6, 124);
        this.label12.Name = "label12";
        this.label12.Size = new System.Drawing.Size(83, 20);
        this.label12.TabIndex = 23;
        this.label12.Text = "Email:";
        this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // txtEmail
        // 
        this.txtEmail.Location = new System.Drawing.Point(90, 125);
        this.txtEmail.Name = "txtEmail";
        this.txtEmail.Size = new System.Drawing.Size(406, 20);
        this.txtEmail.TabIndex = 22;
        this.txtEmail.Text = "issy@rockytopemail.com";
        // 
        // txtWork
        // 
        this.txtWork.Location = new System.Drawing.Point(413, 103);
        this.txtWork.Name = "txtWork";
        this.txtWork.Size = new System.Drawing.Size(83, 20);
        this.txtWork.TabIndex = 21;
        this.txtWork.Text = "706.567.8901";
        // 
        // label11
        // 
        this.label11.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.label11.Location = new System.Drawing.Point(340, 103);
        this.label11.Name = "label11";
        this.label11.Size = new System.Drawing.Size(72, 20);
        this.label11.TabIndex = 20;
        this.label11.Text = "Work:";
        this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // txtCell
        // 
        this.txtCell.Location = new System.Drawing.Point(245, 103);
        this.txtCell.Name = "txtCell";
        this.txtCell.Size = new System.Drawing.Size(93, 20);
        this.txtCell.TabIndex = 19;
        this.txtCell.Text = "706.234.5555";
        // 
        // label10
        // 
        this.label10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.label10.Location = new System.Drawing.Point(175, 103);
        this.label10.Name = "label10";
        this.label10.Size = new System.Drawing.Size(69, 20);
        this.label10.TabIndex = 18;
        this.label10.Text = "Cell:";
        this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // txtHome
        // 
        this.txtHome.Location = new System.Drawing.Point(90, 103);
        this.txtHome.Name = "txtHome";
        this.txtHome.Size = new System.Drawing.Size(83, 20);
        this.txtHome.TabIndex = 17;
        this.txtHome.Text = "706.321.4321";
        // 
        // label9
        // 
        this.label9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.label9.Location = new System.Drawing.Point(6, 103);
        this.label9.Name = "label9";
        this.label9.Size = new System.Drawing.Size(83, 20);
        this.label9.TabIndex = 16;
        this.label9.Text = "Home Phone:";
        this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
        this.txtZip.Text = "30736";
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
        this.txtState.Text = "GA";
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
        this.txtCity.Text = "Ringgold";
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
        this.txtAddr1.Text = "657 Main Street";
        // 
        // label3
        // 
        this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.label3.Location = new System.Drawing.Point(263, 19);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(75, 20);
        this.label3.TabIndex = 5;
        this.label3.Text = "Last Name:";
        this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // txtLastName
        // 
        this.txtLastName.Location = new System.Drawing.Point(340, 19);
        this.txtLastName.Name = "txtLastName";
        this.txtLastName.Size = new System.Drawing.Size(156, 20);
        this.txtLastName.TabIndex = 4;
        this.txtLastName.Text = "Smith";
        // 
        // label2
        // 
        this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.label2.Location = new System.Drawing.Point(192, 19);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(41, 20);
        this.label2.TabIndex = 3;
        this.label2.Text = "Initial:";
        this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // txtMI
        // 
        this.txtMI.Location = new System.Drawing.Point(234, 19);
        this.txtMI.Name = "txtMI";
        this.txtMI.Size = new System.Drawing.Size(27, 20);
        this.txtMI.TabIndex = 2;
        this.txtMI.Text = "C";
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
        this.txtFirstName.Text = "Issy";
        // 
        // btnAdd
        // 
        this.btnAdd.Location = new System.Drawing.Point(530, 27);
        this.btnAdd.Name = "btnAdd";
        this.btnAdd.Size = new System.Drawing.Size(85, 23);
        this.btnAdd.TabIndex = 1;
        this.btnAdd.Text = "&Add New";
        this.btnAdd.UseVisualStyleBackColor = true;
        this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
        // 
        // btnSave
        // 
        this.btnSave.Location = new System.Drawing.Point(530, 56);
        this.btnSave.Name = "btnSave";
        this.btnSave.Size = new System.Drawing.Size(85, 23);
        this.btnSave.TabIndex = 2;
        this.btnSave.Text = "&Save";
        this.btnSave.UseVisualStyleBackColor = true;
        this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
        // 
        // btnDelete
        // 
        this.btnDelete.Location = new System.Drawing.Point(530, 115);
        this.btnDelete.Name = "btnDelete";
        this.btnDelete.Size = new System.Drawing.Size(85, 23);
        this.btnDelete.TabIndex = 3;
        this.btnDelete.Text = "&Delete";
        this.btnDelete.UseVisualStyleBackColor = true;
        this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
        // 
        // btnClose
        // 
        this.btnClose.Location = new System.Drawing.Point(530, 165);
        this.btnClose.Name = "btnClose";
        this.btnClose.Size = new System.Drawing.Size(85, 23);
        this.btnClose.TabIndex = 4;
        this.btnClose.Text = "&Close";
        this.btnClose.UseVisualStyleBackColor = true;
        this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
        // 
        // groupBox2
        // 
        this.groupBox2.Controls.Add(this.btnLast);
        this.groupBox2.Controls.Add(this.btnPrevious);
        this.groupBox2.Controls.Add(this.btnNext);
        this.groupBox2.Controls.Add(this.btnFirst);
        this.groupBox2.Location = new System.Drawing.Point(12, 204);
        this.groupBox2.Name = "groupBox2";
        this.groupBox2.Size = new System.Drawing.Size(512, 61);
        this.groupBox2.TabIndex = 5;
        this.groupBox2.TabStop = false;
        this.groupBox2.Text = "Navigate Records:";
        // 
        // btnLast
        // 
        this.btnLast.Location = new System.Drawing.Point(411, 19);
        this.btnLast.Name = "btnLast";
        this.btnLast.Size = new System.Drawing.Size(85, 23);
        this.btnLast.TabIndex = 5;
        this.btnLast.Text = "&Last";
        this.btnLast.UseVisualStyleBackColor = true;
        this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
        // 
        // btnPrevious
        // 
        this.btnPrevious.Location = new System.Drawing.Point(273, 19);
        this.btnPrevious.Name = "btnPrevious";
        this.btnPrevious.Size = new System.Drawing.Size(85, 23);
        this.btnPrevious.TabIndex = 4;
        this.btnPrevious.Text = "&Previous";
        this.btnPrevious.UseVisualStyleBackColor = true;
        this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
        // 
        // btnNext
        // 
        this.btnNext.Location = new System.Drawing.Point(136, 19);
        this.btnNext.Name = "btnNext";
        this.btnNext.Size = new System.Drawing.Size(85, 23);
        this.btnNext.TabIndex = 3;
        this.btnNext.Text = "&Next";
        this.btnNext.UseVisualStyleBackColor = true;
        this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
        // 
        // btnFirst
        // 
        this.btnFirst.Location = new System.Drawing.Point(6, 19);
        this.btnFirst.Name = "btnFirst";
        this.btnFirst.Size = new System.Drawing.Size(85, 23);
        this.btnFirst.TabIndex = 2;
        this.btnFirst.Text = "&First";
        this.btnFirst.UseVisualStyleBackColor = true;
        this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
        // 
        // label15
        // 
        this.label15.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.label15.Location = new System.Drawing.Point(530, 214);
        this.label15.Name = "label15";
        this.label15.Size = new System.Drawing.Size(85, 20);
        this.label15.TabIndex = 6;
        this.label15.Text = "Record #";
        this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // txtRecord
        // 
        this.txtRecord.Location = new System.Drawing.Point(530, 237);
        this.txtRecord.Name = "txtRecord";
        this.txtRecord.Size = new System.Drawing.Size(85, 20);
        this.txtRecord.TabIndex = 28;
        // 
        // frmMain
        // 
        this.ClientSize = new System.Drawing.Size(624, 271);
        this.Controls.Add(this.txtRecord);
        this.Controls.Add(this.label15);
        this.Controls.Add(this.groupBox2);
        this.Controls.Add(this.btnClose);
        this.Controls.Add(this.btnDelete);
        this.Controls.Add(this.btnSave);
        this.Controls.Add(this.btnAdd);
        this.Controls.Add(this.groupBox1);
        this.Name = "frmMain";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Electronic Phone Book";
        this.groupBox1.ResumeLayout(false);
        this.groupBox1.PerformLayout();
        this.groupBox2.ResumeLayout(false);
        this.ResumeLayout(false);
        this.PerformLayout();

    }
    #endregion

    public frmEditFriend()
    {
        InitializeComponent();
    }


    #endregion

    #region Reading-Displaying data

    /*****
     * Purpose: Read a record and display the results
     * 
     * Parameter list:
     *  n/a
     *  
     * Return value:
     *  void
     ******/
    private int ReadAndShowRecord()
    {
        int flag = 1;

        myData.Open(myData.FileName);
        flag = myData.ReadOneRecord(currentRecord - 1);
        if (flag == 1)
        {
            ShowOneRecord();
            txtRecord.Text = currentRecord.ToString();
        }
        else
        {
            MessageBox.Show("Record not available.", "Error Read");
            flag = 0;
        }
        myData.Close();
        return flag;
    }

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
        txtFirstName.Text = myData.FirstName;
        txtLastName.Text = myData.LastName;
        txtAddr1.Text = myData.Address1;
        txtAddr2.Text = myData.Address2;
        txtCity.Text = myData.City;
        txtState.Text = myData.State;
        txtZip.Text = myData.Zip;
        txtHome.Text = myData.HomePhone;
        txtCell.Text = myData.CellPhone;
        txtWork.Text = myData.WorkPhone;
        txtEmail.Text = myData.Email;
        txtBirthday.Text = myData.Birthday;
        txtAnniversary.Text = myData.Anniversary;
        if (myData.Status == 1)
            chkStatus.Checked = true;
        else
            chkStatus.Checked = false;
    }

    /*****
     * Purpose: Copies data from textboxes to class members.
     * 
     * Parameter list:
     *  n/a
     *  
     * Return value:
     *  void
     ******/

    private void CopyData()
    {
        myData.FirstName = txtFirstName.Text;
        myData.MiddleInitial = txtMI.Text;
        myData.LastName = txtLastName.Text;
        myData.Address1 = txtAddr1.Text;
        myData.Address2 = txtAddr2.Text;
        myData.City = txtCity.Text;
        myData.State = txtState.Text;
        myData.Zip = txtZip.Text;
        myData.HomePhone = txtHome.Text;
        myData.CellPhone = txtCell.Text;
        myData.WorkPhone = txtWork.Text;
        myData.Email = txtEmail.Text;
        myData.Birthday = txtBirthday.Text;
        myData.Anniversary = txtAnniversary.Text;
        if (chkStatus.Checked == true)
            myData.Status = 1;
        else
            myData.Status = 0;
    }
    #endregion

    #region Navigation buttons code

    private void btnFirst_Click(object sender, EventArgs e)
    {
        int flag;

        currentRecord = 1;
        flag = ReadAndShowRecord();
    }

    private void btnNext_Click(object sender, EventArgs e)
    {
        int flag;

        currentRecord++;
        flag = ReadAndShowRecord();
        if (flag == 0)
        {
            currentRecord--;
        }
    }

    private void btnPrevious_Click(object sender, EventArgs e)
    {
        int flag;

        currentRecord--;
        flag = ReadAndShowRecord();
        if (flag == 0)
        {
            currentRecord++;
        }
    }

    private void btnLast_Click(object sender, EventArgs e)
    {
        int flag;

        myData.Open(myData.FileName);
        currentRecord = myData.getRecordCount();
        if (currentRecord > -1)
        {
            flag = ReadAndShowRecord();
        }
    }
    #endregion

    #region Save-Add-Delete buttons code
    /*****
     * Purpose: Save textbox info as a record.
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
        CopyData();
        if (myData.Open(TESTDATAFILE) == 1)
        {
            recs = myData.getRecordCount();
            myData.Open(TESTDATAFILE);
            myData.WriteOneRecord(recs);
            myData.Close();
            MessageBox.Show("Data written successfully.");
        }
        else
        {
            MessageBox.Show("Could not open file " + TESTDATAFILE, "File Error");
            return;
        }
    }

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
        txtFirstName.Text = "";
        txtMI.Text = "";
        txtLastName.Text = "";
        txtAddr1.Text = "";
        txtAddr2.Text = "";
        txtCity.Text = "";
        txtState.Text = "";
        txtZip.Text = "";
        txtHome.Text = "";
        txtCell.Text = "";
        txtWork.Text = "";
        txtEmail.Text = "";
        txtBirthday.Text = "";
        txtAnniversary.Text = "";
        if (myData.Status == 1)
            chkStatus.Checked = true;
        else
            chkStatus.Checked = false;

        txtFirstName.Focus();
    }

    /*****
     * Purpose: Deletes a record by changing the status member
     * 
     * Parameter list:
     *  object sender   control that caused the event
     *  EventArgs e     details about the sender
     *  
     * Return value:
     *  void
     ******/
    private void btnDelete_Click(object sender, EventArgs e)
    {
        DialogResult ask;

        ask = MessageBox.Show("Are you sure you want to delete this record?", "Delete Record", MessageBoxButtons.YesNo);
        if (ask == DialogResult.Yes)
        {
            myData.Status = 0;
            myData.Open(myData.FileName);
            myData.WriteOneRecord(currentRecord - 1);
            MessageBox.Show("Record deleted", "Delete Record");
        }

    }
    #endregion

    #region Close button code

    private void btnClose_Click(object sender, EventArgs e)
    {
        Close();
    }
    #endregion

}