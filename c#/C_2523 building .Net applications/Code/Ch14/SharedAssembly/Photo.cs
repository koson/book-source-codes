using System;

namespace SharedPhotoAlbum
{
	public class Photo
	{
		protected string sCategory;
		protected string sFileName;
		protected string sTitle;

		public Photo()
		{
			// Constructor logic goes here.
		}

		public Photo(string Category, string FileName, string Title)
		{
			// Intitialize protected variables.
			sCategory = Category; sFileName = FileName; sTitle = Title;
		}

		public string GetFullDescription()
		{
			return "Catergory is " + sCategory +
				" and title is " + sTitle +
				" for the file " + sFileName;
		}

		public string Category
		{
			get {return sCategory;}
			set {sCategory = value;}
		}

		public string FileName
		{
			get {return sFileName;}
			set {sFileName = value;}
		}

		public string Title
		{
			get {return sTitle;}
			set {sTitle = value;}
		}

	}
}
