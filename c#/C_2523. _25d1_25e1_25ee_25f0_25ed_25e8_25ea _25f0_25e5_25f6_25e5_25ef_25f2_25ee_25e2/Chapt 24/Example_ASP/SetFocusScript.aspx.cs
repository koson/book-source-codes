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

namespace Example
{
	/// <summary>
	/// Summary description for SetFocusScript.
	/// </summary>
	public class SetFocusScript : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.TextBox TextBox1;
		protected System.Web.UI.WebControls.TextBox TextBox2;
		protected System.Web.UI.WebControls.TextBox TextBox3;

		private void SetFocus(string ControlName)
		{
			// Добавляем функцию установки фокуса 
			System.Text.StringBuilder sb = new System.Text.StringBuilder(""); 
			sb.Append("<script language=javascript>"); 
			sb.Append("function setFocus(ctl) {"); 
			sb.Append(" if (document.all[ctl] != null)"); 
			sb.Append(" {document.all[ctl].focus();}"); 
			sb.Append("}"); 
			// Добавляем вызов функции установки фокуса 
			sb.Append("setFocus('"); 
			sb.Append(ControlName); 
			sb.Append("');<"); 
			sb.Append("/"); 
			sb.Append("script>"); 
			// Регистрируем клиентский скрипт 
			if (!Page.IsStartupScriptRegistered("InputFocusHandler")) 
				Page.RegisterStartupScript("InputFocusHandler", sb.ToString()); 
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Вызываем установку фокуса на TextBox2
			SetFocus(TextBox2.ClientID);
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
