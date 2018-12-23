using System;

namespace StringSample
{
	class Extract
	{
		static void Main()
		{
			string[] sPhotos = new string[4] {
									"lri_familyreunion_jan2001_001.jpg", 
									"hri_familyreunion_jan2001_001.jpg", 
									"lri_familyreunion_jan2001_002.jpg", 
									"hri_familyreunion_jan2001_002.jpg",};
			int iCount = 0;
			string sChoice="";

			Console.WriteLine("Choose the format of photo to view:");

			// Loop through each string member and output to the Console.
			foreach ( string sPhoto in sPhotos )  
			{
				Console.WriteLine( "[{0}] {1}", iCount,sPhoto );
				iCount++;
			}
			Console.Write("? ");

			int iUserInput = int.Parse(Console.ReadLine().ToString());
			switch (sPhotos[iUserInput].Substring(0,3))
			{
				case "hri":
					sChoice = "high resolution image";
					break;

				case "lri":
					sChoice = "low resolution image";
					break;

				default:
					sChoice = "unknown image type";
					break;
			}
			// Display the user's choice.
			Console.WriteLine("You choose to view a " + sChoice);
		}
	}
}
