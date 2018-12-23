namespace VisualCSharpBlueprint
{
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

	/// <summary>
	///		Summary description for photos.
	/// </summary>
	public class photos : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.Image imgPhoto;
		protected System.Web.UI.WebControls.DropDownList cboPhotoList;
		protected System.Web.UI.WebControls.Button cmdChangePhoto;
		protected System.Web.UI.WebControls.DropDownList DropDownList1;
	
		public photos()
		{
			Page.Init += new System.EventHandler(Page_Init);
		}

		protected void Page_Init(object sender, EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Windows Form Designer.
			//
			InitializeComponent();
		}

		#region Web Form Designer generated code
		/// <summary>
		///	Required method for Designer support - do not modify
		///	the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.cmdChangePhoto.Click += new System.EventHandler(this.cmdChangePhoto_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Page_Load(object sender, System.EventArgs e)
		{
		}

		protected void cmdChangePhoto_Click(object sender, System.EventArgs e)
		{
			imgPhoto.ImageUrl = "/VisualCSharpBlueprint/images/" + cboPhotoList.SelectedItem.Value;
		}
	}
}
