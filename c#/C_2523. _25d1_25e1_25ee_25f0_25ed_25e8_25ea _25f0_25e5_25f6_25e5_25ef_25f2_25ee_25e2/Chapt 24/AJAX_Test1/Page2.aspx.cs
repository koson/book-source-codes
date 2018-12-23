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
        public class Page2 : System.Web.UI.Page
        {
                protected System.Web.UI.WebControls.Button btnGetData;
                protected System.Web.UI.WebControls.Label labelData;
        
                private void Page_Load(object sender, System.EventArgs e)
                {
                        RegisterScripts();
                        btnGetData.Attributes.Add("OnClick", "return GetAJAXData();");
                }

                private const string strGetAJAXData = @"
                   <script language='javascript'>
                        var xRequest; 
                        function GetAJAXData() 
                        {
                                if (window.XMLHttpRequest) 
                                {
                                        xRequest= new XMLHttpRequest();
                                }
                                else 
                                {
                                        if (typeof ActiveXObject != 'undefined')
                                        {
                                                xRequest= new ActiveXObject('Microsoft.XMLHTTP');
                                        }
                                }
                
                                if (xRequest != null)
                                {
                                        xRequest.onreadystatechange = ProcessResponse;
                                        xRequest.open('GET', '%DATA_PAGE%',††true); 
                                        xRequest.send(null);††††††††    
                                }
                
                                return false;
                        }
                        function ProcessResponse()
                        {
                                if(xRequest.readyState == 4) 
                                {
                                        if(xRequest.status == 200)
                                        {
                                                var retval = xRequest.responseText;
                                                if (xRequest.responseXML.loadXML(xRequest.responseText))
                                                {
                                                        var value  = xRequest.responseXML.getElementsByTagName('value')[0].firstChild.data;
                                                        if (document.getElementById('%LABEL_NAME%') != null) 
                                                                document.getElementById('%LABEL_NAME%').innerHTML = value;
                                                }
                                                else
                                                {
                                                        alert('Œ¯Ë·Í‡ ‚ XML ÓÚ‚ÂÚÂ!');
                                                }
                                        }
                                        else
                                        {
                                                alert('Œ¯Ë·Í‡ ÔÓÎÛ˜ÂÌËˇ ‰‡ÌÌ˚ı!');
                                        }   
                                }
                        }
                        </script>
                ";

                private void RegisterScripts()
                {
                        if (!Page.IsStartupScriptRegistered("GetAJAXData")) 
                        {
                                string script = strGetAJAXData;
                                script = script.Replace("%DATA_PAGE%", "Data2.aspx");
                                script = script.Replace("%LABEL_NAME%", labelData.ClientID);

                                Page.RegisterStartupScript("GetAJAXData", script); 
                        }
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
