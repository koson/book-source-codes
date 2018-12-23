using System;
using PhotoAlbum;

namespace PhotoAlbum
{
	class Client
	{
		static void Main()
		{
			Photo myPhoto = new Photo();

			myPhoto.Category = "vacation";
			myPhoto.FileName = "src_christmas_dec-1998_01.jpg";
			myPhoto.Title = "Christmas in the Mountains";

			Console.WriteLine(myPhoto.GetFullDescription());
		}
	}
}
