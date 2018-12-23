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

namespace AJAX_Test1
{
        public class Data1 : System.Web.UI.Page
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
                        Response.Write(string.Format("������� �����: {0}", DateTime.Now));

                        Response.End();
                }

                #region Web Form Designer generated code
                override protected void OnInit(EventArgs e)
                {
                        InitializeComponent();
                        base.OnInit(e);
                }
        
                private void InitializeComponent()
                {    
                        this.Load += new System.EventHandler(this.Page_Load);
                }
                #endregion
        }
}
