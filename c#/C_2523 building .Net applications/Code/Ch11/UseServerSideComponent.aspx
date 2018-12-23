<%@ Page language="c#" Codebehind="UseServerSideComponent.aspx.cs" AutoEventWireup="false" Inherits="VisualCSharpBlueprint.UseServerSideComponent" %>

<HTML>
	<HEAD>
		<meta name=vs_targetSchema content="Internet Explorer 5.0">
		<meta name="GENERATOR" Content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" Content="C#">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form method="post" runat="server">
			<asp:ListBox id=lstPhotos style="Z-INDEX: 101; LEFT: 6px; POSITION: absolute; TOP: 8px" runat="server" Width="144" Height="188">
			</asp:ListBox>
			<asp:Button id=cmdGetPhotos style="Z-INDEX: 102; LEFT: 167px; POSITION: absolute; TOP: 11px" runat="server" Text="Get Photos">
			</asp:Button>
		</form>
	</body>
</HTML>
