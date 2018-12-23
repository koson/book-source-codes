using System;

namespace VersionedPhotoAlbum
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
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
			Console.WriteLine("Intializing VersionedPhotoAlbum - Version 2.0.0.0");
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
