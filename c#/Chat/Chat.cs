using System;
using System.Web;
using System.Threading;

using System.Text;
using System.IO;
using System.Xml;

namespace ChatApp
{
	public class Chat
	{

		private String From  = string.Empty;

		/// <summary>
		/// This method isa endless loop method
		/// with thread corrosponding to current Response sleeping for "x" time.
		/// This way the connection between the client and the browser is kept open
		/// and any updates on the server are send to client.
		/// </summary>
	
		public void Display()
		{
			From  = HttpContext.Current.Session.SessionID;
			if(From.Length > 5) From  = From.Substring(0,5);
			bool Flag = true;
			
			while(Flag)
			{
				XmlDocument ChatDoc = (XmlDocument)HttpContext.Current.Application["ChatDoc"];
				if(ChatDoc!=null)
				{
					String SessId = HttpContext.Current.Session.SessionID;
					int Count = ChatDoc.DocumentElement.ChildNodes.Count;
					for(int z=0;z<Count;z++)
					{
						XmlNode Each  = ChatDoc.DocumentElement.ChildNodes.Item(z);
						XmlNodeList ToList = Each.SelectNodes("TO");
						bool Found  = false;
						for(int x=0;x< ToList.Count;x++)
						{
							XmlNode EachTo = ToList.Item(x);
							
							if(EachTo.InnerText==SessId)Found  = true;
						}
					
						// new data is found
						// for the current client
						// so send it to the browser
						if(Found==false)
						{
							XmlNode MsgNode = Each.SelectSingleNode("TXT");
							XmlNode FromNode = Each.SelectSingleNode("FROM");
							if(MsgNode!=null)
							{
								HttpContext.Current.Response.Write("<table width='100%'><tr width='100%'>");
								HttpContext.Current.Response.Write("<td width='10%' align='left'><font color='gray' size='1' face='verdana'>"+FromNode.InnerText+"</font><font color='black' size='1'><strong>:</strong></font></td>");
								string FontColor= string.Empty;
								
								// some logic to identify  - 
								// own content and other's content.
								if(FromNode.InnerText== From)FontColor = "black";
								else FontColor = "red";
								HttpContext.Current.Response.Write("<td align='left'><font face='verdana' size='1' color='"+FontColor+"'>" +MsgNode.InnerText + "</font></td></tr></table>");
								XmlDocumentFragment DocFrag = ChatDoc.CreateDocumentFragment();
								// Marked new data send to the client
								// so that next time you do not resend 
								// it again.
								DocFrag.InnerXml="<TO>"+SessId+"</TO>";
								Each.AppendChild(DocFrag);
							}

						}
						
						
					} 
				}
				HttpContext.Current.Application["ChatDoc"]= ChatDoc;
				// sleep the current thread for "x" time.
				Thread.Sleep(2000);
			}
		}
		/// <summary>
		/// currently posted data is saved in to application object in XML  format
		/// for the sake of simplicity
		/// but this data can be saved as per the business requirements
		/// </summary>
		public void PostMessage()
		{
			String From  = string.Empty;
			From  = HttpContext.Current.Session.SessionID;
			if(From.Length > 5) From  = From.Substring(0,5);
			XmlDocument ChatDoc = (XmlDocument)HttpContext.Current.Application["ChatDoc"];
			if(ChatDoc==null)
			{
				ChatDoc = new XmlDocument();
				ChatDoc.LoadXml("<CHAT/>");
				HttpContext.Current.Application["ChatDoc"]=ChatDoc;
			}
			XmlDocumentFragment DocFrag = ChatDoc.CreateDocumentFragment();
			String Txt = HttpContext.Current.Request.Form.Get("msg");
			DocFrag.InnerXml = "<MESSAGE><TXT>"+Txt+"</TXT><FROM>"+From+"</FROM></MESSAGE>";
			ChatDoc.DocumentElement.AppendChild(DocFrag);
			HttpContext.Current.Application["ChatDoc"]=ChatDoc;
		}
	}
	
}
