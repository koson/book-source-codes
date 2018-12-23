﻿using System;

class clsCardDeck
{
    // =============== symbolic constants ==================
    private const int DECKSIZE = 52;    // The number of cards in the deck

    // =============== static members ======================
    private static string[] pips = {"", 
      "AS","2S","3S","4S","5S","6s","7S","8S","9S","TS","JS","QS","KS",
      "AH","2H","3H","4H","5H","6H","7H","8H","9H","TH","JH","QH","KH",
      "AD","2D","3D","4D","5D","6D","7D","8D","9D","TD","JD","QD","KD",
      "AC","2C","3C","4C","5C","6C","7C","8C","9C","TC","JC","QC","KC"
                                   };
    
    // =============== instance members =====================
    private int nextCard;                         // The next card to be dealth from deck
    private int[] deck = new int[DECKSIZE + 1];   // The deck of cards.
    private int passCount;                        // To count loop passes to shuffle deck

    // =============== constructor(s) ======================
    public clsCardDeck()
    {
        nextCard = 1;
    }

    // =============== property methods =====================
    public int DeckSize
    {
        get
        {
            return DECKSIZE;  // How many cards in the deck
        }
    }

    public int NextCard
    {
        get
        {
            return nextCard;
        }
        set
        {
            if (value > 0 && value <= deck.Length)
            {
                nextCard = value;
            }
        }
    }

    public int PassCount
    {
        get
        {
            return passCount;
        }
    }
    // =============== helper methods =======================

    // =============== general methods ======================
    /**
    * Purpose: Shuffle the deck
    * 
    * Parameter list:
    *      N/A
    * Return value:
    *      int     number of passes to shuffle the deck 
    */
    public int ShuffleDeck()
    {
        int index;
        int val;
        Random rnd = new Random();
#if DEBUG
        val = 3;
#else
        val = 0;
#endif

        passCount = 0;                      // Count how many times through the while loop
        index = 1;
        Array.Clear(deck, 0, deck.Length);  // Initialize array to 0's

        while (index < deck.Length)         // Must add one to offset 0-based arrays
            {
            val = rnd.Next(DECKSIZE) + 1;   // Generates values between 1 and 52
            if (deck[val] == 0)
            {           // Is this card place in the deck is "unused"?
                deck[val] = index;          // Yep, so assign it a card place
                index++;                    // Get ready for next card
            }
            passCount++;
        }
        nextCard = 1;                       // Prepare to deal the first card
        return passCount;
    }

    /**
    * Purpose: Show a given card in the deck.
    * 
    * Parameter list:  
    *      int         the index of the position where the card is found
    * Return value:
    *      string      the pip for the card, or empty on error
    */
    public string getOneCard(int index)
    {
        if (index > 0 && index <= deck.Length && nextCard <= deck.Length)
        {
            nextCard++;
            return pips[deck[index]];
        }
        else
        {
            return "";      // Error
        }
    }

    /**
    * Purpose: Show the abbreviation used for a given card in the deck.
    * 
    * Parameter list:  
    *      index       an integer for the index position in the deck
    *      
    * Return value:
    *      string      the pip for the card, or empty on error
    */
    public string getCardPip(int index)
    {
        if (index > 0 && index <= DECKSIZE)
        {
            return pips[index];
        }
        else
        {
            return "";      // Error
        }
    }
}

