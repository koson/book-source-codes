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
	///		Summary description for RespondToEvent.
	/// </summary>
	public class RespondToEvent : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button cmdTest;
		protected System.Web.UI.WebControls.TextBox txtEventMessage;
	
		public RespondToEvent()
		{
			Page.Init += new System.EventHandler(Page_Init);
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
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
			this.cmdTest.Click += new System.EventHandler(this.cmdTest_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		protected void cmdTest_Click(object sender, System.EventArgs e)
		{
			txtEventMessage.Text = "cmdTest_Click event was captured.";
		}
	}
}
