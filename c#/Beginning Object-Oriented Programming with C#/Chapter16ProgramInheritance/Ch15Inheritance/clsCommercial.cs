using System;

class clsCommercial : clsBuilding
{
    //--------------------- Instance variables -------------------
    private int squareFeet;
    private int parkingSpaces;
    private decimal rentPerSquareFoot;

    //--------------------- Constructor --------------------------
    public clsCommercial(string addr, decimal price, decimal payment,
                        decimal tax, decimal insur, DateTime date, int type) :
                        base(addr, price, payment, tax, insur, date, type)
    {
        buildingType = type;   // Commercial type from base
    }
    //--------------------- Property Methods ---------------------
    public int SquareFeet
    {
        get
        {
            return squareFeet;
        }
        set
        {
            if (value > 0)
                squareFeet = value;
        }
    }
    public int ParkingSpaces
    {
        get
        {
            return parkingSpaces;
        }
        set
        {
            parkingSpaces = value;
        }
    }
    public decimal RentPerSquareFoot
    {
        get
        {
            return rentPerSquareFoot;
        }
        set
        {
            if (value > 0M)
                rentPerSquareFoot = value;
        }
    }

    //--------------------- General Methods ----------------------
    public override string RemoveSnow()
    {
        return "Commercial: Call Acme Snow Plowing: 803.234.5566";
    }

}

