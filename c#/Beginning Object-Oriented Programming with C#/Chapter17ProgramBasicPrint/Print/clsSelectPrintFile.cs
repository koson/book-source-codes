using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.IO;



class clsSelectPrintFile
{
    const int SAMPLELINES = 6;

    private string fileName;
    private string buffer;

    //================================= Construcctor =================
    public clsSelectPrintFile()
    {
        buffer = null;
        fileName = null;
    }

    private StreamReader myReader;
    //================================= Property Methods ==============
    public string FileName
    {
        get 
        { return fileName;
        }
    }

    public string GetBuffer
    {
        get 
        { return buffer;
        }
    }

    //================================ General Methods ===============

    public string SelectPrintFile()
    {
        try
        {

            OpenFileDialog myFile = new OpenFileDialog();
            myFile.Title = "Select File to Print";
            myFile.InitialDirectory = @"C:\ ";
            myFile.FilterIndex = 2;
            myFile.Filter = "Text files (*.txt | .txt | All files (*.*) | *.*";
            if (myFile.ShowDialog() == DialogResult.OK)
            {
                fileName = myFile.FileName;
                return myFile.FileName;
            }
            fileName = null;
            return null;
        }
        catch (Exception ex)
        {
            MessageBox.Show("File read error: " + ex.Message);
            return null;
        }
    }

    public void ReadSampleFromFile(string infile, int LINESTOPRINT)
    {
        int lineCount = 0;
        string temp;

        try
        {

            myReader = new StreamReader(infile);

            while (true)
            {
                temp = myReader.ReadLine();
                if (temp == null)
                    break;
                buffer += temp + Environment.NewLine;
                lineCount++;
                if (lineCount >= LINESTOPRINT)
                    break;
            }
            myReader.Close();
        }
        catch
        {
            buffer = null;
        }
    }
}
