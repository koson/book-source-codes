using System;
using System.Xml.Xsl;

namespace ApplyXSL
{
	class ApplyXSL
	{
		static void Main(string[] args)
		{
				XslTransform xsltransform = new XslTransform();
				xsltransform.Load("favorite.xsl");
				xsltransform.Transform("photo_library.xml", "transformed_photos.xml");
		}
	}
}
