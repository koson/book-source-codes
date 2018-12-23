using System;
using System.Configuration;
using System.Xml;

namespace ConfigHandler
{
	// Обработчик конфигурации возвращает XmlElement
	public class CustomSectionHandler : IConfigurationSectionHandler
	{
		public object Create(object parent, object configContext, XmlNode section)
		{
			XmlElement root = (XmlElement)section;   
			return root;
		}
	}

	public class Address
	{
		private string company;
		public string Company
		{
			get { return company; }
		}

		private string country;
		public string Country
		{
			get { return country; }
		}

		public Address(string company, string country)
		{
			this.company = company; 
			this.country = country;
		}
	}

	// Обработчик конфигурации возвращает значения
	public class AddressSectionHandler : IConfigurationSectionHandler
	{
		public object Create(object parent, object configContext, XmlNode section)
		{
			string company = section.SelectSingleNode("company").InnerText;
			string country = section.SelectSingleNode("country").InnerText;
			return new Address(company, country);
		}
	}

}
