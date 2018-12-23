using System;
using System.Windows.Forms;

public class frmMain : Form
{
    const int MAXITERATIONS = 200000;    // Limit on loop passes

    private Button btnClose;
    private Label lblAnswer;
    private Label label1;
    private TextBox txtMax;
    private Button btnStart;
    #region Windows code
    private void InitializeComponent()
    {
        this.btnStart = new System.Windows.Forms.Button();
        this.btnClose = new System.Windows.Forms.Button();
        this.lblAnswer = new System.Windows.Forms.Label();
        this.label1 = new System.Windows.Forms.Label();
        this.txtMax = new System.Windows.Forms.TextBox();
        this.SuspendLayout();
        // 
        // btnStart
        // 
        this.btnStart.Location = new System.Drawing.Point(26, 77);
        this.btnStart.Name = "btnStart";
        this.btnStart.Size = new System.Drawing.Size(75, 23);
        this.btnStart.TabIndex = 0;
        this.btnStart.Text = "&Start";
        this.btnStart.UseVisualStyleBackColor = true;
        this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
        // 
        // btnClose
        // 
        this.btnClose.Location = new System.Drawing.Point(182, 77);
        this.btnClose.Name = "btnClose";
        this.btnClose.Size = new System.Drawing.Size(75, 23);
        this.btnClose.TabIndex = 1;
        this.btnClose.Text = "&Close";
        this.btnClose.UseVisualStyleBackColor = true;
        this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
        // 
        // lblAnswer
        // 
        this.lblAnswer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lblAnswer.Location = new System.Drawing.Point(23, 139);
        this.lblAnswer.Name = "lblAnswer";
        this.lblAnswer.Size = new System.Drawing.Size(234, 20);
        this.lblAnswer.TabIndex = 2;
        this.lblAnswer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // label1
        // 
        this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.label1.Location = new System.Drawing.Point(12, 9);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(183, 20);
        this.label1.TabIndex = 3;
        this.label1.Text = "Generate numbers between 0 and:";
        this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // txtMax
        // 
        this.txtMax.Location = new System.Drawing.Point(201, 9);
        this.txtMax.Name = "txtMax";
        this.txtMax.Size = new System.Drawing.Size(56, 20);
        this.txtMax.TabIndex = 4;
        this.txtMax.Text = "1000";
        // 
        // frmMain
        // 
        this.ClientSize = new System.Drawing.Size(276, 183);
        this.Controls.Add(this.txtMax);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.lblAnswer);
        this.Controls.Add(this.btnClose);
        this.Controls.Add(this.btnStart);
        this.Name = "frmMain";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Duplicate Random Numbers";
        this.ResumeLayout(false);
        this.PerformLayout();

    }
    #endregion

    public frmMain()
    {
        //====================== Program Initialize Step ==============
        InitializeComponent();
    }

    public static void Main()
    {
        frmMain main = new frmMain();
        Application.Run(main);
    }

    private void btnStart_Click(object sender, EventArgs e)
    {
        bool flag;
        int counter = 0;    // Pass counter
        int max;        // Max value for random number
        int last;       
        int current;
        Random randomNumber = new Random();

        //=================== Program Input Step =====================
        flag = int.TryParse(txtMax.Text, out max);
        if (flag == false)
        {
            MessageBox.Show("Digit characters only.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            txtMax.Focus();
            return;
        }

        //================== Program Process Step ====================
        counter = 0;
        last = (int)randomNumber.Next(max);
        do
        {
            current = randomNumber.Next(max);
            if (last == current)
            {
                break;
            }
            last = current;
            counter++;
        } while (counter < MAXITERATIONS);


        //================== Program Output Step =====================
        if (counter < MAXITERATIONS)
        {
            lblAnswer.Text = "It took " + counter.ToString() + " passes for a back-to-back match.";
        } else 
        {
            lblAnswer.Text = "No back-to-back match";
        }
    }

    //======================= Program Termination Step ================
    private void btnClose_Click(object sender, EventArgs e)
    {
        Close();
    }
}