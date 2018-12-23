using System.IO;

   [WebMethod]
   public string GetFavoritesList(int UserID) {
      string sServerPath = Server.MapPath("");

      // Here you would make a database call to get XML.
      // To simplify, just put a file called Favorites1.xml
      // in same directory as this web service.
      string sFilePath = sServerPath+ "\\" + "Favorites1.xml";
      string sList = GetXMLAsString(sFilePath);

     return sList;
   }
   private string GetXMLAsString(string XMLDocumentPath) {
      FileStream fsFavorites = new FileStream
         (XMLDocumentPath,FileMode.Open,FileAccess.Read);
      StreamReader srFavorites = new StreamReader(fsFavorites);

      return srFavorites.ReadToEnd();
   }
