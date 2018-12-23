using System;

namespace StringSample
{
	class Replace
	{
		static void Main()
		{
			string sPhotoList = "src_disney_jan_2001_001.jpg, " + 
				"lri_familyreunion_jan2001_001.jpg, " +
				"hri_familyreunion_jan2001_001.jpg";

			// Take the csv and change the deliminator to a tab.
			Console.WriteLine(sPhotoList.Replace(",", "\t"));
		}
	}
}
