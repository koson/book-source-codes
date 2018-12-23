using System;
using System.Windows.Forms;

public class frmMain : Form
{
    const int MALE = 1;
    const int FEMALE = 0;
    const int MUSHROOMS = 0;
    const int OLIVES = 1;
    const int SAUSAGE = 2;
    const int EXTRACHEESE = 3;
  
    private GroupBox groupBox1;
    private RadioButton rbFemale;
    private GroupBox groupBox2;
    private RadioButton rbJunior;
    private RadioButton rbSenior;
    private Button btnChoice;
    private GroupBox groupBox3;
    private CheckBox ckbExtraCheese;
    private CheckBox ckbSausage;
    private CheckBox ckbOlives;
    private CheckBox ckbMushroom;
    private ComboBox cmbSize;
    private Label label1;
    private DateTimePicker dtpDate;
    private DateTimePicker dtpTime;
    private RadioButton rbMale;
    #region Windows code
    private void InitializeComponent()
    {
        this.groupBox1 = new System.Windows.Forms.GroupBox();
        this.rbFemale = new System.Windows.Forms.RadioButton();
        this.rbMale = new System.Windows.Forms.RadioButton();
        this.groupBox2 = new System.Windows.Forms.GroupBox();
        this.rbJunior = new System.Windows.Forms.RadioButton();
        this.rbSenior = new System.Windows.Forms.RadioButton();
        this.btnChoice = new System.Windows.Forms.Button();
        this.groupBox3 = new System.Windows.Forms.GroupBox();
        this.ckbExtraCheese = new System.Windows.Forms.CheckBox();
        this.ckbSausage = new System.Windows.Forms.CheckBox();
        this.ckbOlives = new System.Windows.Forms.CheckBox();
        this.ckbMushroom = new System.Windows.Forms.CheckBox();
        this.cmbSize = new System.Windows.Forms.ComboBox();
        this.label1 = new System.Windows.Forms.Label();
        this.dtpDate = new System.Windows.Forms.DateTimePicker();
        this.dtpTime = new System.Windows.Forms.DateTimePicker();
        this.groupBox1.SuspendLayout();
        this.groupBox2.SuspendLayout();
        this.groupBox3.SuspendLayout();
        this.SuspendLayout();
        // 
        // groupBox1
        // 
        this.groupBox1.Controls.Add(this.rbFemale);
        this.groupBox1.Controls.Add(this.rbMale);
        this.groupBox1.Location = new System.Drawing.Point(12, 12);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new System.Drawing.Size(200, 100);
        this.groupBox1.TabIndex = 11;
        this.groupBox1.TabStop = false;
        this.groupBox1.Text = "Gender:";
        // 
        // rbFemale
        // 
        this.rbFemale.AutoSize = true;
        this.rbFemale.Location = new System.Drawing.Point(109, 42);
        this.rbFemale.Name = "rbFemale";
        this.rbFemale.Size = new System.Drawing.Size(59, 17);
        this.rbFemale.TabIndex = 12;
        this.rbFemale.TabStop = true;
        this.rbFemale.Text = "&Female";
        this.rbFemale.UseVisualStyleBackColor = true;
        // 
        // rbMale
        // 
        this.rbMale.AutoSize = true;
        this.rbMale.Location = new System.Drawing.Point(32, 42);
        this.rbMale.Name = "rbMale";
        this.rbMale.Size = new System.Drawing.Size(48, 17);
        this.rbMale.TabIndex = 11;
        this.rbMale.TabStop = true;
        this.rbMale.Text = "&Male";
        this.rbMale.UseVisualStyleBackColor = true;
        // 
        // groupBox2
        // 
        this.groupBox2.Controls.Add(this.rbJunior);
        this.groupBox2.Controls.Add(this.rbSenior);
        this.groupBox2.Location = new System.Drawing.Point(12, 129);
        this.groupBox2.Name = "groupBox2";
        this.groupBox2.Size = new System.Drawing.Size(200, 100);
        this.groupBox2.TabIndex = 12;
        this.groupBox2.TabStop = false;
        this.groupBox2.Text = "Membership group:";
        // 
        // rbJunior
        // 
        this.rbJunior.AutoSize = true;
        this.rbJunior.Location = new System.Drawing.Point(112, 42);
        this.rbJunior.Name = "rbJunior";
        this.rbJunior.Size = new System.Drawing.Size(53, 17);
        this.rbJunior.TabIndex = 9;
        this.rbJunior.TabStop = true;
        this.rbJunior.Text = "&Junior";
        this.rbJunior.UseVisualStyleBackColor = true;
        // 
        // rbSenior
        // 
        this.rbSenior.AutoSize = true;
        this.rbSenior.Location = new System.Drawing.Point(35, 42);
        this.rbSenior.Name = "rbSenior";
        this.rbSenior.Size = new System.Drawing.Size(55, 17);
        this.rbSenior.TabIndex = 8;
        this.rbSenior.TabStop = true;
        this.rbSenior.Text = "&Senior";
        this.rbSenior.UseVisualStyleBackColor = true;
        // 
        // btnChoice
        // 
        this.btnChoice.Location = new System.Drawing.Point(235, 12);
        this.btnChoice.Name = "btnChoice";
        this.btnChoice.Size = new System.Drawing.Size(75, 23);
        this.btnChoice.TabIndex = 14;
        this.btnChoice.Text = "&Choices";
        this.btnChoice.UseVisualStyleBackColor = true;
        this.btnChoice.Click += new System.EventHandler(this.btnChoice_Click);
        // 
        // groupBox3
        // 
        this.groupBox3.Controls.Add(this.ckbExtraCheese);
        this.groupBox3.Controls.Add(this.ckbSausage);
        this.groupBox3.Controls.Add(this.ckbOlives);
        this.groupBox3.Controls.Add(this.ckbMushroom);
        this.groupBox3.Location = new System.Drawing.Point(12, 251);
        this.groupBox3.Name = "groupBox3";
        this.groupBox3.Size = new System.Drawing.Size(200, 90);
        this.groupBox3.TabIndex = 15;
        this.groupBox3.TabStop = false;
        this.groupBox3.Text = "Toppings:";
        // 
        // ckbExtraCheese
        // 
        this.ckbExtraCheese.AutoSize = true;
        this.ckbExtraCheese.Location = new System.Drawing.Point(105, 61);
        this.ckbExtraCheese.Name = "ckbExtraCheese";
        this.ckbExtraCheese.Size = new System.Drawing.Size(89, 17);
        this.ckbExtraCheese.TabIndex = 3;
        this.ckbExtraCheese.Text = "Extra Cheese";
        this.ckbExtraCheese.UseVisualStyleBackColor = true;
        // 
        // ckbSausage
        // 
        this.ckbSausage.AutoSize = true;
        this.ckbSausage.Location = new System.Drawing.Point(10, 61);
        this.ckbSausage.Name = "ckbSausage";
        this.ckbSausage.Size = new System.Drawing.Size(68, 17);
        this.ckbSausage.TabIndex = 2;
        this.ckbSausage.Text = "Sausage";
        this.ckbSausage.UseVisualStyleBackColor = true;
        // 
        // ckbOlives
        // 
        this.ckbOlives.AutoSize = true;
        this.ckbOlives.Location = new System.Drawing.Point(105, 29);
        this.ckbOlives.Name = "ckbOlives";
        this.ckbOlives.Size = new System.Drawing.Size(55, 17);
        this.ckbOlives.TabIndex = 1;
        this.ckbOlives.Text = "Olives";
        this.ckbOlives.UseVisualStyleBackColor = true;
        // 
        // ckbMushroom
        // 
        this.ckbMushroom.AutoSize = true;
        this.ckbMushroom.Location = new System.Drawing.Point(10, 29);
        this.ckbMushroom.Name = "ckbMushroom";
        this.ckbMushroom.Size = new System.Drawing.Size(80, 17);
        this.ckbMushroom.TabIndex = 0;
        this.ckbMushroom.Text = "Mushrooms";
        this.ckbMushroom.UseVisualStyleBackColor = true;
        // 
        // cmbSize
        // 
        this.cmbSize.FormattingEnabled = true;
        this.cmbSize.Items.AddRange(new object[] {
            "Small",
            "Medium",
            "large"});
        this.cmbSize.Location = new System.Drawing.Point(89, 358);
        this.cmbSize.Name = "cmbSize";
        this.cmbSize.Size = new System.Drawing.Size(125, 21);
        this.cmbSize.TabIndex = 16;
        // 
        // label1
        // 
        this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.label1.Location = new System.Drawing.Point(12, 359);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(71, 20);
        this.label1.TabIndex = 17;
        this.label1.Text = "Size:";
        this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // dtpDate
        // 
        this.dtpDate.Location = new System.Drawing.Point(230, 54);
        this.dtpDate.Name = "dtpDate";
        this.dtpDate.Size = new System.Drawing.Size(200, 20);
        this.dtpDate.TabIndex = 18;
        // 
        // dtpTime
        // 
        this.dtpTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
        this.dtpTime.Location = new System.Drawing.Point(230, 129);
        this.dtpTime.Name = "dtpTime";
        this.dtpTime.ShowUpDown = true;
        this.dtpTime.Size = new System.Drawing.Size(200, 20);
        this.dtpTime.TabIndex = 19;
        // 
        // frmMain
        // 
        this.ClientSize = new System.Drawing.Size(442, 414);
        this.Controls.Add(this.dtpTime);
        this.Controls.Add(this.dtpDate);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.cmbSize);
        this.Controls.Add(this.groupBox3);
        this.Controls.Add(this.btnChoice);
        this.Controls.Add(this.groupBox2);
        this.Controls.Add(this.groupBox1);
        this.Name = "frmMain";
        this.Text = "Common Input Objects";
        this.groupBox1.ResumeLayout(false);
        this.groupBox1.PerformLayout();
        this.groupBox2.ResumeLayout(false);
        this.groupBox2.PerformLayout();
        this.groupBox3.ResumeLayout(false);
        this.groupBox3.PerformLayout();
        this.ResumeLayout(false);

    }
    #endregion

    public frmMain()
    {
        InitializeComponent();
        rbFemale.Checked = true;
        rbSenior.Checked = true;
        cmbSize.SelectedIndex = 0;
    }
    //[STAThread]
    public static void Main()
    {
        frmMain main = new frmMain();
        Application.Run(main);
    }

    private void btnChoice_Click(object sender, EventArgs e)
    {
        int choice;
        int[] toppings = new int[4];

        if (rbMale.Checked == true)
        {
            choice = MALE;
        }
        else
        {
            choice = FEMALE;
        }

        Array.Clear(toppings, 0, toppings.Length);

        if (ckbMushroom.Checked == true)
        {
            toppings[MUSHROOMS] = 1;
        }
        if (ckbOlives.Checked == true)
        {
            toppings[OLIVES] = 1;
        }
        if (ckbSausage.Checked == true)
        {
            toppings[SAUSAGE] = 1;
        }
        if (ckbExtraCheese.Checked == true)
        {
            toppings[EXTRACHEESE] = 1;
        }

        string buff = cmbSize.Text;
        int i = cmbSize.SelectedIndex;
        string date = dtpDate.Value.ToShortDateString();
        string time = dtpTime.Value.ToShortTimeString();


    }




}