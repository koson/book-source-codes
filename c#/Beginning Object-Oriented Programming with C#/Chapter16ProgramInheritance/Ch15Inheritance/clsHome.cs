using System;

public class clsHome : clsBuilding
{
    //--------------------- Instance variables -------------------
    private int squareFeet;
    private int bedrooms;
    private double bathrooms;
    private decimal rentPerMonth;

    //--------------------- Constructor --------------------------
 
    public clsHome(string addr, decimal price, decimal payment,
                       decimal tax, decimal insur, DateTime date, int type) :
                        base(addr, price, payment, tax, insur, date, type)
    {
        buildingType = 3;      // Home type from base
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
    public int BedRooms
    {
        get
        {
            return bedrooms;
        }
        set
        {
            bedrooms = value;
        }
    }
    public double BathRooms
    {
        get
        {
            return bathrooms;
        }
        set
        {
            bathrooms = value;
        }
    }
    public decimal RentPerMonth
    {
        get
        {
            return rentPerMonth;
        }
        set
        {
            if (value > 0M)
                rentPerMonth = value;
        }
    }

    //--------------------- General Methods ----------------------
   

}

