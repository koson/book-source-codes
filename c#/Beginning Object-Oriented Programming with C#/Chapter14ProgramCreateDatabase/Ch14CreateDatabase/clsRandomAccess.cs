using System;
using System.IO;


class clsRandomAccess
{
    // ------------------ Constants --------------------
    const int NAMESIZES = 20;
    const int ADDRESSSIZES = 30;
    const int PHONESIZES = 12;
    const int EMAILSIZE = 100;
    const int DATESIZES = 10;

    const int RECORDSIZE = NAMESIZES            // First name
                         + 1                    // Middle Initial
                         + NAMESIZES            // Last name
                         + ADDRESSSIZES * 2     // Both addresses
                         + NAMESIZES            // City
                         + 2                    // State abbreviation
                         + 5                    // Zip
                         + PHONESIZES * 3       // Phone numbers
                         + EMAILSIZE            // Email address
                         + DATESIZES * 2        // Birthday & anniversary
                         + sizeof(int)          // Status
                         + 14;                  // Each string's length byte

    // ----------------------- Instance variables ------------------
    private string firstName;       // Demographics
    private string middleInitial;
    private string lastName;
    private string address1;
    private string address2;
    private string city;
    private string state;
    private string zip;
    private string homePhone;
    private string cellPhone;
    private string workPhone;
    private string email;
    private string birthday;
    private string anniversary;

    private int status;                         // Active = 1, inactive = 0

    private string errorMessage;
    private string fileName;

    private FileStream myFile;
    private BinaryReader br;
    private BinaryWriter bw;

    // ----------------------- Constructor --------------------
    public clsRandomAccess()
    {
        // initialise instance variables
        myFile = null;
        errorMessage = "";
        fileName = "Friends.bin";               // Default file name
        status = 1;
    }
    public clsRandomAccess(string fn)
        : this()    // Call no-arg constructor first
    {
        fileName = fn;
    }
    #region Property Methods
    // -------------- Property Methods --------------------

    public string FirstName
    {
        get
        {
            return firstName;
        }
        set
        {
            if (value.Length > 0)                   // Do we have a string?
            {
                firstName = value;
                if (firstName.Length > NAMESIZES)   // Too long
                {
                    firstName = firstName.Substring(0, NAMESIZES);  // Trim it.
                }
            }
        }
    }

    public string MiddleInitial
    {
        get
        {
            return middleInitial;
        }
        set
        {
            if (value.Length > 0)               // Do we have a string?
            {
                middleInitial = value;
                if (middleInitial.Length != 1)   // Too long?
                {
                    middleInitial = "n/a";
                }
            }
        }
    }

    public string LastName
    {
        get
        {
            return lastName;
        }
        set
        {
            if (value.Length > 0)                  // Do we have a string?
            {
                lastName = value;
                if (lastName.Length > NAMESIZES)   // Too long?
                {
                    lastName = lastName.Substring(0, NAMESIZES);  // Trim it.
                }
            }
        }
    }

    public string Address1
    {
        get
        {
            return address1;
        }
        set
        {
            if (value.Length > 0)                  // Do we have a string?
            {
                address1 = value;
                if (address1.Length > ADDRESSSIZES)   // Too long?
                {
                    address1 = address1.Substring(0, ADDRESSSIZES);  // Trim it.
                }
            }
            else
            {
                address1 = "n/a";
            }
        }
    }
    public string Address2
    {
        get
        {
            return address2;
        }
        set
        {
            if (value.Length > 0)                  // Do we have a string?
            {
                address2 = value;
                if (address2.Length > ADDRESSSIZES)   // Too long?
                {
                    address2 = address2.Substring(0, ADDRESSSIZES);  // Trim it.
                }
            }
            if (address2 == null)   // None given?
            {
                address2 = "n/a";
            }
        }
    }
    public string City
    {
        get
        {
            return city;
        }
        set
        {
            if (value.Length > 0)                  // Do we have a string?
            {
                city = value;
                if (city.Length > NAMESIZES)   // Too long?
                {
                    city = city.Substring(0, NAMESIZES);  // Trim it.
                }
            }
        }
    }
    public string State
    {
        get
        {
            return state;
        }
        set
        {
            if (value.Length > 0)                  // Do we have a string?
            {
                state = value.ToUpper();
                if (state.Length != 2)   // Must be 2
                {
                    state = "";  // Error
                }
            }
        }
    }
    public string Zip
    {
        get
        {
            return zip;
        }
        set
        {
            if (value.Length > 0)      // Do we have a string?
            {
                zip = value;
                if (zip.Length != 5)   // Must be 5
                {
                    zip = "";  // Error
                }
            }
        }
    }
    public string HomePhone
    {
        get
        {
            return homePhone;
        }
        set
        {
            if (value.Length > 0)      // Do we have a string?
            {
                homePhone = value;
                if (homePhone.Length > PHONESIZES)
                {
                    homePhone = homePhone.Substring(0, PHONESIZES);  // Trim it
                }
            }
            if (homePhone == null)
            {
                homePhone = "n/a";
            }
        }
    }

