using System;

namespace StringSample
{
	class Split
	{
		static void Main()
		{
			string sFileName = "hri_disney_jan2001_001.jpg";
			string[] sFileParts = new string[4];
			char [] cDelim = new char[1] {char.Parse("_")};
			
			sFileParts = sFileName.Split(cDelim,4);

			// Because the photo filenames have a specified
			// naming convention, it is known that the third
			// member will have the date.
			Console.WriteLine("The photo was taken " + sFileParts[2]);
		}
	}
}
