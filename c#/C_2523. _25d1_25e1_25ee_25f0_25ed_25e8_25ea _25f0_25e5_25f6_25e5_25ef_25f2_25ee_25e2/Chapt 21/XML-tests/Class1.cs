using System;

/*
<?xml version='1.0' encoding='ISO-8859-1' ?>
<ListOfBooks>
	<Book>
		<Title>Titile-1</Title>
		<Price>50</Price>
	</Book>
	<Book>
		<Title>Title-2</Title>
		<Price>150</Price>
	</Book>
</ListOfBooks>  
*/


namespace ConsoleApplication29
{
	[Serializable]
	class Test
	{
		int i;
		int j;
	}


	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
//			XmlDocument doc = new XmlDocument();
//			doc.LoadXml("<book genre='novel' ISBN='1-861001-57-5'>" +
//				"<title>Pride And Prejudice</title>" +
//				"</book>");
//
//			XmlNode root = doc.DocumentElement;
//
//			// OuterXml �������� ��������� �������� ����
//			Console.WriteLine("OuterXml:");
//			Console.WriteLine(root.OuterXml);
//            
//			// InnerXml �� �������� ��������� �������� ����
//			Console.WriteLine();
//			Console.WriteLine("InnerXml:");
//			Console.WriteLine(root.InnerXml);
//
//			// InnerText �������� ����� ����
//			Console.WriteLine();
//			Console.WriteLine("InnerText:");
//			Console.WriteLine(root.InnerText);


//			#region �������� Xml �� �����
//			System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
//			doc.Load("ex001.xml");
//			#endregion

//			#region ����� ����������� XML
//			Console.WriteLine("doc.InnerText.ToString()");
//			Console.WriteLine(doc.InnerText.ToString());
//			Console.WriteLine();
//
//			Console.WriteLine("doc.InnerXml.ToString()");
//			Console.WriteLine(doc.InnerXml.ToString());
//			Console.WriteLine();
//
//			Console.WriteLine("doc.OuterXml.ToString()");
//			Console.WriteLine(doc.OuterXml.ToString());
//			Console.WriteLine("===================================");
//			#endregion
//
//			#region ����� ���� ���������
//			System.Xml.XmlNode root = doc.DocumentElement;
//			Console.WriteLine("doc.DocumentElement={0}", root.LocalName);
//			foreach(System.Xml.XmlNode childnode in root.ChildNodes)
//			{
//				Console.WriteLine(childnode.InnerText.ToString());
//			}
//			Console.WriteLine("===================================");
//			#endregion

//			#region	������ ���������� ���������
//			System.Xml.XmlTextReader reader = new System.Xml.XmlTextReader("ex001.xml");
//			while(reader.Read())
//			{
//				if (reader.NodeType == System.Xml.XmlNodeType.Element)
//				{
//					if (reader.Name.Equals("Title"))
//					{
//						Console.WriteLine("<{0}>", reader.GetAttribute("FontSize"));
//					}
//				}
//			}
//			reader.Close();
//			#endregion

//			#region	������ ���� ���������
//			System.Xml.XmlTextReader reader = new System.Xml.XmlTextReader("ex001.xml");
//			while(reader.Read())
//			{
//				if (reader.NodeType == System.Xml.XmlNodeType.Element)
//				{
//					if (reader.HasAttributes)
//					{	
//						while (reader.MoveToNextAttribute())
//						{
//							Console.WriteLine("{0} = {1}", reader.Name, reader.Value);
//						}
//					}
//				}
//			}
//			reader.Close();
//			#endregion

//			#region ������������ ������ xml ����� � ������� XmlTextReader
//			System.Xml.XmlTextReader reader = new System.Xml.XmlTextReader("ex001.xml");
//			while(reader.Read())
//			{
//				if(reader.NodeType == System.Xml.XmlNodeType.Element)
//				{
//					reader.Read(); // ������ ���������� ����
//					Console.WriteLine("{0}:{1}", reader.NodeType, reader.Value);
//				} else {
//					Console.WriteLine("{0}", reader.NodeType);
//				}
//			}
//			Console.WriteLine("===================================");
//			#endregion

//			#region ������� �� xml � ������� XPath
//			// �������� xpath ���������
//			System.Xml.XPath.XPathDocument xpdoc = new System.Xml.XPath.XPathDocument("ex001.xml");
//			System.Xml.XPath.XPathNavigator nav = xpdoc.CreateNavigator();

//			// ������ ������ xpath
//			System.Xml.XPath.XPathNodeIterator iterator1 = nav.Select("ListOfBooks/Book/Title");
//			while (iterator1.MoveNext())
//				Console.WriteLine(iterator1.Current);
//
//			// ���������������� ������ xpath
//			System.Xml.XPath.XPathExpression expr = nav.Compile("ListOfBooks/Book/Price");
// 			System.Xml.XPath.XPathNodeIterator iterator2 = nav.Select(expr);
//			while (iterator2.MoveNext())
//				Console.WriteLine(iterator2.Current);
//
//			// �������������� ������ � ��������������� �����������
//			System.Xml.XPath.XPathExpression expr1 = nav.Compile("sum(ListOfBooks/Book/Price/text())");
//			Console.WriteLine(expr1.ReturnType);
//			if (expr1.ReturnType == System.Xml.XPath.XPathResultType.Number)
//			{
//				double summ = (double) nav.Evaluate(expr1);
//				Console.WriteLine(summ);
//			}
//
//			// �������������� ������ ��� ��������������� ����������
//			// ����� ������ ������������ �������������� ����������
//			double summ1 = (double) nav.Evaluate("sum(ListOfBooks/Book/Price/text())*10");
//			Console.WriteLine(summ1);
//
//			Console.WriteLine("===================================");
//			#endregion

//			#region Xsl �������������
//			System.Xml.Xsl.XslTransform xslt = new System.Xml.Xsl.XslTransform();
//			xslt.Load("ex001.xsl");
//			System.IO.StringWriter output = new System.IO.StringWriter();
//
//			//��������� ��������������
//			System.Xml.Xsl.XsltArgumentList xsltArgs = new System.Xml.Xsl.XsltArgumentList();
//			xsltArgs.AddParam("min_pos", "", 1);
//			xsltArgs.AddParam("max_pos", "", 5);
//
//			xslt.Transform(nav, xsltArgs, output);
//			Console.WriteLine(output);
//			Console.WriteLine("===================================");
//			#endregion

//			#region �������� xml � dataset
//			System.Data.DataSet ds = new System.Data.DataSet(); 
//			ds.ReadXml("config.ini", System.Data.XmlReadMode.Auto); 
//			System.Data.DataTable table = ds.Tables["settings"]; 
//			System.Data.DataRow row = table.Rows[0]; 
//
//			Console.WriteLine((string)row["base_path"  ]); 
//			Console.WriteLine((string)row["update_path"]); 
//			Console.WriteLine((string)row["output_file"]); 
//			row["base_path"] = "test"; 
//			ds.WriteXml("config+.ini");
//			#endregion


//			System.Xml.XmlTextWriter xmlwriter = new System.Xml.XmlTextWriter("ex002.xml", System.Text.Encoding.GetEncoding(1251));
//			xmlwriter.WriteStartElement("ListOfBooks");
//			xmlwriter.WriteComment("�����");
//			xmlwriter.WriteEndElement();
//			xmlwriter.Close();

//			System.Xml.XmlTextWriter xmlwriter = new System.Xml.XmlTextWriter("ex002.xml", System.Text.Encoding.GetEncoding(1251));
//			xmlwriter.WriteStartDocument(); // ��������� xml
//			xmlwriter.WriteStartElement("ListOfBooks"); // �������� �������
//			xmlwriter.WriteStartElement("Book"); // ����� 1
//
//			xmlwriter.WriteStartAttribute("FontSize", null);
//			xmlwriter.WriteString("8");
//			xmlwriter.WriteEndAttribute();
//
//			xmlwriter.WriteString("Title-1");
//			xmlwriter.WriteEndElement();
//
//			xmlwriter.WriteStartElement("Book"); // ����� 2
//			xmlwriter.WriteString("Title-2");
//			xmlwriter.WriteEndElement();
//			xmlwriter.WriteEndElement();
//			xmlwriter.Close();

//			System.Xml.XmlTextWriter xmlwriter = new System.Xml.XmlTextWriter("ex002.xml", System.Text.Encoding.GetEncoding(1251));
//			xmlwriter.WriteStartDocument(); // ��������� xml
//			xmlwriter.WriteStartElement("ListOfBooks", "http://localhost/test"); // �������� �������
//			 xmlwriter.WriteStartElement("Book"); // ����� 1
//			 xmlwriter.WriteString("Title-1");
//			 xmlwriter.WriteEndElement();
//			xmlwriter.WriteEndElement();
//			xmlwriter.Close();

//			System.Xml.XmlTextWriter xmlwriter = new System.Xml.XmlTextWriter("ex002.xml", null);
//			// format xml document
//			xmlwriter.Formatting = System.Xml.Formatting.Indented;
//			// use tab as indenting character
//			xmlwriter.IndentChar = ' ';
//			// just one tab character
//          xmlwriter.Indentation = 2;
//			xmlwriter.QuoteChar = '\''; // ������������ ��������� �������
//			xmlwriter.WriteStartDocument(); // ��������� xml
//			xmlwriter.WriteStartElement("ListOfBooks"); // �������� �������
//			xmlwriter.WriteStartElement("Book"); // ����� 1
//			xmlwriter.WriteString("Title-1");
//			xmlwriter.WriteEndElement();
//			xmlwriter.WriteEndElement();
//			xmlwriter.Close();




			Console.WriteLine("END. Press Enter for continue...");
			Console.ReadLine();
		}
	}
}
