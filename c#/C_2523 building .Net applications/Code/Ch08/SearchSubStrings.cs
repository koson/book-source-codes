using System;

namespace StringSample
{
	class Search
	{
		static void Main()
		{
			string[] sFileNames = new string[3] {"allphotos.aspx",
									"lri_familyreunion_jan2001_001.jpg", 
									"hri_familyreunion_jan2001_001.jpg"};
			
			foreach (string sFileName in sFileNames)
			{
				if (sFileName.EndsWith("jpg"))
				{
					Console.WriteLine("{0} is a photo file.",sFileName);
				}
			}			
		}
	}
}
