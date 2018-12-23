using System;


class clsInBetweenRules
{
    // =============== symbolic constants ===================
    const int TIE = 0;
    const int PLAYERWINS = 1;
    const int DEALERWINS = 2;

    // =============== static members =======================
    // =============== instance members =====================
    private int balance;        // The player's money balance
    private int wager;          // Amount of current bet
    private int lowCard;        // The low card value 1 - 13
    private int lowCardIndex;   // The position of this card in pips
    private int hiCard;         // The high card value 1 - 13
    private int hiCardIndex;    // The position of this card in pips
    private int dealtCard;      // The dealt card value 1 - 13
    private int dealtCardIndex; // The position of this card in pips

    private clsCardDeck myDeck; // A card deck object

    // =============== constructor(s) ======================
    public clsInBetweenRules()
    {
        balance = 100;
        wager = 10;
        myDeck = new clsCardDeck();
    }
    // =============== property methods =====================
    public int Balance
    {
        get
        {
            return balance;
        }
        set
        {
            if (value >= 0)
            {
                balance = value;
            }
        }
    }

    public int Wager
    {
        get
        {
            return wager;
        }
        set
        {
            if (value > 0)
            {
                wager = value;
            }
        }
    }


    // =============== helper methods =======================

    /*****
    * Purpose: Deals out the next three cards and fills in the hand[]
    *          array that was passed in from frmMain. It always arranges
    *          cards so lower of first two cards is displayed on the
    *          left of frmMain.
    * 
    * Parameter list:
    *  string[] hand        the three cards for a hand
    *  
    * Return value:
    *  void
    *  
    *  *****/
    private void SetCards(string[] hand)
    {
        int temp;

        hand[0] = myDeck.getCardPip(lowCardIndex);
        hand[1] = myDeck.getCardPip(hiCardIndex);
        hand[2] = myDeck.getCardPip(dealtCardIndex);
        if (lowCard == hiCard || lowCard < hiCard)      // A tie
        {
            hand[0] = myDeck.getCardPip(lowCardIndex);
            hand[1] = myDeck.getCardPip(hiCardIndex);
        }
        else
        {
            temp = hiCard;          // Swap hi and lo cards
            hiCard = lowCard;
            lowCard = temp;

            temp = hiCardIndex;     // Swap hi and lo indexes
            hiCardIndex = lowCardIndex;
            lowCardIndex = temp;

            hand[0] = myDeck.getCardPip(lowCardIndex);
            hand[1] = myDeck.getCardPip(hiCardIndex);
        }
    }

    /*****
    * Purpose: Sets the outcome of the bet and tells where to display the
    *          down card.
    * 
    * Parameter list:
    *  ref int outCome      who won the game
    *  ref int position     where to display the down card
    *  
    * Return value:
    *  void
    *  
    * CAUTION: the two ints are passed in by reference, which means this
    *          method can permanently change their values.
    *  *****/
    private void SetWinnerAndPosition(ref int outCome, ref int position)
    {

        if (dealtCard == lowCard)   // Dealt and low card equal
        {
            outCome = DEALERWINS;
            position = 2;
            return;
        }
        if (dealtCard < lowCard)    // Dealt card less than low card
        {
            outCome = DEALERWINS;
            position = 1;
            return;
        }
        if (dealtCard > lowCard && dealtCard < hiCard) // Dealt card in between
        {
            outCome = PLAYERWINS;
            position = 3;
            return;
        }

        if (dealtCard == hiCard) // Dealt card equals hi card
        {
            outCome = DEALERWINS;
            position = 4;
            return;
        }
        if (dealtCard > hiCard) // Dealt card equals hi card
        {
            outCome = DEALERWINS;
            position = 5;
            return;
        }
    }

    // =============== general methods ======================

    /*****
     * Purpose: Gets the first card and treats it as first displayed card
     * 
     * Parameter list:
     *  n/a
     *  
     * Return value:
     *  void
     *  
     * CAUTION: King is a special case since its modulus = 0
     *  *****/
    public void getFirstCard()
    {
        lowCardIndex = myDeck.getOneCard();
        lowCard = lowCardIndex % 13;
        if (lowCard == 0)               // A King
            lowCard = 13;
    }

    /*****
     * Purpose: Gets the second card and treats it as second displayed card
     * 
     * Parameter list:
     *  n/a
     *  
     * Return value:
     *  void
     *  
     * CAUTION: King is a special case since its modulus = 0
     *  *****/
    public void getSecondCard()
    {
        hiCardIndex = myDeck.getOneCard();
        hiCard = hiCardIndex % 13;
        if (hiCard == 0)               // A King
            hiCard = 13;
    }
    /*****
    * Purpose: Gets the last card and treats it as down card
    * 
    * Parameter list:
    *  n/a
    *  
    * Return value:
    *  void
    *  
    * CAUTION: King is a special case since its modulus = 0
    *  *****/
    public void getDealtCard()
    {
        dealtCardIndex = myDeck.getOneCard();
        dealtCard = dealtCardIndex % 13;
        if (dealtCard == 0)            // A King
            dealtCard = 13;
    }

    /*****
     * Purpose: Shuffle the deck
     * 
     * Parameter list:
     *  n/a
     *  
     * Return value:
     *  void
     *  
     *  *****/
    public void Shuffle()
    {
        myDeck.ShuffleDeck();
    }

    /*****
     * Purpose: Gets the number of cards left in the deck.
     * 
     * Parameter list:
     *  n/a
     *  
     * Return value:
     *  int             the number of cards left
     *  
     * CAUTION: King is a special case since its modulus = 0
     *  *****/
    public int getCardsLeft()
    {
        return myDeck.getCardsLeftInDeck();
    }

    /*****
    * Purpose: Deals out a hand. Note that all three cards are dealth at
    *          once, but the dealt card is not displayed until after the
    *          bet. The results are known before the bet, but not revealed
    *          now.
    * 
    * Parameter list:
    *  string[] hand        the three cards for a hand
    *  ref int outCome      who won the game
    *  ref int position     where to display the down card
    *  
    * Return value:
    *  void
    *  
    * CAUTION: the two ints are passed in by reference, which means this
    *          method can permanently change their values.
    *  *****/
    public void DealHand(string[] hand, ref int outCome, ref int position)
    {
        getFirstCard();     // Get first two display cards
        getSecondCard();

        getDealtCard();     // Get down card

        SetCards(hand);     // Rearrange if necessary

                            // Who wins and where to display down card
        SetWinnerAndPosition(ref outCome, ref position);
    }

}

