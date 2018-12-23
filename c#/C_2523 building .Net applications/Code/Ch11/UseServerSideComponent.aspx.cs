namespace VisualCSharpBlueprint
{
	using System;
	using System.IO;
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
	///		Summary description for UseServerSideComponent.
	/// </summary>
	public class UseServerSideComponent : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button cmdGetPhotos;
		protected System.Web.UI.WebControls.ListBox lstPhotos;
	
		public UseServerSideComponent()
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
			this.cmdGetPhotos.Click += new System.EventHandler(this.cmdGetPhotos_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		protected void cmdGetPhotos_Click(object sender, System.EventArgs e)
		{

			DirectoryInfo dir = new DirectoryInfo(".");
			foreach (FileInfo f in dir.GetFiles("*.jpg")) 
			{
				String name = f.FullName;
				long size = f.Length;
				DateTime creationTime = f.CreationTime;


				ListItem li = new ListItem();
				li.Text = name;
				li.Value = size.ToString();
				lstPhotos.Items.Add( li );

			}


		}
	}
}
