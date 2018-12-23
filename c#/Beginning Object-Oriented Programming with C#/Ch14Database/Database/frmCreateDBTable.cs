using System;
using System.Windows.Forms;

public class frmCreateDBTable : Form
{
    private string dbName;
    private int length;

    private System.Windows.Forms.TextBox txtTableName;
    private System.Windows.Forms.Label label2;
    private Button btnAddButton;
    private TextBox txtMaxStringLength;
    private Label lbStringLen;
    private Label label3;
    private TextBox txtFieldName;
    private Label label1;
    private GroupBox groupBox1;
    private RadioButton rbBool;
    private RadioButton rbDateTime;
    private RadioButton rbString;
    private RadioButton rbDouble;
    private RadioButton rbFloat;
    private RadioButton rbDecimal;
    private RadioButton rbUnsignedLong;
    private RadioButton rbLong;
    private RadioButton rnUnsignedInt;
    private RadioButton rbUnsignedShort;
    private RadioButton rbInt;
    private RadioButton rbShort;
    private RadioButton rbChar;
    private RadioButton rbByte;
    private Button btnWrite;
    private Button btnClose;
    private ListView lstFieldsToAdd;
    private ColumnHeader fieldName;
    private ColumnHeader Bytes;
    private ColumnHeader Type;
    private GroupBox groupBox2;

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.txtTableName = new System.Windows.Forms.TextBox();
        this.label2 = new System.Windows.Forms.Label();
        this.groupBox2 = new System.Windows.Forms.GroupBox();
        this.lstFieldsToAdd = new System.Windows.Forms.ListView();
        this.fieldName = new System.Windows.Forms.ColumnHeader();
        this.Bytes = new System.Windows.Forms.ColumnHeader();
        this.Type = new System.Windows.Forms.ColumnHeader();
        this.btnAddButton = new System.Windows.Forms.Button();
        this.txtMaxStringLength = new System.Windows.Forms.TextBox();
        this.lbStringLen = new System.Windows.Forms.Label();
        this.label3 = new System.Windows.Forms.Label();
        this.txtFieldName = new System.Windows.Forms.TextBox();
        this.label1 = new System.Windows.Forms.Label();
        this.groupBox1 = new System.Windows.Forms.GroupBox();
        this.rbBool = new System.Windows.Forms.RadioButton();
        this.rbDateTime = new System.Windows.Forms.RadioButton();
        this.rbString = new System.Windows.Forms.RadioButton();
        this.rbDouble = new System.Windows.Forms.RadioButton();
        this.rbFloat = new System.Windows.Forms.RadioButton();
        this.rbDecimal = new System.Windows.Forms.RadioButton();
        this.rbUnsignedLong = new System.Windows.Forms.RadioButton();
        this.rbLong = new System.Windows.Forms.RadioButton();
        this.rnUnsignedInt = new System.Windows.Forms.RadioButton();
        this.rbUnsignedShort = new System.Windows.Forms.RadioButton();
        this.rbInt = new System.Windows.Forms.RadioButton();
        this.rbShort = new System.Windows.Forms.RadioButton();
        this.rbChar = new System.Windows.Forms.RadioButton();
        this.rbByte = new System.Windows.Forms.RadioButton();
        this.btnWrite = new System.Windows.Forms.Button();
        this.btnClose = new System.Windows.Forms.Button();
        this.groupBox2.SuspendLayout();
        this.groupBox1.SuspendLayout();
        this.SuspendLayout();
        // 
        // txtTableName
        // 
        this.txtTableName.Location = new System.Drawing.Point(118, 10);
        this.txtTableName.Name = "txtTableName";
        this.txtTableName.Size = new System.Drawing.Size(176, 20);
        this.txtTableName.TabIndex = 9;
        this.txtTableName.Text = "Friend";
        // 
        // label2
        // 
        this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.label2.Location = new System.Drawing.Point(12, 9);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(100, 20);
        this.label2.TabIndex = 8;
        this.label2.Text = "New Table Name:";
        this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // groupBox2
        // 
        this.groupBox2.Controls.Add(this.lstFieldsToAdd);
        this.groupBox2.Controls.Add(this.btnAddButton);
        this.groupBox2.Controls.Add(this.txtMaxStringLength);
        this.groupBox2.Controls.Add(this.lbStringLen);
        this.groupBox2.Controls.Add(this.label3);
        this.groupBox2.Controls.Add(this.txtFieldName);
        this.groupBox2.Controls.Add(this.label1);
        this.groupBox2.Controls.Add(this.groupBox1);
        this.groupBox2.Location = new System.Drawing.Point(12, 36);
        this.groupBox2.Name = "groupBox2";
        this.groupBox2.Size = new System.Drawing.Size(534, 406);
        this.groupBox2.TabIndex = 15;
        this.groupBox2.TabStop = false;
        this.groupBox2.Text = "New Field Information:";
        // 
        // lstFieldsToAdd
        // 
        this.lstFieldsToAdd.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.fieldName,
            this.Bytes,
            this.Type});
        this.lstFieldsToAdd.FullRowSelect = true;
        this.lstFieldsToAdd.Location = new System.Drawing.Point(282, 15);
        this.lstFieldsToAdd.Name = "lstFieldsToAdd";
        this.lstFieldsToAdd.Size = new System.Drawing.Size(246, 376);
        this.lstFieldsToAdd.TabIndex = 22;
        this.lstFieldsToAdd.UseCompatibleStateImageBehavior = false;
        this.lstFieldsToAdd.View = System.Windows.Forms.View.Details;
        this.lstFieldsToAdd.DoubleClick += new System.EventHandler(this.lstFieldsToAdd_DoubleClick);
        // 
        // fieldName
        // 
        this.fieldName.Text = "Field Name";
        this.fieldName.Width = 120;
        // 
        // Bytes
        // 
        this.Bytes.Text = "Length";
        this.Bytes.Width = 50;
        // 
        // Type
        // 
        this.Type.Text = "Type";
        this.Type.Width = 72;
        // 
        // btnAddButton
        // 
        this.btnAddButton.Location = new System.Drawing.Point(17, 368);
        this.btnAddButton.Name = "btnAddButton";
        this.btnAddButton.Size = new System.Drawing.Size(251, 23);
        this.btnAddButton.TabIndex = 21;
        this.btnAddButton.Text = "Add New Field to Database Table";
        this.btnAddButton.UseVisualStyleBackColor = true;
        this.btnAddButton.Click += new System.EventHandler(this.btnAddButton_Click);
        // 
        // txtMaxStringLength
        // 
        this.txtMaxStringLength.Location = new System.Drawing.Point(154, 327);
        this.txtMaxStringLength.Name = "txtMaxStringLength";
        this.txtMaxStringLength.Size = new System.Drawing.Size(42, 20);
        this.txtMaxStringLength.TabIndex = 20;
        this.txtMaxStringLength.Text = "30";
        // 
        // lbStringLen
        // 
        this.lbStringLen.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbStringLen.Location = new System.Drawing.Point(17, 327);
        this.lbStringLen.Name = "lbStringLen";
        this.lbStringLen.Size = new System.Drawing.Size(131, 20);
        this.lbStringLen.TabIndex = 19;
        this.lbStringLen.Text = "Max string length:";
        this.lbStringLen.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // label3
        // 
        this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.label3.Location = new System.Drawing.Point(17, 95);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(251, 20);
        this.label3.TabIndex = 18;
        this.label3.Text = "New Field Type";
        this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // txtFieldName
        // 
        this.txtFieldName.Location = new System.Drawing.Point(17, 48);
        this.txtFieldName.Name = "txtFieldName";
        this.txtFieldName.Size = new System.Drawing.Size(251, 20);
        this.txtFieldName.TabIndex = 17;
        this.txtFieldName.Text = "Friend";
        // 
        // label1
        // 
        this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.label1.Location = new System.Drawing.Point(17, 25);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(251, 20);
        this.label1.TabIndex = 16;
        this.label1.Text = "New Field Name";
        this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // groupBox1
        // 
        this.groupBox1.Controls.Add(this.rbBool);
        this.groupBox1.Controls.Add(this.rbDateTime);
        this.groupBox1.Controls.Add(this.rbString);
        this.groupBox1.Controls.Add(this.rbDouble);
        this.groupBox1.Controls.Add(this.rbFloat);
        this.groupBox1.Controls.Add(this.rbDecimal);
        this.groupBox1.Controls.Add(this.rbUnsignedLong);
        this.groupBox1.Controls.Add(this.rbLong);
        this.groupBox1.Controls.Add(this.rnUnsignedInt);
        this.groupBox1.Controls.Add(this.rbUnsignedShort);
        this.groupBox1.Controls.Add(this.rbInt);
        this.groupBox1.Controls.Add(this.rbShort);
        this.groupBox1.Controls.Add(this.rbChar);
        this.groupBox1.Controls.Add(this.rbByte);
        this.groupBox1.Location = new System.Drawing.Point(17, 118);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new System.Drawing.Size(251, 197);
        this.groupBox1.TabIndex = 15;
        this.groupBox1.TabStop = false;
        this.groupBox1.Text = "Data Types:";
        // 
        // rbBool
        // 
        this.rbBool.AutoSize = true;
        this.rbBool.Location = new System.Drawing.Point(20, 23);
        this.rbBool.Name = "rbBool";
        this.rbBool.Size = new System.Drawing.Size(64, 17);
        this.rbBool.TabIndex = 13;
        this.rbBool.TabStop = true;
        this.rbBool.Text = "Boolean";
        this.rbBool.UseVisualStyleBackColor = true;
        // 
        // rbDateTime
        // 
        this.rbDateTime.AutoSize = true;
        this.rbDateTime.Location = new System.Drawing.Point(150, 161);
        this.rbDateTime.Name = "rbDateTime";
        this.rbDateTime.Size = new System.Drawing.Size(71, 17);
        this.rbDateTime.TabIndex = 12;
        this.rbDateTime.TabStop = true;
        this.rbDateTime.Text = "DateTime";
        this.rbDateTime.UseVisualStyleBackColor = true;
        // 
        // rbString
        // 
        this.rbString.AutoSize = true;
        this.rbString.Location = new System.Drawing.Point(151, 138);
        this.rbString.Name = "rbString";
        this.rbString.Size = new System.Drawing.Size(52, 17);
        this.rbString.TabIndex = 11;
        this.rbString.TabStop = true;
        this.rbString.Text = "String";
        this.rbString.UseVisualStyleBackColor = true;
        this.rbString.MouseClick += new System.Windows.Forms.MouseEventHandler(this.rbString_MouseClick);
        // 
        // rbDouble
        // 
        this.rbDouble.AutoSize = true;
        this.rbDouble.Location = new System.Drawing.Point(151, 115);
        this.rbDouble.Name = "rbDouble";
        this.rbDouble.Size = new System.Drawing.Size(59, 17);
        this.rbDouble.TabIndex = 10;
        this.rbDouble.TabStop = true;
        this.rbDouble.Text = "Double";
        this.rbDouble.UseVisualStyleBackColor = true;
        // 
        // rbFloat
        // 
        this.rbFloat.AutoSize = true;
        this.rbFloat.Location = new System.Drawing.Point(151, 92);
        this.rbFloat.Name = "rbFloat";
        this.rbFloat.Size = new System.Drawing.Size(48, 17);
        this.rbFloat.TabIndex = 9;
        this.rbFloat.TabStop = true;
        this.rbFloat.Text = "Float";
        this.rbFloat.UseVisualStyleBackColor = true;
        // 
        // rbDecimal
        // 
        this.rbDecimal.AutoSize = true;
        this.rbDecimal.Location = new System.Drawing.Point(151, 69);
        this.rbDecimal.Name = "rbDecimal";
        this.rbDecimal.Size = new System.Drawing.Size(63, 17);
        this.rbDecimal.TabIndex = 8;
        this.rbDecimal.TabStop = true;
        this.rbDecimal.Text = "Decimal";
        this.rbDecimal.UseVisualStyleBackColor = true;
        // 
        // rbUnsignedLong
        // 
        this.rbUnsignedLong.AutoSize = true;
        this.rbUnsignedLong.Location = new System.Drawing.Point(151, 46);
        this.rbUnsignedLong.Name = "rbUnsignedLong";
        this.rbUnsignedLong.Size = new System.Drawing.Size(93, 17);
        this.rbUnsignedLong.TabIndex = 7;
        this.rbUnsignedLong.TabStop = true;
        this.rbUnsignedLong.Text = "Unsigned long";
        this.rbUnsignedLong.UseVisualStyleBackColor = true;
        // 
        // rbLong
        // 
        this.rbLong.AutoSize = true;
        this.rbLong.Location = new System.Drawing.Point(151, 23);
        this.rbLong.Name = "rbLong";
        this.rbLong.Size = new System.Drawing.Size(49, 17);
        this.rbLong.TabIndex = 6;
        this.rbLong.TabStop = true;
        this.rbLong.Text = "Long";
        this.rbLong.UseVisualStyleBackColor = true;
        // 
        // rnUnsignedInt
        // 
        this.rnUnsignedInt.AutoSize = true;
        this.rnUnsignedInt.Location = new System.Drawing.Point(21, 161);
        this.rnUnsignedInt.Name = "rnUnsignedInt";
        this.rnUnsignedInt.Size = new System.Drawing.Size(84, 17);
        this.rnUnsignedInt.TabIndex = 5;
        this.rnUnsignedInt.TabStop = true;
        this.rnUnsignedInt.Text = "Unsinged int";
        this.rnUnsignedInt.UseVisualStyleBackColor = true;
        // 
        // rbUnsignedShort
        // 
        this.rbUnsignedShort.AutoSize = true;
        this.rbUnsignedShort.Location = new System.Drawing.Point(21, 115);
        this.rbUnsignedShort.Name = "rbUnsignedShort";
        this.rbUnsignedShort.Size = new System.Drawing.Size(110, 17);
        this.rbUnsignedShort.TabIndex = 4;
        this.rbUnsignedShort.TabStop = true;
        this.rbUnsignedShort.Text = "Unsigned short int";
        this.rbUnsignedShort.UseVisualStyleBackColor = true;
        // 
        // rbInt
        // 
        this.rbInt.AutoSize = true;
        this.rbInt.Location = new System.Drawing.Point(21, 138);
        this.rbInt.Name = "rbInt";
        this.rbInt.Size = new System.Drawing.Size(37, 17);
        this.rbInt.TabIndex = 3;
        this.rbInt.TabStop = true;
        this.rbInt.Text = "Int";
        this.rbInt.UseVisualStyleBackColor = true;
        // 
        // rbShort
        // 
        this.rbShort.AutoSize = true;
        this.rbShort.Location = new System.Drawing.Point(21, 92);
        this.rbShort.Name = "rbShort";
        this.rbShort.Size = new System.Drawing.Size(64, 17);
        this.rbShort.TabIndex = 2;
        this.rbShort.TabStop = true;
        this.rbShort.Text = "Short int";
        this.rbShort.UseVisualStyleBackColor = true;
        // 
        // rbChar
        // 
        this.rbChar.AutoSize = true;
        this.rbChar.Location = new System.Drawing.Point(20, 69);
        this.rbChar.Name = "rbChar";
        this.rbChar.Size = new System.Drawing.Size(47, 17);
        this.rbChar.TabIndex = 1;
        this.rbChar.TabStop = true;
        this.rbChar.Text = "Char";
        this.rbChar.UseVisualStyleBackColor = true;
        // 
        // rbByte
        // 
        this.rbByte.AutoSize = true;
        this.rbByte.Location = new System.Drawing.Point(20, 46);
        this.rbByte.Name = "rbByte";
        this.rbByte.Size = new System.Drawing.Size(46, 17);
        this.rbByte.TabIndex = 0;
        this.rbByte.TabStop = true;
        this.rbByte.Text = "Byte";
        this.rbByte.UseVisualStyleBackColor = true;
        // 
        // btnWrite
        // 
        this.btnWrite.Location = new System.Drawing.Point(552, 36);
        this.btnWrite.Name = "btnWrite";
        this.btnWrite.Size = new System.Drawing.Size(89, 23);
        this.btnWrite.TabIndex = 18;
        this.btnWrite.Text = "Add New Table";
        this.btnWrite.UseVisualStyleBackColor = true;
        this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
        // 
        // btnClose
        // 
        this.btnClose.Location = new System.Drawing.Point(552, 419);
        this.btnClose.Name = "btnClose";
        this.btnClose.Size = new System.Drawing.Size(89, 23);
        this.btnClose.TabIndex = 19;
        this.btnClose.Text = "&Close";
        this.btnClose.UseVisualStyleBackColor = true;
        this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
        // 
        // frmCreateDBTable
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(653, 462);
        this.Controls.Add(this.btnClose);
        this.Controls.Add(this.btnWrite);
        this.Controls.Add(this.groupBox2);
        this.Controls.Add(this.txtTableName);
        this.Controls.Add(this.label2);
        this.Name = "frmCreateDBTable";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "frmCreateDBTable";
        this.groupBox2.ResumeLayout(false);
        this.groupBox2.PerformLayout();
        this.groupBox1.ResumeLayout(false);
        this.groupBox1.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion


    public frmCreateDBTable(string dbn)
    {
        InitializeComponent();
        dbName = dbn;
        this.Text += " " + dbName;
        rbBool.Checked = true;
        txtMaxStringLength.Visible = false;
        lbStringLen.Visible = false;
    }

    private void btnAddButton_Click(object sender, EventArgs e)
    {
        bool flag;
        string name;
        string len="";
        string type="";

        name = txtFieldName.Text;           // Field name
        if (rbString.Checked == true)
        {
            txtMaxStringLength.Visible = true;
            lbStringLen.Visible = true;
        }
        else
        {
            txtMaxStringLength.Visible = false;
            lbStringLen.Visible = false;
        }

        if (rbBool.Checked == true)
        {
            len = sizeof(bool).ToString();
            type = "BIT";
        }
        else
        {
            if (rbByte.Checked == true)
            {
                len = "8";
                type = "TEXT(1)";
            }
            else
            {
                if (rbChar.Checked == true)
                {
                    len = "8";
                    type = "TEXT(1)";
                }
                else
                {
                    if (rbDateTime.Checked == true)
                    {
                        len = "16";
                        type = "DATETIME";
                    }
                    else
                    {
                        if (rbDecimal.Checked == true)
                        {
                            len = "16";
                            type = "CURRENCY";
                        }
                        else
                        {
                            if (rbDouble.Checked == true)
                            {
                                len = "8";
                                type = "DOUBLE";
                            }
                            else
                            {
                                if (rbFloat.Checked == true)
                                {
                                    len = "4";
                                    type = "DOUBLE";

                                }
                                else
                                {
                                    if (rbInt.Checked == true)
                                    {
                                        len = "4";
                                        type = "LONG";

                                    }
                                    else
                                    {
                                        if (rnUnsignedInt.Checked == true)
                                        {
                                            len = "4";
                                            type = "LONG";
                                        }
                                        else
                                        {
                                            if (rbLong.Checked == true)
                                            {
                                                len = "8";
                                                type = "LONG";
                                            }
                                            else
                                            {
                                                if (rbUnsignedLong.Checked == true)
                                                {
                                                    len = "8";
                                                    type = "LONG";
                                                }
                                                else
                                                {
                                                    if (rbShort.Checked == true)
                                                    {
                                                        len = "2";
                                                        type = "LONG";
                                                    }
                                                    else
                                                    {
                                                        if (rbUnsignedShort.Checked == true)
                                                        {
                                                            len = "2";
                                                            type = "LONG";

                                                        }
                                                        else
                                                        {
                                                            if (rbString.Checked == true)
                                                            {
                                                                flag = int.TryParse(txtMaxStringLength.Text, out length);
                                                                if (flag == false)
                                                                {
                                                                    MessageBox.Show("Length must be integer value between 1 and 255", "INput Error");
                                                                    txtMaxStringLength.Focus();
                                                                    return;
                                                                }
                                                                len = length.ToString();
                                                                type = "TEXT";
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        ListViewItem item = new ListViewItem(name);
        item.SubItems.Add(len);
        item.SubItems.Add(type);
        lstFieldsToAdd.Items.Add(item);

        txtFieldName.Text = "";
        rbBool.Checked = true;
        txtMaxStringLength.Visible = false;
        lbStringLen.Visible = false;
    }

    private void rbString_MouseClick(object sender, MouseEventArgs e)
    {
        if (rbString.Checked == true)
        {
            txtMaxStringLength.Visible = true;
            lbStringLen.Visible = true;
        }
        else
        {
            txtMaxStringLength.Visible = false;
            lbStringLen.Visible = false;
        }

    }

    private void btnClose_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void lstFieldsToAdd_Click(object sender, EventArgs e)
    {
        ListView.SelectedIndexCollection indexes = 
			lstFieldsToAdd.SelectedIndices;
		
		foreach ( int index in indexes )
		{
            lstFieldsToAdd.Items[index].Remove();
		}
    }

    private void lstFieldsToAdd_DoubleClick(object sender, EventArgs e)
    {
        ListView.SelectedIndexCollection indexes =
            lstFieldsToAdd.SelectedIndices;

        foreach (int index in indexes)
        {
            lstFieldsToAdd.Items[index].Remove();
        }

    }

    private void btnWrite_Click(object sender, EventArgs e)
    {
        int i,j;
        int flag;
        string sqlCommand;
        string buff;
        string temp;

        try
        {
          clsDB myDB = new clsDB(dbName);

          sqlCommand = "CREATE TABLE " +
               txtTableName.Text + " (ID COUNTER, ";     // Automatically add

          for (i = 0; i < lstFieldsToAdd.Items.Count; i++)
          {
            temp = "";
            for (j = 0; j < lstFieldsToAdd.Items[i].SubItems.Count; j++)
            {
              buff = lstFieldsToAdd.Items[i].SubItems[j].Text;
              switch (j)
              {
                case 0:
                  sqlCommand += buff + " ";
                  break;
                case 1:
                  temp = buff;
                  break;
                case 2:
                  if (buff.Equals("TEXT") == true)
                  {
                    sqlCommand += "TEXT(" + temp + "), ";
                  }
                  else
                  {
                    sqlCommand += buff + ", ";
                  }
                  break;
              }
            }
          }
          i = sqlCommand.LastIndexOf(",");
          sqlCommand = sqlCommand.Substring(0, sqlCommand.Length - 2);
          sqlCommand += ")";
          flag = myDB.ProcessCommand(sqlCommand);
          if (flag == 1)    // Table created OK, so...
          {
            // ...create the primary key
            sqlCommand = "CREATE INDEX idxID ON " + txtTableName.Text +
                "(ID) WITH PRIMARY";
            flag = myDB.ProcessCommand(sqlCommand);
            if (flag == 1)
            {
              MessageBox.Show("Table created successfully.");
            }
          }
          else
          {
            MessageBox.Show("Failed to create table.", "Process Error");
          }

        }catch (Exception ex)
        {
          MessageBox.Show("Error: " + ex.Message);
        }
    }
  }

 
 