    public string CellPhone
    {
        get
        {
            return cellPhone;
        }
        set
        {
            if (value.Length > 0)      // Do we have a string?
            {
                cellPhone = value;
                if (cellPhone.Length > PHONESIZES)
                {
                    cellPhone = cellPhone.Substring(0, PHONESIZES);  // Trim it
                }
            }
            if (cellPhone == null)
            {
                cellPhone = "n/a";
            }
        }
    }

    public string WorkPhone
    {
        get
        {
            return workPhone;
        }
        set
        {
            if (value.Length > 0)      // Do we have a string?
            {
                workPhone = value;
                if (workPhone.Length > PHONESIZES)
                {
                    workPhone = workPhone.Substring(0, PHONESIZES);  // Trim it
                }
            }
            if (workPhone == null)
            {
                workPhone = "n/a";
            }
        }
    }

    public string Email
    {
        get
        {
            return email;
        }
        set
        {
            if (value.Length > 0)      // Do we have a string?
            {
                email = value;
                if (email.Length > EMAILSIZE)
                {
                    email = email.Substring(0, EMAILSIZE);  // Trim it
                }
            }
            if (email == null)
            {
                email = "n/a";
            }
        }
    }

    public string Birthday
    {
        get
        {
            return birthday;
        }
        set
        {
            if (value.Length > 0)      // Do we have a string?
            {
                birthday = value;
                if (birthday.Length > DATESIZES)
                {
                    birthday = birthday.Substring(0, DATESIZES);  // Trim it
                }
            }
            if (birthday == null)
            {
                birthday = "n/a";
            }
        }
    }
    public string Anniversary
    {
        get
        {
            return anniversary;
        }
        set
        {
            if (value.Length > 0)      // Do we have a string?
            {
                anniversary = value;
                if (anniversary.Length > DATESIZES)
                {
                    anniversary = anniversary.Substring(0, DATESIZES);  // Trim it
                }
            }
            if (anniversary == null)
            {
                anniversary = "n/a";
            }
        }
    }

    public int Status
    {
        get
        {
            return status;
        }
        set
        {
            if (value == 1)      // Active
            {
                status = value;
            }
            else
            {
                status = 0;
            }
        }
    }

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

    public FileStream MyFile
    {
        get
        {
            return myFile;
        }

        set
        {
            myFile = value;
        }
    }

    public BinaryReader BinReader
    {
        get
        {
            return br;
        }

        set
        {
            br = value;
        }
    }

    public BinaryWriter BinWriter
    {
        get
        {
            return bw;
        }

        set
        {
            bw = value;
        }
    }

    public String ErrorText
    {
        get
        {
            return errorMessage;
        }
    }
    #endregion

    // -------------- General Methods --------------------
    /****
     * This creates a random access file.
     * 
     * Parameter list:
     *    fn       a string that holds the file name to use
     * 
     * Return value:
     *    int      0 if error, 1 otherwise 
     ****/
    public int Create(String fn)
    {
        try
        {
            myFile = new FileStream(fn, FileMode.OpenOrCreate);
            bw = new BinaryWriter(myFile);
            fileName = fn;
        }
        catch
        {
            return 0;
        }
        return 1;
    }

