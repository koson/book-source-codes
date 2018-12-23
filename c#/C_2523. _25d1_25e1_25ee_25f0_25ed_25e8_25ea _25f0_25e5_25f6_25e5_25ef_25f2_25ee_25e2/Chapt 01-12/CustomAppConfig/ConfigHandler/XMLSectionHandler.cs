using System;
using System.Configuration;
using System.Xml;
using System.Xml.Serialization;

namespace ConfigHandler
{
	// Обработчик конфигурации возвращает XmlElement
	public class XMLSectionHandler : IConfigurationSectionHandler
	{
		public object Create(object parent, object configContext, XmlNode section)
		{
			XmlSerializer ser = new XmlSerializer(typeof(XMLAddress));
			XmlNodeReader nr = new XmlNodeReader(section);
			try
			{
				XMLAddress address =  ser.Deserialize(nr) as XMLAddress;
				return address;
			}
			finally
			{
				nr.Close();
			}
		}
	}

	[XmlRoot(ElementName="addressXMLConfiguration")]
	public class XMLAddress
	{
		public XMLAddress() 
		{
		}

		private string company;
		[XmlElement(ElementName="company")]
		public string Company
		{
			get { return company;  }
			set { company = value; }
		}

		private string country;
		[XmlElement(ElementName="country")]
		public string Country
		{
			get { return country; }
			set { country = value; }
		}
	}
}
