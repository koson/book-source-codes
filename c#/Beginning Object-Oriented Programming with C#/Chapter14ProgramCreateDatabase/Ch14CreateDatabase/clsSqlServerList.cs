using System;
using System.Collections.Generic;


class clsSqlServerList : IComparable, ICloneable
{
    public string ServerName;

    //================================ Constructor ===========================
    public clsSqlServerList()      
    {
        ServerName = "";
    }
 
#region ICloneable Members
 
    public object Clone()
    {
        try
        {
            if (this == null)
            {
                return null;
            }
            clsSqlServerList myServerList = new clsSqlServerList {ServerName = ServerName};
            return myServerList;
        }
        catch
        {
            throw new NotImplementedException();
        }
    }
 
#endregion
 
#region IComparable Members
 
    public int CompareTo(object myObject)
    {
        try
        {
            if (!(myObject is clsSqlServerList))
            {
                throw new Exception("The object is not a SqlServerList instance.");
            }
            if (this == null)           // Empty object
            {
                return -1;
            }
            return ServerName.CompareTo((myObject as clsSqlServerList).ServerName);
        }
        catch
        {
            throw new NotImplementedException();
        }
    }
 
#endregion
 
}
