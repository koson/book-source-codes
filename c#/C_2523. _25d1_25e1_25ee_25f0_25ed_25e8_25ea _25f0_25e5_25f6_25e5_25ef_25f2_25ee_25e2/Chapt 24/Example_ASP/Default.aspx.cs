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
	public class DefaultPage : Page, IHttpHandler
	{
		protected LinkButton OpenInNewWindow;
		protected LinkButton btnException;
		protected LinkButton btnGException;
		protected LinkButton btnServerTransfer;

		// ���� ���� ��������� ��������� ��������� ��������� ����������
		// � ���������� ��� ����� ����������� ���������� ����������
		bool LocalExceptionHandling = false;

		private void Page_Load(object sender, EventArgs e)
		{
			OpenInNewWindow.Attributes["OnClick"] = "wnd('SetFocusScript.aspx'); return false;";
		}

		public new void ProcessRequest(HttpContext ctxt) 
		{
			// ������������ ��������?
			if (LocalExceptionHandling)
			{
				try 
				{
					base.ProcessRequest(ctxt);
				}
				catch (Exception e) 
				{
					ctxt.Response.Redirect(string.Format("ErrorHandling.aspx?msg={0}", e.Message));
				}
			}
			else
			{
				// �� ����� ������������ ������
				base.ProcessRequest(ctxt);
			}
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
			this.btnServerTransfer.Click += new System.EventHandler(this.btnServerTransfer_Click);
			this.btnException.Click += new System.EventHandler(this.btnException_Click);
			this.btnGException.Click += new System.EventHandler(this.btnGException_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnServerTransfer_Click(object sender, EventArgs e)
		{
			Server.Transfer("ShowPaths.aspx");		
		}

		private void btnException_Click(object sender, EventArgs e)
		{
			LocalExceptionHandling = true;
			throw new Exception("������ ������ Exception!");
		}

		private void btnGException_Click(object sender, System.EventArgs e)
		{
			LocalExceptionHandling = false;
			throw new Exception("������ ������ Exception!");
		}
	}
}
