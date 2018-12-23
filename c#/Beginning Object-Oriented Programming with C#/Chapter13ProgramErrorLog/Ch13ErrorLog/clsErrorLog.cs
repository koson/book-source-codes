using System;
using System.Collections.Generic;
using System.IO;

class clsErrorLog
{
    //=================== Instance members ====================
    private string fileName;
 	private string pathName;
    private string errorMessage;
    private int errorFlag;
	
    StreamWriter sw = null;
    StreamReader sr = null;

    //=================== Constructor =========================
    public clsErrorLog(string msg)
    {
        errorMessage = msg;
        errorFlag = 0;
        fileName = "ErrorLog.txt";
    }
    //=================== Property Methods ====================
    public string FileName
    {
        get
        {
            return fileName;
        }
        set
        {
            if (value.Length > 0)
                fileName = value;
        }
    }
    public string Message
    {
        get
        {
            return errorMessage;
        }
        set
        {
            if (value.Length > 0)
                errorMessage = value;
        }
    }

 	public string PathName	// Set the path name thingie
	{
        get
        {
          return pathName;
        }

        set
        {
          if (value.Length > 0)
            pathName = value;
        }
	}
    //=================== Helper Methods ======================
    //=================== General Methods =====================

    /*****
    * Purpose: This reads the error log file.
    * 
    * Parameter list:
    *  n/a
    *  
    * Return value
    *  string          the contents of the error log message file
    *****/
    public string ReadErrorLog()
    {
		string buff;
		try
		{
			string pfn = Path.Combine(pathName, fileName);
            if (File.Exists(pfn) == true)
            {
                sr = new StreamReader(pfn);
                
                buff = sr.ReadToEnd();
                sr.Close();
                return buff;
            }
		} 
		catch
		{
			return "";
		}
		return "";
    }
    /*****
     * Purpose: This writes an error log message to the error log file. The
     *          message has the date and time, the type of error, and the
     *          stack trace for the error.
     * 
     * Parameter list:
     *  n/a
     *  
     * Return value
     *  int          0 = no errors, 1 otherwise
     *****/
    public int WriteErrorLog()
    {
        errorFlag = 0;
        DateTime currentDT = DateTime.Now;

		try
		{
			// Do we have all the stings need?
			if (errorMessage.Length != 0 && pathName.Length != 0 && fileName.Length != 0)
			{
				sw = new StreamWriter(Path.Combine(pathName, fileName), false);
				sw.WriteLine(currentDT.ToShortDateString() + ", " +
                    currentDT.ToShortTimeString() + ": " + errorMessage);
                sw.WriteLine("----------------");
				sw.Close();
			} 
			else 
			{
				errorFlag = 1;	// Something bad happened
			}
		}
		catch (Exception ex)
		{
			errorMessage = ex.Message;
			errorFlag = 1;		// Something bad happened
		}
        return errorFlag;
    }
    /*****
    * Purpose: This writes an error log message to the error log file.
    * 
    * Parameter list:
    *  string msg       the error message to write
    *  
    * Return value
    *  int              0 = no errors, 1 otherwise
    *****/
    public int WriteErrorLog(string msg)
    {
        errorMessage = msg;             // Copy the message
        errorFlag = WriteErrorLog();    // Now call original one
        return errorFlag;
    }
}

