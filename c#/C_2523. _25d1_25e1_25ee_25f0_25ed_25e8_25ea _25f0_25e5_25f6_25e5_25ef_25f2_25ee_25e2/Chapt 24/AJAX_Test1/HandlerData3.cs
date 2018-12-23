using System;
using System.Web;
using System.Xml;

namespace AJAX_Test1
{
        public class HandlerData3 : IHttpHandler
        {
                public HandlerData3()
                {
                }

                public void ProcessRequest(HttpContext context)
                {
                        context.Response.Buffer = false;
                        context.Response.ClearContent();
                        context.Response.ClearHeaders();

                        // Сохраняем в виде XML
                        string strXmlFormat =  @"<response><value>{0}</value></response>";
                        string strXmlValue  = string.Format(strXmlFormat, DateTime.Now.Second);

                        XmlDocument result = new XmlDocument();
                        result.LoadXml(strXmlValue);
                        result.Save(context.Response.OutputStream);

                        context.Response.End();
                }

                public bool IsReusable
                {
                        get { return true; }
                }
        }
}