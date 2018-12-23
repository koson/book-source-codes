using System; using System.IO; using System.Xml;
namespace XMLSamples
{  
   public class ReadXML
   {  
      public static void Main()
      {  
         XmlTextReader reader = null;
         string sXMLDocument = "photo_library.xml";
         
         try         
         {
            // This will attempt to read a missing document
            reader = new XmlTextReader (sXMLDocument);
            reader.Read();
         }
      
         catch (Exception e) 
         {
            throw new Exception 
               ("Error, can not read " + sXMLDocument,e);
         }
         
         finally 
         {
            // Finished with XmlTextReader
            if (reader != null)
               reader.Close();
         }
      }
   }
}