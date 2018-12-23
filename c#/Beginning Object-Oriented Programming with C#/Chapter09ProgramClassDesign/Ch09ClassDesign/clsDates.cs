using System;
using System.Collections.Generic;
using System.Text;

public class clsDates
{
    // =============== symbolic constants ===================

    // =============== static members =======================
    private static int[] daysInMonth = { 0, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

    // =============== instance members =====================
    private int day;
    private int month;
    private int year;
    private int leapYear;

    private DateTime current;

    // =============== constructor(s) ======================

    public clsDates()
    {
        current = DateTime.Now; // Sets DateTime to right now
    }
    public clsDates(int yr)
    {
      
        year = yr;
    }
    // =============== property methods =====================

    // =============== helper methods =======================

    // =============== general methods ======================

    /*****
     * Purpose: To determine if the year is a leap year. Algorithm 
     *          taken from C Programmer's Toolkit, Purdum, Que Corp.,
     *          1993, p. 258.
     *          
     * Parameter list:
     *    int year      the year under consideration
     *    
     * Return value:
     *    int           1 if a leap year, 0 otherwise
     *****/

    public int getLeapYear(int year)
    {
        if (year % 4 == 0 && year % 100 != 0 || year % 400 == 0)
            return 1;   // It is a leap year
        else
            return 0;   // Nope.
    }
    public int getLeapYear()
    {
        return getLeapYear(year);
    }

    /*****
     * Purpose: To determine the date for Easter given a year. Algorithm 
     *          taken from C Programmer's Toolkit, Purdum, Que Corp.,
     *          1993, p. 267.
     *          
     * Parameter list:
     *    int year      the year under consideration
     *    
     * Return value:
     *    string        the date in MM/DD/YYYY format
     *****/
    public string getEaster(int year)
    {
        int offset;
        int leap;
        int day;
        int temp1;
        int temp2;
        int total;


        offset = year % 19;
        leap = year % 4;
        day = year % 7;
        temp1 = (19 * offset + 24) % 30;
        temp2 = (2 * leap + 4 * day + 6 * temp1 + 5) % 7;
        total = (22 + temp1 + temp2);
        if (total > 31)
        {
            month = 4;            // Easter is in April...
            day = total - 31;   // ...on this day
        }
        else
        {
            month = 3;            // Easter is in March...
            day = total;        // ...on this day
        }
        DateTime myDT = new DateTime(year, month, day);
        return myDT.ToLongDateString();
    }
    /*****
     * Purpose: Return the number of days in a given month.
     *          
     * Parameter list:
     *    int year      the year under consideration
     *    int month     the month  "       "
     *    
     * Return value:
     *    int           the number of days in the month or 0 on error
     *****/

    public int getDaysInMonth(int month, int year)
    {
        int days;

        if (month < 1 || month > 12 || year < 1 || year > 9999)
        {
            return 0;
        }
        if (month != 2)    // As long as it's not February
        {
            days = daysInMonth[month];
        }
        else
        {
            days = daysInMonth[2] + getLeapYear(year);
        }
        return days;
    }
}

