<%@ Page language="c#" Codebehind="Cookies.aspx.cs" AutoEventWireup="false" Inherits="Example.Cookies" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 

<HTML>
	<HEAD>
		<title>WebForm1</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<asp:TextBox id="TextBox1" style="Z-INDEX: 101; LEFT: 24px; POSITION: absolute; TOP: 16px" runat="server"
				Width="368px"></asp:TextBox>
			<asp:Button id="Button1" style="Z-INDEX: 102; LEFT: 400px; POSITION: absolute; TOP: 16px" runat="server"
				Text="Read cookies"></asp:Button>
			<asp:Button id="Button2" style="Z-INDEX: 103; LEFT: 528px; POSITION: absolute; TOP: 16px" runat="server"
				Text="Write cookies"></asp:Button>
		</form>
	</body>
</HTML>
