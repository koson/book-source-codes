using System;
using System.IO;
using System.Xml;

namespace XMLSamples
{
   public class ReadXML_ai
   {
      public static void Main()
      {
         StringReader stream;
         XmlTextReader reader = null;

         stream = new StringReader("<?xml version=\"1.0\" standalone=\"no\"?>"
                  + "<!--This is a list of favorite photos-->"
                  + "<photofavorites owner=\"Frank Ryan\">"
                  + "    <photo catagory=\"vacation\" photodate=\"1979\" filename=\"src_minniemouse_jan-2001.jpg\">"
                  + "      <title>Maddie with Minnie</title>"
                  + "    </photo>"
                  + "</photofavorites>");

         reader = new XmlTextReader (stream);

         Console.WriteLine("Processing of stream started...");
         Console.WriteLine();

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

         Console.WriteLine();
         Console.WriteLine("Processing of stream complete.");

         if (reader != null){reader.Close();}
      }

      private static void OutputXML(XmlReader reader, String nodeType)
      {
         // Format the output
         string sValue = "{empty}";

         if (reader.Value.Length != 0)
            sValue = reader.Value;

         Console.WriteLine(reader.Name + "[node type = "
            + nodeType + "] = " + sValue);
      }

   }
} 
