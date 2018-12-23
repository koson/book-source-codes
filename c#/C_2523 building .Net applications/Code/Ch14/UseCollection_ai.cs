using System;
using PhotoAlbum;

namespace PhotoAlbum
{  class Client
   {  static void Main(string[] args)
      {  Photos myPhotos = new PhotoAlbum.Photos();
         
         myPhotos.Add(new Photo(
            "vacation",
            "src_christmas_dec-1998_01.jpg",
            "Christmas in the Mountains"));

         myPhotos.Add(new Photo(
            "vacation",
            "src_christmas_dec-1998_02.jpg",
            "Openning Gifts"));

         foreach (Photo myPhoto in myPhotos) {
            Console.WriteLine(myPhoto.GetFullDescription());
         }  
      }
   }
}