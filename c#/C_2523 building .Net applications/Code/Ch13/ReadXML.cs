using System;
using System.IO;
using System.Xml;

namespace XMLSamples
{
	public class ReadXML
	{
		public static void Main()
		{
			XmlTextReader reader = null;
			reader = new XmlTextReader ("photo_library.xml");
			while (reader.Read())
			{
				switch (reader.NodeType)
				{
					case XmlNodeType.Element:
						Console.WriteLine(reader.ReadOuterXml());
						break;
					default:
						break;
				}
			}
		}
	}
}

  