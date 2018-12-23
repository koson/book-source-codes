using System;
using VersionedPhotoAlbum;

namespace PhotoAlbumClient
{
	class Client
	{
		static void Main(string[] args)
		{
			Photo myPhotos = new Photo("Vacation","src_christmas_dec-1998_01.jpg", 
				"Christmas in the Mountains");

			Console.WriteLine(myPhotos.GetFullDescription());
		}
	}
}
