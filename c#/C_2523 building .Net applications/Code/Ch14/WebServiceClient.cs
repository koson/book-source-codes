using System;
using WebServiceClient.localhost;

namespace WebServiceClient
{
	class Client
	{
		static void Main()
		{
			Console.Write ("Please give your userid (userids 1 and 2 exist): ");
			string sUserID = Console.ReadLine();
			
			Service1 myFavoritePhotos = new Service1();

			Console.WriteLine(myFavoritePhotos.GetPhotoList(int.Parse(sUserID)));
		}
	}
}
