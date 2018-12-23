using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Xml;

using ConfigHandler;

namespace TestApplication
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			// Обычное получение параметров
			Console.WriteLine(ConfigurationSettings.AppSettings["key1"]);

			// Стандартный обработчик дополнительной секции 
			// <add key="v1" value="val1"/>
			NameValueCollection vars = (NameValueCollection)
				ConfigurationSettings.GetConfig("variableConfig/variableSection");
			Console.WriteLine(vars["v1"]);

			// Чтение Xml узла
			XmlElement cfg = (XmlElement)ConfigurationSettings.GetConfig("taskListConfig/taskConfiguration");
			foreach (XmlNode node in cfg.ChildNodes)
			{
				if (node.NodeType == XmlNodeType.Element)
					Console.WriteLine(node.OuterXml);
			}

			// Чтение специальным классом
			Address adr =(Address) ConfigurationSettings.GetConfig("addressConfig/addressConfiguration");
			Console.WriteLine("company={0}, country={1}", adr.Company, adr.Country);

			// Чтение через XML
			XMLAddress adrXML =(XMLAddress) ConfigurationSettings.GetConfig("addressXMLConfig/addressXMLConfiguration");
			Console.WriteLine("company={0}, country={1}", adrXML.Company, adrXML.Country);

			Console.ReadLine();
		}
	}
}
