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
	/// Summary description for Cookies.
	/// </summary>
	public class Cookies : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.TextBox TextBox1;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.Button Button2;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
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
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.Button2.Click += new System.EventHandler(this.Button2_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Button1_Click(object sender, System.EventArgs e)
		{
			// Получаем объект cookie
			HttpCookie cookie = Request.Cookies["test_cookie"];
			// Если cookie существует читаем запись test_text
			if (cookie!=null)
				TextBox1.Text = cookie["test_text"];
		}

		private void Button2_Click(object sender, System.EventArgs e)
		{
			// Пробуем получить объект cookie
			HttpCookie cookie = Request.Cookies["test_cookie"];
			// Если такого объекта еще нет, то создаем
			if (cookie==null)
				cookie = new HttpCookie("test_cookie");
			// Записываем test_text
			cookie["test_text"] = TextBox1.Text;
			// Время жизни cookie - один год
			cookie.Expires = DateTime.Now.AddYears(1);
			// Сохраняем cookie
			Response.Cookies.Add(cookie);
		}
	}
}
