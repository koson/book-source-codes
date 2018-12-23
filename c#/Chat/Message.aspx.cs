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

namespace ChatApp
{
	/// <summary>
	/// Summary description for Message.
	/// </summary>
	public class Message : System.Web.UI.Page
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			Response.Buffer = false;
			Response.Write("<table width='100%'><tr width='100%'><td align='center'><font face='verdana' size='1' color='black'>This Chat Application is prototype of Streaming of Data from Server to Client.</font></td></tr>");
			Response.Write("<tr width='100%'><td align='left'><font face='verdana' size='1' color='red'>This Chat Application should work with any firewall restrictions--- since It is not using");
			Response.Write("the follwowing Techniques which are used in any of the normal Chat App and each of the Techniques is restricted in some or the other formats in corporate Networrks");
			Response.Write("1.) Meta Refresh Tag 2.) Activex Component</font></td></tr></table>");
			Response.Write("<hr/>");
			Chat objChat  = new Chat();
			objChat.Display();
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
