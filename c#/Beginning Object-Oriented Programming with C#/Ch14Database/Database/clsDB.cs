#define DEBUG
//#undef DEBUG

using System;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

class clsDB
{
    // --------------------- Symbolic constants -----------------

    // These are for an Access database...
    private const string CONNECTSTRING = "Provider=Microsoft.Jet.OLEDB.4.0;Data source =";
    private const string CONNECTSTRINGPART2 = "Jet OLEDB:Engine Type=5";

    // --------------------- Instance members -------------------
    private string dbName;              // Database name
    private string pathName;            // Full path to db
    private string combinedName;        // Two above combined
    private string connectString;       // Connection string

    // --------------------- Constructor ------------------------
    public clsDB()
    {
        dbName = "";
    }
    public clsDB(string name)           // Overridden constructor
    {
        dbName = name;
        pathName = Application.StartupPath;
        connectString = CONNECTSTRING;
    }

    public string getConnectString
    {
        get
        {
            return connectString;
        }
        set
        {
            if (value.Length > 0)
                connectString = value;
        }
    }

    // --------------------- Helper methods ---------------------
    // --------------------- General methods --------------------

    /*****
     * Purpose: To create a new database
     * 
     * Parameter list:
     *  string name     the name of the new database
     *  
     * Return value:
     *  int             1 on success, 0 otherwise
     *****/
    public int CreateNewDB(string name)
    {
        int index;
        string newDB;

        try
        {
            if (name.Length == 0 && dbName.Length == 0)   // No DB name given?
            {
                return 0;
            }
            index = name.LastIndexOf('.');
            if (index == -1)        // No secondary file name?
            {
                dbName += ".mdb";     // Assume Access DB
                name = dbName;
            }
            combinedName = Path.Combine(pathName, name);    // Where to put it

            ADOX.CatalogClass myCat = new ADOX.CatalogClass();

            newDB = CONNECTSTRING + combinedName + ";" + CONNECTSTRINGPART2;
            myCat.Create(newDB);
            myCat = null;

        }
        catch (Exception ex)
        {
#if DEBUG
            // Don't put user I/O in a class except for debugging
            MessageBox.Show("Error: " + ex.Message);
#endif
            return 0;
        }
        return 1;
    }

    /*****
    * Purpose: Process an SQL command.
    * 
    * Parameter list:
    *  string sqlCommand    a command string that holds the CREATE TABLE
    *                       directives
    *  
    * Return value:
    *  int             1 on success, 0 otherwise
    *  
    * CAUTION: The connect string assumes an Access database. It will need
    *           to be changed for other databases.
    *****/
    public int ProcessCommand(string sqlCommand)
    {
        int flag = 1;
        OleDbConnection myDB = new OleDbConnection();
        OleDbCommand command;
        connectString = CONNECTSTRING + dbName;

        try
        {
            myDB.ConnectionString = connectString;          // Initialize 
            myDB.Open();                                    // Open DB

            command = new OleDbCommand(sqlCommand, myDB);   // Set the command
            command.ExecuteNonQuery();                      // Create new table
        }
        catch (Exception ex)
        {
#if DEBUG
            MessageBox.Show("Error: " + ex.Message);
#endif
            flag = 0;
        }
        finally
        {
            myDB.Close();                                   // Close it
        }
        return flag;
    }

    /*****
    * Purpose: This method adds one record to the database.
    * 
    * Parameter list:
    *  string sqlCommand    a command string that holds the INSET INTO
    *                       directive
    *  
    * Return value:
    *  int             1 on success, 0 otherwise
    *  
    * CAUTION: The connect string assumes an Access database. It will need
    *           to be changed for other databases.
    *****/
    public int InsertData(string sqlCommand)
    {
        int flag = 1;
        OleDbConnection myDB = new OleDbConnection();
        OleDbCommand command;

        connectString = CONNECTSTRING + dbName;

        try
        {
            myDB.ConnectionString = connectString;          // Initialize 
            myDB.Open();                                    // Open DB
            command = new OleDbCommand(sqlCommand, myDB);   // Set the command
            command.ExecuteNonQuery();                      // Create new table
        }
        catch (Exception ex)
        {
#if DEBUG
            MessageBox.Show("Error: " + ex.Message);
#endif
            flag = 0;
        }
        finally
        {
            myDB.Close();                                   // Close it
        }
        return flag;
    }


