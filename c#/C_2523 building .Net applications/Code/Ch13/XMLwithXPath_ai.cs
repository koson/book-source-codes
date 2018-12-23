using System;
using System.IO;
using System.Xml;
using System.Xml.XPath;

namespace XMLSamples
{
   public class XMLwithXPath
   {
      private const String sXMLDocument = "photo_library.xml";

      public static void Main()
      {
         Console.WriteLine("XPath query has started and the results are:");
 
         XPathDocument xpdPhotoLibrary = new XPathDocument(sXMLDocument);
         XPathNavigator xpnPhotoLibrary = xpdPhotoLibrary.CreateNavigator();

         try
         {
            // Get all titles of the photos.
            XPathNodeIterator xpniPhotoLibrary =  xpnPhotoLibrary.Select ("//photo/title");

            while (xpniPhotoLibrary.MoveNext())
            {
               Console.WriteLine(xpniPhotoLibrary.Current.Name + " = " + xpniPhotoLibrary.Current.Value);
            }
         }
         catch (Exception e)
         {
            Console.WriteLine ("Exception: {0}", e.ToString());
         }
      }
   }
}
