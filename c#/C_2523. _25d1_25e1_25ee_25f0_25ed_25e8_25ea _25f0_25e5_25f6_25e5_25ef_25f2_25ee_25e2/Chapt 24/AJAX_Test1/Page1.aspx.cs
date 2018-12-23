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
        public class Page1 : System.Web.UI.Page
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
                                                if (document.getElementById('%LABEL_NAME%') != null)
                                                        document.getElementById('%LABEL_NAME%').innerHTML = retval;
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
                                script = script.Replace("%DATA_PAGE%", "Data1.aspx");
                                script = script.Replace("%LABEL_NAME%", labelData.ClientID);

                                Page.RegisterStartupScript("GetAJAXData", script); 
                        }
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
