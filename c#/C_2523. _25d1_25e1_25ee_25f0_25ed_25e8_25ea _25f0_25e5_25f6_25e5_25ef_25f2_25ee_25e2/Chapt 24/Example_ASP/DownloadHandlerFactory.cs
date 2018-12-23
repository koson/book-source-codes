using System;
using System.Web;

namespace Example
{
	public class DownloadHandlerFactory : IHttpHandlerFactory
	{
		public IHttpHandler GetHandler(HttpContext context, string requestType, String url, String pathTranslated)
		{         
			IHttpHandler handlerToReturn = new DownloadTxtHandler();

//			// можно создавать различные обработчики для post, get и т.д.
//			switch (context.Request.RequestType.ToLower()) 
//			{
//				case "get": 
//					handlerToReturn = ...;
//				case "post":
//					handlerToReturn = ...;
//			}

			return handlerToReturn;
		}

		public void ReleaseHandler(IHttpHandler handler)
		{
		}

		public bool IsReusable
		{
			get
			{
				// Для использования пула, нужно вернуть true
				return false;
			}
		}
	
	}
}
