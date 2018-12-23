using System.Text;
using System.Web;

namespace Example
{
	public class DownloadTxtHandler: IHttpHandler
	{
		bool IHttpHandler.IsReusable
		{
			get
			{
				return true;
			}
		}

		void IHttpHandler.ProcessRequest(HttpContext context)
		{
			string source_str  = "<b>Test</b>";
			byte[] source_data = Encoding.UTF8.GetBytes(source_str);

			context.Response.ContentType = "text/plain";
			context.Response.AddHeader("Content-Disposition", "attachment; filename='test.txt'");
			context.Response.OutputStream.Write(source_data, 0, source_data.Length);
			context.Response.End();
		}
	}
}



