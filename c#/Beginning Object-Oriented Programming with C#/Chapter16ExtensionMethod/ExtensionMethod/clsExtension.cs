using System;
using System.Text.RegularExpressions;

namespace StringExtensionMethods
{
    public static class clsExtension
    {
        // For details on regualr expression pattern options, see:
        // http://www.regular-expressions.info/reference.html

        public static bool CheckValidSSN(this string str)
        {
            int len = str.Length;

            Regex pattern = null;
            if (len == 11 || len == 9)  // Is it xxx-xx-xxxx or xxxxxxxxx?
            {
                if (len ==  9)
                {
                    pattern = new Regex(@"\d{9}");     // Accept 9 digit characters
                } else {
                    pattern =  new Regex(@"\d{3}-\d{2}-\d{4}"); // Accept ddd-dd-dddd
                }
                return pattern.IsMatch(str);
            }
            else
            {
            return false;               // Not valid
            }
        }
   }
}

