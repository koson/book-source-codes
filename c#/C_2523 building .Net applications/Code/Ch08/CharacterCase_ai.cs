using System;

namespace StringSample
{  
   class InitialCaps
   {  
      static void Main()
      {  
         string sFullName = "joE mARkiewiCz";
         string [] sNameParts = 
            sFullName.Split(char.Parse(" "));
         string [] sNewParts = new string[2];
         int iCount = 0;

         foreach (string sPart in sNameParts)
         {
            sNewParts[iCount] = 
               sPart[0].ToString().ToUpper() + 
               sPart.Substring(1,(sPart.Length - 1)).ToLower();
            iCount++;
         }
         
         string sNewFullName = String.Join(" ",sNewParts);

         Console.WriteLine("Applying the custom intitial "
            + "caps formatting on '{0}' give the following "
            + "result: {1}", sFullName, sNewFullName);
      }
   }
}
