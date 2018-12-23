<%@ Page language="c#" Codebehind="RespondToEvent.aspx.cs" AutoEventWireup="false" Inherits="VisualCSharpBlueprint.RespondToEvent" %>

<HTML>
	<HEAD>
		<meta name=vs_targetSchema content="Internet Explorer 5.0">
		<meta name="GENERATOR" Content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" Content="C#">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form method="post" runat="server">
			<asp:TextBox id=txtEventMessage style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 8px" runat="server" Width="347px" Height="24px">
			</asp:TextBox>
			<asp:Button id=cmdTest style="Z-INDEX: 102; LEFT: 11px; POSITION: absolute; TOP: 42px" runat="server" Text="Test">
			</asp:Button>
		</form>
	</body>
</HTML>