  /*****
  * Purpose: This method fills in the column names for a database table.
  * 
  * Parameter list:
  *  string sqlCommand    a command string that holds the INSET INTO
  *                       directive
  *  string whichTable    the table for which to get the info
  *  
  * Return value:
  *  int             1 on success, 0 otherwise
  *  
  * CAUTION: The connect string assumes an Access database. It will need
  *          to be changed for other databases.
  *****/
    public int GetColumnInfo(string[] colNames, string whichTable)
  {
      int flag = 1;
      int i;
      string buff = "";
      string index = "";
      bool err;
      OleDbConnection myDB = new OleDbConnection();
      connectString = CONNECTSTRING + dbName;

      try
      {
        myDB.ConnectionString = connectString;          // Initialize 
        myDB.Open();                                    // Open DB

        //string[] elements = new string[] { null, null, "Friend", null };
        string[] elements = new string[] { null, null, whichTable, null };

        DataTable table = myDB.GetSchema("Columns", elements);

        foreach (DataRow row in table.Rows)
        {
          foreach (DataColumn col in table.Columns)
          {
            if (col.ColumnName.Equals("ORDINAL_POSITION") == true)  // Get position...
              index = row[col].ToString();
            if (col.ColumnName.Equals("COLUMN_NAME") == true)       // ... and name
              buff = (string)row[col];
          }
          err = int.TryParse(index, out i);
          colNames[i - 1] = buff;             // Adjust for ordinal values and copy
        }
      } 
      catch (Exception ex)
      {
#if DEBUG
          MessageBox.Show("Error: " + ex.Message);
#endif
          flag = 0;
      }
      finally
      {
          myDB.Close();                                   // Close it
      }
      return flag;
  }

    /*****
  * Purpose: This method gets the names of the tables in a database.
  * 
  * Parameter list:
  *  string sqlCommand    a command string that holds the INSET INTO
  *                       directive
  *  
  * Return value:
  *  int             1 on success, 0 otherwise
  *  
  * CAUTION: The connect string assumes an Access database. It will need
  *           to be changed for other databases.
  *****/
    public int GetTableInfo(string[] colNames)
  {
    int flag = 1;
    string buff = "";
    int index = 0;
    OleDbConnection myDB = new OleDbConnection();
    connectString = CONNECTSTRING + dbName;
    
    try
    {
      myDB.ConnectionString = connectString;          // Initialize 
      myDB.Open();                                    // Open DB

      string[] restrictions1 = new string[] { null, null, null, null };
      System.Data.DataTable table = myDB.GetSchema("Tables", restrictions1);

      foreach (System.Data.DataRow row in table.Rows)
      {
        foreach (System.Data.DataColumn col in table.Columns)
        {
          if (col.ColumnName.Equals("TABLE_NAME") == true)       // ... and name
          {
            buff = (string)row[col];
            break;
          }
        }
        // Don't save Access system tables
        if (buff.Equals("MSysAccessObjects") == true || buff.Equals("MSysAccessXML") == true ||
            buff.Equals("MSysACEs") == true || buff.Equals("MSysObjects") == true || buff.Equals("MSysQueries") == true || 
            buff.Equals("MSysRelationships") == true)
          continue;
        else
          colNames[index++] = buff;    // Save table names
      }
    }
    catch (Exception ex)
    {
#if DEBUG
        MessageBox.Show("Error: " + ex.Message);
#endif
        flag = 0;
    }
    finally
    {
      myDB.Close();                     // Close it
    }
    return flag;
  }

}

