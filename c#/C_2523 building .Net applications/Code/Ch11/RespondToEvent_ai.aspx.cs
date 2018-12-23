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
	///		Summary description for RespondToEvent_ai.
	/// </summary>
	public class RespondToEvent_ai : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Image imgMain;
		protected System.Web.UI.WebControls.Label lblGreeting;
		protected System.Web.UI.WebControls.DropDownList cboPhotoList;
	
		public RespondToEvent_ai()
		{
			Page.Init += new System.EventHandler(Page_Init);
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			imgMain.ImageUrl = "/VisualCSharpBlueprint/images/steamboatwillie.gif";
			lblGreeting.Text = "Mickey on the Boat";
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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
