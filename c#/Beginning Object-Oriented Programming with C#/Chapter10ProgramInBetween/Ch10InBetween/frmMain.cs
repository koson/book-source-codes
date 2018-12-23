using System;
using System.Windows.Forms;

public class frmMain : Form
{
    const int TIE = 0;
    const int PLAYERWINS = 1;
    const int DEALERWINS = 2;

    int betResult;
    int wager;
    int balance;
    int position;

    clsInBetweenRules myRules = new clsInBetweenRules();
    string[] cards = new string[3];

    private Button btnDeal;
    private Button btnClose;
    private Label label1;
    private TextBox txtWager;
    private TextBox txtBalance;
    private Label label2;
    private GroupBox groupBox1;
    private Label lblLow;
    private Label label4;
    private Label label3;
    private TextBox txtHi;
    private TextBox txtLow;
    private Label lblMore;
    private Label lblLess;
    private Label lblHi;
    private Label lblMiddle;
    private Button btnBet;
    private Label lblOutcome;
    private Button btnReset;
    #region Windows code
    private void InitializeComponent()
    {
        this.btnReset = new System.Windows.Forms.Button();
        this.btnDeal = new System.Windows.Forms.Button();
        this.btnClose = new System.Windows.Forms.Button();
        this.label1 = new System.Windows.Forms.Label();
        this.txtWager = new System.Windows.Forms.TextBox();
        this.txtBalance = new System.Windows.Forms.TextBox();
        this.label2 = new System.Windows.Forms.Label();
        this.groupBox1 = new System.Windows.Forms.GroupBox();
        this.lblMore = new System.Windows.Forms.Label();
        this.lblLess = new System.Windows.Forms.Label();
        this.lblHi = new System.Windows.Forms.Label();
        this.lblMiddle = new System.Windows.Forms.Label();
        this.lblLow = new System.Windows.Forms.Label();
        this.label4 = new System.Windows.Forms.Label();
        this.label3 = new System.Windows.Forms.Label();
        this.txtHi = new System.Windows.Forms.TextBox();
        this.txtLow = new System.Windows.Forms.TextBox();
        this.btnBet = new System.Windows.Forms.Button();
        this.lblOutcome = new System.Windows.Forms.Label();
        this.groupBox1.SuspendLayout();
        this.SuspendLayout();
        // 
        // btnReset
        // 
        this.btnReset.Location = new System.Drawing.Point(337, 6);
        this.btnReset.Name = "btnReset";
        this.btnReset.Size = new System.Drawing.Size(75, 23);
        this.btnReset.TabIndex = 0;
        this.btnReset.Text = "&Reset";
        this.btnReset.UseVisualStyleBackColor = true;
        this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
        // 
        // btnDeal
        // 
        this.btnDeal.Location = new System.Drawing.Point(12, 48);
        this.btnDeal.Name = "btnDeal";
        this.btnDeal.Size = new System.Drawing.Size(75, 23);
        this.btnDeal.TabIndex = 1;
        this.btnDeal.Text = "&Deal";
        this.btnDeal.UseVisualStyleBackColor = true;
        this.btnDeal.Click += new System.EventHandler(this.btnDeal_Click);
        // 
        // btnClose
        // 
        this.btnClose.Location = new System.Drawing.Point(337, 186);
        this.btnClose.Name = "btnClose";
        this.btnClose.Size = new System.Drawing.Size(75, 23);
        this.btnClose.TabIndex = 2;
        this.btnClose.Text = "&Close";
        this.btnClose.UseVisualStyleBackColor = true;
        this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
        // 
        // label1
        // 
        this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.label1.Location = new System.Drawing.Point(160, 9);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(69, 20);
        this.label1.TabIndex = 5;
        this.label1.Text = "Wager: $";
        this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // txtWager
        // 
        this.txtWager.Location = new System.Drawing.Point(230, 9);
        this.txtWager.Name = "txtWager";
        this.txtWager.Size = new System.Drawing.Size(85, 20);
        this.txtWager.TabIndex = 6;
        // 
        // txtBalance
        // 
        this.txtBalance.Location = new System.Drawing.Point(82, 9);
        this.txtBalance.Name = "txtBalance";
        this.txtBalance.ReadOnly = true;
        this.txtBalance.Size = new System.Drawing.Size(72, 20);
        this.txtBalance.TabIndex = 8;
        // 
        // label2
        // 
        this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.label2.Location = new System.Drawing.Point(12, 9);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(69, 20);
        this.label2.TabIndex = 7;
        this.label2.Text = "Balance: $";
        this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // groupBox1
        // 
        this.groupBox1.Controls.Add(this.lblMore);
        this.groupBox1.Controls.Add(this.lblLess);
        this.groupBox1.Controls.Add(this.lblHi);
        this.groupBox1.Controls.Add(this.lblMiddle);
        this.groupBox1.Controls.Add(this.lblLow);
        this.groupBox1.Controls.Add(this.label4);
        this.groupBox1.Controls.Add(this.label3);
        this.groupBox1.Controls.Add(this.txtHi);
        this.groupBox1.Controls.Add(this.txtLow);
        this.groupBox1.Location = new System.Drawing.Point(12, 97);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new System.Drawing.Size(303, 112);
        this.groupBox1.TabIndex = 9;
        this.groupBox1.TabStop = false;
        this.groupBox1.Text = "Hand:";
        // 
        // lblMore
        // 
        this.lblMore.Location = new System.Drawing.Point(250, 16);
        this.lblMore.Name = "lblMore";
        this.lblMore.Size = new System.Drawing.Size(35, 20);
        this.lblMore.TabIndex = 12;
        this.lblMore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // lblLess
        // 
        this.lblLess.Location = new System.Drawing.Point(16, 16);
        this.lblLess.Name = "lblLess";
        this.lblLess.Size = new System.Drawing.Size(35, 20);
        this.lblLess.TabIndex = 11;
        this.lblLess.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // lblHi
        // 
        this.lblHi.Location = new System.Drawing.Point(192, 16);
        this.lblHi.Name = "lblHi";
        this.lblHi.Size = new System.Drawing.Size(35, 20);
        this.lblHi.TabIndex = 10;
        this.lblHi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // lblMiddle
        // 
        this.lblMiddle.Location = new System.Drawing.Point(125, 16);
        this.lblMiddle.Name = "lblMiddle";
        this.lblMiddle.Size = new System.Drawing.Size(35, 20);
        this.lblMiddle.TabIndex = 9;
        this.lblMiddle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // lblLow
        // 
        this.lblLow.Location = new System.Drawing.Point(67, 16);
        this.lblLow.Name = "lblLow";
        this.lblLow.Size = new System.Drawing.Size(35, 20);
        this.lblLow.TabIndex = 8;
        this.lblLow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // label4
        // 
        this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.label4.Location = new System.Drawing.Point(195, 73);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(35, 20);
        this.label4.TabIndex = 7;
        this.label4.Text = "High";
        this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // label3
        // 
        this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.label3.Location = new System.Drawing.Point(70, 73);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(35, 20);
        this.label3.TabIndex = 6;
        this.label3.Text = "Low";
        this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // txtHi
        // 
        this.txtHi.Location = new System.Drawing.Point(195, 50);
        this.txtHi.Name = "txtHi";
        this.txtHi.Size = new System.Drawing.Size(35, 20);
        this.txtHi.TabIndex = 5;
        this.txtHi.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
        // 
        // txtLow
        // 
        this.txtLow.Location = new System.Drawing.Point(70, 50);
        this.txtLow.Name = "txtLow";
        this.txtLow.Size = new System.Drawing.Size(35, 20);
        this.txtLow.TabIndex = 4;
        this.txtLow.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
        // 
        // btnBet
        // 
        this.btnBet.Location = new System.Drawing.Point(108, 48);
        this.btnBet.Name = "btnBet";
        this.btnBet.Size = new System.Drawing.Size(75, 23);
        this.btnBet.TabIndex = 10;
        this.btnBet.Text = "&Bet";
        this.btnBet.UseVisualStyleBackColor = true;
        this.btnBet.Click += new System.EventHandler(this.btnBet_Click);
        // 
        // lblOutcome
        // 
        this.lblOutcome.Location = new System.Drawing.Point(12, 74);
        this.lblOutcome.Name = "lblOutcome";
        this.lblOutcome.Size = new System.Drawing.Size(303, 20);
        this.lblOutcome.TabIndex = 11;
        this.lblOutcome.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // frmMain
        // 
        this.ClientSize = new System.Drawing.Size(424, 223);
        this.Controls.Add(this.lblOutcome);
        this.Controls.Add(this.btnBet);
        this.Controls.Add(this.groupBox1);
        this.Controls.Add(this.txtBalance);
        this.Controls.Add(this.label2);
        this.Controls.Add(this.txtWager);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.btnClose);
        this.Controls.Add(this.btnDeal);
        this.Controls.Add(this.btnReset);
        this.Name = "frmMain";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "In Between";
        this.groupBox1.ResumeLayout(false);
        this.groupBox1.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();

    }
    #endregion

    public frmMain()
    {
        bool flag;
        InitializeComponent();

        txtBalance.Text = myRules.Balance.ToString();   // Grub stake
        txtWager.Text = myRules.Wager.ToString();       // Default bet $10
        flag = int.TryParse(txtBalance.Text, out balance);
        flag = int.TryParse(txtWager.Text, out wager);

        myRules.Shuffle();                              // Shuffle deck
    }

    public static void Main()
    {
        frmMain main = new frmMain();
        Application.Run(main);
    }

    private void btnDeal_Click(object sender, EventArgs e)
    {
        int retval;

        ClearRanges();              // Clear old data
        lblOutcome.Text = "";

        retval = myRules.Balance;   // Money left to bet??
        if (retval == 0)
        {
            MessageBox.Show("You're broke. Game over.");
            return;
        }

        retval = myRules.getCardsLeft();    // Enough cards left??
        if (retval < 3)
        {
            lblOutcome.Text = "Deck was shuffled...";
            myRules.Shuffle();
        }
        myRules.DealHand(cards, ref betResult, ref position);
        ShowHiLow();
    }

    private void btnBet_Click(object sender, EventArgs e)
    {
        bool flag = int.TryParse(txtWager.Text, out wager);
        if (flag == false)
        {
            MessageBox.Show("Dollar bets only. Re-enter.", "Input Error");
            txtWager.Focus();
            return;
        }
    
        switch (betResult)
        {
            case TIE:         // This is a tie
                lblOutcome.Text = "Tie. Dealer wins.";
                myRules.Balance -= wager;
                break;

            case PLAYERWINS:
                lblOutcome.Text = "You win!";
                myRules.Balance +=  wager;
                break;

            case DEALERWINS:
                lblOutcome.Text = "Sorry, you lose.";
                myRules.Balance -= wager;
                break;
        }
        txtBalance.Text = myRules.Balance.ToString();
        switch (position)
        {
            case 1:
                lblLess.Text = cards[2];
                break;
            case 2:
                lblLow.Text = cards[2];
                break;
            case 3:
                lblMiddle.Text = cards[2];
                break;
            case 4:
                lblHi.Text = cards[2];
                break;
            case 5:
                lblMore.Text = cards[2];
                break;
            default:
                MessageBox.Show("Results error.", "Processing Error");
                break;
        }
    }

    private void ShowHiLow()
    {
        txtLow.Text = cards[0];
        txtHi.Text = cards[1];
    }

    private void ClearRanges()
    {
        lblLess.Text ="";
        lblLow.Text = "";
        lblMiddle.Text = "";
        lblHi.Text = "";
        lblMore.Text = "";
    }

    private void btnReset_Click(object sender, EventArgs e)
    {
        myRules.Balance = 100;
        txtBalance.Text = "100";
        txtWager.Text = "10";
        ClearRanges();
    }
    private void btnClose_Click(object sender, EventArgs e)
    {
        Close();
    }
}