using System;
using System.IO;
using System.Xml;

namespace XMLSamples
{
	class WriteXML
	{
		public static void Main()
		{
			XmlTextWriter xtwFavorites = null;
			xtwFavorites = new XmlTextWriter ("photo_favorites.xml", null);
			xtwFavorites.WriteStartDocument(false);
			xtwFavorites.WriteStartElement("photofavorites");
			xtwFavorites.WriteAttributeString("owner","Frank Ryan");
			xtwFavorites.WriteEndElement();
			xtwFavorites.Flush();
			xtwFavorites.Close(); 
		}
	}
}

  