using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Text;

namespace XSD
{
	public class XMLValidator
	{
		string xmlFileName;
		string xsdFileName;

		public XMLValidator(string xmlFileName, string xsdFileName)
		{
			this.xmlFileName = xmlFileName;
			this.xsdFileName = xsdFileName;
		}

		// ��������� �� ������
		static string errorMessage = string.Empty;
		public string ErrorMessage
		{
			get { return errorMessage; }
		}

		// ���������� ���������
		public static void ValidationHandler(object sender,	ValidationEventArgs args)
		{
			errorMessage += args.Message + "\r\n";
			
		}

		public void Validate()
		{
			try
			{
				// �����
				XmlTextReader schemaReader  = new XmlTextReader(xsdFileName);
				XmlSchemaCollection schema = new XmlSchemaCollection();
				schema.Add(null, schemaReader);

				// ���������
				XmlTextReader xmlReader = new XmlTextReader(xmlFileName);
				XmlValidatingReader vr = new XmlValidatingReader(xmlReader);
				vr.Schemas.Add(schema);
				vr.ValidationType = ValidationType.Schema;
				vr.ValidationEventHandler += new ValidationEventHandler(ValidationHandler);

				// ���������
				while(vr.Read());

				vr.Close();
			}
			catch (Exception e)
			{
				errorMessage += e.Message + "\r\n";
			}
		}
	}

	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			XMLValidator v = new XMLValidator("test.xml", "test.xsd");
			v.Validate();
			if (v.ErrorMessage.Length > 0)
				Console.WriteLine(v.ErrorMessage);
			else
				Console.WriteLine("OK");
			Console.ReadLine();
		}
	}
}
