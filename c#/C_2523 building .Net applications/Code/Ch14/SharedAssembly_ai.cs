using System;
using SharedPhotoAlbum;

namespace PhotoAlbumClient
{
   class Client
   {
      static void Main(string[] args)
      {
         Photo myPhoto = new Photo(
            "Vacation",
            "src_christmas_dec-1998_01.jpg", 
            "Christmas in the Mountains");
         
         Console.WriteLine(myPhoto.GetFullDescription());
      }
   }
}
