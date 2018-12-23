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

namespace NoAJAX
{
	/// <summary>
	/// Summary description for Data.
	/// </summary>
	public class Data : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label DataLabel;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			string str = string.Empty;
			switch (Request.QueryString["ID"]) 
			{
				case "1": str="X1";	break;
				case "2": str="X2";	break;
				case "3": str="X3";	break;
			}

			DataLabel.Text = str;
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
