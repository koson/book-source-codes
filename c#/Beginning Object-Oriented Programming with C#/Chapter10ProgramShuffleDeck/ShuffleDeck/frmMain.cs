#define DEBUG
#undef DEBUG

using System;
using System.Windows.Forms;

public class frmMain : Form
{
   
    private Button btnShuffle;
    private Button btnClose;
    private Label lblPassCounter;
    private Button btnClear;
    private ListBox lstDeck;
    #region Windows code
    private void InitializeComponent()
    {
        this.lstDeck = new System.Windows.Forms.ListBox();
        this.btnShuffle = new System.Windows.Forms.Button();
        this.btnClose = new System.Windows.Forms.Button();
        this.lblPassCounter = new System.Windows.Forms.Label();
        this.btnClear = new System.Windows.Forms.Button();
        this.SuspendLayout();
        // 
        // lstDeck
        // 
        this.lstDeck.FormattingEnabled = true;
        this.lstDeck.Location = new System.Drawing.Point(12, 58);
        this.lstDeck.Name = "lstDeck";
        this.lstDeck.Size = new System.Drawing.Size(325, 160);
        this.lstDeck.TabIndex = 0;
        // 
        // btnShuffle
        // 
        this.btnShuffle.Location = new System.Drawing.Point(12, 12);
        this.btnShuffle.Name = "btnShuffle";
        this.btnShuffle.Size = new System.Drawing.Size(75, 23);
        this.btnShuffle.TabIndex = 1;
        this.btnShuffle.Text = "&Shuffle";
        this.btnShuffle.UseVisualStyleBackColor = true;
        this.btnShuffle.Click += new System.EventHandler(this.btnShuffle_Click);
        // 
        // btnClose
        // 
        this.btnClose.Location = new System.Drawing.Point(262, 12);
        this.btnClose.Name = "btnClose";
        this.btnClose.Size = new System.Drawing.Size(75, 23);
        this.btnClose.TabIndex = 2;
        this.btnClose.Text = "&Close";
        this.btnClose.UseVisualStyleBackColor = true;
        this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
        // 
        // lblPassCounter
        // 
        this.lblPassCounter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lblPassCounter.Location = new System.Drawing.Point(12, 225);
        this.lblPassCounter.Name = "lblPassCounter";
        this.lblPassCounter.Size = new System.Drawing.Size(325, 20);
        this.lblPassCounter.TabIndex = 3;
        this.lblPassCounter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // btnClear
        // 
        this.btnClear.Location = new System.Drawing.Point(102, 12);
        this.btnClear.Name = "btnClear";
        this.btnClear.Size = new System.Drawing.Size(75, 23);
        this.btnClear.TabIndex = 4;
        this.btnClear.Text = "C&lear";
        this.btnClear.UseVisualStyleBackColor = true;
        this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
        // 
        // frmMain
        // 
        this.ClientSize = new System.Drawing.Size(349, 254);
        this.Controls.Add(this.btnClear);
        this.Controls.Add(this.lblPassCounter);
        this.Controls.Add(this.btnClose);
        this.Controls.Add(this.btnShuffle);
        this.Controls.Add(this.lstDeck);
        this.Name = "frmMain";
        this.Text = "Shuffle Deck";
        this.ResumeLayout(false);

    }
    #endregion

    public frmMain()
    {
        InitializeComponent();
    }

    public static void Main()
    {
        frmMain main = new frmMain();
        Application.Run(main);
    }
   
    private void btnShuffle_Click(object sender, EventArgs e)
    {
        int j;
        int cardIndex;
        int deckSize;
        int passes;
        string buff;
        string temp;
        clsCardDeck myDeck = new clsCardDeck();
 
        bool flag;
        int val = 10;
        double year;
        double result;

        //flag = int.TryParse(txtYear.Text, out val);
        // some code that does whatever…

        year = val;
        val = Convert.ToInt32(year);

        result = Math.Pow(10, year);

        passes = myDeck.ShuffleDeck();
        lblPassCounter.Text = "It took " + passes.ToString() + " passes to shuffle the deck";
        
        deckSize = myDeck.DeckSize;

        for (cardIndex = 1; cardIndex < deckSize + 1; )
        {
            buff = "";
            for (j = 0; j < 13; j++)    // Show 13 cards per line
            {
                temp = myDeck.getOneCard(cardIndex);
                if (temp.Length == 0)
                {
                    MessageBox.Show("Error reading deck.", "Processing Error");
                    return;
                }
                buff += temp + "  ";
                cardIndex++;
            }
            lstDeck.Items.Add(buff);
        }
        lstDeck.Items.Add(" ");     // Add an empty line

    }

    private void btnClose_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void btnClear_Click(object sender, EventArgs e)
    {
        lstDeck.Items.Clear();
    }

}