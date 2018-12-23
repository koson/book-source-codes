using System;

namespace StringSample
{
	class Join
	{
		static void Main()
		{
			string[] sPictures = new string[6] {
									"src_familyreunion_jan2001_001.jpg", 
									"lri_familyreunion_jan2001_001.jpg", 
									"hri_familyreunion_jan2001_001.jpg", 
									"lri_familyreunion_jan2001_002.jpg", 
									"hri_familyreunion_jan2001_002.jpg", 
									"tni_familyreunion_jan2001_002.jpg"};

			string sPictureList = String.Join(", ", sPictures);

			Console.WriteLine("Here is a list of pictures that " +
				"you will be downloading: " + sPictureList);
		}
	}
}
