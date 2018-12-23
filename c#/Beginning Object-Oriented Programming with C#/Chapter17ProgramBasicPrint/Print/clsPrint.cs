using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Printing;
using System.Drawing;
using System.IO;

class clsPrint
{
    private string inFile;

    private Font myFont;
    private StreamReader myReader;

    public clsPrint(string file)
    {
        inFile = file;
    }

    public int Print()
    {
        try {

            myReader = new StreamReader (inFile);   // File to print
            myFont = new Font ("Arial", 10);        // Use this font and size
           
            PrintDocument myDocToPrint = new PrintDocument();

            myDocToPrint.PrintPage += new PrintPageEventHandler(this.PrintTextFileHandler); // PrintPage event handler

            myDocToPrint.Print();

            if (myReader != null)   // If reader not closed already...
                myReader.Close();
            return 1;

        } catch {
            return -1;
        }

    }

    private void PrintTextFileHandler (object sender, PrintPageEventArgs printArgs)
    {
        int lineCount = 0;
        int index;

        float linesAllowed = 0.0f;
        const int CHARSPERLINE = 100;
        float yCoord = 0.0f;
        float leftMargin = printArgs.MarginBounds.Left * .5f;   // Kinda arbitrary, but it works
        float topMargin = printArgs.MarginBounds.Top;
   
        string buffer = null;
        string temp;

        Graphics g = printArgs.Graphics;

        // How many lines per page?
        linesAllowed = printArgs.MarginBounds.Height / myFont.GetHeight (g);

        // Read the file
        while (lineCount < linesAllowed && ((buffer = myReader.ReadLine()) != null))
        {
            // Get the starting position
            yCoord = topMargin + (lineCount * myFont.GetHeight(g));

            if (buffer.Length > CHARSPERLINE)   // Wrap line if necessary
            {
                index = buffer.LastIndexOf(" ", (CHARSPERLINE - 10));
                temp = buffer.Substring(0, index) + Environment.NewLine;
                g.DrawString(temp, myFont, Brushes.Black, leftMargin, yCoord, new StringFormat());
                lineCount++;    // Add for the extra line
                yCoord = topMargin + (lineCount * myFont.GetHeight(g)); // Set y coord for next line
                // The rest of the line, with double-margin spacing added 
                g.DrawString(buffer.Substring(index + 1), myFont, Brushes.Black, leftMargin + leftMargin, yCoord, new StringFormat());
                lineCount++;
            }
            else
            {
                g.DrawString(buffer, myFont, Brushes.Black, leftMargin, yCoord, new StringFormat());
                lineCount++;
            }
        }
       
        if (buffer == null)     // Are we done?
        {
            printArgs.HasMorePages = false; // yep
        }
        else
        {
            printArgs.HasMorePages = true;  // nope
        }
    }
}