    /****
     * This opens a file for reading
     * 
     * Parameter list:
     *    fn        the file name
     * 
     * Return value:
     *    int      0 if error, 1 otherwise 
     ****/

    public int Open(string fn)
    {
        if (bw == null)
        {
            return Create(fn);
        }
        else
        {
            myFile = new FileStream(fn, FileMode.OpenOrCreate);
        }

        return 1;
    }


    /****
     * This closes the currently-open file.
     * 
     * Parameter list:
     *    n/a
     * 
     * Return value:
     *    void      
     ****/

    public void Close()
    {
        if (myFile != null)
            myFile.Close();
        if (bw != null)
            bw.Close();
        if (br != null)
            br.Close();
    }

    /**
     * This writes one record to the currently-open file
     * 
     * Parameter list:
     *    num           an integer that holds the record number
     * 
     * Return value:
     *    int      0 if error, 1 otherwise 
     * 
     * CAUTION: this method assumes that the properties contain the
     * record to be written.
     */

    public int WriteOneRecord(long num)
    {
        int errorFlag = 1;

        try
        {
            if (myFile != null && bw != null)
            {   // Position the file pointer
                myFile.Seek(num * RECORDSIZE, SeekOrigin.Begin);
                bw = new BinaryWriter(myFile);

                bw.Write(firstName);        // Write the data
                bw.Write(middleInitial);
                bw.Write(lastName);
                bw.Write(address1);
                bw.Write(address2);
                bw.Write(city);
                bw.Write(state);
                bw.Write(zip);
                bw.Write(homePhone);
                bw.Write(cellPhone);
                bw.Write(workPhone);
                bw.Write(email);
                bw.Write(birthday);
                bw.Write(anniversary);
                bw.Write(status);
                bw.Close();
            }
        }
        catch (IOException ex)
        {
            errorMessage = ex.Message;    // In case they want to view it.
            errorFlag = 0;
        }
        return errorFlag;
    }

    /**
    * This reads one record and returns it as a string
    * 
    * Parameter list:
    *   num            an integer that holds the record number
    * 
    * Return value
    *   int            0 if error, 1 otherwise 
    */

    public int ReadOneRecord(long num)
    {
        try
        {
            if (myFile != null)
                myFile.Close();

            myFile = new FileStream(fileName, FileMode.Open);
            br = new BinaryReader(myFile);

            if (myFile != null && br != null)
            {
                myFile.Seek(num * RECORDSIZE, SeekOrigin.Begin);    // Position the file pointer
                firstName = br.ReadString();
                middleInitial = br.ReadString();
                lastName = br.ReadString();
                address1 = br.ReadString();
                address2 = br.ReadString();
                city = br.ReadString();
                state = br.ReadString();
                zip = br.ReadString();
                homePhone = br.ReadString();
                cellPhone = br.ReadString();
                workPhone = br.ReadString();
                email = br.ReadString();
                birthday = br.ReadString();
                anniversary = br.ReadString();
                status = br.ReadInt32();
                br.Close();

            }
        }
        catch (IOException ex)
        {
            errorMessage = ex.Message;
            return 0;
        }

        return 1;
    }
    /**
    * Purpose: This determines the number of records currently in the file
    * 
    * Parameter list:
    *   void
    * 
    * Return value
    *   long          the number of bytes in the file
    */
    public long getRecordCount()
    {
        long records = 0;
        long remainder;

        try
        {
            if (myFile != null)
            {
                records = myFile.Seek(0, SeekOrigin.End);    // Position the file pointer
                Close();
            }
        }
        catch (IOException ex)
        {
            //MessageBox.Show("Error: " + ex.Message);
            return -1;
        }
        remainder = records % RECORDSIZE;     // See if there is a partial record
        records = records / RECORDSIZE;       // Calculate the records
        if (remainder > 0)                    // If there was a partial record...
            records++;                          // ...up the counter to account for it.

        return records;
    }

 

}
