using System;
using System.Windows.Forms;

public class frmMain : Form
{
    const int MALE = 1;
    const int FEMALE = 0;
    const int MUSHROOMS = 0;
    const int OLIVES = 1;
    const int SAUSAGE = 2;
    private GroupBox groupBox1;
    private RadioButton rbFemale;
    private RadioButton rbMale;
    private GroupBox groupBox2;
    private RadioButton rbJunior;
    private RadioButton rbSenior;
    private GroupBox groupBox3;
    private CheckBox ckbXCheese;
    private CheckBox ckbOlives;
    private CheckBox ckbSausage;
    private CheckBox ckbMushrooms;
    const int EXTRACHEESE = 3;
    #region Windows code
    private void InitializeComponent()
    {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbFemale = new System.Windows.Forms.RadioButton();
            this.rbMale = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbJunior = new System.Windows.Forms.RadioButton();
            this.rbSenior = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ckbMushrooms = new System.Windows.Forms.CheckBox();
            this.ckbSausage = new System.Windows.Forms.CheckBox();
            this.ckbOlives = new System.Windows.Forms.CheckBox();
            this.ckbXCheese = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbFemale);
            this.groupBox1.Controls.Add(this.rbMale);
            this.groupBox1.Location = new System.Drawing.Point(22, 22);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 100);
            this.groupBox1.TabIndex = 32;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Gender:";
            // 
            // rbFemale
            // 
            this.rbFemale.AutoSize = true;
            this.rbFemale.Location = new System.Drawing.Point(109, 42);
            this.rbFemale.Name = "rbFemale";
            this.rbFemale.Size = new System.Drawing.Size(59, 17);
            this.rbFemale.TabIndex = 31;
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
            this.rbMale.TabIndex = 30;
            this.rbMale.TabStop = true;
            this.rbMale.Text = "&Male";
            this.rbMale.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbJunior);
            this.groupBox2.Controls.Add(this.rbSenior);
            this.groupBox2.Location = new System.Drawing.Point(22, 147);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 100);
            this.groupBox2.TabIndex = 33;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Membership:";
            // 
            // rbJunior
            // 
            this.rbJunior.AutoSize = true;
            this.rbJunior.Location = new System.Drawing.Point(112, 42);
            this.rbJunior.Name = "rbJunior";
            this.rbJunior.Size = new System.Drawing.Size(53, 17);
            this.rbJunior.TabIndex = 33;
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
            this.rbSenior.TabIndex = 32;
            this.rbSenior.TabStop = true;
            this.rbSenior.Text = "&Senior";
            this.rbSenior.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ckbXCheese);
            this.groupBox3.Controls.Add(this.ckbOlives);
            this.groupBox3.Controls.Add(this.ckbSausage);
            this.groupBox3.Controls.Add(this.ckbMushrooms);
            this.groupBox3.Location = new System.Drawing.Point(22, 264);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 85);
            this.groupBox3.TabIndex = 34;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Toppings:";
            // 
            // ckbMushrooms
            // 
            this.ckbMushrooms.AutoSize = true;
            this.ckbMushrooms.Location = new System.Drawing.Point(10, 19);
            this.ckbMushrooms.Name = "ckbMushrooms";
            this.ckbMushrooms.Size = new System.Drawing.Size(80, 17);
            this.ckbMushrooms.TabIndex = 0;
            this.ckbMushrooms.Text = "Mushrooms";
            this.ckbMushrooms.UseVisualStyleBackColor = true;
            // 
            // ckbSausage
            // 
            this.ckbSausage.AutoSize = true;
            this.ckbSausage.Location = new System.Drawing.Point(10, 51);
            this.ckbSausage.Name = "ckbSausage";
            this.ckbSausage.Size = new System.Drawing.Size(68, 17);
            this.ckbSausage.TabIndex = 1;
            this.ckbSausage.Text = "Sausage";
            this.ckbSausage.UseVisualStyleBackColor = true;
            // 
            // ckbOlives
            // 
            this.ckbOlives.AutoSize = true;
            this.ckbOlives.Location = new System.Drawing.Point(109, 19);
            this.ckbOlives.Name = "ckbOlives";
            this.ckbOlives.Size = new System.Drawing.Size(55, 17);
            this.ckbOlives.TabIndex = 2;
            this.ckbOlives.Text = "Olives";
            this.ckbOlives.UseVisualStyleBackColor = true;
            // 
            // ckbXCheese
            // 
            this.ckbXCheese.AutoSize = true;
            this.ckbXCheese.Location = new System.Drawing.Point(109, 51);
            this.ckbXCheese.Name = "ckbXCheese";
            this.ckbXCheese.Size = new System.Drawing.Size(89, 17);
            this.ckbXCheese.TabIndex = 3;
            this.ckbXCheese.Text = "Extra Cheese";
            this.ckbXCheese.UseVisualStyleBackColor = true;
            // 
            // frmMain
            // 
            this.ClientSize = new System.Drawing.Size(442, 414);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Radio Button Objects";
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

    }
    //[STAThread]
    public static void Main()
    {
        frmMain main = new frmMain();
        Application.Run(main);
    }


}
