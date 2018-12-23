using System;

namespace PhotoAlbum
{
   public class Photo_ai
   {
      public Photo()
      {
         // Constructor logic goes here.
      }

      public string GetFullDescription()
      {
         return "Catergory is " + Category +
            " and title is " + Title +
            " for the file " + FileName;
      }

      public string Category;
      public string FileName;
      public string Title;

   }
}
