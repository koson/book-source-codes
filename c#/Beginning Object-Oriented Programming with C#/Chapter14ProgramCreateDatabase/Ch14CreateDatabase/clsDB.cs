#define DEBUG
//#undef DEBUG

using System;
using System.IO;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data;
 

class clsDB
{
    // --------------------- Instance members -------------------
    private string serverName;                  // Server for DB
    private string databaseName;                // Name of DB
    private Int32 recordCount;                  // How many friends in DB
    private string connectString;

    // --------------------- Constructor ------------------------
    public clsDB()
    {
        databaseName = "";
    }
    public clsDB(string conn) : this()
    {
        connectString = conn;
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

    public string getServerName
    {
        get
        {
            return serverName;
        }
        set
        {
            if (value.Length > 0)
                serverName = value;
        }
    }

    public string getDatabaseName
    {
        get
        {
            return databaseName;
        }
        set
        {
            if (value.Length > 0)
                databaseName = value;
        }
    }

    public Int32 getRecordCount
    {
        get
        {
            return recordCount;
        }
        set
        {
            if (value > -1)
                recordCount = value;
        }
    }

    // --------------------- Helper methods ---------------------

    // --------------------- General methods --------------------


    /*****
     * Purpose: Retrieve the current number of records in the DB
     * 
     * Parameter list:
     *  string connectStr     the connection string for the DB
     *  
     * Return value:
     *  Int32                 The number of records in the DB. Note: This also sets recordCount
     *****/
    public Int32 ReadRecordCount(string connectStr)
    {    
        try
        {
            SqlConnection conn = new SqlConnection(connectStr);
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT COUNT(LastName) FROM Friends", conn);
            recordCount = (Int32)cmd.ExecuteScalar();

        } catch
        {
            return -1;
        }
        return recordCount;
    }


    /*****
    * Purpose: To process an SQL command on a database
    * 
    * Parameter list:
    *  string sqlCommand    a command string that holds the CREATE TABLE
    *                       directives
    *  
    * Return value:
    *  int             1 on success, 0 otherwise
    *  
    * CAUTION: The method assumes the connect string is already set
    *****/
    public int ProcessCommand(string sqlCommand)
    {
        int flag = 1;

        try
        {
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                conn.Open();

                SqlCommand command = new SqlCommand(sqlCommand, conn);
                flag = command.ExecuteNonQuery();                      // Add new record
                conn.Close();
            }
        }
        catch
        {
            flag = 0;
        }
        return flag;
    }
}

