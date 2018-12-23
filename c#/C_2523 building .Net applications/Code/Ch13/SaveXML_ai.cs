using System; using System.IO; using System.Xml;

public class Sample
{  public static void Main()
   {  //Create the XmlDocument.
      XmlDocument doc = new XmlDocument();
    
      string sXML = 
@"<?xml version=""1.0"" standalone=""no""?>
<!--This file represents a list of favorite photos-->
<photofavorites owner=""Frank Ryan"">
   <photo catagory=""vacation"" photodate=""2000""> 
      <title>Maddie with Minnie</title>
   </photo>
</photofavorites>";
      
      doc.LoadXml(sXML);

      //Save the document to a file.
      doc.Save("data.xml");
}}
