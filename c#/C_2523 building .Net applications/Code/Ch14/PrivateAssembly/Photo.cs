using System;

namespace PhotoAlbum
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
