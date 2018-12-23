using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
class clsSerial
{
    //------------------- Instance members -----------------
    private string name;
    private string email;
    private int status;

    //------------------- Property methods -----------------

    public string Name
    {
        get
        {
            return name;
        }
        set
        {
            name = value;
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
            email = value;
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
            status = value;
        }
    }
    //------------------- Helper methods ------------------
    //------------------- General methods -----------------

    /*****
     * Purpose: To serialize the contents of this class
     * 
     * Parameter list:
     *  clsSerial myFriend      Serialize an instance
     *  
     * Return value:
     *  int                     0 on error, 1 otherwise
     *****/
    public int SerializeFriend(clsSerial myFriend)
    {
        try
        {
            BinaryFormatter format = new BinaryFormatter();
            FileStream myStream = new FileStream("Test.bin", FileMode.Create);
            format.Serialize(myStream, myFriend);
            myStream.Close();
        }
        catch (Exception ex)
        {
            string buff = ex.Message;
            return 0;
        }
        return 1;
    }

    /*****
    * Purpose: To deserialize an instance of this class from a file
    * 
    * Parameter list:
    *  n/a
    *  
    * Return value:
    *  clsSerial      an instance of the class with the data
    *****/
    public clsSerial DeserializeFriend()
    {
        clsSerial temp = new clsSerial();
        try
        {
            BinaryFormatter format = new BinaryFormatter();
            FileStream myStream = new FileStream("Test.bin", FileMode.Open);
            temp = (clsSerial)format.Deserialize(myStream);
            myStream.Close();
        }
        catch (Exception ex)
        {
            string buff = ex.Message;
            return null;
        }
        return temp;
     }
}
