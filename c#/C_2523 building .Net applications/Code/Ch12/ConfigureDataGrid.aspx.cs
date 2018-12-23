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
using System.Data.SqlClient;

namespace VisualCSharpBlueprint
{
	/// <summary>
	/// Summary description for DisplayDataGrid.
	/// </summary>
	public class ConfigureDataGrid : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.DataGrid dgdTitles;
	
		public ConfigureDataGrid()
		{
			Page.Init += new System.EventHandler(Page_Init);
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			SqlConnection cnPubs = new SqlConnection("server=(local);uid=sa;pwd=;database=pubs");

			SqlDataAdapter daTitles = new SqlDataAdapter("select title, notes, price, pubdate from titles", cnPubs);

			DataSet dsTitles = new DataSet();
			daTitles.Fill(dsTitles, "titles");

			dgdTitles.DataSource=dsTitles.Tables["titles"].DefaultView;
			dgdTitles.DataBind();

		}

		private void Page_Init(object sender, EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
		}

		#region Web Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void dgdTitles_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		}
	}
}
