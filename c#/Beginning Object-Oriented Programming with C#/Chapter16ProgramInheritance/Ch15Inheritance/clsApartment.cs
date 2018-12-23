using System;

class clsApartment : clsBuilding 
{
    //--------------------- Instance variables -------------------
    private int units;
    private decimal rentPerUnit;
    private double occupancyRate;

    //--------------------- Constructor --------------------------
    public clsApartment():base()
    {
    }
    public clsApartment(string addr, decimal price, decimal payment,
                       decimal tax, decimal insur, DateTime date, int type) : 
                        base(addr, price, payment, tax, insur, date, type)
    {
        buildingType = type;  // Apartment type from base
  }
   //--------------------- Property Methods ---------------------
    public int Units
    {
        get
        {
            return units;
        }
        set
        {
            if (value > 0)
                units = value;
        }
    }
    public decimal RentPerUnit
    {
        get
        {
            return rentPerUnit;
        }
        set
        {
            if (value > 0M)
                rentPerUnit = value;
        }
    }
    public double OccupancyRate
    {
        get
        {
            return occupancyRate;
        }
        set
        {
            if (value > 0.0)
                occupancyRate = value;
        }
    }

    //--------------------- General Methods ----------------------
    public override string RemoveSnow()
    {
        return "Apartment: Call John's Snow Removal: 859.444.7654";
    }
}

