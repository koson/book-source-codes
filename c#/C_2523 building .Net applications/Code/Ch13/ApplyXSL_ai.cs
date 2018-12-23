using System;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;

namespace ApplyXSL
{
   class ApplyXSL
   {
      static void Main()
      {
         try
         {
            XPathDocument xpdPhotoLibrary = new XPathDocument ("photo_library.xml");
            XslTransform xsltPhotoFavorites = new XslTransform();
            xsltPhotoFavorites.Load("favorite.xsl");
            XmlReader reader = xsltPhotoFavorites.Transform(xpdPhotoLibrary, null);
            
            // Use the code that was shown for reading XML with the XMLReader
            while (reader.Read())
            {
               switch (reader.NodeType)
               {
                  case XmlNodeType.ProcessingInstruction:
                     OutputXML (reader, "ProcessingInstruction");
                     break;
                  case XmlNodeType.DocumentType:
                     OutputXML (reader, "DocumentType");
                     break;
                  case XmlNodeType.Comment:
                     OutputXML (reader, "Comment");
                     break;
                  case XmlNodeType.Element:
                     OutputXML (reader, "Element");
                     while(reader.MoveToNextAttribute())
                     {
                        OutputXML (reader, "Attribute");
                     }
                     break;
                  case XmlNodeType.Text:
                     OutputXML (reader, "Text");
                     break;
                  case XmlNodeType.Whitespace:
                     break;
               }
            }
         }
         catch (Exception e)
         {
            Console.WriteLine ("Exception: {0}", e.ToString());
         }           
      }
   
      private static void OutputXML(XmlReader reader, String nodeType)
      {
         // Format the output just like we did in earlier ApplyIt
         string sValue;

         if (reader.Value.Length == 0)
            sValue = "{empty}";
         else
            sValue = reader.Value;

         Console.Write(reader.Name + "[node type = "
            + nodeType + "] = " + sValue);
         Console.WriteLine();
      }

   }
}
