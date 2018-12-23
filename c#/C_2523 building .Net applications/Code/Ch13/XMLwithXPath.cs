using System;
using System.IO;
using System.Xml;
using System.Xml.XPath;

namespace XMLSamples
{
	public class XMLwithXPath
	{
		public static void Main()
		{
			XPathDocument xpdPhotoLibrary = new XPathDocument("photo_library.xml");
			XPathNavigator xpnPhotoLibrary = xpdPhotoLibrary.CreateNavigator();
			XPathNodeIterator xpniPhotoLibrary =  xpnPhotoLibrary.Select ("//photo/title");

			while (xpniPhotoLibrary.MoveNext())
			{
				Console.WriteLine(xpniPhotoLibrary.Current.Name + " = " + xpniPhotoLibrary.Current.Value);
			}
		}
	}
}

  