using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Xml;

namespace AJAX_Test1
{
        public class Data2 : System.Web.UI.Page
        {
                private void Page_Load(object sender, System.EventArgs e)
                {
                        // �� ���������
                        Response.Cache.SetNoStore();
                        // �� ��������� � ������� ��������
                        Response.Cache.SetAllowResponseInBrowserHistory(false);
                        // �� ���������� �� �������
                        Response.Cache.SetNoServerCaching();

                        Response.Clear();

                        // ��������� � ���� XML
                        string strXmlFormat =  @"<response><value>{0}</value></response>";
                        string strXmlValue  = string.Format(strXmlFormat, DateTime.Now.Second);

                        XmlDocument result = new XmlDocument();
                        result.LoadXml(strXmlValue);
                        result.Save(Response.OutputStream);

                        Response.End();
                }

                #region Web Form Designer generated code
                override protected void OnInit(EventArgs e)
                {
                        //
                        // CODEGEN: This call is required by the ASP.NET Web Form Designer.
                        //
                        InitializeComponent();
                        base.OnInit(e);
                }
                
                /// <summary>
                /// Required method for Designer support - do not modify
                /// the contents of this method with the code editor.
                /// </summary>
                private void InitializeComponent()
                {    
                        this.Load += new System.EventHandler(this.Page_Load);
                }
                #endregion
        }
}
