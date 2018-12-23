using System;
using System.Collections;
using System.IO;


class clsDirectory
{
    const string TAB = "  ";
    static private int visits;  // How many times here

    //============================= Instance variables ===================
    private int dirCounter;     // How many directories

    //============================= Constructor ==========================
    public clsDirectory()
    {
        dirCounter = 1; // The directory passed in
    }
    //============================= Property methods =====================

    public int DirectoryCount   // Make it read-only
    {
        get
        {
            return dirCounter;
        }
    }

    /*****
     * Purpose: This method creates a directory list at a given path
     * 
     * Parameter list:
     *  DirectoryInfo curDir        the current directory info
     *  int inLevel                 how deep in list
     *  ArrayList dirs              array of directory strings
     *  
     * Return value:
     *  int                 directory count or -1 on error
     *  
     *****/ 
    public int ShowDirectory(DirectoryInfo curDir, int inLevel, ArrayList dirs)
    {
        int i;
        string indent = "";

        try
        {
            for (i = 0; i < visits; i++)    // Indent subdirectories
            {
                indent += TAB;
            }

            dirs.Add(indent + curDir.Name); // Add it to list
            visits++;

            foreach (DirectoryInfo subDir in curDir.GetDirectories())
            {
                dirCounter++;
                ShowDirectory(subDir, visits, dirs);  // Recurse                    
                // FileInfo[] files = subDir.GetFiles();        
            }
            visits--;   // Go back to previous directory level

            if (indent.Length > 0)  // Adjust the indent level accordingly
                indent.Substring(0, indent.Length - TAB.Length);
        }
        catch (Exception ex)
        {
            return -1;  // Could do something with ex.Message
        }
        return dirCounter;
    }
}




