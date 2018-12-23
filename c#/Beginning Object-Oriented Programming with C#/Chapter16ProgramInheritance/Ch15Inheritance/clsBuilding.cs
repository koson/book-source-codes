using System;

public class clsBuilding
{
    //--------------------- Symbolic constants -------------------
    public const int APARTMENT = 1;
    public const int COMMERCIAL = 2;
    public const int HOME = 3;
    
    private string[] whichType = { "", "Apartment", "Commercial", "Home" };

    //--------------------- Instance variables -------------------
    protected string address;
    protected decimal purchasePrice;
    protected decimal monthlyPayment;
    protected decimal taxes;
    protected decimal insurance;
    protected DateTime datePurchased;
    protected int buildingType;
    //--------------------- Constructor --------------------------
    public clsBuilding()
    {
        address = "Not closed yet";
    }

    public clsBuilding(string addr, decimal price, decimal payment,
                       decimal tax, decimal insur, DateTime date, int type):this()
    {
        if (addr.Equals("") == false)
            address = addr;
        purchasePrice = price;
        monthlyPayment = payment;
        taxes = tax;
        insurance = insur;
        datePurchased = date;
        buildingType = type;
    }
    //--------------------- Property Methods ---------------------

    public string Address
    {
        get
        {
            return address;
        }
        set
        {
            if (value.Length != 0)
                address = value;
        }
    }
    public decimal PurchasePrice
    {
        get
        {
            return purchasePrice;
        }
        set
        {
            if (value > 0M)
                purchasePrice = value;
        }
    }

    public decimal MonthlyPayment
    {
        get
        {
            return monthlyPayment;
        }
        set
        {
            if (value > 0M)
                monthlyPayment = value;
        }
    }

    public decimal Taxes
    {
        get
        {
            return taxes;
        }
        set
        {
            if (value > 0M)
                taxes = value;
        }
    }

    public decimal Insurance
    {
        get
        {
            return insurance;
        }
        set
        {
            if (value > 0M)
                insurance = value;
        }
    }

    public DateTime DatePurchased
    {
        get
        {
            return datePurchased;
        }
        set
        {
            if (value.Year > 2008)
                datePurchased = value;
        }
    }

    public int BuildingType
    {
        get
        {
            return buildingType;
        }
        set
        {
            if (value >= APARTMENT && value <= HOME)
                buildingType = value;
        }
    }
    //--------------------- General Methods ----------------------

    /*****
     * Purpose: Provide a basic description of the property
     * 
     * Parameter list:
     *  string[] desc       a string array to hold description
     *  
     * Return value:
     *  void
     *  
     * CAUTION: Method assumes that there are 3 elements in array
     ******/
    public void PropertySummary(string[] desc)
    {

        desc[0] = "Property type: " + whichType[buildingType] + 
                  ", " + address + 
                  ", Cost: " + purchasePrice.ToString("C") + 
                  ", Monthly payment: " + monthlyPayment.ToString("C");
        desc[1] = "     Insurance: " + insurance.ToString("C") + " Taxes: " + taxes.ToString("C") + 
                  "  Date purchased: " + datePurchased.ToShortDateString();
        desc[2] = " ";
    }

    /*****
     * Purpose: To call someone for snow removal, if available
     * 
     * Parameter list:
     *  n/a
     *  
     * Return value:
     *  string
     ******/
    public virtual string RemoveSnow()
    {
        return whichType[buildingType] + ": No snow removal service available.";
    }
}